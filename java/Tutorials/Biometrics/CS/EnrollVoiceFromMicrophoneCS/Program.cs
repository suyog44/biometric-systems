using System;
using Neurotec.Licensing;
using Neurotec.Devices;
using Neurotec.Biometrics;
using Neurotec.Biometrics.Client;
using System.IO;

namespace Neurotec.Tutorials
{
	class Program
	{
		private static int Usage()
		{
			Console.WriteLine("usage:");
			Console.WriteLine("\t{0} [template] [voice]", TutorialUtils.GetAssemblyName());
			Console.WriteLine();
			Console.WriteLine("\t[template] - filename to store sound template.");
			Console.WriteLine("\t[voice] - filename to store voice audio file.");
			Console.WriteLine();
			Console.WriteLine("\texample: {0} template.dat voice.wav", TutorialUtils.GetAssemblyName());
			Console.WriteLine();

			return 1;
		}

		static int Main(string[] args)
		{
			const string Components = "Devices.Microphones,Biometrics.VoiceExtraction";

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

				using (var biometricClient = new NBiometricClient { UseDeviceManager = true })
				using (var deviceManager = biometricClient.DeviceManager)
				using (var subject = new NSubject())
				using (var voice = new NVoice())
				{
					//set type of the device used
					deviceManager.DeviceTypes = NDeviceType.Microphone;

					//initialize the NDeviceManager
					deviceManager.Initialize();

					int i;

					//get count of connected devices
					int count = deviceManager.Devices.Count;

					if (count > 0)
						Console.WriteLine("found {0} microphones", count);
					else
					{
						Console.WriteLine("no microphones found, exiting ...\n");
						return -1;
					}

					//list detected microphones
					if (count > 1)
						Console.WriteLine("Please select microphones from the list: ");
					for (i = 0; i < count; i++)
					{
						NDevice device = deviceManager.Devices[i];
						Console.WriteLine("{0}) {1}", i + 1, device.DisplayName);
					}

					//microphones selection by user
					if (count > 1)
					{
						Console.Write("Please enter microphones index: ");
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

					//set the selected microphones as NBiometricClient Voice Capturing Device
					biometricClient.VoiceCaptureDevice = (NMicrophone)deviceManager.Devices[i];

					Console.WriteLine("recording from {0}.", biometricClient.VoiceCaptureDevice.DisplayName);

					//define that the voice source will be a stream
					voice.CaptureOptions = NBiometricCaptureOptions.Stream;

					//add NVoice to NSubject
					subject.Voices.Add(voice);

					//create capturing task
					NBiometricTask task = biometricClient.CreateTask(NBiometricOperations.Capture | NBiometricOperations.Segment, subject);

					//Perform task
					biometricClient.PerformTask(task);
					NBiometricStatus status = task.Status;

					if (status == NBiometricStatus.Ok)
					{
						// save voice to file
						using (var sound = subject.Voices[1].SoundBuffer)
						{
							sound.Save(args[1]);
							Console.WriteLine("voice saved successfully");
						}

						// save template to file
						File.WriteAllBytes(args[0], subject.GetTemplateBuffer().ToArray());
						Console.WriteLine("template saved successfully");
					}
					else
					{
						Console.WriteLine("Failed to capture: " + status);
						if (task.Error != null) throw task.Error;
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
