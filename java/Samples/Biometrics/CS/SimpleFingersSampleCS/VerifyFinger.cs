using System;
using System.Windows.Forms;
using Neurotec.Biometrics;
using Neurotec.Biometrics.Client;
using Neurotec.Biometrics.Gui;

namespace Neurotec.Samples
{
	public partial class VerifyFinger : UserControl
	{
		#region Public constructor

		public VerifyFinger()
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

		private string OpenImageTemplate(NFingerView fingerView, out NSubject subject)
		{
			subject = null;
			fingerView.Finger = null;
			msgLabel.Text = string.Empty;
			ResetMatedMinutiaeOnViews();
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

				// If file is a template - return, otherwise assume that the file is an image and try to extract it
				if (subject != null && subject.Fingers.Count > 0)
				{
					fingerView.Finger = subject.Fingers[0];

					return fileLocation;
				}

				// Create a finger object
				var finger = new NFinger { FileName = fileLocation };
				subject = new NSubject();
				subject.Fingers.Add(finger);
				fingerView.Finger = finger;

				// Extract a template from the subject
				_biometricClient.FingersReturnBinarizedImage = true;
				_biometricClient.BeginCreateTemplate(subject, OnExtractCompleted, null);
			}
			return fileLocation;
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
					EnableCheckBoxes();
					EnableVerifyButton();
				}
			}
		}

		private void EnableVerifyButton()
		{
			verifyButton.Enabled = IsSubjectValid(_subject1) && IsSubjectValid(_subject2);
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
						msgLabel.Text = msg;
						MessageBox.Show(string.Format("{0}\n{1}", verificationStatus, msg));

						var matedMinutiae = _subject1.MatchingResults[0].MatchingDetails.Fingers[0].GetMatedMinutiae();

						fingerView1.MatedMinutiaIndex = 0;
						fingerView1.MatedMinutiae = matedMinutiae;

						fingerView2.MatedMinutiaIndex = 1;
						fingerView2.MatedMinutiae = matedMinutiae;

						fingerView1.PrepareTree();
						fingerView2.Tree = fingerView1.Tree;
					}
					else MessageBox.Show(verificationStatus);
				}
				catch (Exception ex)
				{
					Utils.ShowException(ex);
				}
			}
		}

		private bool IsSubjectValid(NSubject subject)
		{
			return subject != null && (subject.Status == NBiometricStatus.Ok
				|| subject.Status == NBiometricStatus.MatchNotFound
				|| subject.Status == NBiometricStatus.None && subject.GetTemplateBuffer() != null);
		}

		private void SetFar()
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

		private void SetDefaultFar()
		{
			matchingFarComboBox.Text = Utils.MatchingThresholdToString(_defaultFar);
			defaultButton.Enabled = false;
			SetFar();
		}

		private void ResetMatedMinutiaeOnViews()
		{
			fingerView1.Tree = fingerView2.Tree =
			fingerView1.MatedMinutiae = fingerView2.MatedMinutiae = null;
		}

		private void EnableCheckBoxes()
		{
			chbShowBinarizedImage1.Enabled = _subject1 != null && _subject1.Status == NBiometricStatus.Ok;
			chbShowBinarizedImage2.Enabled = _subject2 != null && _subject2.Status == NBiometricStatus.Ok;
		}

		#endregion

		#region Private form events

		private void OpenImageButton1Click(object sender, EventArgs e)
		{
			chbShowBinarizedImage1.Enabled = false;
			templateLeftLabel.Text = string.Empty;
			templateLeftLabel.Text = OpenImageTemplate(fingerView1, out _subject1);
		}

		private void OpenImageButton2Click(object sender, EventArgs e)
		{
			chbShowBinarizedImage2.Enabled = false;
			templateRightLabel.Text = string.Empty;
			templateRightLabel.Text = OpenImageTemplate(fingerView2, out _subject2);
		}

		private void MatchingFarComboBoxEnter(object sender, EventArgs e)
		{
			defaultButton.Enabled = true;
		}

		private void DefaultButtonClick(object sender, EventArgs e)
		{
			SetDefaultFar();
		}

		private void ClearImagesButtonClick(object sender, EventArgs e)
		{
			_subject1 = null;
			_subject2 = null;
			verifyButton.Enabled = false;

			fingerView1.Finger = null;
			fingerView2.Finger = null;

			msgLabel.Text = string.Empty;
			templateLeftLabel.Text = string.Empty;
			templateRightLabel.Text = string.Empty;

			chbShowBinarizedImage1.Enabled = false;
			chbShowBinarizedImage2.Enabled = false;
		}

		private void VerifyFingerLoad(object sender, EventArgs e)
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
			matchingFarComboBox.SelectedIndex = 1;
		}

		private void VerifyButtonClick(object sender, EventArgs e)
		{
			if (_subject1 != null && _subject2 != null)
			{
				_biometricClient.MatchingWithDetails = true;
				fingerView1.MatedMinutiae = null;
				fingerView2.MatedMinutiae = null;
				_biometricClient.BeginVerify(_subject1, _subject2, OnVerifyCompleted, null);
			}
			verifyButton.Enabled = false;
		}

		private void MatchingFarComboBoxLeave(object sender, EventArgs e)
		{
			SetFar();
		}

		private void FingerView1SelectedTreeMinutiaIndexChanged(object sender, EventArgs e)
		{
			var args = e as TreeMinutiaEventArgs;
			if (fingerView2 != null && args != null)
			{
				fingerView2.SelectedMinutiaIndex = args.Index;
			}
		}

		private void FingerView2SelectedTreeMinutiaIndexChanged(object sender, EventArgs e)
		{
			var args = e as TreeMinutiaEventArgs;
			if (fingerView1 != null && args != null)
			{
				fingerView1.SelectedMinutiaIndex = args.Index;
			}
		}

		private void VerifyFingerVisibleChanged(object sender, EventArgs e)
		{
			if (Visible && _biometricClient != null)
			{
				SetDefaultFar();
			}
		}

		private void ChbShowBinarizedImage1CheckedChanged(object sender, EventArgs e)
		{
			fingerView1.ShownImage = chbShowBinarizedImage1.Checked ? ShownImage.Result : ShownImage.Original;
		}

		private void ChbShowBinarizedImage2CheckedChanged(object sender, EventArgs e)
		{
			fingerView2.ShownImage = chbShowBinarizedImage2.Checked ? ShownImage.Result : ShownImage.Original;
		}

		private void FingerView1MouseClick(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right && chbShowBinarizedImage1.Enabled)
			{
				chbShowBinarizedImage1.Checked = !chbShowBinarizedImage1.Checked;
			}
		}

		private void FingerView2MouseClick(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right && chbShowBinarizedImage2.Enabled)
			{
				chbShowBinarizedImage2.Checked = !chbShowBinarizedImage2.Checked;
			}
		}

		#endregion

	}
}
