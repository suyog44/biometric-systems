using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace Neurotec.Samples.Controls
{
	public partial class LongTaskForm : Form
	{
		private readonly DoWorkEventHandler _callback;

		private object _argument;
		private RunWorkerCompletedEventArgs _results;
		private Exception _error;

		private LongTaskForm(string title, DoWorkEventHandler callback)
		{
			InitializeComponent();

			lblTitle.Text = title;
			_callback = callback;
		}

		public static RunWorkerCompletedEventArgs RunLongTask(string title, DoWorkEventHandler callback, object arg)
		{
			using (var frmLongTask = new LongTaskForm(title, callback))
			{
				frmLongTask._argument = arg;
				frmLongTask.ShowDialog();
				if (frmLongTask._error != null)
				{
					throw frmLongTask._error;
				}
				return frmLongTask._results;
			}
		}

		private delegate RunWorkerCompletedEventArgs RunLongTaskDel(string title, DoWorkEventHandler callback, object arg, Control ctrl);
		public static RunWorkerCompletedEventArgs RunLongTask(string title, DoWorkEventHandler callback, object arg, Control ctrl)
		{
			if (ctrl.InvokeRequired)
			{
				RunLongTaskDel del = RunLongTask;
				{
					IAsyncResult res = ctrl.BeginInvoke(del, title, callback, arg, ctrl);
					var args = (RunWorkerCompletedEventArgs)ctrl.EndInvoke(res);
					if (args.Error != null)
						throw args.Error;
					return args;
				}
			}
			using (var frmLongTask = new LongTaskForm(title, callback))
			{
				frmLongTask._argument = arg;
				frmLongTask.ShowDialog();
				if (frmLongTask._error != null)
				{
					return new RunWorkerCompletedEventArgs(frmLongTask._results, frmLongTask._error, false);

				}
				return frmLongTask._results;
			}
		}

		private void LongTaskForm_Shown(object sender, EventArgs e)
		{
			bwLongTask.RunWorkerAsync(_argument);
		}

		private void DoWork(object sender, DoWorkEventArgs e)
		{
			try
			{
				if (_callback != null)
				{
					_callback(sender, e);
				}
			}
			catch (Exception ex)
			{
				_error = ex;
			}
		}

		private void RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			_results = e;
			Close();
		}
	}
}
