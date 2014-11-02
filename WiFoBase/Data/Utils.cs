using System;
using System.Collections.Generic;
using WiFo.Data;

namespace WiFoBase.Data
{
	internal static class Utils
	{
		public static int GetNextPacket(RecordList records, int startIndex, int endIndex, out TXInfo info)
		{
			for (int i1 = startIndex; i1 < endIndex; i1++)
			{
				if ((records[i1].State & 512) == 0)
				{
					int i2 = records.NextChange(i1, 512);

					if (i2 >= 0)
					{
						int i3 = records.NextChange(i2, 512);

						if (i3 >= 0)
						{
							int diff = (int)records[i3].Time - (int)records[i2].Time;

							if (diff < 3000 && diff > 192)
							{
								int i4 = records.NextChange(i3, 256);

								info = new TXInfo();
								info.FrameDuration = diff;
								info.StartTime = records[i2].Time;
								info.IFS = (int)(records[i2].Time - records[i1].Time);

								if (i4 >= 0)
								{
									int i5 = records.NextChange(i4, 256);

									if (i5 >= 0)
									{
										info.ACKDuration = (int)(records[i5].Time - records[i4].Time);
										info.EndTime = records[i5].Time;
										return i5;
									}
								}

								info.EndTime = records[i3].Time;
								return i3;
							}
						}
					}
				}
			}

			info = null;
			return -1;
		}

		public static int NextChange(Record[] records, int startIndex, int mask)
		{
			if (startIndex < 0)
				return -1;

			int comp = (int)records[startIndex].State & mask;

			for (int i = startIndex + 1; i < records.Length; i++)
				if (comp != ((int)records[i].State & mask))
					return i;

			return -1;
		}

		public static int PrevChange(Record[] records, int startIndex, int mask)
		{
			if (startIndex < 0)
				return -1;

			int comp = (int)(records[startIndex].State & mask);

			for (int i = startIndex - 1; i >= 0; i--)
			{
				if (comp != (int)(records[i].State & mask))
					return i;
			}

			return -1;
		}
	}
}
