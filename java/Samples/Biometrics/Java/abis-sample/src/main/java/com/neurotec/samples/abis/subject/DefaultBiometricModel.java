package com.neurotec.samples.abis.subject;

import com.neurotec.biometrics.NSubject;
import com.neurotec.biometrics.client.NBiometricClient;
import com.neurotec.samples.abis.AbisModel;
import com.neurotec.samples.abis.schema.DatabaseSchema;

import java.beans.PropertyChangeListener;
import java.beans.PropertyChangeSupport;

public class DefaultBiometricModel implements BiometricModel {

	// ===========================================================
	// Private fields
	// ===========================================================

	private final NSubject subject;
	private final AbisModel abisModel;

	private final PropertyChangeSupport pcs;

	// ===========================================================
	// Public constructor
	// ===========================================================

	public DefaultBiometricModel(NSubject mainSubject, AbisModel abisModel) {
		this.subject = mainSubject;
		this.abisModel = abisModel;
		pcs = new PropertyChangeSupport(this);
	}

	// ===========================================================
	// Public methods
	// ===========================================================

	@Override
	public final NSubject getSubject() {
		return subject;
	}

	@Override
	public final NBiometricClient getClient() {
		return abisModel.getClient();
	}

	@Override
	public DatabaseSchema getDatabaseSchema() {
		return abisModel.getDatabaseSchema();
	}

	@Override
	public void firePropertyChange(String name, Object oldValue, Object newValue) {
		pcs.firePropertyChange(name, oldValue, newValue);
	}

	public void addPropertyChangeListener(PropertyChangeListener listener) {
		pcs.addPropertyChangeListener(listener);
	}

	public void removePropertyChangeListener(PropertyChangeListener listener) {
		pcs.removePropertyChangeListener(listener);
	}

}
