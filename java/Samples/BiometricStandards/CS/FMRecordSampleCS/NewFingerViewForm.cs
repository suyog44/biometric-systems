using System;
using System.Media;
using System.Windows.Forms;

namespace Neurotec.Samples
{
	public partial class NewFingerViewForm : Form
	{
		#region Public constructor

		public NewFingerViewForm()
		{
			InitializeComponent();
		}

		#endregion

		#region Private variables

		private ushort _sizeX;
		private ushort _sizeY;
		private ushort _vertResolution;
		private ushort _horzResolution;

		#endregion

		#region Public properties

		public ushort SizeX { get { return _sizeX; } }
		public ushort SizeY { get { return _sizeY; } }
		public ushort VertResolution { get { return _vertResolution; } }
		public ushort HorzResolution { get { return _horzResolution; } }

		#endregion

		#region Private events

		private void numberBox_KeyPress(object sender, KeyPressEventArgs e)
		{
			ushort isNumber = 0;
			if (Char.IsControl(e.KeyChar))
			{
				e.Handled = false;
				return;
			}

			TextBox tb = sender as TextBox;
			if (!ushort.TryParse(tb.Text + e.KeyChar.ToString(), out isNumber))
			{
				e.Handled = true;
				SystemSounds.Beep.Play();
			}
		}

		private void btnOk_Click(object sender, EventArgs e)
		{
			if (!ushort.TryParse(tbSizeX.Text, out _sizeX) || !ushort.TryParse(tbSizeY.Text, out _sizeY) ||
				!ushort.TryParse(tbVertRes.Text, out _vertResolution) || !ushort.TryParse(tbHorRes.Text, out _horzResolution))
			{
				MessageBox.Show("Parameters can't be parsed!", Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
				DialogResult = DialogResult.None;
				return;
			}
		}

		#endregion
	}
}
