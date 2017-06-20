using System;
using System.Collections.Specialized;
using System.Windows.Forms;
using Neurotec.Devices;
using Neurotec.Media;

namespace Neurotec.Samples
{
	public partial class VoicesSettingsPage : Neurotec.Samples.SettingsPageBase
	{
		#region Public constructor

		public VoicesSettingsPage()
		{
			InitializeComponent();
		}

		#endregion

		#region Private methods

		private void ListDevices()
		{
			try
			{
				var current = Client.VoiceCaptureDevice;
				var selected = cbMicrophones.SelectedItem;
				cbMicrophones.BeginUpdate();
				cbMicrophones.Items.Clear();
				foreach (NDevice item in Client.DeviceManager.Devices)
				{
					if ((item.DeviceType & NDeviceType.Microphone) == NDeviceType.Microphone)
					{
						cbMicrophones.Items.Add(item);
					}
				}

				cbMicrophones.SelectedItem = current;
				if (cbMicrophones.SelectedIndex == -1 && cbMicrophones.Items.Count > 0)
				{
					cbMicrophones.SelectedItem = selected;
					if (cbMicrophones.SelectedIndex == -1 && cbMicrophones.Items.Count > 0)
					{
						cbMicrophones.SelectedIndex = 0;
					}
				}
			}
			catch (Exception ex)
			{
				Utilities.ShowError(ex);
			}
			finally
			{
				cbMicrophones.EndUpdate();
			}
		}

		private void ListAudioFormats()
		{
			cbFormats.BeginUpdate();
			try
			{
				cbFormats.Items.Clear();
				NCaptureDevice device = cbMicrophones.SelectedItem as NCaptureDevice;
				if (device != null)
				{
					foreach (var item in device.GetFormats())
					{
						cbFormats.Items.Add(item);
					}
					NMediaFormat current = device.GetCurrentFormat();
					if (current != null)
					{
						int index = cbFormats.Items.IndexOf(current);
						if (index != -1)
							cbFormats.SelectedIndex = index;
						else
						{
							cbFormats.Items.Insert(0, current);
							cbFormats.SelectedIndex = 0;
						}
					}
				}
			}
			finally
			{
				cbFormats.EndUpdate();
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

			cbMicrophones.SelectedItem = Client.VoiceCaptureDevice;
			chbUniquePhrases.Checked = Client.VoicesUniquePhrasesOnly;
			chbTextDependent.Checked = Client.VoicesExtractTextDependentFeatures;
			chbTextIndependant.Checked = Client.VoicesExtractTextIndependentFeatures;
			nudMaxFileSize.Value = (decimal)(Client.VoicesMaximalLoadedFileSize / 1048576.0f);

			base.LoadSettings();
		}

		public override void DefaultSettings()
		{
			if (cbMicrophones.Items.Count > 0)
				cbMicrophones.SelectedIndex = 0;
			Client.ResetProperty("Voices.UniquePhrasesOnly");
			Client.ResetProperty("Voices.ExtractTextDependentFeatures");
			Client.ResetProperty("Voices.ExtractTextIndependentFeatures");
			Client.ResetProperty("Voices.MaximalLoadedFileSize");

			base.DefaultSettings();
		}

		#endregion

		#region Private events

		private void DevicesCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
		{
			BeginInvoke(new MethodInvoker(ListDevices));
		}

		private void CbMicrophonesSelectedIndexChanged(object sender, EventArgs e)
		{
			Client.VoiceCaptureDevice = cbMicrophones.SelectedItem as NMicrophone;
			ListAudioFormats();
		}

		private void CbFormatsSelectedIndexChanged(object sender, EventArgs e)
		{
			NMediaFormat format = cbFormats.SelectedItem as NMediaFormat;
			if (format != null)
			{
				Client.VoiceCaptureDevice.SetCurrentFormat(format);
			}
		}

		private void ChbUniquePhrasesCheckedChanged(object sender, EventArgs e)
		{
			Client.VoicesUniquePhrasesOnly = chbUniquePhrases.Checked;
		}

		private void ChbTextDependentCheckedChanged(object sender, EventArgs e)
		{
			Client.VoicesExtractTextDependentFeatures = chbTextDependent.Checked;
		}

		private void ChbTextIndependantCheckedChanged(object sender, EventArgs e)
		{
			Client.VoicesExtractTextIndependentFeatures = chbTextIndependant.Checked;
		}

		private void NudMaxFileSizeValueChanged(object sender, EventArgs e)
		{
			Client.VoicesMaximalLoadedFileSize = (long)(nudMaxFileSize.Value * 1048576);
		}

		#endregion
	}
}
