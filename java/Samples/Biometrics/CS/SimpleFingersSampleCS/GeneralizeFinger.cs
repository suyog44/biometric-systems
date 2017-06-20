using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Neurotec.Biometrics;
using Neurotec.Biometrics.Client;
using Neurotec.Images;
using Neurotec.Biometrics.Gui;

namespace Neurotec.Samples
{
	public partial class GeneralizeFinger : UserControl
	{
		#region Public constructor

		public GeneralizeFinger()
		{
			InitializeComponent();
		}

		#endregion

		#region Private fields

		private NSubject _subject = null;

		#endregion

		#region Public properties

		public NBiometricClient BiometricClient { get; set; }

		#endregion

		#region Private methods

		private void OnCreateTemplateCompleted(IAsyncResult result)
		{
			if (InvokeRequired)
			{
				BeginInvoke(new AsyncCallback(OnCreateTemplateCompleted), result);
			}
			else
			{
				try
				{
					NBiometricStatus status = BiometricClient.EndCreateTemplate(result);
					if (status == NBiometricStatus.Ok)
					{
						lblStatus.Text = "Status: Ok";
						lblStatus.ForeColor = Color.Green;
						btnSaveTemplate.Enabled = true;
						fingerView.Finger = _subject.Fingers.Last();
					}
					else
					{
						lblStatus.Text = string.Format("Status: {0}", status);
						lblStatus.ForeColor = Color.Red;
					}
				}
				catch (Exception ex)
				{
					lblStatus.Text = "Status: error occured";
					lblStatus.ForeColor = Color.Red;
					Utils.ShowException(ex);
				}
				btnOpenImages.Enabled = true;
			}
		}

		private void BtnOpenImagesClick(object sender, EventArgs e)
		{
			_subject = null;
			fingerView.Finger = null;
			btnSaveTemplate.Enabled = false;
			lblImageCount.Text = "0";
			lblStatus.Visible = false;

			if (openFileDialog.ShowDialog() == DialogResult.OK)
			{
				var files = openFileDialog.FileNames;
				if (files.Length < 3 || files.Length > 10)
				{
					string msg = string.Format("{0} images selected. Please select at least 3 and no more than 10 images", files.Length > 10 ? "Too many" : "Too few");
					MessageBox.Show(msg, Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
				}
				else
				{
					_subject = new NSubject();
					foreach (var name in files)
					{
						NFinger finger = new NFinger()
						{
							FileName = name,
							SessionId = 1 // all fingers with same session will be generalized
						};
						_subject.Fingers.Add(finger);
					}
					lblImageCount.Text = openFileDialog.FileNames.Length.ToString();
					lblStatus.Text = "Status: performing extraction and generalizarion";
					lblStatus.Visible = true;
					lblStatus.ForeColor = Color.Black;
					BiometricClient.BeginCreateTemplate(_subject, OnCreateTemplateCompleted, null);
					btnOpenImages.Enabled = false;
				}
			}
		}

		private void GeneralizeFingerLoad(object sender, EventArgs e)
		{
			if (!DesignMode)
			{
				openFileDialog.Filter = NImages.GetOpenFileFilterString(true, true);
			}
		}

		private void BtnSaveTemplateClick(object sender, EventArgs e)
		{
			if (_subject != null)
			{
				if (saveFileDialog.ShowDialog() == DialogResult.OK)
				{
					try
					{
						using (var templateBuffer = _subject.GetTemplateBuffer())
						{
							File.WriteAllBytes(saveFileDialog.FileName, templateBuffer.ToArray());
						}
					}
					catch (Exception ex)
					{
						Utils.ShowException(ex);
					}
				}
			}
		}

		private void GeneralizeFingerVisibleChanged(object sender, EventArgs e)
		{
			if (Visible && BiometricClient != null)
			{
				BiometricClient.FingersReturnBinarizedImage = true;
			}
		}

		private void ChbShowBinarizedImageCheckedChanged(object sender, EventArgs e)
		{
			fingerView.ShownImage = chbShowBinarizedImage.Checked ? ShownImage.Result : ShownImage.Original;
		}

		#endregion
	}
}
