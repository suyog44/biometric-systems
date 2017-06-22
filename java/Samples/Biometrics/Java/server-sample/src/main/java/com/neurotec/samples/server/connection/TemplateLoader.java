package com.neurotec.samples.server.connection;

import com.neurotec.biometrics.NSubject;

public interface TemplateLoader {

	// ==============================================
	// Public abstract methods
	// ==============================================

	public abstract void beginLoad() throws Exception;
	public abstract void endLoad() throws Exception;
	public abstract NSubject[] loadNext(int count) throws Exception;
	public abstract int getTemplateCount() throws Exception;
}
