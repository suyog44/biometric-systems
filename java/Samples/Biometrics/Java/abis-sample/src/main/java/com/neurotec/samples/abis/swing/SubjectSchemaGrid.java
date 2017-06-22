package com.neurotec.samples.abis.swing;

import com.neurotec.biometrics.NBiographicDataElement;
import com.neurotec.biometrics.NBiographicDataSchema.ElementCollection;
import com.neurotec.biometrics.NDBType;
import com.neurotec.biometrics.NGender;
import com.neurotec.io.NBuffer;
import com.neurotec.samples.abis.schema.DatabaseSchema;
import com.neurotec.samples.abis.util.KeyValuePair;
import com.neurotec.util.NPropertyBag;

import java.util.ArrayList;
import java.util.Collections;
import java.util.List;
import java.util.Map.Entry;

import javax.swing.DefaultCellEditor;
import javax.swing.JComboBox;
import javax.swing.JTable;
import javax.swing.event.TableModelListener;
import javax.swing.table.TableCellEditor;
import javax.swing.table.TableModel;

public class SubjectSchemaGrid extends JTable {

	// ==============================================
	// Nested classes
	// ==============================================

	private static class SubjectPropertyTableModel implements TableModel {

		private final List<KeyValuePair<String, Object>> properties;
		private boolean editable;
		private boolean showBlobs;

		SubjectPropertyTableModel(List<KeyValuePair<String, Object>> properties) {
			this.properties = properties;
			this.editable = true;
		}

		private boolean isTypeEditable(Object value) {
			return (value instanceof Integer) || (value instanceof String) || (value instanceof NGender);
		}

		private int adjustRowIndex(int rowIndex) {
			if (showBlobs) {
				return rowIndex;
			} else {
				int rows = rowIndex;
				int i = 0;
				for (; rows >= 0; i++) {
					if (!(properties.get(i).getValue() instanceof NBuffer)) {
						rows--;
					}
				}
				return i - 1;
			}
		}

		List<KeyValuePair<String, Object>> getProperties() {
			return properties;
		}

		boolean isEditable() {
			return editable;
		}

		void setEditable(boolean value) {
			editable = value;
		}

		boolean isShowBlobs() {
			return showBlobs;
		}

		void setShowBlobs(boolean showBlobs) {
			this.showBlobs = showBlobs;
		}

		@Override
		public int getRowCount() {
			int count = properties.size();
			if (!showBlobs) {
				for (KeyValuePair<String, Object> pair : properties) {
					if (pair.getValue() instanceof NBuffer) {
						count--;
					}
				}
			}
			return count;
		}

		@Override
		public int getColumnCount() {
			return 2;
		}

		@Override
		public String getColumnName(int columnIndex) {
			return (columnIndex == 0) ? "Property" : "Value";
		}

		@Override
		public Class<?> getColumnClass(int columnIndex) {
			if (columnIndex == 0) {
				return String.class;
			} else {
				return Object.class;
			}
		}

		@Override
		public boolean isCellEditable(int rowIndex, int columnIndex) {
			int index = adjustRowIndex(rowIndex);
			return editable && (columnIndex == 1) && (isTypeEditable(properties.get(index).getValue()));
		}

		@Override
		public Object getValueAt(int rowIndex, int columnIndex) {
			int index = adjustRowIndex(rowIndex);
			if (columnIndex == 0) {
				return properties.get(index).getKey();
			} else {
				return properties.get(index).getValue();
			}
		}

		@Override
		public void setValueAt(Object aValue, int rowIndex, int columnIndex) {
			if (columnIndex != 1) {
				throw new IllegalArgumentException("columnIndex");
			}
			Object value = null;
			if (aValue instanceof String) {
				try {
					value = Integer.valueOf((String) aValue);
				} catch (NumberFormatException e) {
					// Do nothing;
				}
			}
			if (value == null) {
				value = aValue;
			}
			int index = adjustRowIndex(rowIndex);
			Class<?> cls = properties.get(index).getValue().getClass();
			if (cls.isAssignableFrom(value.getClass())) {
				KeyValuePair<String, Object> pair = properties.get(index);
				properties.set(index, new KeyValuePair<String, Object>(pair.getKey(), value));
			}
		}

		@Override
		public void addTableModelListener(TableModelListener l) {
			// This model does not fire events.
		}

		@Override
		public void removeTableModelListener(TableModelListener l) {
			// This model does not fire events.
		}

	}

	// ==============================================
	// Private static fields
	// ==============================================

	private static final long serialVersionUID = 1L;

	// ==============================================
	// Public constructor
	// ==============================================

	public SubjectSchemaGrid() {
		setModel(new SubjectPropertyTableModel(Collections.<KeyValuePair<String, Object>>emptyList()));
	}

	// ==============================================
	// Private methods
	// ==============================================

	private SubjectPropertyTableModel getSubjectPropertyTableModel() {
		if (!(getModel() instanceof SubjectPropertyTableModel)) {
			throw new IllegalStateException("Table model inconsistent with table class");
		}
		return (SubjectPropertyTableModel) getModel();
	}

	private Object getDefaultValue(NDBType type) {
		switch (type) {
		case BLOB:
			return NBuffer.getEmpty();
		case INTEGER:
			return 0;
		case NONE:
			return null;
		case STRING:
			return "";
		default:
			throw new AssertionError("unreachable");
		}
	}

	// ==============================================
	// Public methods
	// ==============================================

	public void setSource(DatabaseSchema schema) {
		List<KeyValuePair<String, Object>> properties = new ArrayList<KeyValuePair<String, Object>>();

		ElementCollection elements = schema.getBiographicData().getElements();
		for (NBiographicDataElement element : elements) {
			if (element.name.equals(schema.getGenderDataName())) {
				properties.add(new KeyValuePair<String, Object>(element.name, NGender.UNSPECIFIED));
			} else {
				properties.add(new KeyValuePair<String, Object>(element.name, getDefaultValue(element.dbType)));
			}
		}

		elements = schema.getCustomData().getElements();
		for (NBiographicDataElement element : elements) {
			if (element.name.equals(schema.getGenderDataName())) {
				properties.add(new KeyValuePair<String, Object>(element.name, NGender.UNSPECIFIED));
			} else {
				properties.add(new KeyValuePair<String, Object>(element.name, getDefaultValue(element.dbType)));
			}
		}

		SubjectPropertyTableModel newModel = new SubjectPropertyTableModel(properties);
		newModel.setEditable(isEditable());
		setModel(newModel);
	}

	public boolean isEmpty() {
		return getSubjectPropertyTableModel().getProperties().isEmpty();
	}

	public void setValue(String key, Object value) {
		for (int i = 0; i < getSubjectPropertyTableModel().getProperties().size(); i++) {
			KeyValuePair<String, Object> pair = getSubjectPropertyTableModel().getProperties().get(i);
			if (pair.getKey().equals(key)) {
				getSubjectPropertyTableModel().getProperties().set(i, new KeyValuePair<String, Object>(key, value));
				return;
			}
		}
		getSubjectPropertyTableModel().getProperties().add(new KeyValuePair<String, Object>(key, value));
	}

	public void setValues(NPropertyBag bag) {
		for (Entry<String, Object> entry : bag.entrySet()) {
			setValue(entry.getKey(), entry.getValue());
		}
	}

	public Object getValue(String key) {
		for (KeyValuePair<String, Object> pair : getSubjectPropertyTableModel().getProperties()) {
			if (pair.getKey().equals(key)) {
				return pair.getValue();
			}
		}
		return null;
	}

	public NPropertyBag getProperties() {
		NPropertyBag bag = new NPropertyBag();
		for (KeyValuePair<String, Object> pair : getSubjectPropertyTableModel().getProperties()) {
			Object value = pair.getValue();
			if ((value != null) && !value.equals(NBuffer.getEmpty()) && !((value instanceof String) && ((String) value).isEmpty())) {
				bag.add(pair.getKey(), pair.getValue());
			}
		}
		return bag;
	}

	public boolean isEditable() {
		return getSubjectPropertyTableModel().isEditable();
	}

	public void setEditable(boolean value) {
		getSubjectPropertyTableModel().setEditable(value);
	}

	@Override
	public TableCellEditor getCellEditor(int row, int column) {
		Class<?> cls = getSubjectPropertyTableModel().getProperties().get(row).getValue().getClass();
		if (cls.isAssignableFrom(NGender.class)) {
			return new DefaultCellEditor(new JComboBox(NGender.values()));
		}
		return super.getCellEditor(row, column);
	}

}
