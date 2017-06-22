using System;
using System.Windows.Forms;
using Neurotec.Biometrics.Standards;
using Neurotec.Images;
using Neurotec.IO;

namespace Neurotec.Samples.RecordCreateForms
{
	public partial class ANType8RecordCreateForm : ANImageBinaryRecordCreateForm
	{
		#region Private fields

		private ANPenVector[] _vectors = new ANPenVector[0];

		#endregion

		#region Public constructor

		public ANType8RecordCreateForm()
		{
			InitializeComponent();

			IsLowResolution = false;
			FromVectorsIr = IsrValue;

			fromVectorsPanel.Enabled = rbFromVectors.Checked;

			foreach (object value in Enum.GetValues(typeof(ANSignatureType)))
			{
				cbSignature.Items.Add(value);
			}
			if (cbSignature.Items.Count > 0)
				cbSignature.SelectedIndex = 0;
			else
				cbSignature.Enabled = false;
		}

		#endregion

		#region Protected methods

		protected override ANRecord OnCreateRecord(ANTemplate template)
		{
			ANType8Record record;
			if (CreateFromImage)
			{
				using (NImage image = Image)
				{
					record = new ANType8Record(ANTemplate.VersionCurrent, Idc, SignatureType,
						(ANSignatureRepresentationType)CompressionAlgorithm, IsrFlag, image);
				}
			}
			else if (CreateFromVector)
			{
				record = new ANType8Record(ANTemplate.VersionCurrent, Idc, SignatureType, Vectors);
			}
			else if (CreateFromData)
			{
				using (NImage image = NImage.FromMemory(ImageData))
				{
					record = new ANType8Record(ANTemplate.VersionCurrent, Idc, SignatureType,
						(ANSignatureRepresentationType)CompressionAlgorithm, IsrFlag, image)
						{
							HorzLineLength = Hll,
							VertLineLength = Vll
						};
				}
			}
			else
			{
				throw new NotSupportedException();
			}

			template.Records.Add(record);
			return record;
		}

		protected override Type GetCompressionFormatsType()
		{
			return typeof(ANSignatureRepresentationType);
		}

		#endregion

		#region Protected properties

		protected bool CreateFromVector
		{
			get
			{
				return rbFromVectors.Checked;
			}
		}

		protected uint FromVectorsIr
		{
			get
			{
				return (uint)Math.Round(resolutioEditBox.PpmValue);
			}
			set
			{
				resolutioEditBox.PpmValue = value;
			}
		}

		protected ANPenVector[] Vectors
		{
			get
			{
				return _vectors;
			}
			set
			{
				_vectors = value;
			}
		}

		#endregion

		#region Public properties

		public ANSignatureType SignatureType
		{
			get
			{
				return (ANSignatureType)cbSignature.SelectedItem;
			}
			set
			{
				cbSignature.SelectedItem = value;
			}
		}

		#endregion

		#region Private form events

		private void FromVectorsRadioButtonCheckedChanged(object sender, EventArgs e)
		{
			fromVectorsPanel.Enabled = rbFromVectors.Checked;
		}

		private void BtnEditVectorsClick(object sender, EventArgs e)
		{
			using (var dialog = new CreateANPenVectorArrayForm())
			{
				dialog.Vectors = Vectors;
				if (dialog.ShowDialog() == DialogResult.OK)
				{
					Vectors = dialog.Vectors;
				}
			}
		}

		#endregion
	}
}
