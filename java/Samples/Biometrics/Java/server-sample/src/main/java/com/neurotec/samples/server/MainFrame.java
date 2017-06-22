package com.neurotec.samples.server;

import java.awt.BorderLayout;
import java.awt.CardLayout;
import java.awt.Color;
import java.awt.Container;
import java.awt.Dimension;
import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;
import java.awt.event.WindowAdapter;
import java.awt.event.WindowEvent;
import java.util.ArrayList;
import java.util.List;
import java.util.concurrent.ExecutionException;

import javax.swing.BorderFactory;
import javax.swing.Box;
import javax.swing.BoxLayout;
import javax.swing.JButton;
import javax.swing.JCheckBox;
import javax.swing.JFrame;
import javax.swing.JLabel;
import javax.swing.JOptionPane;
import javax.swing.JPanel;

import com.neurotec.biometrics.client.NBiometricClient;
import com.neurotec.biometrics.client.NClusterBiometricConnection;
import com.neurotec.lang.NCore;
import com.neurotec.samples.server.connection.AcceleratorConnection;
import com.neurotec.samples.server.connection.DatabaseConnection;
import com.neurotec.samples.server.connection.DirectoryEnumerator;
import com.neurotec.samples.server.connection.TemplateLoader;
import com.neurotec.samples.server.controls.BasePanel;
import com.neurotec.samples.server.controls.DeduplicationPanel;
import com.neurotec.samples.server.controls.EnrollPanel;
import com.neurotec.samples.server.controls.TestSpeedPanel;
import com.neurotec.samples.server.settings.ConnectionSettingsDialog;
import com.neurotec.samples.server.settings.MatchingSettingsPanel;
import com.neurotec.samples.server.settings.Settings;
import com.neurotec.samples.server.util.MessageUtils;
import com.neurotec.samples.util.Utils;

public final class MainFrame extends JFrame implements ActionListener {

	// ==============================================
	// Private static fields
	// ==============================================

	private static final long serialVersionUID = 1L;
	private static final String SAMPLE_TITLE = "Server Sample";
	private static final Color SELECTED_BUTTON_COLOR = Color.BLUE;
	private static final Color NOT_SELECTED_BUTTON_COLOR = new Color(162, 181, 205);

	// ==============================================
	// Public enum
	// ==============================================

	public static enum Task {
		DEDUPLICATION(0), ENROLL(1), SPEED_TEST(2), SETTINGS(3);

		private int value;

		private Task(int value) {
			this.value = value;
		}

		public int value() {
			return value;
		}
	}

	// ==============================================
	// Private fields
	// ==============================================

	private TemplateLoader templateLoader;
	private AcceleratorConnection acceleratorConnection;
	private NBiometricClient biometricClient;
	private NClusterBiometricConnection biometricConnection;

	private Settings settings = Settings.getInstance();
	private Dimension buttonSize = new Dimension(185, 40);

	// ==============================================
	// Private GUI components
	// ==============================================

	private final JButton[] mainFrameButtons = new JButton[5];
	private final List<BasePanel> panels = new ArrayList<BasePanel>();
	private JButton btnConnection;
	private JButton btnDeduplication;
	private JButton btnEnroll;
	private JButton btnTestSpeed;
	private JButton btnMatchingSettings;
	private JPanel panelCardLayoutContainer;
	private JPanel panelLeft;
	private CardLayout cardLayoutTaskPanels;
	private JCheckBox chkAccelerator;
	private JLabel lblServerAddress;
	private JLabel lblClientPortValue;
	private JLabel lblAdminPortValue;
	private JLabel lblSource;
	private JLabel lblSourceValue;
	private JLabel lblDSN;
	private JLabel lblDSNValue;
	private JLabel lblTable;
	private JLabel lblTableValue;

	private BasePanel activePanel;

	// ==============================================
	// Public constructor
	// ==============================================

	public MainFrame() {
		try {
			javax.swing.UIManager.setLookAndFeel(javax.swing.UIManager.getSystemLookAndFeelClassName());
		} catch (Exception e) {
			e.printStackTrace();
			JOptionPane.showMessageDialog(null, e.toString());
		}

		setIconImage(Utils.createIconImage("images/Logo16x16.png"));
		initializeComponents();

		addWindowListener(new WindowAdapter() {
			@Override
			public void windowClosing(WindowEvent e) {
				mainFrameClosing();
			}
		});
	}

	// ==============================================
	// Private methods
	// ==============================================

	private void initializeComponents() {
		Container contentPane = getContentPane();
		contentPane.setLayout(new BorderLayout());

		panelLeft = new JPanel();
		panelLeft.setBackground(Color.LIGHT_GRAY);
		panelLeft.setLayout(new BoxLayout(panelLeft, BoxLayout.Y_AXIS));

		Dimension leftPanelSize = new Dimension(195, 415);
		panelLeft.setPreferredSize(leftPanelSize);
		panelLeft.setMinimumSize(leftPanelSize);
		panelLeft.setMaximumSize(leftPanelSize);

		btnConnection = createMainButton("<html><p align=\"center\">Change connection<br>settings</p></html>", 0);
		btnConnection.setIcon(Utils.createIcon("images/settings.png"));
		btnConnection.setToolTipText("Change connection settings to Server and/or database containing templates");
		btnDeduplication = createMainButton("Deduplication", 1);
		btnDeduplication.setToolTipText("Perform template deduplication on Megamatcher Accelerator");
		btnEnroll = createMainButton("Enroll templates", 2);
		btnEnroll.setToolTipText("Enroll templates to MegaMatcher Accelerator");
		btnTestSpeed = createMainButton("<html><p align=\"center\"> Calculate/Test Accelerator<br>matching speed</p></html>", 3);
		btnTestSpeed.setToolTipText("Test MegaMatcher Accelerator matching speed");
		btnMatchingSettings = createMainButton("Change matching settings", 4);

		cardLayoutTaskPanels = new CardLayout();
		panelCardLayoutContainer = new JPanel(cardLayoutTaskPanels);
		panelCardLayoutContainer.setBorder(BorderFactory.createEmptyBorder(5, 5, 5, 5));
		initializeTaskPanels();

		contentPane.add(panelCardLayoutContainer, BorderLayout.CENTER);
		contentPane.add(panelLeft, BorderLayout.BEFORE_LINE_BEGINS);
		contentPane.add(initializeInformationPanel(), BorderLayout.AFTER_LAST_LINE);
		pack();
	}

	private JButton createMainButton(String text, int i) {
		JButton button = new JButton(text);
		button.setOpaque(true);
		button.setPreferredSize(buttonSize);
		button.setMinimumSize(buttonSize);
		button.setMaximumSize(buttonSize);
		button.addActionListener(this);
		panelLeft.add(Box.createVerticalStrut(5));
		panelLeft.add(button);
		button.setAlignmentX(CENTER_ALIGNMENT);
		mainFrameButtons[i] = button;
		return button;
	}

	private JPanel initializeInformationPanel() {
		JPanel informationPanel = new JPanel(new BorderLayout(2, 2));
		informationPanel.setBorder(BorderFactory.createTitledBorder("Connection information"));
		informationPanel.setBackground(Color.LIGHT_GRAY);

		JPanel serverInfoPanel = new JPanel();
		serverInfoPanel.setOpaque(false);
		BoxLayout serverPanelLayout = new BoxLayout(serverInfoPanel, BoxLayout.X_AXIS);
		serverInfoPanel.setLayout(serverPanelLayout);

		lblServerAddress = new JLabel("N/A");
		lblClientPortValue = new JLabel("N/A");
		lblAdminPortValue = new JLabel("N/A");
		chkAccelerator = new JCheckBox("Is Accelerator");
		chkAccelerator.setEnabled(false);
		chkAccelerator.setOpaque(false);

		serverInfoPanel.add(Box.createHorizontalStrut(2));
		serverInfoPanel.add(new JLabel("Server:"));
		serverInfoPanel.add(Box.createHorizontalStrut(2));
		serverInfoPanel.add(lblServerAddress);
		serverInfoPanel.add(Box.createHorizontalStrut(10));
		serverInfoPanel.add(new JLabel("Client port:"));
		serverInfoPanel.add(Box.createHorizontalStrut(2));
		serverInfoPanel.add(lblClientPortValue);
		serverInfoPanel.add(Box.createHorizontalStrut(10));
		serverInfoPanel.add(new JLabel("Admin port:"));
		serverInfoPanel.add(Box.createHorizontalStrut(2));
		serverInfoPanel.add(lblAdminPortValue);
		serverInfoPanel.add(Box.createHorizontalStrut(10));
		serverInfoPanel.add(chkAccelerator);
		serverInfoPanel.add(Box.createHorizontalGlue());

		JPanel templateInfoPanel = new JPanel();
		templateInfoPanel.setOpaque(false);
		BoxLayout templateInfoLayout = new BoxLayout(templateInfoPanel, BoxLayout.X_AXIS);
		templateInfoPanel.setLayout(templateInfoLayout);

		lblSource = new JLabel("Templates loaded from:");
		lblSourceValue = new JLabel("N/A");
		lblDSN = new JLabel("DSN:");
		lblDSNValue = new JLabel("N/A");
		lblTable = new JLabel("Table:");
		lblTableValue = new JLabel("N/A");

		templateInfoPanel.add(Box.createHorizontalStrut(2));
		templateInfoPanel.add(lblSource);
		templateInfoPanel.add(Box.createHorizontalStrut(2));
		templateInfoPanel.add(lblSourceValue);
		templateInfoPanel.add(Box.createHorizontalStrut(10));
		templateInfoPanel.add(lblDSN);
		templateInfoPanel.add(Box.createHorizontalStrut(2));
		templateInfoPanel.add(lblDSNValue);
		templateInfoPanel.add(Box.createHorizontalStrut(10));
		templateInfoPanel.add(lblTable);
		templateInfoPanel.add(Box.createHorizontalStrut(2));
		templateInfoPanel.add(lblTableValue);
		templateInfoPanel.add(Box.createHorizontalGlue());

		informationPanel.add(serverInfoPanel, BorderLayout.BEFORE_FIRST_LINE);
		informationPanel.add(templateInfoPanel, BorderLayout.AFTER_LAST_LINE);
		return informationPanel;
	}

	private void initializeTaskPanels() {
		BasePanel panel = new DeduplicationPanel(this);
		panels.add(panel);
		panelCardLayoutContainer.add(panel, Task.DEDUPLICATION.name());

		panel = new EnrollPanel(this);
		panels.add(panel);
		panelCardLayoutContainer.add(panel, Task.ENROLL.name());

		panel = new TestSpeedPanel(this);
		panels.add(panel);
		panelCardLayoutContainer.add(panel, Task.SPEED_TEST.name());

		panel = new MatchingSettingsPanel(this);
		panels.add(panel);
		panelCardLayoutContainer.add(panel, Task.SETTINGS.name());

		for (BasePanel bPanel : panels) {
			bPanel.setBiometricClient(biometricClient);
		}

	}

	private void showConnectionSettings(boolean isLoadingTime) {
		if (isLoadingTime) {
			settings.setDSNConnection(false);
		}
		ConnectionSettingsDialog dialog = new ConnectionSettingsDialog(this);
		dialog.setLocationRelativeTo(this);
		dialog.setModal(true);
		dialog.setVisible(true);
		connectionSettingsChanged(isLoadingTime);
	}

	private void changeConnectionSettings() {
		if (activePanel.isBusy()) {
			if (MessageUtils.showQuestion(this, "Action in progress. Stop current action?")) {
				activePanel.cancel();
			} else {
				return;
			}
		}
		showConnectionSettings(false);
		activePanel.setBiometricClient(biometricClient);
		activePanel.setTemplateLoader(templateLoader);
		activePanel.setAccelerator(acceleratorConnection);
	}

	private void mainFrameClosing() {
		try {
			if (activePanel != null && activePanel.isBusy()) {
				activePanel.cancel();
				setTitle(String.format("%s: Closing, please wait ...", SAMPLE_TITLE));
				try {
					activePanel.waitForCurrentProcessToFinish();
				} catch (InterruptedException e) {
					e.printStackTrace();
					MessageUtils.showError(this, e);
				} catch (ExecutionException e) {
					e.printStackTrace();
					MessageUtils.showError(this, e);
				}
			}
		} finally {
			try {
				NCore.shutdown();
			} catch (Exception e) {
				e.printStackTrace();
			}
		}
	}

	private void showPanel(Task task, boolean force) {
		int index = task.value();
		if (activePanel == panels.get(index)) {
			return;
		}
		if (!force && activePanel != null && activePanel.isBusy()) {
			if (MessageUtils.showQuestion(this, "Action in progress. Stop current action?")) {
				activePanel.cancel();
				try {
					activePanel.waitForCurrentProcessToFinish();
				} catch (InterruptedException e) {
					e.printStackTrace();
					MessageUtils.showError(this, e);
				} catch (ExecutionException e) {
					e.printStackTrace();
					MessageUtils.showError(this, e);
				}
			} else {
				return;
			}
		}

		activePanel = panels.get(index);

		for (int i = 1; i < 5; i++) {
			mainFrameButtons[i].setBackground((index == i - 1) ? SELECTED_BUTTON_COLOR : NOT_SELECTED_BUTTON_COLOR);
		}

		cardLayoutTaskPanels.show(panelCardLayoutContainer, task.name());

		activePanel.setBiometricClient(biometricClient);
		activePanel.setTemplateLoader(templateLoader);
		activePanel.setAccelerator(acceleratorConnection);

		String title = activePanel.getTitle();
		if (title == null || title.equals("")) {
			setTitle(SAMPLE_TITLE);
		} else {
			setTitle(String.format("%s: %s", SAMPLE_TITLE, title));
		}
	}

	private void connectionSettingsChanged(boolean isLoadingTime) {
		biometricClient = new NBiometricClient();
		biometricConnection = new NClusterBiometricConnection(settings.getServer(), settings.getClientPort(), settings.getAdminPort());
		biometricClient.getRemoteConnections().add(biometricConnection);
		boolean isUseDB = settings.isTemplateSourceDb();
		if (!isUseDB) {
			lblSourceValue.setText(settings.getTemplateDirectory());
		} else {
			lblDSNValue.setText(settings.getDSN());
			lblTableValue.setText(settings.getTable());
		}

		if (settings.isServerModeAccelerator()) {
			acceleratorConnection = new AcceleratorConnection(settings.getServer(), settings.getClientPort(), settings.getAdminPort(), settings.getMMAUser(),
					settings.getMMAPassword());
		} else {
			acceleratorConnection = null;
		}

		updateConnectionInformation();

		if (isLoadingTime) {
			setVisible(true);
			showPanel(Task.DEDUPLICATION, false);
		}
	}

	private void updateConnectionInformation() {
		chkAccelerator.setSelected(settings.isServerModeAccelerator());
		boolean isUseDB = settings.isTemplateSourceDb();
		if (!isUseDB) {
			lblSourceValue.setText(settings.getTemplateDirectory());
			templateLoader = new DirectoryEnumerator(settings.getTemplateDirectory());
		} else {
			lblDSNValue.setText(settings.getDSN());
			lblTableValue.setText(settings.getTable());
			lblSourceValue.setText("N/A");
			templateLoader = new DatabaseConnection();
		}
		lblDSN.setVisible(isUseDB);
		lblDSNValue.setVisible(isUseDB);
		lblTable.setVisible(isUseDB);
		lblTableValue.setVisible(isUseDB);

		lblServerAddress.setText(settings.getServer());
		lblClientPortValue.setText(String.valueOf(settings.getClientPort()));
		lblAdminPortValue.setText(String.valueOf(settings.getAdminPort()));
	}

	// ==============================================
	// Public methods
	// ==============================================

	public void showMainFrame() {
		setVisible(false);
		showConnectionSettings(true);
	}

	public boolean isPanelBusy() {
		if (activePanel != null && activePanel.isBusy()) {
			return true;
		}
		return false;
	}

	public void actionPerformed(ActionEvent e) {
		Object source = e.getSource();
		if (source == btnConnection && !isPanelBusy()) {
			changeConnectionSettings();
		} else if (source == btnDeduplication) {
			showPanel(Task.DEDUPLICATION, false);
		} else if (source == btnEnroll) {
			showPanel(Task.ENROLL, false);
		} else if (source == btnTestSpeed) {
			showPanel(Task.SPEED_TEST, false);
		} else if (source == btnMatchingSettings) {
			showPanel(Task.SETTINGS, false);
		}
	}
}
