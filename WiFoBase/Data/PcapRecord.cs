using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace WiFoBase.Data
{
	public enum FrameTypes
	{
		Management = 0,
		Control,
		Data
	}

	public class PcapRecord
	{
		private uint time;
		private FrameTypes type;
		private string srcAddr, dstAddr;
		private ushort duration;
		private int size;
		private double mbps;

		public uint Time
		{
			get
			{
				return time;
			}

			internal set
			{
				time = value;
			}
		}

		public FrameTypes Type
		{
			get
			{
				return type;
			}
			internal set
			{
				type = value;
			}
		}

		public string SourceAddress
		{
			get
			{
				return srcAddr;
			}
			internal set
			{
				srcAddr = value;
			}
		}

		public string DestinationAddress
		{
			get
			{
				return dstAddr;
			}
			internal set
			{
				dstAddr = value;
			}
		}

		public ushort Duration
		{
			get
			{
				return duration;
			}
			internal set
			{
				duration = value;
			}
		}

		public int Size
		{
			get
			{
				return size;
			}
			internal set
			{
				size = value;
			}
		}

		public double Rate
		{
			get
			{
				return mbps;
			}
			internal set
			{
				mbps = value;
			}
		}
	}
}
