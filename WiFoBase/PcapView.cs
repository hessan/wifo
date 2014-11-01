using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using PacketDotNet;
using PacketDotNet.Ieee80211;
using SharpPcap;
using SharpPcap.LibPcap;
using WiFo.Data;
using WiFo.Extensibility;
using WiFo.Extensibility.Settings;
using WiFo.UI;
using WiFoBase.Data;

namespace WiFoBase
{
	public class PcapView : ITimelineView, ISettingsContributor
	{
		[Browsable(false)]
		public string DisplayName
		{
			get
			{
				return "Pcap Overlay";
			}
		}

		[Browsable(false)]
		public string Author
		{
			get
			{
				return "Hessan Feghhi";
			}
		}

		[Description("The MAC address of the WiFo access-point."), DisplayName("AP MAC Address")]
		public string APMacAddress
		{
			get
			{
				return apMACAddr;
			}
			set
			{
				if (value.Length == 8)
					apMACAddr = value.ToLower();
			}
		}

		public void OnSelected(RecordTimeline timeline, IWiFoContext wifo)
		{
			object res = UserInput
				.For(UserInputTypes.FileName)
				.SetTitle("Open Pcap File")
				.SetFilter("Capture Files|*.pcap;*.cap|All Files|*.*")
				.Execute(wifo);

			if (res != null)
			{
				CaptureFileReaderDevice device = new CaptureFileReaderDevice(res as string);
				RawCapture capture;
				int result;
				device.Open();

				int offset = 0;

				do
				{
					result = device.GetNextPacket(out capture);

					if (result == 1)
					{
						if (capture.LinkLayerType == PacketDotNet.LinkLayers.Ieee80211_Radio)
						{
							RadioPacket radio = (RadioPacket)RadioPacket.ParsePacket(PacketDotNet.LinkLayers.Ieee80211_Radio, capture.Data);
							Packet packet = RadioPacket.GetInnerPayload(radio);

							if (packet is MacFrame)
							{
								MacFrame frame = (MacFrame)packet;
								PcapRecord record = new PcapRecord();

								record.Time = (uint)(((TsftRadioTapField)radio[RadioTapType.Tsft]).TimestampUsec & (ulong)uint.MaxValue);
								record.Size = frame.Bytes.Length;
								record.Rate = ((RateRadioTapField)radio[RadioTapType.Rate]).RateMbps;

								if (frame is DataFrame)
								{
									record.Type = FrameTypes.Data;
									record.SourceAddress = ((DataFrame)frame).SourceAddress.ToString();
									record.DestinationAddress = ((DataFrame)frame).DestinationAddress.ToString();
								}
								else if (frame is ManagementFrame)
								{
									if (frame is BeaconFrame)
									{
										BeaconFrame beacon = (BeaconFrame)frame;

										if (beacon.SourceAddress.ToString().Equals(apMACAddr))
										{
											offset = (int)((long)record.Time - (long)(beacon.Timestamp & (ulong)uint.MaxValue));
										}
									}

									record.Type = FrameTypes.Management;
									record.SourceAddress = ((ManagementFrame)frame).SourceAddress.ToString();
									record.DestinationAddress = ((ManagementFrame)frame).DestinationAddress.ToString();
								}
								else
								{
									record.Type = FrameTypes.Control;

									if (frame is RtsFrame)
									{
										record.SourceAddress = ((RtsFrame)frame).TransmitterAddress.ToString();
										record.DestinationAddress = ((RtsFrame)frame).ReceiverAddress.ToString();
									}
									else if (frame is CtsFrame)
									{
										record.SourceAddress = null;
										record.DestinationAddress = ((CtsFrame)frame).ReceiverAddress.ToString();
									}
									else if (frame is AckFrame)
									{
										record.SourceAddress = null;
										record.DestinationAddress = ((AckFrame)frame).ReceiverAddress.ToString();
									}

								}

								record.Duration = frame.Duration.Field;
								records.Add(record);
							}
						}

					}
				}
				while (result == 1);

				device.Close();

				foreach (PcapRecord record in records)
					record.Time = (uint)((int)record.Time - offset);
			}
		}

		public void OnUnSelected(RecordTimeline timeline, IWiFoContext wifo)
		{
			records.Clear();
		}

		public void Draw(RecordTimeline timeline, uint startTime, uint endTime, IWiFoCanvas g)
		{
			int startIndex = GetIndexBefore(startTime), endIndex = GetIndexBefore(endTime);

			for (int i = startIndex; i < endIndex; i++)
			{
				PcapRecord record = records[i];
				Color c = cData;

				if (record.Type == FrameTypes.Control)
					c = cCtrl;
				else if (record.Type == FrameTypes.Management)
					c = cMgmt;

				g.DrawShade(c, record.Time, record.Time + record.Duration);
			}
		}

		private int GetIndexBefore(uint time)
		{
			int lowerIndex = 0, upperIndex = records.Count - 1;

			while (upperIndex - lowerIndex > 1)
			{
				int mid = (upperIndex + lowerIndex) / 2;

				if (records[mid].Time >= time)
					upperIndex = mid;

				if (records[mid].Time <= time)
					lowerIndex = mid;
			}

			return lowerIndex;
		}

		private int GetIndexAfter(uint time)
		{
			return Math.Min(records.Count - 1, GetIndexBefore(time) + 1);
		}

		public void OnClick(uint timeStamp, IWiFoContext wifo)
		{
			int candidateIndex = GetIndexBefore(timeStamp);

			if (candidateIndex >= 0)
			{
				PcapRecord candidate = records[candidateIndex];

				if (candidate.Time <= timeStamp && candidate.Time + candidate.Duration >= timeStamp)
					UserOutput
						.For(UserOutputTypes.Results)
						.SetTitle("Frame Information")
						.SetResults(candidate)
						.Execute(wifo);
			}
		}

		public void Load(ISettings settings)
		{
			apMACAddr = settings.GetString("APMAC", "0014a46d08d0");
		}

		public void Save(ISettings settings)
		{
			settings.Put("APMAC", apMACAddr);
		}

		private List<PcapRecord> records = new List<PcapRecord>();
		private string apMACAddr;

		private static readonly Color cData = Color.FromArgb(100, 255, 0, 0);
		private static readonly Color cCtrl = Color.FromArgb(100, 0, 255, 0);
		private static readonly Color cMgmt = Color.FromArgb(100, 0, 100, 0);
	}
}
