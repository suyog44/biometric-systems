using System;
using System.ComponentModel;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using Neurotec.Biometrics;
using Neurotec.Biometrics.Client;
using Neurotec.Images;

namespace Neurotec.Samples
{
	public partial class CaptureIrisPage : Neurotec.Samples.PageBase
	{
		#region Public constructor

		public CaptureIrisPage()
		{
			InitializeComponent();
		}

		#endregion

		#region Private fields

		private NBiometricClient _biometricClient;
		private NSubject _subject;
		private ManualResetEvent _isIdle = new ManualResetEvent(true);
		private NIris _iris = null;
		private NSubject _newSubject = null;

		#endregion

		#region Public methods

		public override void OnNavigatedTo(params object[] args)
		{
			if (args == null || args.Length != 2) throw new ArgumentException("args");

			_subject = (NSubject)args[0];
			_biometricClient = (NBiometricClient)args[1];
			if (_subject == null || _biometricClient == null) throw new ArgumentException("args");
			_biometricClient.PropertyChanged += OnBiometricClientPropertyChanged;

			_newSubject = new NSubject();
			_iris = new NIris();
			_newSubject.Irises.Add(_iris);
			irisView.Iris = _iris;
			lblStatus.Visible = false;

			OnIrisScannerChanged();
			ToggleRadioButton();

			base.OnNavigatedTo(args);
		}

		public override void OnNavigatingFrom()
		{
			if (IsBusy()) LongActionDialog.ShowDialog(this, "Finishing current action ...", CancelAndWait);
			_biometricClient.PropertyChanged -= OnBiometricClientPropertyChanged;
			if (_iris.Status == NBiometricStatus.Ok)
			{
				var irises = _newSubject.Irises.ToArray();
				_newSubject.Irises.Clear();
				foreach (var item in irises)
				{
					_subject.Irises.Add(item);
				}
			}
			_newSubject = null;
			_iris = null;
			irisView.Iris = null;
			_subject = null;
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

		private void ToggleRadioButton()
		{
			cbPosition.BeginUpdate();
			try
			{
				cbPosition.Items.Clear();
				if (rbFile.Checked)
				{
					cbPosition.Items.Add(NEPosition.Left);
					cbPosition.Items.Add(NEPosition.Right);
					cbPosition.Items.Add(NEPosition.Unknown);
				}
				else
				{
					try
					{
						foreach (var item in _biometricClient.IrisScanner.GetSupportedPositions())
						{
							cbPosition.Items.Add(item);
						}
					}
					catch (Exception ex)
					{
						cbPosition.Items.Add(NEPosition.Unknown);
						Utilities.ShowError(ex);
					}
				}
				cbPosition.SelectedIndex = 0;
			}
			finally
			{
				cbPosition.EndUpdate();
			}
			EnableControls();
		}

		private void EnableControls()
		{
			bool isBusy = IsBusy();
			gbCaptureOptions.Enabled = !isBusy;
			btnCancel.Enabled = isBusy && rbIrisScanner.Checked;
			btnCapture.Visible = rbIrisScanner.Checked;
			btnForce.Visible = rbIrisScanner.Checked;
			btnForce.Enabled = isBusy && !chbCaptureAutomatically.Checked;
			chbCaptureAutomatically.Visible = rbIrisScanner.Checked;
			chbCaptureAutomatically.Enabled = !isBusy;
			btnOpen.Visible = rbFile.Checked;
			rbIrisScanner.Enabled = _biometricClient.IrisScanner != null && _biometricClient.IrisScanner.IsAvailable;
			if (!btnCapture.Visible && !btnForce.Visible)
			{
				tableLayoutPanel2.SetCellPosition(btnOpen, tableLayoutPanel2.GetCellPosition(btnCapture));
			}

			bool boldFinish = !isBusy && _iris != null && _iris.Status == NBiometricStatus.Ok;
			btnFinish.Font = new Font(btnFinish.Font, boldFinish ? FontStyle.Bold : FontStyle.Regular);

			busyIndicator.Visible = isBusy;
		}

		private void SetStatusText(Color backColor, string format, params object[] args)
		{
			lblStatus.Text = string.Format(format, args);
			lblStatus.BackColor = backColor;
			lblStatus.Visible = true;
		}

		private void OnBiometricClientPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			if (e.PropertyName == "IrisScanner")
			{
				BeginInvoke(new Action(() =>
				{
					if (IsPageShown) OnIrisScannerChanged();
				}));
			}
		}

		private void OnIrisScannerChanged()
		{
			var device = _biometricClient.IrisScanner;
			if (device == null || !device.IsAvailable)
			{
				if (rbIrisScanner.Checked) rbFile.Checked = true;
				rbIrisScanner.Text = "Scanner (Not connected)";
			}
			else
			{
				rbIrisScanner.Text = string.Format("Scanner ({0})", device.DisplayName);
			}
			EnableControls();
		}

		#endregion

		#region Private form events

		private void OnIrisPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			if (e.PropertyName == "Status")
			{
				BeginInvoke(new Action<NBiometricStatus>(status =>
					{
						Color backColor = status == NBiometricStatus.Ok || status == NBiometricStatus.None ? Color.Green : Color.Red;
						SetStatusText(backColor, "Status: {0}", status);
					}), _iris.Status);
			}
		}

		private void OnCaptureCompleted(IAsyncResult r)
		{
			NBiometricStatus status = NBiometricStatus.InternalError;
			_iris.PropertyChanged -= OnIrisPropertyChanged;
			try
			{
				status = _biometricClient.EndCreateTemplate(r);
			}
			catch (Exception ex)
			{
				Utilities.ShowError(ex);
			}

			SetIsBusy(false);
			BeginInvoke(new Action(() =>
				{
					if (IsPageShown)
					{
						Color backColor = status == NBiometricStatus.Ok ? Color.Green : Color.Red;
						SetStatusText(backColor, "Extraction status: {0}", status);
						EnableControls();
					}
				}));
		}

		private void OnCreateTemplateCompleted(IAsyncResult r)
		{
			NBiometricStatus status = NBiometricStatus.InternalError;
			try
			{
				status = _biometricClient.EndCreateTemplate(r);
			}
			catch (Exception ex)
			{
				Utilities.ShowError(ex);
			}

			SetIsBusy(false);
			BeginInvoke(new Action(() =>
				{
					if (IsPageShown)
					{
						Color backColor = status == NBiometricStatus.Ok ? Color.Green : Color.Red;
						SetStatusText(backColor, "Extraction status: {0}", status);
						EnableControls();
					}
				}));
		}

		private void BtnCancelClick(object sender, EventArgs e)
		{
			_biometricClient.Cancel();
		}

		private void BtnOpenClick(object sender, EventArgs e)
		{
			if (!DesignMode)
			{
				openFileDialog.Filter = NImages.GetOpenFileFilterString(true, true);
			}
			if (openFileDialog.ShowDialog() == DialogResult.OK)
			{
				_iris.Image = null;
				_iris.Position = (NEPosition)cbPosition.SelectedItem;
				_iris.FileName = openFileDialog.FileName;
				_iris.CaptureOptions = NBiometricCaptureOptions.None;
				SetStatusText(Color.Green, "Extracting template ...");
				SetIsBusy(true);
				_biometricClient.BeginCreateTemplate(_newSubject, OnCreateTemplateCompleted, null);
				EnableControls();
			}
		}

		private void BtnCaptureClick(object sender, EventArgs e)
		{
			_iris.Image = null;
			_iris.FileName = null;
			_iris.Position = (NEPosition)cbPosition.SelectedItem;
			_iris.PropertyChanged += OnIrisPropertyChanged;
			_iris.CaptureOptions = !chbCaptureAutomatically.Checked ? NBiometricCaptureOptions.Manual : NBiometricCaptureOptions.None;
			SetStatusText(Color.Orange, "Starting capture from camera ...");
			SetIsBusy(true);
			_biometricClient.BeginCreateTemplate(_newSubject, OnCaptureCompleted, null);
			EnableControls();
		}

		private void BtnFinishClick(object sender, EventArgs e)
		{
			PageController.NavigateToStartPage();
		}

		private void RbIrisScannerCheckedChanged(object sender, EventArgs e)
		{
			if (rbIrisScanner.Checked)
				ToggleRadioButton();
		}

		private void RbFileCheckedChanged(object sender, EventArgs e)
		{
			if (rbFile.Checked)
				ToggleRadioButton();
		}

		private void BtnForceClick(object sender, EventArgs e)
		{
			_biometricClient.Force();
		}

		#endregion

	}
}
