using WiFo.UI;

namespace WiFo.UI

{
	public interface IWiFoContext
	{
		object Execute(UserInput input);
		void Execute(UserOutput output);
	}
}
