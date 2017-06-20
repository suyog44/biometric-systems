package com.neurotec.samples.abis.swing;

import java.awt.event.ActionEvent;
import java.util.ArrayList;
import java.util.Collections;
import java.util.List;

import javax.swing.AbstractAction;
import javax.swing.JTextField;
import javax.swing.KeyStroke;
import javax.swing.SwingUtilities;
import javax.swing.event.DocumentEvent;
import javax.swing.event.DocumentListener;
import javax.swing.text.BadLocationException;

public class AutoCompleteTextField extends JTextField {

	// ===========================================================
	// Nested classes
	// ===========================================================

	private static enum AutocompleteState {
		OFF,
		ON
	};

	private static class Autocomplete implements DocumentListener {

		private final AutoCompleteTextField textField;
		private final List<String> autocompleteList;

		Autocomplete(AutoCompleteTextField textField, List<String> autocompleteList) {
			this.textField = textField;
			this.autocompleteList = autocompleteList;
			Collections.sort(this.autocompleteList);
		}

		@Override
		public void changedUpdate(DocumentEvent ev) {
			// Do nothing.
		}

		@Override
		public void removeUpdate(DocumentEvent ev) {
			// Do nothing.
		}

		@Override
		public void insertUpdate(DocumentEvent ev) {
			if (ev.getLength() != 1) {
				return;
			}

			int pos = ev.getOffset();
			String content = null;
			try {
				content = textField.getText(0, pos + 1);
			} catch (BadLocationException e) {
				throw new AssertionError("Can't happen");
			}

			// Find where the word starts.
			int w;
			for (w = pos; w >= 0; w--) {
				if (!Character.isLetter(content.charAt(w))) {
					break;
				}
			}

			// Too few chars.
			if (pos - w < 2) {
				return;
			}

			//String prefix = content.substring(w + 1).toLowerCase();
			String prefix = content.substring(w + 1);
			int n = Collections.binarySearch(autocompleteList, prefix);
			if (n < 0 && -n <= autocompleteList.size()) {
				String match = autocompleteList.get(-n - 1);
				if (match.startsWith(prefix)) {

					// A completion is found.
					String completion = match.substring(pos - w);

					// We cannot modify Document from within notification, so we submit a task that does the change later.
					SwingUtilities.invokeLater(new CompletionTask(textField, completion, pos + 1));
				}
			} else {
				// Nothing found
				textField.setAutocompleteState(AutocompleteState.OFF);
			}
		}

	}

	private static class CommitAction extends AbstractAction {

		private static final long serialVersionUID = 1L;

		private final AutoCompleteTextField textField;
		private AutocompleteState state;

		CommitAction(AutoCompleteTextField textField, AutocompleteState mode) {
			this.textField = textField;
			this.state = mode;
		}

		public void setAutocompleteState(AutocompleteState autocompleteState) {
			this.state = autocompleteState;
		}

		@Override
		public void actionPerformed(ActionEvent ev) {
			if (state == AutocompleteState.ON) {
				int pos = textField.getSelectionEnd();
				textField.setCaretPosition(pos);
				textField.setAutocompleteState(AutocompleteState.OFF);
			} else {
				textField.replaceSelection("\t");
			}
		}

	}

	private static class CompletionTask implements Runnable {

		private final AutoCompleteTextField textField;
		private final String completion;
		private final int position;

		CompletionTask(AutoCompleteTextField textField, String completion, int position) {
			this.textField = textField;
			this.completion = completion;
			this.position = position;
		}

		@Override
		public void run() {
			StringBuilder sb = new StringBuilder(textField.getText());
			sb.insert(position, completion);
			textField.setText(sb.toString());
			textField.setCaretPosition(position + completion.length());
			textField.moveCaretPosition(position);
			textField.setAutocompleteState(AutocompleteState.ON);
		}

	}

	// ===========================================================
	// Private static fields
	// ===========================================================

	private static final long serialVersionUID = 1L;

	private static final String COMMIT_ACTION = AutoCompleteTextField.class .getName() + ".commit";

	// ===========================================================
	// Private fields
	// ===========================================================

	private final Autocomplete autocomplete;
	private final List<String> autocompleteList;
	private final CommitAction commitAction;

	// ===========================================================
	// Public constructor
	// ===========================================================

	public AutoCompleteTextField() {

		// Without this, cursor always leaves text field
		setFocusTraversalKeysEnabled(false);

		autocompleteList = new ArrayList<String>();
		autocomplete = new Autocomplete(this, autocompleteList);
		getDocument().addDocumentListener(autocomplete);

		getInputMap().put(KeyStroke.getKeyStroke("TAB"), COMMIT_ACTION);
		commitAction = new CommitAction(this, AutocompleteState.OFF);
		getActionMap().put(COMMIT_ACTION, commitAction);
	}

	// ===========================================================
	// Package private methods
	// ===========================================================

	void setAutocompleteState(AutocompleteState state) {
		commitAction.setAutocompleteState(state);
	}

	// ===========================================================
	// Public methods
	// ===========================================================

	public void addAutocomplete(String text) {
		autocompleteList.add(text);
		Collections.sort(autocompleteList);
	}

	public void removeAutocomplete(String text) {
		autocompleteList.remove(text);
	}

	public void addAutocomplete(List<String> texts) {
		autocompleteList.addAll(texts);
		Collections.sort(autocompleteList);
	}

	public void clearAutocomplete() {
		autocompleteList.clear();
	}

	public boolean containsAutocomplete(String text) {
		return autocompleteList.contains(text);
	}

	public List<String> getAutocomplete() {
		return new ArrayList<String>(autocompleteList);
	}

}
