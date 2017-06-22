using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using Neurotec.Samples.Properties;

namespace Neurotec.Samples.Forms
{
	public partial class EditInfoForm : Form
	{
		#region Public constructor

		public EditInfoForm()
		{
			InitializeComponent();

			_enrollToServerColumn = new DataGridViewCheckBoxColumn();
			_enrollToServerColumn.HeaderText = @"Enroll to server";
			_enrollToServerColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
		}

		#endregion

		#region Private fields

		private bool _suspendEvents;
		private readonly DataGridViewCheckBoxColumn _enrollToServerColumn;
		private InfoField [] _information;

		#endregion

		#region Public properties

		public InfoField [] Information
		{
			get { return _information; }
			set { _information = value; }
		}

		#endregion

		#region Private form events

		private void EditInfoFormLoad(object sender, EventArgs e)
		{
			_suspendEvents = true;

			dataGridView.Rows.Clear();
			foreach (InfoField item in Information)
			{
				dataGridView.Rows.Add(item.Key, item.IsEditable);
			}

			UpdateComboxes();
			cbThumbnailField.SelectedItem = Settings.Default.InformationThumbnailField;
			_suspendEvents = false;
		}

		private void BtnUpClick(object sender, EventArgs e)
		{
			DataGridViewSelectedRowCollection rows = dataGridView.SelectedRows;
			if (rows.Count > 0)
			{
				DataGridViewRow row = rows[0];
				if (row.Index != 0)
				{
					_suspendEvents = true;

					int index = row.Index;
					dataGridView.Rows.RemoveAt(index--);
					dataGridView.Rows.Insert(index, row);
					dataGridView.ClearSelection();
					dataGridView.Rows[index].Selected = true;

					_suspendEvents = false;
				}
			}
		}

		private void BtnDownClick(object sender, EventArgs e)
		{
			DataGridViewSelectedRowCollection rows = dataGridView.SelectedRows;
			if (rows.Count > 0)
			{
				DataGridViewRow row = rows[0];
				if (row.Index != dataGridView.RowCount - 2)
				{
					_suspendEvents = true;

					int index = row.Index;
					dataGridView.Rows.RemoveAt(index++);
					dataGridView.Rows.Insert(index, row);
					dataGridView.ClearSelection();
					dataGridView.Rows[index].Selected = true;

					_suspendEvents = false;
				}
			}
		}

		private void DataGridViewRowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
		{
			if (_suspendEvents) return;
			UpdateComboxes();
		}

		private void DataGridViewRowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
		{
			if (_suspendEvents) return;
			UpdateComboxes();
		}

		private void DataGridViewCellEndEdit(object sender, DataGridViewCellEventArgs e)
		{
			if (e.ColumnIndex == 0)
			{
				int thumbnail = cbThumbnailField.SelectedIndex;
				UpdateComboxes();
				if (thumbnail - 1 == e.RowIndex) cbThumbnailField.SelectedIndex = thumbnail;
			}
		}

		private void BtnDeleteClick(object sender, EventArgs e)
		{
			DataGridViewSelectedRowCollection rows = dataGridView.SelectedRows;
			if (rows.Count > 0)
			{
				dataGridView.Rows.RemoveAt(rows[0].Index);
			}
		}

		private void DataGridViewCellValueChanged(object sender, DataGridViewCellEventArgs e)
		{
			if (_suspendEvents) return;

			if (e.ColumnIndex == 0 && e.RowIndex >= 0)
			{
				int thumbnail = cbThumbnailField.SelectedIndex;
				UpdateComboxes();
				if (thumbnail - 1 == e.RowIndex) cbThumbnailField.SelectedIndex = thumbnail;
			}
		}

		private void ComboBoxSelectedIndexChanged(object sender, EventArgs e)
		{
			ComboBox combo = (ComboBox)sender;
			if (dataGridView.ColumnCount > 1)
			{
				dataGridView.Rows[combo.SelectedIndex].Cells[1].Value = true;
			}
		}

		private void BtnOkClick(object sender, EventArgs e)
		{
			foreach (DataGridViewRow row in dataGridView.Rows)
			{
				if (!row.IsNewRow && string.IsNullOrEmpty(row.Cells[0].Value as string))
				{
					Utilities.ShowError("Key value is invalid");
					dataGridView.ClearSelection();
					row.Selected = true;
					return;
				}
			}

			if (dataGridView.Rows.Count <= 1)
			{
				Utilities.ShowError("Create at least one row of information description");
				return;
			}

			string thumbnail = cbThumbnailField.SelectedItem as string;
			Settings.Default.InformationThumbnailField = thumbnail;

			List<InfoField> fields = new List<InfoField>();
			StringBuilder builder = new StringBuilder();
			foreach (DataGridViewRow row in dataGridView.Rows)
			{
				if (row.IsNewRow) continue;

				InfoField inf = new InfoField();
				inf.Key = row.Cells[0].Value as string;
				inf.ShowAsThumbnail = thumbnail == inf.Key;
				inf.IsEditable = !inf.ShowAsThumbnail;
				if (inf.Key != null) inf.Key = inf.Key.Trim();

				fields.Add(inf);
				builder.AppendFormat("{0};", inf);
			}

			Settings.Default.Information = builder.ToString();
			Settings.Default.Save();

			_information = fields.ToArray();

			DialogResult = DialogResult.OK;
		}

		#endregion

		#region Private methods

		private void UpdateComboxes()
		{
			UpdateComboBox(cbThumbnailField, true);
		}

		private void UpdateComboBox(ComboBox combo, bool allowEmpty)
		{
			combo.BeginUpdate();
			try
			{
				string selected = combo.SelectedItem as string;
				combo.Items.Clear();
				if (allowEmpty) combo.Items.Add(string.Empty);
				foreach (DataGridViewRow row in dataGridView.Rows)
				{
					if (row.Cells[0].Value != null)
					{
						combo.Items.Add(row.Cells[0].Value);
					}
				}
				combo.SelectedItem = selected;
			}
			finally
			{
				combo.EndUpdate();
			}
		}

		#endregion
	}
}
