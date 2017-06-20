using System;
using System.IO;
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
			Console.WriteLine("\t{0} [image]", TutorialUtils.GetAssemblyName());
			Console.WriteLine();
			Console.WriteLine("\timage - image of fingerprint to be classified");
			Console.WriteLine();
			return 1;
		}

		static int Main(string[] args)
		{
			const string Components = "Biometrics.FingerSegmentsDetection";

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
				// Create subject
				using (NSubject subject = CreateSubject(args[0], NFPosition.Unknown))
				{
					// Set paramter to classfiy fingeprint
					biometricClient.FingersDeterminePatternClass = true;

					// Create task
					NBiometricTask task = biometricClient.CreateTask(NBiometricOperations.DetectSegments, subject);

					// Perform task
					biometricClient.PerformTask(task);
					if (task.Status == NBiometricStatus.Ok)
					{
						using (var result = subject.Fingers.Last())
						{
							Console.WriteLine("Fingerprint pattern class is \"{0}\", confidence {1:f2}",
								result.Objects[0].PatternClass, result.Objects[0].PatternClassConfidence);
						}
					}
					else
					{
						Console.WriteLine("Calssification failed. Status: {0}", task.Status);
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

		private static NSubject CreateSubject(string fileName, NFPosition position)
		{
			var subject = new NSubject {Id = fileName};
			var finger = new NFinger { FileName = fileName, Position = position };
			subject.Fingers.Add(finger);
			return subject;
		}
	}
}
