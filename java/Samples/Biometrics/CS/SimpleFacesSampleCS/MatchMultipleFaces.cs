using System;
using System.Windows.Forms;
using Neurotec.Biometrics;
using Neurotec.Biometrics.Client;
using Neurotec.Images;

namespace Neurotec.Samples
{
	public partial class MatchMultipleFaces : UserControl
	{
		#region Public constructor

		public MatchMultipleFaces()
		{
			InitializeComponent();
		}

		#endregion

		#region Private fields

		private NBiometricClient _biometricClient;
		private NSubject _referenceSubject;
		private NSubject _multipleFacesSubject;

		#endregion

		#region Public properties

		public NBiometricClient BiometricClient
		{
			get { return _biometricClient; }
			set { _biometricClient = value; }
		}

		#endregion

		#region Private methods

		private void ExtractReferenceFace(String imagePath)
		{
			var face = new NFace { FileName = imagePath };
			_referenceSubject = new NSubject();
			_referenceSubject.Faces.Add(face);
			faceViewReference.Face = face;
			_biometricClient.BeginCreateTemplate(_referenceSubject, OnReferenceExtractionCompleted, null);
		}

		private void OnReferenceExtractionCompleted(IAsyncResult r)
		{
			if (InvokeRequired)
			{
				BeginInvoke(new AsyncCallback(OnReferenceExtractionCompleted), r);
			}
			else
			{
				try
				{
					NBiometricStatus status = _biometricClient.EndCreateTemplate(r);
					if (status != NBiometricStatus.Ok)
					{
						MessageBox.Show(@"Could not extract template from reference image.");
						_referenceSubject = null;
					}
				}
				catch (Exception ex)
				{
					Utils.ShowException(ex);
					_referenceSubject = null;
				}
			}
		}

		private void ExtractMultipleFace(String imagePath)
		{
			var face = new NFace { FileName = imagePath };
			_multipleFacesSubject = new NSubject();
			_multipleFacesSubject.Faces.Add(face);
			faceViewMultiFace.Face = face;
			// Image can have more than one faces
			_multipleFacesSubject.IsMultipleSubjects = true;
			_biometricClient.BeginCreateTemplate(_multipleFacesSubject, OnMultipleExtractionCompleted, null);
		}

		private void OnMultipleExtractionCompleted(IAsyncResult r)
		{
			if (InvokeRequired)
			{
				BeginInvoke(new AsyncCallback(OnMultipleExtractionCompleted), r);
			}
			else
			{
				NBiometricStatus status = NBiometricStatus.None;
				try
				{
					status = _biometricClient.EndCreateTemplate(r);
					if (status == NBiometricStatus.Ok)
					{
						// Enroll extracted faces
						EnrollMultipleFaceSubject();
					}
					else
					{
						MessageBox.Show(@"Could not extract template from multiple face image.");
						_multipleFacesSubject = null;
					}
				}
				catch (Exception ex)
				{
					Utils.ShowException(ex);
					_multipleFacesSubject = null;
				}
			}
		}

		private void EnrollMultipleFaceSubject()
		{
			_biometricClient.Clear();
			var enrollTask = new NBiometricTask(NBiometricOperations.Enroll);

			// Enroll all faces
			_multipleFacesSubject.Id = "firstSubject";
			enrollTask.Subjects.Add(_multipleFacesSubject);

			for (int i = 0; i < _multipleFacesSubject.RelatedSubjects.Count; i++)
			{
				NSubject tmpSubject = _multipleFacesSubject.RelatedSubjects[i];
				tmpSubject.Id = "relatedSubject" + i;
				enrollTask.Subjects.Add(tmpSubject);
			}
			_biometricClient.BeginPerformTask(enrollTask, OnEnrollCompleted, null);
		}

		private void OnEnrollCompleted(IAsyncResult r)
		{
			if (InvokeRequired)
			{
				BeginInvoke(new AsyncCallback(OnEnrollCompleted), r);
			}
			else
			{
				NBiometricTask task = _biometricClient.EndPerformTask(r);
				if (task.Status != NBiometricStatus.Ok) MessageBox.Show(string.Format("Enroll failed: {0}", task.Status));
				else if (_referenceSubject != null)
				{
					MatchFaces();
				}
			}
		}

		private void MatchFaces()
		{
			_biometricClient.BeginIdentify(_referenceSubject, OnIdentifyCompleted, null);
		}

		private void OnIdentifyCompleted(IAsyncResult r)
		{
			if (InvokeRequired)
			{
				BeginInvoke(new AsyncCallback(OnIdentifyCompleted), r);
			}
			else
			{
				int multipleFacesCount = _multipleFacesSubject.Faces.Count
					+ _multipleFacesSubject.RelatedSubjects.Count;
				var results = new string[multipleFacesCount];
				// Get matching scores
				foreach (NMatchingResult t in _referenceSubject.MatchingResults)
				{
					int score = t.Score;
					if (t.Id == _multipleFacesSubject.Id)
					{
						results[0] = string.Format("score: {0} (match)", score);
					}
					else
					{
						for (int j = 0; j < _multipleFacesSubject.RelatedSubjects.Count; j++)
						{
							if (t.Id == _multipleFacesSubject.RelatedSubjects[j].Id)
							{
								results[j + 1] = string.Format("score: {0} (match)", score);
							}
						}
					}
				}

				// All not matched faces have score 0
				for (int i = 0; i < results.Length; i++)
				{
					if (results[i] == null)
						results[i] = string.Format("score: 0");
				}
				faceViewMultiFace.FaceIds = results;
			}
		}

		#endregion

		#region Private form events

		private void TsbOpenReferenceClick(object sender, EventArgs e)
		{
			openImageFileDlg.Filter = NImages.GetOpenFileFilterString(true, true);
			openImageFileDlg.Title = @"Open Reference Image";
			if (openImageFileDlg.ShowDialog() == DialogResult.OK)
			{
				try
				{
					// Set template size (for matching medium is recommended) (optional)
					_biometricClient.FacesTemplateSize = NTemplateSize.Medium;
					ExtractReferenceFace(openImageFileDlg.FileName);
				}
				catch (Exception ex)
				{
					Utils.ShowException(ex);
				}
			}
		}

		private void TsbOpenMultifaceImageClick(object sender, EventArgs e)
		{
			openImageFileDlg.Filter = NImages.GetOpenFileFilterString(true, true);
			openImageFileDlg.Title = @"Open Reference Image";
			if (openImageFileDlg.ShowDialog() == DialogResult.OK)
			{
				try
				{
					// Set template size (for enrolling large is recomended) (optional)
					_biometricClient.FacesTemplateSize = NTemplateSize.Large;
					ExtractMultipleFace(openImageFileDlg.FileName);
				}
				catch (Exception ex)
				{
					Utils.ShowException(ex);
				}
			}
		}

		#endregion
	}
}
