using System.Drawing;

namespace WiFo.UI
{
	/// <summary>
	/// Specifies constants defining which of WiFo's unified text styles to use.
	/// </summary>
	public enum TextStyle
	{
		/// <summary>
		/// Regular-sized text with no decoration.
		/// </summary>
		NormalText = 0,

		/// <summary>
		/// Regular-sized bold text.
		/// </summary>
		BoldText,
	}

	/// <summary>
	/// Defines a canvas that can be used for timeline oriented plotting of data.
	/// </summary>
	public interface IWiFoCanvas
	{
		/// <summary>
		/// Gets the width, in pixels, of the canvas.
		/// </summary>
		int Width { get; }

		/// <summary>
		/// Gets the height, in pixels, of the canvas.
		/// </summary>
		int Height { get; }

		/// <summary>
		/// Draws a straight line along the specified points.
		/// </summary>
		/// <param name="color">The color of the line.</param>
		/// <param name="timestamp1">This first point's timestamp.</param>
		/// <param name="y1">The first point's y value, in pixels.</param>
		/// <param name="timestamp2">The second point's timestamp.</param>
		/// <param name="y2">The second point's y value, in pixels.</param>
		void DrawLine(Color color, uint timestamp1, int y1, uint timestamp2, int y2);

		/// <summary>
		/// Draws a rectangular stroke bounded by the specified timestamps and vertical bounds, with the specified color.
		/// </summary>
		/// <param name="color">The fill color of the rectangle.</param>
		/// <param name="timestamp">This first point's timestamp.</param>
		/// <param name="y">The first point's y value, in pixels.</param>
		/// <param name="duration">The duration covered by the rectangle.</param>
		/// <param name="height">The height of the rectangle.</param>
		void DrawRect(Color color, uint timestamp, int y, uint duration, int height);

		/// <summary>
		/// Fills a rectangle bounded by the specified timestamps and vertical bounds, with the specified color.
		/// </summary>
		/// <param name="color">The fill color of the rectangle.</param>
		/// <param name="timestamp">This first point's timestamp.</param>
		/// <param name="y">The first point's y value, in pixels.</param>
		/// <param name="duration">The duration covered by the rectangle.</param>
		/// <param name="height">The height of the rectangle.</param>
		void FillRect(Color color, uint timestamp, int y, uint duration, int height);

		/// <summary>
		/// Draws a string on the given point on the canvas, independent of the current time.
		/// </summary>
		/// <param name="s">The string to be drawn.</param>
		/// <param name="color">The color of the text.</param>
		/// <param name="style">The unified text style to use.</param>
		/// <param name="x">The x value of the text's left-most point.</param>
		/// <param name="y">The y value of the text's top-most point.</param>
		void DrawStaticString(string s, Color color, TextStyle style, int x, int y);

		/// <summary>
		/// Draws a text at a point in timeline.
		/// </summary>
		/// <param name="s">The string to be drawn.</param>
		/// <param name="color">The color of the text.</param>
		/// <param name="style">The unified text style to use.</param>
		/// <param name="startTime">The timestamp where the left-most point of the text resides.</param>
		/// <param name="y">The y value of the text's top-most point.</param>
		void DrawString(string s, Color color, TextStyle style, uint startTime, int y);
	}
}
