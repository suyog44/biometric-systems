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
			Console.WriteLine("\t{0} [image1] [image2] [template]", TutorialUtils.GetAssemblyName());
			Console.WriteLine("");
			Console.WriteLine("\t[image1]    - image filename to store left iris image.");
			Console.WriteLine("\t[image2]    - image filename to store right iris image.");
			Console.WriteLine("\t[template]  - filename to store template.");

			return 1;
		}

		static int Main(string[] args)
		{
			const string Components = "Biometrics.IrisExtraction,Devices.IrisScanners";

			TutorialUtils.PrintTutorialHeader(args);

			if (args.Length < 3)
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
				using (var leftIris = new NIris())
				using (var rightIris = new NIris())
				{
					//set type of the device used
					deviceManager.DeviceTypes = NDeviceType.IrisScanner;

					//initialize the NDeviceManager
					deviceManager.Initialize();

					int i;

					//get count of connected devices
					int count = deviceManager.Devices.Count;

					if (count > 0)
						Console.WriteLine("found {0} iris scanners", count);
					else
					{
						Console.WriteLine("no iris scanners found, exiting ...\n");
						return -1;
					}

					//list detected iris scanners
					if (count > 1)
						Console.WriteLine("Please select iris scanners from the list: ");
					for (i = 0; i < count; i++)
					{
						NDevice device = deviceManager.Devices[i];
						Console.WriteLine("{0}) {1}", i + 1, device.DisplayName);
					}

					//iris scanners selection by user
					if (count > 1)
					{
						Console.Write("Please enter iris scanners index: ");
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

					//set the selected camera as NBiometricClient Iris Scanner
					biometricClient.IrisScanner = (NIrisScanner)deviceManager.Devices[i];

					Console.WriteLine("started capturing left iris from {0} ..", biometricClient.IrisScanner.DisplayName);

					// Set NIris position
					leftIris.Position = NEPosition.Left;
					
					// add NIris to NSubject
					subject.Irises.Add(leftIris);

					// Capture left iris
					NBiometricStatus status = biometricClient.Capture(subject);
					Console.WriteLine(status == NBiometricStatus.Ok
						? "Captured"
						: String.Format("Capturing failed: {0}", status));

					Console.WriteLine("started capturing right iris from {0} ..", biometricClient.IrisScanner.DisplayName);

					// Set NIris position
					rightIris.Position = NEPosition.Right;
					
					// add NIris to NSubject
					subject.Irises.Add(rightIris);

					// Capture right iris
					status = biometricClient.Capture(subject);
					Console.WriteLine(status == NBiometricStatus.Ok
						? "Captured"
						: String.Format("Capturing failed: {0}", status));

					//Set iris template size (recommended, for enroll to database, is large) (optional)
					biometricClient.IrisesTemplateSize = NTemplateSize.Large;

					//Create template from added iris image
					status = biometricClient.CreateTemplate(subject);
					Console.WriteLine(status == NBiometricStatus.Ok
						? "Template extracted"
						: String.Format("Extraction failed: {0}", status));

					// save first iris image to file
					using (var image = subject.Irises[0].Image)
					{
						image.Save(args[0]);
						Console.WriteLine("image saved successfully");
					}

					// save second iris image to file
					using (var image = subject.Irises[1].Image)
					{
						image.Save(args[1]);
						Console.WriteLine("image saved successfully");
					}

					// save template to file
					File.WriteAllBytes(args[2], subject.GetTemplateBuffer().ToArray());
					Console.WriteLine("template saved successfully");

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
