using System;
using System.Globalization;
using System.Windows.Forms;
using Neurotec.Biometrics;
using Neurotec.Biometrics.Client;
using Neurotec.Images;
using System.Linq;

namespace Neurotec.Samples
{
	public partial class SegmentIris : UserControl
	{
		#region Public constructor

		public SegmentIris()
		{
			InitializeComponent();
		}

		#endregion

		#region Private fields

		private NBiometricClient _biometricClient;
		private NIris _iris;
		private NIris _segmentedIris;

		#endregion

		#region Public properties

		public NBiometricClient BiometricClient
		{
			get { return _biometricClient; }
			set { _biometricClient = value; }
		}

		#endregion

		#region Private methods

		private void OnSegmentCompleted(IAsyncResult r)
		{
			if (InvokeRequired)
			{
				BeginInvoke(new AsyncCallback(OnSegmentCompleted), r);
			}
			else
			{
				NBiometricTask task = _biometricClient.EndPerformTask(r);
				NBiometricStatus status = task.Status;
				if (status == NBiometricStatus.Ok)
				{
					NEAttributes attributes = _iris.Objects.First();
					// Segmented iris
					_segmentedIris = attributes.Child as NIris;
					irisView2.Iris = _segmentedIris;

					tbQuality.Text = attributes.Quality.ToString(CultureInfo.InvariantCulture);
					tbGrayLevelSpread.Text = attributes.GrayScaleUtilisation.ToString(CultureInfo.InvariantCulture);
					tbPupilToIrisRatio.Text = attributes.PupilToIrisRatio.ToString(CultureInfo.InvariantCulture);
					tbUsableIrisArea.Text = attributes.UsableIrisArea.ToString(CultureInfo.InvariantCulture);
					tbIrisScleraContrast.Text = attributes.IrisScleraContrast.ToString(CultureInfo.InvariantCulture);
					tbIrisPupilContrast.Text = attributes.IrisPupilContrast.ToString(CultureInfo.InvariantCulture);
					tbIrisPupilConcentricity.Text = attributes.IrisPupilConcentricity.ToString(CultureInfo.InvariantCulture);
					tbPupilBoundaryCircularity.Text = attributes.PupilBoundaryCircularity.ToString(CultureInfo.InvariantCulture);
					tbMarginAdequacy.Text = attributes.MarginAdequacy.ToString(CultureInfo.InvariantCulture);
					tbSharpness.Text = attributes.Sharpness.ToString(CultureInfo.InvariantCulture);
					tbInterlace.Text = attributes.Interlace.ToString(CultureInfo.InvariantCulture);

					btnSegmentIris.Enabled = false;
					btnSaveImage.Enabled = true;
				}
				else
				{
					MessageBox.Show(string.Format("Segmentation failed: {0}.", status), Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
				btnOpenImage.Enabled = true;
			}
		}

		private void ResetControls()
		{
			irisView1.Iris = null;
			irisView2.Iris = null;

			tbQuality.Text = string.Empty;
			tbGrayLevelSpread.Text = string.Empty;
			tbPupilToIrisRatio.Text = string.Empty;
			tbUsableIrisArea.Text = string.Empty;
			tbIrisScleraContrast.Text = string.Empty;
			tbIrisPupilContrast.Text = string.Empty;
			tbIrisPupilConcentricity.Text = string.Empty;
			tbPupilBoundaryCircularity.Text = string.Empty;
			tbMarginAdequacy.Text = string.Empty;
			tbSharpness.Text = string.Empty;
			tbInterlace.Text = string.Empty;

			btnSaveImage.Enabled = false;
			btnSegmentIris.Enabled = false;
		}

		#endregion

		#region Private form events

		private void BtnOpenImageClick(object sender, EventArgs e)
		{
			ResetControls();

			if (openFileDialog.ShowDialog() == DialogResult.OK)
			{
				try
				{
					_iris = new NIris { Image = NImage.FromFile(openFileDialog.FileName) };
					irisView1.Iris = _iris;
					btnSegmentIris.Enabled = true;
				}
				catch (Exception ex)
				{
					Utils.ShowException(ex);
				}
			}
		}

		private void BtnSegmentIrisClick(object sender, EventArgs e)
		{
			if (_iris != null)
			{
				_iris.ImageType = NEImageType.CroppedAndMasked;
				var subject = new NSubject();
				subject.Irises.Add(_iris);

				NBiometricTask segmentTask = _biometricClient.CreateTask(NBiometricOperations.Segment, subject);
				_biometricClient.BeginPerformTask(segmentTask, OnSegmentCompleted, null);
				btnSegmentIris.Enabled = false;
				btnOpenImage.Enabled = false;
			}
		}

		private void BtnSaveImageClick(object sender, EventArgs e)
		{
			if (_segmentedIris != null)
			{
				saveFileDialog.Filter = NImages.GetSaveFileFilterString();
				if (saveFileDialog.ShowDialog() == DialogResult.OK)
				{
					try
					{
						_segmentedIris.Image.Save(saveFileDialog.FileName);
					}
					catch (Exception ex)
					{
						Utils.ShowException(ex);
					}
				}
			}
		}

		private void SegmentIrisLoad(object sender, EventArgs e)
		{
			if (!DesignMode)
			{
				btnSaveImage.Enabled = false;
				btnSegmentIris.Enabled = false;
				saveFileDialog.Filter = NImages.GetSaveFileFilterString();
				openFileDialog.Filter = NImages.GetOpenFileFilterString(true, false);
			}
		}

		#endregion
	}
}
