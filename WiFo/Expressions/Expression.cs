using System;
using System.Collections.Generic;

namespace WiFo.Expressions
{
	public class Expression
	{
		private List<Token> tokens = new List<Token>();

		public Expression(string s)
		{
			int currentIndex = -1;

			while (currentIndex < s.Length)
			{
				Token token = Token.Read(s, currentIndex + 1, out currentIndex);

				if (token == null)
					break;

				tokens.Add(token);
			}
		}

		private int currentIndex, varId;
		private string currentToken = null;

		private bool Accept(Token.TokenType type)
		{
			if (currentIndex >= tokens.Count)
				return false;

			if (tokens[currentIndex].SymbolType == type)
			{
				currentToken = tokens[currentIndex].Value;
				currentIndex++;
				return true;
			}

			return false;
		}

		private bool Expect(Token.TokenType type)
		{
			if (Accept(type))
				return true;

			//error("expect: unexpected symbol");
			return false;
		}

		private bool Factor(uint state)
		{
			bool negate = Accept(Token.TokenType.NOT);

			if (Accept(Token.TokenType.MACRO))
			{
				bool ret = Macro.Resolve(currentToken, state);
				return negate ? !ret : ret;
			}

			if (Accept(Token.TokenType.NUMERIC))
			{
				bool ret = (state & (1 << int.Parse(currentToken))) != 0;
				return negate ? !ret : ret;
			}

			if (Accept(Token.TokenType.LParen))
			{
				bool ret = Term(state);
				Expect(Token.TokenType.RParen);
				return negate ? ret : !ret;
			}

			return false; // ERROR
		}

		private bool And(uint state)
		{
			bool val = Factor(state);

			while (Accept(Token.TokenType.AND))
				val = val && Factor(state);

			return val;
		}

		private bool Term(uint state)
		{
			bool val = And(state);

			while (true)
			{
				if (Accept(Token.TokenType.OR))
					val = val || And(state);
				else if (Accept(Token.TokenType.XOR))
				{
					bool newVal = And(state);
					val = (val && !newVal) || (!val && newVal);
				}
				else break;
			}

			return val;
		}

		public int TokenCount
		{
			get
			{
				return tokens.Count;
			}
		}

		public Token this[int index]
		{
			get
			{
				return tokens[index];
			}
		}

		protected bool EvaluateSingle(uint state)
		{
			currentIndex = 0;
			return Term(state);
		}

		public List<Variable<bool>> Evaluate(uint state)
		{
			List<Variable<bool>> ret = new List<Variable<bool>>();
			currentIndex = 0;
			varId = 0;
			ret.Add(EvaluateVariable(state));

			while (Accept(Token.TokenType.COMMA))
				ret.Add(EvaluateVariable(state));

			return ret;
		}

		private Variable<bool> EvaluateVariable(uint state)
		{
			bool result = Term(state);
			string name;

			if (Accept(Token.TokenType.AS))
			{
				if (Accept(Token.TokenType.MACRO))
					name = currentToken;
				else if (Accept(Token.TokenType.NUMERIC))
					name = currentToken;
				else name = "INVALID";
			}
			else name = "VAR" + ++varId;

			return new Variable<bool>(name, result);
		}
	}
}
