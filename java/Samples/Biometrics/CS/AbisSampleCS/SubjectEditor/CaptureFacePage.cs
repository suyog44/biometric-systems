using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using Neurotec.Biometrics;
using Neurotec.Biometrics.Client;
using Neurotec.Images;

namespace Neurotec.Samples
{
	public partial class CaptureFacePage : Neurotec.Samples.PageBase
	{
		#region Public constructor

		public CaptureFacePage()
		{
			InitializeComponent();
		}

		#endregion

		#region Private fields

		private NSubject _subject;
		private NBiometricClient _biometricClient;
		private ManualResetEvent _isIdle = new ManualResetEvent(true);
		private NFace _currentBiometric = null;
		private NSubject _newSubject;
		private bool _isExtractStarted = false;
		private int _sessionId = -1;
		private string _titlePrefix = string.Empty;

		#endregion

		#region Public overridden methods

		public override void OnNavigatedTo(params object[] args)
		{
			if (args == null || args.Length != 2) throw new ArgumentException("args");

			_subject = (NSubject)args[0];
			_biometricClient = (NBiometricClient)args[1];
			if (_subject == null || _biometricClient == null) throw new ArgumentException("args");

			_sessionId = _subject.Faces.Count > 0 ? _subject.Faces.Max(x => x.SessionId) + 1 : 0;
			_newSubject = new NSubject();
			_biometricClient.PropertyChanged += OnBiometricClientPropertyChanged;
			_biometricClient.CurrentBiometricCompletedTimeout = 5000;
			_biometricClient.CurrentBiometricCompleted += OnCurrentBiometricCompleted;

			subjectTreeControl.Visible = false;
			icaoWarningView.Visible = false;
			OnFaceCaptureDeviceChanged();
			ToggleRadioButton();
			lblStatus.Visible = false;
			generalizationView.Visible = false;
			faceView.MirrorHorizontally = chbMirrorHorizontally.Checked = SettingsManager.FacesMirrorHorizontally;

			subjectTreeControl.Subject = _newSubject;

			base.OnNavigatedTo(args);
		}

		public override void OnNavigatingFrom()
		{
			if (IsBusy()) LongActionDialog.ShowDialog(this, "Finish current action ...", CancelAndWait);
			_biometricClient.PropertyChanged -= OnBiometricClientPropertyChanged;
			_biometricClient.CurrentBiometricCompleted -= OnCurrentBiometricCompleted;
			_biometricClient.CurrentBiometricCompletedTimeout = 0;
			subjectTreeControl.SelectedItem = null;
			subjectTreeControl.Subject = null;
			faceView.Face = null;
			icaoWarningView.Face = null;
			if (_newSubject.Status == NBiometricStatus.Ok)
			{
				var faces = _newSubject.Faces.ToArray();
				_newSubject.Clear();
				foreach (var face in faces)
				{
					_subject.Faces.Add(face);
				}
			}
			_newSubject = null;
			_subject = null;
			_biometricClient = null;

			SettingsManager.FacesMirrorHorizontally = chbMirrorHorizontally.Checked;

			base.OnNavigatingFrom();
		}

		#endregion

		#region Private methods

		private void SetIsBusy(bool value)
		{
			if (value)
				_isIdle.Reset();
			else
				_isIdle.Set();
		}

		private bool IsBusy()
		{
			return !_isIdle.WaitOne(0);
		}

		private void CancelAndWait()
		{
			if (IsBusy())
			{
				_biometricClient.Cancel();
				_isIdle.WaitOne();
			}
		}

		private void SetStatusText(Color backColor, string format, params object[] args)
		{
			lblStatus.Text = string.Format(format, args);
			lblStatus.BackColor = backColor;
			lblStatus.Visible = true;
		}

		private void EnableControls()
		{
			bool canCancel = rbFromCamera.Checked || rbFromVideo.Checked;
			bool isManual = chbManual.Checked;
			bool isStream = chbStream.Checked;
			bool isIdle = !IsBusy();
			bool isLocalCreate = (_biometricClient.LocalOperations & NBiometricOperations.CreateTemplate) == NBiometricOperations.CreateTemplate;
			bool checkIcao = chbCheckIcaoCompliance.Checked;
			chbStream.Enabled = !rbFromFile.Checked && isLocalCreate;
			chbStream.Checked = chbStream.Checked && isLocalCreate;
			chbManual.Enabled = !rbFromFile.Checked;
			chbCheckIcaoCompliance.Enabled = isIdle;
			rbFromCamera.Enabled = _biometricClient.FaceCaptureDevice != null;
			gbCaptureOptions.Enabled = isIdle;
			btnCancel.Enabled = !isIdle && canCancel;
			btnForceStart.Enabled = !isIdle && ((isManual && !_isExtractStarted) || checkIcao);
			btnForceEnd.Enabled = false;
			btnCancel.Visible = btnForceEnd.Visible = btnForceStart.Visible = canCancel;
			btnRepeat.Enabled = false;
			btnRepeat.Visible = chbWithGeneralization.Checked && btnCancel.Visible;

			bool boldStart = btnForceStart.Enabled && isManual;
			bool boldFinish = isIdle && _newSubject.Status == NBiometricStatus.Ok;
			btnForceStart.Font = new Font(btnForceStart.Font, boldStart ? FontStyle.Bold : FontStyle.Regular);
			btnForceStart.Text = checkIcao && !isManual ? "Force" : "Start";
			btnFinish.Font = new Font(btnFinish.Font, boldFinish ? FontStyle.Bold : FontStyle.Regular);

			busyIndicator.Visible = !isIdle;
		}

		private void ToggleRadioButton()
		{
			EnableControls();
		}

		private void OnFacePropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			if (e.PropertyName == "Status")
			{
				BeginInvoke(new Action<NBiometricStatus>(OnFaceStatusChanged), ((NBiometric)sender).Status);
			}
		}

		private void OnFaceStatusChanged(NBiometricStatus status)
		{
			string format = _isExtractStarted ? "Extracion status: {0}" : "Detection status: {0}";
			Color backColor = status == NBiometricStatus.Ok || status == NBiometricStatus.None ? Color.Green : Color.Red;
			SetStatusText(backColor, _titlePrefix + format, status);
		}

		private void OnFaceCaptureDeviceChanged()
		{
			var device = _biometricClient.FaceCaptureDevice;
			if (device == null || !device.IsAvailable)
			{
				if (rbFromCamera.Checked) rbFromFile.Checked = true;
				rbFromCamera.Text = "From camera (Not connected)";
			}
			else
			{
				rbFromCamera.Text = string.Format("From camera ({0})", device.DisplayName);
			}
			EnableControls();
		}

		private void OnBiometricClientPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			if (e.PropertyName == "FaceCaptureDevice")
			{
				BeginInvoke(new Action(() =>
				{
					if (IsPageShown) OnFaceCaptureDeviceChanged();
				}));
			}
			else if (e.PropertyName == "CurrentBiometric")
			{
				BeginInvoke(new Action<NBiometric>(x =>
					{
						if (IsPageShown)
						{
							NFace face = (NFace)x;
							if (face != null) faceView.Face = face;
							if (face != null && chbCheckIcaoCompliance.Checked) icaoWarningView.Face = face;
							if (chbWithGeneralization.Checked && face != null)
							{
								int index = _newSubject.Faces.IndexOf(face);
								generalizationView.Selected = x;
								_titlePrefix = string.Format("Capturing face {0} of {1}. ", index + 1, SettingsManager.FacesGeneralizationRecordCount);
							}
							if (rbFromCamera.Checked || rbFromVideo.Checked)
							{
								_isExtractStarted = !chbManual.Checked;
								if (_currentBiometric != null) _currentBiometric.PropertyChanged -= OnFacePropertyChanged;
								_currentBiometric = face;
								if (_currentBiometric != null) _currentBiometric.PropertyChanged += OnFacePropertyChanged;
							}

							EnableControls();
						}
					}), ((NBiometricClient)sender).CurrentBiometric);
			}
		}

		private void UpdateWithTaskResult(NBiometricStatus status)
		{
			if (IsPageShown)
			{
				PrepareViews(false, chbCheckIcaoCompliance.Checked, status == NBiometricStatus.Ok);

				bool withGeneralization = chbWithGeneralization.Checked;
				Color backColor = status == NBiometricStatus.Ok ? Color.Green : Color.Red;
				SetStatusText(backColor, "Extraction status: {0}", status == NBiometricStatus.Timeout ? "Liveness check failed" : status.ToString());
				if (withGeneralization && status == NBiometricStatus.Ok)
				{
					NFace generalized = _newSubject.Faces.Last();
					generalizationView.Generalized = new[] { generalized };
					generalizationView.Selected = generalized;
				}
				generalizationView.EnableMouseSelection = true;
				EnableControls();
			}
		}

		private void OnCurrentBiometricCompleted(object sender, EventArgs e)
		{
			NFace face = (NFace)_biometricClient.CurrentBiometric;
			NBiometricStatus status = face.Status;
			if (status == NBiometricStatus.Ok)
			{
				var attributes = face.Objects.FirstOrDefault();
				var child = attributes != null ? attributes.Child : null;
				if (child != null && child.Status != NBiometricStatus.Ok)
					status = child.Status;
			}

			BeginInvoke(new Action<NBiometricStatus>(OnCurrentBiometricCompletedInternal), status);
		}

		private void OnCurrentBiometricCompletedInternal(NBiometricStatus status)
		{
			bool allowRepeat = btnRepeat.Visible && status != NBiometricStatus.Ok;
			if (!allowRepeat)
				_biometricClient.Force();
			else
			{
				SetStatusText(Color.Red, _titlePrefix + "Extracion status: {0}", status);
			}
			btnRepeat.Enabled = allowRepeat;
		}

		private void ChbCheckIcaoComplianceCheckedChanged(object sender, EventArgs e)
		{
			if (chbCheckIcaoCompliance.Checked)
			{
				chbStream.Checked = chbStream.Enabled;
				chbManual.Checked = false;
			}
		}

		#endregion

		#region Private form events

		private void BtnForceStartClick(object sender, EventArgs e)
		{
			if (!chbCheckIcaoCompliance.Checked)
			{
				_isExtractStarted = true;
				btnForceEnd.Enabled = chbStream.Checked;
				btnForceStart.Enabled = false;
			}
			else if (chbManual.Checked)
			{
				btnForceStart.Text = "Force";
				btnForceStart.Font = new Font(btnFinish.Font, FontStyle.Regular);
			}
			_biometricClient.Force();
		}

		private void BtnForceEndClick(object sender, EventArgs e)
		{
			btnForceEnd.Enabled = false;
			_isExtractStarted = false;
			_biometricClient.Force();
		}

		private void BtnCancelClick(object sender, EventArgs e)
		{
			_biometricClient.Cancel();
		}

		private void RadioButtonCheckedChanged(object sender, EventArgs e)
		{
			if (((RadioButton)sender).Checked)
				ToggleRadioButton();
		}

		private void BtnFinishClick(object sender, EventArgs e)
		{
			PageController.NavigateToStartPage();
		}

		private void PrepareViews(bool isCapturing, bool checkIcao)
		{
			PrepareViews(isCapturing, checkIcao, true);
		}

		private void PrepareViews(bool isCapturing, bool checkIcao, bool isOk)
		{
			icaoWarningView.Visible = checkIcao;
			if (isCapturing)
			{
				faceView.ShowAge = !checkIcao;
				faceView.ShowEmotions = !checkIcao;
				faceView.ShowExpression = !checkIcao;
				faceView.ShowGender = !checkIcao;
				faceView.ShowProperties = !checkIcao;
				faceView.ShowIcaoArrows = true;
				subjectTreeControl.Visible = false;
			}
			else
			{
				faceView.ShowAge = true;
				faceView.ShowEmotions = true;
				faceView.ShowExpression = true;
				faceView.ShowGender = true;
				faceView.ShowProperties = true;
				faceView.ShowIcaoArrows = false;
				subjectTreeControl.Visible = checkIcao && isOk;
				if (checkIcao)
				{
					subjectTreeControl.UpdateTree();
					var node = subjectTreeControl.Nodes.First();
					subjectTreeControl.SelectedItem = node.GetChildren().FirstOrDefault() ?? node;
				}
			}
			faceView.Invalidate();
		}

		private void BtnCaptureClick(object sender, EventArgs e)
		{
			bool generalize = chbWithGeneralization.Checked;
			bool fromFile = rbFromFile.Checked;
			bool fromCamera = rbFromCamera.Checked;
			bool checkIcao = chbCheckIcaoCompliance.Checked;
			int count = generalize ? SettingsManager.FacesGeneralizationRecordCount : 1;
			NBiometricCaptureOptions options = NBiometricCaptureOptions.None;
			if (chbManual.Checked) options |= NBiometricCaptureOptions.Manual;
			if (chbStream.Checked) options |= NBiometricCaptureOptions.Stream;

			lblStatus.Visible = false;
			_titlePrefix = string.Empty;
			_newSubject.Clear();
			faceView.Face = null;
			generalizationView.Clear();
			generalizationView.EnableMouseSelection = false;
			generalizationView.Visible = generalize;

			List<string> selectedFiles = new List<string>();
			string title = fromFile ? "Select image" : "Select video file";
			string titleFormat = fromFile ? "Select face image ({0} out of {1})" : "Select video file ({0} out of {1})";
			string fileFilter = fromFile ? NImages.GetOpenFileFilterString(true, true) : null;
			if (rbFromFile.Checked || rbFromVideo.Checked)
			{
				openFileDialog.FileName = null;
				openFileDialog.Filter = fileFilter;
				openFileDialog.Title = title;
				while (selectedFiles.Count < count)
				{
					if (generalize) openFileDialog.Title = string.Format(titleFormat, selectedFiles.Count + 1, count);
					if (openFileDialog.ShowDialog() != DialogResult.OK) return;
					selectedFiles.Add(openFileDialog.FileName);
				}
			}

			int id = generalize ? _sessionId : -1;
			for (int i = 0; i < count; i++)
			{
				NFace face = new NFace
				{
					SessionId = id,
					FileName = !fromCamera ? selectedFiles[i] : null,
					CaptureOptions = options
				};
				_newSubject.Faces.Add(face);
			}
			faceView.Face = _newSubject.Faces.First();

			if (generalize)
			{
				generalizationView.Biometrics = _newSubject.Faces.ToArray();
				generalizationView.Selected = _newSubject.Faces.First();
			}

			icaoWarningView.Face = _newSubject.Faces.First();

			_biometricClient.FacesCheckIcaoCompliance = checkIcao;
			NBiometricOperations operations = fromFile ? NBiometricOperations.CreateTemplate : NBiometricOperations.Capture | NBiometricOperations.CreateTemplate;
			if (checkIcao) operations |= NBiometricOperations.Segment;
			NBiometricTask biometricTask = _biometricClient.CreateTask(operations, _newSubject);
			SetStatusText(Color.Orange, fromFile ? "Extracting template ..." : "Starting capturing ...");
			SetIsBusy(true);
			_biometricClient.BeginPerformTask(biometricTask, OnCreateTemplateCompleted, null);
			EnableControls();
			PrepareViews(true, checkIcao);
		}

		private void OnCreateTemplateCompleted(IAsyncResult r)
		{
			NBiometricStatus status = NBiometricStatus.InternalError;
			try
			{
				var biometricTask = _biometricClient.EndPerformTask(r);
				status = biometricTask.Status;
				if (biometricTask.Error != null) Utilities.ShowError(biometricTask.Error);
			}
			catch (Exception ex)
			{
				Utilities.ShowError(ex);
			}
			finally
			{
				SetIsBusy(false);
				BeginInvoke(new Action<NBiometricStatus>(UpdateWithTaskResult), status);
			}
		}

		private void BtnRepeatClick(object sender, EventArgs e)
		{
			_biometricClient.Repeat();
		}

		private void CaptureFacePageLoad(object sender, EventArgs e)
		{
			chbStream.Checked = (_biometricClient.LocalOperations & NBiometricOperations.CreateTemplate) != 0;
		}

		private void ChbMirrorHorizontallyCheckedChanged(object sender, EventArgs e)
		{
			faceView.MirrorHorizontally = chbMirrorHorizontally.Checked;
			faceView.Invalidate();
		}

		private void OnSubjectTreeControlPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			if (subjectTreeControl.Visible && e.PropertyName == "SelectedItem")
			{
				var selected = subjectTreeControl.SelectedItem;
				if (selected != null)
				{
					if (generalizationView.Visible)
					{
						generalizationView.Biometrics = selected.Items;
						generalizationView.Generalized = selected.GetAllGeneralized();
						generalizationView.Selected = selected.AllItems.First();
					}
					else
					{
						faceView.Face = selected.Items.First() as NFace;
						icaoWarningView.Face = faceView.Face;
					}
				}
			}
		}

		#endregion
	}
}
