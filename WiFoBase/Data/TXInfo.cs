namespace WiFoBase.Data
{
	internal class TXInfo
	{
		public int ACKDuration
		{
			get
			{
				return ack_time;
			}
			set
			{
				ack_time = value;
			}
		}

		public uint EndTime
		{
			get
			{
				return end_time;
			}
			set
			{
				end_time = value;
			}
		}

		public int FrameDuration
		{
			get
			{
				return frame_time;
			}
			set
			{
				frame_time = value;
			}
		}

		public int IFS
		{
			get
			{
				return ifs;
			}
			set
			{
				ifs = value;
			}
		}

		public uint StartTime
		{
			get
			{
				return start_time;
			}
			set
			{
				start_time = value;
			}
		}

		private int ifs;
		private int frame_time;
		private int ack_time = -1;
		private uint start_time;
		private uint end_time;
	}
}
