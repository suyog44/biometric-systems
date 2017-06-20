package com.neurotec.samples.server.controls;

import java.awt.Frame;
import java.awt.event.ActionListener;
import java.util.concurrent.ExecutionException;

import javax.swing.JPanel;

import com.neurotec.biometrics.client.NBiometricClient;
import com.neurotec.samples.server.LongTask;
import com.neurotec.samples.server.connection.AcceleratorConnection;
import com.neurotec.samples.server.connection.TemplateLoader;

public abstract class BasePanel extends JPanel implements ActionListener {

	// ==============================================
	// Private static fields
	// ==============================================

	private static final long serialVersionUID = 1L;

	// ==============================================
	// Private fields
	// ==============================================

	private Frame owner;
	private NBiometricClient biometricClient;
	private TemplateLoader templateLoader;
	private AcceleratorConnection acceleratorConnection;

	// ==============================================
	// Public constructor
	// ==============================================

	public BasePanel(Frame owner) {
		super();
		this.owner = owner;
	}

	// ==============================================
	// Public abstract methods
	// ==============================================

	public abstract boolean isBusy();

	public abstract void cancel();

	public abstract String getTitle();

	public abstract void waitForCurrentProcessToFinish() throws InterruptedException, ExecutionException;

	// ==============================================
	// Public methods
	// ==============================================

	public final Frame getOwner() {
		return owner;
	}

	public final int getTemplateCount() throws InterruptedException, ExecutionException {
		return (Integer)LongTaskDialog.runLongTask(owner, "Calculating template count", new TemplateCounter());
	}

	public final NBiometricClient getBiometricClient() {
		return biometricClient;
	}

	public final void setBiometricClient(NBiometricClient biometricClient) {
		this.biometricClient = biometricClient;
	}

	public final TemplateLoader getTemplateLoader() {
		return templateLoader;
	}

	public final void setTemplateLoader(TemplateLoader templateLoader) {
		this.templateLoader = templateLoader;
	}

	public final AcceleratorConnection getAccelerator() {
		return acceleratorConnection;
	}

	public void setAccelerator(AcceleratorConnection acceleratorConnection) {
		this.acceleratorConnection = acceleratorConnection;
	}

	// ==============================================
	// Private class
	// ==============================================

	private class TemplateCounter implements LongTask {
		@Override
		public Object doInBackground() {
			int result = 0;
			try {
				if (templateLoader != null) {
					result = templateLoader.getTemplateCount();
				} else {
					result = -1;
				}
			} catch (Exception e) {
				e.printStackTrace();
			}
			return result;
		}

	}

}
