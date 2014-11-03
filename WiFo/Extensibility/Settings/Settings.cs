namespace WiFo.Extensibility.Settings
{
	public interface ISettings
	{
		void Put(string key, int value);
		void Put(string key, long value);
		void Put(string key, float value);
		void Put(string key, double value);
		void Put(string key, string value);

		T Get<T>(string key, T defaultValue = default(T));
	}

	public interface ISettingsContributor
	{
		void Load(ISettings settings);
		void Save(ISettings settings);
	}
}
