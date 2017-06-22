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
			Console.WriteLine("\t{0} [srcImage] [dstImage] <optional: bitRate>", TutorialUtils.GetAssemblyName());
			Console.WriteLine();
			Console.WriteLine("\tsrcImage - filename of source finger image.");
			Console.WriteLine("\tdstImage - name of a file to save the created WSQ image to.");
			Console.WriteLine("\tbitRate  - specifies WSQ image compression level. Typical bit rates: 0.75, 2.25.");
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

				// Create an NImage from file
				using (NImage image = NImage.FromFile(args[0]))
				{
					// Create WSQInfo to store bit rate
					using (var info = (WsqInfo) NImageFormat.Wsq.CreateInfo(image))
					{
						// Set specified bit rate (or default if bit rate was not specified).
						var bitrate = args.Length > 2 ? float.Parse(args[2]) : WsqInfo.DefaultBitRate;
						info.BitRate = bitrate;

						// Save image in WSQ format and bitrate to file.
						image.Save(args[1], info);
						Console.WriteLine("WSQ image with bit rate {0} was saved to {1}", bitrate, args[1]);
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
				NLicense.ReleaseComponents(Components);
			}
		}
	}
}
