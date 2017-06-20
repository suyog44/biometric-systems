package com.neurotec.samples.biometrics.standards;

import com.neurotec.biometrics.standards.BDIFStandard;
import com.neurotec.util.NVersion;

public final class StandardVersion {

	private BDIFStandard standard;
	private NVersion version;
	private String standardName;

	public StandardVersion(BDIFStandard standard, NVersion version, String standardName) {
		this.standard = standard;
		this.version = version;
		this.standardName = standardName;
	}

	public BDIFStandard getStandard() {
		return standard;
	}

	public NVersion getVersion() {
		return version;
	}

	public String getStandardName() {
		return standardName;
	}

	@Override
	public String toString() {
		return String.format("%s. %s]", version, standardName);
	}
}