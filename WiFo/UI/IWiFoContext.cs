using WiFo.UI;

namespace WiFo.UI

{
	public interface IWiFoContext
	{
		dynamic Execute(UserInput input);
		void Execute(UserOutput output);
	}
}
