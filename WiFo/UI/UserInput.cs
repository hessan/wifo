using System;

namespace WiFo.UI
{
	public enum UserInputTypes
	{
		Boolean,
		Integer,
		String,
		FileName
	}

	public class UserInput
	{
		public static UserInput For(UserInputTypes type)
		{
			return new UserInput(type);
		}

		public int DefaultIntValue
		{
			get
			{
				return def == null ? 0 : (int)def;
			}
		}

		public string DefaultValue
		{
			get
			{
				return this.def as string;
			}
		}

		public string Filter
		{
			get
			{
				return this.param;
			}
		}

		public UserInputTypes InputType
		{
			get
			{
				return type;
			}
		}

		public int Maximum
		{
			get
			{
				return max;
			}
		}

		public string Message
		{
			get
			{
				return this.param;
			}
		}

		public int Minimum
		{
			get
			{
				return min;
			}
		}

		public string Title
		{
			get
			{
				return title;
			}
		}

		public object Execute(IWiFoContext ctx)
		{
			return ctx.Execute(this);
		}

		public UserInput SetDefault(int def)
		{
			if (type == UserInputTypes.Integer)
				this.def = def;

			return this;
		}

		public UserInput SetDefault(string def)
		{
			if (type != UserInputTypes.Integer)
				this.def = def;

			return this;
		}

		public UserInput SetFilter(string filter)
		{
			if (type == UserInputTypes.FileName)
				this.param = filter;

			return this;
		}

		public UserInput SetMaximum(int max)
		{
			this.max = max;
			return this;
		}

		public UserInput SetMessage(string message)
		{
			if (type == UserInputTypes.Boolean ||
				type == UserInputTypes.String)
				this.param = message;

			return this;
		}

		public UserInput SetMinimum(int min)
		{
			this.min = min;
			return this;
		}

		public UserInput SetMaxLength(int max)
		{
			return SetMaximum(max);
		}

		public UserInput SetTitle(string title)
		{
			this.title = title;
			return this;
		}

		public UserInput SetUnitText(string unit)
		{
			if(type == UserInputTypes.Integer)
				this.param = unit;

			return this;
		}

		private UserInput(UserInputTypes type)
		{
			this.type = type;
			min = int.MinValue;
			max = int.MaxValue;

			switch (type)
			{
				case UserInputTypes.Integer:
					def = 0;
					break;
				case UserInputTypes.String:
					def = String.Empty;
					break;
				case UserInputTypes.FileName:
					def = null;
					break;
			}
		}

		private int min, max;
		private object def;
		private string title;
		private string param;
		private UserInputTypes type;
	}
}
