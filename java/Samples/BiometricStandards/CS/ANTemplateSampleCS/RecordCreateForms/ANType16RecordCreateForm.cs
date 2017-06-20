using System;
using Neurotec.Biometrics.Standards;
using Neurotec.IO;
using Neurotec.Images;

namespace Neurotec.Samples.RecordCreateForms
{
	public partial class ANType16RecordCreateForm : ANRecordCreateForm
	{
		#region Public constructor

		public ANType16RecordCreateForm()
		{
			InitializeComponent();

			tbUdi.MaxLength = ANType16Record.MaxUserDefinedImageLength;
		}

		#endregion

		#region Private properties

		private string Udi
		{
			get
			{
				return tbUdi.Text;
			}
		}

		#endregion

		#region Protected methods

		protected override ANRecord OnCreateRecord(ANTemplate template)
		{
			ANType16Record record;
			if (imageLoader.CreateFromImage)
			{
				record = new ANType16Record(ANTemplate.VersionCurrent, Idc, Udi, imageLoader.Src,
					imageLoader.ScaleUnits, imageLoader.CompressionAlgorithm, imageLoader.Image);
			}
			else if (imageLoader.CreateFromData)
			{
				using (NImage image = NImage.FromMemory(imageLoader.ImageData))
				{
					record = new ANType16Record(ANTemplate.VersionCurrent, Idc, Udi, imageLoader.Src,
						imageLoader.ScaleUnits, imageLoader.CompressionAlgorithm, image)
					{
						HorzLineLength = imageLoader.Hll,
						VertLineLength = imageLoader.Vll,
						HorzPixelScale = imageLoader.Hps,
						VertPixelScale = imageLoader.Vps,
						BitsPerPixel = imageLoader.Bpx,
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
