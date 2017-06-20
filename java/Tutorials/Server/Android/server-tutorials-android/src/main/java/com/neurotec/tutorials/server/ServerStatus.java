package com.neurotec.tutorials.server;

import android.app.ProgressDialog;
import android.content.Context;
import android.os.AsyncTask;
import android.os.Bundle;
import android.util.Log;
import android.view.Menu;
import android.view.MenuItem;
import android.view.View;
import android.view.inputmethod.InputMethodManager;
import android.widget.Button;
import android.widget.EditText;
import android.widget.TextView;
import android.widget.Toast;

import com.neurotec.cluster.Admin;
import com.neurotec.cluster.ClusterNodeInfo;
import com.neurotec.cluster.ClusterNodeState;
import com.neurotec.cluster.ClusterTaskInfo;
import com.neurotec.samples.app.BaseActivity;
import com.neurotec.samples.view.InfoDialogFragment;

public final class ServerStatus extends BaseActivity {

	// ==============================================
	// Private static fields
	// ==============================================

	private static final String DEFAULT_PORT = "24932";
	private static final String TAG = ServerStatus.class.getSimpleName();

	// ==============================================
	// Private fields
	// ==============================================

	private EditText mTxtIpAddress;
	private EditText mTxtPortNumber;
	private TextView mTxtResult;
	private ProgressDialog mProgressDialog;

	// ==============================================
	// Private methods
	// ==============================================

	private void usage() {
		InfoDialogFragment.newInstance(getString(R.string.msg_hint_server_status)).show(getFragmentManager(), "help");
		mTxtResult.setText("");
	}

	// ==============================================
	// Protected methods
	// ==============================================

	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		setContentView(R.layout.tutorial_server_status);
		mTxtIpAddress = (EditText) findViewById(R.id.tutorials_field_1);
		mTxtIpAddress.setHint(R.string.ip_address);
		mTxtPortNumber = (EditText) findViewById(R.id.tutorials_field_2);
		mTxtPortNumber.setText(DEFAULT_PORT);
		Button btnExecute = (Button) findViewById(R.id.tutorials_button_1);
		btnExecute.setText(R.string.execute);
		mTxtResult = (TextView) findViewById(R.id.tutorials_results);

		btnExecute.setOnClickListener(new View.OnClickListener() {

			@Override
			public void onClick(View v) {
				InputMethodManager inputManager = (InputMethodManager) getSystemService(Context.INPUT_METHOD_SERVICE);
				inputManager.hideSoftInputFromWindow(getCurrentFocus().getWindowToken(), InputMethodManager.HIDE_NOT_ALWAYS);
				String[] arguments = new String[3];

				// Validate IP address.
				if (!VerifyUtil.getInstance().verifyIpAddress(mTxtIpAddress.getText().toString())) {
					Toast.makeText(getApplicationContext(), getString(R.string.msg_invalid_ip), Toast.LENGTH_SHORT).show();
					return;
				} else {
					arguments[0] = mTxtIpAddress.getText().toString();
				}

				try {
					if (mTxtPortNumber.getText() == null) {
						arguments[1] = DEFAULT_PORT;
					} else {
						arguments[1] = mTxtPortNumber.getText().toString();
					}

				} catch (Exception e) {
					Log.e(TAG, e.toString(), e);
					Toast.makeText(getApplicationContext(), getString(R.string.msg_invalid_port), Toast.LENGTH_SHORT).show();
					return;
				}

				new ServerStatusTask().execute(arguments);
			}
		});

	}

	@Override
	protected void onResume() {
		super.onResume();
		if (ServerTutorialsApp.getIpAddress() != null) {
			mTxtIpAddress.setText(ServerTutorialsApp.getIpAddress());
		}
	}

	@Override
	protected void onPause() {
		super.onPause();
		try {
			ServerTutorialsApp.setIpAddress(mTxtIpAddress.getText().toString());
		} catch (Exception e) {
			Log.e(TAG, e.toString(), e);
		}
	}

	// ==============================================
	// Public methods
	// ==============================================

	@Override
	public boolean onCreateOptionsMenu(Menu menu) {
		getMenuInflater().inflate(R.menu.main, menu);
		return true;
	}

	@Override
	public boolean onOptionsItemSelected(MenuItem item) {

		// Hide the keyboard.
		InputMethodManager inputManager = (InputMethodManager) getSystemService(Context.INPUT_METHOD_SERVICE);
		inputManager.hideSoftInputFromWindow(getCurrentFocus().getWindowToken(), InputMethodManager.HIDE_NOT_ALWAYS);
		int itemId = item.getItemId();
		if (itemId == R.id.help) {
			usage();
			return true;
		} else {
			throw new AssertionError(getString(R.string.msg_invalid_id));
		}
	}

	// ===========================================================
	// Inner classes
	// ===========================================================

	private final class ServerStatusTask extends AsyncTask<String, String, String> {

		private final StringBuilder mResult = new StringBuilder();

		private void requestServerStatus(Admin admin) throws Exception {
			publishProgress(getString(R.string.msg_request_server_info));
			com.neurotec.cluster.ServerStatus status = admin.getServerStatus();
			if (status != null) {
				mResult.append("\n");
				mResult.append(getString(R.string.msg_server_status));
				mResult.append(": ");
				mResult.append(status);
			} else {
				mResult.append("\n");
				mResult.append(getString(R.string.msg_unable_to_get_serverstatus));
			}

		}

		private void requestNodeStatus(Admin admin) throws Exception {
			ClusterNodeState[] states = admin.getClusterNodeInfo();
			mResult.append("\n");
			mResult.append(getString(R.string.msg_node_info));
			if (states != null) {

				mResult.append(String.format("%n%d %s%n", states.length, getString(R.string.msg_running_nodes)));
				for (ClusterNodeState item : states) {
					mResult.append(String.format("%n%d (%s)%n", item.getID(), item.getState()));
				}
			} else {
				mResult.append("\n");
				mResult.append(getString(R.string.msg_failed_to_get_node_info));
			}
		}

		private void requestTaskStatus(Admin admin) throws Exception {
			ClusterTaskInfo[] tasks = admin.getClusterTaskInfo();
			mResult.append("\n");
			mResult.append(getString(R.string.msg_tasks_info));
			mResult.append(": ");
			if (tasks != null) {
				mResult.append(String.format("%n%d %s:%n", tasks.length, getString(R.string.msg_tasks)));
				for (ClusterTaskInfo taskInfo : tasks) {
					mResult.append(String.format("%n%s: %d", getString(R.string.msg_id), taskInfo.getTaskId()));
					mResult.append(String.format("%n%s: %d", getString(R.string.msg_progress), taskInfo.getTaskProgress()));
					mResult.append(String.format("%n%s: %d", getString(R.string.msg_completed_nodes), taskInfo.getNodesCompleted()));
					mResult.append(String.format("%n%s: %d", getString(R.string.msg_working_nodes), taskInfo.getWorkingNodesCount()));

					for (ClusterNodeInfo info : taskInfo.getWorkingNodesInfo()) {
						mResult.append(String.format("%n\t\t%s: %d", getString(R.string.msg_node_id), info.getNodeId()));
						mResult.append(String.format("%n\t\t%s: %d", getString(R.string.msg_node_progress), info.getProgress()));
					}
				}
			} else {
				mResult.append(getString(R.string.msg_failed_to_get_tasks_info));
			}
		}

		private void requestResultStatus(Admin admin) throws Exception {
			int[] results = admin.getResultIds();
			mResult.append(String.format("%n%s:%n", getString(R.string.msg_result_info)));
			if (results != null) {

				mResult.append(String.format("%n%d %s:%n", results.length, getString(R.string.msg_completed_tasks)));
				for (int res : results) {
					mResult.append("\n");
					mResult.append(String.valueOf(res));
				}
			} else {
				mResult.append("\n");
				mResult.append(getString(R.string.msg_failed_to_get_results_info));
			}
		}

		@Override
		protected void onPreExecute() {
			mTxtResult.setText("");
			showProgress(R.string.msg_processing);
		}

		@Override
		protected String doInBackground(String... params) {
			String server = params[0];
			int port = Integer.parseInt(params[1]);
			Admin admin = null;
			try {
				admin = new Admin(server, port);

				publishProgress(String.format("%s %s: %d ...%n%n", getString(R.string.msg_ask_info), server, port));
				requestServerStatus(admin);

				publishProgress(getString(R.string.msg_ask_node_info));
				requestNodeStatus(admin);

				publishProgress(getString(R.string.msg_ask_task_info));
				requestTaskStatus(admin);

				publishProgress(getString(R.string.msg_ask_result_info));
				requestResultStatus(admin);

			} catch (Exception e) {
				Log.e(TAG, e.toString(), e);
				mResult.append("\n");
				mResult.append(e.toString());
				return mResult.toString();
			} finally {
				if (admin != null) {
					try {
						admin.finalize();
					} catch (Throwable e) {
						Log.e(TAG, e.toString(), e);
					}
				}
			}

			return mResult.toString();
		}

		@Override
		protected void onProgressUpdate(String... values) {
			if (mProgressDialog != null) {
				mProgressDialog.setMessage(values[0]);
			}
		}

		@Override
		protected void onPostExecute(String results) {
			hideProgress();
			mTxtResult.append(results);
		}
	}

}
