package com.neurotec.samples.abis.schema;

import com.neurotec.biometrics.NBiographicDataElement;
import com.neurotec.biometrics.NDBType;

import java.util.ArrayList;
import java.util.List;

import javax.swing.event.TableModelEvent;
import javax.swing.event.TableModelListener;
import javax.swing.table.TableModel;

public class SchemaBuilderModel implements TableModel {

	// ===========================================================
	// Private fields
	// ===========================================================

	private DatabaseSchema schema;
	private final SchemaElementGroup group;
	private final List<TableModelListener> modelListeners;

	// ===========================================================
	// Public constructor
	// ===========================================================

	public SchemaBuilderModel(DatabaseSchema schema, SchemaElementGroup group) {
		this.schema = schema;
		this.group = group;
		this.modelListeners = new ArrayList<TableModelListener>();
	}

	// ===========================================================
	// Private methods
	// ===========================================================

	private SchemaColumnType getColumnType(NBiographicDataElement element) {
		if (element.name.equals(schema.getGenderDataName())) {
			return SchemaColumnType.GENDER;
		} else if (element.name.equals(schema.getEnrollDataName())) {
			return SchemaColumnType.ENROLL_DATA;
		} else if (element.name.equals(schema.getThumbnailDataName())) {
			return SchemaColumnType.THUMBNAIL;
		} else {
			boolean isCustom = contains(schema.getCustomData().getElements(), element);
			if (element.dbType == NDBType.STRING) {
				return isCustom ? SchemaColumnType.CUSTOM_DATA_STRING : SchemaColumnType.BIOGRAPHIC_DATA_STRING;
			}
			if (element.dbType == NDBType.INTEGER) {
				return isCustom ? SchemaColumnType.CUSTOM_DATA_INTEGER : SchemaColumnType.BIOGRAPHIC_DATA_INTEGER;
			}
			if (element.dbType == NDBType.BLOB && isCustom) {
				return SchemaColumnType.CUSTOM_DATA_BLOB;
			}
		}
		return SchemaColumnType.UNKNOWN;
	}

	private boolean contains(List<NBiographicDataElement> list, NBiographicDataElement element) {
		for (NBiographicDataElement e : list) {
			if (e.name.equals(element.name)) {
				return true;
			}
		}
		return false;
	}

	private void fireModelChange() {
		for (TableModelListener l : modelListeners) {
			l.tableChanged(new TableModelEvent(this));
		}
	}

	// ===========================================================
	// Public methods
	// ===========================================================

	public void setSchema(DatabaseSchema schema) {
		this.schema = schema;
		fireModelChange();
	}

	public DatabaseSchema getSchema() {
		return schema;
	}

	public void refresh() {
		fireModelChange();
	}

	@Override
	public int getRowCount() {
		switch (group) {
		case BIOGRAPHIC:
			return schema.getBiographicData().getElements().size();
		case SAMPLE:
		case CUSTOM:
			int count = 0;
			for (NBiographicDataElement element : schema.getCustomData().getElements()) {
				if (getColumnType(element).getSchemaElementGroup() == group) {
					count++;
				}
			}
			return count;
		default:
			throw new AssertionError("unreachable");
		}
	}

	@Override
	public int getColumnCount() {
		return 3;
	}

	@Override
	public String getColumnName(int columnIndex) {
		switch(columnIndex) {
		case 0:
			return "Type";
		case 1:
			return "Name";
		case 2:
			return "DB Column";
		default:
			throw new AssertionError("unreachable");
		}
	}

	@Override
	public Class<?> getColumnClass(int columnIndex) {
		return String.class;
	}

	@Override
	public boolean isCellEditable(int rowIndex, int columnIndex) {
		return false;
	}

	@Override
	public Object getValueAt(int rowIndex, int columnIndex) {
		NBiographicDataElement element;
		if (group == SchemaElementGroup.BIOGRAPHIC) {
			element = schema.getBiographicData().getElements().get(rowIndex);
		} else if ((group == SchemaElementGroup.CUSTOM) || (group == SchemaElementGroup.SAMPLE)) {
			int i = -1;
			element = null;
			for (NBiographicDataElement e : schema.getCustomData().getElements()) {
				if (getColumnType(e).getSchemaElementGroup() == group) {
					i++;
					if (i == rowIndex) {
						element = e;
						break;
					}
				}
			}
			if (element == null) {
				throw new IllegalArgumentException("rowIndex: " + rowIndex);
			}
		} else {
			throw new AssertionError("No such SchemaElementGroup: " + group);
		}
		switch(columnIndex) {
		case 0:
			return getColumnType(element).toString();
		case 1:
			return element.name;
		case 2:
			return element.dbColumn;
		default:
			throw new AssertionError("unreachable");
		}
	}

	@Override
	public void setValueAt(Object aValue, int rowIndex, int columnIndex) {
		throw new UnsupportedOperationException();
	}

	@Override
	public void addTableModelListener(TableModelListener l) {
		modelListeners.add(l);
	}

	@Override
	public void removeTableModelListener(TableModelListener l) {
		modelListeners.remove(l);
	}

}
