using System;
using System.Drawing;
using System.Globalization;
using Neurotec.Biometrics;
using Neurotec.Samples.Code;

namespace Neurotec.Samples.Controls
{
	public partial class TestSpeedPanel : ControlBase
	{
		#region Public constructor

		public TestSpeedPanel()
		{
			InitializeComponent();
		}

		#endregion

		#region Private fields

		private TaskSender _taskSender;

		#endregion

		#region Public overriden properties

		public override AcceleratorConnection Accelerator
		{
			get
			{
				return base.Accelerator;
			}
			set
			{
				base.Accelerator = value;
				if (Visible)
				{
					var isAccelerator = value != null;
					var supportsGetCount = (BiometricClient.RemoteConnections[0].Operations & NBiometricOperations.GetCount) == NBiometricOperations.GetCount;
					lblTemplateInfo.Visible = !(isAccelerator || supportsGetCount);
					lblTemplatesOnAcc.Text = string.Format("Templates on {0}:", isAccelerator ? "accelerator" : supportsGetCount ? "server" : "server*");
				}
			}
		}

		#endregion

		#region Public methods

		public override string GetTitle()
		{
			return "Test matching speed";
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

		public override void Cancel()
		{
			if (!IsBusy) return;

			_taskSender.Cancel();
			rtbStatus.Text = "Canceling ...\r\n";

			btnCancel.Enabled = false;
		}

		#endregion

		#region Private form events

		private void BtnStartClick(object sender, EventArgs e)
		{
			var isAccelerator = Accelerator != null;
			var supportsGetCount = (BiometricClient.RemoteConnections[0].Operations | NBiometricOperations.GetCount) == NBiometricOperations.GetCount;
			try
			{
				if (IsBusy) return;

				EnableControls(false);
				tbSpeed.Text = @"N/A";
				tbTime.Text = @"N/A";
				tbTaskCount.Text = @"N/A";
				rtbStatus.Text = @"Preparing ...";
				lblCount.Text = string.Empty;
				pbarProgress.Value = 0;
				int maxCount = Convert.ToInt32(nudMaxCount.Value);
				int loaderTemplateCount = GetTemplateCount();
				pbarProgress.Maximum = maxCount > loaderTemplateCount ? loaderTemplateCount : maxCount;
				pbStatus.Image = null;

				// if speed is counted not on MegaMatcher Accelerator and server does not support get count operation 
				// servers DB size is assumed to be equal to probe template databases size
				int templateCount = isAccelerator ? Accelerator.GetDbSize() : supportsGetCount ? BiometricClient.GetCount() : loaderTemplateCount;
				tbDBSize.Text = templateCount.ToString(CultureInfo.InvariantCulture);
				_taskSender.BunchSize = maxCount;
				_taskSender.SendOneBunchOnly = true;
				_taskSender.TemplateLoader = TemplateLoader;
				_taskSender.IsAccelerator = isAccelerator;
				BiometricClient.MatchingWithDetails = false;
				_taskSender.BiometricClient = BiometricClient;
				_taskSender.Start();
			}
			catch (Exception ex)
			{
				Utilities.ShowError(ex);
				rtbStatus.Text = ex.ToString();
				pbStatus.Image = Properties.Resources.error;
				EnableControls(true);
			}
		}

		private void BtnCancelClick(object sender, EventArgs e)
		{
			Cancel();
		}

		private void TestSpeedPanelLoad(object sender, EventArgs e)
		{
			try
			{
				_taskSender = new TaskSender(BiometricClient, TemplateLoader, NBiometricOperations.Identify);
				_taskSender.ProgressChanged += TaskSenderProgressChanged;
				_taskSender.Finished += TaskSenderFinished;
				_taskSender.ExceptionOccured += TaskSenderExceptionOccured;
				lblCount.Text = string.Empty;
				lblRemaining.Text = string.Empty;
			}
			catch (Exception ex)
			{
				Utilities.ShowError(ex);
			}
		}

		#endregion

		#region Private methods

		private void EnableControls(bool isIdle)
		{
			btnStart.Enabled = isIdle;
			gbProperties.Enabled = isIdle;
			btnCancel.Enabled = !isIdle;
		}

		private void TaskSenderProgressChanged()
		{
			double templatesMatched = _taskSender.PerformedTaskCount;
			if (templatesMatched == 1)
			{
				rtbStatus.Text = @"Matching templates ...";
			}

			var formatInfo = (NumberFormatInfo)NumberFormatInfo.CurrentInfo.Clone();
			formatInfo.NumberGroupSeparator = " ";
			tbTaskCount.Text = templatesMatched.ToString(CultureInfo.InvariantCulture);

			double dbSize = Convert.ToInt32(tbDBSize.Text);
			double timeElapsed = _taskSender.ElapsedTime.TotalSeconds;
			double speed = dbSize * templatesMatched / timeElapsed;

			tbSpeed.Text = speed.ToString("N2", formatInfo);
			tbTime.Text = timeElapsed.ToString("#.## s");
			TimeSpan remaining = TimeSpan.FromSeconds(timeElapsed / templatesMatched * (pbarProgress.Maximum - templatesMatched));
			if (remaining.TotalSeconds < 0)
				remaining = TimeSpan.Zero;
			lblRemaining.Text = string.Format("Estimated time remaining: {0:00}.{1:00}:{2:00}:{3:00}", remaining.Days, remaining.Hours, remaining.Minutes, remaining.Seconds);

			pbarProgress.Value = (int)templatesMatched;
			lblCount.Text = string.Format("{0} / {1}", templatesMatched, pbarProgress.Maximum);
		}

		private void TaskSenderFinished()
		{
			EnableControls(true);
			if (_taskSender.Canceled)
			{
				rtbStatus.AppendText("Speed test canceled\r\n");
				pbStatus.Image = null;
				return;
			}

			if (_taskSender.Successful)
			{
				rtbStatus.Text = string.Format("Speed: {0} templates per second.\nTotal of {1} templates were sent and matched against {2} templates per {3} seconds.", tbSpeed.Text, tbTaskCount.Text, tbDBSize.Text, tbTime.Text);
				pbStatus.Image = Properties.Resources.ok;
				return;
			}

			if (!_taskSender.Successful)
			{
				rtbStatus.AppendText("\r\nOperation completed with errors\r\n");
				pbStatus.Image = Properties.Resources.error;
			}
			pbarProgress.Value = pbarProgress.Maximum;
		}

		private void TaskSenderExceptionOccured(Exception ex)
		{
			AppendStatus(string.Format("{0}\r\n", ex), Color.DarkRed);
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
