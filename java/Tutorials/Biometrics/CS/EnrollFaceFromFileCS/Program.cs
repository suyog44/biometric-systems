using System;
using System.IO;

using Neurotec.Biometrics;
using Neurotec.Biometrics.Client;
using Neurotec.Licensing;

namespace Neurotec.Tutorials
{
	class Program
	{
		private static int Usage()
		{
			Console.WriteLine("usage:");
			Console.WriteLine("\t{0} [input file] [output template] [still image or video file]", TutorialUtils.GetAssemblyName());
			Console.WriteLine();
			Console.WriteLine("\t[input file]  - image filename or video file with face.");
			Console.WriteLine("\t[template] - filename to store face template.");
			Console.WriteLine("\t[still image or video file] - specifies that passed source parameter is image (value: 0) or video (value: 1)");
			Console.WriteLine();
			Console.WriteLine("\texample: {0} image.jpg template.dat 0", TutorialUtils.GetAssemblyName());
			Console.WriteLine("\texample: {0} video.avi template.dat 1", TutorialUtils.GetAssemblyName());
			Console.WriteLine();
			return 1;
		}

		static int Main(string[] args)
		{
			string components = "Biometrics.FaceExtraction";
			const string AdditionalComponents = "Biometrics.FaceSegmentsDetection";

			TutorialUtils.PrintTutorialHeader(args);

			if (args.Length < 3)
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

				bool isVideo = false;

				using (var biometricClient = new NBiometricClient())
				using (var subject = new NSubject())
				using (var face = new NFace())
				{
					// Read face image from file and add it to NFace object
					face.FileName = args[0];

					if (args.Length > 2)
						isVideo = args[2] == "1";

					//define that the face source will be a stream
					face.CaptureOptions = isVideo ? NBiometricCaptureOptions.Stream : NBiometricCaptureOptions.None;

					// Read face image from file and add it NSubject
					subject.Faces.Add(face);

					//Set face template size (recommended, for enroll to database, is large) (optional)
					biometricClient.FacesTemplateSize = NTemplateSize.Large;

					// Detect all faces features
					bool isAdditionalComponentActivated = NLicense.IsComponentActivated(AdditionalComponents);
					biometricClient.FacesDetectAllFeaturePoints = isAdditionalComponentActivated;

					// Create template from added face image
					var status = biometricClient.CreateTemplate(subject);
					if (status == NBiometricStatus.Ok)
					{
						Console.WriteLine("Template extracted");
						// Save compressed template to file
						File.WriteAllBytes(args[1], subject.GetTemplateBuffer().ToArray());
						Console.WriteLine("template saved successfully");
					}
					else
					{
						Console.WriteLine("Extraction failed: {0}", status);
						return -1;
					}

					// Get detection details if the face was detected
					foreach (var nface in subject.Faces)
					{
						foreach (var attributes in nface.Objects)
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
