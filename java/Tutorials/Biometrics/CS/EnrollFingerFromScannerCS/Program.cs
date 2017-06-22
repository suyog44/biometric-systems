using System;
using System.IO;

using Neurotec.Licensing;
using Neurotec.Devices;
using Neurotec.Biometrics;
using Neurotec.Biometrics.Client;

namespace Neurotec.Tutorials
{
	class Program
	{
		private static int Usage()
		{
			Console.WriteLine("usage:");
			Console.WriteLine("\t{0} [image] [template]", TutorialUtils.GetAssemblyName());
			Console.WriteLine();
			Console.WriteLine("\t[image]  - image filename to store finger image.");
			Console.WriteLine("\t[template] - filename to store finger template.");
			Console.WriteLine();

			return 1;
		}

		static int Main(string[] args)
		{
			const string Components = "Biometrics.FingerExtraction,Devices.FingerScanners";

			TutorialUtils.PrintTutorialHeader(args);
			if (args.Length < 2)
			{
				return Usage();
			}

			try
			{
				if (!NLicense.ObtainComponents("/local", 5000, Components))
				{
					throw new ApplicationException(string.Format("Could not obtain licenses for components: {0}", Components));
				}

				using (var biometricClient = new NBiometricClient { UseDeviceManager = true})
				using (var deviceManager = biometricClient.DeviceManager)
				using (var subject = new NSubject())
				using (var finger = new NFinger())
				{
					//set type of the device used
					deviceManager.DeviceTypes = NDeviceType.FingerScanner;

					//initialize the NDeviceManager
					deviceManager.Initialize();

					int i;

					//get count of connected devices
					int count = deviceManager.Devices.Count;

					if (count > 0)
						Console.WriteLine("found {0} finger scanners", count);
					else
					{
						Console.WriteLine("no finger scanners found, exiting ...\n");
						return -1;
					}

					//list detected scanners
					if (count > 1)
						Console.WriteLine("Please select finger scanner from the list: ");
					for (i = 0; i < count; i++)
					{
						NDevice device = deviceManager.Devices[i];
						Console.WriteLine("{0}) {1}", i + 1, device.DisplayName);
					}

					//finger scanner selection by user
					if (count > 1)
					{
						Console.Write("Please enter finger scanner index: ");
						string line = Console.ReadLine();
						if (line == null) throw new ApplicationException("Nothing read from standard input");
						i = int.Parse(line);
						if (i > count || i < 1)
						{
							Console.WriteLine("Incorrect index provided, exiting ...");
							return -1;
						}
					}
					i--;

					//set the selected finger scanner as NBiometricClient Finger Scanner
					biometricClient.FingerScanner = (NFScanner)deviceManager.Devices[i];

					//add NFinger to NSubject
					subject.Fingers.Add(finger);

					//start capturing
					NBiometricStatus status = biometricClient.Capture(subject);
					if (status != NBiometricStatus.Ok)
					{
						Console.WriteLine("Failed to capture: " + status);
						return -1;
					}

					//Set finger template size (recommended, for enroll to database, is large) (optional)
					biometricClient.FingersTemplateSize = NTemplateSize.Large;

					//Create template from added finger image
					status = biometricClient.CreateTemplate(subject);
					if (status == NBiometricStatus.Ok)
					{
						Console.WriteLine("Template extracted");

						// save image to file
						using (var image = subject.Fingers[0].Image)
						{
							image.Save(args[0]);
							Console.WriteLine("image saved successfully");
						}

						// save template to file
						File.WriteAllBytes(args[1], subject.GetTemplateBuffer().ToArray());
						Console.WriteLine("template saved successfully");
					}
					else
					{
						Console.WriteLine("Extraction failed! Status: {0}", status);
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
