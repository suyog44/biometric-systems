using System;
using System.IO;
using System.Windows.Forms;
using Neurotec.Biometrics;
using Neurotec.Biometrics.Client;
using Neurotec.IO;

namespace Neurotec.Samples
{
	public partial class VerifyVoice : UserControl
	{
		#region Public constructor

		public VerifyVoice()
		{
			InitializeComponent();
		}

		#endregion

		#region Private fields

		private NBiometricClient _biometricClient;
		private NSubject _subject1;
		private NSubject _subject2;

		private int _defaultFar;
		private bool _defaultUniquePhrases;

		#endregion

		#region Public properties

		public NBiometricClient BiometricClient
		{
			get { return _biometricClient; }
			set
			{
				_biometricClient = value;
				_defaultUniquePhrases = _biometricClient.VoicesUniquePhrasesOnly;
				_defaultFar = _biometricClient.MatchingThreshold;
				chbUniquePhrases.Checked = _defaultUniquePhrases;
				cbMatchingFAR.Text = Utils.MatchingThresholdToString(_defaultFar);
			}
		}

		#endregion

		#region Private methods

		private string OpenTemplateOrFile(out NSubject subject)
		{
			subject = null;
			lblMsg.Text = string.Empty;
			string fileName = string.Empty;

			openFileDialog.FileName = null;
			openFileDialog.Title = @"Open voice template or audio file";
			if (openFileDialog.ShowDialog() == DialogResult.OK) // load template
			{
				fileName = openFileDialog.FileName;

				// Check if given file is a template
				var fileData = new NBuffer(File.ReadAllBytes(openFileDialog.FileName));
				try
				{
					NTemplate.Check(fileData);
					subject = new NSubject();
					subject.SetTemplateBuffer(fileData);
					EnableVerifyButton();
				}
				catch { }

				// If file is not a template, try to load it as audio file
				if (subject == null)
				{
					// Create voice object
					var voice = new NVoice { FileName = fileName };
					subject = new NSubject();
					subject.Voices.Add(voice);

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
					else
					{
						lblMsg.Text = verificationStatus;
						MessageBox.Show(verificationStatus);
					}
				}
				catch (Exception ex)
				{
					Utils.ShowException(ex);
				}
			}
		}

		private void SetFar()
		{
			_biometricClient.VoicesUniquePhrasesOnly = chbUniquePhrases.Checked;

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

		private void BtnVerifyClick(object sender, EventArgs e)
		{
			if (_subject1 != null && _subject2 != null)
			{
				_biometricClient.BeginVerify(_subject1, _subject2, OnVerifyCompleted, null);
			}
			btnVerify.Enabled = false;
		}

		private void BtnDefaultClick(object sender, EventArgs e)
		{
			SetDefaultFar();
		}

		private void BtnOpen1Click(object sender, EventArgs e)
		{
			lblFirstTemplate.Text = OpenTemplateOrFile(out _subject1);
		}

		private void BtnOpen2Click(object sender, EventArgs e)
		{
			lblSecondTemplate.Text = OpenTemplateOrFile(out _subject2);
		}

		private void CbMatchingFAREnter(object sender, EventArgs e)
		{
			btnDefault.Enabled = true;
		}

		private void VerifyVoiceLoad(object sender, EventArgs e)
		{
			lblMsg.Text = string.Empty;
			lblFirstTemplate.Text = string.Empty;
			lblSecondTemplate.Text = string.Empty;
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

		private void CbMatchingFARLeave(object sender, EventArgs e)
		{
			SetFar();
		}

		private void VerifyVoiceVisibleChanged(object sender, EventArgs e)
		{
			if (Visible && _biometricClient != null)
			{
				SetDefaultFar();
			}
		}

		private void chbUniquePhrasesCheckedChanged(object sender, EventArgs e)
		{
			_biometricClient.VoicesUniquePhrasesOnly = chbUniquePhrases.Checked;
		}

		#endregion
	}
}
