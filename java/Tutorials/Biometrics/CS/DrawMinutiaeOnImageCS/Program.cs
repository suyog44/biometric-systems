using System;
using System.Drawing;
using System.Drawing.Imaging;
using Neurotec.Biometrics;
using Neurotec.Biometrics.Client;
using Neurotec.Licensing;
using Neurotec.Biometrics.Gui;
using Neurotec.Images;

namespace Neurotec.Tutorials
{
	class Program
	{
		private static int Usage()
		{
			Console.WriteLine("usage:");
			Console.WriteLine("\t{0} [image] [bitmap]", TutorialUtils.GetAssemblyName());
			Console.WriteLine();
			Console.WriteLine("\t[image] - image file containing a finger");
			Console.WriteLine("\t[bitmap] - filename to store finger.");

			return 1;
		}

		static int Main(string[] args)
		{
			const string Components = "Biometrics.FingerExtraction";

			TutorialUtils.PrintTutorialHeader(args);
			if (args.Length < 2)
			{
				return Usage();
			}

			try
			{
				// obtain license
				if (!NLicense.ObtainComponents("/local", 5000, Components))
				{
					throw new ApplicationException(string.Format("Could not obtain licenses for components: {0}", Components));
				}

				using (var client = new NBiometricClient())
				using (var fingerView = new NFingerView())
				using (var subject = new NSubject())
				using (var finger = new NFinger())
				using (var image = NImage.FromFile(args[0]))
				{
					// setting fingers image
					finger.Image = image;

					// adding finger to subject
					subject.Fingers.Add(finger);

					// creating template from subject
					NBiometricStatus status = client.CreateTemplate(subject);

					if (status == NBiometricStatus.Ok)
					{
						Console.WriteLine("Template creation succeeded");

						fingerView.Width = (int)image.Width;
						fingerView.Height = (int)image.Height;

						// settings finger with template to finger view
						fingerView.Finger = subject.Fingers[0];

						// creating new bitmap with not indexed pixel format
						using (Bitmap tempBitmap = new Bitmap(fingerView.Width, fingerView.Height, PixelFormat.Format32bppArgb))
						{
							Rectangle rect = new Rectangle(0, 0, fingerView.Width, fingerView.Height);

							// draw minutiae on bitmap
							fingerView.DrawToBitmap(tempBitmap, rect);

							// save bitmap
							tempBitmap.Save(args[1]);
						}
					}
					else
					{
						Console.WriteLine("Template creation failed. Status {0}", status);

						return -1;
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
