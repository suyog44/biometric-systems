using System;
using Neurotec.Biometrics.Standards;
using Neurotec.Images;
using Neurotec.IO;

namespace Neurotec.Samples.RecordCreateForms
{
	public partial class ANType6RecordCreateForm : ANImageBinaryRecordCreateForm
	{
		#region Public constructor

		public ANType6RecordCreateForm()
		{
			InitializeComponent();

			IsLowResolution = false;
		}

		#endregion

		#region Protected methods

		protected override ANRecord OnCreateRecord(ANTemplate template)
		{
			ANType6Record record;
			if (CreateFromImage)
			{
				using (NImage image = Image)
				{
					record = new ANType6Record(ANTemplate.VersionCurrent, Idc, IsrFlag, (ANBinaryImageCompressionAlgorithm)CompressionAlgorithm, image);
					template.Records.Add(record);
					return record;
				}
			}
			else if (CreateFromData)
			{
				using (NImage image = NImage.FromMemory(ImageData))
				{
					record = new ANType6Record(ANTemplate.VersionCurrent, Idc, IsrFlag, (ANBinaryImageCompressionAlgorithm)CompressionAlgorithm, image)
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
