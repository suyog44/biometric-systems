using System;
using System.IO;
using System.Windows.Forms;
using Neurotec.Biometrics;
using Neurotec.Biometrics.Client;
using Neurotec.IO;

namespace Neurotec.Samples
{
	public partial class IdentifyVoice : UserControl
	{
		#region Public constructor

		public IdentifyVoice()
		{
			InitializeComponent();
		}

		#endregion

		#region Private fields

		private NBiometricClient _biometricClient;
		private NSubject _subject;
		private NSubject[] _subjects;

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
				cbMatchingFar.Text = Utils.MatchingThresholdToString(_defaultFar);
			}
		}

		#endregion

		#region Private methods

		private void OnEnrollCompleted(IAsyncResult r)
		{
			if (InvokeRequired)
			{
				BeginInvoke(new AsyncCallback(OnEnrollCompleted), r);
			}
			else
			{
				NBiometricTask task = _biometricClient.EndPerformTask(r);
				if (task.Status == NBiometricStatus.Ok)
				{
					btnIdentify.Enabled = IsSubjectValid(_subject);
				}
				else MessageBox.Show(string.Format("Enrollment failed: {0}", task.Status));
			}
		}

		private void OnIdentifyDone(IAsyncResult r)
		{
			if (InvokeRequired)
			{
				BeginInvoke(new AsyncCallback(OnIdentifyDone), r);
			}
			else
			{
				try
				{
					NBiometricStatus status = _biometricClient.EndIdentify(r);
					if (status == NBiometricStatus.Ok || status == NBiometricStatus.MatchNotFound)
					{
						// Matching subjects
						foreach (var result in _subject.MatchingResults)
						{
							listView.Items.Add(new ListViewItem(new string[] { result.Id, result.Score.ToString() }));
						}
						bool isMatchingResult;
						// Non-matching subjects
						foreach (var subject in _subjects)
						{
							isMatchingResult = false;
							foreach (var result in _subject.MatchingResults)
							{
								if (subject.Id == result.Id)
								{
									isMatchingResult = true;
									break;
								}
							}
							if (!isMatchingResult)
								listView.Items.Add(new ListViewItem(new string[] { subject.Id, "0" }));
						}
					}
					else MessageBox.Show(string.Format("Identification failed: {0}", status));
				}
				catch (Exception ex)
				{
					Utils.ShowException(ex);
				}
			}
		}

		private void OnExtractionCompleted(IAsyncResult r)
		{
			if (InvokeRequired)
			{
				BeginInvoke(new AsyncCallback(OnExtractionCompleted), r);
			}
			else
			{
				NBiometricStatus status = NBiometricStatus.None;
				try
				{
					status = _biometricClient.EndCreateTemplate(r);
					if (status != NBiometricStatus.Ok)
					{
						MessageBox.Show(string.Format("Template was not extracted: {0}.", status), Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
					}
				}
				catch (Exception ex)
				{
					Utils.ShowException(ex);
				}
				finally
				{
					if (status == NBiometricStatus.Ok)
					{
						btnIdentify.Enabled = _subjects != null;
					}
					else
					{
						_subject = null;
						btnIdentify.Enabled = false;
					}
				}
			}
		}

		private void SetFar()
		{
			_biometricClient.VoicesUniquePhrasesOnly = chbUniquePhrases.Checked;

			try
			{
				_biometricClient.MatchingThreshold = Utils.MatchingThresholdFromString(cbMatchingFar.Text);
				cbMatchingFar.Text = Utils.MatchingThresholdToString(_biometricClient.MatchingThreshold);
			}
			catch
			{
				MessageBox.Show(@"FAR is not valid", Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
				cbMatchingFar.Select();
			}
		}

		private void SetDefaultFar()
		{
			cbMatchingFar.Text = Utils.MatchingThresholdToString(_defaultFar);
			btnDefault.Enabled = false;
			SetFar();
		}

		private bool IsSubjectValid(NSubject subject)
		{
			return subject != null && (subject.Status == NBiometricStatus.Ok
				|| subject.Status == NBiometricStatus.None && subject.GetTemplateBuffer() != null);
		}

		#endregion

		#region Private form events

		private void BtnOpenTemplatesClick(object sender, EventArgs e)
		{
			btnIdentify.Enabled = false;

			lblTemplatesCount.Text = @"0";
			_subjects = null;
			openFileDialog.Multiselect = true;
			openFileDialog.FileName = null;
			openFileDialog.Filter = string.Empty;
			openFileDialog.Title = @"Open Templates Files";
			if (openFileDialog.ShowDialog() == DialogResult.OK)
			{
				try
				{
					int templatesCount = openFileDialog.FileNames.Length;
					_biometricClient.Clear(); // Clear previously enrolled subjects
					_subjects = new NSubject[templatesCount];
					for (int i = 0; i < templatesCount; ++i)
					{
						_subjects[i] = NSubject.FromFile(openFileDialog.FileNames[i]);
						_subjects[i].Id = Path.GetFileName(openFileDialog.FileNames[i]);
					}
					lblTemplatesCount.Text = templatesCount.ToString();

					NBiometricTask task = _biometricClient.CreateTask(NBiometricOperations.Enroll, null);
					foreach (NSubject item in _subjects)
					{
						task.Subjects.Add(item);
					}
					_biometricClient.BeginPerformTask(task, OnEnrollCompleted, null);
				}
				catch (Exception ex)
				{
					Utils.ShowException(ex);
				}
			}
		}

		private void BtnOpenAudioFileClick(object sender, EventArgs e)
		{
			btnIdentify.Enabled = false;

			openFileDialog.FileName = null;
			openFileDialog.Filter = string.Empty;
			openFileDialog.Title = @"Open Audio File Or Template";

			lblFileForIdentification.Text = string.Empty;
			_subject = null;
			if (openFileDialog.ShowDialog() == DialogResult.OK)
			{
				lblFileForIdentification.Text = openFileDialog.FileName;

				// Check if given file is a template
				try
				{
					_subject = NSubject.FromFile(openFileDialog.FileName);
					btnIdentify.Enabled = _subjects != null;
				}
				catch { }

				// If file is not a template, try to load it as audio file
				if (_subject == null)
				{
					// Create an iris object
					NVoice voice = new NVoice() { FileName = openFileDialog.FileName };
					// Add the voice to a subject
					_subject = new NSubject();
					_subject.Voices.Add(voice);

					// Extract the template from the subject
					_biometricClient.BeginCreateTemplate(_subject, OnExtractionCompleted, null);
				}
			}
		}

		private void BtnIdentifyClick(object sender, EventArgs e)
		{
			listView.Items.Clear();
			if (_subject != null && _subjects != null && _subjects.Length > 0)
			{
				_biometricClient.BeginIdentify(_subject, OnIdentifyDone, null);
			}
		}

		private void BtnDefaultClick(object sender, EventArgs e)
		{
			cbMatchingFar.SelectedIndex = 1;
			btnDefault.Enabled = false;
		}

		private void CbMatchingFarEnter(object sender, EventArgs e)
		{
			btnDefault.Enabled = true;
		}

		private void IdentifyVoiceLoad(object sender, EventArgs e)
		{
			lblFileForIdentification.Text = string.Empty;
			lblTemplatesCount.Text = @"0";

			try
			{
				cbMatchingFar.BeginUpdate();
				cbMatchingFar.Items.Add(0.001.ToString("P1"));
				cbMatchingFar.Items.Add(0.0001.ToString("P2"));
				cbMatchingFar.Items.Add(0.00001.ToString("P3"));
			}
			finally
			{
				cbMatchingFar.EndUpdate();
			}
		}

		private void CbMatchingFarLeave(object sender, EventArgs e)
		{
			SetFar();
		}

		private void IdentifyVoiceVisibleChanged(object sender, EventArgs e)
		{
			if (Visible && _biometricClient != null)
			{
				SetDefaultFar();
			}
		}

		private void ChbUniquePhrasesCheckedChanged(object sender, EventArgs e)
		{
			_biometricClient.VoicesUniquePhrasesOnly = chbUniquePhrases.Checked;
		}

		#endregion
	}
}
