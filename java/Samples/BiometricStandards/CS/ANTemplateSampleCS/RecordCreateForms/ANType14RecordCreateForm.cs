using System;
using Neurotec.Biometrics.Standards;
using Neurotec.IO;
using Neurotec.Images;

namespace Neurotec.Samples.RecordCreateForms
{
	public partial class ANType14RecordCreateForm : ANRecordCreateForm
	{
		#region Public constructor

		public ANType14RecordCreateForm()
		{
			InitializeComponent();
		}

		#endregion

		#region Protected methods

		protected override ANRecord OnCreateRecord(ANTemplate template)
		{
			ANType14Record record;
			if (imageLoader.CreateFromImage)
			{
				record = new ANType14Record(ANTemplate.VersionCurrent, Idc, imageLoader.Src, imageLoader.ScaleUnits,
					imageLoader.CompressionAlgorithm, imageLoader.Image);
			}
			else if (imageLoader.CreateFromData)
			{
				using (NImage image = NImage.FromMemory(imageLoader.ImageData))
				{
					record = new ANType14Record(ANTemplate.VersionCurrent, Idc, imageLoader.Src, imageLoader.ScaleUnits,
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
