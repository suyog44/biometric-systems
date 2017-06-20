using System;

namespace Neurotec.Samples
{
	public interface IPageController
	{
		void NavigateToPage(Type pageType, object navigationParam, params object[] arguments);
		void NavigateToStartPage();
		ITabController TabController { get; set; }
	}
}
