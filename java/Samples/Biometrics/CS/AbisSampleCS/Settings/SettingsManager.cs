using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Neurotec.Biometrics;
using Neurotec.Biometrics.Client;
using Neurotec.Devices;
using Neurotec.Samples.Properties;

namespace Neurotec.Samples
{
	public enum ConnectionType
	{
		SQLiteDatabase,
		OdbcDatabase,
		RemoteMatchingServer,
	};

	public class SampleDbSchema
	{
		#region Public constructor

		public SampleDbSchema()
		{
			BiographicData = new NBiographicDataSchema();
			CustomData = new NBiographicDataSchema();
		}

		#endregion

		#region Public properties

		public NBiographicDataSchema BiographicData { get; set; }
		public NBiographicDataSchema CustomData { get; set; }
		public string GenderDataName { get; set; }
		public string EnrollDataName { get; set; }
		public string ThumbnailDataName { get; set; }
		public string SchemaName { get; set; }
		public bool HasCustomData
		{
			get
			{
				return CustomData != null && CustomData.Elements.Count > 0;
			}
		}
		public bool IsEmpty
		{
			get
			{
				return this == Empty;
			}
		}

		#endregion

		#region Public fields

		public static readonly SampleDbSchema Empty = new SampleDbSchema() { SchemaName = "None" };

		#endregion

		#region Public methods

		public override string ToString()
		{
			return SchemaName;
		}

		public static SampleDbSchema Parse(string value)
		{
			SampleDbSchema sc = new SampleDbSchema();
			var values = value.Split(new string[] { "#" }, StringSplitOptions.None);
			if (values.Length != 6) throw new ArgumentException();

			sc.SchemaName = values[0];
			sc.BiographicData = NBiographicDataSchema.Parse(values[1]);
			if (!string.IsNullOrEmpty(values[2]))
				sc.CustomData = NBiographicDataSchema.Parse(values[2]);

			sc.GenderDataName = values[3].Split(new char[] { '=' })[1];
			sc.ThumbnailDataName = values[4].Split(new char[] { '=' })[1];
			sc.EnrollDataName = values[5].Split(new char[] { '=' })[1];

			return sc;
		}

		public string Save()
		{
			if (IsEmpty) throw new InvalidOperationException();

			string format = "{0}#{1}#{2}#Gender={3}#Thumbnail={4}#EnrollData={5}";
			return string.Format(format, SchemaName, (object)BiographicData ?? string.Empty, (object)CustomData ?? string.Empty, GenderDataName, ThumbnailDataName, EnrollDataName);
		}

		#endregion
	};

	public static class SettingsManager
	{
		#region Public methods

		public static void LoadSettings(NBiometricClient client)
		{
			Settings s = Settings.Default;
			NPropertyBag propertyBag = null;
			string propertiesString = string.Empty;

			if (client == null) throw new ArgumentNullException("client");

			client.Reset();
			client.UseDeviceManager = true;
			try { propertiesString = Settings.Default.ClientProperties; }
			catch { }
			propertyBag = NPropertyBag.Parse(propertiesString);
			propertyBag.ApplyTo(client);

			client.FingersDeterminePatternClass = client.FingersDeterminePatternClass && LicensingTools.CanDetectFingerSegments(client.LocalOperations); ;
			client.FingersCalculateNfiq = client.FingersCalculateNfiq && LicensingTools.CanAssessFingerQuality(client.LocalOperations);
			var remoteConnection = client.RemoteConnections.FirstOrDefault();
			NBiometricOperations remoteOperations = remoteConnection != null ? remoteConnection.Operations : NBiometricOperations.None;
			client.FingersCheckForDuplicatesWhenCapturing = client.FingersCheckForDuplicatesWhenCapturing && LicensingTools.CanFingerBeMatched(remoteOperations);
			if (!LicensingTools.CanDetectFaceSegments(client.LocalOperations))
			{
				client.FacesDetectAllFeaturePoints = false;
				client.FacesDetectBaseFeaturePoints = false;
				client.FacesDetermineGender = false;
				client.FacesRecognizeEmotion = false;
				client.FacesDetectProperties = false;
				client.FacesRecognizeEmotion = false;
				client.FacesDetermineAge = false;
			}
		}

		public static void LoadPreferedDevices(NBiometricClient client)
		{
			Settings s = Settings.Default;
			try
			{
				if (!string.IsNullOrEmpty(s.FingerScanner))
				{
					NDevice device = client.DeviceManager.Devices.FirstOrDefault(x => x.Id == s.FingerScanner);
					if (device != null) client.FingerScanner = (NFScanner)device;
				}
			}
			catch { }
			try
			{
				if (!string.IsNullOrEmpty(s.PalmScanner))
				{
					NDevice device = client.DeviceManager.Devices.FirstOrDefault(x => x.Id == s.PalmScanner);
					if (device != null) client.PalmScanner = (NFScanner)device;
				}
			}
			catch { }
			try
			{
				if (!string.IsNullOrEmpty(s.IrisScanner))
				{
					NDevice device = client.DeviceManager.Devices.FirstOrDefault(x => x.Id == s.IrisScanner);
					if (device != null) client.IrisScanner = (NIrisScanner)device;
				}
			}
			catch { }
			try
			{
				if (!string.IsNullOrEmpty(s.FaceCaptureDevice))
				{
					NDevice device = client.DeviceManager.Devices.FirstOrDefault(x => x.Id == s.FaceCaptureDevice);
					if (device != null) client.FaceCaptureDevice = (NCamera)device;
				}
			}
			catch { }
			try
			{
				if (!string.IsNullOrEmpty(s.VoiceCaptureDevice))
				{
					NDevice device = client.DeviceManager.Devices.FirstOrDefault(x => x.Id == s.VoiceCaptureDevice);
					if (device != null) client.VoiceCaptureDevice = (NMicrophone)device;
				}
			}
			catch { }
		}

		public static void SaveSettings(NBiometricClient client)
		{
			Settings s = Settings.Default;
			NPropertyBag properties = new NPropertyBag();

			if (client == null) throw new ArgumentNullException("client");

			client.CaptureProperties(properties);
			s.ClientProperties = properties.ToString();

			// prefered devices
			s.FaceCaptureDevice = client.FaceCaptureDevice != null ? client.FaceCaptureDevice.Id : null;
			s.FingerScanner = client.FingerScanner != null ? client.FingerScanner.Id : null;
			s.PalmScanner = client.PalmScanner != null ? client.PalmScanner.Id : null;
			s.IrisScanner = client.IrisScanner != null ? client.IrisScanner.Id : null;
			s.VoiceCaptureDevice = client.VoiceCaptureDevice != null ? client.VoiceCaptureDevice.Id : null;

			s.Save();
		}

		#endregion

		#region Public properties

		public static IEnumerable<Phrase> Phrases
		{
			get
			{
				string values = null;
				try { values = Settings.Default.Phrases; }
				catch { };

				if (!string.IsNullOrEmpty(values))
				{
					string[] split = values.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
					foreach (var item in split)
					{
						Phrase phrase = null;
						try
						{
							var splitPhrase = item.Split('=');
							phrase = new Phrase(int.Parse(splitPhrase[0]), splitPhrase[1]);
						}
						catch
						{
							//ignore invalid entries
							continue;
						};
						yield return phrase;
					}
				}
			}
			set
			{
				if (value != null)
				{
					StringBuilder phrases = new StringBuilder();
					foreach (Phrase phrase in value)
					{
						phrases.AppendFormat("{0}={1};", phrase.Id, phrase.String);
					}
					Settings.Default.Phrases = phrases.ToString();
				}
				else
				{
					Settings.Default.Phrases = string.Empty;
				}
				Settings.Default.Save();
			}
		}

		public static ConnectionType ConnectionType
		{
			get
			{
				try { return (ConnectionType)Settings.Default.ConnectionType; }
				catch { return ConnectionType.SQLiteDatabase; }
			}
			set
			{
				Settings.Default.ConnectionType = (int)value;
				Settings.Default.Save();
			}
		}

		public static string OdbcConnectionString
		{
			get
			{
				try { return Settings.Default.OdbcConnectionString; }
				catch { return string.Empty; }
			}
			set
			{
				Settings.Default.OdbcConnectionString = value;
				Settings.Default.Save();
			}
		}

		public static string TableName
		{
			get
			{
				try { return Settings.Default.TableName; }
				catch { return string.Empty; }
			}
			set
			{
				Settings.Default.TableName = value;
				Settings.Default.Save();
			}
		}

		public static string RemoteServerAddress
		{
			get
			{
				try { return Settings.Default.HostName; }
				catch { return "localhost"; }
			}
			set
			{
				Settings.Default.HostName = value;
				Settings.Default.Save();
			}
		}

		public static int RemoteServerPort
		{
			get
			{
				try { return Settings.Default.ClientPort; }
				catch { return 25452; }
			}
			set
			{
				Settings.Default.ClientPort = value;
				Settings.Default.Save();
			}
		}

		public static int RemoteServerAdminPort
		{
			get
			{
				try { return Settings.Default.AdminPort; }
				catch { return 24932; }
			}
			set
			{
				Settings.Default.AdminPort = value;
				Settings.Default.Save();
			}
		}

		public static int FingersGeneralizationRecordCount
		{
			get
			{
				try { return Settings.Default.FingersGeneralizationRecordCount; }
				catch { return 3; }
			}
			set
			{
				Settings.Default.FingersGeneralizationRecordCount = value;
				Settings.Default.Save();
			}
		}

		public static int PalmsGeneralizationRecordCount
		{
			get
			{
				try { return Settings.Default.PalmsGeneralizationRecordCount; }
				catch { return 3; }
			}
			set
			{
				Settings.Default.PalmsGeneralizationRecordCount = value;
				Settings.Default.Save();
			}
		}

		public static int FacesGeneralizationRecordCount
		{
			get
			{
				try { return Settings.Default.FacesGeneralizationRecordCount; }
				catch { return 3; }
			}
			set
			{
				Settings.Default.FacesGeneralizationRecordCount = value;
				Settings.Default.Save();
			}
		}

		public static string[] QuerySuggestions
		{
			get
			{
				try { return Settings.Default.QueryAutoComplete.OfType<string>().ToArray(); }
				catch { return new string[0]; }
			}
			set
			{
				Settings.Default.QueryAutoComplete = new System.Collections.Specialized.StringCollection();
				if (value != null)
				{
					Settings.Default.QueryAutoComplete.AddRange(value);
				}
				Settings.Default.Save();
			}
		}

		public static bool WarnHasSchema
		{
			get
			{
				try { return Settings.Default.WarnHasSchema; }
				catch { return true; }
			}
			set
			{
				Settings.Default.WarnHasSchema = value;
				Settings.Default.Save();
			}
		}

		public static int CurrentSchemaIndex
		{
			get
			{
				try { return Settings.Default.CurrentScema; }
				catch { return 0; }
			}
			set
			{
				Settings.Default.CurrentScema = value;
				Settings.Default.Save();
			}
		}

		public static IEnumerable<SampleDbSchema> Schemas
		{
			get
			{
				string[] schemas = null;
				try { schemas = Settings.Default.Schemas.OfType<string>().ToArray(); }
				catch { schemas = new string[0]; }

				return schemas.Select(x => SampleDbSchema.Parse(x)).ToArray();
			}
			set
			{
				Settings.Default.Schemas.Clear();
				if (value != null)
				{
					foreach (var item in value)
					{
						Settings.Default.Schemas.Add(item.Save());
					}
				}
				Settings.Default.Save();
			}
		}

		public static SampleDbSchema CurrentSchema
		{
			get
			{
				if (CurrentSchemaIndex == -1)
					return SampleDbSchema.Empty;
				else
					return Schemas.ToArray()[CurrentSchemaIndex];
			}
		}

		public static int LocalOperationsIndex
		{
			get
			{
				try { return Settings.Default.LocalOperations; }
				catch { return 5; }
			}
			set
			{
				Settings.Default.LocalOperations = value;
				Settings.Default.Save();
			}
		}

		public static bool FacesMirrorHorizontally
		{
			get
			{
				try { return Settings.Default.FacesMirrorHorizontally; }
				catch { return false; }
			}
			set
			{
				Settings.Default.FacesMirrorHorizontally = value;
				Settings.Default.Save();
			}
		}

		#endregion
	}
}
