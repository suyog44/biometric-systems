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
			Console.WriteLine("\t{0} [image]", TutorialUtils.GetAssemblyName());
			Console.WriteLine();
			Console.WriteLine("\timage - image of fingerprint to be evaluated");
			Console.WriteLine();
			return 1;
		}

		static int Main(string[] args)
		{
			const string Components = "Biometrics.FingerQualityAssessmentBase";

			TutorialUtils.PrintTutorialHeader(args);

			if (args.Length < 1)
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

					// Read finger image from file and add it NSubject
					subject.Fingers.Add(finger);

					// Set parameter to calculate NFIQ value
					biometricClient.FingersCalculateNfiq = true;

					// Create Quality Assesment task
					NBiometricTask task = biometricClient.CreateTask(NBiometricOperations.AssessQuality, subject);

					//Perform task
					biometricClient.PerformTask(task);
					NBiometricStatus status = task.Status;
					if (status == NBiometricStatus.Ok)
					{
						Console.WriteLine("Finger quality is: {0}", subject.Fingers[0].Objects[0].NfiqQuality);
					}
					else
					{
						Console.WriteLine("Quality assesment failed: {0}", status);
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
