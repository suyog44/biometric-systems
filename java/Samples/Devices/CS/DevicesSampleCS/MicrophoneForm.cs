using System;
using Neurotec.Devices;
using Neurotec.Sound;
using Neurotec.Sound.Processing;
using System.Windows.Forms;

namespace Neurotec.Samples
{
	public sealed partial class MicrophoneForm : CaptureDeviceForm
	{
		#region Private fields

		private double _soundLevel;

		#endregion

		#region Public constructor

		public MicrophoneForm()
		{
			InitializeComponent();
			OnDeviceChanged();
			EnableForcedCapture = false;
		}

		#endregion

		#region Private methods

		private void OnSoundSample(NSoundBuffer soundBuffer)
		{
			lock (_statusLock)
			{
				_soundLevel = NSoundProc.GetSoundLevel(soundBuffer);
			}
			BeginInvoke(new MethodInvoker(OnStatusChanged));
		}

		#endregion

		#region Protected methods

		protected override void OnStatusChanged()
		{
			lock (_statusLock)
			{
				var level = (int)(_soundLevel * 100.0);
				soundLevelProgressBar.Value = level;
				soundLevelProgressBar.Visible = IsCapturing;
			}
			base.OnStatusChanged();
		}

		protected override bool IsValidDeviceType(Type value)
		{
			return base.IsValidDeviceType(value) && typeof(NMicrophone).IsAssignableFrom(value);
		}

		protected override bool OnObtainSample()
		{
			var microphone = (NMicrophone)Device;
			using (NSoundBuffer soundSample = microphone.GetSoundSample())
			{
				if (soundSample != null)
				{
					OnSoundSample(soundSample);
					return true;
				}
				return false;
			}
		}

		#endregion
	}
}
