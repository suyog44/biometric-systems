using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Neurotec.Biometrics;
using Neurotec.Biometrics.Client;
using Neurotec.Gui;
using Neurotec.IO;

namespace Neurotec.Samples
{
	public partial class MainForm : Form, ITabController
	{
		#region Public constructor

		public MainForm()
		{
			InitializeComponent();
		}

		#endregion

		#region ITabController Members

		public void CreateNewSubjectTab(NSubject subject)
		{
			ShowTab(typeof(EditSubjectTab), true, true, subject);
		}

		public void OpenSubject()
		{
			using (var dialog = new OpenSubjectDialog())
			{
				if (dialog.ShowDialog() == DialogResult.OK)
				{
					try
					{
						NSubject result = (NSubject)LongActionDialog.ShowDialog(this, "Opening subject", new Func<string, ushort, ushort, NSubject>((fileName, formatOwner, formatType) =>
						{
							NSubject subject = NSubject.FromFile(fileName, formatOwner, formatType);
							NBiometricStatus status = Client.CreateTemplate(subject);
							if (status != NBiometricStatus.Ok && status != NBiometricStatus.None)
							{
								Utilities.ShowError("Failed to process template: {0}", status);
								return null;
							}
							return subject;
						}), dialog.FileName, dialog.FormatOwner, dialog.FormatType);

						if (result != null)
							ShowTab(typeof(EditSubjectTab), true, true, result);
					}
					catch (Exception ex)
					{
						Utilities.ShowError(ex);
					}
				}
			}
		}

		public void ShowSettings(params object[] args)
		{
			ShowTab(typeof(SettingsTab), false, true, args);
		}

		public bool ShowChangeDatabase()
		{
			int count = tabControl.TabPages.Count;
			if (count > 1)
			{
				if (!Utilities.ShowQuestion(this, "Changing database will close all currently opened tabs. Do you want to continue?"))
					return false;

				tabControl.SelectedIndex = 0;
				for (int i = 1; i < count; i++)
				{
					tabControl.TabPages.RemoveAt(1);
				}
			}

			using (var dialog = new ChangeDatabaseDialog())
			{
				if (dialog.ShowDialog() == DialogResult.OK)
				{
					if (Client != null) Client.Dispose();
					Client = dialog.BiometricClient;
					NBiometricOperations operations = Client.LocalOperations;
					if (Client.RemoteConnections.Count > 0)
						operations |= Client.RemoteConnections[0].Operations;
					tsbGetSubject.Enabled = (operations & NBiometricOperations.Get) == NBiometricOperations.Get;
					return true;
				}
			}
			return false;
		}

		public void ShowAbout()
		{
			AboutBox.Show(this);
		}

		public void ShowTab(Type tabType, bool alwaysCreateNew, bool canClose, params object[] args)
		{
			if (tabType == null || !tabType.IsSubclassOf(typeof(TabPageContentBase))) throw new ArgumentException("tabType");
			if (!alwaysCreateNew)
			{
				CloseableTabPage tab = tabControl.TabPages
					.OfType<CloseableTabPage>()
					.FirstOrDefault(x => x.Content != null && x.Content.GetType() == tabType);
				if (tab != null)
				{
					tab.Content.SetParams(args);
					tabControl.SelectedTab = tab;
					return;
				}
			}

			TabPageContentBase content = (TabPageContentBase)Activator.CreateInstance(tabType);
			CloseableTabPage page = new CloseableTabPage { Content = content, CanClose = canClose };
			content.Dock = DockStyle.Fill;
			content.TabController = this;
			content.SetParams(args);
			page.DataBindings.Add("Text", content, "TabName");
			tabControl.TabPages.Add(page);
			tabControl.SelectedTab = page;
		}

		public void CloseTab(TabPageContentBase tab)
		{
			CloseableTabPage owner = tabControl.TabPages
				.OfType<CloseableTabPage>()
				.First(x => x.Content == tab);

			if (tabControl.TabPages.Count > tabControl.LastPageIndex)
			{
				tabControl.SelectedIndex = tabControl.LastPageIndex;
			}
			else if (tabControl.TabPages.Count > 0)
			{
				tabControl.SelectedIndex = 0;
			}
			tabControl.TabPages.Remove(owner);
		}

		public NBiometricClient Client { get; set; }

		#endregion

		#region Private form events

		private void TsbAboutClick(object sender, EventArgs e)
		{
			ShowAbout();
		}

		private void TsbSettingsClick(object sender, EventArgs e)
		{
			ShowSettings();
		}

		private void TsbOpenSubjectClick(object sender, EventArgs e)
		{
			OpenSubject();
		}

		private void TsbNewSubjectClick(object sender, EventArgs e)
		{
			CreateNewSubjectTab(new NSubject());
		}

		private void TbsChangeDatabaseClick(object sender, EventArgs e)
		{
			ShowChangeDatabase();
		}

		private void TsbGetSubjectClick(object sender, EventArgs e)
		{
			using (GetSubjectDialog dialog = new GetSubjectDialog { Client = this.Client })
			{
				if (dialog.ShowDialog() == DialogResult.OK)
				{
					try
					{
						NSubject subject = RecreateSubject(dialog.Subject);
						CreateNewSubjectTab(subject);
					}
					catch (Exception ex)
					{
						Utilities.ShowError(ex);
					}
				}
			}
		}

		private void MainFormLoad(object sender, EventArgs e)
		{
			ShowTab(typeof(StartTab), true, false);
		}

		private void MainFormFormClosing(object sender, FormClosingEventArgs e)
		{
			tabControl.TabPages.Clear();
			if (Client != null)
			{
				Client.Dispose();
				Client = null;
			}
		}

		private void MainFormShown(object sender, EventArgs e)
		{
			if (!ShowChangeDatabase())
				Close();
		}

		#endregion

		#region Private methods

		private NSubject RecreateSubject(NSubject subject)
		{
			SampleDbSchema schema = SettingsManager.CurrentSchema;
			bool hasSchema = !schema.IsEmpty;
			int[] galeryRecordCounts = null;
			NSubject resultSubject = subject;

			if (hasSchema)
			{
				NPropertyBag bag = new NPropertyBag();
				subject.CaptureProperties(bag);

				if (!string.IsNullOrEmpty(schema.EnrollDataName) && bag.ContainsKey(schema.EnrollDataName))
				{
					NBuffer templateBuffer = subject.GetTemplateBuffer();
					NBuffer enrollData = (NBuffer)bag[schema.EnrollDataName];
					resultSubject = EnrollDataSerializer.Deserialize(templateBuffer, enrollData, out galeryRecordCounts);

					List<string> allProperties = bag.Select(x => x.Key).ToList();
					List<string> allowedProperties = Enumerable.Union(schema.BiographicData.Elements, schema.CustomData.Elements)
						.Select(x => x.Name)
						.ToList();
					foreach (var name in allProperties.Where(x => !allowedProperties.Contains(x)))
					{
						bag.Remove(name);
					}
					bag.ApplyTo(resultSubject);
					resultSubject.Id = subject.Id;
				}
				if (!string.IsNullOrEmpty(schema.GenderDataName) && bag.ContainsKey(schema.GenderDataName))
				{
					string genderString = (string)bag[schema.GenderDataName];
					resultSubject.SetProperty(schema.GenderDataName, Enum.Parse(typeof(NGender), genderString));
				}

			}

			return resultSubject;
		}

		#endregion

		#region Protected methods

		protected override void OnResize(EventArgs e)
		{
			base.OnResize(e);

			if (WindowState == FormWindowState.Maximized)
			{
				Update();
			}
		}

		#endregion
	}
}
