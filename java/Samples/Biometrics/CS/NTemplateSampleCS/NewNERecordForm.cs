using System.Windows.Forms;

namespace Neurotec.Samples
{
	public partial class NewNERecordForm : Form
	{
		public NewNERecordForm()
		{
			InitializeComponent();
		}

		public ushort RecordWidth
		{
			get
			{
				return (ushort)nudWidth.Value;
			}
			set
			{
				nudWidth.Value = value;
			}
		}

		public ushort RecordHeight
		{
			get
			{
				return (ushort)nudHeight.Value;
			}
			set
			{
				nudHeight.Value = value;
			}
		}
	}
}
