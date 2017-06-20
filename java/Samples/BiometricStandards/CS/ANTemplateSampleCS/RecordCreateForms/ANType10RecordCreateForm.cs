using System;
using Neurotec.Biometrics.Standards;
using Neurotec.IO;
using Neurotec.Images;

namespace Neurotec.Samples.RecordCreateForms
{
	public partial class ANType10RecordCreateForm : ANRecordCreateForm
	{
		#region Public constructor

		public ANType10RecordCreateForm()
		{
			InitializeComponent();

			foreach (object value in Enum.GetValues(typeof(ANImageType)))
			{
				imageTypeComboBox.Items.Add(value);
			}
			imageTypeComboBox.SelectedIndex = 0;
		}

		#endregion

		#region Private properties

		private ANImageType ImageType
		{
			get
			{
				return (ANImageType)imageTypeComboBox.SelectedItem;
			}
		}

		private string Smt
		{
			get
			{
				return smtTextBox.Text;
			}
		}

		#endregion

		#region Protected methods

		protected override ANRecord OnCreateRecord(ANTemplate template)
		{
			ANType10Record record;
			if (imageLoader.CreateFromImage)
			{
				record = new ANType10Record(ANTemplate.VersionCurrent, Idc, ImageType, imageLoader.Src,
					imageLoader.ScaleUnits, imageLoader.CompressionAlgorithm, Smt, imageLoader.Image);
			}
			else if (imageLoader.CreateFromData)
			{
				using (NImage image = NImage.FromMemory(imageLoader.ImageData))
				{
					record = new ANType10Record(ANTemplate.VersionCurrent, Idc, ImageType, imageLoader.Src,
						imageLoader.ScaleUnits, imageLoader.CompressionAlgorithm, Smt, image)
						{
							HorzLineLength = imageLoader.Hll,
							VertLineLength = imageLoader.Vll,
							HorzPixelScale = imageLoader.Hps,
							VertPixelScale = imageLoader.Vps,
							ColorSpace = imageLoader.Colorspace
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

		#endregion
	}
}
