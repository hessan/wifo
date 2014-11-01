using System;
using System.Collections.Generic;

namespace WiFo.Expressions
{
	public class Macro : Expression
	{
		private static Dictionary<string, Macro> macros;

		private string name;

		static Macro()
		{
			macros = new Dictionary<string, Macro>();
			AddMacro(new Macro("FREE_NAV", "0"));
			AddMacro(new Macro("FREE_PHY", "1"));
			AddMacro(new Macro("FREE_ONE_SLOT", "2"));
			AddMacro(new Macro("FREE_TWO_SLOTS", "3"));
			AddMacro(new Macro("MPDU_TIMEOUT", "4"));
			AddMacro(new Macro("BACKOFF_COMPLETE", "7"));
			AddMacro(new Macro("TX_BUSY", "8"));
			AddMacro(new Macro("RX_BUSY", "9"));
			AddMacro(new Macro("TX_RX_BUSY", "10"));
			AddMacro(new Macro("TX_RX_BUSY2", "11"));
			AddMacro(new Macro("PLCP_TIMEOUT", "15"));
		}

		public static void AddMacro(Macro macro)
		{
			macros[macro.name] = macro;
		}

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

		public Macro(string name, string s)
			: base(s)
		{
			this.name = name;
		}

		public string Name
		{
			get
			{
				return name;
			}
		}
	}
}
