using System;
using System.Windows.Forms;
using Neurotec.Biometrics;
using Neurotec.Biometrics.Client;
using Neurotec.Biometrics.Gui;

namespace Neurotec.Samples
{
	public partial class VerifyIris : UserControl
	{
		#region Public constructor

		public VerifyIris()
		{
			InitializeComponent();
		}

		#endregion

		#region Private fields

		private NBiometricClient _biometricClient;
		private NSubject _subject1;
		private NSubject _subject2;

		private int _defaultFar;

		#endregion

		#region Public properties

		public NBiometricClient BiometricClient
		{
			get { return _biometricClient; }
			set
			{
				_biometricClient = value;
				_defaultFar = _biometricClient.MatchingThreshold;
				cbMatchingFAR.Text = Utils.MatchingThresholdToString(_defaultFar);
			}
		}

		#endregion

		#region Private methods

		private string OpenImageTemplate(NIrisView irisView, out NSubject subject)
		{
			subject = null;
			irisView.Iris = null;
			lblMsg.Text = string.Empty;
			string fileName = string.Empty;

			openFileDialog.FileName = null;
			openFileDialog.Title = @"Open Template";
			if (openFileDialog.ShowDialog() == DialogResult.OK) // load template
			{
				fileName = openFileDialog.FileName;

				// Check if given file is a template
				try
				{
					subject = NSubject.FromFile(openFileDialog.FileName);
					EnableVerifyButton();
				}
				catch { }

				// If file is not a template, try to load it as image
				if (subject == null)
				{
					// Create an iris object
					NIris iris = new NIris() { FileName = fileName };
					irisView.Iris = iris;
					subject = new NSubject();
					subject.Irises.Add(iris);

					// Extract a template from the subject
					_biometricClient.BeginCreateTemplate(subject, OnExtractCompleted, subject);
				}
			}
			return fileName;
		}

		private void OnExtractCompleted(IAsyncResult r)
		{
			if (InvokeRequired)
			{
				BeginInvoke(new AsyncCallback(OnExtractCompleted), r);
			}
			else
			{
				try
				{
					NBiometricStatus status = _biometricClient.EndCreateTemplate(r);
					if (status != NBiometricStatus.Ok)
					{
						MessageBox.Show(string.Format("The template was not extracted: {0}.", status), Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
					}
				}
				catch (Exception ex)
				{
					Utils.ShowException(ex);
				}
				finally
				{
					EnableVerifyButton();
				}
			}
		}

		private void EnableVerifyButton()
		{
			btnVerify.Enabled = IsSubjectValid(_subject1) && IsSubjectValid(_subject2);
		}

		private bool IsSubjectValid(NSubject subject)
		{
			return subject != null && (subject.Status == NBiometricStatus.Ok
				|| subject.Status == NBiometricStatus.MatchNotFound
				|| subject.Status == NBiometricStatus.None && subject.GetTemplateBuffer() != null);
		}

		private void OnVerifyCompleted(IAsyncResult r)
		{
			if (InvokeRequired)
			{
				BeginInvoke(new AsyncCallback(OnVerifyCompleted), r);
			}
			else
			{
				try
				{
					NBiometricStatus status = _biometricClient.EndVerify(r);
					var verificationStatus = string.Format("Verification status: {0}", status);
					if (status == NBiometricStatus.Ok)
					{
						// Get matching score
						int score = _subject1.MatchingResults[0].Score;
						string msg = string.Format("Score of matched templates: {0}", score);
						lblMsg.Text = msg;
						MessageBox.Show(string.Format("{0}\n{1}", verificationStatus, msg));
					}
					else MessageBox.Show(verificationStatus);
				}
				catch (Exception ex)
				{
					Utils.ShowException(ex);
				}
			}
		}

		private void SetFar()
		{
			try
			{
				_biometricClient.MatchingThreshold = Utils.MatchingThresholdFromString(cbMatchingFAR.Text);
				cbMatchingFAR.Text = Utils.MatchingThresholdToString(_biometricClient.MatchingThreshold);
				EnableVerifyButton();
			}
			catch
			{
				MessageBox.Show(@"FAR is not valid", Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
				cbMatchingFAR.Select();
			}
		}

		private void SetDefaultFar()
		{
			cbMatchingFAR.Text = Utils.MatchingThresholdToString(_defaultFar);
			btnDefault.Enabled = false;
			SetFar();
		}

		#endregion

		#region Private form events

		private void BtnOpenImage1Click(object sender, EventArgs e)
		{
			lblTemplateLeft.Text = string.Empty;
			lblTemplateLeft.Text = OpenImageTemplate(irisView1, out _subject1);
		}

		private void BtnDefaultClick(object sender, EventArgs e)
		{
			SetDefaultFar();
		}

		private void BtnOpenImage2Click(object sender, EventArgs e)
		{
			lblTemplateRight.Text = string.Empty;
			lblTemplateRight.Text = OpenImageTemplate(irisView2, out _subject2);
		}

		private void BtnClearClick(object sender, EventArgs e)
		{
			_subject1 = null;
			_subject2 = null;
			btnVerify.Enabled = false;

			irisView1.Iris = null;
			irisView2.Iris = null;

			lblMsg.Text = string.Empty;
			lblTemplateLeft.Text = string.Empty;
			lblTemplateRight.Text = string.Empty;
		}

		private void BtnVerifyClick(object sender, EventArgs e)
		{
			if (_subject1 != null && _subject2 != null)
			{
				_biometricClient.BeginVerify(_subject1, _subject2, OnVerifyCompleted, null);
			}
			btnVerify.Enabled = false;
		}

		private void VerifyIrisLoad(object sender, EventArgs e)
		{
			lblMsg.Text = string.Empty;
			lblTemplateLeft.Text = string.Empty;
			lblTemplateRight.Text = string.Empty;
			try
			{
				cbMatchingFAR.BeginUpdate();
				cbMatchingFAR.Items.Add(0.001.ToString("P1"));
				cbMatchingFAR.Items.Add(0.0001.ToString("P2"));
				cbMatchingFAR.Items.Add(0.00001.ToString("P3"));
			}
			finally
			{
				cbMatchingFAR.EndUpdate();
			}
		}

		private void CbMatchingFAREnter(object sender, EventArgs e)
		{
			btnDefault.Enabled = true;
		}

		private void CbMatchingFARLeave(object sender, EventArgs e)
		{
			SetFar();
		}

		private void VerifyIrisVisibleChanged(object sender, EventArgs e)
		{
			if (Visible && _biometricClient != null)
			{
				SetDefaultFar();
			}
		}

		#endregion
	}
}
