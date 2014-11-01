using System;

namespace WiFo.Data
{
	/// <summary>
	/// Represents a single status change event, which contains a timestamp and the new value of the Broadcom bcm43xx status register.
	/// </summary>
	public sealed class Record : IComparable
	{
		private uint time;
		private uint ifs_state;

		/// <summary>
		/// Initializes a new instance of the Record with given timestamp and status.
		/// </summary>
		/// <param name="time">The timestamp associated with this record</param>
		/// <param name="state">The value of the status register that this record represents</param>
		public Record(uint time, uint state)
		{
			this.time = time;
			this.ifs_state = state;
		}

		/// <summary>
		/// Gets the timestamp associated with this record.
		/// </summary>
		public uint Time
		{
			get
			{
				return time;
			}
		}

		/// <summary>
		/// Gets the state value (taken from the hardware status register) associated with this record.
		/// </summary>
		public uint State
		{
			get
			{
				return ifs_state;
			}
		}

		/// <summary>
		/// Gets a value indicating whether the channel is free according to the State value.
		/// </summary>
		public bool IsChannelFree
		{
			get
			{
				return (ifs_state & 2) == 2;
			}
		}

		public int CompareTo(object obj)
		{
			Record other = obj as Record;

			if (other == null)
				return 1;

			if (time > other.time)
				return 1;

			if (time < other.time)
				return -1;

			return 0;
		}

		public override bool Equals(object obj)
		{
			Record rec = obj as Record;

			if(rec == null)
				return false;

			return rec.time == time && rec.ifs_state == ifs_state;
		}

		public override int GetHashCode()
		{
			return (int)time;
		}

		public override string ToString()
		{
			return ifs_state.ToString();
		}
	}
}
