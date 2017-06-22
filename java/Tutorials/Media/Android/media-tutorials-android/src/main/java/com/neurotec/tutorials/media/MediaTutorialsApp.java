package com.neurotec.tutorials.media;

import com.neurotec.media.NAudioFormat;
import com.neurotec.media.NMediaFormat;
import com.neurotec.media.NVideoFormat;
import com.neurotec.samples.util.EnvironmentUtils;
import com.neurotec.samples.util.ResourceUtils;

import android.app.Application;
import android.os.Environment;
import android.util.Log;

public final class MediaTutorialsApp extends Application {

	// ===========================================================
	// Private static fields
	// ===========================================================

	private static final String TAG = MediaTutorialsApp.class.getSimpleName();
	private static final String APP_NAME = "media-tutorials";
	private static final String OUTPUT_DIR_NAME = "output";

	// ===========================================================
	// Public static fields
	// ===========================================================

	public static final String FILE_SEPARATOR = System.getProperty("file.separator");
	public static final String TUTORIALS_OUTPUT_DATA_DIR = EnvironmentUtils.getDataDirectory(EnvironmentUtils.SAMPLE_DATA_DIR_NAME, APP_NAME, OUTPUT_DIR_NAME)
			.getAbsolutePath();
	public static final String TUTORIALS_ASSET_DIRECTORY = "input";

	// ===========================================================
	// Public static Methods
	// ===========================================================

	public static String dumpMediaFormat(NMediaFormat mediaFormat) {
		if (mediaFormat == null) {
			throw new NullPointerException("mediaFormat");
		}

		switch (mediaFormat.getMediaType()) {
		case VIDEO:
			NVideoFormat videoFormat = (NVideoFormat) mediaFormat;
			return String.format("video format .. %sx%s @ %s/%s (interlace: %s, aspect ratio: %s/%s)\n", videoFormat.getWidth(), videoFormat.getHeight(),
					videoFormat.getFrameRate().numerator, videoFormat.getFrameRate().denominator, videoFormat.getInterlaceMode(),
					videoFormat.getPixelAspectRatio().numerator, videoFormat.getPixelAspectRatio().denominator);
		case AUDIO:
			NAudioFormat audioFormat = (NAudioFormat) mediaFormat;
			return String.format("audio format .. channels: %s, samples/second: %s, bits/channel: %s\n", audioFormat.getChannelCount(),
					audioFormat.getSampleRate(), audioFormat.getBitsPerChannel());
		default:
			throw new AssertionError("unknown media type specified in format!");
		}
	}

	// ===========================================================
	// Public Methods
	// ===========================================================

	@Override
	public void onCreate() {
		super.onCreate();
		try {
			System.setProperty("jna.nounpack", "true");
			System.setProperty("java.io.tmpdir", getCacheDir().getAbsolutePath());
		} catch (Exception e) {
			Log.e(TAG, "Exception", e);
		}
	}
}
