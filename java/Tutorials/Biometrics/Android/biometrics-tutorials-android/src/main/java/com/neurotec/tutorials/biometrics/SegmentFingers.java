package com.neurotec.tutorials.biometrics;

import android.app.AlertDialog;
import android.app.Dialog;
import android.app.ProgressDialog;
import android.content.Context;
import android.content.DialogInterface;
import android.content.Intent;
import android.net.Uri;
import android.os.Bundle;
import android.util.Log;
import android.view.View;
import android.widget.Button;
import android.widget.TextView;

import java.io.File;
import java.io.IOException;
import java.util.ArrayList;
import java.util.Arrays;
import java.util.EnumSet;
import java.util.List;

import com.neurotec.biometrics.NBiometricOperation;
import com.neurotec.biometrics.NBiometricStatus;
import com.neurotec.biometrics.NBiometricTask;
import com.neurotec.biometrics.NFPosition;
import com.neurotec.biometrics.NFinger;
import com.neurotec.biometrics.NSubject;
import com.neurotec.biometrics.client.NBiometricClient;
import com.neurotec.samples.app.BaseActivity;
import com.neurotec.samples.app.DirectoryViewer;
import com.neurotec.samples.licensing.LicensingManager;
import com.neurotec.samples.licensing.LicensingManager.LicensingStateCallback;
import com.neurotec.samples.licensing.LicensingStateResult;
import com.neurotec.samples.util.NImageUtils;
import com.neurotec.samples.view.BaseDialogFragment;

public final class SegmentFingers extends BaseActivity implements LicensingStateCallback {

	private static final String TAG = SegmentFingers.class.getSimpleName();
	private static final int REQUEST_CODE_GET_IMAGE = 1;

	private static final String[] LICENSES = {LicensingManager.LICENSE_FINGER_SEGMENTATION, LicensingManager.LICENSE_FINGER_EXTRACTION};

	private static final NFPosition[] sPositions = {NFPosition.PLAIN_RIGHT_FOUR_FINGERS, NFPosition.PLAIN_LEFT_FOUR_FINGERS, NFPosition.PLAIN_THUMBS};
	private static final NFPosition[] sMissing = {NFPosition.RIGHT_THUMB, NFPosition.RIGHT_INDEX_FINGER, NFPosition.RIGHT_MIDDLE_FINGER, NFPosition.RIGHT_RING_FINGER, NFPosition.RIGHT_LITTLE_FINGER, NFPosition.LEFT_THUMB, NFPosition.LEFT_INDEX_FINGER, NFPosition.LEFT_MIDDLE_FINGER, NFPosition.LEFT_RING_FINGER, NFPosition.LEFT_LITTLE_FINGER};

	private interface PositionSelectionListener {
		void onPositionSelected(NFPosition position);
	}

	private interface MissingPositionsSelectionListener {
		void onMissingPositionsSelected(List<NFPosition> positions);
	}

	private Button mButton1;
	private Button mButton2;
	private Button mButton3;
	private TextView mResult;
	private ProgressDialog mProgressDialog;
	private NFPosition mPosition;
	private List<NFPosition> mMissingPositions;

	private PositionSelectionListener mPositionListener = new PositionSelectionListener() {

		@Override
		public void onPositionSelected(NFPosition position) {
			mPosition = position;
			showMessage(getString(R.string.format_nfposition_selected, mPosition));
		}
	};

	private MissingPositionsSelectionListener mMissingPositionListener = new MissingPositionsSelectionListener() {

		@Override
		public void onMissingPositionsSelected(List<NFPosition> positions) {
			mMissingPositions = positions;
			showMessage(getString(R.string.format_missing_positions_selected, Arrays.toString(positions.toArray(new NFPosition[positions.size()]))));
		}
	};

	@Override
	public void onLicensingStateChanged(final LicensingStateResult state) {
		final Context context = this;
		runOnUiThread(new Runnable() {
			@Override
			public void run() {
				switch (state.getState()) {
				case OBTAINING:
					Log.i(TAG, getString(R.string.format_obtaining_licenses, Arrays.toString(LICENSES)));
					mProgressDialog = ProgressDialog.show(context, "", getString(R.string.msg_obtaining_licenses));
					break;
				case OBTAINED:
					mProgressDialog.dismiss();
					showMessage(getString(R.string.msg_licenses_obtained));
					break;
				case NOT_OBTAINED:
					mProgressDialog.dismiss();
					showMessage(getString(R.string.msg_licenses_not_obtained));
					if (state.getException() != null) {
						showMessage(state.getException().getMessage());
					}
					break;
				default:
					throw new AssertionError("Unknown state: " + state);
				}
			}
		});
	}

	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		setContentView(R.layout.tutorial_segment_finger);
		mButton1 = (Button) findViewById(R.id.tutorials_button_1);
		mButton1.setText(R.string.msg_select_position);
		mButton1.setOnClickListener(new View.OnClickListener() {
			@Override
			public void onClick(View v) {
				SelectPositionFragment.newInstance(mPositionListener).show(getFragmentManager(), "position");;
			}
		});
		mButton2 = (Button) findViewById(R.id.tutorials_button_2);
		mButton2.setText(R.string.msg_select_missing_positions);
		mButton2.setOnClickListener(new View.OnClickListener() {
			@Override
			public void onClick(View v) {
				SelectMissingPositionFragment.newInstance(mMissingPositionListener).show(getFragmentManager(), "missing_positions");
			}
		});
		mButton3 = (Button) findViewById(R.id.tutorials_button_3);
		mButton3.setText(R.string.msg_select_image);
		mButton3.setOnClickListener(new View.OnClickListener() {
			@Override
			public void onClick(View v) {
				if (validateInput()) {
					getImage();
				}
			}
		});
		mResult = (TextView) findViewById(R.id.tutorials_results);

		mMissingPositions = new ArrayList<NFPosition>();

		LicensingManager.getInstance().obtain(this, this, Arrays.asList(LICENSES));
	}

	@Override
	protected void onDestroy() {
		super.onDestroy();
		try {
			LicensingManager.getInstance().release(Arrays.asList(LICENSES));
		} catch (IOException e) {
			Log.e(TAG, getString(R.string.msg_licenses_not_obtained), e);
		}
		if ((mProgressDialog != null) && (mProgressDialog.isShowing())) {
			mProgressDialog.dismiss();
		}
	}

	@Override
	protected void onActivityResult(int requestCode, int resultCode, Intent data) {
		if (requestCode == REQUEST_CODE_GET_IMAGE) {
			if (resultCode == RESULT_OK) {
				try {
					segment(data.getData());
				} catch (Exception e) {
					showMessage(e.toString());
					Log.e(TAG, "Exception", e);
				}
			}
		}
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

	private boolean validateInput() {
		if (mPosition == null) {
			showMessage(getString(R.string.msg_select_nfposition_first));
			return false;
		} else {
			return true;
		}
	}

	private void segment(Uri imageUri) throws IOException {
		NBiometricClient biometricClient = null;
		NSubject subject = null;
		NFinger finger = null;
		NBiometricTask task = null;

		try {
			biometricClient = new NBiometricClient();
			subject = new NSubject();
			finger = new NFinger();

			finger.setImage(NImageUtils.fromUri(this, imageUri));
			finger.setPosition(mPosition);

			subject.getFingers().add(finger);

			for (NFPosition position : mMissingPositions) {
				subject.getMissingFingers().add(position);
			}

			task = biometricClient.createTask(EnumSet.of(NBiometricOperation.SEGMENT, NBiometricOperation.CREATE_TEMPLATE), subject);

			biometricClient.performTask(task);
			if (task.getError() != null) {
				showError(task.getError());
				return;
			}

			if (task.getStatus() == NBiometricStatus.OK) {
				if(finger.getWrongHandWarning()) {
					showMessage("Warning: possibly wrong hand.");
				}
				showMessage(String.format("Found %d segments", subject.getFingers().size() - 1));
				for (int i = 1; i < subject.getFingers().size(); i++) {
					NFinger segmentedFinger = subject.getFingers().get(i);
					if (segmentedFinger.getStatus() == NBiometricStatus.OK) {
						File outputFile = new File(BiometricsTutorialsApp.TUTORIALS_OUTPUT_DATA_DIR, segmentedFinger.getPosition() + ".png");
						segmentedFinger.getImage().save(outputFile.getAbsolutePath());
						showMessage(getString(R.string.format_finger_segment_saved_to, outputFile.getAbsolutePath()));

					} else {
						showMessage(getString(R.string.format_segment_position, segmentedFinger.getPosition(), segmentedFinger.getStatus()));
					}
				}
			} else {
				showMessage(String.format("Segmentation failed. Status: %s\n", task.getStatus()));
			}
		} finally {
			if (biometricClient != null) biometricClient.dispose();
			if (subject != null) subject.dispose();
			if (finger != null) finger.dispose();
			if (task != null) task.dispose();
		}
	}

	private static String[] asStringArray(NFPosition[] positions) {
		String[] arr = new String[positions.length];
		for (int i = 0; i < positions.length; i++) {
			arr[i] = positions[i].name();
		}
		return arr;
	}

	// ===========================================================
	// Dialog fragments
	// ===========================================================

	private static class SelectPositionFragment extends BaseDialogFragment {

		private static SelectPositionFragment newInstance(PositionSelectionListener listener) {
			SelectPositionFragment frag = new SelectPositionFragment();
			frag.mPositionListener = listener;
			return frag;
		}

		private PositionSelectionListener mPositionListener;

		@Override
		public Dialog onCreateDialog(Bundle savedInstanceState) {
			AlertDialog.Builder builder = new AlertDialog.Builder(getActivity());
			builder.setTitle(R.string.msg_nfposition_in_image_to_be_segmented);
			builder.setItems(asStringArray(sPositions), new DialogInterface.OnClickListener() {
				@Override
				public void onClick(DialogInterface dialog, int which) {
					mPositionListener.onPositionSelected(sPositions[which]);
					dialog.dismiss();
				}
			});
			return builder.create();
		}

	}

	private static class SelectMissingPositionFragment extends BaseDialogFragment {

		private static SelectMissingPositionFragment newInstance(MissingPositionsSelectionListener listener) {
			SelectMissingPositionFragment frag = new SelectMissingPositionFragment();
			frag.mMissingPositionListener = listener;
			return frag;
		}

		private MissingPositionsSelectionListener mMissingPositionListener;

		@Override
		public Dialog onCreateDialog(Bundle savedInstanceState) {
			final List<NFPosition> missingPositions = new ArrayList<NFPosition>();
			AlertDialog.Builder builder = new AlertDialog.Builder(getActivity());
			builder.setTitle(R.string.msg_missing_positions_in_image);
			builder.setMultiChoiceItems(asStringArray(sMissing), null, new DialogInterface.OnMultiChoiceClickListener() {
				@Override
				public void onClick(DialogInterface dialog, int which, boolean isChecked) {
					NFPosition selected = sMissing[which];
					if (isChecked) {
						missingPositions.add(selected);
					} else {
						missingPositions.remove(selected);
					}
				}
			});
			builder.setPositiveButton(R.string.msg_ok, new DialogInterface.OnClickListener() {
				@Override
				public void onClick(DialogInterface dialog, int id) {
					mMissingPositionListener.onMissingPositionsSelected(missingPositions);
					dialog.dismiss();
				}
			});
			builder.setNegativeButton(R.string.msg_cancel, new DialogInterface.OnClickListener() {
				@Override
				public void onClick(DialogInterface dialog, int id) {
					dialog.dismiss();
				}
			});

			return builder.create();
		}

	}


}
