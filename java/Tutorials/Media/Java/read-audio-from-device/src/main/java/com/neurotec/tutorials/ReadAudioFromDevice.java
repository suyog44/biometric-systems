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

public final class ReadAudioFromDevice {

	private static final String DESCRIPTION = "Demonstrates reading sound from device.";
	private static final String NAME = "read-audio-from-device";
	private static final String VERSION = "9.0.0.0";

	private static void usage() {
		System.out.println("usage:");
		System.out.format("\t%s [bufferCount]", NAME);
		System.out.println();
		System.out.println("\tbufferCount - number of sound buffers to capture from each device to current directory");
		System.out.println();
	}

	public static void main(String[] args) {
		final String Components = "Media";

		LibraryManager.initLibraryPath();

		Utils.printTutorialHeader(DESCRIPTION, NAME, VERSION, args);

		if (args.length < 1) {
			usage();
			System.exit(-1);
		}

		try {
			if (!(NLicense.obtainComponents("/local", 5000, Components))) {
				System.err.println(String.format("Could not obtain licenses for components: %s", Components));
				System.exit(-1);
			}

			int bufferCount = Integer.parseInt(args[0]);
			if (bufferCount == 0) {
				System.out.println("no sound buffers will be captured as sound buffer count is not specified");
			}

			System.out.println("quering connected audio devices ...");
			NMediaSource[] devices = NMediaSource.enumDevices(EnumSet.of(NMediaType.AUDIO));
			System.out.format("devices found: %s\n", devices.length);

			for (NMediaSource device : devices) {
				System.out.format("found device: %s\n", device.getDisplayName());
				readSoundBufers(device, bufferCount);
				System.out.println("done");
			}
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
				NLicense.releaseComponents(Components);
			} catch (IOException e) {
				e.printStackTrace();
			}
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

	private static void readSoundBufers(NMediaSource source, int bufferCount) throws Exception {
		NMediaReader mediaReader = null;
		try {
			mediaReader = new NMediaReader(source, EnumSet.of(NMediaType.AUDIO), true);
			System.out.format("media length: %s\n", mediaReader.getLength());

			NMediaFormat[] mediaFormats = source.getFormats(NMediaType.AUDIO);
			if (mediaFormats == null) {
				System.out.println("formats are not yet availbel (should be availble after media reader is started");
			} else {
				System.out.format("format count: %s\n", mediaFormats.length);
				for (int i = 0; i < mediaFormats.length; i++) {
					System.out.printf("[%s] ", i);
					dumpMediaFormat(mediaFormats[i]);
				}
			}

			NMediaFormat currentMediaFormat = source.getCurrentFormat(NMediaType.AUDIO);
			if (currentMediaFormat != null) {
				System.out.println("current media format:");
				dumpMediaFormat(currentMediaFormat);

				if (mediaFormats != null) {
					System.out.println("set the last supported format (optional) ... ");
					source.setCurrentFormat(NMediaType.AUDIO, mediaFormats[mediaFormats.length - 1]);
				}
			} else {
				System.out.println("current media format is not yet available (will be availble after media reader start)");
			}

			System.out.print("starting capture ... ");
			mediaReader.start();
			System.out.println("capture started");

			try {
				currentMediaFormat = source.getCurrentFormat(NMediaType.AUDIO);
				if (currentMediaFormat == null) {
					throw new Exception("current media format is not set even after media reader start!");
				}
				System.out.println("capturing with format: ");
				dumpMediaFormat(currentMediaFormat);

				for (int i = 0; i < bufferCount; i++) {
					NMediaReader.ReadResult result = mediaReader.readAudioSample();
					if (result.getSoundBuffer() == null) return;// end of stream

					System.out.format("[%s %s] sample rate: %s, sample length: %s\n", result.getTimeStamp(), result.getDuration(), result.getSoundBuffer().getSampleRate(), result.getSoundBuffer().getLength());
				}
			} finally {
				mediaReader.stop();
			}
		} finally {
			if (mediaReader != null) mediaReader.dispose();
		}
	}
}
