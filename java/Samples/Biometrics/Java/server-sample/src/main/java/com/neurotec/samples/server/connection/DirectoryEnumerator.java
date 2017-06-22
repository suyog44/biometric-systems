package com.neurotec.samples.server.connection;

import java.io.File;
import java.io.FileFilter;
import java.io.IOException;

import com.neurotec.biometrics.NSubject;
import com.neurotec.io.NBuffer;
import com.neurotec.io.NFile;

public final class DirectoryEnumerator implements TemplateLoader {

	// ==============================================
	// Private fields
	// ==============================================

	private File[] files = null;
	private final File directory;
	private int index = -1;
	private int resultCount;

	// ==============================================
	// Public constructor
	// ==============================================

	public DirectoryEnumerator(String directoryPath) {
		String templateDir = directoryPath;
		if (templateDir == null || templateDir.equals("")) {
			throw new IllegalArgumentException("Specified directory doesn't exists");
		}
		directory = new File(templateDir);
		if (!directory.exists() || !directory.isDirectory()) {
			throw new IllegalArgumentException("Specified directory doesn't exists");
		}
	}

	// ==============================================
	// Public methods
	// ==============================================

	@Override
	public synchronized void beginLoad() {
		if (index != -1) {
			throw new IllegalStateException();
		}
		listFiles();
		index = 0;
	}

	@Override
	public synchronized void endLoad() {
		index = -1;
	}

	@Override
	public synchronized NSubject[] loadNext(int n) throws IOException {
		if (index == -1) {
			throw new IllegalStateException();
		}
		if (resultCount == 0 || resultCount <= index) {
			return new NSubject[0];
		}

		int count = resultCount - index;
		count = count > n ? n : count;
		NSubject[] results = new NSubject[count];
		for (int i = 0; i < count; i++) {
			File file = files[index++];
			String id = file.getName();
			NBuffer template = NFile.readAllBytes(file.getAbsolutePath());
			NSubject result = new NSubject();
			result.setTemplateBuffer(template);
			result.setId(id);
			results[i] = result;
		}
		return results;
	}

	public void dispose() {
	}

	@Override
	public int getTemplateCount() {
		listFiles();
		return resultCount;
	}

	@Override
	public String toString() {
		return directory.getPath();
	}

	// ==============================================
	// Private methods
	// ==============================================

	public synchronized void listFiles() {
		if (files == null) {
			files = directory.listFiles(new FileFilter() {
				@Override
				public boolean accept(File pathname) {
					return pathname.isFile();
				}
			});
			resultCount = files.length;
		}
	}
	
}
