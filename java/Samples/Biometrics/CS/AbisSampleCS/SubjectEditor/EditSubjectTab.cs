using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using Neurotec.Biometrics;
using Neurotec.Biometrics.Client;

namespace Neurotec.Samples
{
	public partial class EditSubjectTab : Neurotec.Samples.TabPageContentBase, IPageController
	{
		#region Public constructor

		public EditSubjectTab()
		{
			InitializeComponent();

			TabName = "Subject";
		}

		#endregion

		#region Private fields

		private NSubject _subject;
		private NBiometricClient _client;
		private List<PageBase> _pages = new List<PageBase>();
		private PageBase _currentPage = null;

		#endregion

		#region Public methods

		public override void OnTabAdded()
		{
			NBiometricType types = NBiometricType.None;
			if (LicensingTools.CanCreateFingerTemplate(_client.LocalOperations))
				types |= NBiometricType.Finger;
			if (LicensingTools.CanCreateFaceTemplate(_client.LocalOperations))
				types |= NBiometricType.Face;
			if (LicensingTools.CanCreateIrisTemplate(_client.LocalOperations))
				types |= NBiometricType.Iris;
			if (LicensingTools.CanCreatePalmTemplate(_client.LocalOperations))
				types |= NBiometricType.Palm;
			if (LicensingTools.CanCreateVoiceTemplate(_client.LocalOperations))
				types |= NBiometricType.Voice;
			subjectTree.AllowNew = types;

			NavigateToStartPage();
			subjectTree.PropertyChanged += SubjectTreePropertyChanged;
			base.OnTabAdded();
		}

		public override void OnTabLeave()
		{
			NavigateToStartPage();
			base.OnTabLeave();
		}

		public override void SetParams(params object[] parameters)
		{
			if (parameters == null || parameters.Length != 1) throw new ArgumentException("parameters");
			_subject = (NSubject)parameters[0];
			_client = TabController.Client;
			subjectTree.Subject = _subject;

			DataBindings.Clear();
			var binding = DataBindings.Add("TabName", _subject, "Id");
			binding.Format += (sender, e) =>
				{
					string value = e.Value == null ? "Subject" : string.Format("Subject: {0}", e.Value);
					if (value.Length > 30)
						value = value.Substring(0, 30) + "...";
					e.Value = value;
				};
		}

		public override void OnTabClose()
		{
			DataBindings.Clear();
			subjectTree.PropertyChanged -= SubjectTreePropertyChanged;
			subjectTree.Subject = null;

			if (_currentPage != null)
			{
				_currentPage.OnNavigatingFrom();
				_currentPage.NavigationParam = null;
			}
			pagePanel.Controls.Clear();
			foreach (var item in _pages)
			{
				item.Dispose();
			}
			_pages.Clear();
			if (_subject != null)
			{
				_subject.Dispose();
				_subject = null;
			}
		}

		#endregion

		#region Private methods

		private void SubjectTreePropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			if (e.PropertyName == "SelectedItem")
			{
				if (IsHandleCreated)
				{
					BeginInvoke(new MethodInvoker(OnSubjectTreeSelectedItemChanged));
				}
			}
		}

		private void OnSubjectTreeSelectedItemChanged()
		{
			var selected = subjectTree.SelectedItem;
			if (selected == null || selected.IsSubjectNode)
			{
				NavigateToStartPage();
			}
			else
			{
				if (selected.IsBiometricNode)
				{
					NavigateToPage(typeof(BiometricPreviewPage), selected);
				}
				else
				{
					Type pageType = null;
					switch (selected.BiometricType)
					{
						case NBiometricType.Face: pageType = typeof(CaptureFacePage); break;
						case NBiometricType.Finger: pageType = typeof(CaptureFingersPage); break;
						case NBiometricType.Iris: pageType = typeof(CaptureIrisPage); break;
						case NBiometricType.Palm: pageType = typeof(CapturePalmsPage); break;
						case NBiometricType.Voice: pageType = typeof(CaptureVoicePage); break;
						default: throw new NotImplementedException();
					}

					NavigateToPage(pageType, selected, _subject, _client);
				}
			}
		}

		public void NavigateToPage(Type pageType, object navigationParam, params object[] args)
		{
			if (_currentPage != null &&
					_currentPage.GetType() == pageType &&
					_currentPage.NavigationParam == navigationParam)
			{
				// Already in this page
				return;
			}

			if (_currentPage != null)
			{
				_currentPage.OnNavigatingFrom();
				_currentPage.NavigationParam = null;
				_currentPage = null;
				pagePanel.Controls.Clear();
			}

			if (args == null || args.Length == 0) args = new object[] { navigationParam };

			PageBase page = _pages.FirstOrDefault(x => x.GetType() == pageType);
			if (page == null)
			{
				page = (PageBase)Activator.CreateInstance(pageType);
				page.Dock = DockStyle.Fill;
				page.PageController = this;
				_pages.Add(page);
			}

			_currentPage = page;
			page.OnNavigatedTo(args);
			page.NavigationParam = navigationParam;
			pagePanel.Controls.Add(page);

			subjectTree.SelectedItem = subjectTree.GetNodeFor(navigationParam);
		}

		public void NavigateToStartPage()
		{
			NavigateToPage(typeof(SubjectStartPage), _subject);
		}

		#endregion
	}
}
