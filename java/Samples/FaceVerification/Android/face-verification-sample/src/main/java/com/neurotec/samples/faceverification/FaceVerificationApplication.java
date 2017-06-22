package com.neurotec.samples.faceverification;

import com.neurotec.face.verification.NFaceVerification;
import com.neurotec.face.verification.NFaceVerificationCapturePreviewEvent;
import com.neurotec.face.verification.NFaceVerificationCapturePreviewListener;
import com.neurotec.face.verification.NFaceVerificationStatus;
import com.neurotec.face.verification.NFaceVerificationUser;
import com.neurotec.face.verification.view.NFaceVerificationView;
import com.neurotec.lang.NCore;
import com.neurotec.samples.faceverification.gui.EnrollmentDialogFragment;
import com.neurotec.samples.faceverification.gui.SettingsActivity;
import com.neurotec.samples.faceverification.gui.SettingsFragment;
import com.neurotec.samples.faceverification.gui.UserListFragment;
import com.neurotec.samples.faceverification.gui.EnrollmentDialogFragment.EnrollmentDialogListener;
import com.neurotec.samples.faceverification.gui.UserListFragment.UserSelectionListener;
import com.neurotec.samples.faceverification.utils.BaseActivity;

import android.content.Intent;
import android.os.Bundle;
import android.view.Menu;
import android.view.MenuItem;
import android.view.View;
import android.widget.Button;

public class FaceVerificationApplication extends BaseActivity implements EnrollmentDialogListener, UserSelectionListener {

	// ===========================================================
	// Private fields
	// ===========================================================

	private static final String EXTRA_REQUEST_CODE = "request_code";
	private static final int VERIFICATION_REQUEST_CODE = 1;
	private static final int TIMEOUT = 60000;
	private boolean mAppClosing;
	private NFaceVerificationView mFaceView;

	// ===========================================================
	// Protected methods
	// ===========================================================

	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		setContentView(R.layout.activity_nlvdemo);
		// on application start you must set NCore context
		NCore.setContext(this);

		new Thread(new Runnable() {

			@Override
			public void run() {
				try {
					showProgress(R.string.msg_initialising);

					// get NFV for the first time
					final NFaceVerification nfv = NFV.getInstance();

					// load settings
					SettingsFragment.loadSettings();

					// button implementations
					Button mEnrollButton = (Button) findViewById(R.id.button_enroll);
					mEnrollButton.setOnClickListener(new View.OnClickListener() {

						@Override
						public void onClick(View v) {
							EnrollmentDialogFragment enrollDialog = new EnrollmentDialogFragment();
							enrollDialog.show(getFragmentManager(), "enrollment");
						}
					});

					Button mCancelButton = (Button) findViewById(R.id.button_cancel);
					mCancelButton.setOnClickListener(new View.OnClickListener() {

						@Override
						public void onClick(View v) {
							showProgress(R.string.msg_cancelling);
							nfv.cancel();
							hideProgress();
						}
					});

					Button mVerifyButton = (Button) findViewById(R.id.button_verify);
					mVerifyButton.setOnClickListener(new View.OnClickListener() {

						@Override
						public void onClick(View v) {
							Bundle bundle = new Bundle();
							bundle.putInt(EXTRA_REQUEST_CODE, VERIFICATION_REQUEST_CODE);
							UserListFragment userList = (UserListFragment) UserListFragment.newInstance(nfv.getUsers(), true, bundle);
							userList.show(getFragmentManager(), "verification");
						}
					});

					// set frontal camera
					String[] names = nfv.getAvailableCameraNames();
					for (String n : names) {
						if (n.contains("Front")) {
							nfv.setCamera(n);
							break;
						}
					}

					mFaceView = (NFaceVerificationView) findViewById(R.id.nFaceView);
					nfv.addCapturePreviewListener(new NFaceVerificationCapturePreviewListener() {

						@Override
						public void capturePreview(NFaceVerificationCapturePreviewEvent arg0) {
							mFaceView.setEvent(arg0);
						}
					});

					hideProgress();
				} catch (Exception ex) {
					hideProgress();
					showError(ex);
				}
			}

		}).start();
	}

	@Override
	public boolean onCreateOptionsMenu(Menu menu) {
		// Inflate the menu; this adds items to the action bar if it is present.
		getMenuInflater().inflate(R.menu.nlvdemo, menu);
		return true;
	}

	@Override
	public void onResume() {
		super.onResume();
		mAppClosing = false;
	}

	@Override
	public void onBackPressed() {
		super.onBackPressed();
		mAppClosing = true;
	}

	@Override
	public void onEnrollmentIDProvided(final String id) {
		new Thread(new Runnable() {

			@Override
			public void run() {
				try {
					// cancel in there are any other operations in progress
					NFV.getInstance().cancel();
					NFaceVerificationStatus status = NFV.getInstance().enroll(id, TIMEOUT, null);
					if (!mAppClosing) showInfo(String.format(getString(R.string.msg_operation_status), status.toString()));
				} catch (Throwable e) {
					showError(e);
				}
			}
		}).start();
	};

	@Override
	public void onUserSelected(final NFaceVerificationUser user, Bundle bundle) {
		new Thread(new Runnable() {

			@Override
			public void run() {
				try {
					// cancel in there are any other operations in progress
					NFV.getInstance().cancel();
					NFaceVerificationStatus status = NFV.getInstance().verify(user.getId(), TIMEOUT);
					if (!mAppClosing) showInfo(String.format(getString(R.string.msg_operation_status), status.toString()));
				} catch (Throwable e) {
					showError(e);
				}
			}
		}).start();
	};

	@Override
	protected void onStop() {
		mAppClosing = true;
		NFV.getInstance().cancel();
		super.onStop();
	}

	@Override
	public boolean onOptionsItemSelected(MenuItem item) {
		int id = item.getItemId();
		if (id == R.id.action_clear_db) {
			new Thread(new Runnable() {

				@Override
				public void run() {
					// cancel if there are any other operations in progress
					NFV.getInstance().cancel();
					NFV.getInstance().getUsers().clear();
				}
			}).start();
			return true;
		} else if (id == R.id.action_settings) {
			Intent intent = new Intent(this, SettingsActivity.class);
			startActivity(intent);

			return true;
		}
		return super.onOptionsItemSelected(item);
	}

}
