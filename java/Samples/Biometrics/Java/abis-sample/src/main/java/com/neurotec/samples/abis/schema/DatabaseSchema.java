package com.neurotec.samples.abis.schema;

import com.neurotec.biometrics.NBiographicDataElement;
import com.neurotec.biometrics.NBiographicDataSchema;
import com.neurotec.biometrics.NBiographicDataSchema.ElementCollection;
import com.neurotec.biometrics.NSubject;
import com.neurotec.lang.reflect.NPropertyInfo;
import com.neurotec.samples.util.Utils;
import java.util.ArrayList;
import java.util.List;

public class DatabaseSchema {

	// ===========================================================
	// Public constants
	// ===========================================================

	public static final DatabaseSchema EMPTY = new DatabaseSchema("None");

	// ===========================================================
	// Public static methods
	// ===========================================================

	public static DatabaseSchema parse(String value)	{
		String[] values = value.split("#");
		if (values.length != 6) {
			throw new IllegalArgumentException("value must be splittable to 6 parts");
		}
		NBiographicDataSchema biographic = NBiographicDataSchema.parse(values[1]);
		NBiographicDataSchema custom = Utils.isNullOrEmpty(values[2]) ? null : NBiographicDataSchema.parse(values[2]);

		DatabaseSchema sc = new DatabaseSchema(values[0], biographic, custom);
		String[] keyAndValue = values[3].split("=");
		if (keyAndValue.length > 1) {
			sc.setGenderDataName(keyAndValue[1]);
		} else {
			sc.setGenderDataName("");
		}
		keyAndValue = values[4].split("=");
		if (keyAndValue.length > 1) {
			sc.setThumbnailDataName(keyAndValue[1]);
		} else {
			sc.setThumbnailDataName("");
		}
		keyAndValue = values[5].split("=");
		if (keyAndValue.length > 1) {
			sc.setEnrollDataName(keyAndValue[1]);
		} else {
			sc.setEnrollDataName("");
		}

		return sc;
	}

	// ===========================================================
	// Private fields
	// ===========================================================

	private final String name;
	private NBiographicDataSchema biographicData;
	private NBiographicDataSchema customData;

	private String genderDataName;
	private String enrollDataName;
	private String thumbnailDataName;

	// ===========================================================
	// Public constructors
	// ===========================================================

	public DatabaseSchema(String name, NBiographicDataSchema biographicData, NBiographicDataSchema customData) {
		this.name = name;
		this.biographicData = biographicData;
		this.customData = customData;
	}

	public DatabaseSchema(String name) {
		this.name = name;
		this.biographicData = new NBiographicDataSchema();
		this.customData = new NBiographicDataSchema();
	}

	// ===========================================================
	// Private methods
	// ===========================================================

	private boolean checkNameDoesNotConflict(String elementName) {
		for (NPropertyInfo info : NSubject.nativeTypeOf().getDeclaredProperties()) {
			if (info.getName().equals(elementName)) {
				return false;
			}
		}
		return true;
	}

	// ===========================================================
	// Public methods
	// ===========================================================

	public void addElement(SchemaColumnType columnType, String elementName, String dbColumn) {
		SchemaElementGroup group = columnType.getSchemaElementGroup();
		ElementCollection elements;
		if (group == SchemaElementGroup.BIOGRAPHIC) {
			elements = getBiographicData().getElements();
		} else if ((group == SchemaElementGroup.CUSTOM) || (group == SchemaElementGroup.SAMPLE)) {
			elements = getCustomData().getElements();
		} else {
			throw new AssertionError("No such SchemaElementGroup: " + group);
		}
		if (!checkNameDoesNotConflict(elementName)) {
			throw new IllegalArgumentException("Name can not be same as NSubject property name");
		}
		for (NBiographicDataElement element : elements) {
			if (element.name.equals(elementName)) {
				throw new IllegalArgumentException("Item with same name already exists");
			}
			if (!Utils.isNullOrEmpty(element.dbColumn) && element.dbColumn.equals(dbColumn)) {
				throw new IllegalArgumentException("Item with same column name alread exists");
			}
			if (!Utils.isNullOrEmpty(dbColumn)) {
				if (Utils.isNullOrEmpty(element.dbColumn) && element.name.equals(dbColumn)) {
					throw new IllegalArgumentException("Item with same column name alread exists");
				}
			}
		}
		if (!getEnrollDataName().isEmpty() && columnType == SchemaColumnType.ENROLL_DATA) {
			throw new IllegalArgumentException("Enroll data field already exists");
		}
		if (!getGenderDataName().isEmpty() && columnType == SchemaColumnType.GENDER) {
			throw new IllegalArgumentException("Gender field already exists");
		}
		if (!getThumbnailDataName().isEmpty() && columnType == SchemaColumnType.THUMBNAIL) {
			throw new IllegalArgumentException("Thumbnail data field already exists");
		}
		NBiographicDataElement element = new NBiographicDataElement(elementName, dbColumn, columnType.getDbType());
		elements.add(element);
	}

	public boolean removeElement(String elementName) {
		ElementCollection elements = getBiographicData().getElements();
		for (int i = 0; i < elements.size(); i++) {
			NBiographicDataElement element = elements.get(i);
			if (element.name.equals(elementName)) {
				elements.remove(i);
				if (element.name.equals(getEnrollDataName())) {
					setEnrollDataName(null);
				} else if (element.name.equals(getGenderDataName())) {
					setGenderDataName(null);
				} else if (element.name.equals(getThumbnailDataName())) {
					setThumbnailDataName(null);
				}
				return true;
			}
		}
		elements = getCustomData().getElements();
		for (int i = 0; i < elements.size(); i++) {
			NBiographicDataElement element = elements.get(i);
			if (element.name.equals(elementName)) {
				elements.remove(i);
				return true;
			}
		}
		return false;
	}

	public String save() {
		if (isEmpty()) {
			throw new IllegalStateException("Cannot save empty schema");
		}
		String format = "%s#%s#%s#Gender=%s#Thumbnail=%s#EnrollData=%s";
		String biographic = biographicData == null ? "" : biographicData.toString();
		String custom = customData == null ? "" : customData.toString();

		String gender = genderDataName == null ? "" : genderDataName;
		String thumbnail = thumbnailDataName == null ? "" : thumbnailDataName;
		String enroll = enrollDataName == null ? "" : enrollDataName;

		return String.format(format, name, biographic, custom, gender, thumbnail, enroll);
	}

	public List<String> getAllowedProperties() {
		List<String> allowedProperties = new ArrayList<String>();
		for (NBiographicDataElement element : getBiographicData().getElements()) {
			allowedProperties.add(element.name);
		}
		for (NBiographicDataElement element : getCustomData().getElements()) {
			allowedProperties.add(element.name);
		}
		return allowedProperties;
	}

	public String getName() {
		return name;
	}

	public NBiographicDataSchema getBiographicData() {
		return biographicData;
	}

	public void setBiographicData(NBiographicDataSchema biographicData) {
		this.biographicData = biographicData;
	}

	public NBiographicDataSchema getCustomData() {
		return customData;
	}

	public void setCustomData(NBiographicDataSchema customData) {
		this.customData = customData;
	}

	public String getGenderDataName() {
		return genderDataName == null ? "" : genderDataName;
	}

	public void setGenderDataName(String genderDataName) {
		this.genderDataName = genderDataName;
	}

	public String getEnrollDataName() {
		return enrollDataName == null ? "" : enrollDataName;
	}

	public void setEnrollDataName(String enrollDataName) {
		this.enrollDataName = enrollDataName;
	}

	public String getThumbnailDataName() {
		return thumbnailDataName == null ? "" : thumbnailDataName;
	}

	public void setThumbnailDataName(String thumbnailDataName) {
		this.thumbnailDataName = thumbnailDataName;
	}

	public boolean hasCustomData() {
		return (customData != null) && !customData.getElements().isEmpty();
	}

	public boolean isEmpty() {
		return this == EMPTY;
	}

	@Override
	public String toString() {
		return name;
	}

	@Override
	public int hashCode() {
		return save().hashCode();
	}

	@Override
	public boolean equals(Object obj) {
		if (obj == null) {
			return false;
		}
		if (getClass() != obj.getClass()) {
			return false;
		}
		if ((this == EMPTY) || (obj == EMPTY)) {
			return this == obj;
		}
		final DatabaseSchema other = (DatabaseSchema) obj;
		return other.save().equals(save());
	}

}
