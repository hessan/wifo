using System;

namespace WiFo.Data
{
	/// <summary>
	/// Represents a single status change event, which contains a timestamp and the new value of the Broadcom bcm43xx status register.
	/// </summary>
	public sealed class Record : IComparable<Record>
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="Record"/> with given timestamp and status.
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

		/// <summary>
		/// Determines whether the specified object is a <see cref="Record"/> and is equal to the current object.
		/// </summary>
		/// <param name="obj">The other object.</param>
		/// <returns><b>true</b> if the specified object is equal to the current object; otherwise, <b>false</b>.</returns>
		public override bool Equals(object obj)
		{
			Record rec = obj as Record;

			if(rec == null)
				return false;

			return rec.time == time && rec.ifs_state == ifs_state;
		}

		/// <summary>
		/// Compares the current instance with another <see cref="Record"/> object and returns an integer that indicates
		/// whether the current instance precedes, follows, or occurs at the same time as the other object.
		/// </summary>
		/// <param name="other">The <see cref="Record"/> object to compare with this instance.</param>
		/// <returns>A value that indicates the relative order of the objects being compared.</returns>
		public int CompareTo(Record other)
		{
			if (other == null)
				return 1;

			if (time > other.time)
				return 1;

			if (time < other.time)
				return -1;

			return 0;
		}

		/// <summary>
		/// Serves as the hash function for <see cref="Record"/> objects.
		/// </summary>
		/// <returns>The hash code based on the timestamp.</returns>
		public override int GetHashCode()
		{
			return (int)time;
		}

		/// <summary>
		/// Returns a string that represents the current instance.
		/// </summary>
		/// <returns>A string that represents the current object.</returns>
		/// <value>This is typically the string representation of the <see cref="State"/> property.</value>
		public override string ToString()
		{
			return ifs_state.ToString();
		}

		private uint time;
		private uint ifs_state;
	}
}
