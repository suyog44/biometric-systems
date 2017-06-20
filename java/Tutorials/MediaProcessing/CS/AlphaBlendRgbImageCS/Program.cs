using System;
using Neurotec.Images;
using Neurotec.Images.Processing;

namespace Neurotec.Tutorials
{
	class Program
	{
		static int Usage()
		{
			Console.WriteLine("usage:");
			Console.WriteLine("\t{0} [imageA] [imageB] [alpha] [output image]", TutorialUtils.GetAssemblyName());
			Console.WriteLine("\texample: {0} c:\\image1.bmp c:\\image2.bmp 0.5 c:\\result.bmp", TutorialUtils.GetAssemblyName());
			Console.WriteLine("\tnote: images must be of the same width and height");
			Console.WriteLine();
			return 1;
		}

		static int Main(string[] args)
		{
			TutorialUtils.PrintTutorialHeader(args);
			if (args.Length < 4)
			{
				return Usage();
			}

			try
			{
				double alpha = double.Parse(args[2]);

				// open images
				NImage imageA = NImage.FromFile(args[0]);
				NImage imageB = NImage.FromFile(args[1]);

				// convert images to rgb
				var rgbImageA = NImage.FromImage(NPixelFormat.Rgb8U, 0, imageA);
				var rgbImageB = NImage.FromImage(NPixelFormat.Rgb8U, 0, imageB);

				// alpha blend
				NImage result = Nrgbip.AlphaBlend(rgbImageA, rgbImageB, alpha);
				result.Save(args[3]);
				Console.WriteLine("image saved to \"{0}\"", args[3]);

				return 0;
			}
			catch (Exception ex)
			{
				return TutorialUtils.PrintException(ex);
			}
		}
	}
}
