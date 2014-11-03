using System;
using System.Collections.Generic;
using System.IO;
using WiFo.Extensibility.Settings;

namespace WiFoUI.Logic
{
	internal class SettingsBundle : ISettings
	{
		private enum DataType
		{
			Nothing = 0,
			Int,
			Long,
			Float,
			Double,
			String
		}

		public SettingsBundle(string fileName)
		{
			this.fileName = fileName;
		}

		public void Dispose()
		{
			data.Clear();
		}

		public T Get<T>(string key, T defaultValue = default(T))
		{
			try
			{
				object obj = data[prefix + key];

				if (obj is T)
					return (T)obj;
			}
			catch { }

			return defaultValue;
		}

		public void Put(string key, int value)
		{
			data[prefix + key] = value;
		}

		public void Put(string key, long value)
		{
			data[prefix + key] = value;
		}

		public void Put(string key, float value)
		{
			data[prefix + key] = value;
		}

		public void Put(string key, double value)
		{
			data[prefix + key] = value;
		}

		public void Put(string key, string value)
		{
			data[prefix + key] = value;
		}

		public void Load()
		{
			FileStream stream = new FileStream(fileName, FileMode.Open, FileAccess.Read);
			BinaryReader reader = new BinaryReader(stream);
			DataType type = (DataType)reader.ReadInt32();

			while (type != DataType.Nothing)
			{
				string key = reader.ReadString();

				switch (type)
				{
					case DataType.Int:
						data[key] = reader.ReadInt32();
						break;
					case DataType.Long:
						data[key] = reader.ReadInt64();
						break;
					case DataType.Float:
						data[key] = reader.ReadSingle();
						break;
					case DataType.Double:
						data[key] = reader.ReadDouble();
						break;
					case DataType.String:
						data[key] = reader.ReadString();
						break;
				}

				type = (DataType)reader.ReadInt32();
			}

			stream.Close();
		}

		public void Save()
		{
			FileStream stream = new FileStream(fileName, FileMode.Create, FileAccess.Write);
			BinaryWriter writer = new BinaryWriter(stream);

			foreach (string key in data.Keys)
			{
				object obj = data[key];

				if (obj is int)
				{
					writer.Write((int)DataType.Int);
					writer.Write(key);
					writer.Write((int)obj);
				}
				else if (obj is long)
				{
					writer.Write((int)DataType.Long);
					writer.Write(key);
					writer.Write((long)obj);
				}
				else if (obj is float)
				{
					writer.Write((int)DataType.Float);
					writer.Write(key);
					writer.Write((float)obj);
				}
				else if (obj is double)
				{
					writer.Write((int)DataType.Double);
					writer.Write(key);
					writer.Write((double)obj);
				}
				else if (obj is string)
				{
					writer.Write((int)DataType.String);
					writer.Write(key);
					writer.Write((string)obj);
				}
			}

			writer.Write((int)DataType.Nothing);

			writer.Flush();
			stream.Close();
		}

		internal string prefix = "wifo_";

		private Dictionary<string, object> data = new Dictionary<string, object>();
		private string fileName;
	}
}
