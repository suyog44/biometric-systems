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
			Console.WriteLine("\t{0} [image] [position] <optional: missing positions>", TutorialUtils.GetAssemblyName());
			Console.WriteLine("\t[image]             - image containing fingerprints");
			Console.WriteLine("\t[position]          - fingerpints position in provided image");
			Console.WriteLine("\t[missing positions] - one or more NFPosition value of missing fingers");
			Console.WriteLine();
			Console.WriteLine("\tvalid positions:");
			Console.WriteLine("\t\tPlainRightFourFingers = 13, PlainLeftFourFingers = 14, PlainThumbs = 15");
			Console.WriteLine("\t\tRightThumb = 1, RightIndex = 2, RightMiddle = 3, RightRing = 4, RightLittle = 5");
			Console.WriteLine("\t\tLeftThumb = 6, LeftIndex = 7, LeftMiddle = 8, LeftRing = 9, LeftLittle = 10");
			Console.WriteLine();
			Console.WriteLine("\texample: {0} image.png 15", TutorialUtils.GetAssemblyName());
			Console.WriteLine("\texample: {0} image.png 13 2 3", TutorialUtils.GetAssemblyName());
			return 1;
		}

		static int Main(string[] args)
		{
			const string Components = "Biometrics.FingerSegmentation,Biometrics.FingerExtraction";

			TutorialUtils.PrintTutorialHeader(args);
			if (args.Length < 2)
			{
				return Usage();
			}

			try
			{
				// Obtain license
				if (!NLicense.ObtainComponents("/local", 5000, Components))
				{
					throw new ApplicationException(string.Format("Could not obtain licenses for components: {0}", Components));
				}

				using (var biometricClient = new NBiometricClient())
				using (var subject = new NSubject())
				using (var finger = new NFinger())
				{
					// Read finger image from file and add it to NFinger object
					finger.FileName = args[0];

					// Set finger position
					finger.Position = (NFPosition)int.Parse(args[1]);

					// Add finger image from file to NSubject
					subject.Fingers.Add(finger);

					// Set missing finger positions
					for (int i = 2; i < args.Length; i++)
					{
						subject.MissingFingers.Add((NFPosition)int.Parse(args[i]));
					}

					// Create Segment and Create Template task
					NBiometricTask task = biometricClient.CreateTask(NBiometricOperations.Segment
						| NBiometricOperations.CreateTemplate, subject);

					// Perform task
					biometricClient.PerformTask(task);

					// Get task status
					NBiometricStatus status = task.Status;
					if (status == NBiometricStatus.Ok)
					{
						// Check if wrong hand is detected
						if (finger.WrongHandWarning)
						{
							Console.WriteLine("Warning: possibly wrong hand.");
						}
						int segmentCount = subject.Fingers.Count;
						Console.WriteLine("Found {0} segments", segmentCount - 1);

						for (int i = 1; i < segmentCount; i++)
						{
							if (subject.Fingers[i].Status == NBiometricStatus.Ok)
							{
								Console.Write("\t {0}: ", subject.Fingers[i].Position);
								subject.Fingers[i].Image.Save(subject.Fingers[i].Position + ".png");
								Console.WriteLine("Saving image...");
							}
							else
							{
								Console.WriteLine("\t {0}: {1}", subject.Fingers[i].Position, subject.Fingers[i].Status);
							}
						}
					}
					else
					{
						Console.WriteLine("Segementation failed. Status: {0}", status);
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
