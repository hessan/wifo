using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WiFo.Data
{
	internal class RecordPage : IEnumerable<Record>
	{
		private Record[] records;
		private int count;

		public RecordPage(int size)
		{
			records = new Record[size];
			count = 0;
		}

		public int Count
		{
			get
			{
				return count;
			}
		}

		public Record LastRecord
		{
			get
			{
				return records[count - 1];
			}
		}

		public Record this[int index]
		{
			get
			{
				return records[index];
			}
		}

		public bool Add(Record record)
		{
			if (count < records.Length)
			{
				records[count++] = record;
				return count == records.Length;
			}

			return true;
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
	}
}
