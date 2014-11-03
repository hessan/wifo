using WiFo.Data;
using WiFo.UI;

namespace WiFo.Extensibility
{
	/// <summary>
	/// Defines a extension, which adds additional functionality to WiFo.
	/// </summary>
	public interface IExtension
	{
		/// <summary>
		/// Gets the name of the author of the extension.
		/// </summary>
		string Author
		{
			get;
		}

		/// <summary>
		/// Gets the name to be displayed in the user interface.
		/// </summary>
		string DisplayName
		{
			get;
		}
	}

	/// <summary>
	/// Defines an extension that performs an analysis on records and produces results.
	/// </summary>
	/// <seealso cref="IExtension" />
	/// <seealso cref="Record" />
	public interface IStudy : IExtension
	{
		/// <summary>
		/// Performs the study on the records and optionally produces some results.
		/// </summary>
		/// <param name="records">The list of records to be studied.</param>
		/// <param name="wifo">User interface context for input/output.</param>
		/// <seealso cref="UserInput" />
		/// <seealso cref="UserOutput" />
		void Perform(RecordList records, IWiFoContext wifo);
	}

	/// <summary>
	/// Defines an alternative custom representation of data on the timeline.
	/// </summary>
	public interface ITimelineView : IExtension
	{
		/// <summary>
		/// Draws the graphical representation of data within the given timeframe.
		/// </summary>
		/// <param name="timeline">Timeline-wrapped record list.</param>
		/// <param name="startTime">The start of the area of interest.</param>
		/// <param name="endTime">The end of the area of interest.</param>
		/// <param name="g">The graphical on which drawing is performed.</param>
		/// <seealso cref="RecordTimeline" />
		/// <seealso cref="IWiFoCanvas" />
		void Draw(RecordTimeline timeline, uint startTime, uint endTime, IWiFoCanvas g);

		/// <summary>
		/// This method is invoked whenever the user clicks on the view.
		/// </summary>
		/// <param name="timeStamp">The timestamp the clicked point represents.</param>
		/// <param name="wifo">User interface context for input/output.</param>
		void OnClick(uint timeStamp, IWiFoContext wifo);

		/// <summary>
		/// This method is invoked when this view is switched to, so it can run initialization code.
		/// </summary>
		/// <param name="timeline">Timeline-wrapped record list.</param>
		/// <param name="wifo">User interface context for input/output.</param>
		/// <remarks>
		/// This method may be invoked more than once per session, as it is invoked whenever the
		/// view is selected. So, one-off initialization that does not depend on the context can still
		/// be performed in a constructor.
		/// </remarks>
		void OnSelected(RecordTimeline timeline, IWiFoContext wifo);

		/// <summary>
		/// This method is invoked when this view is switched to, so it can free its resources.
		/// </summary>
		/// <param name="timeline">Timeline-wrapped record list.</param>
		/// <param name="wifo">User interface context for input/output.</param>
		/// <remarks>
		/// This method might be called more than once per session, and the view might be switched
		/// back to, even after this method is called (following a new invocation of
		/// <see cref="OnSelected(RecordTimeline, IWiFoContext)" />). So, it is adviced that the developer
		/// does not release global session state in this method, or make sure to reallocate it in
		/// <see cref="OnSelected(RecordTimeline, IWiFoContext)" />.
		/// </remarks>
		void OnUnSelected(RecordTimeline timeline, IWiFoContext wifo);
	}
}
