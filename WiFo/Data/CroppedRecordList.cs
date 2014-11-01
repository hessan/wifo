using System;
using System.Collections.Generic;

namespace WiFo.Data
{
	internal class CroppedRecordList : RecordList
	{
		private RecordList records;
		private int startIndex, endIndex;

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

		public override int Count
		{
			get
			{
				return endIndex - startIndex + 1;
			}
		}

		public override Record this[int index]
		{
			get
			{
				if (index >= endIndex)
					throw new IndexOutOfRangeException();

				return records[index + startIndex];
			}
		}

		public override void Clear() { }

		public override void AppendAll(IEnumerable<Record> records) { }

		public override IEnumerator<Record> GetEnumerator()
		{
			for (int i = startIndex; i <= endIndex; i++)
				yield return records[i];
		}
	}
}
