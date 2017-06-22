package com.neurotec.samples.abis.subject;

import com.neurotec.biometrics.NFace;
import com.neurotec.biometrics.NFinger;
import com.neurotec.biometrics.NIris;
import com.neurotec.biometrics.NLAttributes;
import com.neurotec.biometrics.NLTemplate;
import com.neurotec.biometrics.NPalm;
import com.neurotec.biometrics.NSubject;
import com.neurotec.biometrics.NTemplate;
import com.neurotec.biometrics.NVoice;
import com.neurotec.images.NImage;
import com.neurotec.images.NImageFormat;
import com.neurotec.io.NBuffer;
import com.neurotec.sound.NSoundBuffer;
import com.neurotec.util.NVersion;

import java.io.ByteArrayInputStream;
import java.io.ByteArrayOutputStream;
import java.io.DataInputStream;
import java.io.DataOutputStream;
import java.io.IOException;
import java.io.UnsupportedEncodingException;
import java.nio.ByteBuffer;
import java.util.ArrayList;
import java.util.List;
import java.util.zip.DataFormatException;

public final class EnrollDataSerializer {

	// ===========================================================
	// Private static fields
	// ===========================================================

	private static final NVersion VERSION = new NVersion(1, 0);
	private static final String ENROLL_DATA_HEADER = "NeurotechnologySampleData";
	private static final String ENCODING = "US-ASCII";

	private static EnrollDataSerializer instance;

	// ===========================================================
	// Public static methods
	// ===========================================================

	public static EnrollDataSerializer getInstance() {
		synchronized (EnrollDataSerializer.class) {
			if (instance == null) {
				instance = new EnrollDataSerializer();
			}
			return instance;
		}
	}

	// ===========================================================
	// Private constructor
	// ===========================================================

	private EnrollDataSerializer() {
		// Suppress default constructor for noninstantiability.
	}

	// ===========================================================
	// Private methods
	// ===========================================================

	private void writeBuffer(DataOutputStream output, NBuffer buffer) throws IOException {
		byte[] bytes = buffer.toByteArray();
		output.writeInt(bytes.length);
		output.write(bytes);
	}

	private void writeImage(DataOutputStream output, NImage image, NImageFormat format) throws IOException {
		if (image != null) {
			NBuffer buffer = image.save(format);
			try {
				writeBuffer(output, buffer);
			} finally {
				buffer.dispose();
			}
		} else {
			writeBuffer(output, NBuffer.getEmpty());
		}
	}

	private boolean checkHeaderAndVersion(DataInputStream input) throws IOException {
		try {
			byte[] headerBytes = new byte[ENROLL_DATA_HEADER.getBytes(ENCODING).length];
			input.read(headerBytes);
			String header = new String(headerBytes, ENCODING);
			if (!ENROLL_DATA_HEADER.equals(header)) {
				return false;
			}
			NVersion version = new NVersion(input.readShort());
			return VERSION.equals(version);
		} catch (UnsupportedEncodingException e) {
			throw new AssertionError(ENCODING + " can't be unsupported");
		}
	}

	private static void writeHeaderAndVersion(DataOutputStream output) throws IOException {
		try {
			output.write(ENROLL_DATA_HEADER.getBytes(ENCODING));
			output.writeShort(VERSION.getValue());
		} catch (UnsupportedEncodingException e) {
			throw new AssertionError(ENCODING + " can't be unsupported");
		}
	}

	// ===========================================================
	// Public methods
	// ===========================================================

	public NBuffer serialize(NSubject subject, boolean useWsqForFingers) {
		ByteArrayOutputStream byteStream = new ByteArrayOutputStream();
		DataOutputStream dataStream = new DataOutputStream(byteStream);

		try {

			writeHeaderAndVersion(dataStream);

			List<NFinger> fingers = SubjectUtils.getTemplateCompositeFingers(subject);
			dataStream.writeInt(fingers.size());
			for (NFinger finger : fingers) {
				writeImage(dataStream, finger.getImage(), useWsqForFingers ? NImageFormat.getWSQ() : NImageFormat.getPNG());
			}

			List<NFace> faces = SubjectUtils.getTemplateCompositeFaces(subject);
			dataStream.writeInt(faces.size());
			for (NFace face : faces) {
				NLAttributes attributes = face.getObjects().get(0);
				NLTemplate template = attributes.getTemplate();
				dataStream.writeInt(template.getRecords().size());
				writeImage(dataStream, face.getImage(), NImageFormat.getPNG());
			}

			List<NIris> irises = SubjectUtils.getTemplateCompositeIrises(subject);
			dataStream.writeInt(irises.size());
			for (NIris iris : irises) {
				writeImage(dataStream, iris.getImage(), NImageFormat.getPNG());
			}

			List<NPalm> palms = SubjectUtils.getTemplateCompositePalms(subject);
			dataStream.writeInt(palms.size());
			for (NPalm palm : palms) {
				writeImage(dataStream, palm.getImage(), NImageFormat.getPNG());
			}

			List<NVoice> voices = SubjectUtils.getTemplateCompositeVoices(subject);
			dataStream.writeInt(voices.size());
			for (NVoice voice : voices) {
				NSoundBuffer sb = voice.getSoundBuffer();
				writeBuffer(dataStream, sb != null ? sb.save() : NBuffer.getEmpty());
			}

			dataStream.writeInt(0); // fin
			dataStream.flush();

		} catch (IOException e) {
			throw new AssertionError("Can't happen, because we're writing to ByteArrayOutputStream");
		}

		return new NBuffer(byteStream.toByteArray());
	}

	public NBuffer serialize(NSubject subject) throws IOException {
		return serialize(subject, false);
	}

	public NSubject deserialize(NBuffer templateData, NBuffer serializedData, List<Integer> faceRecordCounts) throws DataFormatException {

		NTemplate template = new NTemplate(templateData);
		try {
			ByteArrayInputStream byteStream = new ByteArrayInputStream(serializedData.toByteArray());
			DataInputStream dataStream = new DataInputStream(byteStream);
			NSubject subject = new NSubject();

			if (!checkHeaderAndVersion(dataStream)) {
				throw new DataFormatException("Unexpected header and version format");
			}

			// Fingers
			int length = dataStream.readInt();
			for (int i = 0; i < length; i++) {
				int bufferSize = dataStream.readInt();
				byte[] bytes = new byte[bufferSize];
				dataStream.read(bytes);
				NImage image = null;
				try {
					if (bufferSize > 0) {
						image = NImage.fromMemory(ByteBuffer.wrap(bytes));
					} else {
						image = null;
					}
					NFinger finger = (NFinger) NFinger.fromImageAndTemplate(image, template.getFingers().getRecords().get(i));
					subject.getFingers().add(finger);
				} finally {
					if (image != null) {
						image.dispose();
					}
				}
			}

			// Faces
			length = dataStream.readInt();
			int recordIndex = 0;
			faceRecordCounts.clear();
			for (int i = 0; i < length; i++) {
				int recordCount = dataStream.readInt();
				faceRecordCounts.add(recordCount);

				int bufferSize = dataStream.readInt();
				byte[] bytes = new byte[bufferSize];
				dataStream.read(bytes);
				NImage image = null;
				try {
					if (bufferSize > 0) {
						image = NImage.fromMemory(ByteBuffer.wrap(bytes));
					} else {
						image = null;
					}
					NLTemplate actualTemplate = new NLTemplate();
					NLAttributes attributes = new NLAttributes();
					for (int j = 0; j < recordCount; j++) {
						actualTemplate.getRecords().add(template.getFaces().getRecords().get(recordIndex));
						recordIndex++;
					}

					attributes.setTemplate(actualTemplate);
					NFace face = NFace.fromImageAndAttributes(image, attributes);
					subject.getFaces().add(face);
				} finally {
					if (image != null) {
						image.dispose();
					}
				}
			}

			// Irises
			length = dataStream.readInt();
			for (int i = 0; i < length; i++) {
				int bufferSize = dataStream.readInt();
				byte[] bytes = new byte[bufferSize];
				dataStream.read(bytes);
				NImage image = null;
				try {
					if (bufferSize > 0) {
						image = NImage.fromMemory(ByteBuffer.wrap(bytes));
					} else {
						image = null;
					}
					NIris iris = NIris.fromImageAndTemplate(image, template.getIrises().getRecords().get(i));
					subject.getIrises().add(iris);
				} finally {
					if (image != null) {
						image.dispose();
					}
				}
			}

			// Palms
			length = dataStream.readInt();
			for (int i = 0; i < length; i++) {
				int bufferSize = dataStream.readInt();
				byte[] bytes = new byte[bufferSize];
				dataStream.read(bytes);
				NImage image = null;
				try {
					if (bufferSize > 0) {
						image = NImage.fromMemory(ByteBuffer.wrap(bytes));
					} else {
						image = null;
					}
					NPalm palm = (NPalm) NPalm.fromImageAndTemplate(image, template.getPalms().getRecords().get(i));
					subject.getPalms().add(palm);
				} finally {
					if (image != null) {
						image.dispose();
					}
				}
			}

			// Voices
			length = dataStream.readInt();
			for (int i = 0; i < length; i++) {
				int bufferSize = dataStream.readInt();
				byte[] bytes = new byte[bufferSize];
				dataStream.read(bytes);
				NSoundBuffer sb = null;
				try {
					if (bufferSize > 0) {
						sb = NSoundBuffer.fromMemory(ByteBuffer.wrap(bytes));
					} else {
						sb = null;
					}
					NVoice voice = NVoice.fromSoundBufferAndTemplate(sb, template.getVoices().getRecords().get(i));
					subject.getVoices().add(voice);
				} finally {
					if (sb != null) {
						sb.dispose();
					}
				}
			}

			return subject;

		} catch (IOException e) {
			throw new AssertionError("Reading from byte array cannot result in IOException.");
		} finally {
			template.dispose();
		}
	}

	public NSubject deserialize(NBuffer templateData, NBuffer serializeData) throws DataFormatException {
		List<Integer> recordCounts = new ArrayList<Integer>();
		return deserialize(templateData, serializeData, recordCounts);
	}

}
