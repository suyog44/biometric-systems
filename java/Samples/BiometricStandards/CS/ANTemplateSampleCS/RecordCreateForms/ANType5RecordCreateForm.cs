using System;
using Neurotec.Biometrics.Standards;
using Neurotec.Images;
using Neurotec.IO;

namespace Neurotec.Samples.RecordCreateForms
{
	public partial class ANType5RecordCreateForm : ANImageBinaryRecordCreateForm
	{
		#region Public constructor

		public ANType5RecordCreateForm()
		{
			InitializeComponent();

			IsLowResolution = true;
		}

		#endregion

		#region Protected methods

		protected override ANRecord OnCreateRecord(ANTemplate template)
		{
			ANType5Record record;
			if (CreateFromImage)
			{
				using (NImage image = Image)
				{
					record = new ANType5Record(ANTemplate.VersionCurrent, Idc, IsrFlag, (ANBinaryImageCompressionAlgorithm)CompressionAlgorithm, image);
				}
			}
			else if (CreateFromData)
			{
				using (NImage image = NImage.FromMemory(ImageData))
				{
					record = new ANType5Record(ANTemplate.VersionCurrent, Idc, IsrFlag, (ANBinaryImageCompressionAlgorithm)CompressionAlgorithm, image)
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
			return typeof(ANBinaryImageCompressionAlgorithm);
		}

		#endregion
	}
}
