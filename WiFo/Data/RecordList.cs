using System;
using System.Collections.Generic;

namespace WiFo.Data
{
	/// <summary>
	/// <para>Represents a special-purpose enumerable list of <see cref="Record"/> objects.</para>
	/// </summary>
	/// <remarks>
	/// <para>The list only supports addition and purging, and individual items cannot be deleted from it.</para>
	/// <para>
	/// Adding elements is performed in batches rather than single items. This behavior is
	/// optimized for WiFo client to fetch and consume data efficiently from a server.
	/// </para>
	/// </remarks>
	
	public class RecordList : IEnumerable<Record>, System.Collections.IEnumerable
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="RecordList" /> with the specified capacity.
		/// </summary>
		/// <remarks>
		/// <para>
		/// The capacity specified in this constructor is the maximum desired number of items that can be
		/// stored in this <see cref="RecordList"/> instance. The list will never hold more items than this
		/// capacity.
		/// </para>
		/// <para>
		/// After reaching the capacity, the list turns into a ring buffer, and adding more items causes older
		/// records to be purged. Purging old records is performed in batches. Therefore, a ring buffer typically
		/// holds less than the capacity.
		/// </para>
		/// <para>
		/// The design of the <see cref="RecordList"/> class and typical batch size prohibits capacities of less
		/// than 512. To comply with this, this constructor ignores lower capacities specified, using 512 instead.
		/// </para>
		/// </remarks>
		/// <param name="capacity">The desired capacity of the list.</param>
		public RecordList(int capacity)
		{
			capacity = Math.Max(512, capacity);
			this.capacity = capacity;
			this.listCount = 0;
		}

		/// <summary>
		/// Gets the <see cref="Record"/> at the specified index.
		/// </summary>
		/// <param name="index">The zero-based index of the element to get.</param>
		/// <returns>The record at the specified index.</returns>
		public virtual Record this[int index]
		{
			get
			{
				int pageIndex = index / pageSize, pageOffset = index % pageSize;
				return pages[pageIndex][pageOffset];
			}
		}

		/// <summary>
		/// Gets the capacity of this instance.
		/// </summary>
		public int Capacity
		{
			get
			{
				return capacity;
			}
		}

		/// <summary>
		/// Gets the number of items currently in the list.
		/// </summary>
		/// <remarks>
		/// This value is equal to <see cref="Capacity" />, but it is typically less (see <see cref="RecordList(int)" />).
		/// </remarks>
		public virtual int Count
		{
			get { return listCount; }
		}

		/// <summary>
		/// Gets the first record in the list.
		/// </summary>
		/// <remarks>
		/// To gain the best performance regardless of specific implementation details in current and future
		/// releases, it is recommended to use this property instead of using the subscript operator when
		/// accessing the first element of the list.
		/// </remarks>
		public Record FirstRecord
		{
			get
			{
				return this[0];
			}
		}

		/// <summary>
		/// Gets the last record in the list.
		/// </summary>
		/// <remarks>
		/// To gain the best performance regardless of specific implementation details in current and future
		/// releases, it is recommended to use this property instead of using the subscript operator when
		/// accessing the last element of the list.
		/// </remarks>
		public Record LastRecord
		{
			get
			{
				return this[Count - 1];
			}
		}

		/// <summary>
		/// Appends all items in the given range to the list.
		/// </summary>
		/// <param name="records">The enumerable range of elements to add.</param>
		/// <remarks>
		/// This method is intended for internal use, and should not be called by extensions.
		/// </remarks>
		/// <seealso cref="WiFo.Extensibility.IExtension" />
		public virtual void AppendAll(IEnumerable<Record> records)
		{
			if (pages == null)
			{
				pages = new List<RecordPage>();
				pages.Add(new RecordPage(pageSize));
			}

			RecordPage page = pages[pages.Count - 1];

			foreach (Record record in records)
			{
				if (page.Count == 0 || record.Time > page.LastRecord.Time)
				{
					bool full = page.Add(record);
					listCount++;

					if (full)
					{
						RecordPage newPage = new RecordPage(pageSize);

						if (listCount > capacity)
							pages.RemoveAt(0);

						pages.Add(newPage);
						page = newPage;
					}
				}
			}
		}

		/// <summary>
		/// Clears this list, purging all records.
		/// </summary>
		public virtual void Clear()
		{
			if(pages != null)
				pages.RemoveRange(1, pages.Count - 1);

			listCount = 0;
		}

		/// <summary>
		/// Returns a <see cref="RecordList"/> instance, containing a subrange of the records in the
		/// current instance.
		/// </summary>
		/// <remarks>
		/// You may assume that the new list allcoates constant memory, which is independent of the number
		/// of items or the capacity of the current list.
		/// </remarks>
		/// <param name="startIndex">The index which marks the beginning of the new list.</param>
		/// <param name="endIndex">The end of the new list (exclusive of this index).</param>
		/// <returns>The cropped list.</returns>
		public RecordList Crop(int startIndex, int endIndex)
		{
			return new CroppedRecordList(this, startIndex, endIndex);
		}

		/// <summary>
		/// Finds the next record in the list where the masked value of the <see cref="State"/> property
		/// is changed.
		/// </summary>
		/// <param name="startIndex">The starting index.</param>
		/// <param name="mask">The mask to be tracked.</param>
		/// <returns>The index of the next change.</returns>
		public int NextChange(int startIndex, uint mask)
		{
			if (startIndex < 0)
				return -1;

			uint comp = this[startIndex].State & mask;

			for (int i = startIndex + 1; i < Count; i++)
				if (comp != (this[i].State & mask))
					return i;

			return -1;
		}

		/// <summary>
		/// Searching backwards, finds the next record in the list where the masked value of the <see cref="State"/>
		/// property is changed.
		/// </summary>
		/// <param name="startIndex">The starting index.</param>
		/// <param name="mask">The mask to be tracked.</param>
		/// <returns>The index of the next change.</returns>
		public int PrevChange(int startIndex, uint mask)
		{
			if (startIndex < 0)
				return -1;

			uint comp = (this[startIndex].State & mask);

			for (int i = startIndex - 1; i >= 0; i--)
				if (comp != (this[i].State & mask))
					return i;

			return -1;
		}

		/// <summary>
		/// Returns an enumerator that iterates through the records in this list.
		/// </summary>
		/// <returns>An IEnumerator object that can be used to iterate through the collection.</returns>
		public virtual IEnumerator<Record> GetEnumerator()
		{
			foreach(RecordPage page in pages)
				foreach (Record record in page)
					yield return record;
		}

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			throw new NotImplementedException();
		}

		private const int pageSize = 512;
		private List<RecordPage> pages = null;
		private int listCount;
		private int capacity;
	}
}
