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
			Console.WriteLine("\t{0} [input image] [output image]", TutorialUtils.GetAssemblyName());
			Console.WriteLine();

			return 1;
		}

		static int Main(string[] args)
		{
			const string Components = "Biometrics.IrisExtraction,Biometrics.IrisSegmentation";

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
				using (var iris = new NIris())
				{
					// Read iris image from file and add it to NIris object
					iris.FileName = args[0];

					// Set iris image type;
					iris.ImageType = NEImageType.CroppedAndMasked;

					// Read iris image from file and add it NSubject
					subject.Irises.Add(iris);

					// Create segmentation task
					NBiometricTask task = biometricClient.CreateTask(NBiometricOperations.Segment, subject);

					// Perform task
					biometricClient.PerformTask(task);
					NBiometricStatus status = task.Status;
					if (status == NBiometricStatus.Ok)
					{
						foreach (var attributes in subject.Irises[0].Objects)
						{
							Console.WriteLine("overall quality\t{0}\n", attributes.Quality);
							Console.WriteLine("GrayScaleUtilisation\t{0}\n", attributes.GrayScaleUtilisation);
							Console.WriteLine("Interlace\t{0}\n", attributes.Interlace);
							Console.WriteLine("IrisPupilConcentricity\t{0}\n", attributes.IrisPupilConcentricity);
							Console.WriteLine("IrisPupilContrast\t{0}\n", attributes.IrisPupilContrast);
							Console.WriteLine("IrisRadius\t{0}\n", attributes.IrisRadius);
							Console.WriteLine("IrisScleraContrast\t{0}\n", attributes.IrisScleraContrast);
							Console.WriteLine("MarginAdequacy\t{0}\n", attributes.MarginAdequacy);
							Console.WriteLine("PupilBoundaryCircularity\t{0}\n", attributes.PupilBoundaryCircularity);
							Console.WriteLine("PupilToIrisRatio\t{0}\n", attributes.PupilToIrisRatio);
							Console.WriteLine("Sharpness\t{0}\n", attributes.Sharpness);
							Console.WriteLine("UsableIrisArea\t{0}\n", attributes.UsableIrisArea);
						}
						using (var image = subject.Irises[1].Image)
						{
							image.Save(args[1]);
						}
					}
					else
					{
						Console.WriteLine("Segmentation failed. Status {0}", status);
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
