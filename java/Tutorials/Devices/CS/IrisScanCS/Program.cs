using System;

using Neurotec.Biometrics;
using Neurotec.Devices;
using Neurotec.Licensing;

namespace Neurotec.Tutorials
{
	class Program
	{
		static int Main(string[] args)
		{
			const string Components = "Devices.IrisScanners";

			TutorialUtils.PrintTutorialHeader(args);

			try
			{
				if (!NLicense.ObtainComponents("/local", 5000, Components))
				{
					throw new NotActivatedException(string.Format("Could not obtain licenses for components: {0}", Components));
				}

				using (var deviceManager = new NDeviceManager { DeviceTypes = NDeviceType.IrisScanner, AutoPlug = true })
				{
					deviceManager.Initialize();
					Console.WriteLine("device manager created. found scanners: {0}", deviceManager.Devices.Count);

					foreach (NIrisScanner scanner in deviceManager.Devices)
					{
						Console.Write("found scanner {0}", scanner.DisplayName);

						Console.Write("\tcapturing right iris: ");
						using (var rightIrisBiometric = new NIris())
						{
							rightIrisBiometric.Position = NEPosition.Right;
							var biometricStatus = scanner.Capture(rightIrisBiometric, -1);
							if (biometricStatus != NBiometricStatus.Ok)
							{
								Console.WriteLine("failed to capture from scanner, status: {0}", biometricStatus);
								continue;
							}
							string filename = string.Format("{0}_iris_right.jpg", scanner.DisplayName);
							rightIrisBiometric.Image.Save(filename);
							Console.WriteLine("done");
						}

						Console.Write("\tcapturing left eye: ");
						using (var leftIrisBiometric = new NIris())
						{
							leftIrisBiometric.Position = NEPosition.Left;
							var biometricStatus = scanner.Capture(leftIrisBiometric, -1);
							if (biometricStatus != NBiometricStatus.Ok)
							{
								Console.WriteLine("failed to capture from scanner, status: {0}", biometricStatus);
								continue;
							}
							string filename = string.Format("{0}_iris_left.jpg", scanner.DisplayName);
							leftIrisBiometric.Image.Save(filename);
							Console.WriteLine("done");
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
