using System;
using System.IO;

using Neurotec.Images;
using Neurotec.Biometrics.Standards;
using Neurotec.Licensing;

namespace Neurotec.Tutorials
{
	class Program
	{
		private static int Usage()
		{
			Console.WriteLine("usage: {0} [FIRecord] [Standard] [Version] {{[image]}}", TutorialUtils.GetAssemblyName());
			Console.WriteLine("\t[FIRecord] - output FIRecord");
			Console.WriteLine("\t[Standard] - standard for the record (ANSI or ISO)");
			Console.WriteLine("\t[Version] - version for the record");
			Console.WriteLine("\t\t 1 - ANSI/INCITS 381-2004");
			Console.WriteLine("\t\t 2.5 - ANSI/INCITS 381-2009");
			Console.WriteLine("\t\t 1 - ISO/IEC 19794-4:2005");
			Console.WriteLine("\t\t 2 - ISO/IEC 19794-4:2011");
			Console.WriteLine("\t[image]    - one or more images");

			return 1;
		}

		static int Main(string[] args)
		{
			const string Components = "Biometrics.Standards.Fingers";

			TutorialUtils.PrintTutorialHeader(args);

			if (args.Length < 4)
			{
				return Usage();
			}

			FIRecord fi = null;
			try
			{
				if (!NLicense.ObtainComponents("/local", 5000, Components))
				{
					throw new NotActivatedException(string.Format("Could not obtain licenses for components: {0}", Components));
				}

				var standard = (BdifStandard)Enum.Parse(typeof(BdifStandard), args[1], true);
				NVersion version;
				switch (args[2])
				{
					case "1":
						version = standard == BdifStandard.Ansi ? FIRecord.VersionAnsi10 : FIRecord.VersionIso10;
						break;
					case "2":
						if (standard != BdifStandard.Iso) throw new ArgumentException("Standard and version is incompatible");
						version = FIRecord.VersionIso20;
						break;
					case "2.5":
						if (standard != BdifStandard.Ansi) throw new ArgumentException("Standard and version is incompatible");
						version = FIRecord.VersionAnsi25;
						break;
					default:
						throw new ArgumentException("Version was not recognised");
				}

				for (int i = 3; i < args.Length; i++)
				{
					using (NImage imageFromFile = NImage.FromFile(args[i]))
					using (NImage grayscaleImage = NImage.FromImage(NPixelFormat.Grayscale8U, 0, imageFromFile))
					{
						if (grayscaleImage.ResolutionIsAspectRatio
							|| grayscaleImage.HorzResolution < 250
							|| grayscaleImage.VertResolution < 250)
						{
							grayscaleImage.HorzResolution = 500;
							grayscaleImage.VertResolution = 500;
							grayscaleImage.ResolutionIsAspectRatio = false;
						}

						if (fi == null)
						{
							fi = new FIRecord(standard, version);
							if (IsRecordFirstVersion(fi))
							{
								fi.PixelDepth = 8;
								fi.HorzImageResolution = (ushort)grayscaleImage.HorzResolution;
								fi.HorzScanResolution = (ushort)grayscaleImage.HorzResolution;
								fi.VertImageResolution = (ushort)grayscaleImage.VertResolution;
								fi.VertScanResolution = (ushort)grayscaleImage.VertResolution;
							}
						}
						FirFingerView fingerView = new FirFingerView(fi.Standard, fi.Version);
						if (!IsRecordFirstVersion(fi))
						{
							fingerView.PixelDepth = 8;
							fingerView.HorzImageResolution = (ushort)grayscaleImage.HorzResolution;
							fingerView.HorzScanResolution = (ushort)grayscaleImage.HorzResolution;
							fingerView.VertImageResolution = (ushort)grayscaleImage.VertResolution;
							fingerView.VertScanResolution = (ushort)grayscaleImage.VertResolution;
						}
						fi.FingerViews.Add(fingerView);
						fingerView.SetImage(grayscaleImage);
					}
				}

				if (fi != null)
				{
					File.WriteAllBytes(args[0], fi.Save().ToArray());
					Console.WriteLine("FIRecord saved to {0}", args[0]);
				}
				else
				{
					Console.WriteLine("no images were added to FIRecord");
				}

				return 0;
			}
			catch (Exception ex)
			{
				return TutorialUtils.PrintException(ex);
			}
			finally
			{
				if (fi != null)
				{
					fi.Dispose();
				}

				NLicense.ReleaseComponents(Components);
			}
		}

		private static bool IsRecordFirstVersion(FIRecord record)
		{
			return record.Standard == BdifStandard.Ansi && record.Version == FIRecord.VersionAnsi10
				|| record.Standard == BdifStandard.Iso && record.Version == FIRecord.VersionIso10;
		}
	}
}
