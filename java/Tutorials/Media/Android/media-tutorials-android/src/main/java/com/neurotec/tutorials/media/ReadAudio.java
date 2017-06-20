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

import com.neurotec.media.NMediaFormat;
import com.neurotec.media.NMediaReader;
import com.neurotec.media.NMediaSource;
import com.neurotec.media.NMediaType;
import com.neurotec.samples.app.DirectoryViewer;
import com.neurotec.samples.licensing.LicensingManager;
import com.neurotec.samples.licensing.LicensingManager.LicensingStateCallback;
import com.neurotec.samples.licensing.LicensingStateResult;

public final class ReadAudio extends Activity implements LicensingStateCallback {

	// ===========================================================
	// Private static fields
	// ===========================================================

	private static final String TAG = ReadAudio.class.getSimpleName();
	private static final String LICENSE = LicensingManager.LICENSE_MEDIA;
	private static final int REQUEST_CODE_GET_AUDIO = 124;
	private static final String ANDROID_ASSET_DESCRIPTOR = "file:///android_asset/";

	// ===========================================================
	// Private fields
	// ===========================================================

	private enum Source {
		AUDIO_FILE,
		URL;
	}

	private Button mFromAudioFileButton;
	private Button mFromUrlButton;
	private TextView mResult;
	private EditText mTextAudioUrl;
	private EditText mTextBufferLength;
	private ProgressDialog mProgressDialog;
	private Uri mSelectedFile;

	// ===========================================================
	// Private Methods
	// ===========================================================

	private void getAudio(Source source) {
		if (source == Source.AUDIO_FILE) {
			Intent intent = new Intent(getApplicationContext(), DirectoryViewer.class);
			intent.putExtra(DirectoryViewer.ASSET_DIRECTORY_LOCATION, MediaTutorialsApp.TUTORIALS_ASSET_DIRECTORY);
			startActivityForResult(intent, REQUEST_CODE_GET_AUDIO);
		} else {
			readAudio(source);
		}
	}

	private void showMessage(String message) {
		mResult.append(message + "\n");
	}

	private void readSoundBuffers(NMediaReader mediaReader, int bufferCount) throws Exception {
		NMediaSource mediaSource = mediaReader.getSource();

		showMessage(String.format("media length: %s\n", mediaReader.getLength()));

		NMediaFormat[] mediaFormats = mediaSource.getFormats(NMediaType.AUDIO);
		if (mediaFormats == null) {
			showMessage("formats are not yet available (should be available after media reader is started");
		} else {
			showMessage(String.format("format count: %s\n", mediaFormats.length));
			for (int i = 0; i < mediaFormats.length; i++) {
				System.out.format("[%s] ", i);
				showMessage(MediaTutorialsApp.dumpMediaFormat(mediaFormats[i]));
			}
		}

		NMediaFormat currentMediaFormat = mediaSource.getCurrentFormat(NMediaType.AUDIO);
		if (currentMediaFormat != null) {
			showMessage("current media format:");
			showMessage(MediaTutorialsApp.dumpMediaFormat(currentMediaFormat));

			if (mediaFormats != null) {
				showMessage("set the last supported format (optional) ... ");
				mediaSource.setCurrentFormat(NMediaType.AUDIO, mediaFormats[mediaFormats.length - 1]);
			}
		} else {
			showMessage("current media format is not yet available (will be availble after media reader start)");
		}

		showMessage("starting capture ... ");
		mediaReader.start();
		showMessage("capture started");

		try {
			currentMediaFormat = mediaSource.getCurrentFormat(NMediaType.AUDIO);
			if (currentMediaFormat == null) {
				throw new Exception("current media format is not set even after media reader start!");
			}
			showMessage("capturing with format: ");
			showMessage(MediaTutorialsApp.dumpMediaFormat(currentMediaFormat));

			for (int i = 0; i < bufferCount; i++) {
				NMediaReader.ReadResult result = mediaReader.readAudioSample();
				if (result.getSoundBuffer() == null) {
					return; // end of stream
				}

				showMessage(String.format("[%s %s] sample rate: %s, sample length: %s\n", result.getTimeStamp(), result.getDuration(), result.getSoundBuffer()
						.getSampleRate(), result.getSoundBuffer().getLength()));
			}
		} finally {
			mediaReader.stop();
		}
	}

	private void readAudio(Source source) {
		NMediaSource mediaSource = null;
		NMediaReader mediaReader = null;

		try {
			int bufferCount = 0;
			try {
				bufferCount = Integer.parseInt(mTextBufferLength.getText().toString());
			} catch (NumberFormatException exc) {
				showMessage("Invalid buffer length");
			}
			if (bufferCount <= 0) {
				showMessage("no sound buffers will be captured as sound buffer count is not specified");
				return;
			}

			// create media source
			switch (source) {
			case URL:
				if (!mTextAudioUrl.getText().toString().isEmpty()) {
					mediaSource = NMediaSource.fromUrl(mTextAudioUrl.getText().toString());
				} else {
					showMessage("Invalid Audio URL");
					return;
				}
				break;
			case AUDIO_FILE:
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

			mediaReader = new NMediaReader(mediaSource, EnumSet.of(NMediaType.AUDIO), true);
			readSoundBuffers(mediaReader, bufferCount);
			showMessage("Done");
		} catch (Exception e) {
			e.printStackTrace();
			showMessage(e.toString());
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
		setContentView(R.layout.tutorial_read_audio);
		mFromUrlButton = (Button) findViewById(R.id.tutorials_button_1);
		mFromUrlButton.setText(getString(R.string.msg_audio_url));
		mFromUrlButton.setOnClickListener(new View.OnClickListener() {
			@Override
			public void onClick(View v) {
				mResult.setText("");
				getAudio(Source.URL);
			}
		});
		mFromAudioFileButton = (Button) findViewById(R.id.tutorials_button_2);
		mFromAudioFileButton.setText(getString(R.string.msg_audio_file));
		mFromAudioFileButton.setOnClickListener(new View.OnClickListener() {
			@Override
			public void onClick(View v) {
				mResult.setText("");
				getAudio(Source.AUDIO_FILE);
			}
		});
		mResult = (TextView) findViewById(R.id.tutorials_results);

		mTextBufferLength = (EditText) findViewById(R.id.tutorials_field_1);
		mTextBufferLength.setKeyListener(DigitsKeyListener.getInstance("0123456789."));
		mTextBufferLength.setHint("Buffer Length");
		mTextAudioUrl = (EditText) findViewById(R.id.tutorials_field_2);
		mTextAudioUrl.setHint("Audio URL");
		LicensingManager.getInstance().obtain(this, this, Arrays.asList(LICENSE));
	}

	@Override
	protected void onDestroy() {
		super.onDestroy();
		if ((mProgressDialog != null) && (mProgressDialog.isShowing())) {
			mProgressDialog.dismiss();
		}
		try {
			LicensingManager.getInstance().release(Arrays.asList(LICENSE));
		} catch (IOException e) {
			Log.e(TAG, getString(R.string.msg_licenses_not_obtained), e);
		}
	}

	@Override
	protected void onActivityResult(int requestCode, int resultCode, Intent data) {
		if (requestCode == REQUEST_CODE_GET_AUDIO) {
			if (resultCode == RESULT_OK) {
				mSelectedFile = data.getData();
				readAudio(Source.AUDIO_FILE);
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
