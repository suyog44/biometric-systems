using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Xml;
using System.Threading;
using Neurotec.Devices;
using Neurotec.Images;
using System.Diagnostics;
using Neurotec.Media;

namespace Neurotec.Samples
{
	public partial class CaptureForm : Form
	{
		#region Private fields

		private bool _autoCaptureStart;
		private NDevice _device;
		private bool _gatherImages;
		private bool _forceCapture;
		private bool _isCapturing;
		private int _fps;
		private Bitmap _bitmap, _finalBitmap;
		private string _userStatus, _finalUserStatus;
		Stopwatch _sw;
		Queue<TimeSpan> _timestamps;
		TimeSpan _lastReportTime = TimeSpan.Zero;
		private string _imagesPath;
		private int _imageCount = 0;

		#endregion

		#region Protected fields

		protected readonly object _statusLock = new object();

		#endregion

		#region Public constructor

		public CaptureForm()
		{
			InitializeComponent();
		}

		#endregion

		#region Protected methods

		protected virtual void OnDeviceChanged()
		{
			Text = _device == null ? "No device" : _device.DisplayName;
			OnStatusChanged();
		}

		protected virtual void OnStatusChanged()
		{
			lock (_statusLock)
			{
				Bitmap theBitmap;
				string theUserStatus;
				statusTextBox.Clear();
				if (_isCapturing)
				{
					statusTextBox.AppendText(string.Format("Capturing ({0} fps)", _fps));
					theBitmap = _bitmap;
					theUserStatus = _userStatus;
				}
				else
				{
					statusTextBox.AppendText(string.Format("Finished"));
					theBitmap = _finalBitmap;
					theUserStatus = _finalUserStatus;
				}
				if (pictureBox.Image != theBitmap)
				{
					if (pictureBox.Image != null)
					{
						pictureBox.Image.Dispose();
						pictureBox.Image = null;
					}
					pictureBox.Image = theBitmap;
				}
				if (theBitmap != null)
				{
					statusTextBox.AppendText(string.Format(" ({0}x{1}, {2}x{3} ppi)", theBitmap.Width, theBitmap.Height, theBitmap.HorizontalResolution, theBitmap.VerticalResolution));
				}
				if (theUserStatus != null)
				{
					statusTextBox.AppendText(": ");
					statusTextBox.AppendText(theUserStatus);
				}
				statusTextBox.AppendText(Environment.NewLine);
				forceButton.Enabled = _isCapturing;
				closeButton.Text = _isCapturing ? "Cancel" : "Close";
				closeButton.DialogResult = _isCapturing ? DialogResult.Cancel : DialogResult.OK;
				AcceptButton = _isCapturing ? null : closeButton;
			}
		}

		protected void CheckIsBusy()
		{
			if (backgroundWorker.IsBusy) throw new InvalidOperationException("Capturing is running");
		}

		protected virtual bool IsValidDeviceType(Type value)
		{
			return true;
		}

		protected virtual void OnCaptureStarted()
		{
			_isCapturing = true;
			if (InvokeRequired) BeginInvoke(new MethodInvoker(OnStatusChanged));
			else OnStatusChanged();
		}

		protected virtual void OnCaptureFinished()
		{
			_isCapturing = false;
			if (InvokeRequired) BeginInvoke(new MethodInvoker(OnStatusChanged));
			else OnStatusChanged();
		}

		protected bool OnImage(NImage image, string userStatus, string imageName, bool isFinal)
		{
			lock (_statusLock)
			{
				if (!isFinal)
				{
					TimeSpan elapsed = _sw.Elapsed;
					_timestamps.Enqueue(elapsed);
					if ((elapsed - _lastReportTime).TotalSeconds >= 0.3)
					{
						double s;
						for (; ; _timestamps.Dequeue())
						{
							s = (elapsed - _timestamps.Peek()).TotalSeconds;
							if (_timestamps.Count <= 1 || s <= 1) break;
						}
						_fps = s > double.Epsilon ? (int)Math.Round(_timestamps.Count / s) : 0;
						_lastReportTime = elapsed;
					}
				}
				if (_gatherImages && image != null)
				{
					image.Save(Path.Combine(_imagesPath, string.Format("{0}{1}.png", isFinal ? "Final" : (_imageCount++).ToString("D8"), imageName == null ? null : '_' + imageName)));
				}
				if (isFinal)
				{
					_finalBitmap = image == null ? null : image.ToBitmap();
					_finalUserStatus = userStatus;
				}
				else
				{
					_bitmap = image == null ? null : image.ToBitmap();
					_userStatus = userStatus;
				}
			}
			BeginInvoke(new MethodInvoker(OnStatusChanged));
			return _forceCapture;
		}

		protected void WriteParameter(XmlWriter writer, string key, object parameter)
		{
			writer.WriteStartElement("Parameter");
			writer.WriteAttributeString("Name", key);
			writer.WriteString(parameter.ToString());
			writer.WriteEndElement();
		}

		protected virtual void OnWriteScanParameters(XmlWriter writer)
		{
		}

		protected virtual void OnCancelCapture()
		{
			backgroundWorker.CancelAsync();
		}

		protected virtual void OnCapture()
		{
			throw new NotImplementedException();
		}

		private delegate void AddMediaFormatsHandler(IEnumerable<NMediaFormat> mediaFormats, NMediaFormat currentFormat);
		private bool _suppressMediaFormatEvents;

		protected void AddMediaFormats(IEnumerable<NMediaFormat> mediaFormats, NMediaFormat currentFormat)
		{
			if (InvokeRequired)
			{
				BeginInvoke(new AddMediaFormatsHandler(AddMediaFormats), mediaFormats, currentFormat);
			}
			else
			{
				if (mediaFormats == null) throw new ArgumentNullException("mediaFormats");
				_suppressMediaFormatEvents = true;
				formatsComboBox.BeginUpdate();
				foreach (NMediaFormat mediaFormat in mediaFormats)
				{
					formatsComboBox.Items.Add(mediaFormat);
				}
				if (currentFormat != null)
				{
					int currentIndex = formatsComboBox.Items.IndexOf(currentFormat);
					formatsComboBox.SelectedIndex = currentIndex;
				}
				formatsComboBox.EndUpdate();
				_suppressMediaFormatEvents = false;
			}
		}

		protected virtual void OnMediaFormatChanged(NMediaFormat mediaFormat)
		{
		}

		protected Rectangle GetPictureArea()
		{
			Bitmap bmp = _finalBitmap ?? _bitmap;
			int frameWidth = bmp == null ? 0 : bmp.Width;
			int frameHeight = bmp == null ? 0 : bmp.Height;
			Size cs = pictureBox.ClientSize;
			float zoom = 1;
			if (frameWidth != 0 && frameHeight != 0)
				zoom = Math.Min(cs.Width / (float)frameWidth, cs.Height / (float)frameHeight);
			float sx = frameWidth * zoom;
			float sy = frameHeight * zoom;
			return new Rectangle((int)Math.Round((cs.Width - sx) / 2), (int)Math.Round((cs.Height - sy) / 2), (int)Math.Round(sx), (int)Math.Round(sy));
		}

		#endregion

		#region Protected properties

		protected bool IsCapturing
		{
			get
			{
				return _isCapturing;
			}
		}

		protected bool HasFinal
		{
			get
			{
				return _finalBitmap != null;
			}
		}

		protected bool AutoCaptureStart
		{
			get
			{
				return _autoCaptureStart;
			}
			set
			{
				_autoCaptureStart = value;
			}
		}

		protected bool EnableForcedCapture
		{
			get
			{
				return forceButton.Visible;
			}
			set
			{
				forceButton.Visible = value;
			}
		}

		protected bool IsCancellationPending
		{
			get
			{
				return backgroundWorker.CancellationPending;
			}
		}

		#endregion

		#region Public methods

		public void WaitForCaptureToFinish()
		{
			while (backgroundWorker.IsBusy)
			{
				Thread.Sleep(0);
				Application.DoEvents();
			}
		}

		#endregion

		#region Public properties

		public NDevice Device
		{
			get
			{
				return _device;
			}
			set
			{
				if (_device != value)
				{
					if (_device != null && !IsValidDeviceType(_device.GetType())) throw new ArgumentException("Invalid NDevice type");
					CheckIsBusy();
					_device = value;
					OnDeviceChanged();
				}
			}
		}

		public bool GatherImages
		{
			get
			{
				return _gatherImages;
			}
			set
			{
				if (_gatherImages != value)
				{
					CheckIsBusy();
					_gatherImages = value;
				}
			}
		}

		#endregion

		private void CaptureForm_Shown(object sender, EventArgs e)
		{
			if (_device == null) return;

			if (_gatherImages)
			{
				_imagesPath = Path.Combine(string.Format("{0}_{1}", Device.Make, Device.Model), Guid.NewGuid().ToString());
				Directory.CreateDirectory(_imagesPath);
				_imageCount = 0;

				XmlWriter writer = XmlWriter.Create(Path.Combine(_imagesPath, "ScanInfo.xml"));
				writer.WriteStartElement("Scan");
				OnWriteScanParameters(writer);
				writer.Close();
			}
			if (!_autoCaptureStart) OnCaptureStarted();
			backgroundWorker.RunWorkerAsync();
		}

		private void CaptureForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (!backgroundWorker.IsBusy) return;

			OnCancelCapture();
			WaitForCaptureToFinish();
		}

		private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
		{
			_timestamps = new Queue<TimeSpan>();
			_sw = Stopwatch.StartNew();
			OnCapture();
		}

		private void backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			if (!_autoCaptureStart) OnCaptureFinished();
			if (e.Error == null) return;

			statusTextBox.AppendText("Error: ");
			statusTextBox.AppendText(e.Error.ToString());
		}

		private void forceButton_Click(object sender, EventArgs e)
		{
			_forceCapture = true;
		}

		private void closeButton_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void formatsComboBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (_suppressMediaFormatEvents) return;
			NMediaFormat mediaFormat = formatsComboBox.SelectedIndex >= 0 ? (NMediaFormat)formatsComboBox.Items[formatsComboBox.SelectedIndex] : null;
			OnMediaFormatChanged(mediaFormat);
		}

		private void customizeFormatButton_Click(object sender, EventArgs e)
		{
			NMediaFormat selectedFormat = formatsComboBox.SelectedItem as NMediaFormat;
			if (selectedFormat == null)
			{
				NDevice device = Device;
				if ((device.DeviceType & NDeviceType.Camera) == NDeviceType.Camera)
				{
					selectedFormat = new NVideoFormat();
				}
				else if ((device.DeviceType & NDeviceType.Microphone) == NDeviceType.Microphone)
				{
					selectedFormat = new NAudioFormat();
				}
				else
				{
					throw new NotImplementedException();
				}
			}
			NMediaFormat customFormat = CustomizeFormatForm.CustomizeFormat(selectedFormat);
			if (customFormat != null)
			{
				int index = formatsComboBox.Items.IndexOf(customFormat);
				if (index == -1)
				{
					formatsComboBox.Items.Add(customFormat);
				}
				formatsComboBox.SelectedItem = customFormat;
			}
		}
	}
}
