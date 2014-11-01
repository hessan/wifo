using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using WiFo.Extensibility;

namespace WiFoUI.Logic
{
	class ExtensionManager
	{
		private static IExtension[] extensions;

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

					extensions = lExtensions.ToArray();
				}
				catch { }
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

		public static IExtension[] All
		{
			get
			{
				return extensions;
			}
		}
	}
}
