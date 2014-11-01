using System;
using WiFo.Extensibility;
using WiFo.UI;

namespace WiFo.Data
{
	public class RecordTimeline
	{
		private RecordList records;

		public RecordTimeline(RecordList list)
		{
			records = list;
		}

		public RecordList Records
		{
			get
			{
				return records;
			}
		}

		public uint StartTime
		{
			get
			{
				if (records.Count == 0)
					return 0;

				return records.FirstRecord.Time;
			}
		}

		public uint EndTime
		{
			get
			{
				if (records.Count == 0)
					return 0;

				return records.LastRecord.Time;
			}
		}

		public uint GetStateAt(uint time)
		{
			return records[GetIndexBefore(time)].State;
		}

		public int GetIndexBefore(uint time)
		{
			uint startTime = StartTime;
			uint duration = EndTime - startTime;
			int count = records.Count;

			if (time < startTime)
				return 0;

			if (time > EndTime)
				return count - 1;

			int index = (int)((time - startTime) * count / duration);

			if (records[index].Time < time)
			{
				for (int i = index + 1; i < count; i++)
					if (records[i].Time > time)
						return i - 1;
			}
			else
			{
				for (int i = index - 1; i > 0; i--)
					if (records[i].Time < time)
						return i;
			}

			return index;
		}

		public int GetIndexAfter(uint time)
		{
			return Math.Min(records.Count - 1, GetIndexBefore(time) + 1);
		}

		public void PerformStudy(IStudy study, uint startTime, uint endTime, IWiFoContext wifo)
		{
			if (startTime > 0 && endTime > startTime)
			{
				int startIndex = GetIndexAfter(startTime), endIndex = GetIndexBefore(endTime);

				if (startIndex >= 0 && endIndex > startIndex)
					study.Perform(records.Crop(startIndex, endIndex), wifo);
			}
		}
	}
}
