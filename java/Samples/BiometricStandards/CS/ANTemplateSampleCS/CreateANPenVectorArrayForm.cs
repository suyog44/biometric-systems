using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Neurotec.Biometrics.Standards;

namespace Neurotec.Samples
{
	public partial class CreateANPenVectorArrayForm : Form
	{
		#region Public constructor

		public CreateANPenVectorArrayForm()
		{
			InitializeComponent();
		}

		#endregion

		#region Private fields

		private ANPenVector[] _vectors;

		#endregion

		#region Public constructors

		public ANPenVector[] Vectors
		{
			get { return _vectors; }
			set
			{
				_vectors = value;
				dataGridView.Rows.Clear();
				foreach (ANPenVector item in Vectors)
				{
					dataGridView.Rows.Add(item.X, item.Y, item.Pressure);
				}
			}
		}

		#endregion

		#region Private form events

		private void DataGridViewCellEndEdit(object sender, DataGridViewCellEventArgs e)
		{
			DataGridViewRow row = dataGridView.Rows[e.RowIndex];
			if (row.IsNewRow) return;
			DataGridViewCell cell = row.Cells[e.ColumnIndex];
			object cellValue = cell.FormattedValue;
			bool isValid = cellValue != null;
			if (isValid)
			{
				bool isPressure = e.ColumnIndex == 2;
				if (isPressure)
				{
					byte value;
					isValid = byte.TryParse(cellValue.ToString(), out value);
				}
				else
				{
					ushort value;
					isValid = ushort.TryParse(cellValue.ToString(), out value);
				}
			}

			cell.Style.BackColor = isValid ? Color.White : Color.Red;
		}

		private void BtnOkClick(object sender, EventArgs e)
		{
			bool isOk = true;
			List<ANPenVector> list = new List<ANPenVector>();
			foreach (DataGridViewRow row in dataGridView.Rows)
			{
				if (row.IsNewRow) continue;

				ushort x, y;
				byte pressure;
				if (!GetValue(row.Cells[0].Value, out x))
				{
					isOk = false;
					row.Cells[0].Style.BackColor = Color.Red;
				}
				if (!GetValue(row.Cells[1].Value, out y))
				{
					isOk = false;
					row.Cells[1].Style.BackColor = Color.Red;
				}
				if (!GetValue(row.Cells[2].Value, out pressure))
				{
					isOk = false;
					row.Cells[2].Style.BackColor = Color.Red;
				}
				if (!isOk) break;
				list.Add(new ANPenVector(x, y, pressure));
			}

			if (isOk)
			{
				Vectors = list.ToArray();
				DialogResult = DialogResult.OK;
			}
		}

		#endregion

		#region Private methods

		private bool GetValue(object cellValue, out ushort value)
		{
			value = 0;
			if (cellValue == null) return false;
			return ushort.TryParse(cellValue.ToString(), out value);
		}

		private bool GetValue(object cellValue, out byte value)
		{
			value = 0;
			if (cellValue == null) return false;
			return byte.TryParse(cellValue.ToString(), out value);
		}

		#endregion
	}
}
