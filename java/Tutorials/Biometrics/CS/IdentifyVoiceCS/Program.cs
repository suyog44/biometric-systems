using System;
using Neurotec.Licensing;
using Neurotec.Biometrics;
using Neurotec.Biometrics.Client;

namespace Neurotec.Tutorials
{
	static class Program
	{
		private static int Usage()
		{
			Console.WriteLine("usage:");
			Console.WriteLine("\t{0} [probe voice] [one or more gallery voices]", TutorialUtils.GetAssemblyName());
			Console.WriteLine();

			return 1;
		}

		static int Main(string[] args)
		{
			const string Components = "Biometrics.VoiceExtraction,Biometrics.VoiceMatching";

			TutorialUtils.PrintTutorialHeader(args);

			if (args.Length < 2)
			{
				return Usage();
			}

			try
			{
				// obtain license
				if (!NLicense.ObtainComponents("/local", 5000, Components))
				{
					throw new ApplicationException(string.Format("Could not obtain licenses for components: {0}", Components));
				}

				using (var client = new NBiometricClient())
				using (var probeSubject = CreateSubject(args[0], "ProbeSubject"))
				{
					// creating  probe subjects template
					NBiometricStatus status = client.CreateTemplate(probeSubject);
					if (status != NBiometricStatus.Ok)
					{
						Console.WriteLine("Failed to create probe template. Status: {0}.", status);
						return -1;
					}

					// creating task for enrollment
					NBiometricTask task = client.CreateTask(NBiometricOperations.Enroll, null);
					for (int i = 1; i < args.Length; ++i)
					{
						task.Subjects.Add(CreateSubject(args[i], string.Format("GallerySubject_{0}", i)));
					}

					// perform enrollment task
					client.PerformTask(task);
					status = task.Status;
					if (status != NBiometricStatus.Ok)
					{
						Console.WriteLine("Enrollment was unsuccessful. Status: {0}.", status);
						if (task.Error != null) throw task.Error;
						return -1;
					}

					// Set matching threshold
					client.MatchingThreshold = 48;

					// Identify probe subject
					status = client.Identify(probeSubject);

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
			var voice = new NVoice { FileName = fileName };
			subject.Voices.Add(voice);
			subject.Id = subjectId;
			return subject;
		}
	}
}
