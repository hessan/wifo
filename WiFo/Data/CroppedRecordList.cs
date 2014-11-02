using System;
using System.Collections.Generic;

namespace WiFo.Data
{
	/// <summary>
	/// Represents a cropped version of a <see cref="RecordList"/>, which is read-only.
	/// </summary>
	/// <remarks>
	/// A <b>CroppedRecordList</b> basically holds an instance of a list, with end pointers. Access requests to elements of a 
	/// cropped list are translated into corresponding elements of the original list. Modification methods are ignored.
	/// </remarks>
	internal sealed class CroppedRecordList : RecordList
	{
		public CroppedRecordList(RecordList records, int startIndex, int endIndex)
			: base(records.Count)
		{

			if (startIndex >= records.Count || endIndex >= records.Count)
				throw new IndexOutOfRangeException();

			if (startIndex > endIndex)
				throw new ArgumentException("Start index larger than end index.");

			this.records = records;
			this.startIndex = startIndex;
			this.endIndex = endIndex;
		}

		/// <summary>
		/// Gets the record at the specified index.
		/// </summary>
		/// <param name="index">The zero-based index of the element to get or set.</param>
		/// <returns>The record at the specified index.</returns>
		/// <exception cref="T:System.IndexOutOfRangeException">
		///	  <paramref name="index"/> is beyond the cropped range.</exception>
		public override Record this[int index]
		{
			get
			{
				if (index >= endIndex)
					throw new IndexOutOfRangeException();

				return records[index + startIndex];
			}
		}

		public override void AppendAll(IEnumerable<Record> records) { }

		public override void Clear() { }

		public override int Count
		{
			get
			{
				return endIndex - startIndex + 1;
			}
		}

		public override IEnumerator<Record> GetEnumerator()
		{
			for (int i = startIndex; i <= endIndex; i++)
				yield return records[i];
		}

		private RecordList records;
		private int startIndex, endIndex;

	}
}
