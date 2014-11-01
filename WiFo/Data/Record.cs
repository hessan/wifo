using System;

namespace WiFo.Data
{
	public sealed class Record : IComparable
	{
		private uint time;
		private uint ifs_state;

		public Record(uint time, uint state)
		{
			this.time = time;
			this.ifs_state = state;
		}

		public uint Time
		{
			get
			{
				return time;
			}
		}

		public uint State
		{
			get
			{
				return ifs_state;
			}
		}

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
