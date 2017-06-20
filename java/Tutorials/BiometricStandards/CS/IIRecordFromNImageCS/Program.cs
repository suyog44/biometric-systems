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
			Console.WriteLine("usage: {0} [IIRecord] [Standard] [Version] [image]", TutorialUtils.GetAssemblyName());
			Console.WriteLine("\t[IIRecord] - output IIRecord");
			Console.WriteLine("\t[Standard] - standard for the record (ANSI or ISO)");
			Console.WriteLine("\t[Version] - version for the record");
			Console.WriteLine("\t\t 1 - ANSI/INCITS 379-2004");
			Console.WriteLine("\t\t 1 - ISO/IEC 19794-6:2005");
			Console.WriteLine("\t\t 2 - ISO/IEC 19794-6:2011");
			Console.WriteLine("\t[image]    - one or more images");

			return 1;
		}

		static int Main(string[] args)
		{
			const string Components = "Biometrics.Standards.Irises";

			TutorialUtils.PrintTutorialHeader(args);

			if (args.Length < 4)
			{
				return Usage();
			}

			IIRecord iiRec = null;
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
						version = standard == BdifStandard.Ansi ? IIRecord.VersionAnsi10 : IIRecord.VersionIso10;
						break;
					case "2":
						if (standard != BdifStandard.Iso) throw new ArgumentException("Standard and version is incompatible");
						version = IIRecord.VersionIso20;
						break;
					default:
						throw new ArgumentException("Version was not recognised");
				}

				for (int i = 3; i < args.Length; i++)
				{
					using (NImage imageFromFile = NImage.FromFile(args[i]))
					using (NImage image = NImage.FromImage(NPixelFormat.Grayscale8U, 0, imageFromFile))
					{
						if (iiRec == null)
						{
							iiRec = new IIRecord(standard, version);
							if (IsRecordFirstVersion(iiRec))
							{
								iiRec.RawImageHeight = (ushort)image.Height;
								iiRec.RawImageWidth = (ushort)image.Width;
								iiRec.IntensityDepth = 8;
							}
						}
						var iirIrisImage = new IirIrisImage(iiRec.Standard, iiRec.Version);
						if (!IsRecordFirstVersion(iiRec))
						{
							iirIrisImage.ImageWidth = (ushort)image.Width;
							iirIrisImage.ImageHeight = (ushort)image.Height;
							iirIrisImage.IntensityDepth = 8;
						};

						iirIrisImage.SetImage(image);
						iiRec.IrisImages.Add(iirIrisImage);
					}
				}

				if (iiRec != null)
				{
					File.WriteAllBytes(args[0], iiRec.Save().ToArray());

					Console.WriteLine("IIRecord saved to {0}", args[0]);
				}
				else
				{
					Console.WriteLine("no images were added to IIRecord");
				}

				return 0;
			}
			catch (Exception ex)
			{
				return TutorialUtils.PrintException(ex);
			}
			finally
			{
				if (iiRec != null)
				{
					iiRec.Dispose();
				}

				NLicense.ReleaseComponents(Components);
			}
		}

		private static bool IsRecordFirstVersion(IIRecord record)
		{
			return record.Standard == BdifStandard.Ansi && record.Version == IIRecord.VersionAnsi10
				|| record.Standard == BdifStandard.Iso && record.Version == IIRecord.VersionIso10;
		}
	}
}
