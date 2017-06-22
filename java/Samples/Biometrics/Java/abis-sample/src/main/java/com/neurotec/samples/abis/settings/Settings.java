package com.neurotec.samples.abis.settings;

import java.io.File;
import java.util.ArrayList;
import java.util.List;

import com.neurotec.samples.abis.ConnectionType;
import com.neurotec.samples.abis.LocalOperationsOption;
import com.neurotec.samples.abis.util.SettingsUtils;
import com.neurotec.samples.util.Utils;
import com.neurotec.util.NPropertyBag;

import org.simpleframework.xml.Default;
import org.simpleframework.xml.Element;
import org.simpleframework.xml.ElementList;
import org.simpleframework.xml.Serializer;
import org.simpleframework.xml.core.Persister;

@Default
public final class Settings {

	// ===========================================================
	// Private static fields
	// ===========================================================

	private static Settings instance;
	private static Settings defaultSettings;

	// ===========================================================
	// Public static method
	// ===========================================================

	public static synchronized Settings getInstance() {
		if (instance == null) {
			instance = new Settings();
			instance.load();
		}
		return instance;
	}

	// ===========================================================
	// Private fields
	// ===========================================================

	@Element (required = false)
	private String filename;
	@Element (required = false)
	private String connectionType;
	@Element (required = false)
	private String hostName;
	@Element (required = false)
	private Integer clientPort;
	@Element (required = false)
	private Integer adminPort;
	@Element (required = false)
	private String phrases;
	@Element (required = false)
	private Boolean mirrorFaceView;
	@Element (required = false)
	private String fingerScanner;
	@Element (required = false)
	private String irisScanner;
	@Element (required = false)
	private String palmScanner;
	@Element (required = false)
	private String voiceCaptureDevice;
	@Element (required = false)
	private String faceCaptureDevice;
	@Element (required = false)
	private String clientProperties;
	@Element (required = false)
	private String tableName;
	@Element (required = false)
	private String odbcConnectionString;
	@Element (required = false)
	private String lastFingerFilePath;
	@Element (required = false)
	private String lastFaceFilePath;
	@Element (required = false)
	private String lastIrisFilePath;
	@Element (required = false)
	private String lastPalmFilePath;
	@Element (required = false)
	private String lastVoiceFilePath;
	@Element (required = false)
	private Integer fingersGeneralizationRecordCount;
	@Element (required = false)
	private Integer palmsGeneralizationRecordCount;
	@Element (required = false)
	private Integer facesGeneralizationRecordCount;
	@ElementList (required = false)
	private List<String> queryAutoComplete;
	@ElementList (required = false)
	private List<String> schemas;
	@Element (required = false)
	private Integer currentSchema;
	@Element (required = false)
	private String currentLocalOperationsOption;
	@Element (required = false)
	private Boolean warnHasSchema;

	// ===========================================================
	// Private constructor
	// ===========================================================

	private Settings() {
		filename = SettingsUtils.getSettingsFolder() + Utils.FILE_SEPARATOR + "settings.xml";
		setFingersGeneralizationRecordCount(3);
		setPalmsGeneralizationRecordCount(3);
		setFacesGeneralizationRecordCount(3);
	}

	// ===========================================================
	// Private methods
	// ===========================================================

	private void load() {
		File file = new File(filename);
		try {
			if (file.exists()) {
				Serializer serializer = new Persister();
				instance = serializer.read(Settings.class, file);
			} else {
				instance = getDefault();
			}
		} catch (Exception e) {
			e.printStackTrace();
		}
	}

	// ===========================================================
	// Public methods
	// ===========================================================

	public Settings getDefault() {
		if (defaultSettings == null) {
			defaultSettings = new Settings();
			defaultSettings.setConnectionType(ConnectionType.SQLITE_DATABASE);
			defaultSettings.setHostName("localhost");
			defaultSettings.setAdminPort(24932);
			defaultSettings.setClientPort(25452);
			NPropertyBag defaultProperties = new NPropertyBag();
			defaultProperties.putAll(DefaultClientProperties.getInstance().getGeneral());
			defaultProperties.putAll(DefaultClientProperties.getInstance().getFingers());
			defaultProperties.putAll(DefaultClientProperties.getInstance().getFaces());
			defaultProperties.putAll(DefaultClientProperties.getInstance().getIrises());
			defaultProperties.putAll(DefaultClientProperties.getInstance().getVoices());
			defaultProperties.putAll(DefaultClientProperties.getInstance().getPalms());
			defaultSettings.setClientProperties(defaultProperties.toString());
			defaultSettings.setTableName("Subjects");
			defaultSettings.setMirrorFaceView(true);
			defaultSettings.queryAutoComplete = new ArrayList<String>();
			defaultSettings.getQueryAutoComplete().add("Id IN (");
			defaultSettings.getQueryAutoComplete().add("Id=");
			defaultSettings.getQueryAutoComplete().add("Country=");
			defaultSettings.getQueryAutoComplete().add("City=");
			defaultSettings.getQueryAutoComplete().add("FirstName=");
			defaultSettings.getQueryAutoComplete().add("LastName=");
			defaultSettings.getQueryAutoComplete().add("YearOfBirth > ");
			defaultSettings.getQueryAutoComplete().add("GenderString='Male'");
			defaultSettings.getQueryAutoComplete().add("GenderString='Female'");
			defaultSettings.schemas = new ArrayList<String>();
			defaultSettings.getSchemas().add("Sample db schema#(FirstName string , LastName string , YearOfBirth int , GenderString string, Country string, City string)#(Thumbnail blob, EnrollData blob)#GenderString=GenderString#Thumbnail=Thumbnail#EnrollData=EnrollData");
			defaultSettings.getSchemas().add("Remote server schema#(FirstName string , LastName string , YearOfBirth int , GenderString string, Country string, City string)#()#GenderString=GenderString#Thumbnail=#EnrollData=");
			defaultSettings.setCurrentSchema(0);
			defaultSettings.setCurrentLocalOperationsOption(LocalOperationsOption.ALL.name());
			defaultSettings.setWarnHasSchema(true);
		}
		return defaultSettings;
	}

	public ConnectionType getConnectionType() {
		return ConnectionType.valueOf(connectionType);
	}

	public void setConnectionType(ConnectionType connectionType) {
		this.connectionType = connectionType.name();
	}

	public String getHostName() {
		return hostName;
	}

	public void setHostName(String hostName) {
		this.hostName = hostName;
	}

	public int getClientPort() {
		return clientPort;
	}

	public void setClientPort(int clientPort) {
		this.clientPort = clientPort;
	}

	public int getAdminPort() {
		return adminPort;
	}

	public void setAdminPort(int adminPort) {
		this.adminPort = adminPort;
	}

	public String getPhrases() {
		return phrases;
	}

	public void setPhrases(String phrases) {
		this.phrases = phrases;
	}

	public boolean isMirrorFaceView() {
		return mirrorFaceView;
	}

	public void setMirrorFaceView(boolean mirrorFaceView) {
		this.mirrorFaceView = mirrorFaceView;
	}

	public String getFingerScanner() {
		return fingerScanner;
	}

	public void setFingerScanner(String fingerScanner) {
		this.fingerScanner = fingerScanner;
	}

	public String getIrisScanner() {
		return irisScanner;
	}

	public void setIrisScanner(String irisScanner) {
		this.irisScanner = irisScanner;
	}

	public String getPalmScanner() {
		return palmScanner;
	}

	public void setPalmScanner(String palmScanner) {
		this.palmScanner = palmScanner;
	}

	public String getVoiceCaptureDevice() {
		return voiceCaptureDevice;
	}

	public void setVoiceCaptureDevice(String voiceCaptureDevice) {
		this.voiceCaptureDevice = voiceCaptureDevice;
	}

	public String getFaceCaptureDevice() {
		return faceCaptureDevice;
	}

	public void setFaceCaptureDevice(String faceCaptureDevice) {
		this.faceCaptureDevice = faceCaptureDevice;
	}

	public String getClientProperties() {
		return clientProperties;
	}

	public void setClientProperties(String clientProperties) {
		this.clientProperties = clientProperties;
	}

	public String getTableName() {
		return tableName;
	}

	public void setTableName(String tableName) {
		this.tableName = tableName;
	}

	public String getOdbcConnectionString() {
		return odbcConnectionString;
	}

	public void setOdbcConnectionString(String odbcConnectionString) {
		this.odbcConnectionString = odbcConnectionString;
	}

	public String getLastFingerFilePath() {
		if (lastFingerFilePath == null) {
			return "";
		} else {
			return lastFingerFilePath;
		}
	}

	public void setLastFingerFilePath(String lastFingerFilePath) {
		this.lastFingerFilePath = lastFingerFilePath;
	}

	public String getLastFaceFilePath() {
		if (lastFaceFilePath == null) {
			return "";
		} else {
			return lastFaceFilePath;
		}
	}

	public void setLastFaceFilePath(String lastFaceFilePath) {
		this.lastFaceFilePath = lastFaceFilePath;
	}

	public String getLastIrisFilePath() {
		if (lastIrisFilePath == null) {
			return "";
		} else {
			return lastIrisFilePath;
		}
	}

	public void setLastIrisFilePath(String lastIrisFilePath) {
		this.lastIrisFilePath = lastIrisFilePath;
	}

	public String getLastPalmFilePath() {
		if (lastPalmFilePath == null) {
			return "";
		} else {
			return lastPalmFilePath;
		}
	}

	public void setLastPalmFilePath(String lastPalmFilePath) {
		this.lastPalmFilePath = lastPalmFilePath;
	}

	public String getLastVoiceFilePath() {
		if (lastVoiceFilePath == null) {
			return "";
		} else {
			return lastVoiceFilePath;
		}
	}

	public void setLastVoiceFilePath(String lastVoiceFilePath) {
		this.lastVoiceFilePath = lastVoiceFilePath;
	}

	public int getFingersGeneralizationRecordCount() {
		return fingersGeneralizationRecordCount;
	}

	public void setFingersGeneralizationRecordCount(int fingersGeneralizationRecordCount) {
		this.fingersGeneralizationRecordCount = fingersGeneralizationRecordCount;
	}

	public int getPalmsGeneralizationRecordCount() {
		return palmsGeneralizationRecordCount;
	}

	public void setPalmsGeneralizationRecordCount(int palmsGeneralizationRecordCount) {
		this.palmsGeneralizationRecordCount = palmsGeneralizationRecordCount;
	}

	public int getFacesGeneralizationRecordCount() {
		return facesGeneralizationRecordCount;
	}

	public void setFacesGeneralizationRecordCount(int facesGeneralizationRecordCount) {
		this.facesGeneralizationRecordCount = facesGeneralizationRecordCount;
	}

	public List<String> getQueryAutoComplete() {
		return queryAutoComplete;
	}

	public void setQueryAutoComplete(List<String> queryAutoComplete) {
		this.queryAutoComplete = queryAutoComplete;
	}

	public List<String> getSchemas() {
		return schemas;
	}

	public void setSchemas(List<String> schemas) {
		this.schemas = schemas;
	}

	public int getCurrentSchema() {
		return currentSchema;
	}

	public void setCurrentSchema(int currentSchema) {
		this.currentSchema = currentSchema;
	}

	public String getCurrentLocalOperationsOption() {
		return currentLocalOperationsOption;
	}

	public void setCurrentLocalOperationsOption(String currentLocalOperationsOption) {
		this.currentLocalOperationsOption = currentLocalOperationsOption;
	}

	public boolean isWarnHasSchema() {
		return warnHasSchema;
	}

	public void setWarnHasSchema(boolean warnHasSchema) {
		this.warnHasSchema = warnHasSchema;
	}

	public void save() {
		Serializer serializer = new Persister();
		File file = new File(filename);
		try {
			//Make sure you create a directory if it is missing
			File directory = file.getParentFile();
			if (!directory.exists()) {
				directory.mkdirs();
			}
			serializer.write(this, file);
		} catch (Exception e) {
			e.printStackTrace();
		}
	}
}
