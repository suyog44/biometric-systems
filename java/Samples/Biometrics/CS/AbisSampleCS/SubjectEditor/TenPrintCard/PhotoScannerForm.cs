using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Drawing;
using Neurotec.Images;

namespace Neurotec.Samples.TenPrintCard
{
	public class NPhotoScannerForm : Form, IMessageFilter
	{
		const double InchesPerMeter = 39.3700787;

		private Twain _tw;
		private BITMAPINFOHEADER _bmi;
		private bool _scanning;
		private NImage _image;

		private IntPtr _bmpptr;
		private IntPtr _pixptr;
		private IntPtr _dibhand;

		public NPhotoScannerForm(Form parent)
		{
			InitializeComponent();

			Owner = parent;
			_tw = new Twain();

			IntPtr handle = IntPtr.Zero;
			if (parent != null) handle = parent.Handle;
			if (!_tw.Init(handle))
				throw new Exception("Twain library failed to initialize");
		}

		private void Clearmem()
		{
			if (_dibhand != IntPtr.Zero)
			{
				GlobalFree(_dibhand);
				_dibhand = IntPtr.Zero;
			}
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing)
				_tw.Finish();

			Clearmem();

			base.Dispose(disposing);
		}

		public void SelectDevice()
		{
			_tw.Select();
		}

		public NImage Scan()
		{
			Owner.Enabled = false;

			try
			{
				_scanning = true;
				Application.AddMessageFilter(this);

				if (!_tw.Acquire())
					return null;

				while (_scanning)
				{
					Application.DoEvents();
					System.Threading.Thread.Sleep(50);
				}
			}
			finally
			{
				EndingScan();
				Owner.Enabled = true;
			}

			if (_image == null)
			{
				Clearmem();
				return null;
			}

			NImage img = _image;
			img.HorzResolution = (float)Math.Round(_bmi.biXPelsPerMeter / InchesPerMeter);
			img.VertResolution = (float)Math.Round(_bmi.biYPelsPerMeter / InchesPerMeter);
			Clearmem();

			return img;
		}

		bool IMessageFilter.PreFilterMessage(ref Message m)
		{
			TwainCommand cmd = _tw.PassMessage(ref m);
			if (cmd == TwainCommand.Not)
				return false;

			switch (cmd)
			{
				case TwainCommand.CloseRequest:
				{
					EndingScan();
					_tw.CloseSrc();
					break;
				}
				case TwainCommand.CloseOk:
				{
					EndingScan();
					_tw.CloseSrc();
					break;
				}
				case TwainCommand.DeviceEvent:
				{
					break;
				}
				case TwainCommand.TransferReady:
				{
					List<Twain.TransferedPicture> pics = _tw.TransferPictures();
					EndingScan();
					_tw.CloseSrc();

					if (pics.Count == 0)
						break;

					// only use first scanned image, free others
					for (int i = 1; i < pics.Count; i++)
					{
						IntPtr img = pics[i].hBitmap;

						if (img != IntPtr.Zero)
							GlobalFree(img);
					}

					SetImage(pics[0].hBitmap, pics[0].xResolution, pics[0].yResolution);

					break;
				}
			}

			return true;
		}

		private void EndingScan()
		{
			if (_scanning)
			{
				Application.RemoveMessageFilter(this);
				_scanning = false;
			}
		}

		private void SetImage(IntPtr img, int xResolution, int yResolution)
		{
			_dibhand = img;
			_bmpptr = GlobalLock(_dibhand);
			_pixptr = GetPixelInfo(_bmpptr);

			using(Bitmap bmp = new Bitmap(_bmi.biWidth, _bmi.biHeight))
			{
				if (_bmi.biXPelsPerMeter != 0
					&& _bmi.biYPelsPerMeter != 0)
				{
					bmp.SetResolution(
						(int)Math.Round(_bmi.biXPelsPerMeter / InchesPerMeter),
						(int)Math.Round(_bmi.biYPelsPerMeter / InchesPerMeter));
				}
				else
				{
					bmp.SetResolution(xResolution, yResolution);
				}

				Graphics graphics = Graphics.FromImage(bmp);

				IntPtr hdc = graphics.GetHdc();

				SetDIBitsToDevice(hdc, 0, 0, _bmi.biWidth, _bmi.biHeight,
						0, 0, 0, _bmi.biHeight, _pixptr, _bmpptr, 0);

				graphics.ReleaseHdc(hdc);

				_image = NImage.FromBitmap(bmp);
			}
		}

		#region Windows API stuff

		Rectangle _bmprect;

		private IntPtr GetPixelInfo(IntPtr bmpptr)
		{
			_bmi = new BITMAPINFOHEADER();
			Marshal.PtrToStructure(bmpptr, _bmi);

			_bmprect.X = _bmprect.Y = 0;
			_bmprect.Width = _bmi.biWidth;
			_bmprect.Height = _bmi.biHeight;

			if (_bmi.biSizeImage == 0)
				_bmi.biSizeImage = ((((_bmi.biWidth * _bmi.biBitCount) + 31) & ~31) >> 3) * _bmi.biHeight;

			int p = _bmi.biClrUsed;

			if ((p == 0) && (_bmi.biBitCount <= 8))
				p = 1 << _bmi.biBitCount;

			p = (p * 4) + _bmi.biSize + (int)bmpptr;

			return (IntPtr)p;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 2)]
		internal class BITMAPINFOHEADER
		{
			public int biSize;
			public int biWidth;
			public int biHeight;
			public short biPlanes;
			public short biBitCount;
			public int biCompression;
			public int biSizeImage;
			public int biXPelsPerMeter;
			public int biYPelsPerMeter;
			public int biClrUsed;
			public int biClrImportant;
		}

		[DllImport("gdi32.dll", ExactSpelling = true)]
		internal static extern int SetDIBitsToDevice(IntPtr hdc, int xdst, int ydst,
												int width, int height, int xsrc, int ysrc, int start, int lines,
												IntPtr bitsptr, IntPtr bmiptr, int color);

		[DllImport("kernel32.dll", ExactSpelling = true)]
		internal static extern IntPtr GlobalLock(IntPtr handle);

		[DllImport("kernel32.dll", ExactSpelling = true)]
		internal static extern IntPtr GlobalFree(IntPtr handle);

		#endregion

		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NPhotoScannerForm));
			SuspendLayout();
			// 
			// NPhotoScannerForm
			// 
			ClientSize = new Size(284, 262);
			Icon = ((Icon)(resources.GetObject("$this.Icon")));
			Name = "NPhotoScannerForm";
			ResumeLayout(false);
		}
	}
}
