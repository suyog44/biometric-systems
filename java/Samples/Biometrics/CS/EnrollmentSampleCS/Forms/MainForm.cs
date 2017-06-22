using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Neurotec.Biometrics;
using Neurotec.Biometrics.Client;
using Neurotec.Biometrics.Gui;
using Neurotec.Devices;
using Neurotec.Images;
using Neurotec.IO;
using Neurotec.Licensing;
using Neurotec.Samples.Controls;
using Neurotec.Samples.Properties;

namespace Neurotec.Samples.Forms
{
	public partial class MainForm : Form
	{
		#region Public constructor

		public MainForm()
		{
			InitializeComponent();
		}

		#endregion

		#region Private fields

		private readonly NFPosition[] _slaps = new NFPosition[]
		{
			NFPosition.PlainLeftFourFingers,
			NFPosition.PlainRightFourFingers,
			NFPosition.PlainThumbs,
		};

		private readonly NFPosition[] _fingers = new NFPosition[]
		{
			NFPosition.LeftLittle,
			NFPosition.LeftRing,
			NFPosition.LeftMiddle,
			NFPosition.LeftIndex,
			NFPosition.LeftThumb,
			NFPosition.RightThumb,
			NFPosition.RightIndex,
			NFPosition.RightMiddle,
			NFPosition.RightRing,
			NFPosition.RightLittle,
		};

		private bool _canCaptureSlaps;
		private bool _canCaptureRolled;
		private bool _captureStarted;
		private bool _newSubject = true;
		private FingerCaptureForm _captureForm;
		private bool _exit;

		private DataModel _model;
		private NBiometricClient _biometricClient;

		#endregion

		#region Private form events

		private void MainFormLoad(object sender, EventArgs e)
		{
			if (DesignMode) return;
			BeginInvoke(new MethodInvoker(OnMainFormLoaded));
		}

		private void FingerSelectorFingerClick(object sender, FingerSelector.FingerClickArgs e)
		{
			var missing = new List<NFPosition>(fSelector.MissingPositions);
			NFPosition position = e.Position;
			if (missing.Contains(position))
				missing.Remove(position);
			else
				missing.Add(position);

			fSelector.MissingPositions = missing.ToArray();
		}

		private void BtnStartCapturingClick(object sender, EventArgs e)
		{
			if (_model.Subject == null)
			{
				_model.Subject = new NSubject();
				_model.Subject.Fingers.CollectionChanged += OnFingersCollectionChanged;
				CreateFingers(_model.Subject);
				if (_model.Subject.Fingers.Count == 0)
				{
					Utilities.ShowWarning(this, "No fingers selected for capturing");
					return;
				}
			}
			_captureStarted = true;
			_newSubject = false;
			EnableControls(false);

			_captureForm = new FingerCaptureForm();
			_captureForm.BiometricClient = _biometricClient;
			_captureForm.Subject = _model.Subject;
			_captureForm.FormClosed += new FormClosedEventHandler(CaptureFormFormClosed);
			_captureForm.Show(this);
		}

		private void OnFingersCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
		{
			switch (e.Action)
			{
				case NotifyCollectionChangedAction.Add:
					{
						BeginInvoke(new Action<NFinger[]>(newItems =>
							{
								NFingerView view = null;
								foreach (NFinger f in newItems)
								{
									view = GetView(f);
									if (view != null)
										view.Finger = f;
								}
							}), (object)e.NewItems.Cast<NFinger>().ToArray());
							break;
					}
				case NotifyCollectionChangedAction.Remove:
					{
						BeginInvoke(new Action<NFinger[]>(oldItems =>
							{
								NFingerView view = null;
								foreach (NFinger f in oldItems)
								{
									view = GetView(f);
									if (view != null)
										view.Finger = null;
								}
							}), (object)e.OldItems.Cast<NFinger>().ToArray());
						break;
					}
				case NotifyCollectionChangedAction.Reset:
					{
						BeginInvoke(new Action(() =>
						{
							NFingerView view = null;
							foreach (NFPosition position in _slaps)
							{
								view = GetView(position, false);
								view.Finger = null;
							}
							foreach (NFPosition position in _fingers)
							{
								view = GetView(position, false);
								view.Finger = null;
								view = GetView(position, true);
								view.Finger = null;
							}
						}));
						break;
					}
				default:
					break;
			}
		}

		private void MainFormFormClosing(object sender, FormClosingEventArgs e)
		{
			SaveSettings();

			if (_captureForm != null)
			{
				Text = @"Enrollment Sample: Closing ...";
				e.Cancel = true;
				_exit = true;
				_captureForm.Close();
			}
		}

		private void CaptureFormFormClosed(object sender, FormClosedEventArgs e)
		{
			_captureForm.Dispose();
			_captureForm = null;

			_captureStarted = false;
			EnableControls(true);

			if (_exit) Close();
		}

		private void CaptureComboBoxCheckedChanged(object sender, EventArgs e)
		{
			if (sender == chbCapturePlainFingers && !chbCapturePlainFingers.Checked)
			{
				chbCaptureSlaps.Checked = false;
			}
			if (sender == chbCaptureSlaps && chbCaptureSlaps.Checked)
			{
				chbCapturePlainFingers.Checked = true;
			}

			btnStartCapturing.Enabled = chbCaptureRolled.Checked || chbCapturePlainFingers.Checked;
		}

		private void ChbShowOriginalCheckedChanged(object sender, EventArgs e)
		{
			CheckBox target = (CheckBox)sender;
			CheckBox other = target == chbShowOriginal ? chbShowOriginalRolled : chbShowOriginal;
			if (other.Checked == target.Checked) return;
			other.Checked = target.Checked;

			NFingerView view;
			ShownImage shown = target.Checked ? ShownImage.Original : ShownImage.Result;
			foreach (NFPosition position in _fingers)
			{
				view = (NFingerView)GetView(position, false);
				view.ShownImage = shown;

				view = (NFingerView)GetView(position, true);
				view.ShownImage = shown;
			}
		}

		private void ViewMouseEnter(object sender, EventArgs e)
		{
			NFingerView view = sender as NFingerView;
			if (view == null || _captureStarted) return;
			if (toolStripViewControls.Parent != null) toolStripViewControls.Parent.Controls.Remove(toolStripViewControls);
			if (view.Finger != null)
			{
				NFrictionRidge finger = view.Finger;
				if (finger != _biometricClient.CurrentBiometric)
				{
					if (finger.Image != null)
					{
						bool canSaveRecord = false;
						if (NBiometricTypes.IsPositionSingleFinger(finger.Position))
						{
							canSaveRecord = finger.Objects.ToArray().Any(x => x.Template != null);
						}
						tsbSaveRecord.Visible = canSaveRecord;
						toolStripViewControls.Tag = finger;
						view.Parent.Controls.Add(toolStripViewControls);
						toolStripViewControls.Visible = true;
						toolStripViewControls.BringToFront();
					}
				}
			}
		}

		private void ViewMouseLeave(object sender, EventArgs e)
		{
			if (!toolStripViewControls.Bounds.Contains(toolStripViewControls.PointToClient(MousePosition)))
			{
				toolStripViewControls.Visible = false;
				toolStripViewControls.Tag = null;
			}
		}

		private void TsbSaveImageClick(object sender, EventArgs e)
		{
			string fileName = string.Empty;
			NFinger finger = (NFinger)toolStripViewControls.Tag;
			saveFileDialog.Filter = NImages.GetSaveFileFilterString();
			saveFileDialog.FileName = fileName;
			if (finger != null)
			{
				bool originalImage = chbShowOriginal.Checked && NBiometricTypes.IsPositionSingleFinger(finger.Position);
				bool isRolled = NBiometricTypes.IsImpressionTypeRolled(finger.ImpressionType);
				using (NImage image = originalImage ? finger.GetImage(false) : finger.GetBinarizedImage(false))
				{
					fileName = string.Format("{0}{1}{2}", finger.Position, isRolled ? "_Rolled" : string.Empty, originalImage ? string.Empty : "_Binarized");
					if (saveFileDialog.ShowDialog() == DialogResult.OK)
					{
						image.Save(saveFileDialog.FileName);
					}
				}
			}
		}

		private void TsbSaveRecordClick(object sender, EventArgs e)
		{
			NFinger finger = (NFinger)toolStripViewControls.Tag;
			saveFileDialog.Filter = string.Empty;
			if (finger != null)
			{
				bool isRolled = NBiometricTypes.IsImpressionTypeRolled(finger.ImpressionType);
				saveFileDialog.FileName = string.Format("{0}{1}", finger.Position, isRolled ? "_Rolled" : string.Empty);
				if (saveFileDialog.ShowDialog() == DialogResult.OK)
				{
					NFRecord record = finger.Objects.ToArray().First().Template;
					using (NBuffer buffer = record.Save())
					{
						File.WriteAllBytes(saveFileDialog.FileName, buffer.ToArray());
					}
				}
			}
		}

		private void NewToolStripMenuItemClick(object sender, EventArgs e)
		{
			if (!Utilities.ShowQuestion(this, "This will erase all images and records. Are you sure you want to continue?")) return;

			if (_model.Subject != null)
			{
				_model.Subject.Fingers.Clear();
				_model.Subject = null;
			}
			fSelector.MissingPositions = null;

			_model = new DataModel();
			_model.Info.AddRange(LoadInfoFields());
			infoPanel.Model = _model;
			_newSubject = true;
			EnableControls(true);
		}

		private void SaveTemplateToolStripMenuItemClick(object sender, EventArgs e)
		{
			saveFileDialog.FileName = string.Empty;
			saveFileDialog.Filter = string.Empty;

			if (_model.Subject == null)
			{
				Utilities.ShowWarning("Nothing to save");
			}
			else
			{
				using (NTemplate template = _model.Subject.GetTemplate())
				{
					if (template.Fingers == null)
					{
						Utilities.ShowWarning("Nothing to save");
					}
					else if (saveFileDialog.ShowDialog() == DialogResult.OK)
					{
						using (NBuffer buffer = template.Save())
							File.WriteAllBytes(saveFileDialog.FileName, buffer.ToArray());
					}
				}
			}
		}

		private void SaveImagesToolStripMenuItemClick(object sender, EventArgs e)
		{
			if (_model.Subject != null)
			{
				NSubject subject = _model.Subject;
				var fingers = subject.Fingers.Where(x => x.Status == NBiometricStatus.Ok);
				if (fingers.Count() > 0)
				{
					if (folderBrowserDialog.ShowDialog() != DialogResult.OK) return;
					try
					{
						string dir = folderBrowserDialog.SelectedPath;
						foreach (NFinger item in fingers)
						{
							bool isRolled = NBiometricTypes.IsImpressionTypeRolled(item.ImpressionType);
							string name = string.Format("{0}{1}.png", item.Position, isRolled? "_rolled" : string.Empty);
							item.Image.Save(Path.Combine(dir, name));
						}
					}
					catch (Exception ex)
					{
						Utilities.ShowError(ex);
					}
				}
				else Utilities.ShowWarning("Nothing to save");
			}
			else Utilities.ShowWarning("Nothing to save");
		}

		private void ExitToolStripMenuItemClick(object sender, EventArgs e)
		{
			Close();
		}

		private void ChangeScannerToolStripMenuItemClick(object sender, EventArgs e)
		{
			using (DeviceSelectForm form = new DeviceSelectForm())
			{
				form.SelectedDevice = _biometricClient.FingerScanner;
				form.DeviceManager = _biometricClient.DeviceManager;
				if (form.ShowDialog() == DialogResult.OK)
				{
					if (_biometricClient.FingerScanner != form.SelectedDevice)
					{
						OnSelectedDeviceChanging(form.SelectedDevice);
					}
				}
			}
		}

		private void OptionsToolStripMenuItemClick(object sender, EventArgs e)
		{
			using (ExtractionOptionsForm form = new ExtractionOptionsForm())
			{
				form.BiometricClient = _biometricClient;
				form.ShowDialog();
			}
		}

		private void AboutToolStripMenuItemClick(object sender, EventArgs e)
		{
			Gui.AboutBox.Show(this);
		}

		private void ExportToolStripMenuItemClick(object sender, EventArgs e)
		{
			if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
			{
				_model.Save(folderBrowserDialog.SelectedPath);
			}
		}

		private void EditRequiredInfoToolStripMenuItemClick(object sender, EventArgs e)
		{
			using (EditInfoForm form = new EditInfoForm())
			{
				form.Information = LoadInfoFields();
				if (form.ShowDialog() != DialogResult.OK) return;
				_model.Info.Clear();
				_model.Info.AddRange(form.Information);
				infoPanel.Model = _model;
			}
		}

		#endregion

		#region Private methods

		private void OnMainFormLoaded()
		{
			try
			{
				_biometricClient = CreateBiometricClient();
			}
			catch (Exception ex)
			{
				Utilities.ShowError(ex);
				BeginInvoke(new MethodInvoker(Close));
				return;
			}

			_model = new DataModel();
			_model.Info.AddRange(LoadInfoFields());
			infoPanel.Model = _model;
			infoPanel.DeviceManager = _biometricClient.DeviceManager;

			toolStripViewControls.Visible = false;
			try
			{
				chbCaptureRolled.Checked = Settings.Default.ScanRolled;
				chbCapturePlainFingers.Checked = Settings.Default.ScanPlain;
				chbCaptureSlaps.Checked = Settings.Default.ScanSlaps;
				chbShowOriginal.Checked = Settings.Default.ShowOriginal;
			}
			catch { }

			if (!chbShowOriginal.Checked)
			{
				foreach (NFPosition position in _fingers)
				{
					NFingerView view = GetView(position, false);
					view.ShownImage = ShownImage.Result;

					view = GetView(position, true);
					view.ShownImage = ShownImage.Result;
				}
			}

			if (_biometricClient.FingerScanner == null)
			{
				using (DeviceSelectForm form = new DeviceSelectForm())
				{
					form.DeviceManager = _biometricClient.DeviceManager;
					if (form.ShowDialog() == DialogResult.OK)
					{
						_biometricClient.FingerScanner = form.SelectedDevice;
					}
					else
					{
						Close();
					}
				}
			}

			OnSelectedDeviceChanging(_biometricClient.FingerScanner);
		}

		private NBiometricClient CreateBiometricClient()
		{
			NBiometricClient client = new NBiometricClient();

			string propertiesString = string.Empty;
			try { propertiesString = Settings.Default.ClientProperties; }
			catch { }

			NPropertyBag propertyBag = NPropertyBag.Parse(propertiesString);
			propertyBag.ApplyTo(client);

			client.BiometricTypes = NBiometricType.Finger | NBiometricType.Face;
			client.FingersReturnBinarizedImage = true;
			client.UseDeviceManager = true;
			client.FingersCalculateNfiq = NLicense.IsComponentActivated("Biometrics.FingerQualityAssessmentBase");

			LongTaskForm.RunLongTask("Initializing biometric client ...", (sender, args) => { ((NBiometricClient)args.Argument).Initialize(); }, client);

			string preferedDeviceId = string.Empty;
			try { preferedDeviceId = Settings.Default.SelectedFScannerId; }
			catch { }

			NDevice device = null;
			if (!string.IsNullOrEmpty(preferedDeviceId))
			{
				if (client.DeviceManager.Devices.Contains(preferedDeviceId))
					device = client.DeviceManager.Devices[preferedDeviceId];
			}
			client.FingerScanner = (NFScanner)device;

			return client;
		}

		private NFingerView GetView(NFPosition position, bool isRolled)
		{
			switch (position)
			{
				case NFPosition.LeftIndex: return isRolled ? nfvLeftIndexRolled : nfvLeftIndex;
				case NFPosition.LeftLittle: return isRolled ? nfvLeftLittleRolled : nfvLeftLittle;
				case NFPosition.LeftMiddle: return isRolled ? nfvLeftMiddleRolled : nfvLeftMiddle;
				case NFPosition.LeftRing: return isRolled ? nfvLeftRingRolled : nfvLeftRing;
				case NFPosition.LeftThumb: return isRolled ? nfvLeftThumbRolled : nfvLeftThumb;
				case NFPosition.RightIndex: return isRolled ? nfvRightIndexRolled : nfvRightIndex;
				case NFPosition.RightLittle: return isRolled ? nfvRightLittleRolled : nfvRightLittle;
				case NFPosition.RightMiddle: return isRolled ? nfvRightMiddleRolled : nfvRightMiddle;
				case NFPosition.RightRing: return isRolled ? nfvRightRingRolled : nfvRightRing;
				case NFPosition.RightThumb: return isRolled ? nfvRightThumbRolled : nfvRightThumb;
				case NFPosition.PlainLeftFourFingers: return nfvLeftFour;
				case NFPosition.PlainRightFourFingers: return nfvRightFour;
				case NFPosition.PlainThumbs: return nfvThumbs;
				default: return null;
			}
		}

		private NFingerView GetView(NFinger finger)
		{
			NFPosition position = finger.Position;
			bool isRolled = NBiometricTypes.IsImpressionTypeRolled(finger.ImpressionType);
			return GetView(position, isRolled);
		}

		private InfoField[] LoadInfoFields()
		{
			List<InfoField> info = new List<InfoField>();
			string[] split = Settings.Default.Information.Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
			foreach (string item in split)
			{
				InfoField inf = new InfoField(item);
				if (inf.ShowAsThumbnail)
				{
					inf.IsEditable = false;
				}
				info.Add(inf);
			}
			return info.ToArray();
		}

		private void CreateFingers(NSubject subject)
		{
			var missing = fSelector.MissingPositions;
			foreach (var item in missing)
			{
				subject.MissingFingers.Add(item);
			}

			var fingers = _fingers.Where(x => !missing.Contains(x));
			if (chbCaptureSlaps.Checked)
			{
				var slaps = _slaps.Where(x => NBiometricTypes.GetPositionAvailableParts(x, missing).Length != 0);
				foreach (var pos in slaps)
				{
					subject.Fingers.Add(new NFinger { Position = pos });
				}
			}
			else if (chbCapturePlainFingers.Checked)
			{
				foreach (var pos in fingers)
				{
					subject.Fingers.Add(new NFinger { Position = pos });
				}
			}

			if (chbCaptureRolled.Checked)
			{
				foreach (var pos in fingers)
				{
					subject.Fingers.Add(new NFinger { Position = pos, ImpressionType = NFImpressionType.LiveScanRolled });
				}
			}
		}

		private void OnSelectedDeviceChanging(NFScanner newDevice)
		{
			bool canCaptureRolled = false;
			bool canCaptureSlaps = false;
			if (newDevice != null)
			{
				foreach (NFPosition item in newDevice.GetSupportedPositions())
				{
					if (NBiometricTypes.IsPositionFourFingers(item))
					{
						canCaptureSlaps = true;
						break;
					}
				}

				foreach (NFImpressionType item in newDevice.GetSupportedImpressionTypes())
				{
					if (NBiometricTypes.IsImpressionTypeRolled(item))
					{
						canCaptureRolled = true;
						break;
					}
				}
			}

			if (_biometricClient.FingerScanner != null && _biometricClient.FingerScanner != newDevice)
			{
				if (Utilities.ShowQuestion("Changing scanner will clear all currently captured data. Proceed?"))
				{
					if (_model.Subject != null)
					{
						_model.Subject.Fingers.Clear();
						_model.Subject = null;
					}
					fSelector.MissingPositions = null;

					_model = new DataModel();
					_model.Info.AddRange(LoadInfoFields());
					infoPanel.Model = _model;
					_newSubject = true;
				}
				else return;
			}

			_canCaptureSlaps = canCaptureSlaps;
			_canCaptureRolled = canCaptureRolled;

			_biometricClient.FingerScanner = newDevice;
			if (!_canCaptureSlaps) chbCaptureSlaps.Checked = false;
			if (!_canCaptureRolled) chbCaptureRolled.Checked = false;
			EnableControls(true);
		}

		private void SaveSettings()
		{
			if (_biometricClient != null)
			{
				Settings.Default.SelectedFScannerId = _biometricClient.FingerScanner != null ? _biometricClient.FingerScanner.Id : null;
				Settings.Default.ScanRolled = chbCaptureRolled.Checked;
				Settings.Default.ScanSlaps = chbCaptureSlaps.Checked;
				Settings.Default.ScanPlain = chbCapturePlainFingers.Checked;
				Settings.Default.ShowOriginal = chbShowOriginal.Checked;

				NPropertyBag propertyBag = new NPropertyBag();
				_biometricClient.CaptureProperties(propertyBag);
				Settings.Default.ClientProperties = propertyBag.ToString();
				Settings.Default.Save();
			}
		}

		private void EnableControls(bool enable)
		{
			btnStartCapturing.Enabled = enable && (chbCaptureRolled.Checked || chbCapturePlainFingers.Checked);
			chbCapturePlainFingers.Enabled = enable && _newSubject;
			chbCaptureRolled.Enabled = _canCaptureRolled && enable && _newSubject;
			chbCaptureSlaps.Enabled = _canCaptureSlaps && enable && _newSubject;
			gbFingerSelector.Enabled = enable && _newSubject;
			fSelector.Enabled = enable && _newSubject;
			changeScannerToolStripMenuItem.Enabled = enable;
			optionsToolStripMenuItem.Enabled = enable;
			saveImagesToolStripMenuItem.Enabled = enable;
			saveTemplateToolStripMenuItem.Enabled = enable;
			newToolStripMenuItem.Enabled = enable;
			exportToolStripMenuItem.Enabled = enable;
		}

		#endregion
	}
}
