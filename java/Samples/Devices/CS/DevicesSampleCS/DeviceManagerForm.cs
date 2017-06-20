using System;
using System.Windows.Forms;
using Neurotec.Devices;

namespace Neurotec.Samples
{
	public partial class DeviceManagerForm : Form
	{
		#region Public constructor

		public DeviceManagerForm()
		{
			InitializeComponent();

			anyCheckBox.Tag = NDeviceType.Any;
			captureDeviceCheckBox.Tag = NDeviceType.CaptureDevice;
			microphoneCheckBox.Tag = NDeviceType.Microphone;
			cameraCheckBox.Tag = NDeviceType.Camera;
			biometricDeviceCheckBox.Tag = NDeviceType.BiometricDevice;
			fScannerCheckBox.Tag = NDeviceType.FScanner;
			fingerScannerCheckBox.Tag = NDeviceType.FingerScanner;
			palmScannerCheckBox.Tag = NDeviceType.PalmScanner;
			irisScannerCheckBox.Tag = NDeviceType.IrisScanner;
			DeviceTypes = NDeviceType.Any;
		}

		#endregion

		#region Public properties

		public NDeviceType DeviceTypes
		{
			get
			{
				var value = NDeviceType.None;
				foreach (CheckBox checkBox in deviceTypesGroupBox.Controls)
				{
					if (checkBox.Checked) value |= (NDeviceType)checkBox.Tag;
				}
				return value;
			}
			set
			{
				foreach (CheckBox checkBox in deviceTypesGroupBox.Controls)
				{
					checkBox.Checked = (value & (NDeviceType)checkBox.Tag) != 0;
				}
			}
		}

		public bool AutoPlug
		{
			get
			{
				return cbAutoplug.Checked;
			}
			set
			{
				cbAutoplug.Checked = value;
			}
		}

		#endregion

		#region Private form events

		private void deviceTypeCheckBox_Click(object sender, EventArgs e)
		{
			var checkBox = (CheckBox)sender;
			var deviceType = (NDeviceType)checkBox.Tag;
			DeviceTypes = checkBox.CheckState == CheckState.Unchecked ? DeviceTypes | deviceType : DeviceTypes & ~deviceType;
		}

		private void btnAccept_Click(object sender, EventArgs e)
		{
			if (DeviceTypes == NDeviceType.None)
			{
				DeviceTypes = NDeviceType.Any;
			}
		}

		#endregion
	}
}
