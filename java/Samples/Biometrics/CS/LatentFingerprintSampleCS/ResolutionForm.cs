using System;
using System.Drawing;
using System.Windows.Forms;

namespace Neurotec.Samples
{
	public partial class ResolutionForm : Form
	{
		#region Private Variables

		private Point _startPt;
		private Point _endPt;
		private bool _dragging;

		private const int EndMarkerSize = 4;

		#endregion Private Variables

		#region Constructor
		public ResolutionForm()
		{
			InitializeComponent();
		}
		#endregion

		#region Public Properties
		public float HorzResolution
		{
			get
			{
				return Convert.ToSingle(nudHorzResolution.Value);
			}
			set
			{
				decimal val = Convert.ToDecimal(value);
				if (val < nudHorzResolution.Minimum)
				{
					val = nudHorzResolution.Minimum;
				}
				else if (val > nudHorzResolution.Maximum)
				{
					val = nudHorzResolution.Maximum;
				}
				nudHorzResolution.Value = val;
			}
		}

		public float VertResolution
		{
			get
			{
				return Convert.ToSingle(nudVertResolution.Value);
			}
			set
			{
				decimal val = Convert.ToDecimal(value);
				if (val < nudVertResolution.Minimum)
				{
					val = nudVertResolution.Minimum;
				}
				else if (val > nudVertResolution.Maximum)
				{
					val = nudVertResolution.Maximum;
				}
				nudVertResolution.Value = val;
			}
		}

		public Bitmap FingerImage
		{
			set
			{
				pbFingerprint.Image = value;
			}
		}

		#endregion Public Properties

		#region Event Handling

		private float CalculateDistance()
		{
			return Convert.ToSingle(Math.Sqrt((_startPt.X - _endPt.X) * (double)(_startPt.X - _endPt.X) + (_startPt.Y - _endPt.Y) * (double)(_startPt.Y - _endPt.Y)));
		}

		private void pbFingerprint_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				_startPt = e.Location;
				_endPt = Point.Empty;
				pbFingerprint.Cursor = Cursors.Cross;
				_dragging = true;
			}
		}

		private void pbFingerprint_MouseMove(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left
				&& _dragging)
			{
				_endPt = e.Location;
				pbFingerprint.Invalidate();
			}
		}

		private void pbFingerprint_MouseUp(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left
				&& _dragging)
			{
				_endPt = e.Location;
				Cursor = Cursors.Arrow;
				_dragging = false;

				float scaleToInch = 1.0f;
				if (rbCentimeters.Checked)
				{
					scaleToInch = 1.0f / 2.54f;
				}

				float distance = CalculateDistance();
				if (distance > 50.0)
				{
					nudHorzResolution.Value = Convert.ToDecimal(distance / (Convert.ToSingle(nudUnitScale.Value) * scaleToInch));
					nudVertResolution.Value = nudHorzResolution.Value;
				}
				else
				{
					Utilities.ShowInformation(@"Please draw a longer line segment.");
				}

				_startPt = Point.Empty;
				_endPt = Point.Empty;
				pbFingerprint.Invalidate();
			}
		}

		private void DrawSelectionLine(Graphics g, Pen pen)
		{
			// draw pointer markers
			g.DrawLine(pen, _startPt.X - EndMarkerSize, _startPt.Y, _startPt.X + EndMarkerSize, _startPt.Y);
			g.DrawLine(pen, _startPt.X, _startPt.Y - EndMarkerSize, _startPt.X, _startPt.Y + EndMarkerSize);
			g.DrawLine(pen, _endPt.X - EndMarkerSize, _endPt.Y, _endPt.X + EndMarkerSize, _endPt.Y);
			g.DrawLine(pen, _endPt.X, _endPt.Y - EndMarkerSize, _endPt.X, _endPt.Y + EndMarkerSize);
			// draw line between starting and ending points
			g.DrawLine(pen, _startPt, _endPt);
		}

		private void pbFingerprint_Paint(object sender, PaintEventArgs e)
		{
			if (!_startPt.IsEmpty
				&& !_endPt.IsEmpty)
			{
				Graphics g = e.Graphics;

				System.Drawing.Drawing2D.SmoothingMode smoothingMode = g.SmoothingMode;
				g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

				using (var pen = new Pen(Color.White, 3))
				{
					DrawSelectionLine(g, pen);
				}

				using (var pen = new Pen(Color.Green, 1))
				{
					DrawSelectionLine(g, pen);
				}

				g.SmoothingMode = smoothingMode;
			}
		}

		private void nudHorzResolution_ValueChanged(object sender, EventArgs e)
		{
			errorProvider1.SetError(nudHorzResolution,
				nudHorzResolution.Value < 250m ? "Current resolution is lower than recommended minimum." : "");
		}

		private void nudVertResolution_ValueChanged(object sender, EventArgs e)
		{
			errorProvider1.SetError(nudVertResolution,
				nudVertResolution.Value < 250m ? "Current resolution is lower than recommended minimum." : "");
		}

		#endregion Event Handling
	}
}
