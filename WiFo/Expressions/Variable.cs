namespace WiFo.Expressions
{
	/// <summary>
	/// Represents a variable in an expression-parsing context.
	/// </summary>
	/// <typeparam name="T">The data type for this variable's value.</typeparam>
	/// <seealso cref="Expression"/>
	public class Variable<T>
	{
		/// <summary>
		/// Initializes a new instance of <see cref="Variable"/> with the specified name and value.
		/// </summary>
		/// <param name="name">The name of the variable.</param>
		/// <param name="value">The value of the variable.</param>
		public Variable(string name, T value)
		{
			this.name = name;
			_value = value;
		}

		/// <summary>
		/// Gets the name of this variable.
		/// </summary>
		public string Name
		{
			get
			{
				return name;
			}
		}

		/// <summary>
		/// Gets or sets the value of this variable.
		/// </summary>
		public T Value
		{
			get
			{
				return _value;
			}
			set
			{
				_value = value;
			}
		}

		/// <summary>
		/// Returns the string representation of this variable's value.
		/// </summary>
		/// <returns>String representation of the value.</returns>
		public override string ToString()
		{
			return _value.ToString();
		}

		private readonly string name;
		private T _value;
	}
}
