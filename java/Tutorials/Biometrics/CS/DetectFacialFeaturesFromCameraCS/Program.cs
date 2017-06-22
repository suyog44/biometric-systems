using System;
using System.IO;
using Neurotec.Biometrics;
using Neurotec.Biometrics.Client;
using Neurotec.ComponentModel;
using Neurotec.Devices;
using Neurotec.Licensing;
using Neurotec.Plugins;

namespace Neurotec.Tutorials
{
	class Program
	{
		const string AdditionalComponents = "Biometrics.FaceSegmentsDetection";

		private static int Usage()
		{
			Console.WriteLine("usage:");
			Console.WriteLine("\t{0} [FaceTemplate] [FaceImage] [TokenFaceImage]", TutorialUtils.GetAssemblyName());
			Console.WriteLine("\t{0} [FaceTemplate] [FaceImage] [TokenFaceImage] [-u url](optional)", TutorialUtils.GetAssemblyName());
			Console.WriteLine("\t{0} [FaceTemplate] [FaceImage] [TokenFaceImage] [-f filename](optional)", TutorialUtils.GetAssemblyName());
			Console.WriteLine();
			Console.WriteLine("\t[FaceTemplate] - filename for template.");
			Console.WriteLine("\t[FaceImage] - filename for face image.");
			Console.WriteLine("\t[TokenFaceImage] - filename for token face image.");
			Console.WriteLine("\t[-u url] - (optional) url to RTSP stream");
			Console.WriteLine("\t[-f filename] - (optional) video file containing a face");
			Console.WriteLine("\tIf url(-u) or filename(-f) attribute is not specified first attached camera will be used");
			return 1;
		}

		static int Main(string[] args)
		{
			string components = "Biometrics.FaceDetection,Biometrics.FaceExtraction,Devices.Cameras,"
				+ "Biometrics.FaceSegmentation,Biometrics.FaceQualityAssessment";

			TutorialUtils.PrintTutorialHeader(args);

			if (args.Length != 3 && args.Length != 5)
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

				using (var biometricClient = new NBiometricClient { UseDeviceManager = true, BiometricTypes = NBiometricType.Face })
				using (var subject = new NSubject())
				using (var face = new NFace { CaptureOptions = NBiometricCaptureOptions.Stream })
				{
					biometricClient.Initialize();

					// Create camera from filename or RTSP stram if attached
					NCamera camera;
					var deviceManager = biometricClient.DeviceManager;
					if (args.Length == 5)
					{
						camera = (NCamera)ConnectDevice(deviceManager, args[4], args[3].Equals("-u"));

					}
					else
					{
						// Get count of connected devices
						int count = deviceManager.Devices.Count;

						if (count == 0)
						{
							Console.WriteLine("no cameras found, exiting ...\n");
							return -1;
						}

						//select the first available camera
						camera = (NCamera)deviceManager.Devices[0];
					}
					biometricClient.FaceCaptureDevice = camera;

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

					biometricClient.FacesTemplateSize = NTemplateSize.Large;

					subject.Faces.Add(face);

					var task = biometricClient.CreateTask(NBiometricOperations.Capture | NBiometricOperations.DetectSegments
						| NBiometricOperations.Segment | NBiometricOperations.AssessQuality, subject);

					Console.Write("Starting to capture. Please look into the camera... ");
					biometricClient.PerformTask(task);
					Console.WriteLine("Done.");

					if (task.Status == NBiometricStatus.Ok)
					{
						// print face attributes
						PrintFaceAttributes(face);
						// template
						File.WriteAllBytes(args[0], subject.GetTemplateBuffer().ToArray());
						// original face
						face.Image.Save(args[1]);
						// token face image
						subject.Faces[1].Image.Save(args[2]);
					}
					else
					{
						Console.WriteLine("Capturing failed! Status: {0}", task.Status);
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

		private static void PrintBaseFeaturePoint(NLFeaturePoint point)
		{
			if (point.Confidence == 0)
			{
				Console.WriteLine("\t\tBase feature point unavailable. confidence: 0");
				return;
			}
			Console.WriteLine("\t\tBase feature point found. X: {0}, Y: {1}, confidence: {2}, Code: {3}", point.X, point.Y, point.Confidence, point.Code);
		}

		private static NDevice ConnectDevice(NDeviceManager deviceManager, string url, bool isUrl)
		{
			NPlugin plugin = NDeviceManager.PluginManager.Plugins["Media"];
			if (plugin.State == NPluginState.Plugged && NDeviceManager.IsConnectToDeviceSupported(plugin))
			{
				NParameterDescriptor[] parameters = NDeviceManager.GetConnectToDeviceParameters(plugin);
				var bag = new NParameterBag(parameters);
				if (isUrl)
				{
					bag.SetProperty("DisplayName", "IP Camera");
					bag.SetProperty("Url", url);
				}
				else
				{
					bag.SetProperty("DisplayName", "Video file");
					bag.SetProperty("FileName", url);
				}
				return deviceManager.ConnectToDevice(plugin, bag.ToPropertyBag());
			}
			throw new Exception("Failed to connect specified device!");
		}
	}
}
