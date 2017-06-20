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
import android.widget.Spinner;
import android.widget.TextView;
import android.widget.Toast;

import java.io.FileInputStream;
import java.io.IOException;

import com.neurotec.cluster.Admin;
import com.neurotec.cluster.InsertDeleteResult;
import com.neurotec.cluster.InsertDeleteStatus;
import com.neurotec.cluster.InsertDeleteTemplateResult;
import com.neurotec.samples.app.BaseActivity;
import com.neurotec.samples.app.DirectoryViewer;
import com.neurotec.samples.util.IOUtils;
import com.neurotec.samples.view.InfoDialogFragment;

public final class ServerDatabase extends BaseActivity {

	// ==============================================
	// Private enum
	// ==============================================

	private enum TaskType {
		INSERT,
		DELETE;
	}

	// ==============================================
	// Private static fields
	// ==============================================

	private static final int REQUEST_CODE_GET_FILE = 1;

	private static final int DEFAULT_PORT = 24932;
	private static final String TAG = ServerDatabase.class.getSimpleName();

	// ==============================================
	// Private fields
	// ==============================================

	private EditText mTxtIpAddress;
	private EditText mTxtPortNumber;
	private Spinner mSpnTaskType;
	private EditText mTxtId;

	private TextView mTxtResult;
	private ProgressDialog mProgressDialog;

	private Uri mSelectedFile;

	// ==============================================
	// Private methods
	// ==============================================

	private void usage() {
		InfoDialogFragment.newInstance(getString(R.string.msg_hint_server_database)).show(getFragmentManager(), "help");
		mTxtResult.setText("");
	}

	private void executeServerDatabase() {
		int serverPort = Integer.parseInt(mTxtPortNumber.getText().toString());
		TaskType taskType = TaskType.valueOf(mSpnTaskType.getSelectedItem().toString());
		new ServerDatabaseTask().execute(new ParseArgsResult(mTxtIpAddress.getText().toString(), serverPort, mTxtId.getText().toString(), taskType, mSelectedFile));
	}

	// ==============================================
	// Protected methods
	// ==============================================

	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		setContentView(R.layout.tutorial_server_database);
		mTxtIpAddress = (EditText) findViewById(R.id.editTextIp);
		mTxtPortNumber = (EditText) findViewById(R.id.editTextPort);
		mTxtPortNumber.setText(String.valueOf(DEFAULT_PORT));
		mSpnTaskType = (Spinner) findViewById(R.id.spinnerTextTaskType);
		mTxtId = (EditText) findViewById(R.id.editTextId);

		Button btnExecute = (Button) findViewById(R.id.buttonExecute);
		mTxtResult = (TextView) findViewById(R.id.textViewResult);

		btnExecute.setOnClickListener(new View.OnClickListener() {

			@Override
			public void onClick(View v) {
				InputMethodManager inputManager = (InputMethodManager) getSystemService(Context.INPUT_METHOD_SERVICE);
				inputManager.hideSoftInputFromWindow(getCurrentFocus().getWindowToken(), InputMethodManager.HIDE_NOT_ALWAYS);

				// Validate ip address.
				if (!VerifyUtil.getInstance().verifyIpAddress(mTxtIpAddress.getText().toString())) {
					Toast.makeText(getApplicationContext(), getString(R.string.msg_invalid_ip), Toast.LENGTH_SHORT).show();
					return;
				}

				TaskType task = TaskType.valueOf(mSpnTaskType.getSelectedItem().toString());

				switch (task) {
				case INSERT:
					Intent intent = new Intent(getApplicationContext(), DirectoryViewer.class);
					intent.putExtra(DirectoryViewer.ASSET_DIRECTORY_LOCATION, ServerTutorialsApp.TUTORIALS_ASSETS_DIR);
					startActivityForResult(intent, REQUEST_CODE_GET_FILE);
					break;
				case DELETE:
					executeServerDatabase();
					break;
				default:
					throw new AssertionError("Invalid Task");
				}

			}
		});
	}

	@Override
	protected void onActivityResult(int requestCode, int resultCode, Intent data) {
		if (requestCode == REQUEST_CODE_GET_FILE) {
			if (resultCode == RESULT_OK) {
				try {
					mSelectedFile = data.getData();
					executeServerDatabase();
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

	// ==============================================
	// Private Inner classes
	// ==============================================

	private class ServerDatabaseTask extends AsyncTask<ParseArgsResult, String, String> {
		private final StringBuilder mResult = new StringBuilder();

		// Read file to get the template.
		private byte[] readFile(Uri fileName) {
			byte[] template;
			FileInputStream fis = null;
			try {
				template = IOUtils.toByteArray(IOUtils.toByteBuffer(ServerDatabase.this, fileName));
				if (template == null) {
					publishProgress(getString(R.string.msg_file_read_failed));
				} else {
					publishProgress(getString(R.string.msg_file_read_completed));
				}
			} catch (NullPointerException e) {
				Log.e(TAG, e.toString(), e);
				publishProgress(getString(R.string.msg_invalid_selection));
				return null;
			} catch (Exception e) {
				Log.e(TAG, e.toString(), e);
				publishProgress(String.format("%s %s.%n", getString(R.string.msg_templates_not_loaded), fileName));
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

		// Execute the task.
		private int executeTask(Admin admin, TaskType task, String id, Uri template) throws Exception {
			int taskId;

			switch (task) {
			case INSERT:
				publishProgress(getString(R.string.msg_insert_task_started));
				byte[][] templates = new byte[1][];
				templates[0] = readFile(template);
				taskId = admin.insertDBRecords(new String[] {id}, templates);
				break;
			case DELETE:
				publishProgress(getString(R.string.msg_delete_task_started));
				taskId = admin.deleteDBRecords(new String[] {id});
				break;
			default:
				throw new AssertionError(getString(R.string.msg_invalid_selection));
			}
			mResult.append("\n");
			mResult.append(getString(R.string.msg_task_id));
			mResult.append(taskId);
			return taskId;
		}

		// Wait for the result and process.
		private void waitForResult(Admin admin, TaskType task, int taskId) throws Exception {
			InsertDeleteStatus status;
			InsertDeleteResult taskResult;

			do {
				publishProgress(getString(R.string.msg_waiting_for_result));
				switch (task) {
				case INSERT:
					taskResult = admin.getInsertResult(taskId);
					if (taskResult != null) {
						status = taskResult.getInsertDeleteStatus();
					} else {
						throw new Exception(getString(R.string.msg_failed_to_get_inserted_result));
					}
					break;
				case DELETE:
					taskResult = admin.getDeleteResult(taskId);
					if (taskResult != null) {
						status = taskResult.getInsertDeleteStatus();
					} else {
						throw new Exception(getString(R.string.msg_failed_to_get_deleted_result));
					}
					break;
				default:
					throw new Exception(getString(R.string.msg_invalid_task_type));
				}

				if (status == InsertDeleteStatus.waiting) {
					publishProgress(String.format("%s \"%s\"  ...%n", getString(R.string.msg_waiting_for_result), task));
					Thread.sleep(100);
				}
			} while (status == InsertDeleteStatus.waiting);

			// Process the Results
			switch (status) {
			case succeeded:
				mResult.append(String.format("%n%s%n%s", getString(R.string.msg_task_succeeded), task.toString()));
				break;
			case failed:
				mResult.append(String.format("%n%s%n%s", getString(R.string.msg_task_failed), task.toString()));
				break;
			case notReady:
				mResult.append(String.format("%s %s%n%s", getString(R.string.msg_task_failed), getString(R.string.msg_server_not_ready), task));
				break;
			case partiallySucceeded:
				mResult.append(String.format("%n%s%n%s", getString(R.string.msg_task_partially_succeeded), task));
				break;

			case waiting:
				// Waiting should also throw AssertionError - do-while loop handles the wait case.
			default:
				throw new AssertionError("Invalid status: " + status);

			}
			int i = 0;
			for (InsertDeleteTemplateResult r : taskResult.getInsertDeleteTemplateResult()) {
				mResult.append(String.format("%n%s %d %s%n%s", getString(R.string.template), i++, getString(R.string.status), r.getStatus().toString()));
			}

		}

		@Override
		protected void onPreExecute() {
			showProgress(R.string.msg_processing);
			mTxtResult.setText("");
		}

		@Override
		protected String doInBackground(ParseArgsResult... results) {
			String ip = results[0].getServerIp();
			int port = results[0].getServerPort();
			String id = results[0].getId();
			TaskType task = results[0].getTaskType();
			Uri template = results[0].getFileName();

			Admin admin = null;
			try {
				admin = new Admin(ip, port);
				int taskId;

				// Execute the task.
				publishProgress(getString(R.string.msg_start_executing_task));
				taskId = executeTask(admin, task, id, template);

				// Wait for result.
				publishProgress(getString(R.string.msg_wait_for_result));
				waitForResult(admin, task, taskId);

			} catch (Exception e) {
				Log.e(TAG, e.toString(), e);
				mResult.append("\n");
				mResult.append(getString(R.string.msg_exception));
				mResult.append(e.toString());
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
			mProgressDialog.setMessage(values[0]);
		}

		@Override
		protected void onPostExecute(String results) {
			hideProgress();
			mTxtResult.append("\n");
			mTxtResult.append(results);
		}
	}

	private static final class ParseArgsResult {

		private final String mServerIp;
		private final int mServerPort;
		private final String mId;
		private final TaskType mTaskType;
		private final Uri mFileName;

		public ParseArgsResult(String serverIp, int serverPort, String id, TaskType taskType, Uri fileName) {
			mServerIp = serverIp;
			mServerPort = serverPort;
			mId = id;
			mTaskType = taskType;
			mFileName = fileName;
		}

		public String getServerIp() {
			return mServerIp;
		}

		public int getServerPort() {
			return mServerPort;
		}

		public String getId() {
			return mId;
		}

		public TaskType getTaskType() {
			return mTaskType;
		}

		public Uri getFileName() {
			return mFileName;
		}
	}
}
