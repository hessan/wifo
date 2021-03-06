﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using WiFo.Data;
using WiFo.Expressions;

namespace WiFoUI.UI.Components
{
	public class TimelineChart : AbstractChart
	{
		public TimelineChart()
		{
			timeline = null;
			rightCX = 1000;
		}

		public TimelineChart(IContainer container)
			: this()
		{
			container.Add(this);
		}

		[Browsable(false)]
		public Expression Expression
		{
			get
			{
				return expression;
			}
			set
			{
				expression = value;
			}
		}

		[Browsable(false)]
		public RecordTimeline Timeline
		{
			get
			{
				return timeline;
			}
		}

		public void Initialize(RecordList list)
		{
			timeline = new RecordTimeline(list);
		}

		[CategoryAttribute("Behavior"), DescriptionAttribute("The number of ticks on the horizontal axis.")]
		public int TickInterval
		{
			get
			{
				return tickInterval;
			}
			set
			{
				tickInterval = Math.Max(10, value);
				Invalidate();
			}
		}

		protected override int GetMinimumX()
		{
			return (int)initialTime;
		}

		public void Clear()
		{
			timeline.Records.Clear();
			initialTime = 0;
		}

		protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
		{
			base.OnPaint(e);

			Graphics g = e.Graphics;
			Rectangle rect = ChartBounds;
			Font numberFont = fonts[0];

			g.FillRectangle(Brushes.White, 0, 0, Width, Height);
			
			int leftDisplayCX = leftCX - (int)initialTime;
			int firstTickX = tickInterval - (int)(leftDisplayCX % tickInterval) + leftCX;

			for (int cX = firstTickX, x = ToCanvasX(cX); x < rect.Right; cX += tickInterval, x = ToCanvasX(cX))
			{
				g.DrawLine(gridPen, (int)x, rect.Top, (int)x, rect.Bottom);
				string text = (cX - initialTime).ToString();
				SizeF sz = g.MeasureString(text, numberFont);
				g.DrawString(text, numberFont, Brushes.Black, (int)x - (int)sz.Width / 2, rect.Bottom + 16);
			}

			try
			{
				if (expression != null && timeline.Records.Count > 1)
				{
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

					int startIndex = timeline.GetIndexBefore((uint)leftCX);
					int endIndex = timeline.GetIndexAfter((uint)rightCX);

					RecordList records = timeline.Records;
					Record prevRecord = records[startIndex];
					List<bool> savedResult = expression.Evaluate(prevRecord.State);

					if (endIndex - startIndex > rect.Width * 2)
					{
						for (double xd = rect.Left; xd < rect.Right; xd += 0.5)
						{
							uint time = (uint)ToDataX(xd);
							uint state = timeline.GetStateAt(time);
							List<bool> result = expression.Evaluate(state);

							int x = (int)xd;

							for (int j = 0; j < result.Count; j++)
							{
								int y = rect.Bottom - (result[j] ? 20 : 0) - 35 * (j + 1);
								int savedY = rect.Bottom - (savedResult[j] ? 20 : 0) - 35 * (j + 1);
								g.DrawLine(chartPens[j], x, savedY, x, y);
								g.DrawEllipse(chartPens[j], x - 1, savedY - 1, 2, 2);
							}

							savedResult = result;
						}
					}
					else
					{
						for (int i = startIndex + 1; i <= endIndex; i++)
						{
							Record record = records[i];
							int leftX = ToCanvasX((int)prevRecord.Time);
							int x = ToCanvasX((int)record.Time);
							List<bool> result = expression.Evaluate(record.State);

							for (int j = 0; j < result.Count; j++)
							{
								int y = rect.Bottom - (result[j] ? 20 : 0) - 35 * (j + 1);
								int savedY = rect.Bottom - (savedResult[j] ? 20 : 0) - 35 * (j + 1);

								if (x < rect.Right)
								{
									g.DrawLine(chartPens[j], x, savedY, x, y);
									g.DrawEllipse(chartPens[j], x - 1, savedY - 1, 2, 2);
								}

								g.DrawLine(chartPens[j], Math.Max(rect.Left, leftX), savedY, Math.Min(rect.Right, x), savedY);
							}

							prevRecord = record;
							savedResult = result;
						}
					}
				}
			}
			catch (Exception ex)
			{
				g.DrawString(ex.GetType().Name + "\n" + ex.Message, SystemFonts.MenuFont, Brushes.Red, 16, 16);
			}

			if (MarkerStart > 0)
			{
				int ms = ToCanvasX(MarkerStart);

				Rectangle mrct = new Rectangle(ms, rect.Top, ToCanvasX(MarkerEnd) - ms, rect.Height);
				String text = (MarkerEnd - MarkerStart).ToString();
				SizeF sz = g.MeasureString(text, numberFont);

				if (mrct.Left < rect.Left)
				{
					int diff = rect.Left - mrct.Left;
					mrct.X += diff;
					mrct.Width -= diff;
				}
				else if (mrct.Right > rect.Right)
					mrct.Width -= mrct.Right - rect.Right;

				g.FillRectangle(markerBrush, mrct);

				int x = mrct.Left + (mrct.Width - (int)sz.Width) / 2;

				if (x > rect.Left && x + sz.Width < rect.Right)
					g.DrawString(text, numberFont, markerTextBrush, x, mrct.Top + 16);
			}

			g.DrawRectangle(borderPen, rect);
		}

		private RecordTimeline timeline;
		private Expression expression = null;
		private int tickInterval = 100;
		private uint initialTime = 0;
	}
}
