package com.neurotec.samples.abis.settings;

import java.util.ArrayList;
import java.util.List;

import com.neurotec.biometrics.NBiometricOperation;
import com.neurotec.biometrics.client.NBiometricClient;
import com.neurotec.biometrics.client.NRemoteBiometricConnection;
import com.neurotec.devices.NCamera;
import com.neurotec.devices.NDevice;
import com.neurotec.devices.NFScanner;
import com.neurotec.devices.NIrisScanner;
import com.neurotec.devices.NMicrophone;
import com.neurotec.devices.NPalmScanner;
import com.neurotec.samples.abis.ConnectionType;
import com.neurotec.samples.abis.LocalOperationsOption;
import com.neurotec.samples.abis.schema.DatabaseSchema;
import com.neurotec.samples.abis.subject.voices.Phrase;
import com.neurotec.samples.util.LicenseManager;
import com.neurotec.samples.util.Utils;
import com.neurotec.util.NPropertyBag;

public final class SettingsManager {

	private SettingsManager() {
		// Suppress default constructor for non-instantiation.
	}

	public static void loadSettings(NBiometricClient client) {
		Settings settings = Settings.getInstance();

		if (client == null) {
			throw new NullPointerException("client");
		}
		client.reset();
		client.setUseDeviceManager(true);
		String propertiesString = settings.getClientProperties();
		if (propertiesString == null) {
			DefaultClientProperties.getInstance().getGeneral().applyTo(client);
			DefaultClientProperties.getInstance().getFingers().applyTo(client);
			DefaultClientProperties.getInstance().getFaces().applyTo(client);
			DefaultClientProperties.getInstance().getIrises().applyTo(client);
			DefaultClientProperties.getInstance().getVoices().applyTo(client);
			DefaultClientProperties.getInstance().getPalms().applyTo(client);
		} else {
			NPropertyBag propertyBag = NPropertyBag.parse(propertiesString);
			propertyBag.applyTo(client);
		}

		client.setFingersDeterminePatternClass(client.isFingersDeterminePatternClass() && (LicenseManager.getInstance().isActivated("Biometrics.FingerSegmentsDetection", true)
																						   || !client.getLocalOperations().contains(NBiometricOperation.DETECT_SEGMENTS)));
		client.setFingersCalculateNFIQ(client.isFingersCalculateNFIQ() && (LicenseManager.getInstance().isActivated("Biometrics.FingerQualityAssessmentBase", true)
																		   || !client.getLocalOperations().contains(NBiometricOperation.ASSESS_QUALITY)));
		NRemoteBiometricConnection connection = null;
		if (!client.getRemoteConnections().isEmpty()) {
			connection = client.getRemoteConnections().get(0);
		}
		client.setFingersCheckForDuplicatesWhenCapturing(client.isFingersCheckForDuplicatesWhenCapturing() && (LicenseManager.getInstance().isActivated("Biometrics.FingerMatching")
																											   || ((connection != null) && connection.getOperations().contains(NBiometricOperation.VERIFY_OFFLINE))));
		if (!LicenseManager.getInstance().isActivated("Biometrics.FaceSegmentation", true) && client.getLocalOperations().contains(NBiometricOperation.DETECT_SEGMENTS)) {
			client.setFacesDetectAllFeaturePoints(false);
			client.setFacesDetectBaseFeaturePoints(false);
			client.setFacesDetermineGender(false);
			client.setFacesRecognizeEmotion(false);
			client.setFacesDetectProperties(false);
			client.setFacesRecognizeEmotion(false);
		}
	}

	public static void loadPreferedDevices(NBiometricClient client)	{
		Settings settings = Settings.getInstance();
		try {
			if (!Utils.isNullOrEmpty(settings.getFingerScanner())) {
				for (NDevice device : client.getDeviceManager().getDevices()) {
					if (device.getId().equals(settings.getFingerScanner())) {
						client.setFingerScanner((NFScanner) device);
						break;
					}
				}
			}
		} catch (Exception e) {}
		try {
			if (!Utils.isNullOrEmpty(settings.getPalmScanner())) {
				for (NDevice device : client.getDeviceManager().getDevices()) {
					if (device.getId().equals(settings.getPalmScanner())) {
						client.setPalmScanner((NPalmScanner) device);
						break;
					}
				}
			}
		} catch (Exception e) {}
		try {
			if (!Utils.isNullOrEmpty(settings.getIrisScanner())) {
				for (NDevice device : client.getDeviceManager().getDevices()) {
					if (device.getId().equals(settings.getIrisScanner())) {
						client.setIrisScanner((NIrisScanner) device);
						break;
					}
				}
			}
		} catch (Exception e) {}
		try {
			if (!Utils.isNullOrEmpty(settings.getFaceCaptureDevice())) {
				for (NDevice device : client.getDeviceManager().getDevices()) {
					if (device.getId().equals(settings.getFaceCaptureDevice())) {
						client.setFaceCaptureDevice((NCamera) device);
						break;
					}
				}
			}
		} catch (Exception e) {}
		try {
			if (!Utils.isNullOrEmpty(settings.getVoiceCaptureDevice())) {
				for (NDevice device : client.getDeviceManager().getDevices()) {
					if (device.getId().equals(settings.getVoiceCaptureDevice())) {
						client.setVoiceCaptureDevice((NMicrophone) device);
						break;
					}
				}
			}
		} catch (Exception e) {}
	}

	public static void saveSettings(NBiometricClient client) {
		Settings settings = Settings.getInstance();
		NPropertyBag properties = new NPropertyBag();

		if (client == null) {
			throw new NullPointerException("client");
		}

		client.captureProperties(properties);
		settings.setClientProperties(properties.toString());

		// prefered devices
		settings.setFaceCaptureDevice(client.getFaceCaptureDevice() != null ? client.getFaceCaptureDevice().getId() : null);
		settings.setFingerScanner(client.getFingerScanner() != null ? client.getFingerScanner().getId() : null);
		settings.setPalmScanner(client.getPalmScanner() != null ? client.getPalmScanner().getId() : null);
		settings.setIrisScanner(client.getIrisScanner() != null ? client.getIrisScanner().getId() : null);
		settings.setVoiceCaptureDevice(client.getVoiceCaptureDevice() != null ? client.getVoiceCaptureDevice().getId() : null);

		settings.save();
	}


	public static List<Phrase> getPhrases() {
		List<Phrase> phrases = new ArrayList<Phrase>();
		String values = null;
		try {
			values = Settings.getInstance().getPhrases();
		} catch (Exception e){};

		if (!Utils.isNullOrEmpty(values)) {
			String[] split = values.split(";");
			for (String item : split) {
				Phrase phrase = null;
				try {
					String[] splitPhrase = item.split("=");
					phrase = new Phrase(Integer.valueOf(splitPhrase[0]), splitPhrase[1]);
				} catch (Exception e) {
					continue;
				}
				phrases.add(phrase);
			}
		}
		return phrases;
	}

	public static void setPhrases(List<Phrase> value) {
		if (value != null) {
			StringBuilder phrases = new StringBuilder();
			for (Phrase phrase : value) {
				phrases.append(String.format("%d=%s;", phrase.getID(), phrase.getPhrase()));
			}
			Settings.getInstance().setPhrases(phrases.toString());
		} else {
			Settings.getInstance().setPhrases("");
		}
		Settings.getInstance().save();
	}

	public static boolean isMirrorFaceView() {
		try {
			return Settings.getInstance().isMirrorFaceView();
		} catch (Exception e) {
			return Settings.getInstance().getDefault().isMirrorFaceView();
		}
	}

	public static void setMirrorFaceView(boolean value) {
		Settings.getInstance().setMirrorFaceView(value);
		Settings.getInstance().save();
	}

	public static ConnectionType getConnectionType() {
		try {
			return Settings.getInstance().getConnectionType();
		} catch (Exception e) {
			return ConnectionType.SQLITE_DATABASE;
		}
	}

	public static void setConnectionType(ConnectionType value) {
		Settings.getInstance().setConnectionType(value);
		Settings.getInstance().save();
	}

	public static String getOdbcConnectionString() {
		String value = Settings.getInstance().getOdbcConnectionString();
		return value == null ? "" : value;
	}

	public static void setOdbcConnectionString(String value) {
		Settings.getInstance().setOdbcConnectionString(value);
		Settings.getInstance().save();
	}

	public static String getTableName() {
		String value = Settings.getInstance().getTableName();
		return value == null ? "Subjects" : value;
	}

	public static void setTableName(String value) {
		Settings.getInstance().setTableName(value);
		Settings.getInstance().save();
	}

	public static String getRemoteServerAddress() {
		String value = Settings.getInstance().getHostName();
		return value == null ? "localhost" : value;
	}

	public static void setRemoteServerAddress(String value) {
		Settings.getInstance().setHostName(value);
		Settings.getInstance().save();
	}

	public static int getRemoteServerPort() {
		try {
			return Settings.getInstance().getClientPort();
		} catch (Exception e) {
			return 25452;
		}
	}

	public static void setRemoteServerPort(int value) {
		Settings.getInstance().setClientPort(value);
		Settings.getInstance().save();
	}

	public static int getRemoteServerAdminPort() {
		try {
			return Settings.getInstance().getAdminPort();
		} catch (Exception e) {
			return 24932;
		}
	}

	public static void setRemoteServerAdminPort(int value) {
		Settings.getInstance().setAdminPort(value);
		Settings.getInstance().save();
	}

	public static int getFingersGeneralizationRecordCount() {
		try {
			return Settings.getInstance().getFingersGeneralizationRecordCount();
		} catch (Exception e) {
			return 3;
		}
	}

	public static void setFingersGeneralizationRecordCount(int value) {
		Settings.getInstance().setFingersGeneralizationRecordCount(value);
		Settings.getInstance().save();
	}

	public static int getPalmsGeneralizationRecordCount() {
		try {
			return Settings.getInstance().getPalmsGeneralizationRecordCount();
		} catch (Exception e) {
			return 3;
		}
	}

	public static void setPalmsGeneralizationRecordCount(int value) {
		Settings.getInstance().setPalmsGeneralizationRecordCount(value);
		Settings.getInstance().save();
	}

	public static int getFacesGeneralizationRecordCount() {
		try {
			return Settings.getInstance().getFacesGeneralizationRecordCount();
		} catch (Exception e) {
			return 3;
		}
	}

	public static void setFacesGeneralizationRecordCount(int value) {
		Settings.getInstance().setFacesGeneralizationRecordCount(value);
		Settings.getInstance().save();
	}

	public static List<String> getQueryAutoComplete() {
		List<String> list = Settings.getInstance().getQueryAutoComplete();
		if (list == null) {
			return Settings.getInstance().getDefault().getQueryAutoComplete();
		} else {
			return list;
		}
	}

	public static void setQueryAutoComplete(List<String> value) {
		Settings.getInstance().setQueryAutoComplete(value);
		Settings.getInstance().save();
	}

	public static LocalOperationsOption getCurrentLocalOperationsOption() {
		String value = Settings.getInstance().getCurrentLocalOperationsOption();
		if (value == null) {
			value = Settings.getInstance().getDefault().getCurrentLocalOperationsOption();
		}
		return LocalOperationsOption.valueOf(value);
	}

	public static void setCurrentLocalOperationsOption(LocalOperationsOption value) {
		Settings.getInstance().setCurrentLocalOperationsOption(value.name());
		Settings.getInstance().save();
	}

	public static List<DatabaseSchema> getSchemas() {
		List<String> schemaStrings = Settings.getInstance().getSchemas();
		if (schemaStrings == null) {
			schemaStrings = Settings.getInstance().getDefault().getSchemas();
		}
		List<DatabaseSchema> schemas = new ArrayList<DatabaseSchema>();
		for (String s : schemaStrings) {
			schemas.add(DatabaseSchema.parse(s));
		}
		return schemas;
	}

	public static void setSchemas(List<DatabaseSchema> value) {
		List<String> schemaStrings = new ArrayList<String>();
		for (DatabaseSchema schema : value) {
			schemaStrings.add(schema.save());
		}
		Settings.getInstance().setSchemas(schemaStrings);
		Settings.getInstance().save();
	}

	public static int getCurrentSchemaIndex() {
		try {
			return Settings.getInstance().getCurrentSchema();
		} catch (Exception e) {
			return Settings.getInstance().getDefault().getCurrentSchema();
		}
	}

	public static void setCurrentSchema(int value) {
		Settings.getInstance().setCurrentSchema(value);
		Settings.getInstance().save();
	}

	public static boolean isWarnHasSchema() {
		try {
			return Settings.getInstance().isWarnHasSchema();
		} catch (Exception e) {
			return Settings.getInstance().getDefault().isWarnHasSchema();
		}
	}

	public static void setWarnHasSchema(boolean value) {
		Settings.getInstance().setWarnHasSchema(value);
		Settings.getInstance().save();
	}

}
