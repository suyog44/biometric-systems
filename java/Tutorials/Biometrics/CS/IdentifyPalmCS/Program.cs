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
			Console.WriteLine("\t{0} [probe image] [one or more gallery images]", TutorialUtils.GetAssemblyName());
			Console.WriteLine();

			return 1;
		}

		static int Main(string[] args)
		{
			const string Components = "Biometrics.PalmExtraction,Biometrics.PalmMatching";

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
				// Create probe template
				using (NSubject probeSubject = CreateSubject(args[0], "ProbeSubject"))
				{
					NBiometricStatus status = biometricClient.CreateTemplate(probeSubject);
					if (status != NBiometricStatus.Ok)
					{
						Console.WriteLine("Failed to create probe template. Status: {0}.", status);
						return -1;
					}

					// Create gallery templates and enroll them
					NBiometricTask enrollTask = biometricClient.CreateTask(NBiometricOperations.Enroll, null);
					for (int i = 1; i < args.Length; ++i)
					{
						enrollTask.Subjects.Add(CreateSubject(args[i], string.Format("GallerySubject_{0}", i)));
					}
					biometricClient.PerformTask(enrollTask);
					status = enrollTask.Status;
					if (status != NBiometricStatus.Ok)
					{
						Console.WriteLine("Enrollment was unsuccessful. Status: {0}.", status);
						if (enrollTask.Error != null) throw enrollTask.Error;
						return -1;
					}

					// Set matching threshold
					biometricClient.MatchingThreshold = 48;

					// Set matching speed
					biometricClient.PalmsMatchingSpeed = NMatchingSpeed.Low;

					// Identify probe subject
					status = biometricClient.Identify(probeSubject);
					if (status == NBiometricStatus.Ok)
					{
						foreach (var matchingResult in probeSubject.MatchingResults)
						{
							Console.WriteLine("Matched with ID: '{0}' with score {1}", matchingResult.Id, matchingResult.Score);
						}
					}
					else if (status == NBiometricStatus.MatchNotFound)
					{
						Console.WriteLine("Match not found!");
					}
					else
					{
						Console.WriteLine("Matching failed! Status: {0}", status);
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

		private static NSubject CreateSubject(string fileName, string subjectId)
		{
			var subject = new NSubject();
			var palm = new NPalm { FileName = fileName };
			subject.Palms.Add(palm);
			subject.Id = subjectId;
			return subject;
		}
	}
}
