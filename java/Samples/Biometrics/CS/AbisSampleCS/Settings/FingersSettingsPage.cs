using System;
using System.Collections.Specialized;
using System.Windows.Forms;
using Neurotec.Biometrics;
using Neurotec.Devices;
using System.Linq;

namespace Neurotec.Samples
{
	public partial class FingersSettingsPage : Neurotec.Samples.SettingsPageBase
	{
		#region Private fields

		bool _areComponentsChecked;

		#endregion

		#region Public constructor

		public FingersSettingsPage()
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
				var current = Client.FingerScanner;
				var selected = cbScanners.SelectedItem;
				cbScanners.BeginUpdate();
				cbScanners.Items.Clear();
				foreach (NDevice item in Client.DeviceManager.Devices)
				{
					if ((item.DeviceType & NDeviceType.FScanner) == NDeviceType.FScanner)
					{
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

			if (!_areComponentsChecked)
			{
				_areComponentsChecked = true;
				chbCalculateNfiq.Enabled = LicensingTools.CanAssessFingerQuality(Client.LocalOperations);
				if (!chbCalculateNfiq.Enabled) chbCalculateNfiq.Text += " (Not activated)";
				chbDeterminePatternClass.Enabled = LicensingTools.CanDetectFingerSegments(Client.LocalOperations);
				if (!chbDeterminePatternClass.Enabled) chbDeterminePatternClass.Text += " (Not activated)";
				var remoteConnection = Client.RemoteConnections.FirstOrDefault();
				NBiometricOperations remoteOperations = remoteConnection != null ? remoteConnection.Operations : NBiometricOperations.None;
				chbCheckForDuplicates.Enabled = LicensingTools.CanFingerBeMatched(remoteOperations);
				if (!chbCheckForDuplicates.Enabled) chbCheckForDuplicates.Text += " (Not activated)";
			}
		}

		public override void LoadSettings()
		{
			ListDevices();

			cbScanners.SelectedItem = Client.FingerScanner;
			cbTemplateSize.SelectedItem = Client.FingersTemplateSize;
			cbMatchingSpeed.SelectedItem = Client.FingersMatchingSpeed;
			nudMaximalRotation.Value = Convert.ToDecimal(Client.FingersMaximalRotation);
			nudQuality.Value = Client.FingersQualityThreshold;
			chbFastExtraction.Checked = Client.FingersFastExtraction;
			chbReturnBinarized.Checked = Client.FingersReturnBinarizedImage;
			chbDeterminePatternClass.Checked = Client.FingersDeterminePatternClass;
			chbCalculateNfiq.Checked = Client.FingersCalculateNfiq;
			chbCheckForDuplicates.Checked = Client.FingersCheckForDuplicatesWhenCapturing;

			nudGeneralizationRecordCount.Value = SettingsManager.FingersGeneralizationRecordCount;

			base.LoadSettings();

			btnDisconnect.Enabled = Client.FingerScanner != null && Client.FingerScanner.IsDisconnectable;
		}

		public override void DefaultSettings()
		{
			if (cbScanners.Items.Count > 0)
				cbScanners.SelectedIndex = 0;

			Client.ResetProperty("Fingers.TemplateSize");
			Client.ResetProperty("Fingers.MatchingSpeed");
			Client.ResetProperty("Fingers.MaximalRotation");
			Client.ResetProperty("Fingers.QualityThreshold");
			Client.ResetProperty("Fingers.FastExtraction");
			Client.FingersReturnBinarizedImage = true;
			Client.FingersCalculateNfiq = chbCalculateNfiq.Enabled;
			Client.FingersDeterminePatternClass = chbDeterminePatternClass.Enabled;
			Client.FingersCheckForDuplicatesWhenCapturing = chbCheckForDuplicates.Enabled;
			SettingsManager.FingersGeneralizationRecordCount = 3;

			base.DefaultSettings();
		}

		public override void SaveSettings()
		{
			SettingsManager.FingersGeneralizationRecordCount = Convert.ToInt32(nudGeneralizationRecordCount.Value);

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
			Client.FingerScanner = cbScanners.SelectedItem as NFScanner;
			btnDisconnect.Enabled = Client.FingerScanner != null && Client.FingerScanner.IsDisconnectable;
		}

		private void CbTemplateSizeSelectedIndexChanged(object sender, EventArgs e)
		{
			Client.FingersTemplateSize = (NTemplateSize)cbTemplateSize.SelectedItem;
		}

		private void CbMatchingSpeedSelectedIndexChanged(object sender, EventArgs e)
		{
			Client.FingersMatchingSpeed = (NMatchingSpeed)cbMatchingSpeed.SelectedItem;
		}

		private void NudMaximalRotationValueChanged(object sender, EventArgs e)
		{
			Client.FingersMaximalRotation = Convert.ToSingle(nudMaximalRotation.Value);
		}

		private void NudQualityValueChanged(object sender, EventArgs e)
		{
			Client.FingersQualityThreshold = Convert.ToByte(nudQuality.Value);
		}

		private void ChbFastExtractionCheckedChanged(object sender, EventArgs e)
		{
			Client.FingersFastExtraction = chbFastExtraction.Checked;
		}

		private void ChbReturnBinarizedCheckedChanged(object sender, EventArgs e)
		{
			Client.FingersReturnBinarizedImage = chbReturnBinarized.Checked;
		}

		private void ChbDeterminePatternClassCheckedChanged(object sender, EventArgs e)
		{
			Client.FingersDeterminePatternClass = chbDeterminePatternClass.Checked;
		}

		private void ChbCalculateNfiqCheckedChanged(object sender, EventArgs e)
		{
			Client.FingersCalculateNfiq = chbCalculateNfiq.Checked;
		}

		private void ChbCheckForDuplicatesCheckedChanged(object sender, EventArgs e)
		{
			Client.FingersCheckForDuplicatesWhenCapturing = chbCheckForDuplicates.Checked;
		}

		private void BtnConnectClick(object sender, EventArgs e)
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

		private void BtnDisconnectClick(object sender, EventArgs e)
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
