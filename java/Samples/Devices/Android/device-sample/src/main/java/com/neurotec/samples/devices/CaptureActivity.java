package com.neurotec.samples.devices;

import java.io.IOException;
import java.util.EnumSet;
import java.util.LinkedList;

import android.app.Activity;
import android.content.Intent;
import android.os.AsyncTask;
import android.os.AsyncTask.Status;
import android.os.Bundle;
import android.support.v4.app.NavUtils;
import android.util.Log;
import android.view.Menu;
import android.view.MenuInflater;
import android.view.MenuItem;
import android.widget.Toast;

import com.neurotec.devices.NDevice;
import com.neurotec.devices.NDeviceType;
import com.neurotec.images.NImage;
import com.neurotec.samples.devices.view.CaptureControlFragment.CaptureControlListener;
import com.neurotec.samples.devices.view.ImageFragment;
import com.neurotec.samples.devices.view.StatusFragment;

public abstract class CaptureActivity extends Activity implements CaptureControlListener {

	// ==============================================
	// Private static fields
	// ==============================================

	private static final String TAG = CaptureActivity.class.getSimpleName();

	// ==============================================
	// Public static fields
	// ==============================================

	public static final String ARG_DEVICE_ID = "device_id";

	// ==============================================
	// Private fields
	// ==============================================

	private NDevice mDevice;
	private boolean autoCaptureStart;
	private boolean gatherImages;
	private volatile boolean isCapturing;
	private boolean forceCapture;
	private NImage image, oldImage, finalImage;
	private String userStatus, finalUserStatus;
	private BackgroundTask task;
	private String imagesPath;
	private int imageCount = 0;
	private int fps;
	private long lastReportTime = 0;
	private LinkedList<Long> timestamps = new LinkedList<Long>();

	// =============================================
	// Protected fields
	// =============================================

	protected final Object statusLock = new Object();

	// ==============================================
	// Public constructor
	// ==============================================

	public CaptureActivity() {
	}

	// ==============================================
	// Private methods
	// ==============================================

	private void disconnectFromDevice() {
		if (mDevice != null) {
			try {
				DeviceManagerHelper.getDeviceManager().disconnectFromDevice(mDevice);
				finish();
			} catch (Exception e) {
				Log.e(TAG, "", e);
				showError(e.toString());
			}
		}
	}

	// ==============================================
	// Protected abstract methods
	// ==============================================

	protected abstract void onCapture();

	// ==============================================
	// Protected methods
	// ==============================================

	protected void setImage(NImage image) {
		ImageFragment fragment = (ImageFragment) getFragmentManager().findFragmentById(R.id.fragment_image);
		if (fragment != null && fragment.isInLayout()) {
			fragment.setImage(image);
		}
	}

	protected void setStatus(final String status) {
		runOnUiThread(new Runnable() {
			@Override
			public void run() {
				StatusFragment fragment = (StatusFragment) getFragmentManager().findFragmentById(R.id.fragment_status);
				if (fragment != null && fragment.isInLayout()) {
					fragment.setStatus(status);
				}
			}
		});
	}

	protected void showError(final String message) {
		runOnUiThread(new Runnable() {
			@Override
			public void run() {
				Toast.makeText(getApplicationContext(), message, Toast.LENGTH_LONG).show();
			}
		});
	}

	protected void onDeviceChanged() {
		getActionBar().setTitle(mDevice.getDisplayName());
		onStatusChanged();
	}

	protected void onStatusChanged() {
		synchronized (statusLock) {
			StringBuffer sb = new StringBuffer();
			NImage theImage = null;
			String theUserStatus = null;
			if (isCapturing()) {
				sb.append(String.format("Capturing (%s fps)", fps));
				if(this.oldImage != null && this.image != this.oldImage) {
					this.oldImage.dispose();
				}
				theImage = this.image;
				theUserStatus = userStatus;
			} else {
				sb.append("Finished");
				theImage = this.finalImage;
				theUserStatus = finalUserStatus;
			}
			setImage(theImage);
			this.oldImage = theImage;
			if (theImage != null) {
				sb.append(String.format(" (%sx%s ppi)", theImage.getWidth(), theImage.getHeight())); // TODO add resolution
			}
			if (theUserStatus != null) {
				sb.append(":" + theUserStatus);
			}
			sb.append("\n");
			setStatus(sb.toString());
			// buttonForce.setEnabled(isCapturing);
			if (isCapturing) {
				// buttonClose.setText("Cancel");
			} else {
				// buttonClose.setText("Close");
			}
		}
	}

	protected final void checkIsBusy() {
		if (task != null && task.getStatus() == Status.RUNNING) {
			throw new IllegalStateException("Capturing is running");
		}
	}

	protected boolean isValidDeviceType(EnumSet<NDeviceType> value) {
		return true;
	}

	protected void onCaptureStarted() {
		isCapturing = true;
		onStatusChanged();
	}

	protected void onCaptureFinished() {
		isCapturing = false;
		onStatusChanged();
	}

	protected final boolean onImage(NImage image, String userStatus, String imageName, boolean isFinal) {
		synchronized (statusLock) {
			if (!isFinal) {
				long elapsed = System.currentTimeMillis();
				timestamps.addLast(elapsed);
				if (elapsed - lastReportTime >= 300) {
					long s = 0;
					@SuppressWarnings("unchecked")
					LinkedList<Long> timestampsCopy = (LinkedList<Long>) timestamps.clone();
					for (Long l : timestampsCopy) {
						s = (elapsed - l) / 1000;
						if (timestamps.size() <= 1 || s <= 1) {
							break;
						}
						timestamps.removeFirst();
					}
					if (s > 0) {
						fps = (int) Math.round(timestamps.size() / s);
					} else {
						fps = 0;
					}
					lastReportTime = elapsed;
				}
			}
			if (gatherImages && image != null) {
				try {
					image.save(String.format("%s/%s%s.png", imagesPath, isFinal ? "Final" : String.format("%08d", imageCount++), imageName == null ? null : '_' + imageName));
				} catch (IOException e) {
					Log.e(TAG, "", e);
				}
			}
			if (isFinal) {
				try {
					this.finalImage = image != null ? (NImage) image.clone() : null;
				} catch (CloneNotSupportedException e) {
					Log.e(TAG, "", e);
				}
				this.finalUserStatus = userStatus;
			} else {
				try {
					this.image = (NImage) image.clone();
				} catch (CloneNotSupportedException e) {
					e.printStackTrace();
				}
				this.userStatus = userStatus;
			}
		}
		onStatusChanged();
		return forceCapture;
	}

	protected void onCancelCapture() {
		if (task != null) {
			task.cancel(true);
		}
	}

	protected NDevice getDevice() {
		return mDevice;
	}

	protected boolean isCancellationPending() {
		return task.isCancelled();
	}

	protected void setCapturing(boolean isCapturing) {
		this.isCapturing = isCapturing;
	}

	protected boolean isCapturing() {
		return isCapturing;
	}

	protected boolean hasFinal() {
		return finalImage != null;
	}

	protected boolean isAutoCaptureStart() {
		return autoCaptureStart;
	}

	protected void setAutoCaptureStart(boolean autoCaptureStart) {
		this.autoCaptureStart = autoCaptureStart;
	}

	protected final boolean isForcedCaptureEnabled() {
		return false;// TODO implement
	}

	protected final void setForcedCaptureEnabled(boolean isForcedCaptureEnabled) {
		// TODO implement
	}

	@Override
	protected void onStart() {
		super.onStart();
		try {
			onDeviceChanged();
		} catch (Exception e) {
			Log.e(TAG, "", e);
			showError(e.getMessage());
		}
	}

	// ==============================================
	// Public methods
	// ==============================================

	@Override
	public void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		getActionBar().setDisplayHomeAsUpEnabled(true);
		if (savedInstanceState == null) {
			mDevice = DeviceManagerHelper.getDeviceManager().getDevices().get(getIntent().getStringExtra(ARG_DEVICE_ID));
		}
	}

	@Override
	public boolean onCreateOptionsMenu(Menu menu) {
		MenuInflater inflater = getMenuInflater();
		inflater.inflate(R.menu.device_options_menu, menu);
		return true;
	}

	@Override
	public boolean onPrepareOptionsMenu (Menu menu) {
		menu.getItem(0).setEnabled(mDevice != null && mDevice.isDisconnectable());
		return true;
	}

	@Override
	public boolean onOptionsItemSelected(MenuItem item) {
		switch (item.getItemId()) {
		case android.R.id.home:
			NavUtils.navigateUpTo(this, new Intent(this, MainActivity.class));
			return true;
		case R.id.action_disconnect:
			disconnectFromDevice();
			return true;
		}
		return super.onOptionsItemSelected(item);
	}

	@Override
	public void onStartCapturing() {
		if (isCapturing())
			return;
		task = new BackgroundTask();
		task.execute();
	}

	@Override
	public void onStopCapturing() {
		if (task != null) {
			task.cancel(true);
			onCaptureFinished();
		}
	}

	@Override
	public void onDestroy() {
		onStopCapturing();
		super.onDestroy();
	}

	// ==============================================
	// Private inner class
	// ==============================================

	private class BackgroundTask extends AsyncTask<Boolean, Boolean, Boolean> {
		@Override
		protected Boolean doInBackground(Boolean... params) {
			try {
				onCaptureStarted();
				onCapture();
			} catch (Exception e) {
				Log.e(TAG, "", e);
				showError(e.getMessage());
			}
			return null;
		}

		@Override
		protected void onPostExecute(Boolean result) {
			super.onPostExecute(result);
			if (!isAutoCaptureStart()) {
				onCaptureFinished();
			}
		}

		@Override
		protected void onCancelled(Boolean result) {
			super.onCancelled(result);
			onCaptureFinished();
		}
	}
}
