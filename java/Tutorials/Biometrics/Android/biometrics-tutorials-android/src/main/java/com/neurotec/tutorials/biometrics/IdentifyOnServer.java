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

import java.io.IOException;
import java.util.EnumSet;

import com.neurotec.biometrics.NBiometricOperation;
import com.neurotec.biometrics.NBiometricStatus;
import com.neurotec.biometrics.NBiometricTask;
import com.neurotec.biometrics.NMatchingResult;
import com.neurotec.biometrics.NSubject;
import com.neurotec.biometrics.client.NBiometricClient;
import com.neurotec.biometrics.client.NClusterBiometricConnection;
import com.neurotec.io.NBuffer;
import com.neurotec.samples.app.BaseActivity;
import com.neurotec.samples.app.DirectoryViewer;
import com.neurotec.samples.util.IOUtils;

public final class IdentifyOnServer extends BaseActivity {

	private static final String DEFAULT_IP = "127.0.0.1";
	private static final String DEFAULT_PORT = "24932";
	private static final String TAG = IdentifyOnServer.class.getSimpleName();
	private static final int REQUEST_CODE_GET_TEMPLATE = 1;

	private Button mButton;
	private TextView mResult;
	private EditText mTxtIpAddress;
	private EditText mTxtPortNumber;
	private ProgressDialog mProgressDialog;

	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		setContentView(R.layout.tutorial_identify_on_server);
		mTxtIpAddress = (EditText) findViewById(R.id.tutorials_field_1);
		mTxtIpAddress.setText(DEFAULT_IP);
		mTxtPortNumber = (EditText) findViewById(R.id.tutorials_field_2);
		mTxtPortNumber.setText(DEFAULT_PORT);
		mButton = (Button) findViewById(R.id.tutorials_button_1);
		mButton.setText(R.string.msg_select_template);
		mButton.setOnClickListener(new View.OnClickListener() {
			@Override
			public void onClick(View v) {
				if (mTxtIpAddress.getText() == null || mTxtIpAddress.getText().toString().isEmpty()) {
					showMessage(getString(R.string.msg_ip_not_valid));
				} else if (mTxtPortNumber.getText() == null || mTxtPortNumber.getText().toString().isEmpty()) {
					showMessage(getString(R.string.msg_port_not_valid));
				} else {
					try {
						Integer.parseInt(mTxtPortNumber.getText().toString());
						getTemplate();
					} catch (NumberFormatException ex) {
						showMessage(getString(R.string.msg_port_not_valid));
					}
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
		if (requestCode == REQUEST_CODE_GET_TEMPLATE) {
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

	private void getTemplate() {
		Intent intent = new Intent(this, DirectoryViewer.class);
		intent.putExtra(DirectoryViewer.ASSET_DIRECTORY_LOCATION, BiometricsTutorialsApp.TUTORIALS_ASSETS_DIR);
		startActivityForResult(intent, REQUEST_CODE_GET_TEMPLATE);
	}

	private void showMessage(final String message) {
		runOnUiThread(new Runnable() {
			@Override
			public void run() {
				mResult.append(message + "\n");
			}
		});
	}

	private NSubject createSubject(Uri uri) throws IOException {
		NSubject subject = new NSubject();
		subject.setTemplateBuffer(NBuffer.fromByteBuffer(IOUtils.toByteBuffer(IdentifyOnServer.this, uri)));
		subject.setId(uri.getPath());
		return subject;
	}

	private void enroll(Uri templateUri) throws IOException {

		NBiometricClient biometricClient = null;
		NSubject subject = null;
		NClusterBiometricConnection connection = null;
		NBiometricTask task = null;

		try {
			biometricClient = new NBiometricClient();
			subject = createSubject(templateUri);

			connection = new NClusterBiometricConnection();
			connection.setHost(mTxtIpAddress.getText().toString());
			connection.setAdminPort(Integer.parseInt(mTxtPortNumber.getText().toString()));

			biometricClient.getRemoteConnections().add(connection);

			task = biometricClient.createTask(EnumSet.of(NBiometricOperation.IDENTIFY), subject);
			biometricClient.performTask(task);
			if (task.getError() != null) {
				showError(task.getError());
				return;
			}

			if (task.getStatus() != NBiometricStatus.OK) {
				showMessage(getString(R.string.format_identification_unsuccessful, task.getStatus().toString()));
			} else {
				for (NMatchingResult matchingResult : subject.getMatchingResults()) {
					showMessage(getString(R.string.format_matched_with_id_and_score, matchingResult.getId(), matchingResult.getScore()));
					matchingResult.dispose();
				}
			}

		} finally {
			if (task != null) task.dispose();
			if (connection != null) connection.dispose();
			if (subject != null) subject.dispose();
			if (biometricClient != null) biometricClient.dispose();
		}
	}
}
