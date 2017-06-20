using System;
using System.Drawing;
using System.Globalization;
using Neurotec.Biometrics;
using Neurotec.Samples.Code;

namespace Neurotec.Samples.Controls
{
	public partial class EnrollPanel : ControlBase
	{
		#region Public constructor

		public EnrollPanel()
		{
			InitializeComponent();
		}

		#endregion

		#region Private fields

		TaskSender _taskSender;

		#endregion

		#region Public methods

		public override string GetTitle()
		{
			return "Enroll Templates";
		}

		public override void Cancel()
		{
			if (!IsBusy) return;

			_taskSender.Cancel();
			btnCancel.Enabled = false;
			AppendStatus("\r\nCanceling, please wait ...\r\n");
		}

		public override bool IsBusy
		{
			get
			{
				if (_taskSender != null)
					return _taskSender.IsBusy;
				return false;
			}
		}

		#endregion

		#region Private form events
		private void BtnStartClick(object sender, EventArgs e)
		{
			try
			{
				if (IsBusy) return;

				pbarProgress.Value = 0;
				int count = GetTemplateCount();
				pbarProgress.Maximum = count;
				tbTaskCount.Text = count.ToString(CultureInfo.InvariantCulture);
				rtbStatus.Text = string.Empty;
				pbStatus.Image = null;
				tbTime.Text = @"N/A";

				_taskSender.TemplateLoader = TemplateLoader;
				_taskSender.IsAccelerator = Accelerator != null;
				_taskSender.BiometricClient = BiometricClient;
				_taskSender.BunchSize = (int)nudBunchSize.Value;
				_taskSender.Start();

				AppendStatus(string.Format("Enrolling from: {0}\r\n", TemplateLoader));
				EnableControls(false);
			}
			catch (Exception ex)
			{
				Utilities.ShowError(ex);
			}
		}

		private void EnrollPanelLoad(object sender, EventArgs e)
		{
			_taskSender = new TaskSender(BiometricClient, TemplateLoader, NBiometricOperations.Enroll);
			_taskSender.ProgressChanged += TaskSenderProgressChanged;
			_taskSender.Finished += TaskSenderFinished;
			_taskSender.ExceptionOccured += TaskSenderExceptionOccured;
			lblProgress.Text = string.Empty;
			lblRemaining.Text = string.Empty;
		}

		private void TaskSenderProgressChanged()
		{
			var templatesEnrolled = _taskSender.PerformedTaskCount;
			if (templatesEnrolled == 1)
			{
				rtbStatus.Text = string.Format("Enrolling from: {0}\r\n", TemplateLoader);
			}
			pbarProgress.Value = templatesEnrolled;

			TimeSpan elapsed = _taskSender.ElapsedTime;
			lblProgress.Text = string.Format("{0} / {1}", templatesEnrolled, tbTaskCount.Text);
			tbTime.Text = elapsed.TotalSeconds.ToString("#.## s");

			TimeSpan remaining = TimeSpan.FromTicks(elapsed.Ticks / templatesEnrolled * (pbarProgress.Maximum - templatesEnrolled));
			if (remaining.TotalSeconds < 0)
				remaining = TimeSpan.Zero;
			lblRemaining.Text = string.Format("Estimated time remaining: {0:00}.{1:00}:{2:00}:{3:00}", remaining.Days, remaining.Hours, remaining.Minutes, remaining.Seconds);

		}

		private void TaskSenderFinished()
		{
			EnableControls(true);
			tbTime.Text = string.Format("{0:00}:{1:00}:{2:00}", _taskSender.ElapsedTime.Hours, _taskSender.ElapsedTime.Minutes, _taskSender.ElapsedTime.Seconds);
			lblRemaining.Text = string.Empty;
			lblProgress.Text = string.Empty;
			if (_taskSender.Canceled)
			{
				AppendStatus("\r\nEnrollment canceled");
				pbStatus.Image = Properties.Resources.error;
				return;
			}

			if (_taskSender.Successful)
			{
				AppendStatus("\r\nAll templates enrolled successfully");
				pbStatus.Image = Properties.Resources.ok;
			}
			else
			{
				AppendStatus("\r\nThere were errors during enrollment", Color.DarkRed);
				pbStatus.Image = Properties.Resources.error;
			}
		}

		private void TaskSenderExceptionOccured(Exception ex)
		{
			AppendStatus(string.Format("{0}\r\n", ex), Color.DarkRed);
		}

		private void BtnCancelClick(object sender, EventArgs e)
		{
			Cancel();
		}

		#endregion

		#region Private methods

		private void EnableControls(bool isIdle)
		{
			btnStart.Enabled = isIdle;
			btnCancel.Enabled = !isIdle;
			nudBunchSize.Enabled = isIdle;
		}

		private void AppendStatus(string msg)
		{
			AppendStatus(msg, Color.Black);
		}

		private delegate void AppendStatusDelegate(string msg, Color color);
		private void AppendStatus(string msg, Color color)
		{
			if (InvokeRequired)
			{
				AppendStatusDelegate del = AppendStatus;
				BeginInvoke(del, msg, color);
				return;
			}

			rtbStatus.SelectionStart = rtbStatus.TextLength;
			rtbStatus.SelectionColor = color;
			rtbStatus.AppendText(msg);
			rtbStatus.SelectionColor = rtbStatus.ForeColor;
		}

		#endregion
	}
}
