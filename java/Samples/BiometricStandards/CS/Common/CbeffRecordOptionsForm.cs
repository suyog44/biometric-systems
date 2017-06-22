using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using Neurotec.Biometrics.Standards;

namespace Neurotec.Samples
{
	public partial class CbeffRecordOptionsForm : Form
	{
		#region Private fields

		private Dictionary<string, ushort> _owners;
		private Dictionary<string, ushort> _types;

		#endregion

		#region Public properties

		public uint PatronFormat
		{
			get
			{

				try
				{
					if (rbOwnerType.Checked)
					{
						string selectedOwner = (string)cbOwners.SelectedItem;
						string selectedType = (string)cbTypes.SelectedItem;
						return BdifTypes.MakeFormat(Convert.ToUInt16(_owners[selectedOwner]), Convert.ToUInt16(_types[selectedType]));
					}
					else
					{
						return Convert.ToUInt32(txtBoxFormat.Text, 16);
					}

				}
				catch (Exception)
				{
					return 0;
				}
			}
		}

		#endregion

		#region Public constructor

		public CbeffRecordOptionsForm()
		{
			InitializeComponent();

			InitializeOwners();
			InitializeTypes();

			this.FormBorderStyle = FormBorderStyle.FixedSingle;
		}

		#endregion

		#region Private methods

		private void InitializeOwners()
		{
			_owners = new Dictionary<string, ushort>();

			FieldInfo[] infos = typeof(CbeffBiometricOrganizations).GetFields();
			List<FieldInfo> sortedInfos = infos.ToList();
			sortedInfos.Sort((i1, i2) => i1.Name.CompareTo(i2.Name));

			foreach (FieldInfo info in sortedInfos)
			{
				_owners.Add(info.Name, (ushort)info.GetValue(null));
			}

			foreach (string item in _owners.Keys)
			{
				cbOwners.Items.Add(item);
			}
		}

		private void InitializeTypes()
		{
			_types = new Dictionary<string, ushort>();

			FieldInfo[] infos = typeof(CbeffPatronFormatIdentifiers).GetFields();
			List<FieldInfo> sortedInfos = infos.ToList();
			sortedInfos.Sort((i1, i2) => i1.Name.CompareTo(i2.Name));

			foreach (FieldInfo info in sortedInfos)
			{
				_types.Add(info.Name, (ushort)info.GetValue(null));
			}

			foreach (string item in _types.Keys)
			{
				cbTypes.Items.Add(item);
			}
		}

		#endregion

		#region Private form methods

		private void rbUseFormat_CheckedChanged(object sender, EventArgs e)
		{
			rbOwnerType.Checked = !rbUseFormat.Checked;
			txtBoxFormat.Enabled = rbUseFormat.Checked;
		}

		private void rbOwnerType_CheckedChanged(object sender, EventArgs e)
		{
			rbUseFormat.Checked = !rbOwnerType.Checked;
			cbOwners.Enabled = rbOwnerType.Checked;
			cbTypes.Enabled = rbOwnerType.Checked;
		}

		#endregion
	}
}
