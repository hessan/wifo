namespace WiFo.Extensibility.Settings
{
	public interface ISettings
	{
		void Put(string key, int value);
		void Put(string key, long value);
		void Put(string key, float value);
		void Put(string key, double value);
		void Put(string key, string value);

		int GetInt(string key, int defaultValue = default(int));
		long GetLong(string key, long defaultValue = default(long));
		float GetFloat(string key, float defaultValue = default(float));
		double GetDouble(string key, double defaultValue = default(double));
		string GetString(string key, string defaultValue = default(string));
	}

	public interface ISettingsContributor
	{
		void Load(ISettings settings);
		void Save(ISettings settings);
	}
}
