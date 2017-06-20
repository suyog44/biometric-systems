using System;
using Neurotec.Biometrics;
using Neurotec.Devices;
using Neurotec.Licensing;

namespace Neurotec.Tutorials
{
	class Program
	{
		static int Usage()
		{
			Console.WriteLine("usage:");
			Console.WriteLine("\t{0} [imageCount]", TutorialUtils.GetAssemblyName());
			Console.WriteLine();
			Console.WriteLine("\timageCount - count of fingerprint images to be scanned");
			Console.WriteLine();

			return 1;
		}

		static int Main(string[] args)
		{
			const string Components = "Devices.FingerScanners";

			TutorialUtils.PrintTutorialHeader(args);

			if (args.Length < 1)
			{
				return Usage();
			}

			try
			{
				if (!NLicense.ObtainComponents("/local", 5000, Components))
				{
					throw new NotActivatedException(string.Format("Could not obtain licenses for components: {0}", Components));
				}

				int imageCount = int.Parse(args[0]);
				if (imageCount == 0)
				{
					Console.WriteLine("no frames will be captured as frame count is not specified");
				}

				using (var deviceManager = new NDeviceManager { DeviceTypes = NDeviceType.FingerScanner, AutoPlug = true })
				{
					deviceManager.Initialize();
					Console.WriteLine("device manager created. found scanners: {0}", deviceManager.Devices.Count);

					foreach (NFScanner scanner in deviceManager.Devices)
					{
						Console.WriteLine("found scanner {0}, capturing fingerprints", scanner.DisplayName);

						for (int i = 0; i < imageCount; i++)
						{
							Console.Write("\timage {0} of {1}. please put your fingerprint on scanner:", i + 1, imageCount);
							string filename = String.Format("{0}_{1:d4}.jpg", scanner.DisplayName, i);
							using (var biometric = new NFinger())
							{
								biometric.Position = NFPosition.Unknown;
								var biometricStatus = scanner.Capture(biometric, -1);
								if (biometricStatus != NBiometricStatus.Ok)
								{
									Console.WriteLine("failed to capture from scanner, status: {0}", biometricStatus);
									continue;
								}
								biometric.Image.Save(filename);
								Console.WriteLine(" image captured");
							}
						}
					}
				}
				Console.WriteLine("done");

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
