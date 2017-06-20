using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Neurotec.Biometrics;
using Neurotec.Devices;
using Neurotec.Devices.Virtual;
using Neurotec.Gui;
using Neurotec.Media;
using Neurotec.Samples.Properties;
using System.Collections.Specialized;

namespace Neurotec.Samples
{
	public partial class MainForm : Form
	{
		#region Private constants

		private const string AppName = "Device Manager";

		#endregion

		#region Private fields

		private NDeviceManager _deviceManager;
		private readonly List<CaptureForm> _captureForms = new List<CaptureForm>();
		private List<NFPosition> biometricDevicePositionList = new List<NFPosition>();

		#endregion

		#region Public constructor

		public MainForm()
		{
			InitializeComponent();

			aboutToolStripMenuItem.Text = '&' + AboutBox.Name;

			OnDeviceManagerChanged();
		}

		#endregion

		#region Private methods

		private void Log(string str)
		{
			logRichTextBox.AppendText(str);
			logRichTextBox.AppendText(Environment.NewLine);
			logRichTextBox.ScrollToCaret();
		}

		private void Log(string format, params object[] args)
		{
			Log(string.Format(format, args));
		}

		private void UpdateDeviceList()
		{
			deviceTreeView.BeginUpdate();
			try
			{
				deviceTreeView.Nodes.Clear();
				if (_deviceManager != null)
				{
					foreach (NDevice device in _deviceManager.Devices.ToArray())
					{
						if (device.Parent == null)
						{
							FoundDevice(deviceTreeView.Nodes, device);
						}
					}
				}
				if (deviceTreeView.Nodes.Count != 0)
				{
					deviceTreeView.SelectedNode = deviceTreeView.Nodes[0];
				}
				else
				{
					try
					{
						OnSelectedDeviceChanged();
					}
					catch (Exception ex)
					{
						MessageBox.Show(ex.ToString(), @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
					}
				}
			}
			finally
			{
				deviceTreeView.EndUpdate();
			}
		}

		private void OnDeviceManagerChanged()
		{
			UpdateDeviceList();
			if (_deviceManager != null)
			{
				_deviceManager.Devices.CollectionChanged += deviceManager_devices_CollectionChanged;
			}
			closeToolStripMenuItem.Enabled = _deviceManager != null;
			connectToolStripMenuItem.Enabled = _deviceManager != null;
			Text = _deviceManager == null ? AppName : string.Format("{0} (Device types: {1})", AppName, _deviceManager.DeviceTypes);
		}

		private TreeNode GetDeviceNode(NDevice device, TreeNodeCollection nodes)
		{
			foreach (TreeNode node in nodes)
			{
				if (node.Tag.Equals(device)) return node;
				TreeNode subNode = GetDeviceNode(device, node.Nodes);
				if (subNode != null) return subNode;
			}
			return null;
		}

		private TreeNode GetDeviceNode(NDevice device)
		{
			return GetDeviceNode(device, deviceTreeView.Nodes);
		}

		private TreeNode CreateDeviceNode(NDevice device)
		{
			return new TreeNode(device.DisplayName) {Tag = device};
		}

		private void FoundDevice(TreeNodeCollection nodes, NDevice device)
		{
			Log("Found device: {0}", device.Id);
			TreeNode deviceTreeNode = CreateDeviceNode(device);
			nodes.Add(deviceTreeNode);
			foreach (NDevice child in device.Children)
			{
				FoundDevice(deviceTreeNode.Nodes, child);
			}
		}

		private void AddDevice(NDevice device)
		{
			Log("Added device: {0}", device.Id);
			if (GetDeviceNode(device) != null) return; // Device is already added
			TreeNode deviceTreeNode = CreateDeviceNode(device);
			if (device.Parent == null)
			{
				deviceTreeView.Nodes.Add(deviceTreeNode);
			}
			else
			{
				TreeNode parentTreeNode = GetDeviceNode(device.Parent);
				if (parentTreeNode != null)
				{
					parentTreeNode.Nodes.Add(deviceTreeNode);
				}
			}
			foreach (NDevice child in device.Children)
			{
				TreeNode childTreeNode = GetDeviceNode(child);
				if (childTreeNode != null)
				{
					(childTreeNode.Parent == null ? deviceTreeView.Nodes : childTreeNode.Parent.Nodes).Remove(childTreeNode);
					deviceTreeNode.Nodes.Add(childTreeNode);
				}
			}
		}

		private void RemoveDevice(NDevice device)
		{
			foreach (CaptureForm cf in _captureForms)
				if (cf.Device == device)
					cf.WaitForCaptureToFinish();
			Log("Removed device: {0}", device.Id);
			TreeNode deviceTreeNode = GetDeviceNode(device);
			bool isSelected = deviceTreeView.SelectedNode == deviceTreeNode;
			var childTreeNodes = new TreeNode[deviceTreeNode.Nodes.Count];
			for (int i = deviceTreeNode.Nodes.Count - 1; i >= 0; i--)
			{
				childTreeNodes[i] = deviceTreeNode.Nodes[i];
			}
			deviceTreeNode.Nodes.Clear();
			deviceTreeView.Nodes.AddRange(childTreeNodes);
			(deviceTreeNode.Parent == null ? deviceTreeView.Nodes : deviceTreeNode.Parent.Nodes).Remove(deviceTreeNode);
			if (isSelected)
			{
				try
				{
					OnSelectedDeviceChanged();
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.ToString(), @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
		}

		private NDevice GetSelectedDevice()
		{
			return deviceTreeView.SelectedNode == null ? null : (NDevice)deviceTreeView.SelectedNode.Tag;
		}

		private void SetSelectedDevice(NDevice device)
		{
			if (device == null)
			{
				deviceTreeView.SelectedNode = null;
			}
			else
			{
				foreach (TreeNode node in deviceTreeView.Nodes)
				{
					if (device.Equals(node.Tag))
					{
						deviceTreeView.SelectedNode = node;
						break;
					}
				}
			}
		}

		private void OnSelectedDeviceChanged()
		{
			NDevice device = GetSelectedDevice();
			var captureDevice = device as NCaptureDevice;
			var camera = device as NCamera;
			var microphone = device as NMicrophone;
			var biometricDevice = device as NBiometricDevice;
			var fScanner = device as NFScanner;
			var irisScanner = device as NIrisScanner;
			bool isCaptureDevice = camera != null || microphone != null || fScanner != null || irisScanner != null;

			disconnectToolStripMenuItem.Enabled = device != null && device.IsDisconnectable;
			showPluginToolStripMenuItem.Enabled = device != null;
			typeLabel.Text = device == null ? null : string.Format("Type: {0}", device.GetType());
			devicePropertyGrid.SelectedObject = device != null && device.IsAvailable ? device : null;
			biometricDeviceImpressionTypeComboBox.Visible = fScanner != null;
			biometricDevicePositionComboBox.Visible = fScanner != null || irisScanner != null;
			llCheckBox.Visible = lrCheckBox.Visible = lmCheckBox.Visible = liCheckBox.Visible = ltCheckBox.Visible
				= rtCheckBox.Visible = riCheckBox.Visible = rmCheckBox.Visible = rrCheckBox.Visible = rlCheckBox.Visible = fScanner != null;
			tbMiliseconds.Visible = cbUseTimeout.Visible = cbAutomatic.Visible = lblMiliseconds.Visible = isCaptureDevice && biometricDevice != null;
			cbGatherImages.Visible = deviceCaptureButton.Visible = isCaptureDevice;
			formatsComboBox.Visible = customizeFormatButton.Visible = false;
			startSequenceButton.Visible = endSequenceButton.Visible = biometricDevice != null;

			if (fScanner != null)
			{
				biometricDeviceImpressionTypeComboBox.BeginUpdate();
				biometricDevicePositionComboBox.BeginUpdate();
				biometricDeviceImpressionTypeComboBox.Items.Clear();
				biometricDevicePositionComboBox.Items.Clear();
				biometricDevicePositionList.Clear();			// To make sure list is not filled with supported positions of previously selected device
				try
				{
					if (fScanner.IsAvailable)
					{
						foreach (NFImpressionType impressionType in fScanner.GetSupportedImpressionTypes())
						{
							biometricDeviceImpressionTypeComboBox.Items.Add(impressionType);
						}
						if (biometricDeviceImpressionTypeComboBox.Items.Count != 0) biometricDeviceImpressionTypeComboBox.SelectedIndex = 0;
						foreach (NFPosition position in fScanner.GetSupportedPositions())
						{
							biometricDevicePositionList.Add(position);
						}
						ReformDevicePositionComboBox();
					}
				}
				finally // because it may become unavailable in process
				{
					biometricDeviceImpressionTypeComboBox.EndUpdate();
					biometricDevicePositionComboBox.EndUpdate();
				}
			}
			else if (irisScanner != null)
			{
				biometricDevicePositionComboBox.BeginUpdate();
				biometricDevicePositionComboBox.Items.Clear();
				try
				{
					if (irisScanner.IsAvailable)
					{
						foreach (NEPosition position in irisScanner.GetSupportedPositions())
						{
							biometricDevicePositionComboBox.Items.Add(position);
						}
						if (biometricDevicePositionComboBox.Items.Count != 0) biometricDevicePositionComboBox.SelectedIndex = 0;
					}
				}
				finally // because it may become unavailable in process
				{
					biometricDevicePositionComboBox.EndUpdate();
				}
			}

			if (captureDevice != null)
			{
				formatsComboBox.BeginUpdate();
				formatsComboBox.Items.Clear();
				try
				{
					if (captureDevice.IsAvailable)
					{
						foreach (NMediaFormat format in captureDevice.GetFormats())
						{
							formatsComboBox.Items.Add(format);
						}
						NMediaFormat currentFormat = captureDevice.GetCurrentFormat();
						if (currentFormat != null)
						{
							int formatIndex = formatsComboBox.Items.IndexOf(currentFormat);
							if (formatIndex == -1)
							{
								formatsComboBox.Items.Add(currentFormat);
								formatsComboBox.SelectedIndex = formatsComboBox.Items.Count - 1;
							}
							else
							{
								formatsComboBox.SelectedIndex = formatIndex;
							}
						}
					}
				}
				finally
				{
					formatsComboBox.EndUpdate();
				}
				formatsComboBox.Visible = customizeFormatButton.Visible = true;
			}
		}

		private void SetDeviceManager(NDeviceManager value)
		{
			if (value == _deviceManager) return;
			if (_deviceManager != null)
			{
				_deviceManager.Devices.CollectionChanged -= deviceManager_devices_CollectionChanged;
				_deviceManager.Dispose();
				_deviceManager = null;
			}
			_deviceManager = value;
			OnDeviceManagerChanged();
		}

		private void CloseDeviceManager()
		{
			SetDeviceManager(null);
		}

		private void NewDeviceManager()
		{
			Settings settings = Settings.Default;
			var form = new DeviceManagerForm {Text = @"New Device Manager", DeviceTypes = settings.DeviceTypes, AutoPlug = settings.AutoPlug};
			if (form.ShowDialog() != DialogResult.OK) return;

			var deviceManager = new NDeviceManager {DeviceTypes = form.DeviceTypes, AutoPlug = form.AutoPlug};
			deviceManager.Initialize();

			SetDeviceManager(deviceManager);

			settings.DeviceTypes = form.DeviceTypes;
			settings.AutoPlug = form.AutoPlug;
			settings.Save();
		}

		private void ReformDevicePositionComboBox()
		{
			NFImpressionType selectedImpression = (NFImpressionType)biometricDeviceImpressionTypeComboBox.SelectedItem;

			biometricDevicePositionComboBox.BeginUpdate();
			biometricDevicePositionComboBox.Items.Clear();

			foreach (NFPosition position in biometricDevicePositionList)
			{
				if (NBiometricTypes.IsPositionCompatibleWith(position, selectedImpression)) biometricDevicePositionComboBox.Items.Add(position);
			}
			biometricDevicePositionComboBox.EndUpdate();
			if (biometricDevicePositionComboBox.Items.Count != 0) biometricDevicePositionComboBox.SelectedIndex = 0;
		}

		#endregion

		#region Private form events

		private void MainForm_Shown(object sender, EventArgs e)
		{
			NCore.ErrorSuppressed += NCore_ErrorSuppressed;
			NewDeviceManager();
		}

		private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
		{
			foreach (CaptureForm form in _captureForms.ToArray())
			{
				form.Close();
			}
			CloseDeviceManager();
			NCore.ErrorSuppressed -= NCore_ErrorSuppressed;
		}

		void NCore_ErrorSuppressed(object sender, ErrorSuppressedEventArgs ea)
		{
			BeginInvoke(new MethodInvoker(() => Log("Error suppressed: {0}", ea.Error)));
		}

		void deviceManager_devices_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
		{
			BeginInvoke(new Action<NotifyCollectionChangedEventArgs>(ea =>
				{
					switch (ea.Action)
					{
						case NotifyCollectionChangedAction.Add:
							AddDevice((NDevice)ea.NewItems[0]);
							if (deviceTreeView.SelectedNode == null) deviceTreeView.SelectedNode = deviceTreeView.Nodes[0];
							break;
						case NotifyCollectionChangedAction.Remove:
							RemoveDevice((NDevice)ea.OldItems[0]);
							break;
						case NotifyCollectionChangedAction.Reset:
							Log("Refreshing device list...");
							UpdateDeviceList();
							break;
					}
				}), e);
		}

		private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
		{
			AboutBox.Show();
		}

		private void deviceTreeView_AfterSelect(object sender, TreeViewEventArgs e)
		{
			try
			{
				OnSelectedDeviceChanged();
			}
			catch(Exception ex)
			{
				MessageBox.Show(ex.ToString(), @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void showPluginToolStripMenuItem_Click(object sender, EventArgs e)
		{
			PluginManagerBox.Show(NDeviceManager.PluginManager, GetSelectedDevice().Plugin);
		}

		private void deviceCaptureButton_Click(object sender, EventArgs e)
		{
			NDevice device = GetSelectedDevice();
			var captureDevice = device as NCaptureDevice;
			var camera = device as NCamera;
			var microphone = device as NMicrophone;
			var biometricDevice = device as NBiometricDevice;
			CaptureForm form;
			if (captureDevice != null)
			{
				if (formatsComboBox.SelectedItem != null)
				{
					captureDevice.SetCurrentFormat((NMediaFormat)formatsComboBox.SelectedItem);
				}
			}
			if (camera != null)
			{
				var cameraForm = new CameraForm();
				form = cameraForm;
			}
			else if (microphone != null)
			{
				var microphoneForm = new MicrophoneForm();
				form = microphoneForm;
			}
			else if (biometricDevice != null)
			{
				var fScanner = biometricDevice as NFScanner;
				var irisScanner = biometricDevice as NIrisScanner;
				BiometricDeviceForm biometricDeviceForm;
				if (fScanner != null)
				{
					var fScannerForm = new FScannerForm {ImpressionType = (NFImpressionType) biometricDeviceImpressionTypeComboBox.SelectedItem, Position = (NFPosition) biometricDevicePositionComboBox.SelectedItem};
					var missingPositions = new List<NFPosition>();
					if (llCheckBox.Checked) missingPositions.Add(NFPosition.LeftLittleFinger);
					if (lrCheckBox.Checked) missingPositions.Add(NFPosition.LeftRingFinger);
					if (lmCheckBox.Checked) missingPositions.Add(NFPosition.LeftMiddleFinger);
					if (liCheckBox.Checked) missingPositions.Add(NFPosition.LeftIndexFinger);
					if (ltCheckBox.Checked) missingPositions.Add(NFPosition.LeftThumb);
					if (rtCheckBox.Checked) missingPositions.Add(NFPosition.RightThumb);
					if (riCheckBox.Checked) missingPositions.Add(NFPosition.RightIndexFinger);
					if (rmCheckBox.Checked) missingPositions.Add(NFPosition.RightMiddleFinger);
					if (rrCheckBox.Checked) missingPositions.Add(NFPosition.RightRingFinger);
					if (rlCheckBox.Checked) missingPositions.Add(NFPosition.RightLittleFinger);
					fScannerForm.MissingPositions = missingPositions.ToArray();
					biometricDeviceForm = fScannerForm;
				}
				else if (irisScanner != null)
				{
					var irisScannerForm = new IrisScannerForm {Position = (NEPosition) biometricDevicePositionComboBox.SelectedItem};
					biometricDeviceForm = irisScannerForm;
				}
				else throw new NotImplementedException();

				biometricDeviceForm.Automatic = cbAutomatic.Checked;
				biometricDeviceForm.Timeout = ((cbUseTimeout.Checked) ? (int)uint.Parse(tbMiliseconds.Text) : -1);
				form = biometricDeviceForm;
			}
			else throw new NotImplementedException();

			form.GatherImages = cbGatherImages.Checked;
			form.Owner = this;
			form.Device = device;
			_captureForms.Add(form);
			form.FormClosed += (fcSender, fcE) => _captureForms.Remove((CaptureForm) fcSender);
			form.Show();
		}

		private void exitToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void closeToolStripMenuItem_Click(object sender, EventArgs e)
		{
			CloseDeviceManager();
		}

		private void newToolStripMenuItem_Click(object sender, EventArgs e)
		{
			NewDeviceManager();
		}

		private void cbUseTimeout_CheckedChanged(object sender, EventArgs e)
		{
			tbMiliseconds.Enabled = cbUseTimeout.Checked;
		}

		private void customizeFormatButton_Click(object sender, EventArgs e)
		{
			var selectedFormat = formatsComboBox.SelectedItem as NMediaFormat;
			if (selectedFormat == null)
			{
				NDevice device = GetSelectedDevice();
				if ((device.DeviceType & NDeviceType.Camera) == NDeviceType.Camera)
				{
					selectedFormat = new NVideoFormat();
				}
				else if ((device.DeviceType & NDeviceType.Microphone) == NDeviceType.Microphone)
				{
					selectedFormat = new NAudioFormat();
				}
				else throw new NotImplementedException();
			}
			NMediaFormat customFormat = CustomizeFormatForm.CustomizeFormat(selectedFormat);
			if (customFormat != null)
			{
				int index = formatsComboBox.Items.IndexOf(customFormat);
				if (index == -1)
				{
					formatsComboBox.Items.Add(customFormat);
				}
				formatsComboBox.SelectedItem = customFormat;
			}
		}

		private void startSequenceButton_Click(object sender, EventArgs e)
		{
			var biometricDevice = (NBiometricDevice)GetSelectedDevice();
			if (biometricDevice == null) return;
			try
			{
				biometricDevice.StartSequence();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.ToString(), @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void endSequenceButton_Click(object sender, EventArgs e)
		{
			var biometricDevice = (NBiometricDevice)GetSelectedDevice();
			if (biometricDevice == null) return;
			try
			{
				biometricDevice.EndSequence();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.ToString(), @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void connectToolStripMenuItem_Click(object sender, EventArgs e)
		{
			NDevice device = GetSelectedDevice();
			try
			{
				var form = new ConnectToDeviceForm();
				if (device != null) form.SelectedPlugin = device.Plugin;
				if (form.ShowDialog() == DialogResult.OK)
				{
					NDevice newDevice = _deviceManager.ConnectToDevice(form.SelectedPlugin, form.Parameters);
					BeginInvoke(new Action<NDevice>(SetSelectedDevice), newDevice);
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.ToString(), @"Connect to Device Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void disconnectToolStripMenuItem_Click(object sender, EventArgs e)
		{
			NDevice device = GetSelectedDevice();
			if (device != null)
			{
				try
				{
					_deviceManager.DisconnectFromDevice(device);
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.ToString(), @"Disconnect from Device Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
		}

		private void biometricDeviceImpressionTypeComboBox_SelectionChangeCommitted(object sender, EventArgs e)
		{
			ReformDevicePositionComboBox();
		}

		#endregion
	}
}
