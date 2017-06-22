using System;
using System.Collections.Generic;
using System.Linq;
using Neurotec.Biometrics;
using Neurotec.Biometrics.Standards;

namespace Neurotec.Samples
{
	public class RecordTransformations
	{
		public static ANTemplate NFRecordToANTemplate(NFRecord nfRecord)
		{
			const string Tot = "a"; // type of transaction
			const string Dai = "b"; // destination agency identifier
			const string Ori = "c"; // originating agency identifier
			const string Tcn = "d"; // transaction control number

			try
			{
				var anTemplate = new ANTemplate(Tot, Dai, Ori, Tcn, true, new NTemplate(nfRecord.Save()), 0);
				return anTemplate;
			}
			catch (Exception ex)
			{
				throw new Exception("Error converting NFRecord to ANTemplate.", ex);
			}
		}

		public static ANTemplate NTemplateToANTemplate(NTemplate ntemplate)
		{
			const string Tot = "a"; // type of transaction
			const string Dai = "b"; // destination agency identifier
			const string Ori = "c"; // originating agency identifier
			const string Tcn = "d"; // transaction control number

			// Creating ANTemplate object from NTemplate object
			try
			{
				var anTemplate = new ANTemplate(Tot, Dai, Ori, Tcn, true, ntemplate, 0);
				return anTemplate;
			}
			catch (Exception ex)
			{
				throw new Exception("Error converting NTemplate to ANTemplate.", ex);
			}
		}

		public static NTemplate ANTemplateToNTemplate(ANTemplate antemplate)
		{
			try
			{
				NTemplate ntemplate = antemplate.ToNTemplate();
				return ntemplate;
			}
			catch (Exception ex)
			{
				throw new Exception("Error converting ANTemplate to NTemplate.", ex);
			}
		}

		public static NFTemplate ANTemplateToNFTemplate(ANTemplate antemplate)
		{
			try
			{
				NTemplate ntemplate = antemplate.ToNTemplate();
				return ntemplate.Fingers;
			}
			catch (Exception ex)
			{
				throw new Exception("Error converting ANTemplate to NFTemplate.", ex);
			}
		}

		public static NFTemplate NTemplateToNFTemplate(NTemplate ntemplate)
		{
			try
			{
				NFTemplate nfTemplate = ntemplate.Fingers;
				return nfTemplate;
			}
			catch (Exception ex)
			{
				throw new Exception("Error converting NTemplate to NFTemplate.", ex);
			}
		}

		public static NFRecord[] NTemplateToNFRecords(NTemplate ntemplate)
		{
			try
			{
				NFTemplate nfTemplate = ntemplate.Fingers;
				return Enumerable.ToArray(nfTemplate.Records);
			}
			catch (Exception ex)
			{
				throw new Exception("Error converting NTemplate to NFRecord.", ex);
			}
		}

		public static FMRecord NFRecordToFMRecord(NFRecord nfrecord, BdifStandard standard)
		{
			try
			{
				var fmRecord = new FMRecord(nfrecord, standard, new NVersion(2, 0));
				return fmRecord;
			}
			catch (Exception ex)
			{
				throw new Exception("Error converting NFRecord to FMRecord.", ex);
			}
		}

		public static FMRecord NFTemplateToFMRecord(NFTemplate nftemplate, BdifStandard standard)
		{
			try
			{
				var fmRecord = new FMRecord(nftemplate, standard, new NVersion(2, 0));
				return fmRecord;
			}
			catch (Exception ex)
			{
				throw new Exception("Error converting NFTemplate to FMRecord.", ex);
			}
		}

		public static NFTemplate FMRecordToNFTemplate(FMRecord fmrecord)
		{
			try
			{
				NFTemplate nfTemplate = fmrecord.ToNFTemplate();
				return nfTemplate;
			}
			catch (Exception ex)
			{
				throw new Exception("Error converting FMRecord to NFTemplate.", ex);
			}
		}

		public static NTemplate FMRecordToNTemplate(FMRecord fmrecord)
		{
			try
			{
				NTemplate nTemplate = fmrecord.ToNTemplate();
				return nTemplate;
			}
			catch (Exception ex)
			{
				throw new Exception("Error converting FMRecord to NTemplate.", ex);
			}
		}
	}
}
