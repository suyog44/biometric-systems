using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

using Neurotec.Biometrics;
using Neurotec.Biometrics.Client;
using Neurotec.IO;
using Neurotec.Licensing;

namespace Neurotec.Tutorials
{
	class Program
	{
		private static readonly string[] MatchingComponents =
		{
			"Biometrics.FingerMatching",
			"Biometrics.FaceMatching",
			"Biometrics.IrisMatching",
			"Biometrics.PalmMatching",
			"Biometrics.VoiceMatching"
		};

		private static int Usage()
		{
			Console.WriteLine("usage:");
			Console.WriteLine("\t{0} [probe template] [one or more gallery templates]", TutorialUtils.GetAssemblyName());
			Console.WriteLine();
			return 1;
		}

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
				// Read probe template
				using (NSubject probeSubject = CreateSubject(args[0], "ProbeSubject"))
				{
					// Read gallery templates and enroll them
					NBiometricTask enrollTask = biometricClient.CreateTask(NBiometricOperations.Enroll, null);
					for (int i = 1; i < args.Length; ++i)
					{
						enrollTask.Subjects.Add(CreateSubject(args[i], string.Format("GallerySubject_{0}", i)));
					}
					biometricClient.PerformTask(enrollTask);
					NBiometricStatus status = enrollTask.Status;
					if (status != NBiometricStatus.Ok)
					{
						Console.WriteLine("Enrollment was unsuccessful. Status: {0}.", status);
						if (enrollTask.Error != null) throw enrollTask.Error;
						return -1;
					}

					// Set matching threshold
					biometricClient.MatchingThreshold = 48;

					// Set paramater to return matching details
					biometricClient.MatchingWithDetails = true;
					
					// Identify probe subject
					status = biometricClient.Identify(probeSubject);
					if (status == NBiometricStatus.Ok)
					{
						foreach (var matchingResult in probeSubject.MatchingResults)
						{
							Console.WriteLine("Matched with ID: '{0}' with score {1}", matchingResult.Id, matchingResult.Score);
							if (matchingResult.MatchingDetails != null)
							{
								Console.WriteLine(MatchingDetailsToString(matchingResult.MatchingDetails));
							}
						}
					}
					else if (status == NBiometricStatus.MatchNotFound)
					{
						Console.WriteLine("Match not found!");
					}
					else
					{
						Console.WriteLine("Identification failed. Status: {0}", status);
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

		private static string MatchingDetailsToString(NMatchingDetails details)
		{
			var detailsStr = new StringBuilder();
			if ((details.BiometricType & NBiometricType.Finger) == NBiometricType.Finger)
			{
				detailsStr.Append("    Fingerprint match details:");
				detailsStr.AppendLine(string.Format(" score = {0}", details.FingersScore));
				foreach (NFMatchingDetails fngrDetails in details.Fingers)
				{
					detailsStr.AppendLine(string.Format("    fingerprint index: {0}; score: {1};", fngrDetails.MatchedIndex, fngrDetails.Score));
				}
			}
			if ((details.BiometricType & NBiometricType.Face) == NBiometricType.Face)
			{
				detailsStr.Append("    Face match details:");
				detailsStr.AppendLine(string.Format(" face index: {0}; score: {1}", details.FacesMatchedIndex, details.FacesScore));
				foreach (NLMatchingDetails faceDetails in details.Faces)
				{
					detailsStr.AppendLine(string.Format("    face index: {0}; score: {1};", faceDetails.MatchedIndex, faceDetails.Score));
				}
			}
			if ((details.BiometricType & NBiometricType.Iris) == NBiometricType.Iris)
			{
				detailsStr.Append("    Irises match details:");
				detailsStr.AppendLine(string.Format(" score = {0}", details.IrisesScore));
				foreach (NEMatchingDetails irisesDetails in details.Irises)
				{
					detailsStr.AppendLine(string.Format("    irises index: {0}; score: {1};", irisesDetails.MatchedIndex, irisesDetails.Score));
				}
			}
			if ((details.BiometricType & NBiometricType.Palm) == NBiometricType.Palm)
			{
				detailsStr.Append("    Palmprint match details:");
				detailsStr.AppendLine(string.Format(" score = {0}", details.PalmsScore));
				foreach (NFMatchingDetails fngrDetails in details.Palms)
				{
					detailsStr.AppendLine(string.Format("    palmprint index: {0}; score: {1};", fngrDetails.MatchedIndex, fngrDetails.Score));
				}
			}
			if ((details.BiometricType & NBiometricType.Voice) == NBiometricType.Voice)
			{
				detailsStr.Append("    Voice match details:");
				detailsStr.AppendLine(string.Format(" score = {0}", details.VoicesScore));
				foreach (NSMatchingDetails voicesDetails in details.Voices)
				{
					detailsStr.AppendLine(string.Format("    voices index: {0}; score: {1};", voicesDetails.MatchedIndex, voicesDetails.Score));
				}
			}
			return detailsStr.ToString();
		}
	}
}
