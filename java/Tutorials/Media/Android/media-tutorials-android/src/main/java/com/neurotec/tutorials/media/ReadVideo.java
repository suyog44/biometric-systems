package com.neurotec.tutorials.media;

import android.app.Activity;
import android.app.ProgressDialog;
import android.content.Context;
import android.content.Intent;
import android.net.Uri;
import android.os.Bundle;
import android.text.method.DigitsKeyListener;
import android.util.Log;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;
import android.widget.TextView;

import java.io.IOException;
import java.util.Arrays;
import java.util.EnumSet;

import com.neurotec.images.NImage;
import com.neurotec.media.NMediaFormat;
import com.neurotec.media.NMediaReader;
import com.neurotec.media.NMediaSource;
import com.neurotec.media.NMediaType;
import com.neurotec.samples.app.DirectoryViewer;
import com.neurotec.samples.licensing.LicensingManager;
import com.neurotec.samples.licensing.LicensingManager.LicensingStateCallback;
import com.neurotec.samples.licensing.LicensingStateResult;

public final class ReadVideo extends Activity implements LicensingStateCallback {

	// ===========================================================
	// Private static fields
	// ===========================================================

	private static final String TAG = ReadVideo.class.getSimpleName();
	private static final String LICENSE = LicensingManager.LICENSE_MEDIA;
	private static final int REQUEST_CODE_GET_VIDEO = 123;
	private static final String ANDROID_ASSET_DESCRIPTOR = "file:///android_asset/";

	// ===========================================================
	// Private fields
	// ===========================================================

	private enum Source {
		VIDEO_FILE,
		URL;
	}

	private Button mFromUrlButton;
	private Button mFromVideoFileButton;
	private TextView mResult;
	private EditText mTextVideoUrl;
	private EditText mTextFrameCount;
	private ProgressDialog mProgressDialog;
	private Uri mSelectedFile;

	// ===========================================================
	// Private Methods
	// ===========================================================

	private void getVideo(Source source) {
		if (source == Source.VIDEO_FILE) {
			Intent intent = new Intent(getApplicationContext(), DirectoryViewer.class);
			intent.putExtra(DirectoryViewer.ASSET_DIRECTORY_LOCATION, MediaTutorialsApp.TUTORIALS_ASSET_DIRECTORY);
			startActivityForResult(intent, REQUEST_CODE_GET_VIDEO);
		} else {
			readVideo(source);
		}
	}

	private void showMessage(String message) {
		mResult.append(message + "\n");
	}

	private void readVideoFrames(NMediaReader mediaReader, int frameCount) throws Exception {
		NMediaSource mediaSource = mediaReader.getSource();
		showMessage(String.format("media length: %s\n", mediaReader.getLength()));
		NMediaFormat[] mediaFormats = mediaSource.getFormats(NMediaType.VIDEO);
		if (mediaFormats == null) {
			showMessage("formats are not yet available (should be available after media reader is started");
		} else {
			showMessage(String.format("format count: %s\n", mediaFormats.length));
			for (int i = 0; i < mediaFormats.length; i++) {
				showMessage(String.format("[%s] ", i));
				MediaTutorialsApp.dumpMediaFormat(mediaFormats[i]);
			}
		}

		NMediaFormat currentMediaFormat = mediaSource.getCurrentFormat(NMediaType.VIDEO);
		if (currentMediaFormat != null) {
			showMessage("current media format:");
			MediaTutorialsApp.dumpMediaFormat(currentMediaFormat);

			if (mediaFormats != null) {
				showMessage("set the last supported format (optional) ... ");
				mediaSource.setCurrentFormat(NMediaType.VIDEO, mediaFormats[mediaFormats.length - 1]);
			}
		} else {
			showMessage("current media format is not yet available (will be availble after media reader start)");
		}

		showMessage("starting capture ... ");
		mediaReader.start();
		showMessage("capture started");

		try {
			currentMediaFormat = mediaSource.getCurrentFormat(NMediaType.VIDEO);
			if (currentMediaFormat == null) {
				throw new Exception("current media format is not set even after media reader start!");
			}
			showMessage("capturing with format: ");
			MediaTutorialsApp.dumpMediaFormat(currentMediaFormat);

			for (int i = 0; i < frameCount; i++) {
				NMediaReader.ReadResult result = mediaReader.readVideoSample();
				NImage image = result.getImage();
				if (result.getImage() == null) {
					return; // end of stream
				}
				String filename = String.format("{%d4}.jpg", i);
				image.save(filename);
				showMessage(String.format("[%s %s] %s\n", result.getTimeStamp(), result.getDuration(), filename));
			}
		} finally {
			mediaReader.stop();
		}
	}

	private void readVideo(Source source) {
		NMediaSource mediaSource = null;
		NMediaReader mediaReader = null;

		try {
			int bufferCount = 0;
			try {
				bufferCount = Integer.parseInt(mTextFrameCount.getText().toString());
			} catch (NumberFormatException exc) {
				showMessage("Invalid frame count");
			}
			if (bufferCount <= 0) {
				showMessage("no frames will be captured as frame count is not specified");
				return;
			}

			// create media source
			switch (source) {
			case URL:
				if (!mTextVideoUrl.getText().toString().isEmpty()) {
					mediaSource = NMediaSource.fromUrl(mTextVideoUrl.getText().toString());
				} else {
					showMessage("Video URL is mandatory for RTSP");
					return;
				}
				break;
			case VIDEO_FILE:
				String path;
				if (mSelectedFile.toString().contains(ANDROID_ASSET_DESCRIPTOR)) {
					path = mSelectedFile.toString().replace(ANDROID_ASSET_DESCRIPTOR, "");
				} else {
					path = mSelectedFile.getPath();
				}
				mediaSource = NMediaSource.fromFile(path);
				break;
			default:
				throw new AssertionError("Unknown source type");
			}

			if (mediaSource != null) {
				showMessage(String.format("display name: %s\n", mediaSource.getDisplayName()));
			}

			mediaReader = new NMediaReader(mediaSource, EnumSet.of(NMediaType.VIDEO), true);
			readVideoFrames(mediaReader, bufferCount);
			showMessage("done");
		} catch (Exception e) {
			Log.e(TAG, "", e);
			showMessage(e.getMessage());
		} finally {
			if (mediaSource != null) {
				mediaSource.dispose();
			}
			if (mediaReader != null) {
				mediaReader.dispose();
			}
		}
	}

	// ===========================================================
	// Protected Methods
	// ===========================================================

	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		setContentView(R.layout.tutorial_read_video);
		mFromUrlButton = (Button) findViewById(R.id.tutorials_button_1);
		mFromUrlButton.setText(getString(R.string.msg_rtsp_camera));
		mFromUrlButton.setOnClickListener(new View.OnClickListener() {
			@Override
			public void onClick(View v) {
				mResult.setText("");
				getVideo(Source.URL);
			}

		});
		mFromVideoFileButton = (Button) findViewById(R.id.tutorials_button_2);
		mFromVideoFileButton.setText(getString(R.string.msg_video_file));
		mFromVideoFileButton.setOnClickListener(new View.OnClickListener() {
			@Override
			public void onClick(View v) {
				mResult.setText("");
				getVideo(Source.VIDEO_FILE);
			}

		});
		mResult = (TextView) findViewById(R.id.tutorials_results);
		mTextFrameCount = (EditText) findViewById(R.id.tutorials_field_1);
		mTextFrameCount.setKeyListener(DigitsKeyListener.getInstance("0123456789."));
		mTextFrameCount.setHint("Frame Count");
		mTextVideoUrl = (EditText) findViewById(R.id.tutorials_field_2);
		mTextVideoUrl.setHint("Video URL");
		LicensingManager.getInstance().obtain(this, this, Arrays.asList(LICENSE));
	}

	@Override
	protected void onDestroy() {
		super.onDestroy();
		try {
			LicensingManager.getInstance().release(Arrays.asList(LICENSE));
		} catch (IOException e) {
			Log.e(TAG, getString(R.string.msg_licenses_not_obtained), e);
		}
		if ((mProgressDialog != null) && (mProgressDialog.isShowing())) {
			mProgressDialog.dismiss();
		}
	}

	@Override
	protected void onActivityResult(int requestCode, int resultCode, Intent data) {
		if (requestCode == REQUEST_CODE_GET_VIDEO) {
			if (resultCode == RESULT_OK) {
				mSelectedFile = data.getData();
				readVideo(Source.VIDEO_FILE);
			} else {
				showMessage("Failed to load file");
			}
		}
	}

	// ===========================================================
	// Public Methods
	// ===========================================================

	@Override
	public void onLicensingStateChanged(final LicensingStateResult state) {
		final Context context = this;
		runOnUiThread(new Runnable() {
			@Override
			public void run() {
				switch (state.getState()) {
				case OBTAINING:
					Log.i(TAG, getString(R.string.msg_obtaining_licenses));
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

}
