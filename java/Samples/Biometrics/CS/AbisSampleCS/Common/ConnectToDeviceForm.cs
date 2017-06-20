using System;
using System.Windows.Forms;
using Neurotec.ComponentModel;
using Neurotec.Devices;
using Neurotec.Plugins;

namespace Neurotec.Samples
{
	public partial class ConnectToDeviceForm : Form
	{
		#region Private fields

		private NParameterDescriptor[] _parameters;

		#endregion

		#region Public constructor

		public ConnectToDeviceForm()
		{
			InitializeComponent();

			pluginComboBox.BeginUpdate();
			foreach (NPlugin plugin in NDeviceManager.PluginManager.Plugins)
			{
				if (plugin.State == NPluginState.Plugged && NDeviceManager.IsConnectToDeviceSupported(plugin))
				{
					pluginComboBox.Items.Add(plugin);
				}
			}
			if (pluginComboBox.Items.Count != 0)
			{
				pluginComboBox.SelectedIndex = 0;
			}
			else OnSelectedPluginChanged();
			pluginComboBox.EndUpdate();
		}

		#endregion

		#region Private methods

		private void OnSelectedPluginChanged()
		{
			NPlugin plugin = SelectedPlugin;
			_parameters = plugin == null ? null : NDeviceManager.GetConnectToDeviceParameters(plugin);
			propertyGrid.SelectedObject = _parameters == null ? null : new NParameterBag(_parameters);
			btnOK.Enabled = propertyGrid.Enabled = plugin != null;
		}

		#endregion

		#region Public properties

		public NPlugin SelectedPlugin
		{
			get
			{
				return (NPlugin)pluginComboBox.SelectedItem;
			}
			set
			{
				if (pluginComboBox.Items.Contains(value))
				{
					pluginComboBox.SelectedItem = value;
				}
			}
		}

		public NPropertyBag Parameters
		{
			get
			{
				var parameterBag = (NParameterBag)propertyGrid.SelectedObject;
				return parameterBag == null ? null : parameterBag.ToPropertyBag();
			}
			set
			{
				var parameterBag = (NParameterBag)propertyGrid.SelectedObject;
				if (parameterBag != null)
				{
					parameterBag.Apply(value, true);
				}
			}
		}

		#endregion

		private void pluginComboBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			OnSelectedPluginChanged();
		}

		private void ConnectToDeviceForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (DialogResult != DialogResult.OK) return;
			if (_parameters != null)
			{
				var parameterBag = (NParameterBag)propertyGrid.SelectedObject;
				for (int i = 0; i < _parameters.Length; i++)
				{
					if ((_parameters[i].Attributes & NAttributes.Optional) != NAttributes.Optional)
					{
						if (parameterBag.Values[i] == null)
						{
							MessageBox.Show(string.Format("{0} value not specified", _parameters[i].Name), Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
							DialogResult = DialogResult.None;
							e.Cancel = true;
						}
					}
				}
			}
		}
	}
}
