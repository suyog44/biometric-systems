using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using Neurotec.Biometrics;
using Neurotec.Biometrics.Client;
using Neurotec.Biometrics.Gui;
using Neurotec.Images;

namespace Neurotec.Samples
{
	public partial class CapturePalmsPage : Neurotec.Samples.PageBase
	{
		#region Public constructor

		public CapturePalmsPage()
		{
			InitializeComponent();

			if (!DesignMode)
			{
				openFileDialog.Filter = NImages.GetOpenFileFilterString(true, true);
			}
		}

		#endregion

		#region Private fields

		private NSubject _subject;
		private NBiometricClient _biometricClient;
		private ManualResetEvent _isIdle = new ManualResetEvent(true);
		private NPalm _currentBiometric;
		private NSubject _newSubject;
		private int _sessionId = -1;
		private NPalm[] _nowCapturing;
		private string _titlePrefix = string.Empty;

		#endregion

		#region Public overrided methods

		public override void OnNavigatedTo(params object[] args)
		{
			if (args == null || args.Length != 2) throw new ArgumentException("args");

			_subject = (NSubject)args[0];
			_biometricClient = (NBiometricClient)args[1];
			if (_subject == null || _biometricClient == null) throw new ArgumentException("args");
			_biometricClient.PropertyChanged += OnBiometricClientPropertyChanged;

			_sessionId = _subject.Palms.Count > 0 ? _subject.Palms.Max(x => x.SessionId) + 1 : 0;
			_newSubject = new NSubject();
			palmsTree.Subject = _newSubject;
			lblStatus.Text = string.Empty;
			lblStatus.Visible = false;
			chbShowReturned.Visible = _biometricClient.PalmsReturnBinarizedImage;
			chbShowReturned.Checked = false;
			OnPalmScannerChanged();
			ToggleRadioButtons();

			generalizeProgressView.Clear();
			generalizeProgressView.Visible = false;

			base.OnNavigatedTo(args);
		}

		public override void OnNavigatingFrom()
		{
			if (IsBusy()) LongActionDialog.ShowDialog(this, "Finishing current action ... ", CancelAndWait);
			_biometricClient.PropertyChanged -= OnBiometricClientPropertyChanged;
			palmsTree.Subject = null;

			var palms = _newSubject.Palms.ToArray();
			_newSubject.Palms.Clear();
			foreach (var item in palms)
			{
				_subject.Palms.Add(item);
			}

			palmView.Finger = null;
			_newSubject = null;
			_subject = null;
			_biometricClient = null;

			base.OnNavigatingFrom();
		}

		#endregion

		#region Private form events

		private void BtnOpenClick(object sender, EventArgs e)
		{
			bool generalize = chbWithGeneralization.Checked;
			int sessionId = generalize ? _sessionId++ : -1;
			List<string> files = new List<string>();
			int count = generalize ? SettingsManager.PalmsGeneralizationRecordCount : 1;

			_nowCapturing = null;
			generalizeProgressView.Clear();
			generalizeProgressView.Visible = generalize;

			while (count > files.Count)
			{
				openFileDialog.Title = generalize ? string.Format("Open image ({0} of {1})", files.Count + 1, count) : "Open image";
				if (openFileDialog.ShowDialog() != DialogResult.OK) return;
				files.Add(openFileDialog.FileName);
			}

			_nowCapturing = new NPalm[count];
			for (int i = 0; i < count; i++)
			{
				NPalm palm = new NPalm
				{
					SessionId = sessionId,
					FileName = files[i],
					Position = palmSelector.SelectedPosition,
					ImpressionType = (NFImpressionType)cbImpression.SelectedItem
				};
				_newSubject.Palms.Add(palm);
				_nowCapturing[i] = palm;
			}

			NPalm first = _nowCapturing.First();
			if (generalize)
			{
				generalizeProgressView.Biometrics = _nowCapturing;
				generalizeProgressView.Selected = first;
			}
			palmView.Finger = first;
			palmsTree.UpdateTree();
			palmsTree.SelectedItem = palmsTree.GetBiometricNode(first);

			var biometricTask = _biometricClient.CreateTask(NBiometricOperations.CreateTemplate, _newSubject);
			SetIsBusy(true);
			_biometricClient.BeginPerformTask(biometricTask, OnCreateTemplateCompleted, null);
			SetStatusText(Color.Orange, "Extracting template. Please wait ...");
			EnableControls();
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
				if (status != NBiometricStatus.Ok)
				{
					foreach (var item in SubjectUtils.FlattenPalms(_nowCapturing))
					{
						_newSubject.Palms.Remove(item);
					}
				}
			}

			SetIsBusy(false);
			BeginInvoke(new Action<NBiometricStatus>(taskStatus =>
			{
				if (IsPageShown)
				{
					palmsTree.UpdateTree();
					generalizeProgressView.EnableMouseSelection = true;
					if (taskStatus != NBiometricStatus.Ok)
					{
						palmsTree.SelectedItem = null;
						palmView.Finger = _nowCapturing.FirstOrDefault();
						SetStatusText(Color.Red, "Extraction failed: {0}", taskStatus);
					}
					else
					{
						SetStatusText(Color.Green, "Extraction completed successfully");
						if (chbWithGeneralization.Checked)
						{
							var node = palmsTree.GetBiometricNode(_nowCapturing.First());
							if (node != null)
							{
								var generalized = node.GetAllGeneralized();
								generalizeProgressView.Generalized = generalized;
								generalizeProgressView.Selected = generalized.FirstOrDefault();
								generalizeProgressView.Visible = node.IsGeneralizedNode;
							}
						}
					}
					_nowCapturing = null;
					EnableControls();
				}
			}), status);
		}

		private void PalmSelectorFingerClick(object sender, FingerSelector.FingerClickArgs e)
		{
			palmSelector.SelectedPosition = e.Position;
			cbSelectedPosition.SelectedItem = e.Position;
		}

		private void RadioButtonCheckedChanged(object sender, EventArgs e)
		{
			if (((RadioButton)sender).Checked)
				ToggleRadioButtons();
		}

		private void CbSelectedPositionSelectedIndexChanged(object sender, EventArgs e)
		{
			object selected = cbSelectedPosition.SelectedItem;
			palmSelector.SelectedPosition = selected != null ? (NFPosition)selected : NFPosition.UnknownPalm;
		}

		private void BtnCaptureClick(object sender, EventArgs e)
		{
			bool generalize = chbWithGeneralization.Checked;
			int sessionId = generalize ? _sessionId++ : -1;
			int count = generalize ? SettingsManager.PalmsGeneralizationRecordCount : 1;
			bool manual = !chbCaptureAutomatically.Checked;

			_nowCapturing = null;
			generalizeProgressView.Clear();

			_nowCapturing = new NPalm[count];
			for (int i = 0; i < count; i++)
			{
				NPalm palm = new NPalm
				{
					CaptureOptions = manual ? NBiometricCaptureOptions.Manual : NBiometricCaptureOptions.None,
					SessionId = sessionId,
					Position = palmSelector.SelectedPosition,
					ImpressionType = (NFImpressionType)cbImpression.SelectedItem
				};
				_nowCapturing[i] = palm;
				_newSubject.Palms.Add(palm);
			}
			palmsTree.UpdateTree();
			palmsTree.SelectedItem = palmsTree.GetBiometricNode(_nowCapturing.First());
			SetStatusText(Color.Orange, "Starting capturing from scanner ...");
			var biometricTask = _biometricClient.CreateTask(NBiometricOperations.Capture | NBiometricOperations.CreateTemplate, _newSubject);
			SetIsBusy(true);
			_biometricClient.BeginPerformTask(biometricTask, OnCaptureCompleted, null);
			EnableControls();
		}

		private void BtnCancelClick(object sender, EventArgs e)
		{
			_biometricClient.Cancel();
			btnCancel.Enabled = false;
		}

		private void OnCaptureCompleted(IAsyncResult r)
		{
			NBiometricStatus status = NBiometricStatus.InternalError;
			try
			{
				NBiometricTask task = _biometricClient.EndPerformTask(r);
				status = task.Status;
				if (task.Error != null) Utilities.ShowError(task.Error);
			}
			catch (Exception ex)
			{
				Utilities.ShowError(ex);
			}
			finally
			{
				if (status != NBiometricStatus.Ok)
				{
					foreach (var item in SubjectUtils.FlattenPalms(_nowCapturing))
					{
						_newSubject.Palms.Remove(item);
					}
				}
			}

			SetIsBusy(false);
			BeginInvoke(new Action<NBiometricStatus>(taskStatus =>
			{
				if (IsPageShown)
				{
					generalizeProgressView.EnableMouseSelection = true;
					if (taskStatus == NBiometricStatus.Ok)
					{
						SetStatusText(Color.Green, "Palms captured successfully");
					}
					else
					{
						if (taskStatus == NBiometricStatus.Canceled)
							lblStatus.Visible = false;
						else
							SetStatusText(Color.Red, "Extraction completed successfully: {0}", taskStatus);
					}

					palmsTree.UpdateTree();
					var selected = palmsTree.SelectedItem;
					if (selected != null && selected.IsGeneralizedNode)
					{
						var generalized = selected.GetAllGeneralized();
						generalizeProgressView.Biometrics = selected.Items;
						generalizeProgressView.Generalized = generalized;
						generalizeProgressView.Selected = generalized.FirstOrDefault() ?? selected.Items.First();
					}
					else
					{
						generalizeProgressView.Clear();
						generalizeProgressView.Visible = false;
						if (selected != null)
							palmView.Finger = (NPalm)selected.Items.First();
					}
					_nowCapturing = null;
					EnableControls();
				}
			}), status);
		}

		private void PalmTreePropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			if (e.PropertyName == "SelectedItem")
			{
				BeginInvoke(new MethodInvoker(() =>
					{
						var selection = palmsTree.SelectedItem;
						NPalm first = selection != null ? (NPalm)selection.Items.First() : null;
						generalizeProgressView.Clear();
						if (selection != null && selection.IsGeneralizedNode)
						{
							var generalized = selection.GetAllGeneralized();
							generalizeProgressView.Biometrics = selection.Items;
							generalizeProgressView.Generalized = generalized;
							generalizeProgressView.Selected = generalized.FirstOrDefault() ?? first;
							generalizeProgressView.Visible = true;
						}
						else
							generalizeProgressView.Visible = false;
						palmView.Finger = first;
						chbShowReturned.Enabled = first != null && first.BinarizedImage != null;
						lblStatus.Visible = lblStatus.Visible && IsBusy();
						if (chbShowReturned.Checked && (first == null || first.BinarizedImage == null))
							chbShowReturned.Checked = false;
					}));
			}
		}

		private void BtnFinishClick(object sender, EventArgs e)
		{
			PageController.NavigateToStartPage();
		}

		#endregion

		#region Private methods

		private void SetStatusText(Color backColor, string format, params object[] args)
		{
			lblStatus.BackColor = backColor;
			lblStatus.Text = string.Format(format, args);
			lblStatus.Visible = true;
		}

		private void ToggleRadioButtons()
		{
			cbImpression.Items.Clear();
			palmSelector.SelectedPosition = NFPosition.UnknownPalm;
			palmSelector.AllowedPositions = null;
			cbSelectedPosition.Items.Clear();
			if (rbFile.Checked)
			{
				NFImpressionType[] values = (NFImpressionType[])Enum.GetValues(typeof(NFImpressionType));
				foreach (NFImpressionType item in values.Where(x => NBiometricTypes.IsImpressionTypePalm(x)))
				{
					cbImpression.Items.Add(item);
				}
				cbImpression.SelectedIndex = 0;
				cbImpression.Enabled = true;

				NFPosition[] allowedPositions = new NFPosition[]
				{
					NFPosition.LeftFullPalm, NFPosition.RightFullPalm,
					NFPosition.LeftUpperPalm, NFPosition.RightUpperPalm,
					NFPosition.LeftInterdigital, NFPosition.RightInterdigital,
					NFPosition.LeftHypothenar, NFPosition.RightHypothenar,
					NFPosition.LeftLowerPalm, NFPosition.RightLowerPalm,
					NFPosition.LeftThenar, NFPosition.RightThenar,
				};

				palmSelector.AllowedPositions = allowedPositions;
				foreach (var item in allowedPositions)
				{
					cbSelectedPosition.Items.Add(item);
				}
				cbSelectedPosition.SelectedIndex = 0;
			}
			else
			{
				var device = _biometricClient.PalmScanner;
				cbImpression.Items.Add(device.GetSupportedImpressionTypes().First(x => NBiometricTypes.IsImpressionTypePalm(x)));
				cbImpression.SelectedIndex = 0;

				var supportedPositions = device.GetSupportedPositions().Where(x => NBiometricTypes.IsPositionPalm(x)).ToArray();
				palmSelector.AllowedPositions = supportedPositions;
				foreach (var item in supportedPositions)
				{
					cbSelectedPosition.Items.Add(item);
				}
			}
			cbSelectedPosition.SelectedIndex = 0;
			EnableControls();
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

		private void EnableControls()
		{
			bool isIdle = !IsBusy();
			rbScanner.Enabled = _biometricClient.PalmScanner != null && _biometricClient.PalmScanner.IsAvailable;
			gbSource.Enabled = isIdle;
			gbOptions.Enabled = isIdle;
			palmSelector.AllowHighlight = isIdle;
			cbSelectedPosition.Enabled = isIdle;
			palmsTree.Enabled = isIdle;
			btnCapture.Enabled = isIdle || rbScanner.Checked;
			btnCapture.Visible = rbScanner.Checked && isIdle;
			btnCancel.Visible = rbScanner.Checked;
			btnCancel.Enabled = !isIdle;
			btnOpen.Enabled = isIdle && rbFile.Checked;
			btnOpen.Visible = rbFile.Checked;
			palmsTree.Enabled = isIdle;
			chbShowReturned.Enabled = isIdle;
			chbCaptureAutomatically.Enabled = isIdle && rbScanner.Checked;
			btnForce.Visible = rbScanner.Checked;
			btnForce.Enabled = rbScanner.Checked && !isIdle;
			busyIndicator.Visible = !isIdle;
		}

		private void OnPalmPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			if (e.PropertyName == "Status")
				BeginInvoke(new Action<NBiometricStatus>(status =>
				{
					Color backColor = status == NBiometricStatus.Ok || status == NBiometricStatus.None ? Color.Green : Color.Red;
					SetStatusText(backColor, "{0}Status: {1}", _titlePrefix, status);
				}), _currentBiometric.Status);
		}

		private void ChbShowReturnedCheckedChanged(object sender, EventArgs e)
		{
			palmView.ShownImage = chbShowReturned.Checked ? ShownImage.Result : ShownImage.Original;
		}

		private void OnPalmScannerChanged()
		{
			var device = _biometricClient.PalmScanner;
			if (device == null || !device.IsAvailable)
			{
				rbFile.Checked = true;
				rbScanner.Text = "Scanner (Not connected)";
			}
			else
			{
				rbScanner.Text = string.Format("Scanner ({0})", device.DisplayName);
			}
			EnableControls();
		}

		private void OnBiometricClientPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			if (e.PropertyName == "PalmScanner")
			{
				BeginInvoke(new Action(() =>
				{
					if (IsPageShown) OnPalmScannerChanged();
				}));
			}
			else if (e.PropertyName == "CurrentBiometric")
			{
				BeginInvoke(new Action<NPalm>(current =>
				{
					if (_currentBiometric != null)
					{
						_currentBiometric.PropertyChanged -= OnPalmPropertyChanged;
						_currentBiometric = null;
					}

					if (current != null)
					{
						bool withGeneralization = chbWithGeneralization.Checked;
						var node = palmsTree.GetBiometricNode(current);
						if (withGeneralization)
						{
							generalizeProgressView.Biometrics = node.Items;
							generalizeProgressView.Generalized = node.GetAllGeneralized();
							generalizeProgressView.Selected = current;
							_titlePrefix = string.Format("Capturing {0} ({1} of {2}). ", current.Position, Array.IndexOf(node.Items, current) + 1, SettingsManager.PalmsGeneralizationRecordCount);
						}
						palmView.Finger = current;

						_currentBiometric = current;
						_currentBiometric.PropertyChanged += OnPalmPropertyChanged;
					}
					else
					{
						generalizeProgressView.Clear();
						lblStatus.Visible = false;
						_titlePrefix = string.Empty;
					}
				}), _biometricClient.CurrentBiometric);
			}
		}

		private void BtnForceClick(object sender, EventArgs e)
		{
			_biometricClient.Force();
		}

		#endregion

	}
}
