namespace WiFo.Expressions
{
	public class Variable<T>
	{
		private readonly string name;
		private T _value;

		public Variable(string name, T value)
		{
			this.name = name;
			_value = value;
		}

		public string Name
		{
			get
			{
				return name;
			}
		}

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

		public override string ToString()
		{
			return name + " = " + _value;
		}
	}
}
