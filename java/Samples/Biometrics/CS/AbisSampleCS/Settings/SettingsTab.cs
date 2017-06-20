using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Neurotec.Biometrics;

namespace Neurotec.Samples
{
	public partial class SettingsTab : TabPageContentBase, IPageController
	{
		#region Public constructor

		public SettingsTab()
		{
			InitializeComponent();
			TabName = "Settings";
		}

		#endregion

		#region Private fields

		private List<SettingsPageBase> _pages = new List<SettingsPageBase>();
		private SettingsPageBase _currentPage = null;

		#endregion

		#region Public methods

		public override void OnTabAdded()
		{
			if (listViewPages.SelectedItems.Count == 0)
				NavigateToStartPage();

			base.OnTabAdded();
		}

		public override void SetParams(params object[] parameters)
		{
			if (parameters != null && parameters.Length == 1)
			{
				try
				{
					string param = parameters[0].ToString();
					Type type = Type.GetType(param);
					NavigateToPage(type, TabController.Client);
				}
				catch
				{
				}
			}

			base.SetParams(parameters);
		}

		public void NavigateToPage(Type pageType, object navigationParam, params object[] arguments)
		{
			if (pageType == null || !pageType.IsSubclassOf(typeof(SettingsPageBase))) throw new ArgumentException("pageType");
			if (_currentPage != null && _currentPage.GetType() == pageType) return;

			if (_currentPage != null)
			{

				_currentPage.OnNavigatingFrom();
				_currentPage.NavigationParam = null;
				_currentPage = null;
				panelPage.Controls.Clear();
			}

			if (arguments == null || arguments.Length == 0) arguments = new[] { navigationParam };
			SettingsPageBase page = _pages.FirstOrDefault(x => x.GetType() == pageType);
			if (page == null)
			{
				page = (SettingsPageBase)Activator.CreateInstance(pageType);
				page.PageController = this;
				_pages.Add(page);
			}

			_currentPage = page;
			panelPage.Controls.Add(page);

			page.NavigationParam = navigationParam;
			page.OnNavigatedTo(arguments);

			foreach (ListViewItem item in listViewPages.Items)
			{
				if (item.Tag == pageType)
				{
					item.Selected = true;
					break;
				}
			}
		}

		public void NavigateToStartPage()
		{
			NavigateToPage(typeof(GeneralSettingsPage), TabController.Client);
		}

		#endregion

		#region Private form events

		private void ListViewPagesSelectedIndexChanged(object sender, EventArgs e)
		{
			var selected = listViewPages.SelectedItems;
			ListViewItem selectedItem = null;
			if (selected.Count > 0)
			{
				selectedItem = listViewPages.SelectedItems[0];
				NavigateToPage((Type)selectedItem.Tag, TabController.Client);
			}
		}

		private void BtnOkClick(object sender, EventArgs e)
		{
			if (_currentPage != null) _currentPage.SaveSettings();
			SettingsManager.SaveSettings(TabController.Client);
			TabController.CloseTab(this);
		}

		private void BtnCancelClick(object sender, EventArgs e)
		{
			SettingsManager.LoadSettings(TabController.Client);
			TabController.CloseTab(this);
		}

		private void BtnDefaultClick(object sender, EventArgs e)
		{
			if (_currentPage != null)
				_currentPage.DefaultSettings();
		}

		private void SettingsTabLoad(object sender, EventArgs e)
		{
			if (!DesignMode)
			{
				listViewPages.Items.Add(new ListViewItem("General") { Tag = typeof(GeneralSettingsPage) });
				if (LicensingTools.CanCreateFingerTemplate(TabController.Client.LocalOperations))
					listViewPages.Items.Add(new ListViewItem("Fingers") { Tag = typeof(FingersSettingsPage) });
				if (LicensingTools.CanCreateFaceTemplate(TabController.Client.LocalOperations))
					listViewPages.Items.Add(new ListViewItem("Faces") { Tag = typeof(FacesSettingsPage) });
				if (LicensingTools.CanCreateIrisTemplate(TabController.Client.LocalOperations))
					listViewPages.Items.Add(new ListViewItem("Irises") { Tag = typeof(IrisesSettingsPage) });
				if (LicensingTools.CanCreatePalmTemplate(TabController.Client.LocalOperations))
					listViewPages.Items.Add(new ListViewItem("Palms") { Tag = typeof(PalmsSettingsPage) });
				if (LicensingTools.CanCreateVoiceTemplate(TabController.Client.LocalOperations))
					listViewPages.Items.Add(new ListViewItem("Voices") { Tag = typeof(VoicesSettingsPage) });
			}
		}

		#endregion
	}
}
