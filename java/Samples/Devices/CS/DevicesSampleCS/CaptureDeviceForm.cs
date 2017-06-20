using System;
using Neurotec.Media;
using Neurotec.Devices;
using Neurotec.Gui;

namespace Neurotec.Samples
{
	public partial class CaptureDeviceForm : CaptureForm
	{
		#region Private fields

		private NMediaFormat _nextMediaFormat;
		private readonly object _nextMediaFormatLock = new object();

		#endregion

		#region Public constructor

		public CaptureDeviceForm()
		{
			InitializeComponent();
			AutoCaptureStart = true;
		}

		#endregion

		#region Protected methods

		protected override bool IsValidDeviceType(Type value)
		{
			return base.IsValidDeviceType(value) && typeof(NCaptureDevice).IsAssignableFrom(value);
		}

		protected override void OnCapture()
		{
			var captureDevice = (NCaptureDevice)Device;
			captureDevice.IsCapturingChanged += Device_IsCapturingChanged;
			OnStartingCapture();
			bool stoppedCapturing = false;
			try
			{
				captureDevice.StartCapturing();
				bool sampleObtained = true;
				try
				{
					AddMediaFormats(captureDevice.GetFormats(), captureDevice.GetCurrentFormat());
					while (sampleObtained && !IsCancellationPending)
					{
						lock (_nextMediaFormatLock)
						{
							if (_nextMediaFormat != null)
							{
								captureDevice.SetCurrentFormat(_nextMediaFormat);
								_nextMediaFormat = null;
							}
						}
						sampleObtained = OnObtainSample();
					}
				}
				finally
				{
					if (sampleObtained && captureDevice.IsAvailable)
					{
						captureDevice.StopCapturing();
						stoppedCapturing = true;
					}
				}
			}
			finally
			{
				captureDevice.IsCapturingChanged -= Device_IsCapturingChanged;
				OnFinishingCapture();
				if (!stoppedCapturing) OnCaptureFinished();
			}
		}

		protected override void OnCancelCapture()
		{
			base.OnCancelCapture();
			if (Device.IsAvailable)
			{
				NGui.InvokeAsync(((NCaptureDevice)Device).StopCapturing);
			}
		}

		protected override void OnMediaFormatChanged(NMediaFormat mediaFormat)
		{
			lock (_nextMediaFormatLock)
			{
				_nextMediaFormat = mediaFormat;
			}
		}

		protected virtual bool OnObtainSample()
		{
			throw new NotImplementedException();
		}

		protected virtual void OnFinishingCapture()
		{
		}

		protected virtual void OnStartingCapture()
		{
		}

		#endregion

		#region Private form events

		void Device_IsCapturingChanged(object sender, EventArgs e)
		{
			if (Device.IsAvailable && ((NCaptureDevice)Device).IsCapturing)
			{
				OnCaptureStarted();
			}
			else
			{
				OnCaptureFinished();
			}
		}

		#endregion
	}
}
