using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Neurotec.Biometrics;
using Neurotec.Biometrics.Client;
using Neurotec.Images;

namespace Neurotec.Samples
{
	public partial class GeneralizeFaces : UserControl
	{
		#region Public constructor

		public GeneralizeFaces()
		{
			InitializeComponent();
		}

		#endregion

		#region Private fields

		private NSubject _subject = null;

		#endregion

		#region Public constructor

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
						faceView.Face = _subject.Faces.Last();
					}
					else
					{
						lblStatus.Text = string.Format("Status: {0}", status);
						lblStatus.ForeColor = Color.Red;
					}
				}
				catch (Exception ex)
				{
					lblStatus.Text = "Status: error occurred";
					lblStatus.ForeColor = Color.Red;
					Utils.ShowException(ex);
				}
				btnOpenImages.Enabled = true;
			}
		}

		private void BtnOpenImagesClick(object sender, EventArgs e)
		{
			_subject = null;
			faceView.Face = null;
			btnSaveTemplate.Enabled = false;
			lblImageCount.Text = "0";
			lblStatus.Visible = false;

			if (openFileDialog.ShowDialog() == DialogResult.OK)
			{
				_subject = new NSubject();
				foreach (var name in openFileDialog.FileNames)
				{
					NFace face = new NFace()
					{
						FileName = name,
						SessionId = 1 // all faces with same session will be generalized
					};
					_subject.Faces.Add(face);
				}
				lblImageCount.Text = openFileDialog.FileNames.Length.ToString();
				lblStatus.Text = "Status: performing extraction and generalization";
				lblStatus.Visible = true;
				lblStatus.ForeColor = Color.Black;
				BiometricClient.BeginCreateTemplate(_subject, OnCreateTemplateCompleted, null);
				btnOpenImages.Enabled = false;
			}
		}

		private void BtnSaveTemplateClick(object sender, EventArgs e)
		{
			if (_subject != null)
			{
				if (saveTemplateDialog.ShowDialog() == DialogResult.OK)
				{
					try
					{
						using (var templateBuffer = _subject.GetTemplateBuffer())
						{
							File.WriteAllBytes(saveTemplateDialog.FileName, templateBuffer.ToArray());
						}
					}
					catch (Exception ex)
					{
						Utils.ShowException(ex);
					}
				}
			}
		}

		private void GeneralizeFacesLoad(object sender, EventArgs e)
		{
			if (!DesignMode)
			{
				openFileDialog.Filter = NImages.GetOpenFileFilterString(true, true);
			}
		}

		#endregion
	}
}
