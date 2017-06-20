using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Neurotec.Biometrics;
using Neurotec.Biometrics.Client;
using Neurotec.Images;

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
		private NIris _iris;

		#endregion

		#region Public properties

		public NBiometricClient BiometricClient
		{
			get { return _biometricClient; }
			set { _biometricClient = value; }
		}

		#endregion

		#region Private methods

		private void OnExtractionCompleted(IAsyncResult r)
		{
			if (InvokeRequired)
			{
				BeginInvoke(new AsyncCallback(OnExtractionCompleted), r);
			}
			else
			{
				NBiometricStatus status = _biometricClient.EndCreateTemplate(r);
				if (status == NBiometricStatus.Ok)
				{
					lblQuality.Text = string.Format("Quality: {0}", _iris.Objects.First().Quality);
					btnSaveTemplate.Enabled = true;
				}
				else
				{
					_subject = null;
					_iris = null;
					lblQuality.Text = string.Empty;
					MessageBox.Show(@"Iris image quality is too low.", Text, MessageBoxButtons.OK);
				}
			}
		}

		#endregion

		#region Private form events

		private void BtnOpenClick(object sender, EventArgs e)
		{
			openFileDialog.FileName = null;
			openFileDialog.Filter = NImages.GetOpenFileFilterString(true, false);

			if (openFileDialog.ShowDialog() == DialogResult.OK)
			{
				lblQuality.Text = string.Empty;
				btnSaveTemplate.Enabled = false;

				// Create subject with selected iris image
				_iris = new NIris { FileName = openFileDialog.FileName };
				irisView.Iris = _iris;
				_subject = new NSubject();
				_subject.Irises.Add(_iris);
				// Start extraction
				_biometricClient.BeginCreateTemplate(_subject, OnExtractionCompleted, null);
			}
		}

		private void BtnSaveTemplateClick(object sender, EventArgs e)
		{
			if (_subject != null)
			{
				saveFileDialog.FileName = string.Empty;
				saveFileDialog.Filter = string.Empty;
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
		}

		#endregion
	}
}
