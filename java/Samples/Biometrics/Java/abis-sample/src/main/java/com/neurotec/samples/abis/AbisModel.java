package com.neurotec.samples.abis;

import java.beans.PropertyChangeListener;
import java.beans.PropertyChangeSupport;
import java.io.IOException;
import java.util.HashSet;
import java.util.List;
import java.util.Map.Entry;
import java.util.Set;
import java.util.zip.DataFormatException;

import com.neurotec.biometrics.NBiometricStatus;
import com.neurotec.biometrics.NGender;
import com.neurotec.biometrics.NSubject;
import com.neurotec.biometrics.client.NBiometricClient;
import com.neurotec.io.NBuffer;
import com.neurotec.samples.abis.schema.DatabaseSchema;
import com.neurotec.samples.abis.settings.SettingsManager;
import com.neurotec.samples.abis.subject.EnrollDataSerializer;
import com.neurotec.samples.abis.util.SettingsUtils;
import com.neurotec.samples.util.LicenseManager;
import com.neurotec.samples.util.Utils;
import com.neurotec.util.NPropertyBag;

public class AbisModel {

	// ===========================================================
	// Static fields
	// ===========================================================

	public static final String PROPERTY_PROGRESS = AbisModel.class.getName() + ".progress";

	// ===========================================================
	// Private fields
	// ===========================================================

	private NBiometricClient client;
	private final Set<String> licenses;
	private final String serverAddress;
	private final String serverPort;
	private DatabaseSchema databaseSchema;

	private final PropertyChangeSupport pcs;
	private int progress;
	private boolean canceled;

	// ===========================================================
	// Public constructors
	// ===========================================================

	public AbisModel(NBiometricClient client) {
		this(client, null, null);
	}

	public AbisModel(NBiometricClient client, String serverAddress, String serverPort) {
		this.client = client;
		this.serverAddress = serverAddress;
		this.serverPort = serverPort;
		this.databaseSchema = DatabaseSchema.EMPTY;
		this.pcs = new PropertyChangeSupport(this);

		licenses = new HashSet<String>(26);
		licenses.add("Biometrics.FingerExtraction");
		licenses.add("Biometrics.PalmExtraction");
		licenses.add("Biometrics.FaceExtraction");
		licenses.add("Biometrics.FaceSegmentsDetection");
		licenses.add("Biometrics.IrisExtraction");
		licenses.add("Biometrics.VoiceExtraction");
		licenses.add("Biometrics.FingerMatchingFast");
		licenses.add("Biometrics.FingerMatching");
		licenses.add("Biometrics.PalmMatchingFast");
		licenses.add("Biometrics.PalmMatching");
		licenses.add("Biometrics.VoiceMatching");
		licenses.add("Biometrics.FaceMatchingFast");
		licenses.add("Biometrics.FaceMatching");
		licenses.add("Biometrics.IrisMatchingFast");
		licenses.add("Biometrics.IrisMatching");
		licenses.add("Biometrics.FingerQualityAssessment");
		licenses.add("Biometrics.FingerSegmentation");
		licenses.add("Biometrics.FingerSegmentsDetection");
		licenses.add("Biometrics.PalmSegmentation");
		licenses.add("Biometrics.FaceSegmentation");
		licenses.add("Biometrics.IrisSegmentation");
		licenses.add("Biometrics.VoiceSegmentation");

		licenses.add("Biometrics.Standards.Fingers");
		licenses.add("Biometrics.Standards.FingerTemplates");
		licenses.add("Biometrics.Standards.Faces");
		licenses.add("Biometrics.Standards.Irises");

		licenses.add("Devices.Cameras");
		licenses.add("Devices.FingerScanners");
		licenses.add("Devices.IrisScanners");
		licenses.add("Devices.PalmScanners");
		licenses.add("Devices.Microphones");
		licenses.add("Images.WSQ");
		licenses.add("Media");
	}

	// ===========================================================
	// Private methods
	// ===========================================================

	private void аpplyValues(NBiometricClient target, ConnectionParams params) {
		target.setDatabaseConnection(null);
		target.getRemoteConnections().clear();

		switch (params.getType()) {
		case SQLITE_DATABASE:
			String dbPath = Utils.combinePath(SettingsUtils.getSettingsFolder(), "BiometricsV50.db");
			target.setDatabaseConnection(null);
			target.getRemoteConnections().clear();
			target.setDatabaseConnectionToSQLite(dbPath);
			break;
		case ODBC_DATABASE:
			target.setDatabaseConnectionToOdbc(params.getConnectionString(), params.getTable());
			break;
		case REMOTE_MATCHING_SERVER:
			int port = params.getClientPort();
			int adminPort = params.getAdminPort();
			String host = params.getHost();
			target.getRemoteConnections().addToCluster(host, port, adminPort);
			break;
		default:
			throw new AssertionError("Unknown ConnentionType:" + params.getType());
		}
	}

	private void setProgress(int progress) {
		int old = this.progress;
		this.progress = progress;
		pcs.firePropertyChange(PROPERTY_PROGRESS, old, this.progress);
	}

	// ===========================================================
	// Public methods
	// ===========================================================

	public void obtainLicenses() throws IOException {
		if (Utils.isNullOrEmpty(serverAddress) || Utils.isNullOrEmpty(serverPort)) {
			LicenseManager.getInstance().obtain(licenses);
		} else {
			LicenseManager.getInstance().obtain(licenses, serverAddress, serverPort);
		}
	}

	public NBiometricStatus initClient(ConnectionParams params) {
		if (canceled) {
			return NBiometricStatus.CANCELED;
		}
		setProgress(1);

		NBiometricClient newClient = new NBiometricClient();
		аpplyValues(newClient, params);
		if (params.getType() == ConnectionType.REMOTE_MATCHING_SERVER) {
			newClient.setLocalOperations(SettingsManager.getCurrentLocalOperationsOption().getOperations());
		}
		SettingsManager.loadSettings(newClient);
		if (canceled) {
			return NBiometricStatus.CANCELED;
		}
		setProgress(50);

		newClient.setBiographicDataSchema(databaseSchema.getBiographicData());
		newClient.setCustomDataSchema(databaseSchema.getCustomData());
		setProgress(60);

		newClient.initialize();
		if (canceled) {
			return NBiometricStatus.CANCELED;
		}
		setProgress(90);

		SettingsManager.loadPreferedDevices(newClient);
		if (params.isClearDatabase()) {
			NBiometricStatus status = newClient.clear();
			if (status != NBiometricStatus.OK) {
				return status;
			}
		}

		SettingsManager.setRemoteServerAddress(params.getHost());
		SettingsManager.setRemoteServerAdminPort(params.getAdminPort());
		SettingsManager.setRemoteServerPort(params.getClientPort());
		SettingsManager.setTableName(params.getTable());
		SettingsManager.setOdbcConnectionString(params.getConnectionString());
		SettingsManager.setConnectionType(params.getType());

		if (canceled) {
			return NBiometricStatus.CANCELED;
		}

		client = newClient;
		return NBiometricStatus.OK;
	}

	public NSubject recreateSubject(NSubject subject) {
		DatabaseSchema schema = getDatabaseSchema();
		NSubject result = subject;
		if (!schema.isEmpty()) {
			NPropertyBag bag = new NPropertyBag();
			subject.captureProperties(bag);
			if (!schema.getEnrollDataName().isEmpty() && bag.containsKey(schema.getEnrollDataName())) {
				NBuffer templateBuffer = subject.getTemplateBuffer();
				NBuffer enrollData = (NBuffer) bag.get(schema.getEnrollDataName());
				try {
					result = EnrollDataSerializer.getInstance().deserialize(templateBuffer, enrollData);
				} catch (DataFormatException e) {
					throw new IllegalStateException("Error during enrolled data deserialization: " + e.getMessage());
				}
				List<String> allowedProperties = schema.getAllowedProperties();
				for (Entry<String, Object> entry : bag.entrySet()) {
					if (!allowedProperties.contains(entry.getKey())) {
						bag.remove(entry.getKey());
					}
				}
				bag.applyTo(result);
				result.setId(subject.getId());
			}
			if (!schema.getGenderDataName().isEmpty() && bag.containsKey(schema.getGenderDataName())) {
				String genderString = (String) bag.get(schema.getGenderDataName());
				result.setProperty(schema.getGenderDataName(), NGender.valueOf(genderString.toUpperCase()));
			}
		}
		return result;
	}

	public NBiometricClient getClient() {
		return client;
	}

	public DatabaseSchema getDatabaseSchema() {
		return databaseSchema;
	}

	public void setDatabaseSchema(DatabaseSchema databaseSchema) {
		this.databaseSchema = databaseSchema;
	}

	public Set<String> getLicenses() {
		return new HashSet<String>(licenses);
	}

	public String getServerAddress() {
		return serverAddress;
	}

	public String getServerPort() {
		return serverPort;
	}

	public void addPropertyChangeListener(PropertyChangeListener l) {
		pcs.addPropertyChangeListener(l);
		LicenseManager.getInstance().addPropertyChangeListener(l);
	}

	public void removePropertyChangeListener(PropertyChangeListener l) {
		pcs.removePropertyChangeListener(l);
		LicenseManager.getInstance().removePropertyChangeListener(l);
	}

	public boolean isCanceled() {
		return canceled;
	}

	public void setCanceled(boolean canceled) {
		this.canceled = canceled;
	}

}
