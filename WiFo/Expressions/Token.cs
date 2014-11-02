using System;

namespace WiFo.Expressions
{
	internal class Token
	{
		public enum TokenType
		{
			LParen,
			RParen,
			AND,
			OR,
			XOR,
			NOT,
			NUMERIC,
			MACRO,
			COMMA,
			AS
		}

		public TokenType SymbolType
		{
			get
			{
				return type;
			}
		}

		public string Value
		{
			get
			{
				return value;
			}
		}

		public static Token Read(String source, int startIndex, out int currentIndex)
		{
			for (int i = startIndex; i < source.Length; i++)
			{
				char c = source[i];

				if (!IsWhiteSpace(c))
				{
					if (IsNumeric(c))
					{
						string token = "";

						while (IsNumeric(c))
						{
							token += c.ToString();

							if (++i < source.Length)
								c = source[i];
							else break;
						}

						i--;

						currentIndex = i;
						return new Token(TokenType.NUMERIC, token);
					}
					else if (IsAlpha(c))
					{
						string token = "";

						while ((IsAlphaNumeric(c) || c == '_'))
						{
							token += c.ToString();

							if (++i < source.Length)
								c = source[i];
							else break;
						}

						i--;

						TokenType tokenType = TokenType.MACRO;

						if (token == "and")
							tokenType = TokenType.AND;
						else if (token == "or")
							tokenType = TokenType.OR;
						else if (token == "xor")
							tokenType = TokenType.XOR;
						else if (token == "not")
							tokenType = TokenType.NOT;
						else if (token == "as")
							tokenType = TokenType.AS;

						currentIndex = i;
						return new Token(tokenType, token);
					}
					else
					{
						currentIndex = i;

						if (c == '(')
							return new Token(TokenType.LParen, "(");
						else if (c == ')')
							return new Token(TokenType.RParen, ")");
						else if (c == ',')
							return new Token(TokenType.COMMA, ",");
					}
				}
			}

			currentIndex = source.Length - 1;
			return null;
		}

		private Token(TokenType type, string value)
		{
			this.type = type;
			this.value = value;
		}

		private static bool IsAlpha(char c)
		{
			return (c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z');
		}

		private static bool IsAlphaNumeric(char c)
		{
			return IsAlpha(c) || IsNumeric(c);
		}

		private static bool IsNumeric(char c)
		{
			return c >= '0' && c <= '9';
		}

		private static bool IsWhiteSpace(char c)
		{
			return c == ' ' || c == '\t';
		}

		private TokenType type;
		private string value;
	}
}
