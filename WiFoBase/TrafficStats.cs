using System;
using System.Collections.Generic;
using System.ComponentModel;
using WiFo.Data;
using WiFo.Extensibility;
using WiFo.UI;
using WiFoBase.Data;

namespace WiFoBase
{
	public class TrafficStats : IStudy
	{
		class TrafficInfo
		{
			internal TrafficInfo() { }

			[DisplayName("Frame Count"), Category("Results"), Description("The number of 802.11 frames detected in selected range")]
			public int FrameCount
			{
				get
				{
					return frameCount;
				}

				internal set
				{
					frameCount = value;
				}
			}

			[DisplayName("Frame Duration (avg)"), Category("Results"), Description("The average length of 802.11 frames in selected range")]
			public int AverageFrameDuration
			{
				get
				{
					return avgFrameDuration;
				}

				internal set
				{
					avgFrameDuration = value;
				}
			}

			[DisplayName("Burst Count (avg)"), Category("Results"), Description("Average TXOP burst frames")]
			public double BurstSize
			{
				get
				{
					return burstCount;
				}

				internal set
				{
					burstCount = value;
				}
			}

			[Category("Results"), Description("Throughput in terms of frames/s")]
			public double Throughput
			{
				get
				{
					return throughput;
				}

				internal set
				{
					throughput = value;
				}
			}

			private int frameCount = 0;
			private int avgFrameDuration = 0;
			private double burstCount = 0;
			private double throughput = 0;
		}

		[Browsable(false)]
		public string Author
		{
			get
			{
				return "Hessan Feghhi";
			}
		}

		[Browsable(false)]
		public string DisplayName
		{
			get
			{
				return "Traffic Statistics";
			}
		}

		public void Perform(RecordList records, IWiFoContext wifo)
		{
			TXInfo info;
			int startIndex = 0;
			int totalLength = 0, totalAck = 0, frameCount = 0, ackCount = 0;
			int burstCounter = 0, totalBurstCount = 0;
			bool inburst = false;
			uint savedTime = 0;

			TrafficInfo tinfo = new TrafficInfo();

			do
			{
				startIndex = Utils.GetNextPacket(records, startIndex, records.Count, out info);

				if (info != null)
				{
					totalLength += info.FrameDuration;
					frameCount++;

					if (info.ACKDuration >= 0)
					{
						ackCount++;
						totalAck += info.ACKDuration;

						if (savedTime > 0)
						{
							if (info.StartTime - savedTime < 25)
							{
								if (!inburst)
								{
									inburst = true;
									burstCounter++;
									totalBurstCount++;
								}

								totalBurstCount++;
							}
							else inburst = false;
						}

						savedTime = info.EndTime;
					}
				}
			}
			while (startIndex >= 0 && startIndex < records.Count);

			if (frameCount > 0)
				tinfo.AverageFrameDuration = totalLength / frameCount;

			tinfo.FrameCount = frameCount;
			tinfo.Throughput = frameCount / (double)Math.Max(1, records.LastRecord.Time - records.FirstRecord.Time) * 1000000;

			if (burstCounter > 0)
				tinfo.BurstSize = (double)totalBurstCount / (double)burstCounter;

			if (tinfo.BurstSize == 0 && frameCount > 0)
				tinfo.BurstSize = 1;

			UserOutput
				.For(UserOutputTypes.Results)
				.SetTitle("Traffic Results")
				.SetResults(tinfo)
				.Execute(wifo);
		}
	}
}
