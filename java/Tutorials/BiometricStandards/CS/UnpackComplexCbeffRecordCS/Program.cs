using System;
using System.IO;
using Neurotec.Biometrics.Standards;
using Neurotec.IO;
using Neurotec.Licensing;

namespace Neurotec.Tutorials
{
	class Program
	{
		private enum BdbFormat
		{
			ANTemplate = 0x001B8019,
			FCRecordAnsi = 0x001B0501,
			FCRecordIso = 0x01010008,
			FIRecordAnsi = 0x001B0401,
			FIRecordIso = 0x01010007,
			FMRecordAnsi = 0x001B0202,
			FMRecordIso = 0x01010002,
			IIRecordAnsiPolar = 0x001B0602,
			IIRecordIsoPolar = 0x0101000B,
			IIRecordAnsiRectilinear = 0x001B0601,
			IIRecordIsoRectilinear = 0x01010009
		}

		private static int Usage()
		{
			Console.WriteLine("usage:");
			Console.WriteLine("\t{0} [ComplexCbeffRecord] [PatronFormat]", TutorialUtils.GetAssemblyName());
			Console.WriteLine("");
			Console.WriteLine("\t[ComplexCbeffRecord] - filename of CbeffRecord which will be created.");
			Console.WriteLine("\t[PatronFormat] - hex number identifying patron format (all supported values can be found in CbeffRecord class documentation).");
			Console.WriteLine("");

			return 1;
		}

		static int Main(string[] args)
		{
			const string Components = "Biometrics.Standards.Base";

			TutorialUtils.PrintTutorialHeader(args);

			if (args.Length != 2)
			{
				return Usage();
			}

			try
			{
				if (!NLicense.ObtainComponents("/local", 5000, Components))
				{
					throw new NotActivatedException(string.Format("Could not obtain licenses for components: {0}", Components));
				}

				// Read CbeffRecord buffer
				var packedCbeffRecord = new NBuffer(File.ReadAllBytes(args[0]));

				// Get CbeffRecord patron format
				// all supported patron formats can be found in CbeffRecord class documentation
				uint patronFormat = uint.Parse(args[1], System.Globalization.NumberStyles.HexNumber);

				// Create CbeffRecord object
				var cbeffRecord = new CbeffRecord(packedCbeffRecord, patronFormat);

				// Start unpacking the record
				UnpackRecords(cbeffRecord);

				Console.WriteLine("Records sucessfully saved");
				return 0;
			}
			catch (Exception ex)
			{
				return TutorialUtils.PrintException(ex);
			}
			finally
			{
				NLicense.ReleaseComponents(Components);
			}
		}

		private static void UnpackRecords(CbeffRecord cbeffRecord)
		{
			int recordNumber = 0;
			UnpackRecords(cbeffRecord, ref recordNumber);
		}

		private static void UnpackRecords(CbeffRecord cbeffRecord, ref int recordNumber)
		{
			if (cbeffRecord.Records.Count == 0)
			{
				// Write root record to file
				RecordToFile(cbeffRecord, recordNumber++);
			}
			else
			{
				// Go through all record in this CbeffRecord
				foreach (var record in cbeffRecord.Records)
				{
					// Start unpacking complex record
					UnpackRecords(record, ref recordNumber);
				}
			}
		}

		private static void RecordToFile(CbeffRecord record, int recordNumber)
		{
			string fileName;
			try
			{
				// Find Record format
				fileName = string.Format("Record{0}_{1}.dat", recordNumber, Enum.GetName(typeof(BdbFormat), record.BdbFormat));
			}
			catch
			{
				fileName = string.Format("Record{0}_UnknownFormat.dat", recordNumber);
			}

			// Save specified record
			File.WriteAllBytes(fileName, record.BdbBuffer.ToArray());
		}
	}
}
