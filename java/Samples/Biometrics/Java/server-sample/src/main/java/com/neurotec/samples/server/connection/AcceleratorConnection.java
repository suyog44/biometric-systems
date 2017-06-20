package com.neurotec.samples.server.connection;

import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStream;
import java.io.InputStreamReader;
import java.net.Authenticator;
import java.net.HttpURLConnection;
import java.net.PasswordAuthentication;
import java.net.URL;

import com.neurotec.samples.server.settings.Settings;

public final class AcceleratorConnection extends ServerConnection {

	// ==============================================
	// Private fields
	// ==============================================

	private final String username;
	private final String password;

	// ==============================================
	// Public constructor
	// ==============================================

	public AcceleratorConnection(String url, int clientPort, int adminPort, String username, String password) {
		super(url, clientPort, adminPort);
		this.username = username;
		this.password = password;
	}

	// ==============================================
	// Public methods
	// ==============================================

	public int getDbSize() throws IOException {
		HttpURLConnection connection = null;
		BufferedReader rd = null;
		InputStream is = null;
		try {
			String serverAddress = getServer();
			if (!serverAddress.startsWith("http://")) {
				serverAddress = "http://" + serverAddress;
			}
			if (serverAddress.endsWith("/")) {
				serverAddress = serverAddress.substring(0, serverAddress.length() - 1);
			}
			String request = String.format("%s:%s/rcontrol.php?a=getDatabaseSize", serverAddress, 80);
			URL url = new URL(request);
			connection = (HttpURLConnection) url.openConnection();

			final String authUsername;
			final String authPassword;
			if (username == null || username.equals("")) {
				authUsername = Settings.getInstance().getMMAUser();
				authPassword = Settings.getInstance().getMMAPassword();
			} else {
				authUsername = username;
				authPassword = password;
			}
			Authenticator.setDefault(new Authenticator() {
				@Override
				protected PasswordAuthentication getPasswordAuthentication() {
					return new PasswordAuthentication(authUsername, authPassword.toCharArray());
				}
			});

			connection.setRequestMethod("POST");
			connection.setDoOutput(true);
			connection.setRequestProperty("Content-Type", "application/x-www-form-urlencoded");

			int timeOut = 1000 * 60 * 180;
			connection.setConnectTimeout(timeOut);
			connection.setReadTimeout(timeOut);
			connection.setUseCaches(false);

			is = connection.getInputStream();
			rd = new BufferedReader(new InputStreamReader(is));
			String s = rd.readLine();
			int value = Integer.parseInt(s);
			return value;
		} finally {
			if (is != null) {
				is.close();
			}
			if (rd != null) {
				rd.close();
			}
			if (connection != null) {
				connection.disconnect();
			}
		}
	}
}
