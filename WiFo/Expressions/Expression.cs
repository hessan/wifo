using System;
using System.Collections.Generic;

namespace WiFo.Expressions
{
	/// <summary>
	/// Represents an expression in an expression-parsing context.
	/// </summary>
	public class Expression
	{
		/// <summary>
		/// Initializes a new instance of this expression by parsing the specfied expression string.
		/// </summary>
		/// <param name="s">The expression to be parsed.</param>
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

		/// <summary>
		/// Evaluates the expression using the given state value.
		/// </summary>
		/// <param name="state">The state value to be used in the evaluation.</param>
		/// <returns>A list of variables containing evaluation results.</returns>
		public List<bool> Evaluate(uint state)
		{
			List<bool> ret = new List<bool>();
			currentIndex = 0;
			varId = 0;
			ret.Add(EvaluateValue(state));

			while (Accept(Token.TokenType.COMMA))
				ret.Add(EvaluateValue(state));

			return ret;
		}

		internal Token this[int index]
		{
			get
			{
				return tokens[index];
			}
		}

		internal int TokenCount
		{
			get
			{
				return tokens.Count;
			}
		}

		/// <summary>
		/// Evaluates this expression as a single term. This is mainly used by macros to evaluate themselves.
		/// </summary>
		/// <param name="state">The state value to be used in the evaluation.</param>
		/// <returns>The evaluated value.</returns>
		protected internal bool EvaluateSingle(uint state)
		{
			currentIndex = 0;
			return Term(state);
		}

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

		private bool And(uint state)
		{
			bool val = Factor(state);

			while (Accept(Token.TokenType.AND))
				val = val && Factor(state);

			return val;
		}

		/// <summary>
		/// Evaluates the current term in the expression, returning only the value.
		/// If a variable name is specified in the expression, it is ignored.
		/// </summary>
		/// <param name="state">The state value to be used in the evaluation.</param>
		/// <returns>The evaluated value.</returns>
		private bool EvaluateValue(uint state)
		{
			bool result = Term(state);
			Accept(Token.TokenType.AS);
			return result;
		}

		/// <summary>
		/// Evaluates a named variable from the current term in the expression.
		/// </summary>
		/// <remarks>
		/// If not name is specified in the expression, an auto-generated name will be used.
		/// </remarks>
		/// <param name="state">The state value to be used in the evaluation.</param>
		/// <returns>The evaluated variable.</returns>
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

		private List<Token> tokens = new List<Token>();
		private int currentIndex, varId;
		private string currentToken = null;
	}
}
