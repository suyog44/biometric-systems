using System.Windows.Forms;

namespace Neurotec.Samples
{
	public partial class PageBase : UserControl
	{
		#region Public constructor

		public PageBase()
		{
			InitializeComponent();
		}

		#endregion

		#region Public virtual methods

		public virtual void OnNavigatedTo(params object[] args)
		{
			IsPageShown = true;
		}

		public virtual void OnNavigatingFrom()
		{
			IsPageShown = false;
		}

		#endregion

		#region Public proeprties

		public IPageController PageController { get; set; }
		public object NavigationParam { get; set; }
		public string PageName { get; set; }
		public bool IsPageShown { get; set; }

		#endregion
	}
}
