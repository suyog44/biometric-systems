using System;
using System.Globalization;
using Neurotec.Biometrics;
using Neurotec.Biometrics.Client;
using Neurotec.Licensing;

namespace Neurotec.Tutorials
{
	class Program
	{
		private static int Usage()
		{
			Console.WriteLine(@"usage:");
			Console.WriteLine(@"	{0} [reference_face_image] [multiple_faces_image]", TutorialUtils.GetAssemblyName());
			Console.WriteLine();
			Console.WriteLine(@"	[reference_face_image]  - filename of image with a single (reference) face.");
			Console.WriteLine(@"	[multiple_faces_image]  - filename of image with multiple faces.");

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
				// obtain license
				if (!NLicense.ObtainComponents("/local", 5000, Components))
				{
					throw new ApplicationException(string.Format("Could not obtain licenses for components: {0}", Components));
				}

				using (var biometricClient = new NBiometricClient())
				// Create subjects with face object
				using (NSubject referenceSubject = CreateSubject(args[0], false))
				using (NSubject candidateSubject = CreateSubject(args[1], true))
				{
					// Create reference subject template
					NBiometricStatus status = biometricClient.CreateTemplate(referenceSubject);
					if (status != NBiometricStatus.Ok)
					{
						Console.WriteLine("Template creation was unsuccessful. Status: {0}.", status);
						return -1;
					}

					// Create candidate subjects templates
					status = biometricClient.CreateTemplate(candidateSubject);
					if (status != NBiometricStatus.Ok)
					{
						Console.WriteLine("Template creation was unsuccessful. Status: {0}.", status);
						return -1;
					}

					// Set ids to candidate subjects and related subjects
					int i = 1;
					candidateSubject.Id = "GallerySubject_0";
					foreach (var subject in candidateSubject.RelatedSubjects)
					{
						subject.Id = string.Format("GallerySubject_{0}", i++);
					}

					// Create enrolment task
					NBiometricTask enrollTask = biometricClient.CreateTask(NBiometricOperations.Enroll, null);
					
					// Add subject and related subjects to enrollment task
					enrollTask.Subjects.Add(candidateSubject);
					foreach (var subject in candidateSubject.RelatedSubjects)
					{
						enrollTask.Subjects.Add(subject);
					}

					// Enroll candidate subjects and related subject
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
					biometricClient.FacesMatchingSpeed = NMatchingSpeed.Low;

					// Identify probe subject
					status = biometricClient.Identify(referenceSubject);
					if (status == NBiometricStatus.Ok)
					{
						foreach (var matchingResult in referenceSubject.MatchingResults)
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

		private static NSubject CreateSubject(string fileName, bool isMultipleSubjects)
		{
			var subject = new NSubject {IsMultipleSubjects = isMultipleSubjects};
			var face = new NFace { FileName = fileName };
			subject.Faces.Add(face);
			return subject;
		}
	}
}
