using System;
using System.ComponentModel;
using System.Drawing;

namespace WiFoUI.UI.Components
{
	public abstract class AbstractChart : System.Windows.Forms.Control
	{
		[CategoryAttribute("Behavior"), DescriptionAttribute("Locks manual time navigation to always display the latest update.")]
		public bool PanLock { get; set; }

		[CategoryAttribute("Behavior"), DescriptionAttribute("The starting point of the data range marker.")]
		public int MarkerStart { get; set; }

		[CategoryAttribute("Behavior"), DescriptionAttribute("The ending point of the data range marker.")]
		public int MarkerEnd { get; set; }

		[CategoryAttribute("Behavior"), DescriptionAttribute("Gets or sets the visible time domain.")]
		public int Span
		{
			get
			{
				return rightCX - leftCX;
			}
			set
			{
				if (value > 0)
				{
					rightCX = leftCX + value;
					Invalidate();
				}
			}
		}

		[CategoryAttribute("Appearance"), DescriptionAttribute("The color of the graph's bounding box.")]
		public Color BorderColor
		{
			get
			{
				return borderPen.Color;
			}
			set
			{
				borderPen.Color = value;
			}
		}

		[CategoryAttribute("Appearance"), DescriptionAttribute("The color of the graph's guide lines.")]
		public Color GridColor
		{
			get
			{
				return gridPen.Color;
			}
			set
			{
				gridPen.Color = value;
			}
		}

		[Browsable(false)]
		public Rectangle ChartBounds
		{
			get
			{
				return rect;
			}
		}

		public int ToCanvasX(int x)
		{
			double ppX = (double)rect.Width / (rightCX - leftCX);
			return (int)((x - leftCX) * ppX) + rect.Left;
		}
		public int ToCanvasY(double y)
		{
			double ppY = (double)rect.Height / (topCY - bottomCY);
			return rect.Bottom - (int)((y - bottomCY) * ppY);
		}

		public int ToDataX(int x)
		{
			double ppX = (double)rect.Width / (rightCX - leftCX);
			return (int)((x - rect.Left) / ppX) + leftCX;
		}

		public int ToDataX(double x)
		{
			double ppX = (double)rect.Width / (rightCX - leftCX);
			return (int)((x - rect.Left) / ppX) + leftCX;
		}

		public void CopyVisualStateTo(AbstractChart chart)
		{
			chart.leftCX = leftCX;
			chart.rightCX = rightCX;
			chart.MarkerStart = MarkerStart;
			chart.MarkerEnd = MarkerEnd;
		}

		protected abstract int GetMinimumX();

		protected override void OnResize(EventArgs e)
		{
			base.OnResize(e);
			Rectangle newRect = new Rectangle(24, 0, Width - 48, Height - 36);

			if (rect.Width > 0)
			{
				double ppX = (rightCX - leftCX) / (double)rect.Width;
				rightCX = rightCX + (int)((newRect.Width - rect.Width) * ppX);
			}

			rect = newRect;
			Invalidate();
		}

		protected override void OnMouseDown(System.Windows.Forms.MouseEventArgs e)
		{
			base.OnMouseDown(e);

			if (!PanLock)
			{
				dragStartX = e.X;

				if (e.Button == System.Windows.Forms.MouseButtons.Left)
				{
					dragStartLeftCX = leftCX;
					dragStartRightCX = rightCX;
				}
				else if (e.Button == System.Windows.Forms.MouseButtons.Right)
				{
					MarkerStart = ToDataX(e.X);
					MarkerEnd = MarkerStart;
				}
			}
		}

		protected override void OnMouseMove(System.Windows.Forms.MouseEventArgs e)
		{
			base.OnMouseMove(e);

			if (dragStartX > 0)
			{
				if (e.Button == System.Windows.Forms.MouseButtons.Left)
				{
					double f = (rightCX - leftCX) / (double)rect.Width;
					int dx = (int)((e.X - dragStartX) * f);
					dx = Math.Min(dx, dragStartLeftCX - GetMinimumX());
					leftCX = dragStartLeftCX - dx;
					rightCX = dragStartRightCX - dx;
				}
				else
				{
					int x = ToDataX(e.X);

					if (markerFixedStart)
					{
						if (x < MarkerStart)
						{
							MarkerStart = x;
							markerFixedStart = false;
						}
						else MarkerEnd = x;
					}
					else
					{
						if (x > MarkerEnd)
						{
							MarkerEnd = x;
							markerFixedStart = true;
						}
						else MarkerStart = x;
					}
				}

				Invalidate();
			}
		}

		protected override void OnMouseUp(System.Windows.Forms.MouseEventArgs e)
		{
			base.OnMouseUp(e);

			dragStartX = 0;
			dragStartLeftCX = 0;

			if (MarkerEnd - MarkerStart < 2)
			{
				MarkerStart = 0;
				Invalidate();
			}
		}

		protected override void OnPaintBackground(System.Windows.Forms.PaintEventArgs pevent) { }

		protected int leftCX = 0, rightCX = 1;
		protected double bottomCY = 0, topCY = 1;

		protected readonly Pen borderPen = new Pen(Color.FromArgb(0x6c, 0x86, 0x96));
		protected readonly Pen gridPen = new Pen(Color.FromArgb(0x6c, 0x86, 0x96));
		protected readonly Brush markerBrush = new SolidBrush(Color.FromArgb(100, 108, 194, 255));
		protected readonly Brush markerTextBrush = new SolidBrush(Color.FromArgb(108, 194, 255));
		protected readonly Font numberFont = new Font("Tahoma", 8, FontStyle.Regular, GraphicsUnit.Pixel);

		protected readonly Pen[] chartPens = {
			new Pen(Color.Black),
			new Pen(Color.Blue),
			new Pen(Color.Red),
			new Pen(Color.FromArgb(0, 100, 0)),
			new Pen(Color.FromArgb(0, 100, 100)),
		};

		private Rectangle rect;
		private int dragStartX, dragStartLeftCX, dragStartRightCX;
		private bool markerFixedStart = true;
	}
}
