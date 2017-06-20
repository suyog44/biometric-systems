package com.neurotec.samples.devices;

import java.awt.BorderLayout;
import java.awt.EventQueue;
import java.awt.GridLayout;
import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;
import java.util.ArrayList;
import java.util.List;

import javax.swing.DefaultListModel;
import javax.swing.JButton;
import javax.swing.JDialog;
import javax.swing.JFrame;
import javax.swing.JList;
import javax.swing.JOptionPane;
import javax.swing.JPanel;
import javax.swing.JScrollPane;
import javax.swing.ListSelectionModel;
import javax.swing.border.EmptyBorder;
import javax.swing.event.ListSelectionEvent;
import javax.swing.event.ListSelectionListener;

import com.neurotec.devices.virtual.NVirtualDevice;
import com.neurotec.swing.NPropertyGrid;

public final class VirtualDeviceManagerDialog extends JDialog {

	// ==============================================
	// Private static fields
	// ==============================================

	private static final long serialVersionUID = 1L;

	/**
	 * Launch the application.
	 */
	public static void main(String[] args) {
		EventQueue.invokeLater(new Runnable() {
			public void run() {
				try {
					VirtualDeviceManagerDialog dialog = new VirtualDeviceManagerDialog(null);
					dialog.setDefaultCloseOperation(JDialog.DISPOSE_ON_CLOSE);
					dialog.setVisible(true);
				} catch (Exception e) {
					e.printStackTrace();
				}
			}
		});
	}

	// ==============================================
	// Private fields
	// ==============================================

	private JButton btnAdd;
	private JButton btnPlug;
	private JButton btnRemove;
	private JButton btnUnplug;
	private JList listDevices;
	private DefaultListModel listModelDevices;
	private NPropertyGrid propertyGrid;
	private JPanel panelWest;

	// ==============================================
	// Public constructor
	// ==============================================

	public VirtualDeviceManagerDialog(JFrame owner) {
		super(owner, true);
		initGUI();
		selectedDeviceChanged();
	}

	// ==============================================
	// Private methods
	// ==============================================

	private void initGUI() {
		setBounds(100, 100, 734, 500);

		panelWest = new JPanel();
		panelWest.setBorder(new EmptyBorder(5, 5, 5, 5));
		getContentPane().add(panelWest, BorderLayout.WEST);
		panelWest.setLayout(new BorderLayout(5, 5));

		JScrollPane scrollPane = new JScrollPane();
		panelWest.add(scrollPane, BorderLayout.CENTER);

		listModelDevices = new DefaultListModel();
		listDevices = new JList(listModelDevices);
		listDevices.setSelectionMode(ListSelectionModel.SINGLE_SELECTION);
		listDevices.addListSelectionListener(new ListSelectionListener() {
			@Override
			public void valueChanged(ListSelectionEvent e) {
				selectedDeviceChanged();
			}
		});
		scrollPane.setViewportView(listDevices);

		JPanel panelButtons = new JPanel();
		panelWest.add(panelButtons, BorderLayout.SOUTH);
		panelButtons.setLayout(new GridLayout(2, 2, 5, 5));

		btnAdd = new JButton("Add");
		panelButtons.add(btnAdd);
		btnAdd.addActionListener(new ActionListener() {
			@Override
			public void actionPerformed(ActionEvent e) {
				try {
					addDevice(new NVirtualDevice());
				} catch (Exception ex) {
					JOptionPane.showMessageDialog(getOwner(), ex.getMessage(), "Error", JOptionPane.ERROR_MESSAGE);
				}
			}
		});

		btnPlug = new JButton("Plug");
		panelButtons.add(btnPlug);
		btnPlug.addActionListener(new ActionListener() {
			@Override
			public void actionPerformed(ActionEvent e) {
				NVirtualDevice device = getSelectedItem();
				if (device != null) {
					device.setPluggedIn(true);
					enableControls();
					propertyGrid.setSource(device);
				}
			}
		});

		btnRemove = new JButton("Remove");
		panelButtons.add(btnRemove);
		btnRemove.addActionListener(new ActionListener() {
			@Override
			public void actionPerformed(ActionEvent e) {
				NVirtualDevice device = getSelectedItem();
				int index = getSelectedIndex();
				if (device != null) {
					device.setPluggedIn(false);
					listModelDevices.removeElement(device);
					if (!listModelDevices.isEmpty()) {
						listDevices.setSelectedIndex(index > 0 ? index - 1 : 0);
					}
				}
			}
		});

		btnUnplug = new JButton("Unplug");
		panelButtons.add(btnUnplug);
		btnUnplug.addActionListener(new ActionListener() {
			@Override
			public void actionPerformed(ActionEvent e) {
				NVirtualDevice device = getSelectedItem();
				if (device != null) {
					device.setPluggedIn(false);
					enableControls();
					propertyGrid.setSource(device);
				}
			}
		});

		JPanel panelCenter = new JPanel();
		panelCenter.setBorder(new EmptyBorder(5, 5, 5, 5));
		getContentPane().add(panelCenter, BorderLayout.CENTER);
		panelCenter.setLayout(new BorderLayout(0, 0));

		propertyGrid = new NPropertyGrid();
		panelCenter.add(propertyGrid, BorderLayout.CENTER);
	}

	private void selectedDeviceChanged() {
		NVirtualDevice device = getSelectedItem();
		propertyGrid.setSource(device);
		enableControls();
	}

	private int getSelectedIndex() {
		return listDevices.getSelectedIndex();
	}

	private NVirtualDevice getSelectedItem() {
		return (NVirtualDevice) listDevices.getSelectedValue();
	}

	private void enableControls() {
		NVirtualDevice device = getSelectedItem();
		if (device != null) {
			btnPlug.setEnabled(!device.isPluggedIn());
			btnUnplug.setEnabled(!btnPlug.isEnabled());
			btnRemove.setEnabled(true);
		} else {
			btnRemove.setEnabled(false);
			btnPlug.setEnabled(false);
			btnUnplug.setEnabled(false);
		}
	}

	@SuppressWarnings("unchecked")
	private void addDevice(NVirtualDevice device) {
		listModelDevices.addElement(device);
		listDevices.setSelectedValue(device, true);
	}

	// ==============================================
	// Private methods
	// ==============================================

	public List<NVirtualDevice> getVirtualDevices() {
		List<NVirtualDevice> devices = new ArrayList<NVirtualDevice>(listModelDevices.getSize());
		for (int i = 0; i < listModelDevices.getSize(); i++) {
			devices.add((NVirtualDevice)listModelDevices.getElementAt(i));
		}
		return devices;
	}

	public void setVirtualDevices(List<NVirtualDevice> devices) {
		listModelDevices.clear();
		if (devices != null) {
			for (NVirtualDevice device : devices) {
				addDevice(device);
			}
		}
	}

}
