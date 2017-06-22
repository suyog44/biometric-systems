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
			Console.WriteLine("usage: {0} [FCRecord] {{[image]}}", TutorialUtils.GetAssemblyName());
			Console.WriteLine("\t[FCRecord] - output FCRecord");
			Console.WriteLine("\t[image]    - one or more images");

			return 1;
		}

		static int Main(string[] args)
		{
			const string Components = "Biometrics.Standards.Faces";

			TutorialUtils.PrintTutorialHeader(args);

			if (args.Length < 2)
			{
				return Usage();
			}

			FCRecord fc = null;
			try
			{
				if (!NLicense.ObtainComponents("/local", 5000, Components))
				{
					throw new NotActivatedException(string.Format("Could not obtain licenses for components: {0}", Components));
				}

				for (int i = 1; i < args.Length; i++)
				{
					using (NImage imageFromFile = NImage.FromFile(args[i]))
					using (NImage image = NImage.FromImage(NPixelFormat.Grayscale8U, 0, imageFromFile))
					{
						if (fc == null)
						{
							// Specify standard and version to be used
							fc = new FCRecord(BdifStandard.Iso, FCRecord.VersionIso30);
						}
						FcrFaceImage img = new FcrFaceImage(fc.Standard, fc.Version);
						img.SetImage(image);
						fc.FaceImages.Add(img);
					}
				}
				if (fc != null)
				{
					File.WriteAllBytes(args[0], fc.Save().ToArray());

					Console.WriteLine("FCRecord saved to {0}", args[0]);
				}
				else
				{
					Console.WriteLine("no images were added to FCRecord");
				}

				return 0;
			}
			catch (Exception ex)
			{
				return TutorialUtils.PrintException(ex);
			}
			finally
			{
				if (fc != null)
				{
					fc.Dispose();
				}

				NLicense.ReleaseComponents(Components);
			}
		}
	}
}
