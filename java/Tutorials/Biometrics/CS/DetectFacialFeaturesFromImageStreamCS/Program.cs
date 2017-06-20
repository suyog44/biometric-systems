using System;
using System.IO;

using Neurotec.Biometrics;
using Neurotec.Biometrics.Client;
using Neurotec.Licensing;
using Neurotec.Media;
using Neurotec.Images;

namespace Neurotec.Tutorials
{
	class Program
	{
		const int MaxFrameCount = 100;
		const string AdditionalComponents = "Biometrics.FaceSegmentsDetection";

		private static int Usage()
		{
			Console.WriteLine("usage:");
			Console.WriteLine("\t{0} [-u url]", TutorialUtils.GetAssemblyName());
			Console.WriteLine("\t{0} [-f filename]", TutorialUtils.GetAssemblyName());
			Console.WriteLine("\t{0} [-d directory]", TutorialUtils.GetAssemblyName());
			Console.WriteLine();
			Console.WriteLine("\t[-u url] - url to RTSP stream");
			Console.WriteLine("\t[-f filename] -  video file containing a face");
			Console.WriteLine("\t[-d directory] - directory containing face images");
			return 1;
		}

		static int Main(string[] args)
		{
			string components = "Biometrics.FaceDetection";

			TutorialUtils.PrintTutorialHeader(args);

			if (args.Length < 2)
			{
				return Usage();
			}

			try
			{
				if (!NLicense.ObtainComponents("/local", 5000, components))
				{
					throw new ApplicationException(string.Format("Could not obtain licenses for components: {0}", components));
				}
				if (NLicense.ObtainComponents("/local", 5000, AdditionalComponents))
				{
					components += "," + AdditionalComponents;
				}

				using (var biometricClient = new NBiometricClient())
				using (var subject = new NSubject())
				using (var face = new NFace { HasMoreSamples = true })
				{
					subject.Faces.Add(face);

					//Set face template size (optional)
					biometricClient.FacesTemplateSize = NTemplateSize.Medium;

					bool isAdditionalComponentActivated = NLicense.IsComponentActivated(AdditionalComponents);
					if (isAdditionalComponentActivated)
					{
						// Set which features should be detected
						biometricClient.FacesDetectBaseFeaturePoints = true;
					}

					// Create NMedia reader or prepare to use a gallery
					NMediaReader reader = null;
					string[] files = null;
					switch(args[0])
					{
						case "-f":
							reader = new NMediaReader(NMediaSource.FromFile(args[1]), NMediaType.Video, true);
							break;
						case "-u":
							reader = new NMediaReader(NMediaSource.FromUrl(args[1]), NMediaType.Video, true);
							break;
						case "-d":
							files = Directory.GetFiles(args[1]);
							break;
						default:
							throw new ArgumentException("Unknown input options specified!");
					}

					// Set from how many frames to detect
					bool isReaderUsed = reader != null;
					var maximumFrames = isReaderUsed ? MaxFrameCount : files.Length;

					// Create Detection task
					var task = biometricClient.CreateTask(NBiometricOperations.DetectSegments, subject);

					// Start the reader if gallery is not used
					if (isReaderUsed) reader.Start();

					for (int i = 0; i < maximumFrames; i++)
					{
						// Read from reader otherwise create an image from specified gallery
						var image = isReaderUsed ? reader.ReadVideoSample() : NImage.FromFile(files[i]);

						// Image will be null when reader has read all available frames
						if (image == null) break;
						face.Image = image;
						biometricClient.PerformTask(task);

						Console.WriteLine(string.Format("[{0}] detection status: {1}", i, task.Status));
						if (task.Status == NBiometricStatus.Ok)
						{
							PrintFaceAttributes(face);
						}
						else if (task.Status != NBiometricStatus.ObjectNotFound)
						{
							Console.WriteLine("Detection failed! Status: {0}", task.Status);
							if (task.Error != null) throw task.Error;
							return -1;
						}
						image.Dispose();
					}

					if (isReaderUsed) reader.Stop();

					// Reset HasMoreSamples value since we finished loading images
					face.HasMoreSamples = false;
				}
				return 0;
			}
			catch (Exception ex)
			{
				return TutorialUtils.PrintException(ex);
			}
			finally
			{
				NLicense.ReleaseComponents(components);
			}
		}

		private static void PrintFaceAttributes(NFace face)
		{
			foreach (var attributes in face.Objects)
			{
				Console.WriteLine("\tlocation = ({0}, {1}), width = {2}, height = {3}",
					attributes.BoundingRect.X, attributes.BoundingRect.Y, attributes.BoundingRect.Width, attributes.BoundingRect.Height);

				PrintNleFeaturePoint("LeftEyeCenter", attributes.LeftEyeCenter);
				PrintNleFeaturePoint("RightEyeCenter", attributes.RightEyeCenter);

				if (NLicense.IsComponentActivated(AdditionalComponents))
				{
					PrintNleFeaturePoint("MouthCenter", attributes.MouthCenter);
					PrintNleFeaturePoint("NoseTip", attributes.NoseTip);
				}
				Console.WriteLine();
			}
		}

		private static void PrintNleFeaturePoint(string name, NLFeaturePoint point)
		{
			if (point.Confidence == 0)
			{
				Console.WriteLine("\t\t{0} feature unavailable. confidence: 0", name);
				return;
			}
			Console.WriteLine("\t\t{0} feature found. X: {1}, Y: {2}, confidence: {3}", name, point.X, point.Y, point.Confidence);
		}

	}
}
