using System;
using Neurotec.Biometrics.Standards;
using Neurotec.IO;
using Neurotec.Images;

namespace Neurotec.Samples.RecordCreateForms
{
	public partial class ANType13RecordCreateForm : ANRecordCreateForm
	{
		#region Public constructor

		public ANType13RecordCreateForm()
		{
			InitializeComponent();
			foreach (object value in Enum.GetValues(typeof(BdifFPImpressionType)))
			{
				cbFpImpressionType.Items.Add(value);
			}
			cbFpImpressionType.SelectedIndex = 0;
		}

		#endregion

		#region Private properties

		private BdifFPImpressionType ImpressionType
		{
			get
			{
				return (BdifFPImpressionType)cbFpImpressionType.SelectedItem;
			}
		}

		#endregion

		#region Protected methods

		protected override ANRecord OnCreateRecord(ANTemplate template)
		{
			ANType13Record record;
			if (imageLoader.CreateFromImage)
			{
				record = new ANType13Record(ANTemplate.VersionCurrent, Idc, ImpressionType, imageLoader.Src, imageLoader.ScaleUnits,
					imageLoader.CompressionAlgorithm, imageLoader.Image);
			}
			else if (imageLoader.CreateFromData)
			{
				using (NImage image = NImage.FromMemory(imageLoader.ImageData))
				{
					record = new ANType13Record(ANTemplate.VersionCurrent, Idc, ImpressionType, imageLoader.Src, imageLoader.ScaleUnits,
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
