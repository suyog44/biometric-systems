package com.neurotec.tutorials.biometrics;

import android.app.ProgressDialog;
import android.content.Intent;
import android.net.Uri;
import android.os.Bundle;
import android.util.Log;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;
import android.widget.TextView;

import java.io.File;
import java.io.IOException;
import java.util.EnumSet;

import com.neurotec.biometrics.NBiometricOperation;
import com.neurotec.biometrics.NBiometricStatus;
import com.neurotec.biometrics.NBiometricTask;
import com.neurotec.biometrics.NFinger;
import com.neurotec.biometrics.NSubject;
import com.neurotec.biometrics.NTemplateSize;
import com.neurotec.biometrics.client.NBiometricClient;
import com.neurotec.biometrics.client.NClusterBiometricConnection;
import com.neurotec.io.NFile;
import com.neurotec.samples.app.BaseActivity;
import com.neurotec.samples.app.DirectoryViewer;
import com.neurotec.samples.util.NImageUtils;

public final class CreateFingerTemplateOnServer extends BaseActivity {

	// ==============================================
	// Private static fields
	// ==============================================

	private static final String DEFAULT_IP = "127.0.0.1";
	private static final String DEFAULT_ADMIN_PORT = "24932";
	private static final String DEFAULT_CLIENT_PORT = "25452";
	private static final String TAG = CreateFingerTemplateOnServer.class.getSimpleName();
	private static final int REQUEST_CODE_GET_IMAGE = 1;

	// ==============================================
	// Private fields
	// ==============================================

	private Button mButton;
	private TextView mResult;
	private EditText mIpAddress;
	private EditText mAdminPortNumber;
	private EditText mClientPortNumber;
	private ProgressDialog mProgressDialog;

	// ==============================================
	// Private methods
	// ==============================================

	private boolean validateAdminPort() {
		boolean validPort = true;
		validPort &= mAdminPortNumber.getText() != null;
		validPort &= !mAdminPortNumber.getText().toString().isEmpty();
		try {
			Integer.parseInt(mAdminPortNumber.getText().toString());
		} catch (NumberFormatException e) {
			validPort = false;
		}
		return validPort;
	}

	private boolean validateClientPort() {
		boolean validPort = true;
		validPort &= mClientPortNumber.getText() != null;
		validPort &= !mClientPortNumber.getText().toString().isEmpty();
		try {
			Integer.parseInt(mClientPortNumber.getText().toString());
		} catch (NumberFormatException e) {
			validPort = false;
		}
		return validPort;
	}

	private void getImage() {
		Intent intent = new Intent(this, DirectoryViewer.class);
		intent.putExtra(DirectoryViewer.ASSET_DIRECTORY_LOCATION, BiometricsTutorialsApp.TUTORIALS_ASSETS_DIR);
		startActivityForResult(intent, REQUEST_CODE_GET_IMAGE);
	}

	private void showMessage(final String message) {
		runOnUiThread(new Runnable() {
			@Override
			public void run() {
				mResult.append(message + "\n");
			}
		});
	}

	private void enroll(Uri imageUri) throws IOException {
		NBiometricClient biometricClient = null;
		NClusterBiometricConnection connection = null;
		NSubject subject = null;
		NFinger finger = null;
		NBiometricTask task = null;
		NBiometricStatus status = null;

		try {
			biometricClient = new NBiometricClient();
			connection = new NClusterBiometricConnection();
			subject = new NSubject();
			finger = new NFinger();

			// Perform all biometric operations on remote server only
			biometricClient.setLocalOperations(EnumSet.noneOf(NBiometricOperation.class));

			connection.setHost(mIpAddress.getText().toString());
			connection.setPort(Integer.parseInt(mClientPortNumber.getText().toString()));
			connection.setAdminPort(Integer.parseInt(mAdminPortNumber.getText().toString()));

			biometricClient.getRemoteConnections().add(connection);

			//Set finger image
			finger.setImage(NImageUtils.fromUri(this, imageUri));

			subject.getFingers().add(finger);

			//Set finger template size (recommended, for enroll to database, is large) (optional)
			biometricClient.setFingersTemplateSize(NTemplateSize.SMALL);

			task = biometricClient.createTask(EnumSet.of(NBiometricOperation.CREATE_TEMPLATE), subject);
			biometricClient.performTask(task);
			if (task.getError() != null) {
				showError(task.getError());
				return;
			}
			status = task.getStatus();

			if (subject.getFingers().size() > 1)
			showMessage(String.format("Found %d fingers\n", subject.getFingers().size() - 1));

			if (status == NBiometricStatus.OK) {
				// Save template to file.
				File outputFile = new File(BiometricsTutorialsApp.TUTORIALS_OUTPUT_DATA_DIR, "create-finger-template-on-server.dat");
				NFile.writeAllBytes(outputFile.getAbsolutePath(), subject.getTemplate().save());
				showMessage(getString(R.string.format_finger_image_saved_to, outputFile.getAbsolutePath()));
			} else {
				showMessage(getString(R.string.format_extraction_failed, status.toString()));
			}
		} finally {
			if (biometricClient != null) biometricClient.dispose();
			if (connection != null) connection.dispose();
			if (subject != null) subject.dispose();
			if (finger != null) finger.dispose();
		}
	}

	// ==============================================
	// Protected methods
	// ==============================================

	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		setContentView(R.layout.tutorial_finger_template_on_server);
		mIpAddress = (EditText) findViewById(R.id.tutorials_field_1);
		mIpAddress.setText(DEFAULT_IP);
		mClientPortNumber = (EditText) findViewById(R.id.tutorials_field_2);
		mClientPortNumber.setText(DEFAULT_CLIENT_PORT);
		mClientPortNumber.setHint("Client port, default - " + DEFAULT_CLIENT_PORT);
		mAdminPortNumber = (EditText) findViewById(R.id.tutorials_field_3);
		mAdminPortNumber.setText(DEFAULT_ADMIN_PORT);
		mAdminPortNumber.setHint("Admin port, defalut - " + DEFAULT_ADMIN_PORT);
		mButton = (Button) findViewById(R.id.tutorials_button_1);
		mButton.setText(R.string.msg_select_image);
		mButton.setOnClickListener(new View.OnClickListener() {
			@Override
			public void onClick(View v) {
				if (mIpAddress.getText() == null || mIpAddress.getText().toString().isEmpty()) {
					showMessage(getString(R.string.msg_ip_not_valid));
				} else if (!validateClientPort()) {
					showMessage(getString(R.string.msg_client_port_not_valid));
				} else if (!validateAdminPort()) {
					showMessage(getString(R.string.msg_admin_port_not_valid));
				} else {
					getImage();
				}
			}
		});
		mResult = (TextView) findViewById(R.id.tutorials_results);
	}

	@Override
	protected void onDestroy() {
		super.onDestroy();
		if ((mProgressDialog != null) && (mProgressDialog.isShowing())) {
			mProgressDialog.dismiss();
		}
	}

	@Override
	protected void onActivityResult(int requestCode, int resultCode, Intent data) {
		if (requestCode == REQUEST_CODE_GET_IMAGE) {
			if (resultCode == RESULT_OK) {
				try {
					enroll(data.getData());
				} catch (Exception e) {
					showMessage(e.getMessage());
					Log.e(TAG, "Exception", e);
				}
			}
		}
	}

}
