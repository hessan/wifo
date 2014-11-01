using System;
using System.Collections.Generic;

namespace WiFo.Data
{
	public class RecordList : IEnumerable<Record>
	{
		private List<RecordPage> pages = null;
		private int listCount;
		private int capacity;

		private const int pageSize = 512;

		public RecordList(int capacity)
		{
			capacity = Math.Max(10, capacity);
			this.capacity = capacity;
			this.listCount = 0;
		}

		public Record FirstRecord
		{
			get
			{
				return this[0];
			}
		}

		public Record LastRecord
		{
			get
			{
				return this[Count - 1];
			}
		}

		public virtual int Count
		{
			get { return listCount; }
		}

		public virtual Record this[int ix]
		{
			get
			{
				int pageIndex = ix / pageSize, pageOffset = ix % pageSize;
				return pages[pageIndex][pageOffset];
			}
		}

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

		public virtual void Clear()
		{
			if(pages != null)
				pages.RemoveRange(1, pages.Count - 1);

			listCount = 0;
		}

		public int NextChange(int startIndex, int mask)
		{
			if (startIndex < 0)
				return -1;

			int comp = (int)this[startIndex].State & mask;

			for (int i = startIndex + 1; i < Count; i++)
				if (comp != ((int)this[i].State & mask))
					return i;

			return -1;
		}

		public int PrevChange(int startIndex, int mask)
		{
			if (startIndex < 0)
				return -1;

			int comp = (int)(this[startIndex].State & mask);

			for (int i = startIndex - 1; i >= 0; i--)
			{
				if (comp != (int)(this[i].State & mask))
					return i;
			}

			return -1;
		}

		public RecordList Crop(int startIndex, int endIndex)
		{
			return new CroppedRecordList(this, startIndex, endIndex);
		}

		public virtual IEnumerator<Record> GetEnumerator()
		{
			foreach(RecordPage page in pages)
				foreach (Record record in page)
					yield return record;
		}

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
	}
}
