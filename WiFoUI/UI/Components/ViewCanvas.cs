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

		public void DrawShade(System.Drawing.Color color, uint startTime, uint endTime)
		{
			throw new NotImplementedException();
		}

		public void DrawLine(System.Drawing.Color color, uint startTime, int y1, uint endTime, int y2)
		{
			throw new NotImplementedException();
		}

		public void DrawString(System.Drawing.Color color, int size, uint startTime, int y)
		{
			throw new NotImplementedException();
		}

		public void DrawStaticString(System.Drawing.Color color, int size, int x, int y)
		{
			throw new NotImplementedException();
		}

		protected override int GetMinimumX()
		{
			return (int)timeline.Records.FirstRecord.Time;
		}

		protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
		{
			base.OnPaint(e);

			e.Graphics.FillRectangle(Brushes.White, e.Graphics.ClipBounds);

			if (view != null)
			{
				g = e.Graphics;
				g.FillRectangle(Brushes.White, Bounds);
				view.Draw(timeline, (uint)leftCX, (uint)rightCX, this);
				g = null;
			}

		}

		private ITimelineView view;
		private RecordTimeline timeline;
		private Graphics g;
	}
}
