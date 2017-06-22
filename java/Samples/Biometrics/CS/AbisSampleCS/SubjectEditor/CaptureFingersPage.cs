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
using Neurotec.Devices;
using Neurotec.Images;
using Neurotec.Samples.TenPrintCard;

namespace Neurotec.Samples
{
	public partial class CaptureFingersPage : Neurotec.Samples.PageBase
	{
		#region Private types

		private class Scenario
		{
			#region Public static readonly fields

			public static readonly Scenario UnknownPlainFinger;
			public static readonly Scenario UnknownRolledFinger;
			public static readonly Scenario AllPlainFingers;
			public static readonly Scenario AllRolledFingers;
			public static readonly Scenario Slaps;
			public static readonly Scenario SlapsSeparateThumbs;
			public static readonly Scenario RolledPlusSlaps;
			public static readonly Scenario RolledPlusSlapsSeparateThumbs;

			#endregion

			#region Private types

			private struct Tuple
			{
				public NFPosition Position { get; set; }
				public NFImpressionType ImpressionType { get; set; }
			}

			#endregion

			#region Private fields

			private static readonly Scenario[] _scenarios;

			#endregion

			#region Private constructor

			private Scenario(string name)
			{
				this.Name = name;
			}

			#endregion

			#region Static constructor

			static Scenario()
			{
				NFPosition[] plainFingers = new[]
					{
						NFPosition.LeftLittle, NFPosition.LeftRing, NFPosition.LeftMiddle, NFPosition.LeftIndex, NFPosition.LeftThumb,
						NFPosition.RightThumb, NFPosition.RightIndex, NFPosition.RightMiddle, NFPosition.RightRing, NFPosition.RightLittle,
					};
				NFPosition[] slaps = new[] { NFPosition.PlainLeftFourFingers, NFPosition.PlainRightFourFingers, NFPosition.PlainThumbs };
				NFPosition[] slapsSeparatteThumbs = new[] { NFPosition.PlainLeftFourFingers, NFPosition.PlainRightFourFingers, NFPosition.PlainLeftThumb, NFPosition.PlainRightThumb };
				UnknownPlainFinger = new Scenario("Unknown plain finger")
				{
					Items = new[] { new Tuple() },
					IsUnknownFingers = true
				};
				UnknownRolledFinger = new Scenario("Unknown rolled finger")
				{
					Items = new[] { new Tuple { ImpressionType = NFImpressionType.LiveScanRolled } },
					HasRolled = true,
					IsUnknownFingers = true
				};
				AllPlainFingers = new Scenario("All plain fingers") { Items = plainFingers.Select(x => new Tuple { Position = x }) };
				AllRolledFingers = new Scenario("All rolled fingers")
				{
					Items = plainFingers.Select(x => new Tuple { Position = x, ImpressionType = NFImpressionType.LiveScanRolled }),
					HasRolled = true,
				};
				Slaps = new Scenario("4-4-2")
				{
					Items = slaps.Select(x => new Tuple { Position = x }),
					HasSlaps = true
				};
				SlapsSeparateThumbs = new Scenario("4-4-1-1")
				{
					Items = slapsSeparatteThumbs.Select(x => new Tuple { Position = x }),
					HasSlaps = true
				};
				RolledPlusSlaps = new Scenario("Rolled fingers + 4-4-2")
				{
					Items = Enumerable.Union(AllRolledFingers.Items, Slaps.Items),
					HasSlaps = true,
					HasRolled = true
				};
				RolledPlusSlapsSeparateThumbs = new Scenario("Rolled fingers + 4-4-1-1")
				{
					Items = Enumerable.Union(AllRolledFingers.Items, SlapsSeparateThumbs.Items),
					HasSlaps = true,
					HasRolled = true
				};

				_scenarios = new[]
				{
					UnknownPlainFinger,
					UnknownRolledFinger,
					AllPlainFingers,
					AllRolledFingers,
					Slaps,
					SlapsSeparateThumbs,
					RolledPlusSlaps,
					RolledPlusSlapsSeparateThumbs,
				};
			}

			#endregion

			#region Public static methods

			public static IEnumerable<Scenario> GetAvailableScenarios()
			{
				return _scenarios;
			}

			#endregion

			#region Private properties

			private IEnumerable<Tuple> Items { get; set; }

			#endregion

			#region Public properties

			public bool HasRolled { get; private set; }
			public bool HasSlaps { get; private set; }
			public string Name { get; private set; }
			public bool IsUnknownFingers { get; private set; }

			#endregion

			#region Public methods

			public override string ToString()
			{
				return Name;
			}

			public IEnumerable<NFPosition> GetPositions()
			{
				return Items.Select(x => x.Position).Distinct();
			}

			public IEnumerable<NFinger> GetFingers(int sessionId, int generalizationCount)
			{
				foreach (var item in Items)
				{
					for (int i = 0; i < generalizationCount; i++)
					{
						yield return new NFinger { Position = item.Position, ImpressionType = item.ImpressionType, SessionId = sessionId };
					}
				}
			}

			public IEnumerable<NFinger> GetFingers()
			{
				return GetFingers(-1, 1);
			}

			#endregion
		}

		#endregion

		#region Public constructor

		public CaptureFingersPage()
		{
			InitializeComponent();

			if (!DesignMode)
			{
				openFileDialog.Filter = NImages.GetOpenFileFilterString(true, true);
			}
		}

		#endregion

		#region Private fields

		private NBiometricClient _biometricClient;
		private NSubject _subject;
		private ManualResetEvent _isIdle = new ManualResetEvent(true);
		private NSubject _newSubject;
		private NFinger _currentBiometric = null;
		private bool _captureNeedsAction = false;
		private int _sessionId = -1;
		private NFinger[] _nowCapturing = null;
		private string _titlePrefix = string.Empty;

		#endregion

		#region Public methods

		public override void OnNavigatedTo(params object[] args)
		{
			if (args == null || args.Length != 2) throw new ArgumentException("args");

			_subject = (NSubject)args[0];
			_biometricClient = (NBiometricClient)args[1];
			if (_subject == null || _biometricClient == null) throw new ArgumentException("args");
			_sessionId = _subject.Fingers.Count > 0 ? _subject.Fingers.Max(x => x.SessionId) + 1 : 0;

			_biometricClient.CurrentBiometricCompletedTimeout = -1;
			_biometricClient.CurrentBiometricCompleted += BiometricClientCurrentBiometricCompleted;
			_biometricClient.PropertyChanged += OnBiometricClientPropertyChanged;

			_newSubject = new NSubject();
			CopyMissingFingerPositions(_newSubject, _subject);
			fingersTree.Subject = _newSubject;

			fingerSelector.MissingPositions = _newSubject.MissingFingers.ToArray();
			lblStatus.Text = string.Empty;
			lblStatus.Visible = false;
			chbShowReturned.Visible = _biometricClient.FingersReturnBinarizedImage;
			chbShowReturned.Checked = false;
			generalizeProgressView.Visible = false;
			generalizeProgressView.PropertyChanged += OnGeneralizeProgressViewPropertyChanged;

			OnFingerScannerChanged();
			OnRadioButtonToggle();

			base.OnNavigatedTo(args);
		}

		public override void OnNavigatingFrom()
		{
			if (IsBusy()) LongActionDialog.ShowDialog(this, "Finish current action ...", CancelAndWait);

			_nowCapturing = null;
			generalizeProgressView.PropertyChanged -= OnGeneralizeProgressViewPropertyChanged;
			_biometricClient.PropertyChanged -= OnBiometricClientPropertyChanged;
			_biometricClient.CurrentBiometricCompleted -= BiometricClientCurrentBiometricCompleted;
			_biometricClient.CurrentBiometricCompletedTimeout = 0;
			var fingers = _newSubject.Fingers.ToArray();
			_newSubject.Fingers.Clear();
			foreach (var item in fingers)
			{
				_subject.Fingers.Add(item);
			}
			CopyMissingFingerPositions(_subject, _newSubject);

			_newSubject = null;
			fingersTree.Subject = null;
			fingerView.Finger = null;
			_biometricClient = null;

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

		private void UpdateShowReturned()
		{
			NFrictionRidge ridge = fingerView.Finger;
			chbShowReturned.Enabled = ridge != null && ridge.BinarizedImage != null;
			if (!chbShowReturned.Enabled && chbShowReturned.Checked)
				chbShowReturned.Checked = false;
		}

		private void CopyMissingFingerPositions(NSubject dst, NSubject src)
		{
			dst.MissingFingers.Clear();
			foreach (var item in src.MissingFingers)
			{
				dst.MissingFingers.Add(item);
			}
		}

		private void EnableControls()
		{
			bool fromFile = rbFiles.Checked;
			bool isBusy = IsBusy();
			bool isTenPrint = rbTenPrintCard.Checked;

			btnStart.Visible = !fromFile;
			btnOpenImage.Enabled = fromFile && !isBusy;
			Scenario selected = (Scenario)cbScenario.SelectedItem ?? Scenario.UnknownPlainFinger;
			btnSkip.Enabled = !fromFile && !selected.IsUnknownFingers && isBusy;
			btnRepeat.Enabled = !fromFile && isBusy;
			btnStart.Enabled = !fromFile && !isBusy;
			btnCancel.Enabled = !fromFile && isBusy;
			fingerSelector.AllowHighlight = !isBusy && !isTenPrint;
			rbScanner.Enabled = _biometricClient != null && _biometricClient.FingerScanner != null;
			cbScenario.Enabled = !isBusy;
			cbImpression.Enabled = !isBusy && fromFile;
			fingersTree.Enabled = !isBusy;
			panelNavigations.Visible = !fromFile && !isTenPrint;
			btnOpenImage.Visible = fromFile;
			chbCaptureAutomatically.Enabled = !fromFile && !isTenPrint && !isBusy;
			chbWithGeneralization.Enabled = !isTenPrint && !isBusy;
			btnForce.Visible = !fromFile;
			btnForce.Enabled = isBusy && !chbCaptureAutomatically.Checked;
			gbSource.Enabled = !isBusy;
			chbShowReturned.Enabled = chbShowReturned.Enabled && !isBusy;

			busyIndicator.Visible = isBusy;
		}

		private void ListSupportedScenarios()
		{
			try
			{
				cbScenario.BeginUpdate();

				var scenario = cbScenario.SelectedItem;
				var supportedScenarios = Scenario.GetAvailableScenarios();
				if (rbTenPrintCard.Checked)
				{
					supportedScenarios = new Scenario[] { Scenario.RolledPlusSlapsSeparateThumbs };
				}
				else if (!rbFiles.Checked)
				{
					NFScanner scanner = _biometricClient.FingerScanner;
					var impressions = scanner.GetSupportedImpressionTypes();
					var positions = scanner.GetSupportedPositions();
					bool supportsRolled = Array.Exists(impressions, x => NBiometricTypes.IsImpressionTypeRolled(x));
					bool supportsSlaps = Array.Exists(positions, x => NBiometricTypes.IsPositionFourFingers(x));

					if (!supportsRolled) supportedScenarios = supportedScenarios.Where(x => !x.HasRolled);
					if (!supportsSlaps) supportedScenarios = supportedScenarios.Where(x => !x.HasSlaps);
				}

				cbScenario.Items.Clear();
				foreach (var item in supportedScenarios)
				{
					cbScenario.Items.Add(item);
				}

				cbScenario.SelectedItem = scenario;
			}
			finally
			{
				cbScenario.EndUpdate();
			}
		}

		private void OnRadioButtonToggle()
		{
			ListSupportedScenarios();
			if (cbScenario.SelectedIndex == -1)
			{
				cbScenario.SelectedIndex = 0;
			}
			EnableControls();
			ShowHint();
		}

		private void ShowHint()
		{
			lblNote.Visible = fingerSelector.Visible && !rbTenPrintCard.Checked;
			if (rbFiles.Checked)
			{
				lblNote.Text = "Hint: Click on finger to select it or mark as missing";
			}
			else
			{
				lblNote.Text = "Hint: Click on finger to mark it as missing";
			}
		}

		private void UpdateImpressionTypes(NFPosition position, bool isRolled)
		{
			try
			{
				var impressions = rbScanner.Checked ? _biometricClient.FingerScanner.GetSupportedImpressionTypes() : (NFImpressionType[])Enum.GetValues(typeof(NFImpressionType));
				var valid = impressions.Where(x => NBiometricTypes.IsImpressionTypeRolled(x) == isRolled && NBiometricTypes.IsPositionCompatibleWith(position, x)).Distinct();
				cbImpression.BeginUpdate();
				cbImpression.Items.Clear();
				foreach (var item in valid)
				{
					cbImpression.Items.Add(item);
				}
				if (cbImpression.Items.Count > 0)
					cbImpression.SelectedIndex = 0;
			}
			finally
			{
				cbImpression.EndUpdate();
			}
		}

		private void OnSelectedPositionChanged(Scenario sc, NFPosition position)
		{
			bool isRolled = sc.HasRolled && NBiometricTypes.IsPositionTheFinger(position);
			OnSelectedPositionChanged(sc, position, isRolled);
		}

		private void OnSelectedPositionChanged(Scenario sc, NFPosition position, bool isRolled)
		{
			if (position == NFPosition.Unknown && rbFiles.Checked)
			{
				position = sc.GetPositions().First();
				isRolled = sc.HasRolled && NBiometricTypes.IsPositionTheFinger(position);
			}
			fingerSelector.SelectedPosition = position;
			fingerSelector.IsRolled = isRolled;
			UpdateImpressionTypes(position, isRolled && sc.HasRolled);
		}

		private void SetLabelText(Label lbl, Color backColor, string text)
		{
			lbl.Text = text;
			lbl.BackColor = backColor;
			lbl.Visible = true;
		}

		private void SetStatusText(Color backColor, string format, params object[] args)
		{
			SetLabelText(lblStatus, backColor, string.Format(format, args));
		}

		private void OnFingerScannerChanged()
		{
			var device = _biometricClient.FingerScanner;
			if (device == null || !device.IsAvailable)
			{
				if (rbScanner.Checked) rbFiles.Checked = true;
				rbScanner.Text = "Scanner (Not connected)";
			}
			else
			{
				rbScanner.Text = string.Format("Scanner ({0})", device.DisplayName);
			}
			EnableControls();
		}

		#endregion

		#region Private events

		private void OnGeneralizeProgressViewPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			if (e.PropertyName == "Selected")
			{
				UpdateShowReturned();
			}
		}

		private void BiometricClientCurrentBiometricCompleted(object sender, EventArgs e)
		{
			NBiometricStatus status = _biometricClient.CurrentBiometric.Status;
			if (status == NBiometricStatus.Ok || status == NBiometricStatus.SpoofDetected || status == NBiometricStatus.SourceError || status == NBiometricStatus.CaptureError)
				_biometricClient.Force();
			else
			{
				BeginInvoke(new Action(() =>
					{
						_captureNeedsAction = true;
						SetStatusText(Color.Red, "Capturing failed: {0}. Trying again ...", status);
					}));
				Action repeat = () =>
					{
						if (IsPageShown && _captureNeedsAction)
						{
							_biometricClient.Repeat();
							_captureNeedsAction = false;
						}
					};
				var delayedInvoke = new System.Threading.Timer(obj =>
					{
						if (IsHandleCreated) BeginInvoke(repeat);
					}, null, 3000, Timeout.Infinite);
			}
		}

		private void CbScenarioSelectedIndexChanged(object sender, EventArgs e)
		{
			Scenario selected = (Scenario)cbScenario.SelectedItem;
			fingerSelector.SelectedPosition = NFPosition.Unknown;
			fingerSelector.IsRolled = false;
			fingerSelector.Visible = !selected.IsUnknownFingers;
			fingerSelector.AllowedPositions = selected.GetPositions().ToArray();
			if (rbFiles.Checked)
			{
				NFPosition position = fingerSelector.AllowedPositions.FirstOrDefault();
				OnSelectedPositionChanged(selected, position);
			}
			else
			{
				UpdateImpressionTypes(NFPosition.Unknown, false);
			}
			ShowHint();
			EnableControls();
		}

		private void BtnStartClick(object sender, EventArgs e)
		{
			if (rbScanner.Checked)
			{
				bool generalize = chbWithGeneralization.Checked;
				int sessionId = generalize ? _sessionId++ : -1;
				int count = generalize ? SettingsManager.FingersGeneralizationRecordCount : 1;
				bool manual = !chbCaptureAutomatically.Checked;

				_nowCapturing = null;
				generalizeProgressView.Clear();
				generalizeProgressView.EnableMouseSelection = false;

				Scenario sc = (Scenario)cbScenario.SelectedItem;
				_nowCapturing = sc.GetFingers(sessionId, count).Where(x => !_newSubject.MissingFingers.Contains(x.Position)).ToArray();
				foreach (var finger in _nowCapturing)
				{
					if (manual) finger.CaptureOptions = NBiometricCaptureOptions.Manual;
					_newSubject.Fingers.Add(finger);
				}

				NBiometricOperations operations = NBiometricOperations.Capture | NBiometricOperations.CreateTemplate;
				if (_biometricClient.FingersCalculateNfiq) operations |= NBiometricOperations.AssessQuality;
				var biometricTask = _biometricClient.CreateTask(operations, _newSubject);
				SetIsBusy(true);
				_biometricClient.BeginPerformTask(biometricTask, OnCaptureCompleted, null);
				SetStatusText(Color.Orange, "Starting capturing from scanner ...");
				EnableControls();
			}
			else if (rbTenPrintCard.Checked)
			{
				using (var dialog = new TenPrintCardForm())
				{
					dialog.BiometricClient = _biometricClient;
					if (dialog.ShowDialog() == DialogResult.OK)
					{
						NSubject subject = dialog.Result;
						if (subject != null)
						{
							NFinger[] fingers = subject.Fingers.ToArray();
							subject.Fingers.Clear();

							foreach (var item in fingers)
							{
								_newSubject.Fingers.Add(item);
							}
						}
					}
				}
			}
		}

		private void OnCaptureCompleted(IAsyncResult r)
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
					// Remove fingers where failed
					var grouped = SubjectUtils.GetFingersGeneralizationGroups(_nowCapturing).ToArray();
					var failedGroups = grouped.Where(g => g.FirstOrDefault(x => x.Status != NBiometricStatus.Ok) != null);
					foreach (var group in failedGroups)
					{
						foreach (var item in SubjectUtils.FlattenFingers(group))
						{
							_newSubject.Fingers.Remove(item);
						}
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
							SetStatusText(Color.Green, "Fingers captured successfully");
						}
						else
						{
							if (taskStatus == NBiometricStatus.Canceled)
								lblStatus.Visible = false;
							else
								SetStatusText(Color.Red, "Capture failed: {0}", taskStatus);
							fingerSelector.SelectedPosition = NFPosition.Unknown;
						}

						fingersTree.UpdateTree();
						var selected = fingersTree.SelectedItem;
						if (selected != null && selected.IsGeneralizedNode)
						{
							var generalized = selected.GetAllGeneralized();
							generalizeProgressView.Biometrics = selected.Items;
							generalizeProgressView.Generalized = generalized;
							generalizeProgressView.Selected = generalized.FirstOrDefault() ?? selected.Items.First();
							generalizeProgressView.Visible = true;
						}
						else
						{
							generalizeProgressView.Clear();
							generalizeProgressView.Visible = false;
							if (selected != null)
								fingerView.Finger = (NFinger)selected.Items.First();
						}
						_nowCapturing = null;
						UpdateShowReturned();
						EnableControls();
					}
				}), status);
		}

		private void OnBiometricClientPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			if (e.PropertyName == "CurrentBiometric")
			{
				BeginInvoke(new Action<NFinger>(current =>
				{
					if (_currentBiometric != null)
					{
						_currentBiometric.PropertyChanged -= CurrentBiometricPropertyChanged;
						_currentBiometric = null;
					}

					if (current != null)
					{
						bool withGeneralization = chbWithGeneralization.Checked;
						var node = fingersTree.GetBiometricNode(current);
						OnSelectedPositionChanged((Scenario)cbScenario.SelectedItem, current.Position);
						fingersTree.SelectedItem = node;
						if (withGeneralization)
						{
							generalizeProgressView.Biometrics = node.Items;
							generalizeProgressView.Generalized = node.GetAllGeneralized();
							generalizeProgressView.Selected = current;
							generalizeProgressView.Visible = true;
							_titlePrefix = string.Format("Capturing {0} ({1} of {2}). ", current.Position, Array.IndexOf(node.Items, current) + 1, SettingsManager.FingersGeneralizationRecordCount);
						}
						fingerView.Finger = current;

						_currentBiometric = current;
						_currentBiometric.PropertyChanged += CurrentBiometricPropertyChanged;
					}
					else
					{
						generalizeProgressView.Clear();
						lblStatus.Visible = false;
						_titlePrefix = string.Empty;
					}
				}), _biometricClient.CurrentBiometric);
			}
			else if (e.PropertyName == "FingerScanner")
			{
				BeginInvoke(new Action(() =>
					{
						if (IsPageShown) OnFingerScannerChanged();
					}));
			}
		}

		private void CurrentBiometricPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			if (e.PropertyName == "Status")
			{
				BeginInvoke(new Action<NBiometricStatus>(status =>
				{
					Color backColor = status == NBiometricStatus.Ok || status == NBiometricStatus.None ? Color.Green : Color.Red;
					SetStatusText(backColor, "{0}Status: {1}", _titlePrefix, status);
				}), _currentBiometric.Status);
			}
		}

		private void BtnOpenImageClick(object sender, EventArgs e)
		{
			bool generalize = chbWithGeneralization.Checked;
			int sessionId = generalize ? _sessionId++ : -1;
			List<string> files = new List<string>();
			int count = generalize ? SettingsManager.FingersGeneralizationRecordCount : 1;

			_nowCapturing = null;
			generalizeProgressView.Clear();

			while (count > files.Count)
			{
				openFileDialog.Title = generalize ? string.Format("Open image ({0} of {1})", files.Count + 1, count) : "Open image";
				if (openFileDialog.ShowDialog() != DialogResult.OK) return;
				files.Add(openFileDialog.FileName);
			}

			_nowCapturing = new NFinger[count];
			for (int i = 0; i < count; i++)
			{
				NFinger finger = new NFinger
				{
					SessionId = sessionId,
					FileName = files[i],
					Position = fingerSelector.SelectedPosition,
					ImpressionType = (NFImpressionType)cbImpression.SelectedItem
				};
				_newSubject.Fingers.Add(finger);
				_nowCapturing[i] = finger;
			}

			NFinger first = _nowCapturing.First();
			if (generalize)
			{
				generalizeProgressView.Biometrics = _nowCapturing;
				generalizeProgressView.Selected = first;
			}
			fingerView.Finger = first;
			fingersTree.UpdateTree();
			fingersTree.SelectedItem = fingersTree.GetBiometricNode(first);

			NBiometricOperations operations = NBiometricOperations.CreateTemplate;
			if (_biometricClient.FingersCalculateNfiq) operations |= NBiometricOperations.AssessQuality;
			var biometricTask = _biometricClient.CreateTask(operations, _newSubject);
			SetIsBusy(true);
			_biometricClient.BeginPerformTask(biometricTask, OnCreateTemplateCompleted, null);
			SetStatusText(Color.Orange, "Extracting template. Please wait ...");
			EnableControls();
		}

		private void OnCreateTemplateCompleted(IAsyncResult result)
		{
			NBiometricStatus status = NBiometricStatus.InternalError;
			try
			{
				var biometricTask = _biometricClient.EndPerformTask(result);
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
					var allItems = SubjectUtils.FlattenFingers(_nowCapturing);
					foreach (var item in allItems)
					{
						_newSubject.Fingers.Remove(item);
					}
				}
			}

			SetIsBusy(false);
			BeginInvoke(new Action<NBiometricStatus>(taskStatus =>
			{
				if (IsPageShown)
				{
					fingersTree.UpdateTree();
					generalizeProgressView.EnableMouseSelection = true;
					if (taskStatus != NBiometricStatus.Ok)
					{
						fingersTree.SelectedItem = null;
						fingerView.Finger = _nowCapturing.FirstOrDefault();
						SetStatusText(Color.Red, "Extraction failed: {0}", taskStatus);
					}
					else
					{
						SetStatusText(Color.Green, "Finger extraction completed successfully");
						if (chbWithGeneralization.Checked)
						{
							var node = fingersTree.GetBiometricNode(_nowCapturing.First());
							if (node != null)
							{
								var generalized = node.GetAllGeneralized();
								generalizeProgressView.Generalized = generalized;
								generalizeProgressView.Selected = generalized.FirstOrDefault();
							}
						}
					}
					_nowCapturing = null;
					EnableControls();
					UpdateShowReturned();
				}
			}), status);
		}

		private void RadioButtonCheckedChanged(object sender, EventArgs e)
		{
			if (((RadioButton)sender).Checked)
				OnRadioButtonToggle();
		}

		private void BtnCancelClick(object sender, EventArgs e)
		{
			_captureNeedsAction = false;
			_biometricClient.Cancel();
			btnCancel.Enabled = false;
		}

		private void FingerSelectorFingerClick(object sender, FingerSelector.FingerClickArgs e)
		{
			if (rbFiles.Checked)
			{
				NFPosition position = e.Position;
				NFPosition part = e.PositionPart == NFPosition.Unknown ? position : e.PositionPart;

				bool isMissing = _newSubject.MissingFingers.Contains(position) || _newSubject.MissingFingers.Contains(part);
				bool isSelected = fingerSelector.SelectedPosition == position;

				tsmiSelect.Text = string.Format("Select {0}", position);
				tsmiSelect.Tag = position;
				tsmiMissing.Text = string.Format("Mark {0} as {1}", part, isMissing ? "not missing" : "missing");
				tsmiMissing.Tag = part;
				var location = fingerSelector.PointToScreen(e.Location);
				contextMenuStrip.Show(location);
			}
			else
			{
				NFPosition position = e.Position;
				if (!NBiometricTypes.IsPositionSingleFinger(position))
					position = e.PositionPart;

				bool isMissing = _newSubject.MissingFingers.Contains(position);
				if (isMissing)
					_newSubject.MissingFingers.Remove(position);
				else
					_newSubject.MissingFingers.Add(position);
				fingerSelector.MissingPositions = _newSubject.MissingFingers.ToArray();
			}
		}

		private void ContextMenuStripItemClicked(object sender, ToolStripItemClickedEventArgs e)
		{
			NFPosition position = (NFPosition)e.ClickedItem.Tag;
			if (e.ClickedItem == tsmiMissing)
			{
				bool isMissing = _newSubject.MissingFingers.Contains(position);
				if (isMissing)
					_newSubject.MissingFingers.Remove(position);
				else
					_newSubject.MissingFingers.Add(position);
				fingerSelector.MissingPositions = _newSubject.MissingFingers.ToArray();
			}
			else if (e.ClickedItem == tsmiSelect)
			{
				OnSelectedPositionChanged((Scenario)cbScenario.SelectedItem, position);
			}
		}

		private void FingersTreePropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			if (e.PropertyName == "SelectedItem")
			{
				BeginInvoke(new Action(() =>
				{
					var selection = fingersTree.SelectedItem;
					NFinger first = selection != null ? (NFinger)selection.Items.FirstOrDefault() : null;
					NFPosition position = first != null ? first.Position : NFPosition.Unknown;
					NFImpressionType impression = first != null ? first.ImpressionType : NFImpressionType.LiveScanPlain;
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
					
					fingerView.Finger = first;
					OnSelectedPositionChanged((Scenario)cbScenario.SelectedItem, position, NBiometricTypes.IsImpressionTypeRolled(impression));
					lblStatus.Visible = lblStatus.Visible && IsBusy();
					UpdateShowReturned();
				}));
			}
		}

		private void BtnRepeatClick(object sender, EventArgs e)
		{
			_captureNeedsAction = false;
			_biometricClient.Repeat();
		}

		private void BtnSkipClick(object sender, EventArgs e)
		{
			_captureNeedsAction = false;
			_biometricClient.Skip();
		}

		private void BntFinishClick(object sender, EventArgs e)
		{
			PageController.NavigateToStartPage();
		}

		private void ChbShowReturnedCheckedChanged(object sender, EventArgs e)
		{
			fingerView.ShownImage = chbShowReturned.Checked ? ShownImage.Result : ShownImage.Original;
		}

		private void BtnForceClick(object sender, EventArgs e)
		{
			_biometricClient.Force();
		}

		#endregion
	}
}
