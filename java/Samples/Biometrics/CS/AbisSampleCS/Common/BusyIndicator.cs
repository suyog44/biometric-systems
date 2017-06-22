using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Neurotec.Samples
{
	public class BusyIndicator : Control
	{
		#region Public constructor

		public BusyIndicator()
		{
			InitializeComponent();
			DoubleBuffered = true;
			timer.Start();
		}

		#endregion

		#region Private fields

		private Timer timer;
		private IContainer components;
		private int currentAngle = 0;

		#endregion

		#region Private constants

		private const int Radius = 100;
		private const int CircleRadius = 33;
		private const int BorderMargin = 4;
		private const int Center = BorderMargin + CircleRadius + Radius;
		private const float CombindedSize = BorderMargin + CircleRadius + Radius * 2 + CircleRadius + BorderMargin;

		#endregion

		#region Private methods

		private RectangleF GetRectangle(double angle)
		{
			double x = Center + Radius * Math.Cos(angle);
			double y = Center + Radius * Math.Sin(angle);

			return new RectangleF((float)x - CircleRadius, (float)y - CircleRadius, CircleRadius * 2, CircleRadius * 2);
		}

		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.timer = new System.Windows.Forms.Timer(this.components);
			this.SuspendLayout();
			// 
			// timer
			// 
			this.timer.Interval = 33;
			this.timer.Tick += new System.EventHandler(this.TimerTick);
			this.ResumeLayout(false);
		}

		private void TimerTick(object sender, EventArgs e)
		{
			currentAngle = (currentAngle + 10) % 360;
			Invalidate();
		}

		#endregion

		#region Protected methods

		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);

			Graphics g = e.Graphics;
			g.SmoothingMode = SmoothingMode.AntiAlias;

			Size sz = this.Size;
			float zoom = Math.Min(sz.Width / CombindedSize, sz.Height / CombindedSize);
			float dx = (zoom * CombindedSize - sz.Width) / 2;
			float dy = (zoom * CombindedSize - sz.Height) / 2;

			Matrix m = g.Transform;
			m.RotateAt(currentAngle, new PointF(sz.Width / 2, sz.Height / 2));
			g.Transform = m;

			g.TranslateTransform(-dx, -dy);
			g.ScaleTransform(zoom, zoom);

			byte value = 0;
			for (double angle = 0; angle < 2 * Math.PI; angle += Math.PI / 4)
			{
				value += 25;
				using (var b = new SolidBrush(Color.FromArgb(value, value, value)))
					g.FillEllipse(b, GetRectangle(angle));
			}
		}

		#endregion
	}
}
