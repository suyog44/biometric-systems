using System;
using System.Linq;
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
			Console.WriteLine("\t{0} [FaceImage] [CreateTokenFaceImage]", TutorialUtils.GetAssemblyName());
			Console.WriteLine();
			Console.WriteLine("\t[FaceImage] - an image containing frontal face.");
			Console.WriteLine("\t[CreateTokenFaceImage] - filename of created token face image.");
			return 1;
		}

		static int Main(string[] args)
		{
			const string Components = "Biometrics.FaceDetection,Biometrics.FaceSegmentation,Biometrics.FaceQualityAssessment";

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

				using (var biometricClient = new NBiometricClient())
				using (var subject = new NSubject())
				using (var face = new NFace())
				{
					//Face image will be read from file
					face.FileName = args[0];
					subject.Faces.Add(face);

					//Create segment detection and check the quality of token face image task
					var task = biometricClient.CreateTask(NBiometricOperations.Segment | NBiometricOperations.AssessQuality, subject);

					//Perform task
					biometricClient.PerformTask(task);

					NBiometricStatus status = task.Status;
					if (status == NBiometricStatus.Ok)
					{
						using (var result = subject.Faces[1])
						{
							using (var attributes = result.Objects.First())
							{
								Console.WriteLine("global token face image quality score = {0:f3}. Tested attributes details:", attributes.Quality);
								Console.WriteLine("\tsharpness score = {0:f3}", attributes.Sharpness);
								Console.WriteLine("\tbackground uniformity score = {0:f3}", attributes.BackgroundUniformity);
								Console.WriteLine("\tgrayscale density score = {0:f3}", attributes.GrayscaleDensity);
							}
							using (var image = result.Image)
							{
								image.Save(args[1]);
							}
						}
					}
					else
					{
						Console.WriteLine("Token Face Image creation failed! Status = {0}", status);
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
