using System;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using Neurotec.Biometrics.Standards;

namespace Neurotec.Samples
{
	public partial class OpenSubjectDialog : Form
	{
		#region Private types

		private struct ListItem
		{
			public ushort Value { get; set; }
			public string Name { get; set; }
			public override string ToString()
			{
				return Name;
			}
		}

		#endregion

		#region Public constructor

		public OpenSubjectDialog()
		{
			InitializeComponent();
		}

		#endregion

		#region Private methods

		private void ListTypes()
		{
			cbType.BeginUpdate();
			try
			{
				cbType.Items.Clear();

				ListItem selected = (ListItem)cbOwner.SelectedItem;
				var type = typeof(CbeffBdbFormatIdentifiers);
				var fields = type.GetFields(BindingFlags.Static | BindingFlags.Public).Where(x => x.Name.StartsWith(selected.Name));
				int ownerNameLength = selected.Name.Length;
				foreach (FieldInfo item in fields)
				{
					ListItem li = new ListItem
					{
						Name = item.Name.Substring(ownerNameLength),
						Value = (ushort)item.GetValue(null)
					};
					cbType.Items.Add(li);
				}

				int count = cbType.Items.Count;
				if (count > 0) cbType.SelectedIndex = 0;
				cbType.Enabled = count > 0;
			}
			finally
			{
				cbType.EndUpdate();
			}
		}

		private void ListOwners()
		{
			cbOwner.BeginUpdate();
			try
			{
				cbOwner.Items.Clear();
				var items = new object[]
					{
						new ListItem { Name = "Auto detect", Value = CbeffBiometricOrganizations.NotForUse },
						new ListItem { Name = "Neurotechnologija", Value = CbeffBiometricOrganizations.Neurotechnologija },
						new ListItem { Name = "IncitsTCM1Biometrics", Value = CbeffBiometricOrganizations.IncitsTCM1Biometrics },
						new ListItem { Name = "IsoIecJtc1SC37Biometrics", Value = CbeffBiometricOrganizations.IsoIecJtc1SC37Biometrics },
					};

				cbOwner.Items.AddRange(items);
			}
			finally
			{
				cbOwner.EndUpdate();
			}
		}

		#endregion

		#region Private events

		private void BtnBrowseClick(object sender, EventArgs e)
		{
			if (openFileDialog.ShowDialog() == DialogResult.OK)
			{
				tbFileName.Text = openFileDialog.FileName;
			}
		}

		private void CbOwnerSelectedIndexChanged(object sender, EventArgs e)
		{
			ListTypes();
		}

		private void OpenSubjectDialogLoad(object sender, EventArgs e)
		{
			ListOwners();
			cbOwner.SelectedIndex = 0;
		}

		private void OpenSubjectDialogShown(object sender, EventArgs e)
		{
			var result = openFileDialog.ShowDialog();
			if (result == DialogResult.OK)
			{
				tbFileName.Text = openFileDialog.FileName;
				btnOk.Focus();
			}
			else DialogResult = result;
		}

		#endregion

		#region Public properties

		public string FileName
		{
			get { return tbFileName.Text; }
		}

		public ushort FormatOwner
		{
			get
			{
				ListItem item = (ListItem)cbOwner.SelectedItem;
				return item.Value;
			}
		}

		public ushort FormatType
		{
			get
			{
				if (cbType.SelectedIndex != -1)
				{
					ListItem item = (ListItem)cbType.SelectedItem;
					return item.Value;
				}
				else return 0;
			}
		}

		#endregion
	}
}
