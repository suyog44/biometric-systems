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
import android.widget.AdapterView;
import android.widget.AdapterView.OnItemSelectedListener;
import android.widget.Button;
import android.widget.EditText;
import android.widget.Spinner;
import android.widget.TextView;
import android.widget.Toast;

import com.neurotec.cluster.Admin;
import com.neurotec.cluster.ClusterNodeState;
import com.neurotec.cluster.ClusterShortInfo;
import com.neurotec.cluster.ClusterTaskInfo;
import com.neurotec.samples.app.BaseActivity;
import com.neurotec.samples.view.InfoDialogFragment;

public final class ServerAdmin extends BaseActivity {

	// ==============================================
	// Private enum
	// ==============================================

	private enum CommandType {
		START,
		STOP,
		KILL,
		INFO,
		DBUPDATE,
		DBCHANGED,
		DBFLUSH,
		STATUS
	}

	// ==============================================
	// Private static fields
	// ==============================================

	private static final String DEFAULT_PORT = "24932";
	private static final String TAG = ServerAdmin.class.getSimpleName();

	// ==============================================
	// Private fields
	// ==============================================

	private EditText mTxtIpAddress;
	private EditText mTxtPortNumber;
	private CommandType mSelectedCommandId;
	private TextView mTxtResult;
	private ProgressDialog mProgressDialog;

	// ==============================================
	// Private methods
	// ==============================================

	private void usage() {
		InfoDialogFragment.newInstance(getString(R.string.msg_hint_server_admin)).show(getFragmentManager(), "help");
		mTxtResult.setText("");
	}

	// ==============================================
	// Protected methods
	// ==============================================

	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		setContentView(R.layout.tutorial_server_admin);
		mTxtIpAddress = (EditText) findViewById(R.id.editTextIp);
		mTxtPortNumber = (EditText) findViewById(R.id.editTextPort);
		mTxtPortNumber.setText(DEFAULT_PORT);
		final Spinner spnCommands = (Spinner) findViewById(R.id.spinnerCommands);
		final View viewInfoType = findViewById(R.id.viewInfoType);
		final Spinner spnInfoType = (Spinner) findViewById(R.id.spinnerInfoType);
		final EditText txtId = (EditText) findViewById(R.id.editTextId);

		final String[] commands = getResources().getStringArray(R.array.spinnerCommands);

		spnCommands.setOnItemSelectedListener(new OnItemSelectedListener() {

			@Override
			public void onItemSelected(AdapterView<?> parent, View view, int pos, long id) {
				mSelectedCommandId = CommandType.valueOf(commands[pos].toUpperCase());
				switch (mSelectedCommandId) {
				case START:
				case DBUPDATE:
				case DBFLUSH:
				case STATUS:
					viewInfoType.setVisibility(View.GONE);
					txtId.setVisibility(View.GONE);
					break;
				case STOP:
				case KILL:
				case DBCHANGED:
					viewInfoType.setVisibility(View.GONE);
					txtId.setVisibility(View.VISIBLE);
					break;
				case INFO:
					viewInfoType.setVisibility(View.VISIBLE);
					txtId.setVisibility(View.GONE);
					break;
				default:
					throw new AssertionError("Invalid ID selected");
				}
			}

			@Override
			public void onNothingSelected(AdapterView<?> parent) {
				// Do nothing.
			}
		});

		Button execute = (Button) findViewById(R.id.buttonExecute);
		mTxtResult = (TextView) findViewById(R.id.textViewResult);

		execute.setOnClickListener(new View.OnClickListener() {

			@Override
			public void onClick(View v) {
				InputMethodManager inputManager = (InputMethodManager) getSystemService(Context.INPUT_METHOD_SERVICE);
				inputManager.hideSoftInputFromWindow(getCurrentFocus().getWindowToken(), InputMethodManager.HIDE_NOT_ALWAYS);

				// Validate IP address.
				if (!VerifyUtil.getInstance().verifyIpAddress(mTxtIpAddress.getText().toString())) {
					Toast.makeText(getApplicationContext(), "IP address Invalid", Toast.LENGTH_SHORT).show();
					return;
				}

				String command1 = spnCommands.getSelectedItem().toString();
				String command2 = "";
				switch (mSelectedCommandId) {
				case START:
				case DBUPDATE:
				case DBFLUSH:
				case STATUS:
					break;
				case STOP:
				case KILL:
				case DBCHANGED:
					command2 = txtId.getText().toString();
					break;
				case INFO:
					command2 = spnInfoType.getSelectedItem().toString();
					break;
				default:
					throw new AssertionError("Invalid ID selected");
				}

				String[] arguments = new String[] {command1, command2};
				new ServerAdminTask().execute(arguments);
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
		InputMethodManager inputManager = (InputMethodManager) getSystemService(Context.INPUT_METHOD_SERVICE);
		inputManager.hideSoftInputFromWindow(getCurrentFocus().getWindowToken(), InputMethodManager.HIDE_NOT_ALWAYS);
		int itemId = item.getItemId();
		if (itemId == R.id.help) {
			usage();
			return true;
		} else {
			throw new AssertionError("Invalid item id");
		}
	}

	// ==============================================
	// Private InnerClasses
	// ==============================================

	private class ServerAdminTask extends AsyncTask<String, String, String> {

		private static final String SERVER = "server";

		private static final String RESULTS = "results";
		private static final String NODES = "nodes";
		private static final String TASKS_COMPLETE = "tasks_complete";
		private static final String TASKS_SHORT = "tasks_short";

		private static final String STATUS = "status";
		private static final String DBFLUSH = "dbflush";
		private static final String DBCHANGED = "dbchanged";
		private static final String DBUPDATE = "dbupdate";
		private static final String INFO = "info";
		private static final String KILL = "kill";
		private static final String STOP = "stop";
		private static final String START = "start";

		private StringBuilder mResult = new StringBuilder();

		private int getNodeId(String idString) {
			if (idString.equals(SERVER)) {
				return 0;
			}
			try {
				return Integer.parseInt(idString);
			} catch (Exception e) {
				Log.e(TAG, e.toString(), e);
				return -1;
			}
		}

		private String getTaskResultsInfo(int[] info) {
			if (info == null) {
				return "\nNo Info";
			}
			StringBuilder temp = new StringBuilder("ResultsInfo:\n");
			temp.append(String.format("%s Completed task(s):%n", info.length));
			for (int res : info) {
				temp.append("\n");
				temp.append(res);
			}
			temp.append("\n");
			return temp.toString();
		}

		private String getNodesInfo(ClusterNodeState[] info) {
			if (info == null) {
				return "No Nodes Info";
			}

			StringBuilder temp = new StringBuilder("NodesInfo:\n");
			temp.append(String.format("%s Node(s) running:%n", info.length));
			for (ClusterNodeState nodeInfo : info) {
				temp.append(String.format("%n%d (%s)%n", nodeInfo.getID(), nodeInfo.getState()));
			}
			temp.append("\n");
			return temp.toString();
		}

		private String getCompleteRunningTasksInfo(ClusterTaskInfo[] info) {
			if (info == null) {
				return "\nNo Running Tasks Info";
			}
			StringBuilder temp = new StringBuilder("Running Tasks Info:\n");
			temp.append(String.format("%n%d task(s):%n", info.length));
			for (ClusterTaskInfo taskInfo : info) {
				temp.append(String.format("%n\tid: %d", taskInfo.getTaskId()));
				temp.append(String.format("%n\tprogress: %d", taskInfo.getTaskProgress()));
				temp.append(String.format("%n\tnodes completed: %d", taskInfo.getNodesCompleted()));
				temp.append(String.format("%n\tworking nodes: %d", taskInfo.getWorkingNodesCount()));
				for (int i = 0; i < taskInfo.getWorkingNodesCount(); i++) {
					temp.append(String.format("%n\t\tnode ID: %d", taskInfo.getWorkingNodesInfo()[i].getNodeId()));
					temp.append(String.format("%n\t\tnode progress: %d", taskInfo.getWorkingNodesInfo()[i].getProgress()));
				}
			}
			temp.append("\n");
			return temp.toString();
		}

		private String getShortRunningTasksInfo(ClusterShortInfo[] info) {
			if (info == null) {
				return "\nNo Short Running Tasks Info";
			}
			StringBuilder temp = new StringBuilder("Running Tasks Info:\n");
			temp.append(String.format("%n%d node(s) running:%n", info.length));
			for (ClusterShortInfo shortInfo : info) {
				temp.append(String.format("%n\tid: %d", shortInfo.getTaskId()));
				temp.append(String.format("%n\tnodes completed: %d", shortInfo.getNodesCompleted()));
				temp.append(String.format("%n\tworking nodes: %d", shortInfo.getWorkingNodesCount()));
			}
			temp.append("\n");
			return temp.toString();
		}

		private void startServer(Admin admin) throws Exception {
			admin.clusterStart();
			mResult.append("\nServer Started");
		}

		private void stopServer(Admin admin, String[] cmd) throws Exception {
			if (cmd.length >= 2) {
				int nodeId = getNodeId(cmd[1]);
				if (nodeId != -1) {
					admin.nodeStop(nodeId);
					mResult.append(String.format("%nStop node %d command sent%n", nodeId));
				} else {
					mResult.append("\nInvalid ID");
				}
			} else {
				mResult.append("\nMissing parameter: id");
			}
		}

		private void killServer(Admin admin, String[] cmd) throws Exception {
			if (cmd.length >= 2) {
				int nodeId = getNodeId(cmd[1]);
				if (nodeId != -1) {
					if (nodeId == 0) {
						publishProgress("Kill Server");
						admin.serverKill();
						mResult.append("\nServer Stopped");
					} else {
						publishProgress("Kill Node");
						admin.nodeKill(nodeId);
						mResult.append(String.format("%nkill %d command sent%n", nodeId));
					}
				}
			} else {
				mResult.append("\nMissing parameter: id");
			}
		}

		private void getServerInfo(Admin admin, String[] cmd) throws Exception {
			if (cmd.length >= 2) {
				if (cmd[1].equals(TASKS_SHORT)) {
					publishProgress("Get Short Task Info");
					ClusterShortInfo[] info = admin.getClusterShortInfo();
					mResult.append(getShortRunningTasksInfo(info));
				} else if (cmd[1].equals(TASKS_COMPLETE)) {
					publishProgress("Get Complete Task Info");
					ClusterTaskInfo[] info = admin.getClusterTaskInfo();
					mResult.append(getCompleteRunningTasksInfo(info));
				} else if (cmd[1].equals(NODES)) {
					publishProgress("Get Node Info");
					ClusterNodeState[] info = admin.getClusterNodeInfo();
					mResult.append(getNodesInfo(info));
				} else if (cmd[1].equals(RESULTS)) {
					publishProgress("Get Result Ids");
					int[] info = admin.getResultIds();
					mResult.append(getTaskResultsInfo(info));
				} else {
					mResult.append("Unknown info type: ");
					mResult.append(cmd[1]);
				}
			} else {
				mResult.append("Missing parameter: info type");
			}
		}

		private void getServerStatus(Admin admin) throws Exception {
			com.neurotec.cluster.ServerStatus status = admin.getServerStatus();
			if (status != null) {
				mResult.append("\nServer status is: ");
				mResult.append(status);
			} else {
				mResult.append("\nError while getting server info");
			}
		}

		@Override
		protected void onPreExecute() {
			mTxtResult.setText("");
			showProgress(R.string.msg_processing);
		}

		@Override
		protected String doInBackground(String... cmd) {
			String ipAddress = mTxtIpAddress.getText().toString();
			int portNumber = 0;
			try {
				portNumber = Integer.parseInt(mTxtPortNumber.getText().toString());
			} catch (Exception e) {
				Log.e(TAG, e.toString(), e);
				mResult.append("Invalid Port number");
				return mResult.toString();
			}
			Admin admin = null;
			try {
				admin = new Admin(ipAddress, portNumber);

				if (cmd[0].equals(START)) {
					publishProgress("Starting the Server");
					startServer(admin);
				} else if (cmd[0].equals(STOP)) {
					publishProgress("Stop the Server");
					stopServer(admin, cmd);
				} else if (cmd[0].equals(KILL)) {
					publishProgress("Kill the Server");
					killServer(admin, cmd);
				} else if (cmd[0].equals(INFO)) {
					publishProgress("Getting Server Info");
					getServerInfo(admin, cmd);
				} else if (cmd[0].equals(DBUPDATE)) {
					publishProgress("Update database");
					admin.updateDatabase();
					mResult.append("\nUpdate Database completed");
				} else if (cmd[0].equals(DBCHANGED)) {
					publishProgress("Notify server of changed templates");
					if (cmd.length >= 2) {
						String[] updateIDs = new String[cmd.length - 1];
						System.arraycopy(cmd, 1, updateIDs, 0, cmd.length - 1);
						admin.updateDBRecords(updateIDs);
						mResult.append("\nDbchanged command sent");
					} else {
						mResult.append("\nMissing parameters: Ids of records to update.");
					}
				} else if (cmd[0].equals(DBFLUSH)) {
					publishProgress("Flush the DB");
					admin.flush();
					mResult.append("\nDb Flush completed");
				} else if (cmd[0].equals(STATUS)) {
					publishProgress("Get Server Status");
					getServerStatus(admin);
				} else {
					mResult.append("\nCommand not recognized.");
				}
			} catch (Exception e) {
				Log.e(TAG, e.toString(), e);
				mResult.append("\nCouldn't complete task.\n");
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
			if (mProgressDialog != null) {
				mProgressDialog.setMessage(values[0]);
			}
		}

		@Override
		protected void onPostExecute(String results) {
			hideProgress();
			mTxtResult.append("\n");
			mTxtResult.append(results);
		}
	}

}
