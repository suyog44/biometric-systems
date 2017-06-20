package com.neurotec.tutorials;

import java.io.IOException;
import java.util.EnumSet;

import com.neurotec.lang.NThrowable;
import com.neurotec.licensing.NLicense;
import com.neurotec.media.NAudioFormat;
import com.neurotec.media.NMediaFormat;
import com.neurotec.media.NMediaReader;
import com.neurotec.media.NMediaSource;
import com.neurotec.media.NMediaType;
import com.neurotec.media.NVideoFormat;
import com.neurotec.tutorials.util.LibraryManager;
import com.neurotec.tutorials.util.Utils;

public final class ReadAudio {

	private static final String DESCRIPTION = "Demonstrates reading sound from specified filename or url";
	private static final String NAME = "read-audio";
	private static final String VERSION = "9.0.0.0";

	private static void usage() {
		System.out.println("usage:");
		System.out.format("\t%s [source] [bufferCount] <optional: is url>%n", NAME);
		System.out.println();
		System.out.println("\tsource - filename or url sound buffers should be captured from.");
		System.out.println("\tbufferCount - number of sound buffers to capture from specified filename or url.");
		System.out.println("\tis url - specifies that passed source parameter is url (value: 1) or filename (value: 0).");
		System.out.println();
	}

	public static void main(String[] args) {
		final String components = "Media";

		LibraryManager.initLibraryPath();

		Utils.printTutorialHeader(DESCRIPTION, NAME, VERSION, args);

		if (args.length < 2) {
			usage();
			System.exit(-1);
		}

		NMediaSource mediaSource = null;
		NMediaReader mediaReader = null;

		try {
			if (!NLicense.obtainComponents("/local", 5000, components)) {
				System.err.println("Could not obtain licenses for components: " + components);
				System.exit(-1);
			}

			String uri = args[0];
			boolean isUrl = false;
			int bufferCount = Integer.parseInt(args[1]);
			if (bufferCount <= 0) {
				System.out.println("no sound buffers will be captured as sound buffer count is not specified");
			}

			if (args.length > 2) {
				isUrl = args[2] == "1";
			}

			// create media source
			mediaSource = (isUrl) ? NMediaSource.fromUrl(uri) : NMediaSource.fromFile(uri);
			System.out.format("display name: %s\n", mediaSource.getDisplayName());

			mediaReader = new NMediaReader(mediaSource, EnumSet.of(NMediaType.AUDIO), true);
			readSoundBuffers(mediaReader, bufferCount);
			System.out.println("done");
		} catch (Throwable th) {
			th.printStackTrace();

			int errorCode = -1;

			if (th instanceof NThrowable) {
				errorCode = ((NThrowable)th).getCode();
			}

			System.exit(errorCode);
		} finally {
			try {
				NLicense.releaseComponents(components);
			} catch (IOException e) {
				e.printStackTrace();
			}
			if (mediaSource != null) mediaSource.dispose();
			if (mediaReader != null) mediaReader.dispose();
		}
	}

	private static void readSoundBuffers(NMediaReader mediaReader, int bufferCount) throws Exception {
		NMediaSource mediaSource = mediaReader.getSource();

		System.out.format("media length: %s\n", mediaReader.getLength());

		NMediaFormat[] mediaFormats = mediaSource.getFormats(NMediaType.AUDIO);
		if (mediaFormats == null) {
			System.err.println("formats are not yet available (should be available after media reader is started");
		} else {
			System.out.format("format count: %s\n", mediaFormats.length);
			for (int i = 0; i < mediaFormats.length; i++) {
				System.out.format("[%s] ", i);
				dumpMediaFormat(mediaFormats[i]);
			}
		}

		NMediaFormat currentMediaFormat = mediaSource.getCurrentFormat(NMediaType.AUDIO);
		if (currentMediaFormat != null) {
			System.out.println("current media format:");
			dumpMediaFormat(currentMediaFormat);

			if (mediaFormats != null) {
				System.out.println("set the last supported format (optional) ... ");
				mediaSource.setCurrentFormat(NMediaType.AUDIO, mediaFormats[mediaFormats.length - 1]);
			}
		} else
			System.err.println("current media format is not yet available (will be availble after media reader start)");

		System.out.print("starting capture ... ");
		mediaReader.start();
		System.out.println("capture started");

		try {
			currentMediaFormat = mediaSource.getCurrentFormat(NMediaType.AUDIO);
			if (currentMediaFormat == null) throw new Exception("current media format is not set even after media reader start!");
			System.out.println("capturing with format: ");
			dumpMediaFormat(currentMediaFormat);

			for (int i = 0; i < bufferCount; i++) {

				NMediaReader.ReadResult result = mediaReader.readAudioSample();
				if (result.getSoundBuffer() == null) return; // end of stream

				System.out.format("[%s %s] sample rate: %s, sample length: %s\n", result.getTimeStamp(), result.getDuration(), result.getSoundBuffer().getSampleRate(), result.getSoundBuffer().getLength());
			}
		} finally {
			mediaReader.stop();
		}
	}

	private static void dumpMediaFormat(NMediaFormat mediaFormat) {
		if (mediaFormat == null) throw new NullPointerException("mediaFormat");
		switch (mediaFormat.getMediaType()) {
		case VIDEO:
			NVideoFormat videoFormat = (NVideoFormat) mediaFormat;
			System.out.format("video format .. %sx%s @ %s/%s (interlace: %s, aspect ratio: %s/%s)\n", videoFormat.getWidth(), videoFormat.getHeight(), videoFormat.getFrameRate().numerator, videoFormat.getFrameRate().denominator, videoFormat.getInterlaceMode(), videoFormat.getPixelAspectRatio().numerator, videoFormat.getPixelAspectRatio().denominator);
			break;
		case AUDIO:
			NAudioFormat audioFormat = (NAudioFormat) mediaFormat;
			System.out.format("audio format .. channels: %s, samples/second: %s, bits/channel: %s\n", audioFormat.getChannelCount(), audioFormat.getSampleRate(), audioFormat.getBitsPerChannel());
			break;
		default:
			throw new IllegalArgumentException("unknown media type specified in format!");
		}
	}
}
