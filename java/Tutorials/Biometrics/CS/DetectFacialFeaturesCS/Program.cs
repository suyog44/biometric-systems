using System;
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
			Console.WriteLine("\t{0} [image]", TutorialUtils.GetAssemblyName());
			Console.WriteLine();
			Console.WriteLine("\t[image] - filename of image.");
			return 1;
		}

		static int Main(string[] args)
		{
			string components = "Biometrics.FaceDetection,Biometrics.FaceExtraction";
			const string AdditionalComponents = "Biometrics.FaceSegmentsDetection";

			TutorialUtils.PrintTutorialHeader(args);

			if (args.Length < 1)
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
				using (var face = new NFace())
				{
					// Read face image from file and add it to NFace object
					face.FileName = args[0];

					// Read face image from file and add it NSubject
					subject.Faces.Add(face);

					// Detect multiple faces
					subject.IsMultipleSubjects = true;

					bool isAdditionalComponentActivated = NLicense.IsComponentActivated(AdditionalComponents);
					if (isAdditionalComponentActivated)
					{
						// Set which features should be detected
						biometricClient.FacesDetectBaseFeaturePoints = true;
						biometricClient.FacesDetectAllFeaturePoints = true;
						biometricClient.FacesRecognizeEmotion = true;
						biometricClient.FacesRecognizeExpression = true;
						biometricClient.FacesDetectProperties = true;
						biometricClient.FacesDetermineGender = true;
						biometricClient.FacesDetermineAge = true;
					}

					// Set template size
					biometricClient.FacesTemplateSize = NTemplateSize.Medium;

					// Create segment detection task
					var task = biometricClient.CreateTask(NBiometricOperations.DetectSegments, subject);

					// Perform task
					biometricClient.PerformTask(task);
					NBiometricStatus status = task.Status;

					// Get detection details of the extracted face
					if (status == NBiometricStatus.Ok)
					{
						foreach (var attributes in face.Objects)
						{
							Console.WriteLine("\tlocation = ({0}, {1}), width = {2}, height = {3}",
								attributes.BoundingRect.X, attributes.BoundingRect.Y, attributes.BoundingRect.Width, attributes.BoundingRect.Height);

							PrintNleFeaturePoint("LeftEyeCenter", attributes.LeftEyeCenter);
							PrintNleFeaturePoint("RightEyeCenter", attributes.RightEyeCenter);

							if (isAdditionalComponentActivated)
							{
								PrintNleFeaturePoint("MouthCenter", attributes.MouthCenter);
								PrintNleFeaturePoint("NoseTip", attributes.NoseTip);
								Console.WriteLine();
								foreach (var featurePoint in attributes.FeaturePoints)
								{
									PrintBaseFeaturePoint(featurePoint);
								}

								Console.WriteLine();
								if (attributes.Age == 254) Console.WriteLine("\t\tAge not detected");
								else Console.WriteLine("\t\tAge: {0}", attributes.Age);
								if (attributes.GenderConfidence == 255) Console.WriteLine("\t\tGender not detected");
								else Console.WriteLine("\t\tGender: {0}, Confidence: {1}", attributes.Gender, attributes.GenderConfidence);
								if (attributes.ExpressionConfidence == 255) Console.WriteLine("\t\tExpression not detected");
								else Console.WriteLine("\t\tExpression: {0}, Confidence: {1}", attributes.Expression, attributes.ExpressionConfidence);
								if (attributes.BlinkConfidence == 255) Console.WriteLine("\t\tBlink not detected");
								else Console.WriteLine("\t\tBlink: {0}, Confidence: {1}", (attributes.Properties & NLProperties.Blink) == NLProperties.Blink, attributes.BlinkConfidence);
								if (attributes.MouthOpenConfidence == 255) Console.WriteLine("\t\tMouth open not detected");
								else Console.WriteLine("\t\tMouth open: {0}, Confidence: {1}", (attributes.Properties & NLProperties.MouthOpen) == NLProperties.MouthOpen, attributes.MouthOpenConfidence);
								if (attributes.GlassesConfidence == 255) Console.WriteLine("\t\tGlasses not detected");
								else Console.WriteLine("\t\tGlasses: {0}, Confidence: {1}", (attributes.Properties & NLProperties.Glasses) == NLProperties.Glasses, attributes.GlassesConfidence);
								if (attributes.DarkGlassesConfidence == 255) Console.WriteLine("\t\tDark glasses not detected");
								else Console.WriteLine("\t\tDark glasses: {0}, Confidence: {1}", (attributes.Properties & NLProperties.DarkGlasses) == NLProperties.DarkGlasses, attributes.DarkGlassesConfidence);

								Console.WriteLine();
							}
						}
					}
					else
					{
						Console.WriteLine("Face detection failed! Status = {0}", status);
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

		private static void PrintNleFeaturePoint(string name, NLFeaturePoint point)
		{
			if (point.Confidence == 0)
			{
				Console.WriteLine("\t\t{0} feature unavailable. confidence: 0", name);
				return;
			}
			Console.WriteLine("\t\t{0} feature found. X: {1}, Y: {2}, confidence: {3}", name, point.X, point.Y, point.Confidence);
		}

		private static void PrintBaseFeaturePoint(NLFeaturePoint point)
		{
			if (point.Confidence == 0)
			{
				Console.WriteLine("\t\tBase feature point unavailable. confidence: 0");
				return;
			}
			Console.WriteLine("\t\tBase feature point found. X: {0}, Y: {1}, confidence: {2}, Code: {3}", point.X, point.Y, point.Confidence, point.Code);
		}
	}
}
