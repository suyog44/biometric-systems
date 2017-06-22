using System;
using System.IO;
using Neurotec.Biometrics;
using Neurotec.Biometrics.Client;
using Neurotec.IO;

namespace Neurotec.Tutorials
{
	class Program
	{
		private static int Usage()
		{
			Console.WriteLine("usage:");
			Console.WriteLine("\t{0} [template] [path to database file]", TutorialUtils.GetAssemblyName());
			Console.WriteLine("");
			Console.WriteLine("\ttemplate                   - template for enrollment");
			Console.WriteLine("\tpath to database file      - path to SQLite database file");

			return 1;
		}

		static int Main(string[] args)
		{
			TutorialUtils.PrintTutorialHeader(args);

			if (args.Length < 2)
			{
				return Usage();
			}

			try
			{
				using (var biometricClient = new NBiometricClient())
				// Read template
				using (NSubject subject = CreateSubject(args[0], args[0]))
				{
					// Set the SQLite database
					biometricClient.SetDatabaseConnectionToSQLite(args[1]);

					// Create enrollment task
					NBiometricTask enrollTask = biometricClient.CreateTask(NBiometricOperations.Enroll, subject);

					// Perform task
					biometricClient.PerformTask(enrollTask);

					NBiometricStatus status = enrollTask.Status;
					if (status != NBiometricStatus.Ok)
					{
						Console.WriteLine("Enrollment was unsuccessful. Status: {0}.", status);
						if (enrollTask.Error != null) throw enrollTask.Error;
						return -1;
					}
					Console.WriteLine(String.Format("Enrollment was successful. The SQLite database conatins these IDs:"));
					
					// List of enrolled templates
					NSubject[] subjects = biometricClient.List();
					foreach (NSubject subj in subjects)
					{
						Console.WriteLine("\t{0}", subj.Id);
					}
				}

				return 0;
			}
			catch (Exception ex)
			{
				return TutorialUtils.PrintException(ex);
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
