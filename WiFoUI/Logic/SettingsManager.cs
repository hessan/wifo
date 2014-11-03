using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using WiFo.Extensibility;
using WiFo.Extensibility.Settings;

namespace WiFoUI.Logic
{
	public class SettingsManager
	{
		public static SettingsManager Default
		{
			get
			{
				return instance;
			}
		}

		public SettingsManager(string settingsFile)
		{
			settingsPath = settingsFile;
		}

		public string ServerAddress
		{
			get
			{
				return serverAddress;
			}
			set
			{
				if (value != null)
					serverAddress = value;
			}
		}

		public int ServerPort
		{
			get
			{
				return serverPort;
			}
			set
			{
				if (value > 0)
					serverPort = value;
			}
		}

		public string PythonLibraryPath
		{
			get
			{
				return pythonLibPath;
			}
			set
			{
				if (value != null)
					if (!Directory.Exists(value))
					{
						pythonLibPath = null;
						return;
					}

				pythonLibPath = value;
			}
		}

		public void Load()
		{
			if (File.Exists(settingsPath))
			{
				SettingsBundle bundle = new SettingsBundle(settingsPath);
				bundle.Load();

				if(ExtensionManager.All != null)
					foreach (IExtension ext in ExtensionManager.All)
						if (ext is ISettingsContributor)
						{
							bundle.prefix = ext.GetType().Name + "_";
							((ISettingsContributor)ext).Load(bundle);
						}

				bundle.prefix = "wifo_";
				serverAddress = bundle.Get<string>("ServerAddr");
				serverPort = bundle.Get<int>("ServerPort");
				pythonLibPath = bundle.Get<string>("PythonLibPath");
				bundle.Dispose();
			}
		}

		public void Save()
		{
			SettingsBundle bundle = new SettingsBundle(settingsPath);

			foreach (IExtension ext in ExtensionManager.All)
				if (ext is ISettingsContributor)
				{
					bundle.prefix = ext.GetType().Name + "_";
					((ISettingsContributor)ext).Save(bundle);
				}

			bundle.prefix = "wifo_";
			bundle.Put("ServerAddr", serverAddress);
			bundle.Put("ServerPort", serverPort);
			bundle.Put("PythonLibPath", pythonLibPath);
			bundle.Save();
			bundle.Dispose();
		}

		static SettingsManager()
		{
			instance = new SettingsManager(new FileInfo(Assembly.GetExecutingAssembly().Location).Directory.FullName + "\\settings.dat");
		}

		private string settingsPath;
		private string pythonLibPath = null;
		private string serverAddress = "10.220.10.69";
		private int serverPort = 1363;

		private static SettingsManager instance;
	}
}
