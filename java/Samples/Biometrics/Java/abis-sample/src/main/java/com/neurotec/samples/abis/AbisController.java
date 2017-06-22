package com.neurotec.samples.abis;

import com.neurotec.biometrics.NBiometricTask;
import com.neurotec.biometrics.NSubject;
import com.neurotec.samples.abis.schema.DatabaseSchema;
import com.neurotec.util.concurrent.CompletionHandler;

public interface AbisController {

	void start();
	void createNewSubject();
	void openSubject();
	void getSubject();
	void settings();
	void changeDatabase();
	void editSchema(DatabaseSchema schema);
	CompletionHandler<NBiometricTask, Void> databaseOperation(String title, String message, NSubject subject);
	void about();
	void dispose();
}
