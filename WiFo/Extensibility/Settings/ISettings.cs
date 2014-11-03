namespace WiFo.Extensibility.Settings
{
	/// <summary>
	/// Represents a bundle of persistent settings.
	/// </summary>
	/// <seealso cref="ISettingsContributor"/>
	public interface ISettings
	{
		/// <summary>
		/// Puts a setting of type <see cref="T:System.Int32" /> with the specified key.
		/// </summary>
		/// <param name="key">The key for the setting.</param>
		/// <param name="value">The value of the setting.</param>
		void Put(string key, int value);

		/// <summary>
		/// Puts a setting of type <see cref="T:System.Int64" /> with the specified key.
		/// </summary>
		/// <param name="key">The key for the setting.</param>
		/// <param name="value">The value of the setting.</param>
		void Put(string key, long value);

		/// <summary>
		/// Puts a setting of type <see cref="T:System.Float" /> with the specified key.
		/// </summary>
		/// <param name="key">The key for the setting.</param>
		/// <param name="value">The value of the setting.</param>
		void Put(string key, float value);

		/// <summary>
		/// Puts a setting of type <see cref="T:System.Double" /> with the specified key.
		/// </summary>
		/// <param name="key">The key for the setting.</param>
		/// <param name="value">The value of the setting.</param>
		void Put(string key, double value);

		/// <summary>
		/// Puts a setting of type <see cref="T:System.String" /> with the specified key.
		/// </summary>
		/// <param name="key">The key for the setting.</param>
		/// <param name="value">The value of the setting.</param>
		void Put(string key, string value);

		/// <summary>
		/// Retrieves the settings value of a specific type associated with the specified key.
		/// </summary>
		/// <typeparam name="T">Type of the setting.</typeparam>
		/// <param name="key">The key of the setting.</param>
		/// <param name="defaultValue">The default value to use if the key does not exist.</param>
		/// <returns>The settings value, or <paramref name="defaultValue" /> if the key does not exist or the value is not of the specified type.</returns>
		/// <remarks>Each extension has access only to keys it creates.</remarks>
		T Get<T>(string key, T defaultValue = default(T));
	}
}
