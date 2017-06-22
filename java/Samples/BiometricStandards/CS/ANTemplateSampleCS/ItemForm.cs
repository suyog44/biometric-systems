using System.Windows.Forms;

namespace Neurotec.Samples
{
	public partial class ItemForm : Form
	{
		#region Public constructor

		public ItemForm()
		{
			InitializeComponent();
			OnIsReadOnlyChanged();
		}

		#endregion

		#region Private methods

		private void OnIsReadOnlyChanged()
		{
			btnOk.Visible = !tbValue.ReadOnly;
			btnCancel.Text = !tbValue.ReadOnly ? "Cancel" : "Close";
		}

		#endregion

		#region Public properties

		public string Value
		{
			get
			{
				return tbValue.Text;
			}
			set
			{
				tbValue.Text = value;
			}
		}

		public bool IsReadOnly
		{
			get
			{
				return tbValue.ReadOnly;
			}
			set
			{
				if (tbValue.ReadOnly != value)
				{
					tbValue.ReadOnly = value;
					OnIsReadOnlyChanged();
				}
			}
		}

		#endregion
	}
}
