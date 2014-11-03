using System.Drawing;

namespace WiFo.UI
{
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
		/// Draws a straight line on the canvas.
		/// </summary>
		/// <param name="color">The color of the line.</param>
		/// <param name="timestamp1">This first point's timestamp.</param>
		/// <param name="y1">The first point's y value, in pixels.</param>
		/// <param name="timestamp2">The second point's timestamp.</param>
		/// <param name="y2">The second point's y value, in pixels.</param>
		void DrawLine(Color color, uint timestamp1, int y1, uint timestamp2, int y2);

		/// <summary>
		/// Shades a whole area with the specified color.
		/// </summary>
		/// <param name="color">The color of the shade.</param>
		/// <param name="startTime">The start timestamp.</param>
		/// <param name="endTime">The end timestamp.</param>
		void DrawShade(Color color, uint startTime, uint endTime);

		/// <summary>
		/// Draws a string on the given point on the canvas, independent of the current time.
		/// </summary>
		/// <param name="color">The color of the text.</param>
		/// <param name="size">The font size to be used.</param>
		/// <param name="x">The x value of the text's left-most point.</param>
		/// <param name="y">The y value of the text's top-most point.</param>
		void DrawStaticString(Color color, int size, int x, int y);

		/// <summary>
		/// Draws a text at a point in timeline.
		/// </summary>
		/// <param name="color">The color of the text.</param>
		/// <param name="size">The font size to be used.</param>
		/// <param name="startTime">The timestamp where the left-most point of the text resides.</param>
		/// <param name="y">The y value of the text's top-most point.</param>
		void DrawString(Color color, int size, uint startTime, int y);
	}
}
