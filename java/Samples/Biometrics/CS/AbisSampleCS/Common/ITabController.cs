using System;
using Neurotec.Biometrics;
using Neurotec.Biometrics.Client;

namespace Neurotec.Samples
{
	public interface ITabController
	{
		void CreateNewSubjectTab(NSubject subject);
		void OpenSubject();
		void ShowSettings(params object[] navigationParams);
		void ShowAbout();
		bool ShowChangeDatabase();
		void ShowTab(Type tabType, bool alwaysCreateNew, bool canClose, params object[] args);
		void CloseTab(TabPageContentBase tab);

		NBiometricClient Client { get; set; }
	};
}
