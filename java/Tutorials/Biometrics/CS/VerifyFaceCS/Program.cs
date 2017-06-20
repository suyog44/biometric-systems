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
			Console.WriteLine("\t{0} [reference image] [candidate image]", TutorialUtils.GetAssemblyName());
			Console.WriteLine();

			return 1;
		}

		static int Main(string[] args)
		{
			const string Components = "Biometrics.FaceExtraction,Biometrics.FaceMatching";

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
				// Create subjects with face object
				using (NSubject referenceSubject = CreateSubject(args[0], args[0]))
				using (NSubject candidateSubject = CreateSubject(args[1], args[1]))
				{
					// Set matching threshold
					biometricClient.MatchingThreshold = 48;

					// Set matching speed
					biometricClient.FacesMatchingSpeed = NMatchingSpeed.Low;

					// Verify subjects
					NBiometricStatus status = biometricClient.Verify(referenceSubject, candidateSubject);
					if (status == NBiometricStatus.Ok || status == NBiometricStatus.MatchNotFound)
					{
						int score = referenceSubject.MatchingResults[0].Score;
						Console.Write("image scored {0}, verification.. ", score);
						Console.WriteLine(status == NBiometricStatus.Ok ? "succeeded" : "failed");
					}
					else
					{
						Console.Write("Verification failed. Status: {0}", status);
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
			var subject = new NSubject {Id = subjectId};
			var face = new NFace { FileName = fileName };
			subject.Faces.Add(face);
			return subject;
		}
	}
}
