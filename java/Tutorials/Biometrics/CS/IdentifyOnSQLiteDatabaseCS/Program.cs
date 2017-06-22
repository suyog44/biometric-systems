using System;
using System.Collections.Generic;
using System.IO;
using Neurotec.Biometrics;
using Neurotec.Biometrics.Client;
using Neurotec.IO;
using Neurotec.Licensing;

namespace Neurotec.Tutorials
{
	class Program
	{
		private static int Usage()
		{
			Console.WriteLine("usage:");
			Console.WriteLine("\t{0} [template] [path to database file]", TutorialUtils.GetAssemblyName());
			Console.WriteLine("");
			Console.WriteLine("\ttemplate                   - template for identification");
			Console.WriteLine("\tpath to database file      - path to SQLite database file");

			return 1;
		}

		private static readonly string[] MatchingComponents =
		{
			"Biometrics.FingerMatching",
			"Biometrics.FaceMatching",
			"Biometrics.IrisMatching",
			"Biometrics.PalmMatching",
			"Biometrics.VoiceMatching"
		};

		static int Main(string[] args)
		{
			TutorialUtils.PrintTutorialHeader(args);

			if (args.Length < 2)
			{
				return Usage();
			}

			var obtainedLicenses = new List<string>();
			try
			{
				// Obtain licenses.
				foreach (string matchingComponent in MatchingComponents)
				{
					if (NLicense.ObtainComponents("/local", 5000, matchingComponent))
					{
						Console.WriteLine("Obtained license for component: {0}", matchingComponent);
						obtainedLicenses.Add(matchingComponent);
					}
				}
				if (obtainedLicenses.Count == 0)
				{
					throw new NotActivatedException("Could not obtain any matching license");
				}

				using (var biometricClient = new NBiometricClient())
				// Read template
				using (NSubject subject = CreateSubject(args[0], args[0]))
				{
					// Set the SQLite database
					biometricClient.SetDatabaseConnectionToSQLite(args[1]);

					// Create identification task
					NBiometricTask identifyTask = biometricClient.CreateTask(NBiometricOperations.Identify, subject);
					biometricClient.PerformTask(identifyTask);
					NBiometricStatus status = identifyTask.Status;
					if (status != NBiometricStatus.Ok)
					{
						Console.WriteLine("Identification was unsuccessful. Status: {0}.", status);
						if (identifyTask.Error != null) throw identifyTask.Error;
						return -1;
					}
					foreach (var matchingResult in subject.MatchingResults)
					{
						Console.WriteLine("Matched with ID: '{0}' with score {1}", matchingResult.Id, matchingResult.Score);
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
				foreach (string matchingComponent in obtainedLicenses)
				{
					NLicense.ReleaseComponents(matchingComponent);
				}
			}
		}

		private static NSubject CreateSubject(string fileName, string subjectId)
		{
			var subject = new NSubject();
			subject.SetTemplateBuffer(new NBuffer(File.ReadAllBytes(fileName)));
			subject.Id = subjectId;

			return subject;
		}
	}
}
