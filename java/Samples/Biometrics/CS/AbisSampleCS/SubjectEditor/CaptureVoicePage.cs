using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using Neurotec.Biometrics;
using Neurotec.Biometrics.Client;

namespace Neurotec.Samples
{
	public partial class CaptureVoicePage : Neurotec.Samples.PageBase
	{
		#region Public constructor

		public CaptureVoicePage()
		{
			InitializeComponent();
		}

		#endregion

		#region Private fields

		private NBiometricClient _biometricClient;
		private NSubject _subject;
		private NSubject _newSubject;
		private NVoice _voice;
		private ManualResetEvent _isIdle = new ManualResetEvent(true);
		private List<Phrase> _phrases = new List<Phrase>();

		#endregion

		#region Public methods

		public override void OnNavigatedTo(params object[] args)
		{
			if (args == null || args.Length != 2) throw new ArgumentException("args");

			_subject = (NSubject)args[0];
			_biometricClient = (NBiometricClient)args[1];
			_biometricClient.PropertyChanged += OnBiometricClientPropertyChanged;

			_newSubject = new NSubject();
			_voice = new NVoice();
			_newSubject.Voices.Add(_voice);
			voiceView.Voice = _voice;
			_phrases = new List<Phrase>(SettingsManager.Phrases);
			ListAllPhrases();
			lblHint.Visible = false;

			OnVoiceCaptureDeviceChanged();
			EnableControls();

			base.OnNavigatedTo(args);
		}

		public override void OnNavigatingFrom()
		{
			if (IsBusy()) LongActionDialog.ShowDialog(this, "Finishin current action ...", CancelAndWait);
			_biometricClient.PropertyChanged -= OnBiometricClientPropertyChanged;
			if (_voice.Status == NBiometricStatus.Ok)
			{
				var voices = _newSubject.Voices.ToArray();
				_newSubject.Clear();
				foreach (var item in voices)
				{
					_subject.Voices.Add(item);
				}
			}
			voiceView.Voice = null;
			_newSubject = null;
			_voice = null;
			_subject = null;
			_biometricClient = null;

			base.OnNavigatingFrom();
		}

		#endregion

		#region Private methods

		private void SetHint(Color backColor, string format, params object[] args)
		{
			lblHint.BackColor = backColor;
			lblHint.Text = string.Format(format, args);
			lblHint.Visible = true;
		}

		private void ListAllPhrases()
		{
			try
			{
				cbPhrase.BeginUpdate();
				object selected = cbPhrase.SelectedItem;
				cbPhrase.Items.Clear();
				foreach (var item in _phrases)
				{
					cbPhrase.Items.Add(item);
				}
				cbPhrase.SelectedItem = selected;
				if (cbPhrase.Items.Count > 0 && cbPhrase.SelectedItem == null)
				{
					cbPhrase.SelectedIndex = 0;
				}
			}
			finally
			{
				cbPhrase.EndUpdate();
			}
		}

		private void EnableControls()
		{
			bool isIdle = !IsBusy();
			bool fromFile = rbFromFile.Checked;
			rbMicrophone.Enabled = _biometricClient.VoiceCaptureDevice != null &&
				_biometricClient.VoiceCaptureDevice.IsAvailable &&
				(_biometricClient.LocalOperations & (NBiometricOperations.CreateTemplate | NBiometricOperations.Segment)) != 0;
			gbPhrase.Enabled = isIdle;
			btnOpenFile.Enabled = fromFile && isIdle;
			btnStop.Enabled = !fromFile && !isIdle;
			btnStart.Enabled = !fromFile && isIdle;
			gbSource.Enabled = isIdle;
			chbCaptureAutomatically.Visible = !fromFile;
			chbCaptureAutomatically.Enabled = isIdle;
			btnForce.Visible = !fromFile;
			btnForce.Enabled = !fromFile && !chbCaptureAutomatically.Checked && !isIdle;

			bool boldFinish = isIdle && _voice != null && _voice.Status == NBiometricStatus.Ok;
			btnFinish.Font = new Font(btnFinish.Font, boldFinish ? FontStyle.Bold : FontStyle.Regular);

			busyIndicator.Visible = !isIdle;
		}

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

		private void OnVoiceCaptureDeviceChanged()
		{
			var device = _biometricClient.VoiceCaptureDevice;
			if (device == null || !device.IsAvailable)
			{
				rbFromFile.Checked = true;
				rbMicrophone.Text = "Microphone (Not connected)";
			}
			else
			{
				rbMicrophone.Text = string.Format("Microphone ({0})", device.DisplayName);
			}
			EnableControls();
		}

		private void OnBiometricClientPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			if (e.PropertyName == "VoiceCaptureDevice")
			{
				BeginInvoke(new Action(() =>
				{
					if (IsPageShown) OnVoiceCaptureDeviceChanged();
				}));
			}
		}

		#endregion

		#region Private events

		private void BtnEditClick(object sender, EventArgs e)
		{
			using (EditPhrasesForm form = new EditPhrasesForm())
			{
				form.Phrases = _phrases;
				form.ShowDialog(this);
				SettingsManager.Phrases = _phrases;
				ListAllPhrases();
			}
		}

		private void BtnOpenFileClick(object sender, EventArgs e)
		{
			if (openFileDialog.ShowDialog() == DialogResult.OK)
			{
				Phrase phrase = cbPhrase.SelectedItem as Phrase;
				_voice.SoundBuffer = null;
				_voice.FileName = openFileDialog.FileName;
				_voice.PhraseId = phrase != null ? phrase.Id : 0;
				var biometricTask = _biometricClient.CreateTask(NBiometricOperations.Segment | NBiometricOperations.CreateTemplate, _newSubject);
				SetIsBusy(true);
				_biometricClient.BeginPerformTask(biometricTask, OnCreateTemplateCompleted, null);
				SetHint(Color.Orange, "Extracting record. Please wait ...");
				EnableControls();
			}
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

			SetIsBusy(false);
			BeginInvoke(new Action<NBiometricStatus>(taskStatus =>
			{
				if (IsPageShown)
				{
					if (taskStatus == NBiometricStatus.Ok)
						SetHint(Color.Green, "Extraction successful");
					else
						SetHint(Color.Red, "Extraction failed: {0}", taskStatus);
					EnableControls();
				}
			}), status);
		}

		private void BtnStartClick(object sender, EventArgs e)
		{
			Phrase phrase = cbPhrase.SelectedItem as Phrase;
			_voice.FileName = null;
			_voice.SoundBuffer = null;
			_voice.PhraseId = phrase != null ? phrase.Id : 0;
			_voice.CaptureOptions = chbCaptureAutomatically.Checked ? NBiometricCaptureOptions.None : NBiometricCaptureOptions.Manual;
			var biometricTask = _biometricClient.CreateTask(NBiometricOperations.Capture | NBiometricOperations.Segment | NBiometricOperations.CreateTemplate, _newSubject);
			SetIsBusy(true);
			_biometricClient.BeginPerformTask(biometricTask, OnCreateTemplateCompleted, null);
			SetHint(Color.Orange, "Extracting record. Please say phrase ...");
			EnableControls();
		}

		private void BtnStopClick(object sender, EventArgs e)
		{
			btnStop.Enabled = false;
			_biometricClient.Force();
		}

		private void RadioButtonCheckedChanged(object sender, EventArgs e)
		{
			if (((RadioButton)sender).Checked)
				ToggleRadioButtons();
		}

		private void ToggleRadioButtons()
		{
			EnableControls();
		}

		private void BtnFinishClick(object sender, EventArgs e)
		{
			PageController.NavigateToStartPage();
		}

		private void BtnForceClick(object sender, EventArgs e)
		{
			_biometricClient.ForceStart();
			btnForce.Enabled = false;
		}

		#endregion
	}

	public class Phrase
	{
		#region Public fields

		public readonly int Id;
		public readonly string String;

		#endregion

		#region Public constructor

		public Phrase(int id, string phrase)
		{
			Id = id;
			String = phrase;
		}

		#endregion

		#region public methods

		public override string ToString()
		{
			return String;
		}

		public override bool Equals(object obj)
		{
			Phrase ph = obj as Phrase;
			if (ph != null)
			{
				return string.Equals(String, ph.String) && Id == ph.Id;
			}
			return base.Equals(obj);
		}

		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		#endregion
	}
}
