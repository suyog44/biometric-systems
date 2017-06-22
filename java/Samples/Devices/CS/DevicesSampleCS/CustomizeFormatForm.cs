using System;
using System.Windows.Forms;
using Neurotec.Media;

namespace Neurotec.Samples
{
	public partial class CustomizeFormatForm : Form
	{
		public CustomizeFormatForm()
		{
			InitializeComponent();
		}

		public static NMediaFormat CustomizeFormat(NMediaFormat mediaFormat)
		{
			if (mediaFormat == null) throw new ArgumentNullException("mediaFormat");
			var frm = new CustomizeFormatForm();
			var clone = (NMediaFormat)mediaFormat.Clone();
			frm.formatsPropertyGrid.SelectedObject = clone;
			if (frm.ShowDialog() == DialogResult.OK
				&& clone != mediaFormat)
			{
				return clone;
			}

			clone.Dispose();
			return null;
		}
	}
}
