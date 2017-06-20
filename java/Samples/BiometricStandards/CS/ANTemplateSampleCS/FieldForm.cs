using System;
using System.Text;
using System.Windows.Forms;
using Neurotec.Biometrics.Standards;

namespace Neurotec.Samples
{
	public partial class FieldForm : Form
	{
		#region Public static methods

		public static void GetFieldValue(ANField field, StringBuilder value)
		{
			value.Length = 0;
			bool manySubFields = field.SubFields.Count > 1;
			int sfi = 0;
			foreach (ANSubField subField in field.SubFields)
			{
				if (sfi != 0) value.Append(',');
				if (manySubFields) value.Append('{');
				int ii = 0;
				foreach (string item in subField.Items)
				{
					if (ii != 0) value.Append('|');
					value.Append(item);
					ii++;
				}
				if (manySubFields) value.Append('}');
				sfi++;
			}
		}

		#endregion

		#region Private fields

		private ANField _field;
		private bool _isReadOnly;
		private bool _isModified;

		#endregion

		#region Public constructor

		public FieldForm()
		{
			InitializeComponent();

			OnFieldChanged();
		}

		#endregion

		#region Private methods

		private void UpdateField()
		{
			if (_field != null)
			{
				StringBuilder value = new StringBuilder();
				GetFieldValue(_field, value);
				fieldValueLabel.Text = value.ToString();
			}
			else
			{
				fieldValueLabel.Text = string.Empty;
			}
			editFieldButton.Enabled = _field != null && _field.SubFields.Count == 1 && _field.SubFields[0].Items.Count == 1;
			_isModified = true;
		}

		private void OnFieldChanged()
		{
			subFieldListBox.BeginUpdate();
			subFieldListBox.Items.Clear();
			if (_field != null)
			{
				StringBuilder value = new StringBuilder();
				foreach (ANSubField subField in _field.SubFields)
				{
					GetSubFieldValue(subField, value);
					subFieldListBox.Items.Add(value.ToString());
				}
				subFieldListBox.SelectedIndex = 0;
			}
			subFieldListBox.EndUpdate();
			UpdateField();
			_isModified = false;
		}

		private void EditField()
		{
			ItemForm form = new ItemForm();
			form.Value = _field.Value;
			form.Text = "Edit Field";
			form.IsReadOnly = _isReadOnly;
			if (form.ShowDialog() == DialogResult.OK)
			{
				_field.Value = form.Value;
				UpdateSelectedSubField(); // couse we need to update subfield also
			}
		}

		private void OnIsReadOnlyChanged()
		{
			UpdateSubFieldControls();
			UpdateItemControls();
		}

		private ANSubField GetSelectedSubField()
		{
			return subFieldListBox.SelectedIndices.Count == 1 ? _field.SubFields[subFieldListBox.SelectedIndex] : null;
		}

		private void GetSubFieldValue(ANSubField subField, StringBuilder value)
		{
			value.Length = 0;
			int ii = 0;
			foreach (string item in subField.Items)
			{
				if (ii != 0) value.Append('|');
				value.Append(item);
				ii++;
			}
		}

		private void OnSelectedSubFieldChanged()
		{
			ANSubField selectedSubField = GetSelectedSubField();
			itemListBox.BeginUpdate();
			itemListBox.Items.Clear();
			if (selectedSubField != null)
			{
				foreach (string item in selectedSubField.Items)
				{
					itemListBox.Items.Add(item);
				}
				itemListBox.SelectedIndex = 0;
			}
			itemListBox.EndUpdate();
			UpdateSubFieldControls();
		}

		private void UpdateSubFieldControls()
		{
			ANSubField selectedSubField = GetSelectedSubField();
			addSubFieldButton.Enabled = _field != null && !_isReadOnly;
			insertSubFieldButton.Enabled = !_isReadOnly && selectedSubField != null;
			int sc = subFieldListBox.SelectedIndices.Count;
			removeSubFieldButton.Enabled = !_isReadOnly && sc != 0 && sc != _field.SubFields.Count;
			editSubFieldButton.Enabled = selectedSubField != null && selectedSubField.Items.Count == 1;
		}

		private void UpdateSubField(int index)
		{
			StringBuilder subFieldValue = new StringBuilder();
			GetSubFieldValue(_field.SubFields[index], subFieldValue);
			subFieldListBox.Items[index] = subFieldValue.ToString();
			UpdateField();
		}

		private void UpdateSelectedSubField()
		{
			UpdateSubField(subFieldListBox.SelectedIndex);
		}

		private void EditSubField()
		{
			ANSubField selectedSubField = GetSelectedSubField();
			ItemForm form = new ItemForm();
			form.Value = selectedSubField.Value;
			form.Text = "Edit Subfield";
			form.IsReadOnly = _isReadOnly;
			if (form.ShowDialog() == DialogResult.OK)
			{
				selectedSubField.Value = form.Value;
				UpdateSelectedSubField();
			}
		}

		private int GetSelectedItemIndex()
		{
			return itemListBox.SelectedIndices.Count == 1 ? itemListBox.SelectedIndex : -1;
		}

		private void OnSelectedItemChanged()
		{
			UpdateItemControls();
		}

		private void UpdateItemControls()
		{
			ANSubField selectedSubField = GetSelectedSubField();
			int selectedItemIndex = GetSelectedItemIndex();
			addItemButton.Enabled = selectedSubField != null && !_isReadOnly;
			insertItemButton.Enabled = !_isReadOnly && selectedItemIndex != -1;
			int sc = itemListBox.SelectedIndices.Count;
			removeItemButton.Enabled = selectedSubField != null && !_isReadOnly && sc != 0 && sc != selectedSubField.Items.Count;
			editItemButton.Enabled = selectedItemIndex != -1;
		}

		private void EditItem()
		{
			ANSubField selectedSubField = GetSelectedSubField();
			int selectedItemIndex = GetSelectedItemIndex();
			ItemForm form = new ItemForm();
			form.Value = selectedSubField.Items[selectedItemIndex];
			form.IsReadOnly = _isReadOnly;
			if (form.ShowDialog() == DialogResult.OK)
			{
				selectedSubField.Items[selectedItemIndex] = form.Value;
				UpdateSelectedSubField();
				itemListBox.SelectedIndex = -1;
				itemListBox.SelectedIndex = selectedItemIndex;
			}
		}

		#endregion

		#region Public properties

		public ANField Field
		{
			get
			{
				return _field;
			}
			set
			{
				if (_field != value)
				{
					_field = value;
					OnFieldChanged();
				}
			}
		}

		public bool IsModified
		{
			get
			{
				return _isModified;
			}
		}

		public bool IsReadOnly
		{
			get
			{
				return _isReadOnly;
			}
			set
			{
				if (_isReadOnly != value)
				{
					_isReadOnly = value;
					OnIsReadOnlyChanged();
				}
			}
		}

		#endregion

		#region Private form events

		private void FieldValueLabelDoubleClick(object sender, EventArgs e)
		{
			if (_field != null && _field.SubFields.Count == 1 && _field.SubFields[0].Items.Count == 1)
			{
				EditField();
			}
		}

		private void EditFieldButtonClick(object sender, EventArgs e)
		{
			EditField();
		}

		private void SubFieldListBoxSelectedIndexChanged(object sender, EventArgs e)
		{
			OnSelectedSubFieldChanged();
		}

		private void SubFieldListBoxDoubleClick(object sender, EventArgs e)
		{
			ANSubField selectedSubField = GetSelectedSubField();
			if (selectedSubField != null && selectedSubField.Items.Count == 1)
			{
				EditSubField();
			}
		}

		private void AddSubFieldButtonClick(object sender, EventArgs e)
		{
			ItemForm form = new ItemForm();
			form.Text = "Add Subfield";
			if (form.ShowDialog() == DialogResult.OK)
			{
				_field.SubFields.Add(form.Value);
				subFieldListBox.Items.Add(string.Empty);
				int index = _field.SubFields.Count - 1;
				UpdateSubField(index);
				subFieldListBox.SelectedIndex = -1;
				subFieldListBox.SelectedIndex = index;
			}
		}

		private void InsertSubFieldButtonClick(object sender, EventArgs e)
		{
			int index = subFieldListBox.SelectedIndex;
			ItemForm form = new ItemForm();
			form.Text = "Insert Subfield";
			if (form.ShowDialog() == DialogResult.OK)
			{
				_field.SubFields.Insert(index, form.Value);
				subFieldListBox.Items.Insert(index, string.Empty);
				UpdateSubField(index);
				subFieldListBox.SelectedIndex = -1;
				subFieldListBox.SelectedIndex = index;
			}
		}

		private void RemoveSubFieldButtonClick(object sender, EventArgs e)
		{
			subFieldListBox.BeginUpdate();
			int selCount = subFieldListBox.SelectedIndices.Count;
			int[] selIndices = new int[selCount];
			subFieldListBox.SelectedIndices.CopyTo(selIndices, 0);
			Array.Sort<int>(selIndices);
			for (int i = selCount - 1; i >= 0; i--)
			{
				int index = selIndices[i];
				_field.SubFields.RemoveAt(index);
				subFieldListBox.Items.RemoveAt(index);
			}
			UpdateField();
			subFieldListBox.SelectedIndex = -1;
			subFieldListBox.SelectedIndex = selIndices[0] == _field.SubFields.Count ? _field.SubFields.Count - 1 : selIndices[0];
			subFieldListBox.EndUpdate();
		}

		private void EditSubFieldButtonClick(object sender, EventArgs e)
		{
			EditSubField();
		}

		private void ItemListBoxSelectedIndexChanged(object sender, EventArgs e)
		{
			OnSelectedItemChanged();
		}

		private void ItemListBoxDoubleClick(object sender, EventArgs e)
		{
			if (GetSelectedItemIndex() != -1)
			{
				EditItem();
			}
		}

		private void AddItemButtonClick(object sender, EventArgs e)
		{
			ANSubField selectedSubField = GetSelectedSubField();
			ItemForm form = new ItemForm();
			form.Text = "Add Item";
			if (form.ShowDialog() == DialogResult.OK)
			{
				int index = selectedSubField.Items.Add(form.Value);
				UpdateSelectedSubField();
				itemListBox.SelectedIndex = -1;
				itemListBox.SelectedIndex = index;
			}
		}

		private void InsertItemButtonClick(object sender, EventArgs e)
		{
			ANSubField selectedSubField = GetSelectedSubField();
			ItemForm form = new ItemForm();
			form.Text = "Insert Item";
			if (form.ShowDialog() == DialogResult.OK)
			{
				int index = GetSelectedItemIndex();
				selectedSubField.Items.Insert(index, form.Value);
				UpdateSelectedSubField();
				itemListBox.SelectedIndex = -1;
				itemListBox.SelectedIndex = index;
			}
		}

		private void RemoveItemButtonClick(object sender, EventArgs e)
		{
			itemListBox.BeginUpdate();
			ANSubField selectedSubField = GetSelectedSubField();
			int selCount = itemListBox.SelectedIndices.Count;
			int[] selIndices = new int[selCount];
			itemListBox.SelectedIndices.CopyTo(selIndices, 0);
			Array.Sort<int>(selIndices);
			for (int i = selCount - 1; i >= 0; i--)
			{
				int index = selIndices[i];
				selectedSubField.Items.RemoveAt(index);
			}
			UpdateSelectedSubField();
			itemListBox.SelectedIndex = -1;
			itemListBox.SelectedIndex = selIndices[0] == selectedSubField.Items.Count ? selectedSubField.Items.Count - 1 : selIndices[0];
			itemListBox.EndUpdate();
		}

		private void EditItemButtonClick(object sender, EventArgs e)
		{
			EditItem();
		}

		private void FieldFormShown(object sender, EventArgs e)
		{
			if (_field != null && _field.SubFields.Count == 1 && _field.SubFields[0].Items.Count == 1 && !IsReadOnly)
			{
				EditField();
			}
		}

		#endregion
	}
}
