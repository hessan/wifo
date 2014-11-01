using System.Drawing;

namespace WiFo.UI
{
	public interface IWiFoCanvas
	{
		int Width
		{
			get;
		}

		int Height
		{
			get;
		}

		void DrawShade(Color color, uint startTime, uint endTime);
		void DrawLine(Color color, uint startTime, int y1, uint endTime, int y2);
		void DrawString(Color color, int size, uint startTime, int y);
		void DrawStaticString(Color color, int size, int x, int y);
	}
}
