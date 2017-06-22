using System;
using Neurotec.Devices;
using Neurotec.Images;
using System.Drawing;
using Neurotec.Gui;
using System.Windows.Forms;

namespace Neurotec.Samples
{
	public partial class CameraForm : CaptureDeviceForm
	{
		#region Private fields

		private NCameraStatus _cameraStatus = NCameraStatus.None;
		private RectangleF? _focusRegion;
		private readonly Pen _focusPen = Pens.White;

		#endregion

		#region Public constructor

		public CameraForm()
		{
			InitializeComponent();
			OnDeviceChanged();
			OnCameraStatusChanged();
		}

		#endregion

		#region Private methods

		private void OnCameraStatusChanged()
		{
			cameraStatusLabel.Text = _cameraStatus == NCameraStatus.None ? null : _cameraStatus.ToString();
		}

		#endregion

		#region Protected methods

		protected override bool IsValidDeviceType(Type value)
		{
			return base.IsValidDeviceType(value) && typeof(NCamera).IsAssignableFrom(value);
		}

		protected override sealed void OnDeviceChanged()
		{
			base.OnDeviceChanged();
			var camera = (NCamera)Device;
			EnableForcedCapture = camera != null && camera.IsStillCaptureSupported;
			resetFocusButton.Visible = focusButton.Visible = camera != null && camera.IsFocusSupported;
			clickToFocusCheckBox.Visible = camera != null && camera.IsFocusRegionSupported;
		}

		protected override void OnStatusChanged()
		{
			base.OnStatusChanged();
			resetFocusButton.Enabled = focusButton.Enabled = IsCapturing;
			clickToFocusCheckBox.Enabled = IsCapturing;
		}

		protected override void OnStartingCapture()
		{
			base.OnStartingCapture();
			var camera = (NCamera)Device;
			camera.StillCaptured += Device_StillCaptured;
		}

		protected override void OnFinishingCapture()
		{
			var camera = (NCamera)Device;
			camera.StillCaptured -= Device_StillCaptured;
			base.OnFinishingCapture();
		}

		protected override bool OnObtainSample()
		{
			var camera = (NCamera)Device;
			using (NImage image = camera.GetFrame())
			{
				if (image != null)
				{
					var focusRegion = camera.FocusRegion;
					lock (_statusLock)
					{
						_focusRegion = focusRegion;
					}
					OnImage(image, null, null, false);
					return true;
				}
				return false;
			}
		}

		protected override void OnCaptureFinished()
		{
			lock (_statusLock)
			{
				_focusRegion = null;
			}
			base.OnCaptureFinished();
		}

		#endregion

		#region Public properties

		public bool ClickToFocus
		{
			get
			{
				return clickToFocusCheckBox.Checked && clickToFocusCheckBox.Visible;
			}
			set
			{
				clickToFocusCheckBox.Checked = value;
			}
		}

		#endregion

		#region Private form events

		void Device_StillCaptured(object sender, NCameraStillCapturedEventArgs e)
		{
			if (!HasFinal)
			{
				using (NImage image = NImage.FromStream(e.Stream))
				{
					OnImage(image, null, null, true);
				}
				BeginInvoke(new MethodInvoker(OnCancelCapture));
			}
		}

		#endregion

		private void pictureBox_Paint(object sender, PaintEventArgs e)
		{
			RectangleF? focusRegion;
			lock (_statusLock)
			{
				focusRegion = _focusRegion;
			}
			if (!focusRegion.HasValue) return;

			Rectangle area = GetPictureArea();
			e.Graphics.DrawRectangle(_focusPen, area.X + focusRegion.Value.X * area.Width, area.Y + focusRegion.Value.Y * area.Height,
				focusRegion.Value.Width * area.Width, focusRegion.Value.Height * area.Height);
		}

		private void pictureBox_MouseClick(object sender, MouseEventArgs e)
		{
			Rectangle area = GetPictureArea();
			bool clickToFocus = ClickToFocus;
			NGui.InvokeAsync(delegate
				{
					var camera = (NCamera)Device;
					if (camera != null && camera.IsFocusRegionSupported)
					{
						RectangleF? focusRegion;
						lock (_statusLock)
						{
							focusRegion = _focusRegion;
						}
						float w = focusRegion.HasValue ? focusRegion.Value.Width : 0.1f;
						float h = focusRegion.HasValue ? focusRegion.Value.Height : 0.1f;
						camera.FocusRegion = new RectangleF((e.X - area.X) / (float)area.Width - w / 2, (e.Y - area.Y) / (float)area.Height - h / 2, w, h);
						if (clickToFocus && camera.IsFocusSupported)
						{
							_cameraStatus = camera.Focus();
							BeginInvoke(new MethodInvoker(OnCameraStatusChanged));
						}
					}
				});
		}

		private void focusButton_Click(object sender, EventArgs e)
		{
			NGui.InvokeAsync(delegate
				{
					var camera = (NCamera)Device;
					_cameraStatus = camera.Focus();
					BeginInvoke(new MethodInvoker(OnCameraStatusChanged));
				}
			);
		}

		private void resetFocusButton_Click(object sender, EventArgs e)
		{
			NGui.InvokeAsync(delegate
				{
					var camera = (NCamera)Device;
					camera.ResetFocus();
					_cameraStatus = NCameraStatus.None;
					BeginInvoke(new MethodInvoker(OnCameraStatusChanged));
				});
		}

		private void forceButton_Click(object sender, EventArgs e)
		{
			NGui.InvokeAsync(delegate
			{
				var camera = (NCamera) Device;
				_cameraStatus = camera.CaptureStill();
				BeginInvoke(new MethodInvoker(OnCameraStatusChanged));
			});
		}
	}
}
