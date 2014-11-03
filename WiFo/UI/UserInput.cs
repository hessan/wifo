namespace WiFo.UI
{
	/// <summary>
	/// Specifies constants defining what kind of input to execute.
	/// </summary>
	public enum UserInputType
	{
		/// <summary>
		/// A confirmation dialog is displayed, the result of which is a boolean.
		/// </summary>
		Boolean,

		/// <summary>
		/// A dialog is displayed with a box to enter an integer.
		/// </summary>
		Integer,

		/// <summary>
		/// A dialog is displayed with a box to enter a text.
		/// </summary>
		String,

		/// <summary>
		/// An open file dialog is displayed.
		/// </summary>
		FileName
	}

	/// <summary>
	/// Represents a description for a user input command within the WiFo application.
	/// </summary>
	public class UserInput
	{
		/// <summary>
		/// Creates a new user input descriptor of the specified type.
		/// </summary>
		/// <param name="type">The user input type.</param>
		/// <returns>A <see cref="UserInput" /> object, describing the input.</returns>
		/// <seealso cref="UserInputType" />
		public static UserInput For(UserInputType type)
		{
			return new UserInput(type);
		}

		/// <summary>
		/// Gets the input type associated with this instance.
		/// </summary>
		public UserInputType InputType
		{
			get
			{
				return type;
			}
		}

		/// <summary>
		/// Gets the default value for an integer input.
		/// </summary>
		/// <seealso cref="UserInputType" />
		public int DefaultIntValue
		{
			get
			{
				return def == null ? 0 : (int)def;
			}
		}

		/// <summary>
		/// Gets the default value for a string input.
		/// </summary>
		/// <seealso cref="UserInputType" />
		public string DefaultValue
		{
			get
			{
				return this.def as string;
			}
		}

		/// <summary>
		/// Gets the file filter for a filename input type.
		/// </summary>
		/// <seealso cref="UserInputType" />
		public string Filter
		{
			get
			{
				return this.param;
			}
		}

		/// <summary>
		/// Gets the maximum value for an integer input type, or the maximum length for a string input type.
		/// </summary>
		/// <seealso cref="UserInputType" />
		public int Maximum
		{
			get
			{
				return max;
			}
		}

		/// <summary>
		/// Gets the minimum value for an integer input type.
		/// </summary>
		/// <seealso cref="UserInputType" />
		public int Minimum
		{
			get
			{
				return min;
			}
		}

		/// <summary>
		/// Gets the displayed message text for a string input type.
		/// </summary>
		/// <seealso cref="UserInputType" />
		public string Message
		{
			get
			{
				return this.param;
			}
		}

		/// <summary>
		/// Gets the displayed unit text for an integer input type.
		/// </summary>
		/// <seealso cref="UserInputType" />
		public string UnitText
		{
			get
			{
				return this.param;
			}
		}

		/// <summary>
		/// Gets the dialog title associated with this instance.
		/// </summary>
		public string Title
		{
			get
			{
				return title;
			}
		}

		/// <summary>
		/// Executes this instance on the specified WiFo context.
		/// </summary>
		/// <param name="ctx">The WiFo context that executes the input.</param>
		/// <returns>The results, depending on the input type.</returns>
		/// <seealso cref="UserInputType" />
		public object Execute(IWiFoContext ctx)
		{
			return ctx.Execute(this);
		}

		/// <summary>
		/// Sets the default value for an integer input type.
		/// </summary>
		/// <param name="def">The default value.</param>
		/// <returns>This instance.</returns>
		/// <seealso cref="UserInputType" />
		public UserInput SetDefault(int def)
		{
			if (type == UserInputType.Integer)
				this.def = def;

			return this;
		}

		/// <summary>
		/// Sets the default value for a string input type.
		/// </summary>
		/// <param name="def">The default value.</param>
		/// <returns>This instance.</returns>
		/// <seealso cref="UserInputType" />
		public UserInput SetDefault(string def)
		{
			if (type != UserInputType.Integer)
				this.def = def;

			return this;
		}

		/// <summary>
		/// Sets the file filter for a filename input type.
		/// </summary>
		/// <param name="filter">The file filter.</param>
		/// <returns>This instance.</returns>
		/// <seealso cref="UserInputType" />
		public UserInput SetFilter(string filter)
		{
			if (type == UserInputType.FileName)
				this.param = filter;

			return this;
		}

		/// <summary>
		/// Sets the maximum value for an integer input type.
		/// </summary>
		/// <param name="max">The maximum value.</param>
		/// <returns>This instance.</returns>
		/// <seealso cref="UserInputType" />
		public UserInput SetMaximum(int max)
		{
			this.max = max;
			return this;
		}

		/// <summary>
		/// Sets the minimum value for an integer input type.
		/// </summary>
		/// <param name="min">The minimum value.</param>
		/// <returns>This instance.</returns>
		/// <seealso cref="UserInputType" />
		public UserInput SetMinimum(int min)
		{
			this.min = min;
			return this;
		}

		/// <summary>
		/// Sets the displayed message a string input type.
		/// </summary>
		/// <param name="message">The message to be displayed.</param>
		/// <returns>This instance.</returns>
		/// <seealso cref="UserInputType" />
		public UserInput SetMessage(string message)
		{
			if (type == UserInputType.Boolean ||
				type == UserInputType.String)
				this.param = message;

			return this;
		}

		/// <summary>
		/// Sets the maximum length for a string input type.
		/// </summary>
		/// <param name="max">The maximum length.</param>
		/// <returns>This instance.</returns>
		/// <seealso cref="UserInputType" />
		public UserInput SetMaxLength(int max)
		{
			return SetMaximum(max);
		}

		/// <summary>
		/// Sets the dialog title.
		/// </summary>
		/// <param name="title">The dialog title.</param>
		/// <returns>This instance.</returns>
		public UserInput SetTitle(string title)
		{
			this.title = title;
			return this;
		}

		/// <summary>
		/// Sets the unit text for an integer input type.
		/// </summary>
		/// <param name="unit">The string representation of the unit (e.g. ms for milliseconds).</param>
		/// <returns>This instance.</returns>
		/// <seealso cref="UserInputType" />
		public UserInput SetUnitText(string unit)
		{
			if(type == UserInputType.Integer)
				this.param = unit;

			return this;
		}

		private UserInput(UserInputType type)
		{
			this.type = type;
			min = int.MinValue;
			max = int.MaxValue;

			switch (type)
			{
				case UserInputType.Integer:
					def = 0;
					break;
				case UserInputType.String:
					def = string.Empty;
					break;
				case UserInputType.FileName:
					def = null;
					break;
			}
		}

		private int min, max;
		private object def;
		private string title;
		private string param;
		private UserInputType type;
	}
}
