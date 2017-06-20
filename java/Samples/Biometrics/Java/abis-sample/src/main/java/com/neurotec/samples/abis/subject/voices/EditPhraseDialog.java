package com.neurotec.samples.abis.subject.voices;

import com.neurotec.samples.abis.settings.SettingsManager;
import com.neurotec.samples.abis.util.MessageUtils;
import com.neurotec.samples.util.Utils;
import com.neurotec.swing.NTableModel;

import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;
import java.util.ArrayList;
import java.util.List;

import javax.swing.BorderFactory;
import javax.swing.GroupLayout;
import javax.swing.JButton;
import javax.swing.JFrame;
import javax.swing.JLabel;
import javax.swing.JPanel;
import javax.swing.JScrollPane;
import javax.swing.JTable;
import javax.swing.JTextField;
import javax.swing.table.TableColumnModel;

public final class EditPhraseDialog extends javax.swing.JDialog implements ActionListener {

	// ===========================================================
	// Private static fields
	// ===========================================================

	private static final long serialVersionUID = 1L;

	// ===========================================================
	// Private fields
	// ===========================================================

	private JScrollPane tableScrollPane;
	private JLabel phraseIdLabel;
	private JTextField phraseTextField;
	private JTable phrasesTable;
	private PhrasesTableModel phrasesTableModel;
	private JButton closeButton;
	private JButton addButton;
	private JButton removeButton;
	private JTextField phraseIdTextField;
	private JLabel phraseLabel;
	private JPanel newPhrasePanel;
	private List<Phrase> phrases;

	// ===========================================================
	// Public constructor
	// ===========================================================

	public EditPhraseDialog(JFrame frame) {
		super(frame);
		initGUI();
	}

	// ===========================================================
	// Private methods
	// ===========================================================

	private void initGUI() {
		try {
			{
				setTitle("Edit phrases");
				setSize(300, 500);
				setDefaultCloseOperation(DISPOSE_ON_CLOSE);
				GroupLayout mainLayout = new GroupLayout(getContentPane());
				setLayout(mainLayout);
				mainLayout.setAutoCreateGaps(true);
				mainLayout.setAutoCreateContainerGaps(true);
				{
					tableScrollPane = new JScrollPane();
					{
						String[] columns = new String[] {"Phrase Id", "Phrase"};
						phrasesTableModel = new PhrasesTableModel(columns);
						phrasesTable = new JTable(phrasesTableModel);
						tableScrollPane.setViewportView(phrasesTable);
						phrasesTable.setAutoResizeMode(JTable.AUTO_RESIZE_ALL_COLUMNS);
						TableColumnModel columnModel = phrasesTable.getColumnModel();
						columnModel.getColumn(0).setPreferredWidth(70);
						columnModel.getColumn(1).setPreferredWidth(200);
					}
				}
				{
					removeButton = new JButton("Remove");
					removeButton.addActionListener(this);
				}
				{
					newPhrasePanel = new JPanel();
					newPhrasePanel.setBorder(BorderFactory.createTitledBorder("Add new"));
					GroupLayout newPhraseLayout = new GroupLayout(newPhrasePanel);
					newPhrasePanel.setLayout(newPhraseLayout);
					newPhraseLayout.setAutoCreateGaps(true);
					newPhraseLayout.setAutoCreateContainerGaps(true);
					{
						phraseIdLabel = new JLabel("Phrase id:");
						phraseIdTextField = new JTextField();
						phraseLabel = new JLabel("Phrase:");
						phraseTextField = new JTextField();
					}
					{
						addButton = new JButton("Add");
						addButton.addActionListener(this);
					}
					newPhraseLayout.setHorizontalGroup(
						newPhraseLayout.createSequentialGroup()
							.addGroup(newPhraseLayout.createParallelGroup(GroupLayout.Alignment.TRAILING)
								.addComponent(phraseIdLabel)
								.addComponent(phraseLabel))
							.addGroup(newPhraseLayout.createParallelGroup(GroupLayout.Alignment.TRAILING)
								.addComponent(phraseIdTextField)
								.addComponent(phraseTextField)
								.addComponent(addButton))
					);
					newPhraseLayout.setVerticalGroup(
						newPhraseLayout.createSequentialGroup()
							.addGroup(newPhraseLayout.createParallelGroup(GroupLayout.Alignment.BASELINE)
								.addComponent(phraseIdLabel)
								.addComponent(phraseIdTextField))
							.addGroup(newPhraseLayout.createParallelGroup(GroupLayout.Alignment.BASELINE)
								.addComponent(phraseLabel)
								.addComponent(phraseTextField))
							.addComponent(addButton)
					);
				}
				{
					closeButton = new JButton("Close");
					closeButton.addActionListener(this);
				}
				mainLayout.setHorizontalGroup(
					mainLayout.createParallelGroup(GroupLayout.Alignment.TRAILING)
						.addComponent(tableScrollPane)
						.addComponent(removeButton)
						.addComponent(newPhrasePanel)
						.addComponent(closeButton)
				);
				mainLayout.setVerticalGroup(
					mainLayout.createSequentialGroup()
						.addComponent(tableScrollPane)
						.addComponent(removeButton)
						.addComponent(newPhrasePanel)
						.addComponent(closeButton)
				);
			}
			phrases = new ArrayList<Phrase>();
		} catch (Exception e) {
			e.printStackTrace();
		}
	}

	private void listAllPhrases() {
		phrasesTableModel.clear();
		for (Phrase phrase : phrases) {
			phrasesTableModel.add(phrase);
		}

		if (phrasesTableModel.getRowCount() > 0) {
			phrasesTable.setRowSelectionInterval(0, 0);
		}
	}

	private void saveAllPhrases() {
		SettingsManager.setPhrases(phrases);
	}

	private void removePhrase() {
		if (phrasesTable.getSelectedRowCount() == 1) {
			Phrase phrase = phrasesTableModel.get(phrasesTable.getSelectedRows()[0]);
			if (phrase != null) {
				phrases.remove(phrase);
				phrasesTableModel.remove(phrase);
			}
		} else {
			MessageUtils.showInformation(this, "Information", "No or more than one phrases are selected!");
		}
	}

	private void addPhrase() {
		int phraseId;

		if (Utils.isNullOrEmpty(phraseIdTextField.getText()) || Utils.isNullOrEmpty(phraseTextField.getText())) {
			MessageUtils.showInformation(this, "Information", "One or more fields is empty. Please fill all of the fields.");
			return;
		}

		try {
			phraseId = Integer.parseInt(phraseIdTextField.getText());
		} catch (Exception e) {
			e.printStackTrace();
			MessageUtils.showError(this, "Error", "Phrase Id should be integer (above 0)!");
			return;
		}

		if (phraseId <= 0) {
			MessageUtils.showError(this, "Error", "Phrase Id should be above 0!");
			return;
		}

		if (phraseIdTextField.getText().contains("=") || phraseIdTextField.getText().contains(";")) {
			MessageUtils.showError(this, "Error", "Phrase must not countain symbols: '=', ';'!");
			return;
		}

		Phrase phraseToAdd = new Phrase(phraseId, phraseTextField.getText());
		for (Phrase phrase : phrases) {
			if (phrase.getID() == phraseToAdd.getID()) {
				MessageUtils.showError(this, "Error", "Another phrase with the same phrase id already exist in the list!");
				return;
			}
		}
		phrases.add(phraseToAdd);
		listAllPhrases();
	}

	//!doc
	// ===========================================================
	// Public methods
	// ===========================================================

	@Override
	public void actionPerformed(ActionEvent e) {
		if (e.getSource() == addButton) {
			addPhrase();
		} else if (e.getSource() == removeButton) {
			removePhrase();
		} else if (e.getSource() == closeButton) {
			saveAllPhrases();
			dispose();
		}

	}

	public List<Phrase> getPhrases() {
		return phrases;
	}

	public void setPhrases(List<Phrase> phrases) {
		this.phrases = phrases;
		listAllPhrases();
	}

	//!doc
	// ===========================================================
	// Private inner class
	// ===========================================================

	private class PhrasesTableModel extends NTableModel<Phrase> {

		private static final long serialVersionUID = 1L;

		public PhrasesTableModel(String[] columns) {
			super(columns);
		}

		@Override
		public Object getValueAt(int rowIndex, int columnIndex) {
			if (rowIndex < 0 || rowIndex >=getRowCount()) {
				return "";
			}

			Phrase row = get(rowIndex);
			switch (columnIndex) {
				case 0: return row.getID();
				case 1: return row.getPhrase();
			}
			return "";
		}
	}

}
