using System;
using Neurotec.Biometrics.Standards;
using Neurotec.IO;
using Neurotec.Images;

namespace Neurotec.Samples.RecordCreateForms
{
	public partial class ANType15RecordCreateForm : ANRecordCreateForm
	{
		#region Public constructor

		public ANType15RecordCreateForm()
		{
			InitializeComponent();
		}

		#endregion

		#region Protected methods

		protected override ANRecord OnCreateRecord(ANTemplate template)
		{
			ANType15Record record;
			if (imageLoader.CreateFromImage)
			{
				record = new ANType15Record(ANTemplate.VersionCurrent, Idc, imageLoader.Src, imageLoader.ScaleUnits,
					imageLoader.CompressionAlgorithm, imageLoader.Image);
			}
			else if (imageLoader.CreateFromData)
			{
				using (NImage image = NImage.FromMemory(imageLoader.ImageData))
				{
					record = new ANType15Record(ANTemplate.VersionCurrent, Idc, imageLoader.Src, imageLoader.ScaleUnits,
						imageLoader.CompressionAlgorithm, image)
					{
						HorzLineLength = imageLoader.Hll,
						VertLineLength = imageLoader.Vll,
						HorzPixelScale = imageLoader.Hps,
						VertPixelScale = imageLoader.Vps,
						BitsPerPixel = imageLoader.Bpx
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
