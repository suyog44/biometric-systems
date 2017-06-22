using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Neurotec.Biometrics.Client;
using Neurotec.Images;
using Neurotec.Licensing;

namespace Neurotec.Samples
{
	public partial class DetectFaces : UserControl
	{
		#region Public constructor

		public DetectFaces()
		{
			InitializeComponent();
		}

		#endregion

		#region Private fields

		private NImage _image;
		private NBiometricClient _biometricClient;
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
			_biometricClient.FacesMaximalRoll = (float)cbRollAngle.SelectedItem;
			_biometricClient.FacesMaximalYaw = (float)cbYawAngle.SelectedItem;

			if (!_isSegmentationActivated.HasValue)
				_isSegmentationActivated = NLicense.IsComponentActivated("Biometrics.FaceSegmentsDetection");

			_biometricClient.FacesDetectAllFeaturePoints = _isSegmentationActivated.Value;
			_biometricClient.FacesDetectBaseFeaturePoints = _isSegmentationActivated.Value;
		}

		private void DetectFace(NImage image)
		{
			if (image == null) return;

			SetBiometricClientParams();
			// Detect asynchroniously all faces that are suitable for face recognition in the image
			_biometricClient.BeginDetectFaces(image, OnDetectDone, null);
		}

		private void OnDetectDone(IAsyncResult r)
		{
			if (InvokeRequired)
			{
				BeginInvoke(new AsyncCallback(OnDetectDone), r);
			}
			else
			{
				try
				{
					facesView.Face = _biometricClient.EndDetectFaces(r);
				}
				catch (Exception ex)
				{
					Utils.ShowException(ex);
				}
			}
		}

		#endregion

		#region Private form events

		private void DetectFacesLoad(object sender, EventArgs e)
		{
			try
			{
				float item = _biometricClient.FacesMaximalRoll;
				List<float> items = new List<float>();
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
			}
			catch (Exception ex)
			{
				Utils.ShowException(ex);
			}
		}

		private void TsbOpenImageClick(object sender, EventArgs e)
		{
			openFaceImageDlg.Filter = NImages.GetOpenFileFilterString(true, true);
			if (openFaceImageDlg.ShowDialog() == DialogResult.OK)
			{
				if (_image != null) _image.Dispose();
				_image = null;

				try
				{
					// Read image
					_image = NImage.FromFile(openFaceImageDlg.FileName);
					DetectFace(_image);
				}
				catch (Exception ex)
				{
					Utils.ShowException(ex);
				}
			}
		}

		private void TsbDetectFacialFeaturesClick(object sender, EventArgs e)
		{
			try
			{
				DetectFace(_image);
			}
			catch (Exception ex)
			{
				Utils.ShowException(ex);
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
