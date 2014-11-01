using WiFo.Data;
using WiFo.UI;

namespace WiFo.Extensibility
{
	public interface IExtension
	{
		string DisplayName
		{
			get;
		}

		string Author
		{
			get;
		}
	}

	public interface IStudy : IExtension
	{
		void Perform(RecordList records, IWiFoContext wifo);
	}

	public interface ITimelineView : IExtension
	{
		void OnSelected(RecordTimeline timeline, IWiFoContext wifo);
		void OnUnSelected(RecordTimeline timeline, IWiFoContext wifo);
		void OnClick(uint timeStamp, IWiFoContext wifo);
		void Draw(RecordTimeline timeline, uint startTime, uint endTime, IWiFoCanvas g);
	}
}
