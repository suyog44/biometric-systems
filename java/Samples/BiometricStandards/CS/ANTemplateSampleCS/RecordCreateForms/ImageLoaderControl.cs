using System;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;
using Neurotec.Biometrics.Standards;
using Neurotec.Images;

namespace Neurotec.Samples.RecordCreateForms
{
	public partial class ImageLoaderControl : UserControl
	{
		#region Private members

		private bool _hasBpx;
		private bool _hasColorspace;

		#endregion

		#region Public constructor

		public ImageLoaderControl()
		{
			InitializeComponent();

			foreach (object value in Enum.GetValues(typeof(BdifScaleUnits)))
			{
				cbScaleUnits.Items.Add(value);
			}
			cbScaleUnits.SelectedIndex = 0;

			foreach (object value in Enum.GetValues(typeof(ANImageCompressionAlgorithm)))
			{
				cbCompressionAlgorithm.Items.Add(value);
			}
			cbCompressionAlgorithm.SelectedIndex = 0;

			foreach (object value in Enum.GetValues(typeof(ANImageColorSpace)))
			{
				cbColorSpace.Items.Add(value);
			}
			cbColorSpace.SelectedIndex = 0;

			nudHll.Maximum = nudVll.Maximum = ushort.MaxValue;
			nudHps.Maximum = nudVps.Maximum = ushort.MaxValue;

			panelFromImage.Enabled = rbFromImage.Checked;
			panelFromData.Enabled = rbFromData.Checked;
		}

		#endregion

		#region Public properties

		public string Src
		{
			get
			{
				return tbSrc.Text;
			}
			set
			{
				tbSrc.Text = value;
			}
		}

		public BdifScaleUnits ScaleUnits
		{
			get
			{
				return (BdifScaleUnits)cbScaleUnits.SelectedItem;
			}
			set
			{
				cbScaleUnits.SelectedItem = value;
			}
		}

		public ANImageCompressionAlgorithm CompressionAlgorithm
		{
			get
			{
				return (ANImageCompressionAlgorithm)cbCompressionAlgorithm.SelectedItem;
			}
			set
			{
				cbCompressionAlgorithm.SelectedItem = value;
			}
		}

		[Browsable(false)]
		public bool CreateFromImage
		{
			get
			{
				return rbFromImage.Checked;
			}
		}

		[Browsable(false)]
		public bool CreateFromData
		{
			get
			{
				return rbFromData.Checked;
			}
		}

		public ushort Hll
		{
			get
			{
				return (ushort)nudHll.Value;
			}
			set
			{
				nudHll.Value = value;
			}
		}

		public ushort Vll
		{
			get
			{
				return (ushort)nudVll.Value;
			}
			set
			{
				nudVll.Value = value;
			}
		}

		public ushort Hps
		{
			get
			{
				return (ushort)nudHps.Value;
			}
			set
			{
				nudHps.Value = value;
			}
		}

		public ushort Vps
		{
			get
			{
				return (ushort)nudVps.Value;
			}
			set
			{
				nudVps.Value = value;
			}
		}

		public bool HasBpx
		{
			get
			{
				return _hasBpx;
			}
			set
			{
				_hasBpx = value;
			}
		}

		public byte Bpx
		{
			get
			{
				return (byte)nudBpx.Value;
			}
			set
			{
				nudBpx.Value = value;
			}
		}

		public bool HasColorspace
		{
			get
			{
				return _hasColorspace;
			}
			set
			{
				_hasColorspace = value;
			}
		}

		public ANImageColorSpace Colorspace
		{
			get
			{
				return (ANImageColorSpace)cbColorSpace.SelectedItem;
			}
			set
			{
				cbColorSpace.SelectedItem = value;
			}
		}

		[Browsable(false)]
		public NImage Image
		{
			get
			{
				return NImage.FromFile(tbImagePath.Text);
			}
		}

		[Browsable(false)]
		public byte[] ImageData
		{
			get
			{
				return File.ReadAllBytes(imageDataPathTextBox.Text);
			}
		}

		#endregion

		#region Private methods

		private void BtnBrowseImageClick(object sender, EventArgs e)
		{
			imageOpenFileDialog.Filter = NImages.GetOpenFileFilterString(true, true);
			if (imageOpenFileDialog.ShowDialog() == DialogResult.OK)
			{
				tbImagePath.Text = imageOpenFileDialog.FileName;
			}
		}

		private void BtnBrowseImageDataClick(object sender, EventArgs e)
		{
			if (imageDataOpenFileDialog.ShowDialog() == DialogResult.OK)
			{
				imageDataPathTextBox.Text = imageDataOpenFileDialog.FileName;
			}
		}

		private void RbFomImageCheckedChanged(object sender, EventArgs e)
		{
			panelFromImage.Enabled = rbFromImage.Checked;
		}

		private void RbFromDataCheckedChanged(object sender, EventArgs e)
		{
			panelFromData.Enabled = rbFromData.Checked;

			bpxLabel.Enabled = _hasBpx;
			nudBpx.Enabled = _hasBpx;
			colorspaceLabel.Enabled = _hasColorspace;
			cbColorSpace.Enabled = _hasColorspace;
		}

		private void TbSrcValidating(object sender, CancelEventArgs e)
		{
			if (tbSrc.Text.Length < ANAsciiBinaryRecord.MinSourceAgencyLength
				|| tbSrc.Text.Length > ANAsciiBinaryRecord.MaxSourceAgencyLengthV4)
			{
				errorProvider1.SetError(tbSrc, string.Format("Source agency field length must be between {0} and {1} characters",
					ANAsciiBinaryRecord.MinSourceAgencyLength, ANAsciiBinaryRecord.MaxSourceAgencyLengthV4));
				e.Cancel = true;
			}
			else
			{
				errorProvider1.SetError(tbSrc, null);
			}
		}

		#endregion
	}
}
