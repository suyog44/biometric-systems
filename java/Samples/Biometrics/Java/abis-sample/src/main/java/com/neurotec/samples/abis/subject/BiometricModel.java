package com.neurotec.samples.abis.subject;

import com.neurotec.biometrics.NSubject;
import com.neurotec.biometrics.client.NBiometricClient;
import com.neurotec.samples.abis.schema.DatabaseSchema;

public interface BiometricModel {

	NBiometricClient getClient();
	DatabaseSchema getDatabaseSchema();
	NSubject getSubject();
	void firePropertyChange(String name, Object oldValue, Object newValue);

}
