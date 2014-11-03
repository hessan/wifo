using System;
using WiFo.Extensibility;
using WiFo.UI;

namespace WiFo.Data
{
	/// <summary>
	/// Represents an event timeline based on a list of <see cref="Record"/> objects.
	/// </summary>
	/// <remarks>
	/// You may look at this class as a wrapper for a record list, which provides time-based
	/// methods on the underlying data.
	/// </remarks>
	/// <seealso cref="Record"/>
	/// <seealso cref="RecordList"/>
	public sealed class RecordTimeline
	{
		/// <summary>
		/// Initializes a new instance of <see cref="RecordTimeline"/> with the specified
		/// <see cref="RecordList"/> as the underlying data.
		/// </summary>
		/// <param name="list">The records list to be used.</param>
		public RecordTimeline(RecordList list)
		{
			records = list;
		}

		/// <summary>
		/// Gets the timestamp for the most recent record in the underlying data.
		/// </summary>
		/// <seealso cref="Record"/>
		public uint EndTime
		{
			get
			{
				if (records.Count == 0)
					return 0;

				return records.LastRecord.Time;
			}
		}

		/// <summary>
		/// Gets the underlying record list.
		/// </summary>
		/// <seealso cref="RecordList"/>
		public RecordList Records
		{
			get
			{
				return records;
			}
		}

		/// <summary>
		/// Gets the timestamp for the oldest record in the underlying data.
		/// </summary>
		/// <seealso cref="Record"/>
		public uint StartTime
		{
			get
			{
				if (records.Count == 0)
					return 0;

				return records.FirstRecord.Time;
			}
		}

		/// <summary>
		/// Finds the index of the record that happens immediately after the specified timestamp.
		/// </summary>
		/// <param name="time">The time to precede the record's timestamp.</param>
		/// <returns>The index to the record if such record exists; otherwise the last record in the list.</returns>
		/// <seealso cref="Record"/>
		public int GetIndexAfter(uint time)
		{
			return Math.Min(records.Count - 1, GetIndexBefore(time) + 1);
		}

		/// <summary>
		/// Finds the index of the record that happens immediately before the specified timestamp.
		/// </summary>
		/// <param name="time">The time to succeed the record's timestamp.</param>
		/// <returns>The index to the record.</returns>
		/// <seealso cref="Record"/>
		public int GetIndexBefore(uint time)
		{
			uint startTime = StartTime;
			uint duration = EndTime - startTime + 1;
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

		/// <summary>
		/// Gets the effective state at the specified time.
		/// </summary>
		/// <remarks>
		/// The effective state corresponds to the record immediately preceding the specified timestamp.
		/// </remarks>
		/// <param name="time">The timestamp.</param>
		/// <returns>The effective state at the specified time.</returns>
		/// <seealso cref="State" />
		public uint GetStateAt(uint time)
		{
			return records[GetIndexBefore(time)].State;
		}

		/// <summary>
		/// Performs a study on the specified period of the underlying data, within the specified WiFo context.
		/// </summary>
		/// <param name="study">The study to be peformed.</param>
		/// <param name="startTime">The start time of the range.</param>
		/// <param name="endTime">The end time of the range.</param>
		/// <param name="wifo">The <see cref="IWiFoContext" /> instance on which input/output functions can be executed.</param>
		/// <seealso cref="IStudy" />
		/// <seealso cref="IWiFoContext" />
		public void PerformStudy(IStudy study, uint startTime, uint endTime, IWiFoContext wifo)
		{
			if (startTime > 0 && endTime > startTime)
			{
				int startIndex = GetIndexAfter(startTime), endIndex = GetIndexBefore(endTime);

				if (startIndex >= 0 && endIndex > startIndex)
					study.Perform(records.Crop(startIndex, endIndex), wifo);
			}
		}

		private RecordList records;

	}
}
