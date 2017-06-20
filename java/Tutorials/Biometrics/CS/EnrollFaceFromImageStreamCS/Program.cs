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
		const string AdditionalComponents = "Biometrics.FaceSegmentsDetection";
		const int MaxFrameCount = 100;

		private static int Usage()
		{
			Console.WriteLine("usage:");
			Console.WriteLine("\t{0} [output template] [-u url]", TutorialUtils.GetAssemblyName());
			Console.WriteLine("\t{0} [output template] [-f filename]", TutorialUtils.GetAssemblyName());
			Console.WriteLine("\t{0} [output template] [-d directory]", TutorialUtils.GetAssemblyName());
			Console.WriteLine();
			Console.WriteLine("\t[-u url] - url to RTSP stream");
			Console.WriteLine("\t[-f filename] -  video file containing a face");
			Console.WriteLine("\t[-d directory] - directory containing face images"); ;
			Console.WriteLine();
			Console.WriteLine("\texample: {0} template.dat -f video.avi", TutorialUtils.GetAssemblyName());
			Console.WriteLine("\texample: {0} template.dat -u rtsp://camera_url", TutorialUtils.GetAssemblyName());
			Console.WriteLine("\texample: {0} template.dat -d C:\templates", TutorialUtils.GetAssemblyName());
			Console.WriteLine();
			return 1;
		}

		static int Main(string[] args)
		{
			string components = "Biometrics.FaceExtraction";

			TutorialUtils.PrintTutorialHeader(args);

			if (args.Length != 3)
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

					//Set face template size (recommended, for enroll to database, is large) (optional)
					biometricClient.FacesTemplateSize = NTemplateSize.Large;

					// Detect all faces features
					bool isAdditionalComponentActivated = NLicense.IsComponentActivated(AdditionalComponents);
					biometricClient.FacesDetectAllFeaturePoints = isAdditionalComponentActivated;

					// Create NMedia reader or prepare to use a gallery
					NMediaReader reader = null;
					string[] files = null;
					switch (args[1])
					{
						case "-f":
							reader = new NMediaReader(NMediaSource.FromFile(args[2]), NMediaType.Video, true);
							break;
						case "-u":
							reader = new NMediaReader(NMediaSource.FromUrl(args[2]), NMediaType.Video, true);
							break;
						case "-d":
							files = Directory.GetFiles(args[2]);
							break;
						default:
							throw new ArgumentException("Unknown input options specified!");
					}

					// Start extraction from stream
					var isReaderUsed = reader != null;
					int i = 0;
					var status = NBiometricStatus.None;
					NImage image = null;
					var task = biometricClient.CreateTask(NBiometricOperations.CreateTemplate, subject);

					if (isReaderUsed) reader.Start();
					while (status == NBiometricStatus.None)
					{
						if (isReaderUsed)
						{
							image = reader.ReadVideoSample();
						}
						else
						{
							image = i < files.Length ? NImage.FromFile(files[i++]) : null;
						}
						if (image == null) break;
						face.Image = image;
						biometricClient.PerformTask(task);
						if (task.Error != null) throw task.Error;
						status = task.Status;
						image.Dispose();
					}
					if (isReaderUsed) reader.Stop();

					// Reset HasMoreSamples value since we finished loading images
					face.HasMoreSamples = false;

					// If loading was finished because MeadiaReaded had no more images we have to
					// finalize extraction by performing task after setting HasMoreSamples to false
					if (image == null)
					{
						biometricClient.PerformTask(task);
						status = task.Status;
					}

					// Return extraction results
					if (status == NBiometricStatus.Ok)
					{
						// Get detection details if the face was detected
						foreach (var attributes in face.Objects)
						{
							Console.WriteLine("face:");
							Console.WriteLine("\tlocation = ({0}, {1}), width = {2}, height = {3}",
							attributes.BoundingRect.X, attributes.BoundingRect.Y, attributes.BoundingRect.Width, attributes.BoundingRect.Height);
							if (attributes.RightEyeCenter.Confidence > 0 || attributes.LeftEyeCenter.Confidence > 0)
							{
								Console.WriteLine("\tfound eyes:");
								if (attributes.RightEyeCenter.Confidence > 0)
									Console.WriteLine("\t\tRight: location = ({0}, {1}), confidence = {2}",
										attributes.RightEyeCenter.X, attributes.RightEyeCenter.Y,
										attributes.RightEyeCenter.Confidence);
								if (attributes.LeftEyeCenter.Confidence > 0)
									Console.WriteLine("\t\tLeft: location = ({0}, {1}), confidence = {2}",
										attributes.LeftEyeCenter.X, attributes.LeftEyeCenter.Y,
										attributes.LeftEyeCenter.Confidence);
							}
							if (isAdditionalComponentActivated && attributes.NoseTip.Confidence > 0)
							{
								Console.WriteLine("\tfound nose:");
								Console.WriteLine("\t\tlocation = ({0}, {1}), confidence = {2}", attributes.NoseTip.X, attributes.NoseTip.Y, attributes.NoseTip.Confidence);
							}
							if (isAdditionalComponentActivated && attributes.MouthCenter.Confidence > 0)
							{
								Console.WriteLine("\tfound mouth:");
								Console.WriteLine("\t\tlocation = ({0}, {1}), confidence = {2}", attributes.MouthCenter.X, attributes.MouthCenter.Y, attributes.MouthCenter.Confidence);
							}
						}

						Console.WriteLine("Template extracted");
						// Save compressed template to file
						File.WriteAllBytes(args[0], subject.GetTemplateBuffer().ToArray());
						Console.WriteLine("Template saved successfully");
					}
					else
					{
						Console.WriteLine("Extraction failed: {0}", status);
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
				NLicense.ReleaseComponents(components);
			}
		}
	}
}
