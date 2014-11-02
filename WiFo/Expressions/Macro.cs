using System;
using System.Collections.Generic;

namespace WiFo.Expressions
{
	/// <summary>
	/// Represents a macro in an expression-parsing context.
	/// </summary>
	/// <remarks>
	/// A macro is a sub-class of <see cref="Expression"/> and can be interpretted the same way.
	/// By adding a macro using the <see cref="AddMacro"/> method of this class, the name of the
	/// macro will always be resolved to the corresponding macro expression if it appears in another
	/// expression.
	/// </remarks>
	/// <seealso cref="Expression" />
	public sealed class Macro : Expression
	{
		static Macro()
		{
			macros = new Dictionary<string, Macro>();

			AddMacro("FREE_NAV", "0");
			AddMacro("FREE_PHY", "1");
			AddMacro("FREE_ONE_SLOT", "2");
			AddMacro("FREE_TWO_SLOTS", "3");
			AddMacro("MPDU_TIMEOUT", "4");
			AddMacro("BACKOFF_COMPLETE", "7");
			AddMacro("TX_BUSY", "8");
			AddMacro("RX_BUSY", "9");
			AddMacro("TX_RX_BUSY", "10");
			AddMacro("TX_RX_BUSY2", "11");
			AddMacro("PLCP_TIMEOUT", "15");
		}

		/// <summary>
		/// Adds a macro definition to the global expression-parsing context.
		/// </summary>
		/// <param name="name">Name of the new macro.</param>
		/// <param name="exp">Expression to be parsed for the new macro.</param>
		public static void AddMacro(string name, string exp)
		{
			macros[name] = new Macro(name, exp);
		}

		/// <summary>
		/// Resolves the value of a macro for a given state value.
		/// </summary>
		/// <param name="name">The name of the macro to be evaluated.</param>
		/// <param name="state">The state value to be used in the evaluation.</param>
		/// <returns></returns>
		public static bool Resolve(string name, uint state)
		{
			try
			{
				Macro macro = macros[name];
				return macro.EvaluateSingle(state);
			}
			catch
			{
				return false;
			}
		}

		/// <summary>
		/// Gets the macro name associated with this instance.
		/// </summary>
		public string Name
		{
			get
			{
				return name;
			}
		}

		private Macro(string name, string s)
			: base(s)
		{
			this.name = name;
		}

		private static Dictionary<string, Macro> macros;
		private string name;

	}
}
