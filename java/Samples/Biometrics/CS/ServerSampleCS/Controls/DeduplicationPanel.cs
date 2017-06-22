using System;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using Neurotec.Biometrics;
using Neurotec.Samples.Code;

namespace Neurotec.Samples.Controls
{
	public partial class DeduplicationPanel : ControlBase
	{
		#region Public constructor

		public DeduplicationPanel()
		{
			InitializeComponent();
		}

		#endregion

		#region Private fields

		private TaskSender _taskSender;
		private string _resultsFile;

		#endregion

		#region Public methods

		public override void Cancel()
		{
			if (!IsBusy) return;

			_taskSender.Cancel();
			btnCancel.Enabled = false;
			rtbStatus.AppendText("\r\nCanceling, please wait ...\r\n");
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

		public override string GetTitle()
		{
			return "Deduplication";
		}

		#endregion

		#region Private methods

		private void EnableControls(bool isIdle)
		{
			btnStart.Enabled = isIdle;
			btnCancel.Enabled = !isIdle;
			gbProperties.Enabled = isIdle;
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

		private void WriteLogHeader()
		{
			string line = @"TemplateId, MatchedWith, Score, FingersScore, FingersScores, IrisesScore, IrisesScores";
			if (Accelerator != null)
			{
				line += @", FacesScore, FacesScores, VoicesScore, VoicesScores, PalmsScore, PalmsScores";
			}
			line += "\n";
			File.WriteAllText(_resultsFile, line);
		}

		private void MatchingTasksCompleted(NBiometricTask[] tasks)
		{
			if (tasks != null)
			{
				try
				{
					var text = new StringBuilder();
					foreach (NBiometricTask task in tasks)
					{
						foreach (NSubject subject in task.Subjects)
						{
							if (subject.MatchingResults != null && subject.MatchingResults.Count > 0)
								foreach (NMatchingResult item in subject.MatchingResults)
								{
									var line = new StringBuilder();
									line.Append(string.Format("{0},{1},{2}", subject.Id, item.Id, item.Score));
									using (var details = new NMatchingDetails(item.MatchingDetailsBuffer))
									{
										line.Append(string.Format(",{0},", details.FingersScore));
										foreach (NFMatchingDetails t in details.Fingers)
										{
											line.Append(string.Format("{0};", t.Score));
										}

										line.Append(string.Format(",{0},", details.IrisesScore));
										foreach (NEMatchingDetails t in details.Irises)
										{
											line.Append(string.Format("{0};", t.Score));
										}

										line.Append(string.Format(",{0},", details.FacesScore));
										foreach (NLMatchingDetails t in details.Faces)
										{
											line.Append(string.Format("{0};", t.Score));
										}

										line.Append(string.Format(",{0},", details.VoicesScore));
										foreach (NSMatchingDetails t in details.Voices)
										{
											line.Append(string.Format("{0};", t.Score));
										}

										line.Append(string.Format(",{0},", details.PalmsScore));
										foreach (NFMatchingDetails t in details.Palms)
										{
											line.Append(string.Format("{0};", t.Score));
										}
									}
									text.AppendLine(line.ToString());
								}
							else
							{
								text.AppendLine(string.Format("{0},NoMatches", subject.Id));
							}
						}
						task.Dispose();
					}

					File.AppendAllText(_resultsFile, text.ToString());
				}
				catch (Exception ex)
				{
					AppendStatus(string.Format("{0}\r\n", ex), Color.DarkRed);
				}
			}
		}

		private void TaskSenderExceptionOccured(Exception ex)
		{
			AppendStatus(string.Format("{0}\r\n", ex), Color.DarkRed);
		}

		private void TaskSenderFinished()
		{
			EnableControls(true);

			lblRemaining.Text = string.Empty;
			pbarProgress.Value = pbarProgress.Maximum;

			if (_taskSender.Successful)
			{
				rtbStatus.AppendText("Deduplication completed without errors");
				pbStatus.Image = pbStatus.Image = Properties.Resources.ok;
			}
			else
			{
				rtbStatus.AppendText(_taskSender.Canceled ? "Deduplication canceled." : "There were errors during deduplication");
				pbStatus.Image = pbStatus.Image = Properties.Resources.error;
			}
		}

		private void TaskSenderProgressChanged()
		{
			var numberOfTasksCompleted = _taskSender.PerformedTaskCount;
			TimeSpan elapsed = _taskSender.ElapsedTime;
			TimeSpan remaining = TimeSpan.FromTicks(elapsed.Ticks / numberOfTasksCompleted * (pbarProgress.Maximum - numberOfTasksCompleted));
			if (remaining.TotalSeconds < 0)
				remaining = TimeSpan.Zero;
			lblRemaining.Text = string.Format("Estimated time remaining: {0:00}.{1:00}:{2:00}:{3:00}", remaining.Days, remaining.Hours, remaining.Minutes, remaining.Seconds);

			pbarProgress.Value = numberOfTasksCompleted > pbarProgress.Maximum ? pbarProgress.Maximum : numberOfTasksCompleted;
			lblProgress.Text = string.Format("{0} / {1}", numberOfTasksCompleted, pbarProgress.Maximum);
		}

		#endregion

		#region Private form events

		private void BtnStartClick(object sender, EventArgs e)
		{
			try
			{
				if (IsBusy) return;

				rtbStatus.Text = string.Empty;
				pbStatus.Image = null;
				lblProgress.Text = string.Empty;
				lblRemaining.Text = string.Empty;

				_resultsFile = tbLogFile.Text;
				WriteLogHeader();

				pbarProgress.Value = 0;
				pbarProgress.Maximum = GetTemplateCount();

				_taskSender.TemplateLoader = TemplateLoader;
				_taskSender.IsAccelerator = Accelerator != null;
				BiometricClient.MatchingWithDetails = true;
				_taskSender.BiometricClient = BiometricClient;
				_taskSender.Start();
				EnableControls(false);
			}
			catch (Exception ex)
			{
				Utilities.ShowError(ex);
			}
		}

		private void DeduplicationPanelLoad(object sender, EventArgs e)
		{
			try
			{
				_taskSender = new TaskSender(BiometricClient, TemplateLoader, NBiometricOperations.Identify);
				_taskSender.ProgressChanged += TaskSenderProgressChanged;
				_taskSender.Finished += TaskSenderFinished;
				_taskSender.ExceptionOccured += TaskSenderExceptionOccured;
				_taskSender.MatchingTasksCompleted += MatchingTasksCompleted;

				lblProgress.Text = string.Empty;
				lblRemaining.Text = string.Empty;
			}
			catch (Exception ex)
			{
				Utilities.ShowError(ex);
			}
		}

		private void BtnCancelClick(object sender, EventArgs e)
		{
			Cancel();
		}

		private void BtnBrowseClick(object sender, EventArgs e)
		{
			if (openFileDialog.ShowDialog() == DialogResult.OK)
			{
				tbLogFile.Text = openFileDialog.FileName;
			}
		}

		#endregion
	}
}
