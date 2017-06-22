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
			Console.WriteLine("\t{0} [filename]", TutorialUtils.GetAssemblyName());
			Console.WriteLine();
			Console.WriteLine("\tfilename - image filename.");
			Console.WriteLine();
			return 1;
		}

		static int Main(string[] args)
		{
			const string Components = "Images.WSQ,Images.IHead,Images.JPEG2000";

			TutorialUtils.PrintTutorialHeader(args);

			if (args.Length < 1)
			{
				return Usage();
			}

			bool doRelease = false;
			try
			{
				// Obtain license (optional)
				if (!NLicense.ObtainComponents("/local", 5000, Components))
				{
					Console.WriteLine("Could not obtain licenses for components: {0}", Components);
				}
				else doRelease = true;

				// Create NImage with info from file
				using (NImage image = NImage.FromFile(args[0]))
				{
					NImageFormat format = image.Info.Format;

					// Print info common to all formats
					Console.WriteLine("Format: {0}", format.Name);

					// Print format specific info.
					if (NImageFormat.Jpeg2K.Equals(format))
					{
						var info = (Jpeg2KInfo)image.Info;
						Console.WriteLine("Profile: {0}", info.Profile);
						Console.WriteLine("Compression ratio: {0}", info.Ratio);
					}
					else if (NImageFormat.Jpeg.Equals(format))
					{
						var info = (JpegInfo)image.Info;
						Console.WriteLine("Lossless: {0}", info.IsLossless);
						Console.WriteLine("Quality: {0}", info.Quality);
					}
					else if (NImageFormat.Png.Equals(format))
					{
						var info = (PngInfo)image.Info;
						Console.WriteLine("Compression level: {0}", info.CompressionLevel);
					}
					else if (NImageFormat.Wsq.Equals(format))
					{
						var info = (WsqInfo)image.Info;
						Console.WriteLine("Bit rate: {0}", info.BitRate);
						Console.WriteLine("Implementation number: {0}", info.ImplementationNumber);
					}
				}
				return 0;
			}
			catch (Exception ex)
			{
				return TutorialUtils.PrintException(ex);
			}
			finally
			{
				if (doRelease) NLicense.ReleaseComponents(Components);
			}
		}
	}
}
