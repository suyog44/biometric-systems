package com.neurotec.tutorials.server;

import android.app.ProgressDialog;
import android.content.Context;
import android.content.Intent;
import android.net.Uri;
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

import java.io.FileInputStream;
import java.io.IOException;

import com.neurotec.cluster.Client;
import com.neurotec.cluster.ClusterResult;
import com.neurotec.cluster.Task;
import com.neurotec.cluster.TaskMode;
import com.neurotec.cluster.TaskProgress;
import com.neurotec.cluster.TaskResult;
import com.neurotec.samples.app.BaseActivity;
import com.neurotec.samples.app.DirectoryViewer;
import com.neurotec.samples.util.IOUtils;
import com.neurotec.samples.view.InfoDialogFragment;

public final class SendTask extends BaseActivity {

	// ==============================================
	// Private static fields
	// ==============================================

	private static final int DEFAULT_PORT = 25452;
	private static final String DEFAULT_QUERY = "SELECT node_id, dbid FROM node_tbl";
	private static final String TAG = SendTask.class.getSimpleName();

	private static final int REQUEST_CODE_GET_FILE = 1;

	// ==============================================
	// Private fields
	// ==============================================

	private EditText mTxtIpAddress;
	private EditText mTxtPortNumber;
	private EditText mTxtQuery;
	private TextView mTxtResult;
	private ProgressDialog mProgressDialog;

	private Uri mSelectedFile;

	// ==============================================
	// Private methods
	// ==============================================

	private void usage() {
		InfoDialogFragment.newInstance(getString(R.string.msg_hint_send_task)).show(getFragmentManager(), "help");
		mTxtResult.setText("");
	}

	private void executeSendTask() {
		String serverIp = mTxtIpAddress.getText().toString();

		// IP address validation.
		if (!VerifyUtil.getInstance().verifyIpAddress(serverIp)) {
			Toast.makeText(getApplicationContext(), getString(R.string.msg_invalid_ip), Toast.LENGTH_SHORT).show();
			return;
		}

		int serverPort;
		if (mTxtPortNumber.getText() == null) {
			serverPort = DEFAULT_PORT;
		} else {
			serverPort = Integer.parseInt(mTxtPortNumber.getText().toString());
		}

		String query;
		if ((mTxtQuery.getText() != null) && (mTxtQuery.getText().toString().length() > 0)) {
			query = mTxtQuery.getText().toString();
		} else {
			query = DEFAULT_QUERY;
		}

		if (mSelectedFile == null) {
			Toast.makeText(getApplicationContext(), getString(R.string.msg_no_files_selected), Toast.LENGTH_SHORT).show();
			return;
		}

		new SendTaskTemp().execute(new ParseArgsResult(serverIp, serverPort, mSelectedFile, query));
	}

	// ==============================================
	// Protected methods
	// ==============================================

	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		setContentView(R.layout.tutorial_send_task);
		mTxtIpAddress = (EditText) findViewById(R.id.tutorials_field_1);
		mTxtIpAddress.setHint(getString(R.string.ip_address));
		mTxtPortNumber = (EditText) findViewById(R.id.tutorials_field_2);
		mTxtPortNumber.setText(String.valueOf(DEFAULT_PORT));
		mTxtQuery = (EditText) findViewById(R.id.tutorials_field_3);
		mTxtQuery.setText(DEFAULT_QUERY);
		findViewById(R.id.tutorials_field_4).setVisibility(View.GONE);

		Button btnExecute = (Button) findViewById(R.id.tutorials_button_1);
		btnExecute.setText(getString(R.string.execute));
		mTxtResult = (TextView) findViewById(R.id.tutorials_results);

		btnExecute.setOnClickListener(new View.OnClickListener() {

			@Override
			public void onClick(View v) {

				// Hide the keyboard.
				InputMethodManager inputManager = (InputMethodManager) getSystemService(Context.INPUT_METHOD_SERVICE);
				inputManager.hideSoftInputFromWindow(getCurrentFocus().getWindowToken(), InputMethodManager.HIDE_NOT_ALWAYS);

				Intent intent = new Intent(getApplicationContext(), DirectoryViewer.class);
				intent.putExtra(DirectoryViewer.ASSET_DIRECTORY_LOCATION, ServerTutorialsApp.TUTORIALS_ASSETS_DIR);
				startActivityForResult(intent, REQUEST_CODE_GET_FILE);
			}

		});
	}

	@Override
	protected void onActivityResult(int requestCode, int resultCode, Intent data) {
		if (requestCode == REQUEST_CODE_GET_FILE) {
			if (resultCode == RESULT_OK) {
				try {
					mSelectedFile = data.getData();
					executeSendTask();
				} catch (Exception e) {
					Log.e(TAG, e.toString(), e);
				}
			}
		}
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

		// Hide the keyboard
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

	private class SendTaskTemp extends AsyncTask<ParseArgsResult, String, String> {

		// Read file to get the template
		private byte[] readFile(Uri fileName) {
			byte[] template;
			FileInputStream fis = null;
			try {
				template = IOUtils.toByteArray(IOUtils.toByteBuffer(SendTask.this, fileName));
				publishProgress(getString(R.string.msg_file_read_completed));
			} catch (Exception e) {
				String result = String.format("%s %s.%n", getString(R.string.msg_templates_not_loaded), fileName);
				publishProgress(result);
				Log.e(TAG, e.toString(), e);
				return null;
			} finally {
				if (fis != null) {
					try {
						fis.close();
					} catch (IOException e) {
						Log.e(TAG, e.toString(), e);

					}
				}
			}
			return template;
		}

		// Create the task using the query.
		private Task createTask(byte[] template, String query) {
			Task task = new Task();
			task.setMode(TaskMode.NORMAL);
			task.setTemplate(template);
			task.setQuery(query);
			task.setResultLimit(100);
			publishProgress(getString(R.string.msg_task_created));
			publishProgress(task.toString());
			publishProgress(task.getQuery());
			return task;
		}

		// Create client and send task.
		private int sendTask(Client client, Task task) throws Exception {
			publishProgress(getString(R.string.msg_start_sending_task));
			int taskId;
			try {
				taskId = client.sendTask(task);
			} catch (Exception e) {
				Log.e(TAG, e.toString(), e);
				publishProgress(getString(R.string.msg_unable_to_send_task));
				throw e;
			}
			publishProgress(getString(R.string.msg_send_task_finished));
			if (taskId == -1) {
				publishProgress(getString(R.string.msg_get_taskid_failed) + "\n" + getString(R.string.msg_taskid_not_added));
				throw new IllegalArgumentException(getString(R.string.msg_get_taskid_failed) + "\n" + getString(R.string.msg_taskid_not_added));
			}

			publishProgress(String.format("%s  %d%n", getString(R.string.msg_get_task_started), taskId));
			return taskId;
		}

		@Override
		protected void onPreExecute() {
			mTxtResult.setText("");
			showProgress(R.string.msg_processing);
		}

		@Override
		protected String doInBackground(ParseArgsResult... results) {
			StringBuilder result = new StringBuilder();
			Client client = null;

			// Read file to get the template
			byte[] template = readFile(results[0].getTemplateFile());
			if (template == null) {
				return getString(R.string.msg_file_read_failed);
			} else {
				result.append(results[0].getTemplateFile());
				result.append("\n\n");
			}

			try {
				// Create task.
				Task task = createTask(template, results[0].getQuery());
				int taskId;

				// Send task.
				client = new Client(results[0].getServerIp(), results[0].getServerPort());
				taskId = sendTask(client, task);

				// Wait for task to complete.
				publishProgress(getString(R.string.msg_waiting_for_result));
				int count;
				TaskProgress progress = null;
				do {
					progress = client.getProgress(taskId);
					publishProgress(getString(R.string.msg_waiting_for_result) + progress + "%");
					if (progress.getProgress() != 100) {
						Thread.sleep(100);
					}

					if (progress.getProgress() < 0) {
						publishProgress(getString(R.string.msg_task_aborted));
						return getString(R.string.msg_task_not_completed);
					}

				} while (progress.getProgress() != 100);

				count = progress.getResultCount();

				if (count > 0) {
					TaskResult res = client.getTaskResult(taskId, 1, count);
					for (ClusterResult clusterRes : res.getRes()) {
						result.append(String.format("%n%s: %s, %s: %d", getString(R.string.msg_matched_with_id), clusterRes.getId(),
								getString(R.string.msg_score), clusterRes.getSimilarity()));

						publishProgress(getString(R.string.msg_match_found));
					}
				} else {
					result.append(getString(R.string.msg_no_matches_found));
					publishProgress(getString(R.string.msg_no_matches_found));
				}

				client.deleteTask(taskId);
			} catch (Exception e) {
				Log.e(TAG, e.toString(), e);
				publishProgress(getString(R.string.msg_exception));
				return e.toString();
			} finally {
				if (client != null) {
					try {
						client.finalize();
					} catch (Throwable e) {
						Log.e(TAG, e.toString(), e);
					}
				}
			}

			return result.toString();
		}

		@Override
		protected void onProgressUpdate(String... values) {
			if (mProgressDialog != null) {
				mProgressDialog.setMessage(values[0]);
			}
		}

		@Override
		protected void onPostExecute(String result) {
			hideProgress();
			mTxtResult.append("\n");
			mTxtResult.append(result);
		}

	}

	private static final class ParseArgsResult {

		private final String mServerIp;
		private final int mServerPort;
		private final Uri mTemplateFile;
		private final String mQuery;

		public ParseArgsResult(String serverIp, int serverPort, Uri templateFile, String query) {
			mServerIp = serverIp;
			mServerPort = serverPort;
			mTemplateFile = templateFile;
			mQuery = query;
		}

		public String getServerIp() {
			return mServerIp;
		}

		public int getServerPort() {
			return mServerPort;
		}

		public Uri getTemplateFile() {
			return mTemplateFile;
		}

		public String getQuery() {
			return mQuery;
		}
	}

}
