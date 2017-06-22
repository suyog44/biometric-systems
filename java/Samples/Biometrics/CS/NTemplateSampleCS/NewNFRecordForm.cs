using System.Windows.Forms;

namespace Neurotec.Samples
{
	public partial class NewNFRecordForm : Form
	{
		public NewNFRecordForm()
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

		public ushort HorzResolution
		{
			get
			{
				return (ushort)nudHorzResolution.Value;
			}
			set
			{
				nudHorzResolution.Value = value;
			}
		}

		public ushort VertResolution
		{
			get
			{
				return (ushort)nudVertResolution.Value;
			}
			set
			{
				nudVertResolution.Value = value;
			}
		}
	}
}
