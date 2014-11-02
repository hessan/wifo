namespace WiFo.UI
{
	public enum UserOutputTypes
	{
		Message,
		BarPlot,
		Results
	}

	public class UserOutput
	{
		public static UserOutput For(UserOutputTypes type)
		{
			return new UserOutput(type);
		}

		public UserOutputTypes OutputType
		{
			get
			{
				return type;
			}
		}

		public string Title
		{
			get
			{
				return title;
			}
		}

		public string Message
		{
			get
			{
				if (type == UserOutputTypes.Message)
					return obj1 as string;

				return null;
			}
		}

		public bool IsWarning
		{
			get
			{
				if (type == UserOutputTypes.Message)
				{
					bool? ret = obj2 as bool?;
					return ret == true ? true : false;
				}

				return false;
			}
		}

		public object Results
		{
			get
			{
				return obj1;
			}
		}

		public object[] XValues
		{
			get
			{
				if (type == UserOutputTypes.BarPlot)
					return obj1 as object[];

				return new object[0];
			}
		}

		public double[] YValues
		{
			get
			{
				if (type == UserOutputTypes.BarPlot)
					return obj2 as double[];

				return new double[0];
			}
		}

		public UserOutput SetTitle(string title)
		{
			this.title = title;
			return this;
		}

		public UserOutput SetXValues(object[] values)
		{
			if(type == UserOutputTypes.BarPlot)
				obj1 = values;

			return this;
		}

		public UserOutput SetYValues(double[] values)
		{
			if(type == UserOutputTypes.BarPlot)
				obj2 = values;

			return this;
		}

		public UserOutput SetMessage(string message)
		{
			if (type == UserOutputTypes.Message)
				obj1 = message;

			return this;
		}

		public UserOutput SetWarning(bool warning)
		{
			if (type == UserOutputTypes.Message)
				obj2 = warning;

			return this;
		}

		public UserOutput SetResults(object obj)
		{
			if (type == UserOutputTypes.Results)
				obj1 = obj;

			return this;
		}

		public void Execute(IWiFoContext ctx)
		{
			ctx.Execute(this);
		}

		private UserOutput(UserOutputTypes type)
		{
			this.type = type;
		}

		private object obj1, obj2;
		private string title;
		private UserOutputTypes type;
	}
}
