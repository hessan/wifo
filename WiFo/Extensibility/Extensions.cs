using WiFo.Data;
using WiFo.UI;

namespace WiFo.Extensibility
{
	public interface IExtension
	{
		string Author
		{
			get;
		}

		string DisplayName
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
		void Draw(RecordTimeline timeline, uint startTime, uint endTime, IWiFoCanvas g);
		void OnClick(uint timeStamp, IWiFoContext wifo);
		void OnSelected(RecordTimeline timeline, IWiFoContext wifo);
		void OnUnSelected(RecordTimeline timeline, IWiFoContext wifo);
	}
}
