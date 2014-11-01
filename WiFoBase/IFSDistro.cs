using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using WiFo.Data;
using WiFo.Extensibility;
using WiFo.Extensibility.Settings;
using WiFo.UI;
using WiFoBase.Data;

namespace WiFoBase
{
	public class IFSDistro : IStudy, ISettingsContributor
	{
		[Browsable(false)]
		public string DisplayName
		{
			get
			{
				return "IFS Distribution";
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

		[Description("The size of the bins in microseconds to split time distribution into."), DisplayName("Bin Size"), DefaultValue(20)]
		public int BinSize
		{
			get
			{
				return binSize;
			}
			set
			{
				binSize = value;
			}
		}

		public void Perform(RecordList records, WiFo.UI.IWiFoContext wifo)
		{
			double[] ret;
			List<TXInfo> data = new List<TXInfo>();
			int minBin = int.MaxValue, maxBin = 0;
			int startIndex = 0;
			int f = 0;

			do
			{
				TXInfo info;
				startIndex = Utils.GetNextPacket(records, startIndex, records.Count - 1, out info);

				if (info != null)
				{
					int bin = (info.IFS - 10) / 20;

					if (bin < minBin)
						minBin = bin;
					if (bin > maxBin)
						maxBin = bin;
					if (info.IFS < 0)
						f++;

					data.Add(info);
				}
			}
			while (startIndex >= 0 && startIndex < records.Count);

			ret = new double[maxBin - minBin + 1];

			foreach (TXInfo info in data)
			{
				int bin = (info.IFS - 10) / 20 - minBin;
				ret[bin]++;
			}

			object[] xs = new object[ret.Length];

			for (int i = 0; i < xs.Length; i++)
				xs[i] = i + minBin;

			UserOutput
				.For(UserOutputTypes.BarPlot)
				.SetTitle(String.Format("IFS Distribution ({0} frames)", data.Count))
				.SetXValues(xs)
				.SetYValues(ret)
				.Execute(wifo);
		}

		public void Load(ISettings settings)
		{
			binSize = settings.GetInt("BinSize", 20);
		}

		public void Save(ISettings settings)
		{
			settings.Put("BinSize", binSize);
		}

		private int binSize;

	}
}
