using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
using Neurotec.Images;
using Neurotec.Images.Processing;

namespace Neurotec.Samples
{
	public partial class BandpassFilteringForm : Form
	{
		#region Private types

		enum PenType
		{
			Circle,
			Rectangle
		};

		#endregion

		#region Private fields

		readonly ImageAttributes _imageAtt;
		readonly NImage _originalReal;
		readonly NImage _originalImaginary;
		private readonly int _imgWidth;
		private readonly int _imgHeight;
		readonly uint _originalWidth;
		readonly uint _originalHeight;
		readonly Graphics _graphics;
		readonly Graphics _gr;
		readonly Bitmap _maskBitmap;
		readonly Bitmap _fftBitmap;

		NImage _result;
		//Bitmap _bm;
		int _lastX, _lastY;
		PenType _penType;
		bool _allowPainting;

		#endregion

		#region Public properties

		public NImage ResultImage
		{
			get
			{
				return _result;
			}
		}

		#endregion

		#region Public constructor

		public BandpassFilteringForm(NImage image)
		{
			float[][] matrixItems ={ 
			   new float[] {1, 0, 0, 0, 0},
			   new float[] {0, 0, 0, 0, 0},
			   new float[] {0, 0, 0, 0, 0},
			   new [] {0, 0, 0, 0.2f, 0}, 
			   new float[] {0, 0, 0, 0, 1}};
			var colorMatrix = new ColorMatrix(matrixItems);

			InitializeComponent();

			penSize.Value = 20;
			_penType = PenType.Circle;
			radioCircle.Checked = true;
			_allowPainting = false;

			_imageAtt = new ImageAttributes();
			_imageAtt.SetColorMatrix(colorMatrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);

			try
			{
				uint w;
				uint h;
				Ngip.FFTGetOptimalSize(image, out w, out h);

				_imgWidth = (int)w;
				_imgHeight = (int)h;
				_originalWidth = image.Width;
				_originalHeight = image.Height;

				_gr = Graphics.FromImage(viewFourierMask.Image = new Bitmap((int)w, (int)h));
				_graphics = Graphics.FromImage(_maskBitmap = new Bitmap((int)w, (int)h));

				NImage original = NImage.Create(NPixelFormat.Grayscale8U, w, h, 0);
				original.HorzResolution = image.HorzResolution;
				original.VertResolution = image.VertResolution;
				original.ResolutionIsAspectRatio = false;

				_result = NImage.Create(NPixelFormat.Grayscale8U, w, h, 0);
				_result.HorzResolution = original.HorzResolution;
				_result.VertResolution = original.VertResolution;
				_result.ResolutionIsAspectRatio = false;

				NImage.Copy(image, 0, 0, _originalWidth, _originalHeight, original, w / 2 - _originalWidth / 2, h / 2 - _originalHeight / 2);

				Ngip.FFT(original, out _originalReal, out _originalImaginary);

				NImage fftImage = Ngip.CreateMagnitudeFromSpectrum(_originalReal, _originalImaginary);
				_fftBitmap = NImage.FromImage(NPixelFormat.Rgb8U, 0, ShiftFFT(fftImage)).ToBitmap();
			}
			catch (Exception ex)
			{
				Utilities.ShowError("Error creating FFT image: {0}", ex.Message);
				throw;
			}

			FillMask(Color.Black);
			UpdateFFT(true);
		}

		public override sealed string Text
		{
			get { return base.Text; }
			set { base.Text = value; }
		}

		#endregion

		#region Helper functions

		public void Invert(Bitmap b)
		{
			for (int x = 0; x < b.Width; x ++)
			{
				for (int y = 0; y < b.Height; y ++)
				{
					Color myColor = b.GetPixel(x, y);
					Color myColor2 = Color.FromArgb(myColor.A, 255 - myColor.R, 255 - myColor.G, 255 - myColor.B);
					b.SetPixel(x, y, myColor2);
				}
			}
		}

		private void FillMask(Color color)
		{
			var rect = new Rectangle(0, 0, _imgWidth, _imgHeight);
			_graphics.FillRectangle(new SolidBrush(color), rect);
		}

		private void Draw(MouseEventArgs e)
		{
			Color color;
			if (e.Button == MouseButtons.Left)
			{
				color = Color.White;
			}
			else if (e.Button == MouseButtons.Right)
			{
				color = Color.Black;
			}
			else
				return;

			Pen pen;
			if (_lastX == e.X && _lastY == e.Y)
			{
				pen = new Pen(color, 1);
				var rect = new Rectangle(e.X - penSize.Value / 2, e.Y - penSize.Value / 2, penSize.Value, penSize.Value);

				if (_penType == PenType.Circle)
				{
					_graphics.DrawEllipse(pen, rect);
					_graphics.FillEllipse(new SolidBrush(color), rect);
				}
				else
				{
					_graphics.DrawRectangle(pen, rect);
					_graphics.FillRectangle(new SolidBrush(color), rect);
				}
			}
			else
			{
				pen = new Pen(color, penSize.Value);

				if (_penType == PenType.Circle)
				{
					pen.EndCap = System.Drawing.Drawing2D.LineCap.Round;
					pen.StartCap = System.Drawing.Drawing2D.LineCap.Round;
				}
				else
				{
					pen.EndCap = System.Drawing.Drawing2D.LineCap.Square;
					pen.StartCap = System.Drawing.Drawing2D.LineCap.Square;
				}

				_graphics.DrawLine(pen, _lastX, _lastY, e.X, e.Y);
			}
			pen.Dispose();
		}

		private void UpdateFFT(bool ifft)
		{
			NImage mask = null;
			NImage resultReal = null;
			NImage resultImaginary = null;
			NImage bmpimg = null;
			NImage tmp = null;

			try
			{
				if (ifft)
				{
					bmpimg = NImage.FromBitmap(_maskBitmap);
					mask = ShiftFFT(NImage.FromImage(NPixelFormat.Grayscale8U, 0, bmpimg));

					resultReal = NImage.FromImage(_originalReal.PixelFormat, 0, _originalReal);
					resultImaginary = NImage.FromImage(_originalImaginary.PixelFormat, 0, _originalImaginary);

					Ngip.ApplyMaskToSpectrum(resultReal, resultImaginary, mask);

					Ngip.IFFT(resultReal, resultImaginary, out tmp);
					if (tmp != null)
					{
						_result = tmp.Crop((tmp.Width - _originalWidth) / 2, (tmp.Height - _originalHeight) / 2, _originalWidth, _originalHeight);
						viewResult.Image = _result.ToBitmap();
						viewResult.Invalidate();
					}
				}
				_gr.DrawImage(_fftBitmap, 0, 0, _imgWidth, _imgHeight);
				_gr.DrawImage(_maskBitmap,
				   new Rectangle(0, 0, _imgWidth, _imgHeight),  // destination rectangle
				   0.0f, 0.0f, _imgWidth, _imgHeight, GraphicsUnit.Pixel, _imageAtt);
			}
			catch (Exception ex)
			{
				Utilities.ShowError("Error updating FFT image: {0}", ex.Message);
				return;
			}
			finally
			{
				if (tmp != null)
				{
					tmp.Dispose();
				}

				if (bmpimg != null)
				{
					bmpimg.Dispose();
				}

				if (resultImaginary != null)
				{
					resultImaginary.Dispose();
				}

				if (resultReal != null)
				{
					resultReal.Dispose();
				}

				if (mask != null)
				{
					mask.Dispose();
				}
			}

			viewFourierMask.Refresh();
		}

		private NImage ShiftFFT(NImage inp)
		{
			var shifted = NImage.FromImage(NPixelFormat.Grayscale8U, 0, inp);
			var inpCloned = (NImage)inp.Clone();
			uint cx = inp.Width / 2;
			uint cy = inp.Height / 2;

			NImage.Copy(inpCloned, 0, 0, cx, cy, shifted, cx, cy);
			NImage.Copy(inpCloned, cx, cy, cx, cy, shifted, 0, 0);
			NImage.Copy(inpCloned, cx, 0, cx, cy, shifted, 0, cy);
			NImage.Copy(inpCloned, 0, cy, cx, cy, shifted, cx, 0);

			return shifted;
		}

		#endregion

		#region Mouse events

		private void viewFourier_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left || e.Button == MouseButtons.Right)
			{
				_lastX = e.X;
				_lastY = e.Y;
				_allowPainting = true;
			}
		}

		private void viewFourier_MouseMove(object sender, MouseEventArgs e)
		{
			if (!_allowPainting)
				return;
			if (e.Button != MouseButtons.Left && e.Button != MouseButtons.Right) return;
			Draw(e);
			_lastX = e.X;
			_lastY = e.Y;
			UpdateFFT(false);
		}

		private void viewFourier_MouseUp(object sender, MouseEventArgs e)
		{
			if (!_allowPainting)
				return;
			if (e.Button != MouseButtons.Left && e.Button != MouseButtons.Right) return;
			Draw(e);
			_lastX = e.X;
			_lastY = e.Y;
			UpdateFFT(bRealtime.Checked);
			_allowPainting = false;
		}
		#endregion

		#region Button clicks
		private void button4_Click(object sender, EventArgs e)
		{
			FillMask(Color.White);
			UpdateFFT(bRealtime.Checked);
		}

		private void button3_Click(object sender, EventArgs e)
		{
			FillMask(Color.Black);
			UpdateFFT(bRealtime.Checked);
		}

		private void button5_Click(object sender, EventArgs e)
		{
			Invert(_maskBitmap);
			UpdateFFT(bRealtime.Checked);
		}

		private void button6_Click(object sender, EventArgs e)
		{
			UpdateFFT(true);
		}

		private void button1_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void button2_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void radioCircle_CheckedChanged(object sender, EventArgs e)
		{
			if ( radioCircle.Checked ) 
				_penType = PenType.Circle;
			radioRect.Checked = !radioCircle.Checked;
		}

		private void radioRect_CheckedChanged(object sender, EventArgs e)
		{
			if (radioRect.Checked)
				_penType = PenType.Rectangle;
			radioCircle.Checked = !radioRect.Checked;
		}

		#endregion
	}
}
