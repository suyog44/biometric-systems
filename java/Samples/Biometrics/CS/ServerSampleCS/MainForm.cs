using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Neurotec.Biometrics.Client;
using Neurotec.Samples.Code;
using Neurotec.Samples.Connections;
using Neurotec.Samples.Controls;

namespace Neurotec.Samples
{
	public partial class MainForm : Form
	{
		enum Task
		{
			Enroll = 0,
			Deduplication = 1,
			SpeedTest = 2,
			Settings = 3,
		};

		#region Private fields

		private const string SampleTitle = "Server Sample";
		private readonly Color _selectedButtonColor = SystemColors.Highlight;
		private readonly Color _notSelectedButtonColor = SystemColors.InactiveCaption;
		private readonly List<ControlBase> _panels = new List<ControlBase>();

		private ControlBase _activePanel;
		private ITemplateLoader _templateLoader;
		private AcceleratorConnection _acceleratorConnection;
		private NBiometricClient _biometricClient;
		private NClusterBiometricConnection _biometricConnection;

		#endregion

		#region Public constructor

		public MainForm()
		{
			InitializeComponent();
			toolTip.SetToolTip(btnConnection, "Change connection settings to Server and/or database containing templates");
			toolTip.SetToolTip(btnTestSpeed, "Test Megamatcher Accelerator matching speed");
			toolTip.SetToolTip(btnEnroll, "Enroll templates to Megamatcher Accelerator");
			toolTip.SetToolTip(btnDeduplication, "Perform template deduplication on Megamatcher Accelerator");
		}

		#endregion

		#region Private form events

		private void MainFormLoad(object sender, EventArgs e)
		{
			ControlBase panel = new EnrollPanel();
			panel.Dock = DockStyle.Fill;
			_panels.Add(panel);
			panel.Focus();

			panel = new DeduplicationPanel { Dock = DockStyle.Fill };
			_panels.Add(panel);

			panel = new TestSpeedPanel { Dock = DockStyle.Fill };
			_panels.Add(panel);

			panel = new SettingsPanel { Dock = DockStyle.Fill };
			_panels.Add(panel);

			if (!ShowConnectionSettings())
				Close();
			foreach (var tmpPanel in _panels)
			{
				tmpPanel.BiometricClient = _biometricClient;
			}
		}

		private void BtnConnectionClick(object sender, EventArgs e)
		{
			if (_activePanel.IsBusy)
			{
				if (Utilities.ShowQuestion("Action in progress. Stop current action?"))
				{
					_activePanel.Cancel();
				}
				else return;
			}
			if (ShowConnectionSettings())
			{
				_activePanel.TemplateLoader = _templateLoader;
				_activePanel.Accelerator = _acceleratorConnection;
				_activePanel.BiometricClient = _biometricClient;
			}
		}

		private void BtnTestSpeedClick(object sender, EventArgs e)
		{
			ShowPanel(Task.SpeedTest);
		}

		private void BtnEnrollClick(object sender, EventArgs e)
		{
			ShowPanel(Task.Enroll);
		}

		private void MainFormVisibleChanged(object sender, EventArgs e)
		{
			ShowPanel(Task.Deduplication);
		}

		private void BtnDeduplicationClick(object sender, EventArgs e)
		{
			ShowPanel(Task.Deduplication);
		}

		private void BtnSettingsClick(object sender, EventArgs e)
		{
			ShowPanel(Task.Settings);
		}

		private void MainFormFormClosing(object sender, FormClosingEventArgs e)
		{
			if (_activePanel != null && _activePanel.IsBusy)
			{
				_activePanel.Cancel();
				Text = string.Format("{0}: Closing, please wait ...", SampleTitle);
				splitContainer.Panel1.Enabled = false;
				while (_activePanel.IsBusy)
				{
					Application.DoEvents();
				}
			}
		}

		#endregion

		#region Private methods

		private bool ShowConnectionSettings()
		{
			var dialog = new ConnectionForm { Db = _templateLoader as DatabaseConnection };
			if (dialog.ShowDialog(this) == DialogResult.OK)
			{
				_biometricClient = new NBiometricClient();
				_biometricConnection = new NClusterBiometricConnection(dialog.Server, dialog.ClientPort, dialog.AdminPort);
				_biometricClient.RemoteConnections.Add(_biometricConnection);
				if (dialog.IsAccelerator)
				{
					_acceleratorConnection = new AcceleratorConnection(dialog.Server, dialog.UserName, dialog.Password);
				}
				else
				{
					_acceleratorConnection = null;
				}

				if (!dialog.UseDb)
				{
					_templateLoader = new DirectoryEnumerator(dialog.TemplateDir);
				}
				else
				{
					_templateLoader = dialog.Db;
				}
				return true;
			}
			return false;
		}

		private void ShowPanel(Task task, bool force)
		{
			var index = (int)task;
			if (_activePanel == _panels[index]) return;
			if (!force && _activePanel != null && _activePanel.IsBusy)
			{
				if (Utilities.ShowQuestion("Action in progress. Stop current action?"))
				{
					_activePanel.Cancel();
				}
				else return;
			}

			_activePanel = _panels[index];

			btnEnroll.BackColor = index == 0 ? _selectedButtonColor : _notSelectedButtonColor;
			btnDeduplication.BackColor = index == 1 ? _selectedButtonColor : _notSelectedButtonColor;
			btnTestSpeed.BackColor = index == 2 ? _selectedButtonColor : _notSelectedButtonColor;
			btnSettings.BackColor = index == 3 ? _selectedButtonColor : _notSelectedButtonColor;

			_activePanel.TemplateLoader = _templateLoader;
			_activePanel.Accelerator = _acceleratorConnection;
			_activePanel.BiometricClient = _biometricClient;
			splitContainer.Panel2.Controls.Clear();
			splitContainer.Panel2.Controls.Add(_activePanel);

			string title = _activePanel.GetTitle();
			Text = string.IsNullOrEmpty(title) ? SampleTitle : string.Format("{0}: {1}", SampleTitle, title);
		}

		private void ShowPanel(Task task)
		{
			ShowPanel(task, false);
		}

		#endregion
	}
}
