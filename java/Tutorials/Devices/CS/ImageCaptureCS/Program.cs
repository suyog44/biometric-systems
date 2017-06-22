using System;

using Neurotec.Images;
using Neurotec.Devices;
using Neurotec.Licensing;

namespace Neurotec.Tutorials
{
	class Program
	{
		static int Usage()
		{
			Console.WriteLine("usage:");
			Console.WriteLine("\t{0} [frameCount]", TutorialUtils.GetAssemblyName());
			Console.WriteLine();
			Console.WriteLine("\tframeCount - number of frames to capture from each camera to current directory");
			Console.WriteLine();

			return 1;
		}

		static int Main(string[] args)
		{
			const string Components = "Devices.Cameras";

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

				int frameCount = int.Parse(args[0]);
				if (frameCount == 0)
				{
					Console.WriteLine("no frames will be captured as frame count is not specified");
				}

				using (var deviceManager = new NDeviceManager { DeviceTypes = NDeviceType.Camera, AutoPlug = true })
				{
					deviceManager.Initialize();
					Console.WriteLine("device manager created. found cameras: {0}", deviceManager.Devices.Count);

					foreach (NCamera camera in deviceManager.Devices)
					{
						Console.Write("found camera {0}", camera.DisplayName);

						camera.StartCapturing();

						if (frameCount > 0)
						{
							Console.Write(", capturing");
							for (int i = 0; i < frameCount; ++i)
							{
								string filename = String.Format("{0}_{1:d4}.jpg", camera.DisplayName, i);
								using (NImage image = camera.GetFrame())
								{
									image.Save(filename);
								}
								Console.Write(".");
							}
							Console.Write(" done");
							Console.WriteLine();
						}
						camera.StopCapturing();
					}
				}
				Console.WriteLine("done");

				return -1;
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
