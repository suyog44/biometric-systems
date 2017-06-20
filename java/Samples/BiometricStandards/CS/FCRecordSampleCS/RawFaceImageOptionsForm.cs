using System;
using System.ComponentModel;
using System.Windows.Forms;
using Neurotec.Biometrics.Standards;

namespace Neurotec.Samples
{
	public partial class RawFaceImageOptionsForm : AddFaceImageForm
	{
		public enum RawFaceImageOptionsFormMode
		{
			Load = 1,
			Save = 2
		}

		public RawFaceImageOptionsForm()
		{
			InitializeComponent();

			cbImageColorSpace.Items.AddRange(Enum.GetNames(typeof(FcrImageColorSpace)));
			cbImageColorSpace.SelectedIndex = 0;
		}

		private RawFaceImageOptionsFormMode _mode = RawFaceImageOptionsFormMode.Load;
		public RawFaceImageOptionsFormMode Mode
		{
			get
			{
				return _mode;
			}
			set
			{
				_mode = value;
				OnModeChanged();
			}
		}

		public FcrImageColorSpace ImageColorSpace
		{
			get
			{
				return (FcrImageColorSpace)Enum.Parse(typeof(FcrImageColorSpace), cbImageColorSpace.SelectedItem.ToString());
			}
			set
			{
				cbImageColorSpace.SelectedItem = Enum.GetName(typeof(FcrImageColorSpace), value);
			}
		}

		public ushort ImageWidth
		{
			get
			{
				return ushort.Parse(tbWidth.Text);
			}
			set
			{
				tbWidth.Text = value.ToString();
			}
		}

		public ushort ImageHeight
		{
			get
			{
				return ushort.Parse(tbHeight.Text);
			}
			set
			{
				tbHeight.Text = value.ToString();
			}
		}

		public byte VendorColorSpace
		{
			get
			{
				return byte.Parse(tbVendorImageColorSpace.Text);
			}
		}

		private void cbImageColorSpace_SelectedIndexChanged(object sender, EventArgs e)
		{
			if ((FcrImageColorSpace)Enum.Parse(typeof(FcrImageColorSpace), cbImageColorSpace.SelectedItem.ToString()) == FcrImageColorSpace.Vendor)
			{
				if (!tbVendorImageColorSpace.Enabled)
					tbVendorImageColorSpace.Enabled = true;
			}
			else
			{
				tbVendorImageColorSpace.Enabled = false;
				tbVendorImageColorSpace.Text = @"0";
			}
		}

		private void tbWidthHeight_Validating(object sender, CancelEventArgs e)
		{
			Control s = sender as Control;
			if (s == null) return;

			ushort result;
			if (ushort.TryParse(s.Text, out result))
			{
				errorProvider.SetError(s, string.Empty);
			}
			else
			{
				errorProvider.SetError(s, "Incorrect data entered. Data should be in range 0 to 65535.");
				e.Cancel = true;
			}
		}

		private void tbVendorImageColorSpace_Validating(object sender, CancelEventArgs e)
		{
			Control s = sender as Control;
			if (s == null) return;

			byte result;
			if (byte.TryParse(s.Text, out result))
			{
				errorProvider.SetError(s, string.Empty);
			}
			else
			{
				errorProvider.SetError(s, "Incorrect data entered. Data should be in range 0 to 255.");
				e.Cancel = true;
			}
		}

		private void OnModeChanged()
		{
			switch (Mode)
			{
				case RawFaceImageOptionsFormMode.Load:
					Text = @"Add face from data";
					break;
				case RawFaceImageOptionsFormMode.Save:
					Text = @"Save face as data";
					break;
			}
		}
	}
}
