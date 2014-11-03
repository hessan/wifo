using System.Collections.Generic;

namespace WiFo.Data
{
	/// <summary>
	/// Represents a page of records. This structure is used internally by the framework
	/// for performance optimization of <see cref="RecordList"/> operations.
	/// </summary>
	/// <remarks>
	/// Items can only be added and accessed internally. Deletion is not supported, as this class is an atomic
	/// pack of records, which is added to, or removed from a <see cref="RecordList"/>.
	/// </remarks>
	/// <seealso cref="Record"/>
	/// <seealso cref="RecordList"/>
	internal sealed class RecordPage : IEnumerable<Record>
	{
		/// <summary>
		/// Initializes a new instance of <see cref="RecordPage"/> with the given size.
		/// </summary>
		/// <remarks>
		/// A page cannot hold more records than the size specified.
		/// </remarks>
		/// <param name="size"></param>
		public RecordPage(int size)
		{
			records = new Record[size];
			count = 0;
		}

		/// <summary>
		/// Gets the record at the specified index.
		/// </summary>
		/// <param name="index">The zero-based index of the element to get or set.</param>
		/// <returns>The record at the specified index.</returns>
		/// <seealso cref="Record"/>
		public Record this[int index]
		{
			get
			{
				return records[index];
			}
		}

		/// <summary>
		/// Gets the number of records this page currently represents.
		/// </summary>
		public int Count
		{
			get
			{
				return count;
			}
		}

		/// <summary>
		/// Adds a record to this page.
		/// </summary>
		/// <param name="record">The record to be added.</param>
		/// <returns><b>true</b> if the page is full either before, or after the addition; otherwise <b>false</b>.</returns>
		public bool Add(Record record)
		{
			if (count < records.Length)
			{
				records[count++] = record;
				return count == records.Length;
			}

			return true;
		}

		/// <summary>
		/// Gets the last record on the page.
		/// </summary>
		/// <remarks>
		/// The main purpose of this method is to reduce the overhead of accessing the last record.
		/// </remarks>
		internal Record LastRecord
		{
			get
			{
				return records[count - 1];
			}
		}

		public IEnumerator<Record> GetEnumerator()
		{
			foreach (Record record in records)
				yield return record;
		}

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		private Record[] records;
		private int count;
	}
}
