using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Neurotec.Biometrics;
using Neurotec.Biometrics.Client;

namespace Neurotec.Samples
{
	public partial class DababaseOperationTab : Neurotec.Samples.TabPageContentBase
	{
		#region Public constructor

		public DababaseOperationTab()
		{
			InitializeComponent();
			DoubleBuffered = true;
			TabName = "DatabaseOperationTab";
		}

		#endregion

		#region Public methods

		public override void SetParams(params object[] parameters)
		{
			if (parameters == null || parameters.Length != 2) throw new ArgumentException("parameters");

			_subject = (NSubject)parameters[0];
			_operation = (NBiometricOperations)parameters[1];
			_biometricClient = TabController.Client;

			base.SetParams(parameters);
		}

		public override void OnTabAdded()
		{
			switch (_operation)
			{
				case NBiometricOperations.Enroll:
				case NBiometricOperations.Verify:
				case NBiometricOperations.Update:
					TabName = string.Format("{0}: {1}", _operation, _subject.Id);
					break;
				case NBiometricOperations.EnrollWithDuplicateCheck:
					TabName = string.Format("Enroll: {0}", _subject.Id);
					break;
				case NBiometricOperations.Identify:
					TabName = "Identify";
					break;
				default:
					throw new ArgumentException("parameters");
			}

			if (TabName.Length > 30)
				TabName = TabName.Substring(0, 30) + "...";

			ShowError(null);

			_biometricClient = TabController.Client;
			NBiometricTask task = _biometricClient.CreateTask(_operation, _subject);
			_biometricClient.BeginPerformTask(task, OnTaskCompleted, null);

			base.OnTabAdded();
		}

		#endregion

		#region Private fields

		private NSubject _subject;
		private NBiometricOperations _operation;
		private NBiometricClient _biometricClient;

		#endregion

		#region Private methods

		private void OnTaskCompleted(IAsyncResult result)
		{
			if (InvokeRequired)
			{
				BeginInvoke(new AsyncCallback(OnTaskCompleted), result);
			}
			else
			{
				try
				{
					HideProgressbar();

					NBiometricTask task = _biometricClient.EndPerformTask(result);
					NBiometricStatus status = task.Status;
					lblStatus.Text = string.Format("{0}: {1}", _operation, status);
					lblStatus.BackColor = status == NBiometricStatus.Ok ? Color.Green : Color.Red;

					if (task.Error != null)
					{
						ShowError(task.Error);
					}
					else
					{
						if (_operation != NBiometricOperations.Enroll && _operation != NBiometricOperations.Update &&
							(status == NBiometricStatus.Ok || status == NBiometricStatus.DuplicateFound))
						{
							var details = _subject.MatchingResults.ToArray().Reverse();
							if (details.Count() > 0)
							{
								bool showLink = SettingsManager.ConnectionType != ConnectionType.RemoteMatchingServer;
								foreach (var item in details)
								{
									var view = new MatchingResultView()
									{
										MatchingThreshold = _biometricClient.MatchingThreshold,
										LinkEnabled = showLink,
										Result = item
									};
									view.LinkActivated += new EventHandler(MatchingResultViewLinkActivated);
									flowLayoutPanel.Controls.Add(view);
								}
							}
						}
					}
				}
				catch (Exception ex)
				{
					lblStatus.Text = string.Format("{0}: {1}", _operation, "Error");
					lblStatus.BackColor = Color.Red;
					ShowError(ex);
				}
			}

		}

		private void ShowError(Exception ex)
		{
			int errorIndex = 1;
			int whiteSpaceIndex = tableLayoutPanel.RowCount - 2;
			if (ex != null)
			{
				rtbError.Text = ex.ToString();
				tableLayoutPanel.RowStyles[errorIndex].SizeType = SizeType.Percent;
				tableLayoutPanel.RowStyles[errorIndex].Height = 100;
				tableLayoutPanel.RowStyles[whiteSpaceIndex].SizeType = SizeType.Absolute;
				tableLayoutPanel.RowStyles[whiteSpaceIndex].Height = 0;
			}
			else
			{
				tableLayoutPanel.RowStyles[errorIndex].SizeType = SizeType.Absolute;
				tableLayoutPanel.RowStyles[errorIndex].Height = 0;
				tableLayoutPanel.RowStyles[whiteSpaceIndex].SizeType = SizeType.Percent;
				tableLayoutPanel.RowStyles[whiteSpaceIndex].Height = 100;
			}
		}

		private void HideProgressbar()
		{
			int index = tableLayoutPanel.RowCount - 1;
			tableLayoutPanel.RowStyles[index].SizeType = SizeType.Absolute;
			tableLayoutPanel.RowStyles[index].Height = 0;
		}

		private void MatchingResultViewLinkActivated(object sender, EventArgs e)
		{
			MatchingResultView view = (MatchingResultView)sender;
			TabController.ShowTab(typeof(MatchingResultTab), true, true, _subject, new NSubject { Id = view.Result.Id });
		}

		#endregion
	}
}
