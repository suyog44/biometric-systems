using Neurotec.Biometrics;
using Neurotec.Biometrics.Client;
using Neurotec.Samples.Properties;

namespace Neurotec.Samples.Code
{
	static class SettingsAccesor
	{
		#region Connection Settings

		public static bool UseDb
		{
			get
			{
				try { return Settings.Default.UseDB; }
				catch { return false; }
			}
			set { Settings.Default.UseDB = value; }
		}

		public static string TemplateDir
		{
			get
			{
				try { return Settings.Default.TemplateDir; }
				catch { return string.Empty; }
			}
			set { Settings.Default.TemplateDir = value; }
		}

		public static string DbServer
		{
			get
			{
				try { return Settings.Default.Server; }
				catch { return null; }
			}
			set { Settings.Default.Server = value; }
		}

		public static string DbTable
		{
			get
			{
				try { return Settings.Default.Table; }
				catch { return null; }
			}
			set { Settings.Default.Table = value; }
		}

		public static string DbUser
		{
			get
			{
				try { return Settings.Default.User; }
				catch { return null; }
			}
			set { Settings.Default.User = value; }
		}

		public static string DbPassword
		{
			get
			{
				try { return Settings.Default.Password; }
				catch { return null; }
			}
			set { Settings.Default.Password = value; }
		}

		public static string IdColumn
		{
			get
			{
				try { return Settings.Default.IDCollumn; }
				catch { return "dbid"; }
			}
			set { Settings.Default.IDCollumn = value; }
		}

		public static string TemplateColumn
		{
			get
			{
				try { return Settings.Default.TemplateCollumn; }
				catch { return "template"; }
			}
			set { Settings.Default.TemplateCollumn = value; }
		}

		public static string Server
		{
			get
			{
				try { return Settings.Default.MMAServer; }
				catch { return null; }
			}
			set { Settings.Default.MMAServer = value; }
		}

		public static int ClientPort
		{
			get
			{
				try { return Settings.Default.MMAPort; }
				catch { return 25452; }
			}
			set { Settings.Default.MMAPort = value; }
		}

		public static int AdminPort
		{
			get
			{
				try { return Settings.Default.MMAAdminPort; }
				catch { return 24932; }
			}
			set { Settings.Default.MMAAdminPort = value; }
		}

		public static string UserName
		{
			get
			{
				try { return Settings.Default.MMAUser; }
				catch { return "Admin"; }
			}
			set { Settings.Default.MMAUser = value; }
		}

		public static string Password
		{
			get
			{
				try { return Settings.Default.MMAPassword; }
				catch { return "Admin"; }
			}
			set { Settings.Default.MMAPassword = value; }
		}

		public static bool IsAccelerator
		{
			get
			{
				try { return Settings.Default.IsAccelerator; }
				catch { return false; }
			}
			set { Settings.Default.IsAccelerator = value; }
		}

		#endregion

		#region Matching parameters settings

		#region General

		public static readonly int DefaultMatchingThreshold = 48;
		public static int MatchingThreshold
		{
			get
			{
				try { return Settings.Default.MatchingThreshold; }
				catch { return DefaultMatchingThreshold; }
			}
			set { Settings.Default.MatchingThreshold = value; }
		}

		#endregion

		#region Fingers

		public static readonly NMatchingSpeed DefaultFingersMatchingSpeed = NMatchingSpeed.Low;
		public static NMatchingSpeed FingersMatchingSpeed
		{
			get
			{
				try { return Settings.Default.FingersMatchingSpeed; }
				catch { return DefaultFingersMatchingSpeed; }
			}
			set { Settings.Default.FingersMatchingSpeed = value; }
		}

		public static readonly byte DefaultFingersMaximalRotation = 128;
		public static byte FingersMaximalRotation
		{
			get
			{
				try { return Settings.Default.FingersMaximalRotation; }
				catch { return DefaultFingersMaximalRotation; }
			}
			set { Settings.Default.FingersMaximalRotation = value; }
		}

		#endregion

		#region Faces

		public static readonly NMatchingSpeed DefaultFacesMatchingSpeed = NMatchingSpeed.Low;
		public static NMatchingSpeed FacesMatchingSpeed
		{
			get
			{
				try { return Settings.Default.FacesMatchingSpeed; }
				catch { return DefaultFacesMatchingSpeed; }
			}
			set { Settings.Default.FacesMatchingSpeed = value; }
		}

		#endregion

		#region Irises

		public static readonly NMatchingSpeed DefaultIrisesMatchingSpeed = NMatchingSpeed.Low;
		public static NMatchingSpeed IrisesMatchingSpeed
		{
			get
			{
				try { return Settings.Default.IrisesMatchingSpeed; }
				catch { return NMatchingSpeed.Low; }
			}
			set { Settings.Default.IrisesMatchingSpeed = value; }
		}

		public static readonly byte DefaultIrisesMaximalRotation = 11;
		public static byte IrisesMaximalRotation
		{
			get
			{
				try { return Settings.Default.IrisesMaxRotation; }
				catch { return DefaultIrisesMaximalRotation; }
			}
			set { Settings.Default.IrisesMaxRotation = value; }
		}

		#endregion

		#region Palms

		public static readonly NMatchingSpeed DefaultPalmsMatchingSpeed = NMatchingSpeed.Low;
		public static NMatchingSpeed PalmsMatchingSpeed
		{
			get
			{
				try { return Settings.Default.PalmsMatchingSpeed; }
				catch { return DefaultPalmsMatchingSpeed; }
			}
			set { Settings.Default.PalmsMatchingSpeed = value; }
		}

		public static readonly byte DefaultPalmsMaximalRotation = 128;
		public static byte PalmsMaximalRotation
		{
			get
			{
				try { return Settings.Default.PalmsMaximalRotation; }
				catch { return DefaultPalmsMaximalRotation; }
			}
			set { Settings.Default.PalmsMaximalRotation = value; }
		}

		#endregion

		#endregion

		#region Public static methods

		public static void ResetMatchingSettings()
		{
			MatchingThreshold = DefaultMatchingThreshold;
			FingersMatchingSpeed = DefaultFingersMatchingSpeed;
			FingersMaximalRotation = DefaultFingersMaximalRotation;
			FacesMatchingSpeed = DefaultFacesMatchingSpeed;
			IrisesMatchingSpeed = DefaultIrisesMatchingSpeed;
			IrisesMaximalRotation = DefaultIrisesMaximalRotation;
			PalmsMatchingSpeed = DefaultPalmsMatchingSpeed;
			PalmsMaximalRotation = DefaultPalmsMaximalRotation;
		}

		public static void SetMatchingParameters(NBiometricClient biometricClient)
		{
			//General params
			biometricClient.MatchingThreshold = MatchingThreshold;

			//Finger params
			biometricClient.FingersMatchingSpeed = FingersMatchingSpeed;
			biometricClient.FingersMaximalRotation = FingersMaximalRotation;

			//Faces params
			biometricClient.FacesMatchingSpeed = FacesMatchingSpeed;

			//irises
			biometricClient.IrisesMatchingSpeed = IrisesMatchingSpeed;
			biometricClient.IrisesMaximalRotation = IrisesMaximalRotation;

			//palms
			biometricClient.PalmsMatchingSpeed = PalmsMatchingSpeed;
			biometricClient.PalmsMaximalRotation = PalmsMaximalRotation;
		}

		public static void SaveSettings()
		{
			Settings.Default.Save();
		}

		#endregion
	}
}
