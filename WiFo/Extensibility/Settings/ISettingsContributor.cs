namespace WiFo.Extensibility.Settings
{
	/// <summary>
	/// Defines an object, typically an extension, that contributes to global settings.
	/// </summary>
	/// <remarks>
	/// Extensions that implement this interface automatically receive settings callbacks.
	/// </remarks>
	/// <seealso cref="ISettings" />
	public interface ISettingsContributor
	{
		/// <summary>
		/// This method is invoked when the application is loading settings.
		/// </summary>
		/// <param name="settings">Bundle of persisted settings.</param>
		void Load(ISettings settings);

		/// <summary>
		/// This method is invoked when the application is saving settings. 
		/// </summary>
		/// <param name="settings">Bundle of settings to persist.</param>
		/// <remarks>All settings that need to be persisted must be added to this bundle every time this method is called.</remarks>
		void Save(ISettings settings);
	}
}
