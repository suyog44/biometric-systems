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
			Console.WriteLine("\t{0} [reference voice file] [candidate voice file]", TutorialUtils.GetAssemblyName());
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
				if (!NLicense.ObtainComponents("/local", 5000, Components))
				{
					throw new ApplicationException(string.Format("Could not obtain licenses for components: {0}", Components));
				}

				using (var client = new NBiometricClient())
				using (var referenceSubject = CreateSubject(args[0], "ReferenceSubject"))
				using (var candidateSubject = CreateSubject(args[1], "CandidateSubject"))
				{
					// creating reference subject template
					NBiometricStatus status = client.CreateTemplate(referenceSubject);
					if (status != NBiometricStatus.Ok)
					{
						Console.WriteLine("Reference template creation failed! status: {0}", status);
						return -1;
					}

					// creating candidate subject template
					status = client.CreateTemplate(candidateSubject);
					if (status != NBiometricStatus.Ok)
					{
						Console.WriteLine("Candidate template creation failed! status: {0}", status);
						return -1;
					}

					// verifying subjects
					status = client.Verify(referenceSubject, candidateSubject);
					if (status == NBiometricStatus.Ok || status == NBiometricStatus.MatchNotFound)
					{
						// matching score
						int score = referenceSubject.MatchingResults[0].Score;
						Console.Write("Matching score: {0}, verification ", score);
						Console.WriteLine(status == NBiometricStatus.Ok ? "succeeded" : "failed");
					}
					else
					{
						Console.WriteLine("Verification failed! status: {0}", status);
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
