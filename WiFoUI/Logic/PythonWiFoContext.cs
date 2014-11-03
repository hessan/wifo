using System;
using System.ComponentModel;
using WiFo.Extensibility;
using WiFo.UI;
using IronPython.Runtime;

namespace WiFoUI.Logic
{
	public class PythonWiFoContext
	{
		public PythonWiFoContext(IExtension ext)
		{
			extension = ext;
		}

		public void message(string message)
		{
			UserOutput
				.For(UserOutputType.Message)
				.SetTitle(extension.DisplayName)
				.SetMessage(message)
				.Execute(context);
		}

		public void warning(string message)
		{
			UserOutput
				.For(UserOutputType.Message)
				.SetTitle(extension.DisplayName)
				.SetMessage(message)
				.SetWarning(true)
				.Execute(context);
		}

		public void dictbox(PythonDictionary dict)
		{
			UserOutput
				.For(UserOutputType.Results)
				.SetTitle(extension.DisplayName + " - Results")
				.SetResults(new DictPropertyGridAdapter(dict))
				.Execute(context);
		}

		public void barplot(string title, List xs, List ys)
		{
			dynamic[] xvals = new dynamic[xs.__len__()];
			double[] yvals = new double[ys.__len__()];

			xs.CopyTo(xvals, 0);

			int i = 0;

			foreach (var y in ys)
				yvals[i++] = (double)y;

			UserOutput
				.For(UserOutputType.BarPlot)
				.SetTitle(title)
				.SetXValues(xvals)
				.SetYValues(yvals)
				.Execute(context);
		}

		public bool confirm(string message)
		{
			return (bool)UserInput
				.For(UserInputType.Boolean)
				.SetTitle(extension.DisplayName)
				.SetMessage(message)
				.Execute(context);
		}

		public string ask(string message)
		{
			return (string)UserInput
				.For(UserInputType.String)
				.SetTitle(extension.DisplayName)
				.SetMessage(message)
				.Execute(context);
		}

		public int? askint(string message)
		{
			return (int?)UserInput
				.For(UserInputType.Integer)
				.SetTitle(extension.DisplayName)
				.SetMessage(message)
				.Execute(context);
		}

		public string askfile(string title)
		{
			return (string)UserInput
				.For(UserInputType.FileName)
				.SetTitle(title)
				.SetFilter("All files (*.*) | *.*")
				.Execute(context);
		}

		public void SetContext(IWiFoContext context)
		{
			this.context = context;
		}

		private IExtension extension;
		private IWiFoContext context;
	}

	#region Property grid adapter code
	class DictPropertyGridAdapter : ICustomTypeDescriptor
	{
		public DictPropertyGridAdapter(PythonDictionary dict)
		{
			this.dict = dict;
		}

		public AttributeCollection GetAttributes()
		{
			return TypeDescriptor.GetAttributes(this, true);
		}

		public string GetClassName()
		{
			return TypeDescriptor.GetClassName(this, true);
		}

		public string GetComponentName()
		{
			return TypeDescriptor.GetComponentName(this, true);
		}

		public TypeConverter GetConverter()
		{
			return TypeDescriptor.GetConverter(this, true);
		}

		public EventDescriptor GetDefaultEvent()
		{
			return TypeDescriptor.GetDefaultEvent(this, true);
		}

		public PropertyDescriptor GetDefaultProperty()
		{
			return null;
		}

		public object GetEditor(Type editorBaseType)
		{
			return TypeDescriptor.GetEditor(this, editorBaseType, true);
		}

		public EventDescriptorCollection GetEvents(Attribute[] attributes)
		{
			return TypeDescriptor.GetEvents(this, attributes, true);
		}

		public EventDescriptorCollection GetEvents()
		{
			return TypeDescriptor.GetEvents(this, true);
		}

		public PropertyDescriptorCollection GetProperties(Attribute[] attributes)
		{
			System.Collections.ArrayList properties = new System.Collections.ArrayList();

			foreach (string key in dict.keys())
				properties.Add(new DictionaryPropertyDescriptor(dict, key));

			PropertyDescriptor[] props =
				(PropertyDescriptor[])properties.ToArray(typeof(PropertyDescriptor));

			return new PropertyDescriptorCollection(props);
		}

		public PropertyDescriptorCollection GetProperties()
		{
			return ((ICustomTypeDescriptor)this).GetProperties(new Attribute[0]);
		}

		public object GetPropertyOwner(PropertyDescriptor pd)
		{
			return dict;
		}

		private PythonDictionary dict;
	}

	class DictionaryPropertyDescriptor : PropertyDescriptor
	{
		internal DictionaryPropertyDescriptor(PythonDictionary dict, object key)
			: base(key.ToString(), null)
		{
			this.dict = dict;
			this.key = key;
		}

		public override Type PropertyType
		{
			get { return dict.get(key).GetType(); }
		}

		public override object GetValue(object component)
		{
			return dict.get(key, null);
		}
		public override void SetValue(object component, object value) { }

		public override bool IsReadOnly
		{
			get { return true; }
		}

		public override Type ComponentType
		{
			get { return null; }
		}

		public override bool CanResetValue(object component)
		{
			return false;
		}

		public override void ResetValue(object component) { }

		public override bool ShouldSerializeValue(object component)
		{
			return false;
		}

		private PythonDictionary dict;
		private object key;
	}
	#endregion
}
