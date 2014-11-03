using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using WiFo.Extensibility;

namespace WiFoUI.Logic
{
	class ExtensionManager
	{
		public static IExtension[] All
		{
			get
			{
				return extensions;
			}
		}

		public static IStudy[] Studies
		{
			get
			{
				List<IStudy> lStudies = new List<IStudy>();

				foreach (IExtension ext in extensions)
					if (ext is IStudy)
						lStudies.Add((IStudy)ext);

				return lStudies.ToArray();
			}
		}

		public static ITimelineView[] TimelineViews
		{
			get
			{
				List<ITimelineView> lOverlays = new List<ITimelineView>();

				foreach (IExtension ext in extensions)
					if (ext is ITimelineView)
						lOverlays.Add((ITimelineView)ext);

				return lOverlays.ToArray();
			}
		}

		public static void LoadExtensions()
		{
			DirectoryInfo dir = new FileInfo(Assembly.GetExecutingAssembly().Location).Directory.GetDirectories("ext")[0];
			FileInfo[] extFiles = dir.GetFiles("*.wifo");
			List<IExtension> lExtensions = new List<IExtension>();

			foreach (FileInfo extFile in extFiles)
			{
				try
				{
					Assembly assembly = Assembly.LoadFile(extFile.FullName);

					foreach (Type type in assembly.GetExportedTypes())
					{
						if (typeof(IExtension).IsAssignableFrom(type))
						{
							IExtension ext = (IExtension)type.GetConstructor(new Type[0]).Invoke(new object[0]);
							lExtensions.Add(ext);
						}
					}
				}
				catch { }
			}

			extFiles = dir.GetFiles("*.py");

			foreach (FileInfo extFile in extFiles)
			{
				try
				{
					IExtension study = new PythonStudy(extFile.FullName);
					lExtensions.Add(study);
				}
				catch (Exception ex) {
					System.Diagnostics.Debug.WriteLine(ex.Message);
				}
			}

			extensions = lExtensions.ToArray();
		}

		private static IExtension[] extensions;
	}
}
