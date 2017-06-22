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
		private static int Usage()
		{
			Console.WriteLine("usage:");
			Console.WriteLine("\t{0} [image] [template]", TutorialUtils.GetAssemblyName());
			Console.WriteLine("\t{0} [image] [template] [-u url](optional)", TutorialUtils.GetAssemblyName());
			Console.WriteLine("\t{0} [image] [template] [-f filename](optional)", TutorialUtils.GetAssemblyName());
			Console.WriteLine();
			Console.WriteLine("\t[image]  - image filename to store face image.");
			Console.WriteLine("\t[template] - filename to store face template.");
			Console.WriteLine("\t[-u url] - (optional) url to RTSP stream");
			Console.WriteLine("\t[-f filename] - (optional) video file containing a face");
			Console.WriteLine("\tIf url(-u) or filename(-f) attribute is not specified first attached camera will be used");
			Console.WriteLine();
			return 1;
		}

		static int Main(string[] args)
		{
			string components = "Biometrics.FaceExtraction,Devices.Cameras";
			const string AdditionalComponents = "Biometrics.FaceSegmentsDetection";

			TutorialUtils.PrintTutorialHeader(args);

			if (args.Length != 2 && args.Length != 4)
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

				using (var biometricClient = new NBiometricClient { UseDeviceManager = true })
				using (var deviceManager = biometricClient.DeviceManager)
				using (var subject = new NSubject())
				using (var face = new NFace())
				{
					// Set type of the device used
					deviceManager.DeviceTypes = NDeviceType.Camera;

					// Initialize the NDeviceManager
					deviceManager.Initialize();

					// Create camera from filename or RTSP stram if attached
					NCamera camera;
					if (args.Length == 4)
					{
						camera = (NCamera)ConnectDevice(deviceManager, args[3], args[2].Equals("-u"));
						
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

					// Set the selected camera as NBiometricClient Face Capturing Device
					biometricClient.FaceCaptureDevice = camera;

					Console.WriteLine("Capturing from {0}. Please turn camera to face.", biometricClient.FaceCaptureDevice.DisplayName);

					// Define that the face source will be a stream
					face.CaptureOptions = NBiometricCaptureOptions.Stream;

					// Add NFace to NSubject
					subject.Faces.Add(face);

					// Set face template size (recommended, for enroll to database, is large) (optional)
					biometricClient.FacesTemplateSize = NTemplateSize.Large;

					// Detect all faces features
					bool isAdditionalComponentActivated = NLicense.IsComponentActivated(AdditionalComponents);
					biometricClient.FacesDetectAllFeaturePoints = isAdditionalComponentActivated;

					// Start capturing
					NBiometricStatus status = biometricClient.Capture(subject);
					if (status == NBiometricStatus.Ok)
					{
						Console.WriteLine("Capturing succeeded");
					}
					else
					{
						Console.WriteLine("Failed to capture: {0}", status);
						return -1;
					}

					// Get face detection details if face was detected
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

					// Save image to file
					using (var image = subject.Faces[0].Image)
					{
						image.Save(args[0]);
						Console.WriteLine("image saved successfully");
					}

					// Save template to file
					File.WriteAllBytes(args[1], subject.GetTemplateBuffer().ToArray());
					Console.WriteLine("template saved successfully");
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
