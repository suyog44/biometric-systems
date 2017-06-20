using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Neurotec.Biometrics;
using Neurotec.Biometrics.Client;
using Neurotec.Images;

namespace Neurotec.Samples
{
	public partial class IdentifyFace : UserControl
	{
		#region Public constructor

		public IdentifyFace()
		{
			InitializeComponent();
		}

		#endregion

		#region Private fields

		private NBiometricClient _biometricClient;
		private NSubject _subject;
		private NSubject[] _subjects;

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
					// Identify current subject with enrolled ones
					_biometricClient.BeginIdentify(_subject, OnIdentifyDone, null);
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
							listView.Items.Add(new ListViewItem(new[] { result.Id, result.Score.ToString(CultureInfo.InvariantCulture) }));
						}
						foreach (var subject in _subjects)
						{
							bool isMatchingResult = _subject.MatchingResults.Any(result => subject.Id == result.Id);
							if (!isMatchingResult)
								listView.Items.Add(new ListViewItem(new[] { subject.Id, "0" }));
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
						identifyButton.Enabled = _subjects != null;
					}
					else
					{
						_subject = null;
						identifyButton.Enabled = false;
					}
				}
			}
		}

		#endregion

		#region Private form events

		private void MatchingFarComboBoxEnter(object sender, EventArgs e)
		{
			defaultMatchingFARButton.Enabled = true;
		}

		private void OpenTemplatesButtonClick(object sender, EventArgs e)
		{
			identifyButton.Enabled = false;

			templatesCountLabel.Text = @"0";
			openFileDialog.Multiselect = true;
			openFileDialog.FileName = null;
			openFileDialog.Filter = null;
			openFileDialog.Title = @"Open Templates Files";
			if (openFileDialog.ShowDialog() == DialogResult.OK)
			{
				try
				{
					int templatesCount = openFileDialog.FileNames.Length;
					_subjects = new NSubject[templatesCount];
					// Create subjects from selected templates
					for (int i = 0; i < templatesCount; i++)
					{
						_subjects[i] = NSubject.FromFile(openFileDialog.FileNames[i]);
						_subjects[i].Id = Path.GetFileName(openFileDialog.FileNames[i]);
					}
					templatesCountLabel.Text = templatesCount.ToString(CultureInfo.InvariantCulture);
					identifyButton.Enabled = _subject != null;
				}
				catch (Exception ex)
				{
					Utils.ShowException(ex);
				}
			}
		}

		private void OpenImageButtonClick(object sender, EventArgs e)
		{
			identifyButton.Enabled = false;

			openFileDialog.FileName = null;
			openFileDialog.Filter = string.Empty;

			_subject = null;
			faceView.Face = null;
			fileForIdentificationLabel.Text = string.Empty;

			openFileDialog.Title = @"Open Image File Or Template";
			if (openFileDialog.ShowDialog() == DialogResult.OK)
			{
				fileForIdentificationLabel.Text = openFileDialog.FileName;

				// Check if given file is a template
				try
				{
					_subject = NSubject.FromFile(openFileDialog.FileName);
					identifyButton.Enabled = _subjects != null;
				}
				catch { }

				// If file is not a template, try to load it as image
				if (_subject == null)
				{
					// Create a face object
					var face = new NFace() { FileName = openFileDialog.FileName };
					faceView.Face = face;
					_subject = new NSubject();
					_subject.Faces.Add(face);

					// Extract a template from the subject
					_biometricClient.BeginCreateTemplate(_subject, OnExtractionCompleted, null);
				}
			}
		}

		private void IdentifyButtonClick(object sender, EventArgs e)
		{
			listView.Items.Clear();
			if (_subject != null && _subjects != null && _subjects.Length > 0)
			{
				try
				{
					// Clean earlier data before proceeding, enroll new data
					_biometricClient.Clear();
					// Create enrollment task
					var enrollmentTask = new NBiometricTask(NBiometricOperations.Enroll);
					// Create subjects from templates and set them for enrollment
					foreach (NSubject t in _subjects)
					{
						enrollmentTask.Subjects.Add(t);
					}
					// Enroll subjects
					_biometricClient.BeginPerformTask(enrollmentTask, OnEnrollCompleted, null);
				}
				catch (Exception ex)
				{
					Utils.ShowException(ex);
				}
			}
		}

		private void DefaultMatchingFARButtonClick(object sender, EventArgs e)
		{
			matchingFarComboBox.Text = Utils.MatchingThresholdToString(_defaultFar);
			defaultMatchingFARButton.Enabled = false;
		}

		private void IdentifyFaceLoad(object sender, EventArgs e)
		{
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

		private void MatchingFarComboBoxLeave(object sender, EventArgs e)
		{
			try
			{
				_biometricClient.MatchingThreshold = Utils.MatchingThresholdFromString(matchingFarComboBox.Text);
				matchingFarComboBox.Text = Utils.MatchingThresholdToString(_biometricClient.MatchingThreshold);
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
