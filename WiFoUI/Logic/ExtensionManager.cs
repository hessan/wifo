using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using WiFo.Extensibility;

namespace WiFoUI.Logic
{
	public class ExtensionManager : IComparer<IExtension>
	{
		public static ExtensionManager Default
		{
			get
			{
				if(instance == null)
					instance = new ExtensionManager();

				return instance;
			}
		}

		public IExtension[] All
		{
			get
			{
				return extensions;
			}
		}

		public IStudy[] Studies
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

		public ITimelineView[] TimelineViews
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

		public void LoadExtensions()
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

			lExtensions.Sort(this);
			extensions = lExtensions.ToArray();
		}

		public int Compare(IExtension x, IExtension y)
		{
			return x.DisplayName.CompareTo(y.DisplayName);
		}

		private ExtensionManager() {
			extensions = null;
		}

		private IExtension[] extensions;
		private static ExtensionManager instance;
	}
}
