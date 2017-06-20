using System;
using System.Collections.Specialized;
using System.Windows.Forms;
using Neurotec.Biometrics;
using Neurotec.Devices;
using Neurotec.Media;

namespace Neurotec.Samples
{
	public partial class FacesSettingsPage : Neurotec.Samples.SettingsPageBase
	{
		#region Private fields

		bool _areComponentsChecked;

		#endregion

		#region Public constructor

		public FacesSettingsPage()
		{
			InitializeComponent();

			cbMatchingSpeed.Items.Add(NMatchingSpeed.High);
			cbMatchingSpeed.Items.Add(NMatchingSpeed.Medium);
			cbMatchingSpeed.Items.Add(NMatchingSpeed.Low);

			cbTemplateSize.Items.Add(NTemplateSize.Large);
			cbTemplateSize.Items.Add(NTemplateSize.Medium);
			cbTemplateSize.Items.Add(NTemplateSize.Small);

			foreach (var mode in Enum.GetValues(typeof(NLivenessMode)))
			{
				cbLivenessMode.Items.Add(mode);
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
				bool isActivated = LicensingTools.CanDetectFaceSegments(Client.LocalOperations);
				if (!isActivated)
				{
					chbDetectAllFeaturePoints.Enabled = false;
					chbDetectBaseDeaturePoints.Enabled = false;
					chbDetermineGender.Enabled = false;
					chbRecognizeEmotion.Enabled = false;
					chbDetectProperties.Enabled = false;
					chbRecognizeExpression.Enabled = false;
					chbDetermineAge.Enabled = false;

					chbDetectAllFeaturePoints.Text += " (Not activated)";
					chbDetectBaseDeaturePoints.Text += " (Not activated)";
					chbDetermineGender.Text += " (Not activated)";
					chbRecognizeEmotion.Text += " (Not activated)";
					chbDetectProperties.Text += " (Not activated)";
					chbRecognizeExpression.Text += " (Not activated)";
					chbDetermineAge.Text += " (Not activated)";
				}
			}
		}

		public override void LoadSettings()
		{
			ListDevices();

			cbCamera.SelectedItem = Client.FaceCaptureDevice;
			cbTemplateSize.SelectedItem = Client.FacesTemplateSize;
			cbMatchingSpeed.SelectedItem = Client.FacesMatchingSpeed;
			nudMinIOD.Value = Client.FacesMinimalInterOcularDistance;
			nudConfidenceThreshold.Value = Client.FacesConfidenceThreshold;
			nudMaxRoll.Value = (decimal)Client.FacesMaximalRoll;
			nudMaximalYaw.Value = (decimal)Client.FacesMaximalYaw;
			nudQuality.Value = Client.FacesQualityThreshold;
			cbLivenessMode.SelectedItem = Client.FacesLivenessMode;
			nudLivenessThreshold.Value = Client.FacesLivenessThreshold;
			chbDetectAllFeaturePoints.Checked = Client.FacesDetectAllFeaturePoints && chbDetectAllFeaturePoints.Enabled;
			chbDetectBaseDeaturePoints.Checked = Client.FacesDetectBaseFeaturePoints && chbDetectBaseDeaturePoints.Enabled;
			chbDetermineGender.Checked = Client.FacesDetermineGender && chbDetermineGender.Enabled;
			chbDetectProperties.Checked = Client.FacesDetectProperties && chbDetectProperties.Enabled;
			chbRecognizeExpression.Checked = Client.FacesRecognizeExpression && chbRecognizeExpression.Enabled;
			chbRecognizeEmotion.Checked = Client.FacesRecognizeEmotion && chbRecognizeEmotion.Enabled;
			chbCreateThumbnail.Checked = Client.FacesCreateThumbnailImage;
			nudThumbnailWidth.Value = Client.FacesThumbnailImageWidth;
			nudGeneralizationRecordCount.Value = SettingsManager.FacesGeneralizationRecordCount;
			chbDetermineAge.Checked = Client.FacesDetermineAge;

			base.LoadSettings();

			btnDisconnect.Enabled = Client.FingerScanner != null && Client.FingerScanner.IsDisconnectable;
		}

		public override void DefaultSettings()
		{
			if (cbCamera.Items.Count > 0)
				cbCamera.SelectedIndex = 0;

			Client.ResetProperty("Faces.TemplateSize");
			Client.ResetProperty("Faces.MatchingSpeed");
			Client.ResetProperty("Faces.MinimalInterOcularDistance");
			Client.ResetProperty("Faces.ConfidenceThreshold");
			Client.ResetProperty("Faces.MaximalRoll");
			Client.ResetProperty("Faces.MaximalYaw");
			Client.ResetProperty("Faces.QualityThreshold");
			Client.ResetProperty("Faces.LivenessMode");
			Client.ResetProperty("Faces.LivenessThreshold");
			Client.FacesDetectAllFeaturePoints = chbDetectAllFeaturePoints.Enabled;
			Client.ResetProperty("Faces.DetectBaseFeaturePoints");
			Client.FacesDetermineGender = chbDetermineGender.Enabled;
			Client.FacesDetermineAge = chbDetermineAge.Enabled;
			Client.FacesDetectProperties = chbDetectProperties.Enabled;
			Client.FacesRecognizeExpression = chbRecognizeExpression.Enabled;
			Client.FacesRecognizeEmotion = chbRecognizeEmotion.Enabled;
			Client.FacesCreateThumbnailImage = true;
			Client.FacesThumbnailImageWidth = 90;
			SettingsManager.FacesGeneralizationRecordCount = 3;

			base.DefaultSettings();
		}

		public override void SaveSettings()
		{
			SettingsManager.FacesGeneralizationRecordCount = Convert.ToInt32(nudGeneralizationRecordCount.Value);
			base.SaveSettings();
		}

		#endregion

		#region Private methods

		private void ListDevices()
		{
			try
			{
				var current = Client.FaceCaptureDevice;
				var selected = cbCamera.SelectedItem;
				cbCamera.BeginUpdate();
				cbCamera.Items.Clear();
				foreach (NDevice item in Client.DeviceManager.Devices)
				{
					if ((item.DeviceType & NDeviceType.Camera) == NDeviceType.Camera)
					{
						cbCamera.Items.Add(item);
					}
				}

				cbCamera.SelectedItem = current;
				if (cbCamera.SelectedIndex == -1 && cbCamera.Items.Count > 0)
				{
					cbCamera.SelectedItem = selected;
					if (cbCamera.SelectedIndex == -1 && cbCamera.Items.Count > 0)
					{
						cbCamera.SelectedIndex = 0;
					}
				}
			}
			catch (Exception ex)
			{
				Utilities.ShowError(ex);
			}
			finally
			{
				cbCamera.EndUpdate();
			}
		}

		private void ListVideoFormats()
		{
			cbFormat.BeginUpdate();
			try
			{
				cbFormat.Items.Clear();
				NCaptureDevice device = cbCamera.SelectedItem as NCaptureDevice;
				if (device != null)
				{
					foreach (var item in device.GetFormats())
					{
						cbFormat.Items.Add(item);
					}
					NMediaFormat current = device.GetCurrentFormat();
					if (current != null)
					{
						int index = cbFormat.Items.IndexOf(current);
						if (index != -1)
							cbFormat.SelectedIndex = index;
						else
						{
							cbFormat.Items.Insert(0, current);
							cbFormat.SelectedIndex = 0;
						}
					}
				}
			}
			finally
			{
				cbFormat.EndUpdate();
			}
		}

		#endregion

		#region Private events

		private void CbLivenessModeSelectedIndexChanged(object sender, EventArgs e)
		{
			Client.FacesLivenessMode = (NLivenessMode)cbLivenessMode.SelectedItem;
			nudLivenessThreshold.Enabled = Client.FacesLivenessMode != NLivenessMode.None;
		}

		private void CbCameraSelectedIndexChanged(object sender, EventArgs e)
		{
			Client.FaceCaptureDevice = cbCamera.SelectedItem as NCamera;
			ListVideoFormats();
			btnDisconnect.Enabled = Client.FaceCaptureDevice != null && Client.FaceCaptureDevice.IsDisconnectable;
		}

		private void CbFormatSelectedIndexChanged(object sender, EventArgs e)
		{
			NMediaFormat format = cbFormat.SelectedItem as NMediaFormat;
			if (format != null)
			{
				Client.FaceCaptureDevice.SetCurrentFormat(format);
			}
		}

		private void DevicesCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
		{
			BeginInvoke(new MethodInvoker(ListDevices));
		}

		private void CbTemplateSizeSelectedIndexChanged(object sender, EventArgs e)
		{
			Client.FacesTemplateSize = (NTemplateSize)cbTemplateSize.SelectedItem;
		}

		private void CbMatchingSpeedSelectedIndexChanged(object sender, EventArgs e)
		{
			Client.FacesMatchingSpeed = (NMatchingSpeed)cbMatchingSpeed.SelectedItem;
		}

		private void NudMinIODValueChanged(object sender, EventArgs e)
		{
			Client.FacesMinimalInterOcularDistance = Convert.ToInt32(nudMinIOD.Value);
		}

		private void NudConfidenceThresholdValueChanged(object sender, EventArgs e)
		{
			Client.FacesConfidenceThreshold = Convert.ToByte(nudConfidenceThreshold.Value);
		}

		private void NudMaxRollValueChanged(object sender, EventArgs e)
		{
			Client.FacesMaximalRoll = Convert.ToSingle(nudMaxRoll.Value);
		}

		private void NudMaximalYawValueChanged(object sender, EventArgs e)
		{
			Client.FacesMaximalYaw = Convert.ToSingle(nudMaximalYaw.Value);
		}

		private void NudQualityValueChanged(object sender, EventArgs e)
		{
			Client.FacesQualityThreshold = Convert.ToByte(nudQuality.Value);
		}

		private void NudLivenessThresholdValueChanged(object sender, EventArgs e)
		{
			Client.FacesLivenessThreshold = Convert.ToByte(nudLivenessThreshold.Value);
		}

		private void ChbDetectAllFeaturePointsCheckedChanged(object sender, EventArgs e)
		{
			Client.FacesDetectAllFeaturePoints = chbDetectAllFeaturePoints.Checked;
		}

		private void ChbDetectBaseDeaturePointsCheckedChanged(object sender, EventArgs e)
		{
			Client.FacesDetectBaseFeaturePoints = chbDetectBaseDeaturePoints.Checked;
		}

		private void ChbDetermineGenderCheckedChanged(object sender, EventArgs e)
		{
			Client.FacesDetermineGender = chbDetermineGender.Checked;
		}

		private void ChbDetectPropertiesCheckedChanged(object sender, EventArgs e)
		{
			Client.FacesDetectProperties = chbDetectProperties.Checked;
		}

		private void ChbRecognizeExpressionCheckedChanged(object sender, EventArgs e)
		{
			Client.FacesRecognizeExpression = chbRecognizeExpression.Checked;
		}

		private void ChbRecognizeEmotionCheckedChanged(object sender, EventArgs e)
		{
			Client.FacesRecognizeEmotion = chbRecognizeEmotion.Checked;
		}

		private void ChbCreateThumbnailCheckedChanged(object sender, EventArgs e)
		{
			Client.FacesCreateThumbnailImage = chbCreateThumbnail.Checked;
			nudThumbnailWidth.Enabled = chbCreateThumbnail.Checked;
		}

		private void NudThumbnailWidthValueChanged(object sender, EventArgs e)
		{
			Client.FacesThumbnailImageWidth = Convert.ToInt32(nudThumbnailWidth.Value);
		}

		private void ChbDetermineAgeCheckedChanged(object sender, EventArgs e)
		{
			Client.FacesDetermineAge = chbDetermineAge.Checked;
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
						cbCamera.SelectedItem = newDevice;

						if (cbCamera.SelectedItem != newDevice)
						{
							if(newDevice != null)
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
			NDevice device = (NDevice)cbCamera.SelectedItem;
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
