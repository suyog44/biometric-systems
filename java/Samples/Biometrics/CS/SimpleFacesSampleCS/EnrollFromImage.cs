using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using Neurotec.Biometrics;
using Neurotec.Biometrics.Client;
using Neurotec.Images;
using Neurotec.Licensing;

namespace Neurotec.Samples
{
	public partial class EnrollFromImage : UserControl
	{
		#region Public constructor

		public EnrollFromImage()
		{
			InitializeComponent();
		}

		#endregion

		#region Private fields

		private NBiometricClient _biometricClient;
		private NSubject _subject;
		private bool? _isSegmentationActivated;

		private float _defaultMaxRoll;
		private float _defaultMaxYaw;

		#endregion

		#region Public properties

		public NBiometricClient BiometricClient
		{
			get { return _biometricClient; }
			set
			{
				_biometricClient = value;
				_defaultMaxRoll = _biometricClient.FacesMaximalRoll;
				_defaultMaxYaw = _biometricClient.FacesMaximalYaw;
				cbRollAngle.SelectedItem = _defaultMaxRoll;
				cbYawAngle.SelectedItem = _defaultMaxYaw;
			}
		}

		#endregion

		#region Private methods

		private void SetBiometricClientParams()
		{
			if (!_isSegmentationActivated.HasValue)
				_isSegmentationActivated = NLicense.IsComponentActivated("Biometrics.FaceSegmentsDetection");

			_biometricClient.FacesDetectAllFeaturePoints = _isSegmentationActivated.Value;
			_biometricClient.FacesDetectBaseFeaturePoints = _isSegmentationActivated.Value;
			_biometricClient.FacesDetermineGender = _isSegmentationActivated.Value;
			_biometricClient.FacesDetermineAge = _isSegmentationActivated.Value;
			_biometricClient.FacesDetectProperties = _isSegmentationActivated.Value;
			_biometricClient.FacesRecognizeEmotion = _isSegmentationActivated.Value;
			_biometricClient.FacesRecognizeExpression = _isSegmentationActivated.Value;
		}

		private void ExtractTemplate()
		{
			if (_subject == null) return;
			SetBiometricClientParams();

			// Extract template
			_biometricClient.BeginCreateTemplate(_subject, OnExtractDone, null);
		}

		private void OnExtractDone(IAsyncResult r)
		{
			if (InvokeRequired)
			{
				BeginInvoke(new AsyncCallback(OnExtractDone), r);
			}
			else
			{
				try
				{
					NBiometricStatus status = _biometricClient.EndCreateTemplate(r);
					if (status == NBiometricStatus.Ok)
					{
						lblStatus.Text = @"Template extracted";
						btnSaveTemplate.Enabled = true;
					}
					else
					{
						lblStatus.Text = string.Format("Extraction failed: {0}", status);
						btnSaveTemplate.Enabled = false;
					}
				}
				catch (Exception ex)
				{
					Utils.ShowException(ex);
					lblStatus.Text = "Extraction failed!";
					btnSaveTemplate.Enabled = false;
				}
			}
		}

		#endregion

		#region Private form events

		private void EnrollFromImageLoad(object sender, EventArgs e)
		{
			try
			{
				float item = _biometricClient.FacesMaximalRoll;
				var items = new List<float>();
				for (float i = 15; i <= 180; i += 15)
				{
					items.Add((i));
				}
				if (!items.Contains(item))
					items.Add(item);
				items.Sort();

				int index = items.IndexOf(item);
				for (int i = 0; i != items.Count; i++)
				{
					cbRollAngle.Items.Add(items[i]);
				}
				cbRollAngle.SelectedIndex = index;

				item = _biometricClient.FacesMaximalYaw;
				items.Clear();
				for (float i = 15; i <= 90; i += 15)
				{
					items.Add((i));
				}
				if (!items.Contains(item))
					items.Add(item);
				items.Sort();

				index = items.IndexOf(item);
				for (int i = 0; i != items.Count; i++)
				{
					cbYawAngle.Items.Add(items[i]);
				}
				cbYawAngle.SelectedIndex = index;

				openFileDialog.Filter = NImages.GetOpenFileFilterString(true, true);
			}
			catch (Exception ex)
			{
				Utils.ShowException(ex);
			}
		}

		private void BtnSaveTemplateClick(object sender, EventArgs e)
		{
			if (saveFileDialog.ShowDialog() == DialogResult.OK)
			{
				try
				{
					File.WriteAllBytes(saveFileDialog.FileName, _subject.GetTemplateBuffer().ToArray());
				}
				catch (Exception ex)
				{
					Utils.ShowException(ex);
				}
			}
		}

		private void TsbExtractClick(object sender, EventArgs e)
		{
			try
			{
				ExtractTemplate();
			}
			catch (Exception ex)
			{
				Utils.ShowException(ex);
				btnSaveTemplate.Enabled = false;
			}
		}

		private void TsbOpenImageClick(object sender, EventArgs e)
		{
			try
			{
				openFileDialog.FileName = null;
				if (openFileDialog.ShowDialog() == DialogResult.OK)
				{
					// Create new subject
					_subject = new NSubject();
					var face = new NFace { FileName = openFileDialog.FileName };
					_subject.Faces.Add(face);
					facesView.Face = face;
					ExtractTemplate();
				}
			}
			catch (Exception ex)
			{
				Utils.ShowException(ex);
				btnSaveTemplate.Enabled = false;
			}
		}

		private void CbYawAngleSelectedIndexChanged(object sender, EventArgs e)
		{
			_biometricClient.FacesMaximalYaw = (float)cbYawAngle.SelectedItem;
		}

		private void CbRollAngleSelectedIndexChanged(object sender, EventArgs e)
		{
			_biometricClient.FacesMaximalRoll = (float)cbRollAngle.SelectedItem;
		}

		#endregion
	}
}
