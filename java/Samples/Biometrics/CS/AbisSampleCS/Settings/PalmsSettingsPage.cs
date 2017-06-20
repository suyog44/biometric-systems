using System;
using System.Collections.Specialized;
using System.Windows.Forms;
using Neurotec.Biometrics;
using Neurotec.Devices;

namespace Neurotec.Samples
{
	public partial class PalmsSettingsPage : Neurotec.Samples.SettingsPageBase
	{
		#region Public constructor

		public PalmsSettingsPage()
		{
			InitializeComponent();

			cbMatchingSpeed.Items.Add(NMatchingSpeed.High);
			cbMatchingSpeed.Items.Add(NMatchingSpeed.Medium);
			cbMatchingSpeed.Items.Add(NMatchingSpeed.Low);

			cbTemplateSize.Items.Add(NTemplateSize.Large);
			cbTemplateSize.Items.Add(NTemplateSize.Medium);
			cbTemplateSize.Items.Add(NTemplateSize.Small);
		}

		#endregion

		#region Private methods

		private void ListDevices()
		{
			try
			{
				var current = Client.PalmScanner;
				var selected = cbScanners.SelectedItem;
				cbScanners.BeginUpdate();
				cbScanners.Items.Clear();
				foreach (NDevice item in Client.DeviceManager.Devices)
				{
					if ((item.DeviceType & NDeviceType.FScanner) == NDeviceType.FScanner)
					{
						NFScanner scanner = (NFScanner)item;
						if (Array.Exists(scanner.GetSupportedImpressionTypes(), x => NBiometricTypes.IsImpressionTypePalm(x)))
							cbScanners.Items.Add(item);
					}
				}

				cbScanners.SelectedItem = current;
				if (cbScanners.SelectedIndex == -1 && cbScanners.Items.Count > 0)
				{
					cbScanners.SelectedItem = selected;
					if (cbScanners.SelectedIndex == -1 && cbScanners.Items.Count > 0)
					{
						cbScanners.SelectedIndex = 0;
					}
				}
			}
			catch (Exception ex)
			{
				Utilities.ShowError(ex);
			}
			finally
			{
				cbScanners.EndUpdate();
			}
		}

		#endregion

		#region Public methods

		public override void OnNavigatingFrom()
		{
			Client.DeviceManager.Devices.CollectionChanged -= DevicesCollectionChanged;

			base.OnNavigatingFrom();
		}

		public override void OnNavigatedTo(params object[] args)
		{
			base.OnNavigatedTo(args);

			Client.DeviceManager.Devices.CollectionChanged += DevicesCollectionChanged;
		}

		public override void LoadSettings()
		{
			ListDevices();

			cbScanners.SelectedItem = Client.PalmScanner;
			cbTemplateSize.SelectedItem = Client.PalmsTemplateSize;
			cbMatchingSpeed.SelectedItem = Client.PalmsMatchingSpeed;
			nudMaximalRotation.Value = Convert.ToDecimal(Client.PalmsMaximalRotation);
			nudQuality.Value = Client.PalmsQualityThreshold;
			chbReturnBinarized.Checked = Client.PalmsReturnBinarizedImage;
			nudRecordCount.Value = SettingsManager.PalmsGeneralizationRecordCount;

			base.LoadSettings();

			btnDisconnect.Enabled = Client.FingerScanner != null && Client.FingerScanner.IsDisconnectable;
		}

		public override void DefaultSettings()
		{
			if (cbScanners.Items.Count > 0)
				cbScanners.SelectedIndex = 0;
			Client.ResetProperty("Palms.TemplateSize");
			Client.ResetProperty("Palms.MatchingSpeed");
			Client.ResetProperty("Palms.MaximalRotation");
			Client.ResetProperty("Palms.QualityThreshold");
			Client.PalmsReturnBinarizedImage = true;
			SettingsManager.PalmsGeneralizationRecordCount = 3;

			base.DefaultSettings();
		}

		public override void SaveSettings()
		{
			SettingsManager.PalmsGeneralizationRecordCount = Convert.ToInt32(nudRecordCount.Value);
			base.SaveSettings();
		}

		#endregion

		#region Private events

		private void DevicesCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
		{
			BeginInvoke(new MethodInvoker(ListDevices));
		}

		private void CbScannersSelectedIndexChanged(object sender, EventArgs e)
		{
			Client.PalmScanner = cbScanners.SelectedItem as NFScanner;
			btnDisconnect.Enabled = Client.FingerScanner != null && Client.FingerScanner.IsDisconnectable;
		}

		private void CbTemplateSizeSelectedIndexChanged(object sender, EventArgs e)
		{
			Client.PalmsTemplateSize = (NTemplateSize)cbTemplateSize.SelectedItem;
		}

		private void CbMatchingSpeedSelectedIndexChanged(object sender, EventArgs e)
		{
			Client.PalmsMatchingSpeed = (NMatchingSpeed)cbMatchingSpeed.SelectedItem;
		}

		private void NudMaximalRotationValueChanged(object sender, EventArgs e)
		{
			Client.PalmsMaximalRotation = Convert.ToSingle(nudMaximalRotation.Value);
		}

		private void NudQualityValueChanged(object sender, EventArgs e)
		{
			Client.PalmsQualityThreshold = Convert.ToByte(nudQuality.Value);
		}

		private void ChbReturnBinarizedCheckedChanged(object sender, EventArgs e)
		{
			Client.PalmsReturnBinarizedImage = chbReturnBinarized.Checked;
		}

		private void btnConnect_Click(object sender, EventArgs e)
		{
			NDevice newDevice = null;
			try
			{
				using (var form = new ConnectToDeviceForm())
				{
					if (form.ShowDialog() == DialogResult.OK)
					{
						newDevice = Client.DeviceManager.ConnectToDevice(form.SelectedPlugin, form.Parameters);
						ListDevices();
						cbScanners.SelectedItem = newDevice;

						if (cbScanners.SelectedItem != newDevice)
						{
							if (newDevice != null)
								Client.DeviceManager.DisconnectFromDevice(newDevice);

							Utilities.ShowError("Failed to create connection to device using specified connection details");
						}
					}
				}
			}
			catch (Exception ex)
			{
				if (newDevice != null)
					Client.DeviceManager.DisconnectFromDevice(newDevice);

				Utilities.ShowError(ex);
			}
		}

		private void btnDisconnect_Click(object sender, EventArgs e)
		{
			NDevice device = (NDevice)cbScanners.SelectedItem;
			if (device != null)
			{
				try
				{
					Client.DeviceManager.DisconnectFromDevice(device);
				}
				catch (Exception ex)
				{
					Utilities.ShowError(ex);
				}
			}
		}

		#endregion
	}
}
