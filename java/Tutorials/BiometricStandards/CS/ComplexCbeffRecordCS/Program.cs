using System;
using System.IO;

using Neurotec.Biometrics.Standards;
using Neurotec.Licensing;
using System.Collections.Generic;

namespace Neurotec.Tutorials
{
	class Program
	{
		private static readonly string[] Components =
		{
			"Biometrics.Standards.Base",
			"Biometrics.Standards.Irises",
			"Biometrics.Standards.Fingers",
			"Biometrics.Standards.Faces",
			"Biometrics.Standards.Palms"
		};

		private enum RecordTypes
		{
			ANTemplate,
			FCRecord,
			FIRecord,
			FMRecord,
			IIRecord
		}

		private class RecordInformation
		{
			public String RecordFile { get; set; }
			public BdifStandard Standard { get; set; }
			public RecordTypes RecordType { get; set; }
			public uint PatronFormat { get; set; }
		}

		private static int Usage()
		{
			Console.WriteLine("usage:");
			Console.WriteLine("\t{0} [ComplexCbeffRecord] [PatronFormat] [[Record] [RecordType] [RecordStandard] [PatronFormat]] ...", TutorialUtils.GetAssemblyName());
			Console.WriteLine("");
			Console.WriteLine("\t[ComplexCbeffRecord] - filename of CbeffRecord which will be created.");
			Console.WriteLine("\t[PatronFormat] - hex number identifying root record patron format (all supported values can be found in CbeffRecord class documentation)."); ;
			Console.WriteLine("\t[[Record] [RecordType] [RecordStandard] [PatronFormat]] - record information. Block can be specified more than once.");
			Console.WriteLine("\t\t[Record] - filename containing the record.");
			Console.WriteLine("\t\t[RecordType] - one of record type values(ANTemplate, FCRecord, FIRecord, FMRecord, IIRecord)");
			Console.WriteLine("\t\t[RecordStandard] - one of record standard values(ANSI, ISO or UNSPECIFIED if ANTemplate type is used).");
			Console.WriteLine("\t\t[PatronFormat] - hex number identifying patron format.");
			Console.WriteLine("");

			return 1;
		}

		static int Main(string[] args)
		{
			TutorialUtils.PrintTutorialHeader(args);

			if (args.Length < 6 || (args.Length - 2) % 4 != 0)
			{
				return Usage();
			}

			// Parse record information in arguments
			RecordInformation[] recordInformation;
			try
			{
				recordInformation = ParseArguments(args);
			}
			catch
			{
				return Usage();
			}

			var obtainedLicenses = new List<string>();
			try
			{
				// Obtain licenses.
				foreach (string component in Components)
				{
					if (NLicense.ObtainComponents("/local", 5000, component))
					{
						Console.WriteLine("Obtained license for component: {0}", component);
						obtainedLicenses.Add(component);
					}
				}
				if (obtainedLicenses.Count == 0)
				{
					throw new NotActivatedException("Could not obtain any matching license");
				}

				// Create root CbeffRecord
				var rootPatronFormat = uint.Parse(args[1], System.Globalization.NumberStyles.HexNumber);
				var rootCbeffRecord = new CbeffRecord(rootPatronFormat);

				foreach (var info in recordInformation)
				{
					CbeffRecord cbeffRecord = null;
					var packedRecord = File.ReadAllBytes(info.RecordFile);

					// Create a record object according information specified in arguments
					switch (info.RecordType)
					{
						case RecordTypes.ANTemplate:
							var anTemplate = new ANTemplate(packedRecord, ANValidationLevel.Standard);
							cbeffRecord = new CbeffRecord(anTemplate, info.PatronFormat);
							break;
						case RecordTypes.FCRecord:
							var fcRecord = new FCRecord(packedRecord, info.Standard);
							cbeffRecord = new CbeffRecord(fcRecord, info.PatronFormat);
							break;
						case RecordTypes.FIRecord:
							var fiRecord = new FIRecord(packedRecord, info.Standard);
							cbeffRecord = new CbeffRecord(fiRecord, info.PatronFormat);
							break;
						case RecordTypes.FMRecord:
							var fmRecord = new FMRecord(packedRecord, info.Standard);
							cbeffRecord = new CbeffRecord(fmRecord, info.PatronFormat);
							break;
						case RecordTypes.IIRecord:
							var iiRecord = new IIRecord(packedRecord, info.Standard);
							cbeffRecord = new CbeffRecord(iiRecord, info.PatronFormat);
							break;
					}

					// Add the new CbeffRecord to complex root CbeffRecord
					rootCbeffRecord.Records.Add(cbeffRecord);
				}

				// Save specified record
				File.WriteAllBytes(args[0], rootCbeffRecord.Save().ToArray());
				Console.WriteLine("Record sucessfully saved");

				return 0;
			}
			catch (Exception ex)
			{
				return TutorialUtils.PrintException(ex);
			}
			finally
			{
				foreach (string component in obtainedLicenses)
				{
					NLicense.ReleaseComponents(component);
				}
			}
		}

		private static RecordInformation[] ParseArguments(string[] args)
		{
			var infoList = new List<RecordInformation>();

			for (int i = 2; i < args.Length; i += 4)
			{
				var recInfo = new RecordInformation();
				recInfo.RecordFile = args[i];
				recInfo.RecordType = (RecordTypes)Enum.Parse(typeof(RecordTypes), args[i + 1], true);
				recInfo.Standard = (BdifStandard)Enum.Parse(typeof(BdifStandard), args[i + 2], true);
				recInfo.PatronFormat = uint.Parse(args[i + 3], System.Globalization.NumberStyles.HexNumber);
				infoList.Add(recInfo);
			}

			return infoList.ToArray();
		}
	}
}
