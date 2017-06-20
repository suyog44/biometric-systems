using System;
using Neurotec.Images;
using Neurotec.Licensing;

namespace Neurotec.Tutorials
{
	class Program
	{
		static int Usage()
		{
			Console.WriteLine("usage:");
			Console.WriteLine("\t{0} [srcImage] [dstImage]", TutorialUtils.GetAssemblyName());
			Console.WriteLine();
			Console.WriteLine("\tsrcImage - filename of source WSQ image.");
			Console.WriteLine("\tdstImage - name of a file to save converted image to.");
			Console.WriteLine();
			return 1;
		}

		static int Main(string[] args)
		{
			const string Components = "Images.WSQ";

			TutorialUtils.PrintTutorialHeader(args);

			if (args.Length < 2)
			{
				return Usage();
			}

			try
			{
				// Obtain license
				if (!NLicense.ObtainComponents("/local", 5000, Components))
				{
					throw new NotActivatedException(string.Format("Could not obtain licenses for components: {0}", Components));
				}

				// Get WSQ image format
				// Create an NImage from a WSQ image file
				using (NImage image = NImage.FromFile(args[0], NImageFormat.Wsq))
				{
					Console.WriteLine("loaded wsq bitrate: {0}", ((WsqInfo)image.Info).BitRate);
					// Pick a format to save in, e.g. JPEG
					NImageFormat dstFormat = NImageFormat.Jpeg;
					// Save image to specified file
					image.Save(args[1], dstFormat);
					Console.WriteLine("{0} image was saved to {1}", dstFormat.Name, args[1]);
				}
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
	}
}
