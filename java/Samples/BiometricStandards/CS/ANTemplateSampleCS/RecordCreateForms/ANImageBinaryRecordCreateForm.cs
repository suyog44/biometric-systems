using System;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;
using Neurotec.Biometrics.Standards;
using Neurotec.Images;

namespace Neurotec.Samples.RecordCreateForms
{
	public partial class ANImageBinaryRecordCreateForm : ANRecordCreateForm
	{
		#region Private fields

		private bool _isLowResolution;

		#endregion

		#region Public constructor

		public ANImageBinaryRecordCreateForm()
		{
			InitializeComponent();

			Type compressionFormatsType = GetCompressionFormatsType();
			if (compressionFormatsType != null)
			{
				foreach (object value in Enum.GetValues(compressionFormatsType))
				{
					cbCompressionAlgorithm.Items.Add(value);
				}
				if (cbCompressionAlgorithm.Items.Count > 0)
				{
					cbCompressionAlgorithm.SelectedIndex = 0;
				}
			}
			else
			{
				cbCompressionAlgorithm.Enabled = false;
			}

			panelFromImage.Enabled = rbFromImage.Checked;
			panelFromData.Enabled = rbFromData.Checked;
		}

		#endregion

		#region Public properties

		public bool IsLowResolution
		{
			get
			{
				return _isLowResolution;
			}
			set
			{
				_isLowResolution = value;

				IsrValue = ANType1Record.MinScanningResolution;
				Ir = IsrValue / (value ? 2u : 1u);
			}
		}

		public bool IsrFlag
		{
			get
			{
				return chbIsrFlag.Checked;
			}
		}

		public uint IsrValue
		{
			get
			{
				return (uint)Math.Round(isrResolutionEditBox.PpmValue);
			}
			set
			{
				isrResolutionEditBox.PpmValue = value;
			}
		}

		public double IsrValuePpi
		{
			get
			{
				return Math.Round(isrResolutionEditBox.PpiValue);
			}
			set
			{
				isrResolutionEditBox.PpiValue = value;
			}
		}

		public object CompressionAlgorithm
		{
			get
			{
				return cbCompressionAlgorithm.SelectedItem;
			}
			set
			{
				cbCompressionAlgorithm.SelectedItem = value;
			}
		}

		public bool CreateFromImage
		{
			get
			{
				return rbFromImage.Checked;
			}
		}

		public bool CreateFromData
		{
			get
			{
				return rbFromData.Checked;
			}
		}

		public NImage Image
		{
			get
			{
				return NImage.FromFile(tbImagePath.Text);
			}
		}

		public ushort Hll
		{
			get
			{
				return ushort.Parse(tbHll.Text);
			}
		}

		public ushort Vll
		{
			get
			{
				return ushort.Parse(tbVll.Text);
			}
		}

		public uint Ir
		{
			get
			{
				return (uint)Math.Round(irResolutionEditBox.PpmValue);
			}
			set
			{
				irResolutionEditBox.PpmValue = value;
			}
		}

		public double IrValuePpi
		{
			get
			{
				return Math.Round(irResolutionEditBox.PpiValue);
			}
			set
			{
				irResolutionEditBox.PpiValue = value;
			}
		}

		public byte VendorCA
		{
			get
			{
				return byte.Parse(tbVendorCa.Text);
			}
		}

		public byte[] ImageData
		{
			get
			{
				return File.ReadAllBytes(tbImageDataPath.Text);
			}
		}

		#endregion

		#region Protected methods

		protected virtual Type GetCompressionFormatsType()
		{
			return null;
		}

		#endregion

		#region Private form events

		private void RbFromImageCheckedChanged(object sender, EventArgs e)
		{
			panelFromImage.Enabled = rbFromImage.Checked;
		}

		private void RbFromDataCheckedChanged(object sender, EventArgs e)
		{
			panelFromData.Enabled = rbFromData.Checked;
		}

		private void BtnBrowseImageClick(object sender, EventArgs e)
		{
			imageOpenFileDialog.Filter = NImages.GetOpenFileFilterString(true, true);
			if (imageOpenFileDialog.ShowDialog() == DialogResult.OK)
			{
				tbImagePath.Text = imageOpenFileDialog.FileName;
				try
				{
					using (NImage image = Image)
					{
						IsrValuePpi = image.HorzResolution * (IsLowResolution ? 2 : 1);
						IrValuePpi = image.HorzResolution;
					}
				}
				catch
				{
					MessageBox.Show("Could not load image from specified file.");
				}
			}
		}

		private void BrowseImageDataClick(object sender, EventArgs e)
		{
			imageOpenFileDialog.Filter = NImages.GetOpenFileFilterString(true, true);
			if (imageOpenFileDialog.ShowDialog() == DialogResult.OK)
			{
				tbImageDataPath.Text = imageOpenFileDialog.FileName;
				try
				{
					using (NImage image = NImage.FromFile(tbImageDataPath.Text))
					{
						IsrValuePpi = image.HorzResolution * (IsLowResolution ? 2 : 1);
						IrValuePpi = image.HorzResolution;
					}
				}
				catch
				{
				}
			}
		}

		private void ImageScanningResolutionEditBoxValidating(object sender, CancelEventArgs e)
		{
			uint value;
			try
			{
				value = IsrValue;
			}
			catch
			{
				errorProvider.SetError(isrResolutionEditBox, "Image scanning resolution value is invalid.");
				e.Cancel = true;
				return;
			}

			errorProvider.SetError(isrResolutionEditBox, null);
		}

		private void TbHllValidating(object sender, CancelEventArgs e)
		{
			try
			{
				ushort value = Hll;
				errorProvider.SetError(tbHll, null);
			}
			catch
			{
				errorProvider.SetError(tbHll, "Entered value is invalid.");
				e.Cancel = true;
			}
		}

		private void TbVllValidating(object sender, CancelEventArgs e)
		{
			try
			{
				ushort value = Vll;
				errorProvider.SetError(tbVll, null);
			}
			catch
			{
				errorProvider.SetError(tbVll, "Entered value is invalid.");
				e.Cancel = true;
			}
		}

		private void NativeScanningResolutionEditBoxValidating(object sender, CancelEventArgs e)
		{
			try
			{
				uint value = Ir;
				errorProvider.SetError(irResolutionEditBox, null);
			}
			catch
			{
				errorProvider.SetError(irResolutionEditBox, "Entered value is invalid.");
				e.Cancel = true;
			}
		}

		private void TbVendorCaValidating(object sender, CancelEventArgs e)
		{
			try
			{
				byte value = VendorCA;
				errorProvider.SetError(tbVendorCa, null);
			}
			catch
			{
				errorProvider.SetError(tbVendorCa, "Entered value is invalid.");
				e.Cancel = true;
			}
		}

		private void ANImageBinaryRecordCreateFormClosing(object sender, FormClosingEventArgs e)
		{
			if (DialogResult != DialogResult.OK)
			{
				return;
			}

			try
			{
				if (CreateFromImage)
				{
					NImage image = Image;
					image.Dispose();
				}
				errorProvider.SetError(tbImagePath, null);
			}
			catch
			{
				errorProvider.SetError(tbImagePath, "Could not load image from specified file.");
			}

			try
			{
				if (CreateFromData)
				{
					byte[] data = ImageData;
				}
				errorProvider.SetError(tbImageDataPath, null);
			}
			catch
			{
				errorProvider.SetError(tbImageDataPath, "Could not load image data from specified file.");
			}
		}

		private void CbCompressionAlgorithmSelectedIndexChanged(object sender, EventArgs e)
		{
			vendorCaLabel.Enabled = tbVendorCa.Enabled = CompressionAlgorithm.Equals(ANImageCompressionAlgorithm.Vendor);
		}

		#endregion
	}
}
