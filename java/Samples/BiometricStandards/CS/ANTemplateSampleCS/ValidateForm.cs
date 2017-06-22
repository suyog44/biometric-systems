using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;
using Neurotec.Biometrics.Standards;

namespace Neurotec.Samples
{
	public partial class ValidateForm : Form
	{
		#region Private fields

		private string _path;
		private string _filter;
		private ANValidationLevel _validationLevel = ANValidationLevel.Standard;
		private uint _flags;
		private string _currentFileName;

		#endregion

		#region Public constructor

		public ValidateForm()
		{
			InitializeComponent();
		}

		#endregion

		#region Public properties

		public string Path
		{
			get
			{
				return _path;
			}
			set
			{
				_path = value;
			}
		}

		public string Filter
		{
			get
			{
				return _filter;
			}
			set
			{
				_filter = value;
			}
		}

		public ANValidationLevel ValidationLevel
		{
			get
			{
				return _validationLevel;
			}
			set
			{
				_validationLevel = value;
			}
		}

		public uint Flags
		{
			get
			{
				return _flags;
			}
			set
			{
				_flags = value;
			}
		}

		#endregion

		#region Private methods

		private void ExamineDir(string path, string[] filters, List<string> fileNames)
		{
			if (backgroundWorker.CancellationPending) return;
			backgroundWorker.ReportProgress(-1, path);
			foreach (string filter in filters)
			{
				if (backgroundWorker.CancellationPending) break;
				foreach (string fileName in Directory.GetFiles(path, filter))
				{
					if (backgroundWorker.CancellationPending) break;
					fileNames.Add(fileName);
				}
			}
			if (backgroundWorker.CancellationPending) return;
			foreach (string dirName in Directory.GetDirectories(path))
			{
				if (backgroundWorker.CancellationPending) break;
				ExamineDir(dirName, filters, fileNames);
			}
		}

		#endregion

		#region Private form events

		private void WorkerDoWork(object sender, DoWorkEventArgs e)
		{
			int cc = 0;
			List<string> fileNames = new List<string>();
			ExamineDir(_path, _filter.Split(';'), fileNames);
			if (!backgroundWorker.CancellationPending)
			{
				int i = 0, c = fileNames.Count, twoC = c * 2;
				foreach (string fileName in fileNames)
				{
					backgroundWorker.ReportProgress((i + c) / twoC, fileName);
					if (backgroundWorker.CancellationPending) break;
					try
					{
						using (ANTemplate template = new ANTemplate(fileName, _validationLevel, _flags))
						{
						}
					}
					catch (Exception ex)
					{
						backgroundWorker.ReportProgress(-2, ex);
					}
					i += 200;
					cc++;
				}
			}
			e.Result = backgroundWorker.CancellationPending ? -cc - 1 : cc + 1;
		}

		private void WorkerProgressChanged(object sender, ProgressChangedEventArgs e)
		{
			if (e.ProgressPercentage == -1)
			{
				progressLabel.Text = string.Format("Examing directory: {0}", e.UserState);
			}
			else if (e.ProgressPercentage == -2)
			{
				lbError.Items.Add(new ValidateErrorInfo(_currentFileName, (Exception)e.UserState));
			}
			else
			{
				_currentFileName = (string)e.UserState;
				progressLabel.Text = string.Format("Examing file: {0}", e.UserState);
				progressBar.Value = e.ProgressPercentage;
			}
		}

		private void RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			progressLabel.Text = string.Format("{0}: {1} error(s) in {2} file(s)", (int)e.Result < 0 ? "Stopped" : "Complete", lbError.Items.Count, Math.Abs((int)e.Result) - 1);
			progressBar.Visible = false;
			btnStop.Enabled = false;
			btnClose.Enabled = true;
			if (e.Error != null)
			{
				MessageBox.Show(e.Error.ToString(), Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void BtnStopClick(object sender, EventArgs e)
		{
			backgroundWorker.CancelAsync();
		}

		private void LbErrorSelectedIndexChanged(object sender, EventArgs e)
		{
			tbError.Text = null;
			if (lbError.SelectedItem != null)
			{
				ValidateErrorInfo ei = (ValidateErrorInfo)lbError.SelectedItem;
				tbError.AppendText(ei.Error.ToString());
				tbError.SelectionStart = 0;
				tbError.ScrollToCaret();
			}
		}

		private void ValidateFormShown(object sender, EventArgs e)
		{
			backgroundWorker.RunWorkerAsync();
		}

		#endregion
	}

	internal struct ValidateErrorInfo
	{
		#region Private fields

		private readonly string _fileName;
		private readonly Exception _error;

		#endregion

		#region Public constructor

		public ValidateErrorInfo(string fileName, Exception error)
		{
			_fileName = fileName;
			_error = error;
		}

		#endregion

		#region Public properties

		public string FileName
		{
			get
			{
				return _fileName;
			}
		}

		public Exception Error
		{
			get
			{
				return _error;
			}
		}

		#endregion
	}
}
