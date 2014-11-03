namespace WiFo.UI
{
	/// <summary>
	/// Specifies constants defining what kind of output to execute.
	/// </summary>
	public enum UserOutputType
	{
		/// <summary>
		/// A message box is displayed.
		/// </summary>
		Message,

		/// <summary>
		/// A bar plot is displayed, using specified data.
		/// </summary>
		BarPlot,

		/// <summary>
		/// A dialog displays the results.
		/// </summary>
		Results
	}

	/// <summary>
	/// Represents a description for a user output command within the WiFo application.
	/// </summary>
	public class UserOutput
	{
		/// <summary>
		/// Creates a new user output descriptor of the specified type.
		/// </summary>
		/// <param name="type">The user output type.</param>
		/// <returns>A <see cref="UserOutput" /> object, describing the output.</returns>
		/// <seealso cref="UserOutputType" />
		public static UserOutput For(UserOutputType type)
		{
			return new UserOutput(type);
		}

		/// <summary>
		/// Gets the output type associated with this instance.
		/// </summary>
		public UserOutputType OutputType
		{
			get
			{
				return type;
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
		/// Gets the displayed message for a message output type.
		/// </summary>
		/// <seealso cref="UserOutputType" />
		public string Message
		{
			get
			{
				if (type == UserOutputType.Message)
					return obj1 as string;

				return null;
			}
		}

		/// <summary>
		/// Determines whether this instance represents a warning message.
		/// </summary>
		/// <seealso cref="UserOutputType" />
		public bool IsWarning
		{
			get
			{
				if (type == UserOutputType.Message)
				{
					bool? ret = obj2 as bool?;
					return ret == true ? true : false;
				}

				return false;
			}
		}

		/// <summary>
		/// Gets the results object for a results output type.
		/// </summary>
		/// <seealso cref="UserOutputType" />
		public object Results
		{
			get
			{
				return obj1;
			}
		}

		/// <summary>
		/// Gets the x values for a bar-plot output type.
		/// </summary>
		/// <seealso cref="UserOutputType" />
		public object[] XValues
		{
			get
			{
				if (type == UserOutputType.BarPlot)
					return obj1 as object[];

				return new object[0];
			}
		}

		/// <summary>
		/// Gets the y values for a bar-plot output type.
		/// </summary>
		/// <seealso cref="UserOutputType" />
		public double[] YValues
		{
			get
			{
				if (type == UserOutputType.BarPlot)
					return obj2 as double[];

				return new double[0];
			}
		}

		/// <summary>
		/// Executes this instance on the specified WiFo context.
		/// </summary>
		/// <param name="ctx">The WiFo context that displays the output.</param>
		public void Execute(IWiFoContext ctx)
		{
			ctx.Execute(this);
		}

		/// <summary>
		/// Sets the message for a message output type.
		/// </summary>
		/// <param name="message">The message to be displayed.</param>
		/// <returns>This instance.</returns>
		/// <seealso cref="UserOutputType" />
		public UserOutput SetMessage(string message)
		{
			if (type == UserOutputType.Message)
				obj1 = message;

			return this;
		}

		/// <summary>
		/// Sets the results object for a results output type.
		/// </summary>
		/// <param name="obj">The results object.</param>
		/// <returns>This instance.</returns>
		/// <seealso cref="UserOutputType" />
		public UserOutput SetResults(object obj)
		{
			if (type == UserOutputType.Results)
				obj1 = obj;

			return this;
		}

		/// <summary>
		/// Sets the dialog title.
		/// </summary>
		/// <param name="title">The dialog title.</param>
		/// <returns>This instance.</returns>
		public UserOutput SetTitle(string title)
		{
			this.title = title;
			return this;
		}

		/// <summary>
		/// For a message output type, sets a value indicating whether this instance represents a warning message.
		/// </summary>
		/// <param name="warning">A value indicating whether this instance represents a warning message.</param>
		/// <returns>This instance.</returns>
		/// <seealso cref="UserOutputType" />
		public UserOutput SetWarning(bool warning)
		{
			if (type == UserOutputType.Message)
				obj2 = warning;

			return this;
		}

		/// <summary>
		/// Sets the x values for a bar-plot input type.
		/// </summary>
		/// <param name="values">X values.</param>
		/// <returns>This instance.</returns>
		/// <seealso cref="UserOutputType" />
		public UserOutput SetXValues(object[] values)
		{
			if(type == UserOutputType.BarPlot)
				obj1 = values;

			return this;
		}

		/// <summary>
		/// Sets the y values for a bar-plot input type.
		/// </summary>
		/// <param name="values">Y values.</param>
		/// <returns>This instance.</returns>
		/// <seealso cref="UserOutputType" />
		public UserOutput SetYValues(double[] values)
		{
			if(type == UserOutputType.BarPlot)
				obj2 = values;

			return this;
		}

		private UserOutput(UserOutputType type)
		{
			this.type = type;
		}

		private object obj1, obj2;
		private string title;
		private UserOutputType type;
	}
}
