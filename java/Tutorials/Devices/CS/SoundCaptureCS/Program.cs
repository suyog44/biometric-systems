using System;
using Neurotec.Devices;
using Neurotec.Licensing;
using Neurotec.Sound;

namespace Neurotec.Tutorials
{
	class Program
	{
		static int Usage()
		{
			Console.WriteLine("usage:");
			Console.WriteLine("\t{0} [bufferCount]", TutorialUtils.GetAssemblyName());
			Console.WriteLine();
			Console.WriteLine("\tbufferCount - number of sound buffers to capture from each microphone to current directory");
			Console.WriteLine();

			return 1;
		}

		static int Main(string[] args)
		{
			const string Components = "Devices.Microphones";

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

				int bufferCount = int.Parse(args[0]);
				if (bufferCount == 0)
				{
					Console.WriteLine("no sound buffers will be captured as sound buffer count is not specified");
				}

				using (var deviceManager = new NDeviceManager { DeviceTypes = NDeviceType.Microphone, AutoPlug = true })
				{
					deviceManager.Initialize();
					Console.WriteLine("device manager created. found microphones: {0}", deviceManager.Devices.Count);

					foreach (NMicrophone microphone in deviceManager.Devices)
					{
						Console.Write("found microphone {0}", microphone.DisplayName);

						microphone.StartCapturing();

						if (bufferCount > 0)
						{
							Console.WriteLine(", capturing");
							for (int i = 0; i < bufferCount; ++i)
							{
								using (NSoundBuffer soundSample = microphone.GetSoundSample())
								{
									Console.WriteLine("sample buffer received. sample rate: {0}, sample length: {1}", soundSample.SampleRate, soundSample.Length);
								}
								Console.Write(" ... ");
							}
							Console.Write(" done");
							Console.WriteLine();
						}
						microphone.StopCapturing();
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
