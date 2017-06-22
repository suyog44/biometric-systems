using System;
using System.Collections.Specialized;
using System.Windows.Forms;
using Neurotec.Biometrics;
using Neurotec.Devices;

namespace Neurotec.Samples
{
	public partial class IrisesSettingsPage : Neurotec.Samples.SettingsPageBase
	{
		#region Public constructor

		public IrisesSettingsPage()
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
				var current = Client.IrisScanner;
				var selected = cbScanners.SelectedItem;
				cbScanners.BeginUpdate();
				cbScanners.Items.Clear();
				foreach (NDevice item in Client.DeviceManager.Devices)
				{
					if ((item.DeviceType & NDeviceType.IrisScanner) == NDeviceType.IrisScanner)
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
		}

		public override void LoadSettings()
		{
			ListDevices();

			cbScanners.SelectedItem = Client.IrisScanner;
			cbTemplateSize.SelectedItem = Client.IrisesTemplateSize;
			cbMatchingSpeed.SelectedItem = Client.IrisesMatchingSpeed;
			nudMaximalRotation.Value = Convert.ToDecimal(Client.IrisesMaximalRotation);
			nudQuality.Value = Client.IrisesQualityThreshold;
			chbFastExtraction.Checked = Client.IrisesFastExtraction;

			base.LoadSettings();
		}

		public override void DefaultSettings()
		{
			if (cbScanners.Items.Count > 0)
				cbScanners.SelectedIndex = 0;
			Client.ResetProperty("Irises.TemplateSize");
			Client.ResetProperty("Irises.MatchingSpeed");
			Client.ResetProperty("Irises.MaximalRotation");
			Client.ResetProperty("Irises.QualityThreshold");
			Client.ResetProperty("Irises.FastExtraction");

			base.DefaultSettings();
		}

		#endregion

		#region Private events

		private void DevicesCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
		{
			BeginInvoke(new MethodInvoker(ListDevices));
		}

		private void CbScannersSelectedIndexChanged(object sender, EventArgs e)
		{
			Client.IrisScanner = cbScanners.SelectedItem as NIrisScanner;
		}

		private void CbTemplateSizeSelectedIndexChanged(object sender, EventArgs e)
		{
			Client.IrisesTemplateSize = (NTemplateSize)cbTemplateSize.SelectedItem;
		}

		private void CbMatchingSpeedSelectedIndexChanged(object sender, EventArgs e)
		{
			Client.IrisesMatchingSpeed = (NMatchingSpeed)cbMatchingSpeed.SelectedItem;
		}

		private void NudMaximalRotationValueChanged(object sender, EventArgs e)
		{
			Client.IrisesMaximalRotation = Convert.ToSingle(nudMaximalRotation.Value);
		}

		private void NudQualityValueChanged(object sender, EventArgs e)
		{
			Client.IrisesQualityThreshold = Convert.ToByte(nudQuality.Value);
		}

		private void ChbFastExtractionCheckedChanged(object sender, EventArgs e)
		{
			Client.IrisesFastExtraction = chbFastExtraction.Checked;
		}

		#endregion
	}
}
