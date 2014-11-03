using WiFo.UI;

namespace WiFo.UI

{
	/// <summary>
	/// Represents an application context for WiFo, which can be used for unified input/output.
	/// </summary>
	public interface IWiFoContext
	{
		/// <summary>
		/// Executes an input command.
		/// </summary>
		/// <param name="input">The input command descriptor.</param>
		/// <returns></returns>
		dynamic Execute(UserInput input);

		/// <summary>
		/// Executes and output command.
		/// </summary>
		/// <param name="output">The output command descriptor.</param>
		void Execute(UserOutput output);
	}
}
