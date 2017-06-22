package com.neurotec.samples.abis.schema;

import com.neurotec.biometrics.NDBType;

public enum SchemaColumnType {

	UNKNOWN(NDBType.NONE, null, "Unknown"),
	BIOGRAPHIC_DATA_STRING(NDBType.STRING, SchemaElementGroup.BIOGRAPHIC, "Biographic Data String"),
	BIOGRAPHIC_DATA_INTEGER(NDBType.INTEGER, SchemaElementGroup.BIOGRAPHIC, "Biographic Data Integer"),
	GENDER(NDBType.STRING, SchemaElementGroup.BIOGRAPHIC, "Gender (String)"),
	THUMBNAIL(NDBType.BLOB, SchemaElementGroup.SAMPLE, "Thumbnail (Blob)"),
	ENROLL_DATA(NDBType.BLOB, SchemaElementGroup.SAMPLE, "Enroll Data (Blob)"),
	CUSTOM_DATA_STRING(NDBType.STRING, SchemaElementGroup.CUSTOM, "Custom Data String"),
	CUSTOM_DATA_INTEGER(NDBType.INTEGER, SchemaElementGroup.CUSTOM, "Custom Data Integer"),
	CUSTOM_DATA_BLOB(NDBType.BLOB, SchemaElementGroup.CUSTOM, "Custom Data Blob");

	private final NDBType dbType;
	private final SchemaElementGroup schemaElementGroup;
	private final String displayName;

	private SchemaColumnType(NDBType dbType, SchemaElementGroup schemaElementGroup, String displayName) {
		this.dbType = dbType;
		this.schemaElementGroup = schemaElementGroup;
		this.displayName = displayName;
	}

	public NDBType getDbType() {
		return dbType;
	}

	public SchemaElementGroup getSchemaElementGroup() {
		return schemaElementGroup;
	}

	@Override
	public String toString() {
		return displayName;
	}

}
