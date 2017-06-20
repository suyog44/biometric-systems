using System;
using System.Windows.Forms;
using Neurotec.Biometrics;
using Neurotec.Biometrics.Client;
using Neurotec.Biometrics.Gui;

namespace Neurotec.Samples
{
	public partial class VerifyFace : UserControl
	{
		#region Public constructor

		public VerifyFace()
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
				matchingFarComboBox.Text = Utils.MatchingThresholdToString(_defaultFar);
			}
		}

		#endregion

		#region Private methods

		private string OpenImageTemplate(NFaceView faceView, out NSubject subject)
		{
			subject = null;
			faceView.Face = null;
			msgLabel.Text = string.Empty;
			string fileLocation = string.Empty;

			openFileDialog.FileName = null;
			openFileDialog.Title = @"Open Template";
			if (openFileDialog.ShowDialog() == DialogResult.OK) // load template
			{
				fileLocation = openFileDialog.FileName;

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
					// Create a face object
					NFace face = new NFace() { FileName = fileLocation };
					faceView.Face = face;
					subject = new NSubject();
					subject.Faces.Add(face);

					// Extract a template from the subject
					_biometricClient.BeginCreateTemplate(subject, OnCreationCompleted, null);
				}
			}
			return fileLocation;
		}

		private void OnCreationCompleted(IAsyncResult r)
		{
			if (InvokeRequired)
			{
				BeginInvoke(new AsyncCallback(OnCreationCompleted), r);
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
					EnableVerifyButton();
				}
				catch (Exception ex)
				{
					Utils.ShowException(ex);
				}
			}
		}

		private void EnableVerifyButton()
		{
			verifyButton.Enabled = IsSubjectValid(_subject1) && IsSubjectValid(_subject2);
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
						//get matching score
						int score = _subject1.MatchingResults[0].Score;
						string msg = string.Format("Score of matched templates: {0}", score);
						msgLabel.Text = msg;
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

		#endregion

		#region Private form events

		private void OpenImageButton1Click(object sender, EventArgs e)
		{
			templateLeftLabel.Text = string.Empty;
			templateLeftLabel.Text = OpenImageTemplate(faceView1, out _subject1);
		}

		private void OpenImageButton2Click(object sender, EventArgs e)
		{
			templateRightLabel.Text = string.Empty;
			templateRightLabel.Text = OpenImageTemplate(faceView2, out _subject2);
		}

		private void VerifyFaceLoad(object sender, EventArgs e)
		{
			msgLabel.Text = string.Empty;
			templateLeftLabel.Text = string.Empty;
			templateRightLabel.Text = string.Empty;
			try
			{
				matchingFarComboBox.BeginUpdate();
				matchingFarComboBox.Items.Add(0.001.ToString("P1"));
				matchingFarComboBox.Items.Add(0.0001.ToString("P2"));
				matchingFarComboBox.Items.Add(0.00001.ToString("P3"));
			}
			finally
			{
				matchingFarComboBox.EndUpdate();
			}
		}

		private void MatchingFarComboBoxEnter(object sender, EventArgs e)
		{
			defaultButton.Enabled = true;
		}

		private void DefaultButtonClick(object sender, EventArgs e)
		{
			matchingFarComboBox.Text = Utils.MatchingThresholdToString(_defaultFar);
			defaultButton.Enabled = false;
		}

		private void ClearImagesButtonClick(object sender, EventArgs e)
		{
			_subject1 = null;
			_subject2 = null;
			verifyButton.Enabled = false;

			faceView1.Face = null;
			faceView2.Face = null;

			msgLabel.Text = string.Empty;
			templateLeftLabel.Text = string.Empty;
			templateRightLabel.Text = string.Empty;
		}

		private void VerifyButtonClick(object sender, EventArgs e)
		{
			if (_subject1 != null && _subject2 != null)
			{
				_biometricClient.BeginVerify(_subject1, _subject2, OnVerifyCompleted, null);
			}
			verifyButton.Enabled = false;
		}

		private void MatchingFarComboBoxLeave(object sender, EventArgs e)
		{
			try
			{
				_biometricClient.MatchingThreshold = Utils.MatchingThresholdFromString(matchingFarComboBox.Text);
				matchingFarComboBox.Text = Utils.MatchingThresholdToString(_biometricClient.MatchingThreshold);
				EnableVerifyButton();
			}
			catch
			{
				matchingFarComboBox.Select();
				MessageBox.Show(@"FAR is not valid", Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		#endregion
	}
}
