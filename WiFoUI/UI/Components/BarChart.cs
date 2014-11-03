using System;
using System.ComponentModel;
using System.Drawing;

namespace WiFoUI.UI.Components
{
	public class BarChart : AbstractChart
	{
		public BarChart()
		{
			DoubleBuffered = true;
		}

		public BarChart(IContainer container)
		{
			container.Add(this);
			DoubleBuffered = true;
		}

		public object[] XValues
		{
			get
			{
				return xs;
			}
			set
			{
				xs = value;

				if (value == null)
					ys = null;
				else {
					leftCX = -1;
					rightCX = value.Length + 1;
				}
			}
		}

		public double[] YValues
		{
			get
			{
				return ys;
			}
			set
			{
				ys = value;

				if (value == null)
					xs = null;
				else
				{
					double min = double.MaxValue, max = double.MinValue;

					foreach (double i in value)
					{
						if (i < min)
							min = i;
						else if (i > max)
							max = i;
					}

					bottomCY = 0;
					topCY = max * 3 / 2;
				}

			}
		}

		[CategoryAttribute("Behavior"), DescriptionAttribute("The number of ticks on the vertical axis.")]
		public int YTicks
		{
			get
			{
				return yticks;
			}
			set
			{
				yticks = Math.Max(5, value);
			}
		}

		protected override void OnMouseMove(System.Windows.Forms.MouseEventArgs e)
		{
			base.OnMouseMove(e);
			mouseX = e.X;
			Invalidate();
		}

		protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
		{
			base.OnPaint(e);

			rightCX = Math.Min(Width / 8, rightCX);
			
			Graphics g = e.Graphics;
			Rectangle rect = ChartBounds;

			g.FillRectangle(Brushes.White, 0, 0, Width, Height);

			double span = Span;
			double xfactor = (double)rect.Width / (double)span;
			double yfactor = (rect.Height - 10) / (topCY - bottomCY);

			DrawGraph(g);

			if (mouseX > rect.Left && mouseX < rect.Right)
			{
				int index = ToDataX(mouseX);
				int discreteCX = ToCanvasX(index);

				if(index >= 0 && index < xs.Length) {

					string text;
					SizeF sz;
					int y = ToCanvasY(ys[index]);

					if (y > rect.Top && y < rect.Bottom)
					{
						text = ys[index].ToString();
						sz = g.MeasureString(text, numberFont);
						g.DrawLine(borderPen, rect.Left, y, rect.Right, y);
						g.DrawString(text, numberFont, Brushes.Black, rect.Left - sz.Width, y - sz.Height / 2);
					}

					text = xs[index].ToString();
					sz = g.MeasureString(text, numberFont);
					g.DrawLine(borderPen, discreteCX, rect.Top, discreteCX, rect.Bottom);
					g.DrawString(text, numberFont, Brushes.Black, Math.Max(1, discreteCX - sz.Width / 2), rect.Bottom + 10);
				}
			}
		}

		protected void DrawGraph(Graphics g)
		{
			Rectangle rect = ChartBounds;

			try
			{
				if (xs != null && ys != null && xs.Length > 1)
				{
					int barHalfWidth = (ToCanvasX(1) - ToCanvasX(0)) / 4;

					for (int i = Math.Max(0, leftCX); i <= rightCX; i++)
					{
						if (i < xs.Length)
						{
							int x = ToCanvasX(i), y = ToCanvasY(ys[i]);
							g.FillRectangle(Brushes.DarkGreen, Math.Max(rect.Left, x - barHalfWidth), y, barHalfWidth << 1, rect.Bottom - y);
						}
					}
				}
			}
			catch (Exception ex)
			{
				g.DrawString(ex.Message, numberFont, Brushes.Black, 10, 10);
			}

			g.DrawRectangle(borderPen, rect);
		}

		protected override int GetMinimumX()
		{
			return -1;
		}

		private object[] xs = null;
		private double[] ys = null;
		private int yticks = 10;
		private int mouseX;
	}
}
