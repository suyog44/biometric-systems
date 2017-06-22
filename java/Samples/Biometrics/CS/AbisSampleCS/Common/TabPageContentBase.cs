using System.ComponentModel;
using System.Windows.Forms;

namespace Neurotec.Samples
{
	public partial class TabPageContentBase : UserControl, INotifyPropertyChanged
	{
		#region Public constructor

		public TabPageContentBase()
		{
			InitializeComponent();
		}

		#endregion

		#region Private fields

		private string _tabName = "Tab";

		#endregion

		#region Public properties

		public ITabController TabController { get; set; }

		public string TabName
		{
			get { return _tabName; }
			set
			{
				if (_tabName != value)
				{
					_tabName = value;
					if (PropertyChanged != null)
					{
						PropertyChanged(this, new PropertyChangedEventArgs("TabName"));
					}
				}
			}
		}

		#endregion

		#region Public virtual methods

		public virtual void OnTabAdded()
		{
		}

		public virtual void OnTabEnter()
		{
		}

		public virtual void OnTabLeave()
		{
		}

		public virtual void OnTabClose()
		{
		}

		public virtual void SetParams(params object[] parameters)
		{
		}

		#endregion

		#region INotifyPropertyChanged Members

		public event PropertyChangedEventHandler PropertyChanged;

		#endregion
	}
}
