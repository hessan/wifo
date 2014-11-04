using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using WiFo.Data;
using WiFo.Extensibility;
using WiFo.UI;

namespace WiFoUI.UI.Components
{
	public class ViewCanvas : AbstractChart, IWiFoCanvas
	{
		public ViewCanvas() {
			view = null;
		}

		public ViewCanvas(IContainer container)
		{
			view = null;
			container.Add(this);
		}

		public ITimelineView TimelineView
		{
			get
			{
				return view;
			}
			set
			{
				view = value;
			}
		}

		public void Initialize(RecordList list)
		{
			timeline = new RecordTimeline(list);
		}

		public void DrawLine(Color color, uint timestamp1, int y1, uint timestamp2, int y2)
		{
			Pen pen = this.pen;
			pen.Color = color;
			float x1 = ToCanvasX((int)timestamp1);
			float x2 = ToCanvasX((int)timestamp2);
			g.DrawLine(pen, x1, y1, x2, y2);
		}

		public void DrawRect(Color color, uint timestamp, int y, uint duration, int height)
		{
			Pen pen = this.pen;
			pen.Color = color;
			float x1 = ToCanvasX((int)timestamp);
			float x2 = ToCanvasX((int)(timestamp + duration));
			g.DrawRectangle(pen, x1, y, x2 - x1 + 1, height);
		}

		public void FillRect(Color color, uint timestamp, int y, uint duration, int height)
		{
			SolidBrush brush = this.brush;
			brush.Color = color;
			float x1 = ToCanvasX((int)timestamp);
			float x2 = ToCanvasX((int)(timestamp + duration));
			g.FillRectangle(brush, x1, y, x2 - x1 + 1, height);
		}

		public void DrawString(string s, Color color, TextStyle style, uint startTime, int y)
		{
			SolidBrush brush = this.brush;
			Font font = fonts[(int)style];
			float x = ToCanvasX((int)startTime);
			System.Diagnostics.Debug.WriteLine(leftCX);
			brush.Color = color;
			g.DrawString(s, font, brush, x, y + ChartBounds.Top);
		}

		public void DrawStaticString(string s, Color color, TextStyle style, int x, int y)
		{
			SolidBrush brush = this.brush;
			Font font = fonts[(int)style];
			brush.Color = color;
			g.DrawString(s, font, brush, x + ChartBounds.Left, y + ChartBounds.Top);
		}

		protected override int GetMinimumX()
		{
			return (int)initialTime;
		}

		protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
		{
			base.OnPaint(e);

			e.Graphics.FillRectangle(Brushes.White, e.Graphics.ClipBounds);

			if (view != null && timeline.Records.Count > 1)
			{
				g = e.Graphics;

				if (initialTime == 0)
				{
					int span = Span;
					initialTime = timeline.Records.FirstRecord.Time;
					leftCX = Math.Max(leftCX, (int)initialTime);
					rightCX = leftCX + span;
				}

				if (PanLock)
				{
					int span = Span;
					leftCX = Math.Max((int)initialTime, (int)timeline.EndTime - span);
					rightCX = leftCX + span;
				}

				Region savedRegion = g.Clip;

				try
				{
					g.Clip = new Region(ChartBounds);
					view.Draw(timeline, (uint)leftCX, (uint)rightCX, this);
				}
				catch (Exception ex)
				{
					g.Clip.MakeInfinite();
					string s = ex.GetType().Name;
					SizeF sz = g.MeasureString(s, SystemFonts.MenuFont);
					g.DrawString(s + "\n" + ex.Message, SystemFonts.MenuFont, Brushes.Red, 16, 16);
					g.DrawString(ex.StackTrace, SystemFonts.MenuFont, Brushes.Red, 16, 16 + (float)sz.Height * 2);
						
				}

				g.Clip = savedRegion;
				g.DrawRectangle(borderPen, ChartBounds);
				g = null;
			}

		}

		private ITimelineView view;
		private RecordTimeline timeline;
		private Graphics g;
		private SolidBrush brush = new SolidBrush(Color.White);
		private Pen pen = new Pen(Color.Blue);
		private uint initialTime = 0;
	}
}
