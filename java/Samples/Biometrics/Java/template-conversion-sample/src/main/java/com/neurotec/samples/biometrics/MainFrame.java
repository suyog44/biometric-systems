package com.neurotec.samples.biometrics;

import com.neurotec.lang.NCore;
import com.neurotec.samples.util.LicenseManager;
import com.neurotec.samples.util.Settings;
import com.neurotec.samples.util.Utils;

import java.awt.BorderLayout;
import java.awt.Color;
import java.awt.Dimension;
import java.awt.GridBagConstraints;
import java.awt.GridBagLayout;
import java.awt.Insets;
import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;
import java.awt.event.ComponentAdapter;
import java.awt.event.ComponentEvent;
import java.awt.event.WindowAdapter;
import java.awt.event.WindowEvent;
import java.io.File;
import java.io.IOException;
import java.util.ArrayList;
import java.util.List;

import javax.swing.BorderFactory;
import javax.swing.Box;
import javax.swing.BoxLayout;
import javax.swing.ButtonGroup;
import javax.swing.JButton;
import javax.swing.JComponent;
import javax.swing.JFileChooser;
import javax.swing.JFrame;
import javax.swing.JOptionPane;
import javax.swing.JPanel;
import javax.swing.JRadioButton;
import javax.swing.JScrollPane;
import javax.swing.JTextArea;
import javax.swing.filechooser.FileFilter;

public final class MainFrame extends JFrame implements ActionListener {

	// ===========================================================
	// Private static fields
	// ===========================================================

	private static final long serialVersionUID = 1L;

	// ===========================================================
	// Private fields
	// ===========================================================

	private RecordType fromType;
	private RecordType toType;
	private final RecordTransformations recordTransformations;
	private final Settings settings;

	// ===========================================================
	// Private GUI controls
	// ===========================================================

	private JButton btnOpen;
	private JButton btnConvertAndSave;

	private JTextArea txtFrom;
	private JTextArea txtTo;

	private final JFileChooser openFileDialog;
	private final JFileChooser saveFileDialog;

	private ButtonGroup fromGroup;
	private ButtonGroup toGroup;

	private GridBagConstraints c;

	private JRadioButton[] toRadioButtons = new JRadioButton[8];

	// ===========================================================
	// Public constructor
	// ===========================================================

	public MainFrame() {
		try {
			javax.swing.UIManager.setLookAndFeel(javax.swing.UIManager.getSystemLookAndFeelClassName());
		} catch (Exception e) {
			e.printStackTrace();
			JOptionPane.showMessageDialog(null, e.toString());
		}

		recordTransformations = RecordTransformations.getInstance();
		settings = Settings.getDefault("TemplateConversion");
		initializeComponents();
		setIconImage(Utils.createIconImage("images/Logo16x16.png"));

		addComponentListener(new ComponentAdapter() {
			@Override
			public void componentShown(ComponentEvent e) {
				makeAllEnabledDisabled(false);
				super.componentShown(e);
			}
		});

		addWindowListener(new WindowAdapter() {

			@Override
			public void windowClosed(WindowEvent e) {
				saveSettings();
			}

			@Override
			public void windowClosing(WindowEvent e) {
				try {
					dispose();
					LicenseManager.getInstance().releaseAll();
				} finally {
					NCore.shutdown();
				}
			}
		});

		openFileDialog = new JFileChooser();
		saveFileDialog = new SaveFileChooser();

	}

	// ===========================================================
	// Private methods
	// ===========================================================

	// ===========================================================
	// Create GUI
	// ===========================================================

	private void initializeComponents() {
		getContentPane().setLayout(new BorderLayout());

		JPanel mainPanel = new JPanel();
		mainPanel.setBackground(Color.WHITE);
		GridBagLayout mainPanelLayout = new GridBagLayout();
		mainPanelLayout.rowHeights = new int[] { 5, 230, 50, 200 };
		mainPanel.setLayout(mainPanelLayout);

		txtFrom = new JTextArea();
		txtFrom.setBorder(BorderFactory.createLineBorder(Color.GRAY));
		JScrollPane fromScrollPane = new JScrollPane(txtFrom, JScrollPane.VERTICAL_SCROLLBAR_AS_NEEDED, JScrollPane.HORIZONTAL_SCROLLBAR_AS_NEEDED);
		fromScrollPane.setPreferredSize(new Dimension(700, 200));

		txtTo = new JTextArea();
		txtTo.setBorder(BorderFactory.createLineBorder(Color.GRAY));
		JScrollPane toScrollPane = new JScrollPane(txtTo, JScrollPane.VERTICAL_SCROLLBAR_AS_NEEDED, JScrollPane.HORIZONTAL_SCROLLBAR_AS_NEEDED);
		toScrollPane.setPreferredSize(new Dimension(345, 240));

		c = new GridBagConstraints();
		c.fill = GridBagConstraints.BOTH;
		c.insets = new Insets(1, 1, 1, 1);

		addToGridBagLayout(0, 1, 0.5, 0.5, mainPanel, createFromPanel());
		addToGridBagLayout(1, 1, mainPanel, createToPanel());
		addToGridBagLayout(0, 2, 0, 0, mainPanel, createBtnOpenPanel());
		addToGridBagLayout(1, 2, mainPanel, createBtnConvertPanel());
		addToGridBagLayout(0, 3, mainPanel, fromScrollPane);
		addToGridBagLayout(1, 3, mainPanel, toScrollPane);

		getContentPane().add(mainPanel);
		pack();
	}

	private void addToGridBagLayout(int x, int y, JPanel parent, JComponent component) {
		c.gridx = x;
		c.gridy = y;
		parent.add(component, c);
	}

	private void addToGridBagLayout(int x, int y, double weightx, double weighty, JPanel parent, JComponent component) {
		c.gridx = x;
		c.gridy = y;
		c.weightx = weightx;
		c.weighty = weighty;
		parent.add(component, c);
	}

	private JPanel createBtnOpenPanel() {
		btnOpen = new JButton("Open");
		btnOpen.setIcon(Utils.createIcon("images/OpenFolder.png"));
		btnOpen.addActionListener(this);
		btnOpen.setPreferredSize(new Dimension(100, 25));

		JPanel openCenterPanel = new JPanel();
		openCenterPanel.setLayout(new BoxLayout(openCenterPanel, BoxLayout.X_AXIS));
		openCenterPanel.setOpaque(false);

		openCenterPanel.add(Box.createGlue());
		openCenterPanel.add(btnOpen);
		openCenterPanel.add(Box.createGlue());

		JPanel openPanel = new JPanel();
		openPanel.setLayout(new BoxLayout(openPanel, BoxLayout.Y_AXIS));
		openPanel.setOpaque(false);

		openPanel.add(Box.createGlue());
		openPanel.add(openCenterPanel);
		openPanel.add(Box.createGlue());
		return openPanel;
	}

	private JPanel createBtnConvertPanel() {
		btnConvertAndSave = new JButton("Save");
		btnConvertAndSave.setIcon(Utils.createIcon("images/Save.png"));
		btnConvertAndSave.addActionListener(this);
		btnConvertAndSave.setPreferredSize(new Dimension(100, 25));

		JPanel convertCenterPanel = new JPanel();
		convertCenterPanel.setLayout(new BoxLayout(convertCenterPanel, BoxLayout.X_AXIS));
		convertCenterPanel.setOpaque(false);

		convertCenterPanel.add(Box.createGlue());
		convertCenterPanel.add(btnConvertAndSave);
		convertCenterPanel.add(Box.createGlue());

		JPanel convertPanel = new JPanel();
		convertPanel.setLayout(new BoxLayout(convertPanel, BoxLayout.Y_AXIS));
		convertPanel.setOpaque(false);

		convertPanel.add(Box.createGlue());
		convertPanel.add(convertCenterPanel);
		convertPanel.add(Box.createGlue());
		return convertPanel;
	}

	private JPanel createFromPanel() {
		JPanel fromPanel = new JPanel();
		fromPanel.setBackground(Color.WHITE);
		fromPanel.setLayout(new BoxLayout(fromPanel, BoxLayout.Y_AXIS));
		fromPanel.setBorder(BorderFactory.createTitledBorder("Source template"/* "Template to convert from" */));

		fromGroup = new ButtonGroup();
		for (RecordType t : RecordType.values()) {
			JRadioButton radio = createFromPanelRadioButton(t.getTemplateName(), t);
			fromPanel.add(radio);
		}

		return fromPanel;
	}

	private JRadioButton createFromPanelRadioButton(String text, final RecordType type) {
		JRadioButton radio = new JRadioButton(text);
		radio.addActionListener(new ActionListener() {
			public void actionPerformed(ActionEvent e) {
				fromRadioSelectionChanged(type);
			}
		});
		radio.setOpaque(false);
		fromGroup.add(radio);
		return radio;
	}

	private JPanel createToPanel() {
		JPanel toPanel = new JPanel();
		toPanel.setBackground(Color.WHITE);
		toPanel.setLayout(new BoxLayout(toPanel, BoxLayout.Y_AXIS));
		toPanel.setBorder(BorderFactory.createTitledBorder("Convert to"));

		toGroup = new ButtonGroup();
		int i = 0;
		for (RecordType t : RecordType.values()) {
			JRadioButton radio = createToPanelRadioButton(t.getTemplateName(), t);
			toRadioButtons[i] = radio;
			toPanel.add(radio);
			i++;
		}

		return toPanel;
	}

	private JRadioButton createToPanelRadioButton(String text, final RecordType type) {
		JRadioButton radio = new JRadioButton(text);
		radio.addActionListener(new ActionListener() {

			public void actionPerformed(ActionEvent e) {
				toRadioSelectionChanged(type);
			}
		});
		radio.setOpaque(false);
		toGroup.add(radio);
		return radio;
	}

	// ===========================================================
	// Open and convert templates
	// ===========================================================

	private List<String> openDialogSetup(String extension, boolean multiselect) {
		openFileDialog.setMultiSelectionEnabled(multiselect);
		setFileFilters(openFileDialog, extension);

		if (settings.getLastDirectory() != null) {
			openFileDialog.setCurrentDirectory(new File(settings.getLastDirectory()));
		}
		List<String> filePaths = new ArrayList<String>();
		if (openFileDialog.showOpenDialog(this) == JFileChooser.APPROVE_OPTION) {

			settings.setLastDirectory(openFileDialog.getCurrentDirectory().getPath());

			File[] selectedFiles;
			if (multiselect) {
				selectedFiles = openFileDialog.getSelectedFiles();
			} else {
				selectedFiles = new File[] { openFileDialog.getSelectedFile() };
			}

			for (int i = 0; i < selectedFiles.length; i++) {
				filePaths.add(selectedFiles[i].getAbsolutePath());
			}
		}
		return filePaths;
	}

	private File saveDialogSetup(String extension) {
		setFileFilters(saveFileDialog, extension);
		saveFileDialog.setCurrentDirectory(new File(settings.getLastDirectory()));

		if (saveFileDialog.showSaveDialog(this) == JFileChooser.APPROVE_OPTION) {

			settings.setLastDirectory(saveFileDialog.getCurrentDirectory().getPath());

			String savePath = saveFileDialog.getSelectedFile().getPath();
			FileFilter filter = saveFileDialog.getFileFilter();
			if (filter instanceof Utils.TemplateFileFilter) {
				if (savePath.lastIndexOf('.') == -1) {
					savePath += ".data";
				}
			}
			return new File(savePath);
		}

		return null;
	}

	private void setFileFilters(JFileChooser fileChooser, final String filterString) {
		for (FileFilter f : fileChooser.getChoosableFileFilters()) {
			if (!f.getDescription().equals("All Files")) {
				fileChooser.removeChoosableFileFilter(f);
			}
		}

		FileFilter filter = new Utils.TemplateFileFilter(filterString);
		fileChooser.addChoosableFileFilter(filter);
		fileChooser.setFileFilter(filter);
	}

	private void makeAllEnabledDisabled(boolean enabled) {
		for (JRadioButton r : toRadioButtons) {
			r.setEnabled(enabled);
		}
	}

	private void openTemplate() {
		if (fromType == null) {
			JOptionPane.showMessageDialog(this, "Before loading a template you must select the type of the template.\r\n" + "Please select one of the items above and try again.", "Template Convertion: Warning", JOptionPane.WARNING_MESSAGE);
			return;
		}
		boolean multiSelect;
		switch (fromType) {

		case TT_ANSI_FM_RECORD:
		case TT_AN_TEMPLATE:
		case TT_ISO_FM_RECORD:
		case TT_NF_TEMPLATE:
		case TT_NL_TEMPLATE:
		case TT_N_TEMPLATE:
			multiSelect = false;
			break;

		case TT_NF_RECORD:
		case TT_NL_RECORD:
			multiSelect = true;
			break;

		default:
			throw new AssertionError("Can't happen");
		}

		List<String> fileNames = openDialogSetup(fromType.getFileType(), multiSelect);
		if (fileNames.isEmpty()) {
			return;
		}

		try {
			String text = recordTransformations.openTemplate(fromType, fileNames);
			txtFrom.setText(text);
			if (fromType == RecordType.TT_NF_TEMPLATE || fromType == RecordType.TT_NL_TEMPLATE || fromType == RecordType.TT_N_TEMPLATE) {
				int[] disableArray = recordTransformations.getDisableArray();
				for (int i = 0; i < disableArray.length; i++) {
					if (disableArray[i] == 1) {
						toRadioButtons[i].setEnabled(false);
					} else {
						int[] possible = recordTransformations.getPossibleTransformations(fromType.getValue());
						if (possible[i] == 1) {
							toRadioButtons[i].setEnabled(true);
						}
					}
				}
			}
		} catch (IOException e) {
			e.printStackTrace();
			JOptionPane.showMessageDialog(this, "Error occured while openning file(s).\r\nDetails: " + e.getMessage(), "Template Convertion: Error", JOptionPane.ERROR_MESSAGE);
		} catch (Exception e) {
			e.printStackTrace();
			JOptionPane.showMessageDialog(this, "Error occured while openning file(s).\r\nDetails: " + e.getMessage(), "Template Convertion: Error", JOptionPane.ERROR_MESSAGE);
		}
	}

	private void convertAndSave() {
		if (fromType == null || !recordTransformations.templateLoaded()) {
			JOptionPane.showMessageDialog(this, "Before converting a template you must load a template.\r\n" + "Please select one of the items on the left, then press 'Open Template' button and try again.", "Template Convertion: Warning", JOptionPane.WARNING_MESSAGE);
			return;
		}

		if (toType == null || !toRadioButtons[toType.getValue()].isEnabled()) {
			JOptionPane.showMessageDialog(this, "Before converting a template you must select a type to convert to.\r\n" + "Please select one of the items above and try again.", "Template Convertion: Warning", JOptionPane.WARNING_MESSAGE);
			return;
		}

		File file = saveDialogSetup(toType.getFileType());
		String text;
		try {
			text = recordTransformations.convertAndSave(fromType, toType, file);
			txtTo.setText(txtTo.getText() + text);
		} catch (Exception e) {
			e.printStackTrace();
			JOptionPane.showMessageDialog(this, "Error occured while converting or saving files.\r\nDetails: " + e.getMessage(), "Template Convertion: Error", JOptionPane.ERROR_MESSAGE);
		}
	}

	// ===========================================================
	// Package private methods
	// ===========================================================

	void saveSettings() {
		settings.save();
	}

	void fromRadioSelectionChanged(RecordType type) {
		fromType = type;
		txtFrom.setText("");
		recordTransformations.clearCurrentRecords();

		int[] possibleTransformatons = recordTransformations.getPossibleTransformations(fromType.getValue());

		for (int i = 0; i < toRadioButtons.length; i++) {
			toRadioButtons[i].setEnabled(possibleTransformatons[i] == 1);
		}
	}

	void toRadioSelectionChanged(RecordType type) {
		toType = type;
	}

	// ===========================================================
	// Public methods
	// ===========================================================

	public void actionPerformed(ActionEvent e) {
		Object source = e.getSource();
		if (source == btnOpen) {
			openTemplate();
		} else if (source == btnConvertAndSave) {
			convertAndSave();
		}
	}

	// ===========================================================
	// Private classes
	// ===========================================================

	private static final class SaveFileChooser extends JFileChooser {

		private static final long serialVersionUID = 1L;

		@Override
		public void approveSelection() {
			File f = getSelectedFile();
			if (f.exists() && getDialogType() == SAVE_DIALOG) {
				int result = JOptionPane.showConfirmDialog(this, String.format("%s already exists. Do you want to replace it?", f.getName()), "Confirm Save As", JOptionPane.YES_NO_OPTION);
				switch (result) {
				case JOptionPane.YES_OPTION:
					super.approveSelection();
					return;
				case JOptionPane.NO_OPTION:
					return;
				default:
					throw new AssertionError("Can't happen");
				}
			}
			super.approveSelection();
		}
	}

}
