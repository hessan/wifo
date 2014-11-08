using System;
using IronPython.Hosting;
using IronPython.Runtime;
using Microsoft.Scripting.Hosting;
using WiFo.Data;
using WiFo.Extensibility;
using WiFo.Extensibility.Settings;
using WiFo.UI;
using System.IO;

namespace WiFoUI.Logic
{
	public class PythonStudy : IStudy
	{
		public PythonStudy(string fileName)
		{
			if(py == null)
				py = Python.CreateEngine();

			pythonFile = new FileInfo(fileName);

			Compile();

			scope = py.CreateScope();
			scope.ImportModule("sys");

			if (SettingsManager.Default.PythonLibraryPath != null)
				py.Execute("sys.path.append(r\"" + SettingsManager.Default.PythonLibraryPath + "\")", scope);

			wifoModule = py.CreateModule("wifo");
			wifo = new PythonWiFoContext(this);
			wifoModule.SetVariable("message", new Action<string>(wifo.message));
			wifoModule.SetVariable("warning", new Action<string>(wifo.warning));
			wifoModule.SetVariable("dictbox", new Action<PythonDictionary>(wifo.dictbox));
			wifoModule.SetVariable("barplot", new Action<string, List, List>(wifo.barplot));
			wifoModule.SetVariable("confirm", new Func<string, bool>(wifo.confirm));
			wifoModule.SetVariable("ask", new Func<string, string>(wifo.ask));
			wifoModule.SetVariable("askint", new Func<string, int?>(wifo.askint));
			wifoModule.SetVariable("askfile", new Func<string, string>(wifo.askfile));

			StreamReader reader = new StreamReader(Path.Combine(new FileInfo(System.Reflection.Assembly.GetExecutingAssembly().Location).Directory.FullName, "ext", "__wifo__.py"));
			string script = reader.ReadToEnd();
			reader.Close();
			py.Execute(script, wifoModule);
			code.Execute(scope);
		}

		public void Perform(RecordList records, WiFo.UI.IWiFoContext context)
		{
			dynamic[] data = new dynamic[records.Count];

			pythonFile.Refresh();

			if (pythonFile.LastWriteTime.CompareTo(lastCompiled) > 0)
			{
				Compile();
				code.Execute(scope);
			}

			int i = 0;

			ObjectOperations op = py.Operations;
			var recordType = wifoModule.GetVariable("Record");

			foreach (Record record in records)
			{
				data[i++] = op.CreateInstance(recordType, record.Time, record.State);
			}

			wifo.SetContext(context);
			wifoModule.SetVariable("data", data);

			try
			{
				CallFunction("perform");
			}
			catch
			{
				wifo.warning("Extension encountered an error.");
			}

			wifoModule.RemoveVariable("data");
			wifo.SetContext(null);
		}

		public string Author
		{
			get
			{
				return CallFunction("author").ToString();
			}
		}

		public string DisplayName
		{
			get
			{
				return CallFunction("displayname").ToString();
			}
		}

		private void Compile()
		{
			ScriptSource source = py.CreateScriptSourceFromFile(pythonFile.FullName);
			code = source.Compile();
			lastCompiled = DateTime.Now;
		}

		private dynamic CallFunction(string functionName)
		{
			Func<dynamic> func = (Func<dynamic>)scope.GetVariable(functionName);

			if (func == null)
				return null;

			return func.Invoke();
		}

		private static ScriptEngine py = null;
		private FileInfo pythonFile;
		private DateTime lastCompiled;
		private ScriptScope scope, wifoModule;
		private CompiledCode code;
		private PythonWiFoContext wifo;
	}
}
