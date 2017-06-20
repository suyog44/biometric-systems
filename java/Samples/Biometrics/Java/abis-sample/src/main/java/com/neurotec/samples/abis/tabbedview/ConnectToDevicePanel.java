package com.neurotec.samples.abis.tabbedview;

import com.neurotec.beans.NParameterBag;
import com.neurotec.beans.NParameterDescriptor;
import com.neurotec.devices.NDeviceManager;
import com.neurotec.lang.NAttribute;
import com.neurotec.plugins.NPlugin;
import com.neurotec.plugins.NPluginState;
import com.neurotec.samples.abis.util.MessageUtils;
import com.neurotec.swing.NPropertyGrid;
import com.neurotec.util.NPropertyBag;

import java.awt.BorderLayout;
import java.awt.FlowLayout;
import java.awt.GridLayout;
import java.awt.Window;
import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;
import java.awt.event.ItemEvent;
import java.awt.event.ItemListener;

import javax.swing.BorderFactory;
import javax.swing.JButton;
import javax.swing.JComboBox;
import javax.swing.JLabel;
import javax.swing.JPanel;
import javax.swing.JScrollPane;

public class ConnectToDevicePanel extends JPanel implements ActionListener, ItemListener {

	// ===========================================================
	// Static fields
	// ===========================================================

	private static final long serialVersionUID = 1L;

	// ===========================================================
	// Private fields
	// ===========================================================

	private NParameterDescriptor[] parameters;
	private NPropertyGrid propertyGrid;
	private boolean resultOk;
	private final Window parent;

	private JButton btnCancel;
	private JButton btnOk;
	private JComboBox comboBoxPlugin;
	private JLabel lblPlugin;
	private JPanel panelBottom;
	private JPanel panelCenter;
	private JPanel panelTop;
	private JScrollPane spParameters;

	// ===========================================================
	// Public constructor
	// ===========================================================

	public ConnectToDevicePanel(Window parent) {
		resultOk = false;
		this.parent = parent;
		initGUI();
		updatePlugins();
	}

	// ===========================================================
	// Private methods
	// ===========================================================

	private void initGUI() {
		panelTop = new JPanel();
		lblPlugin = new JLabel();
		comboBoxPlugin = new JComboBox();
		panelCenter = new JPanel();
		spParameters = new JScrollPane();
		panelBottom = new JPanel();
		btnOk = new JButton();
		btnCancel = new JButton();

		setLayout(new BorderLayout());

		panelTop.setBorder(BorderFactory.createEmptyBorder(10, 10, 10, 10));
		panelTop.setLayout(new BorderLayout());

		lblPlugin.setText("Plugin: ");
		panelTop.add(lblPlugin, BorderLayout.WEST);

		panelTop.add(comboBoxPlugin, BorderLayout.CENTER);

		add(panelTop, BorderLayout.NORTH);

		panelCenter.setBorder(BorderFactory.createTitledBorder("Parameters"));
		panelCenter.setLayout(new GridLayout(1, 1, 1, 0));
		panelCenter.add(spParameters);

		add(panelCenter, BorderLayout.CENTER);

		panelBottom.setLayout(new FlowLayout(FlowLayout.TRAILING));

		btnOk.setText("OK");
		panelBottom.add(btnOk);

		btnCancel.setText("Cancel");
		panelBottom.add(btnCancel);

		add(panelBottom, BorderLayout.SOUTH);

		propertyGrid = new NPropertyGrid();
		spParameters.setViewportView(propertyGrid);

		btnOk.addActionListener(this);
		btnCancel.addActionListener(this);
		comboBoxPlugin.addItemListener(this);
	}

	private void updatePlugins() {
		for (NPlugin plugin : NDeviceManager.getPluginManager().getPlugins()) {
			if ((plugin.getState() == NPluginState.PLUGGED) && NDeviceManager.isConnectToDeviceSupported(plugin)) {
				comboBoxPlugin.addItem(plugin);
			}
		}
		if (comboBoxPlugin.getItemCount() != 0) {
			comboBoxPlugin.setSelectedIndex(0);
		}
		selectedPluginChanged();
	}

	private void selectedPluginChanged() {
		NPlugin plugin = getSelectedPlugin();
		if (plugin == null) {
			parameters = null;
		} else {
			parameters = NDeviceManager.getConnectToDeviceParameters(plugin);
		}
		if (parameters == null) {
			propertyGrid.setSource(null);
		} else {
			propertyGrid.setSource(new NParameterBag(parameters));
		}
		btnOk.setEnabled(plugin != null);
		propertyGrid.setEnabled(plugin != null);
	}

	// ===========================================================
	// Public methods
	// ===========================================================

	public NPlugin getSelectedPlugin() {
		return (NPlugin) comboBoxPlugin.getSelectedItem();
	}

	public NPropertyBag getParameters() {
		NParameterBag parameterBag = (NParameterBag) propertyGrid.getSource();
		if (parameterBag == null) {
			return new NPropertyBag();
		} else {
			return parameterBag.toPropertyBag();
		}
	}

	public boolean isResultOk() {
		return resultOk;
	}

	@Override
	public void actionPerformed(ActionEvent ev) {
		try {
			if (ev.getSource().equals(btnOk)) {
				resultOk = true;
				if (parameters != null) {
					NParameterBag parameterBag = (NParameterBag) propertyGrid.getSource();
					for (int i = 0; i < parameters.length; i++) {
						if (!parameters[i].getAttributes().contains(NAttribute.OPTIONAL)) {
							if (parameterBag.values().get(i) == null) {
								resultOk = false;
								MessageUtils.showError(this, "Error", String.format("%s value not specified", parameters[i].getName()));
							}
						}
					}
				}
				parent.dispose();
			} else if (ev.getSource().equals(btnCancel)) {
				parent.dispose();
			}
		} catch (Exception e) {
			MessageUtils.showError(this, e);
		}
	}

	@Override
	public void itemStateChanged(ItemEvent ev) {
		try {
			if (ev.getSource().equals(comboBoxPlugin)) {
				if (ev.getStateChange() == ItemEvent.SELECTED) {
					selectedPluginChanged();
				}
			}
		} catch (Exception e) {
			MessageUtils.showError(this, e);
		}
	}

}
