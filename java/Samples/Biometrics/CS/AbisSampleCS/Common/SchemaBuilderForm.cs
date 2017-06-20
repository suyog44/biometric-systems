using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Neurotec.Biometrics;

namespace Neurotec.Samples
{
	public partial class SchemaBuilderForm : Form
	{
		#region Private types

		private enum ColumnType
		{
			Unknown = 0,
			BiographicDataString,
			BiographicDataInteger,
			Gender,
			Thumbnail,
			EnrollData,
			CustomDataString,
			CustomDataInteger,
			CustomDataBlob,
		};

		#endregion

		#region Public constructor

		public SchemaBuilderForm()
		{
			InitializeComponent();

			foreach (var item in (ColumnType[])Enum.GetValues(typeof(ColumnType)))
			{
				if (item != ColumnType.Unknown)
					cbType.Items.Add(item);
			}
			cbType.SelectedIndex = 0;
		}

		#endregion

		#region Private fields

		private SampleDbSchema _schema;

		#endregion

		#region Public properties

		public SampleDbSchema Schema { get; set; }

		#endregion

		#region Private methods

		private ColumnType GetColumnType(NBiographicDataElement element)
		{
			if (element.Name == _schema.GenderDataName)
				return ColumnType.Gender;
			else if (element.Name == _schema.EnrollDataName)
				return ColumnType.EnrollData;
			else if (element.Name == _schema.ThumbnailDataName)
				return ColumnType.Thumbnail;
			else
			{
				bool isCustom = _schema.CustomData.Elements.Contains(element);
				if (element.DbType == NDBType.String) return isCustom ? ColumnType.CustomDataString : ColumnType.BiographicDataString;
				if (element.DbType == NDBType.Integer) return isCustom ? ColumnType.CustomDataInteger : ColumnType.BiographicDataInteger;
				if (element.DbType == NDBType.Blob && isCustom) return ColumnType.CustomDataBlob;
			}
			return ColumnType.Unknown;
		}

		private ListViewGroup GetGroup(ColumnType type)
		{
			switch (type)
			{
				case ColumnType.BiographicDataString:
				case ColumnType.BiographicDataInteger:
					return listView.Groups["lvgBiographicData"];
				case ColumnType.Gender:
				case ColumnType.Thumbnail:
				case ColumnType.EnrollData:
					return listView.Groups["lvgSampleData"];
				case ColumnType.CustomDataString:
				case ColumnType.CustomDataInteger:
				case ColumnType.CustomDataBlob:
					return listView.Groups["lvgCustomData"];
				default:
					return null;
			}
		}

		private void AddElement(NBiographicDataElement element)
		{
			ColumnType type = GetColumnType(element);
			string typeString = type.ToString();
			if (type == ColumnType.Gender)
				typeString += " (String)";
			else if (type == ColumnType.Thumbnail || type == ColumnType.EnrollData)
				typeString += "(Blob)";
			ListViewItem lvi = new ListViewItem(typeString);
			lvi.SubItems.Add(element.Name);
			lvi.SubItems.Add(element.DbColumn);
			lvi.Group = GetGroup(type);
			lvi.Tag = element;
			listView.Items.Add(lvi);
		}

		private bool CheckNameDoesNotConflict(string name)
		{
			var type = typeof(NSubject);
			var properties = type.GetProperties();
			return properties.FirstOrDefault(p => p.Name == name) == null;
		}

		#endregion

		#region Private form events

		private void SchemaBuilderFormLoad(object sender, EventArgs e)
		{
			if (Schema == null || Schema.IsEmpty) throw new ArgumentNullException("Schema");

			_schema = new SampleDbSchema();
			_schema.SchemaName = Schema.SchemaName;
			_schema.EnrollDataName = Schema.EnrollDataName;
			_schema.ThumbnailDataName = Schema.ThumbnailDataName;
			_schema.GenderDataName = Schema.GenderDataName;
			_schema.BiographicData = NBiographicDataSchema.Parse(Schema.BiographicData.ToString());
			_schema.CustomData = NBiographicDataSchema.Parse((Schema.CustomData ?? new NBiographicDataSchema()).ToString());

			foreach (var element in _schema.BiographicData.Elements)
			{
				AddElement(element);
			}
			foreach (var element in _schema.CustomData.Elements)
			{
				AddElement(element);
			}
		}

		private void BtnAddClick(object sender, EventArgs e)
		{
			string name = tbName.Text.Trim();
			string dbColumn = tbDbColumn.Text.Trim();
			if (name == string.Empty)
			{
				Utilities.ShowInformation("Name field can not be empty");
				tbName.Focus();
			}
			else if (!CheckNameDoesNotConflict(name))
			{
				Utilities.ShowInformation("Name can not be same as NSubject property name");
				tbName.Focus();
			}
			else
			{
				ColumnType type = (ColumnType)cbType.SelectedItem;
				bool isCustom = type != ColumnType.BiographicDataInteger && type != ColumnType.BiographicDataString && type != ColumnType.Gender;
				NBiographicDataElement element = new NBiographicDataElement();
				element.Name = name;
				element.DbColumn = dbColumn;
				if (type == ColumnType.CustomDataBlob || type == ColumnType.EnrollData || type == ColumnType.Thumbnail)
					element.DbType = NDBType.Blob;
				else if (type == ColumnType.BiographicDataInteger || type == ColumnType.CustomDataInteger)
					element.DbType = NDBType.Integer;
				else
					element.DbType = NDBType.String;

				var elements = Enumerable.Union(_schema.BiographicData.Elements, _schema.CustomData.Elements).ToList();
				if (elements.Exists(x => string.Compare(x.Name, element.Name, true) == 0 || string.Compare(x.DbColumn, element.Name) == 0))
				{
					Utilities.ShowInformation("Item with same name or db column name already exists");
					tbName.Focus();
					return;
				}
				else if (!string.IsNullOrEmpty(element.DbColumn))
				{
					if (elements.Exists(x => string.Compare(x.DbColumn, element.DbColumn, true) == 0) ||
						elements.Exists(x => string.Compare(x.Name, element.DbColumn, true) == 0))
					{
						Utilities.ShowInformation("Item with same name or column name already exists");
						tbDbColumn.Focus();
						return;
					}
				}
				else
				{
					if (type == ColumnType.Gender && _schema.GenderDataName != null)
					{
						Utilities.ShowInformation("Gender field already exists");
						return;
					}
					else if (type == ColumnType.EnrollData && _schema.EnrollDataName != null)
					{
						Utilities.ShowInformation("Enroll data field already exists");
						return;
					}
					else if (type == ColumnType.Thumbnail && _schema.ThumbnailDataName != null)
					{
						Utilities.ShowInformation("Thumbnail data field already exists");
						return;
					}
				}

				if (isCustom)
					_schema.CustomData.Elements.Add(element);
				else
					_schema.BiographicData.Elements.Add(element);

				if (type == ColumnType.Gender)
					_schema.GenderDataName = element.Name;
				else if (type == ColumnType.Thumbnail)
					_schema.ThumbnailDataName = element.Name;
				else if (type == ColumnType.EnrollData)
					_schema.EnrollDataName = element.Name;

				AddElement(element);

				tbDbColumn.Text = string.Empty;
				tbName.Text = string.Empty;
			}
		}

		private void BtnOkClick(object sender, EventArgs e)
		{
			Schema = _schema;
			DialogResult = DialogResult.OK;
		}

		private void ListViewSelectedIndexChanged(object sender, EventArgs e)
		{
			btnDelete.Enabled = listView.SelectedItems.Count > 0;
		}

		private void BtnDeleteClick(object sender, EventArgs e)
		{
			ListViewItem selected = listView.SelectedItems[0];
			NBiographicDataElement element = (NBiographicDataElement)selected.Tag;
			_schema.BiographicData.Elements.Remove(element);
			_schema.CustomData.Elements.Remove(element);
			if (element.Name == _schema.GenderDataName)
				_schema.GenderDataName = null;
			else if (element.Name == _schema.ThumbnailDataName)
				_schema.ThumbnailDataName = null;
			else if (element.Name == _schema.EnrollDataName)
				_schema.EnrollDataName = null;
			selected.Remove();
		}

		#endregion
	}
}
