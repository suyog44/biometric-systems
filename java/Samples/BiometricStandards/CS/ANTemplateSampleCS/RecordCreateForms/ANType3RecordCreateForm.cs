using System;
using Neurotec.Biometrics.Standards;
using Neurotec.Images;
using Neurotec.IO;

namespace Neurotec.Samples.RecordCreateForms
{
	public partial class ANType3RecordCreateForm : ANImageBinaryRecordCreateForm
	{
		#region Public constructor

		public ANType3RecordCreateForm()
		{
			InitializeComponent();

			IsLowResolution = true;
		}

		#endregion

		#region Protected methods

		protected override ANRecord OnCreateRecord(ANTemplate template)
		{
			ANType3Record record;
			if (CreateFromImage)
			{
				using (NImage image = Image)
				{
					record = new ANType3Record(ANTemplate.VersionCurrent, Idc, IsrFlag, (ANImageCompressionAlgorithm)CompressionAlgorithm, image);
				}
			}
			else if (CreateFromData)
			{
				using (NImage image = NImage.FromMemory(ImageData))
				{
					record = new ANType3Record(ANTemplate.VersionCurrent, Idc, IsrFlag, (ANImageCompressionAlgorithm)CompressionAlgorithm, image)
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
			return typeof(ANImageCompressionAlgorithm);
		}

		#endregion
	}
}
