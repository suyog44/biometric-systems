package com.neurotec.samples.biometrics.standards;

import java.awt.Dimension;
import java.awt.GridLayout;
import java.awt.Toolkit;
import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;
import java.awt.event.KeyEvent;
import java.awt.event.KeyListener;

import javax.swing.BorderFactory;
import javax.swing.JButton;
import javax.swing.JDialog;
import javax.swing.JLabel;
import javax.swing.JOptionPane;
import javax.swing.JPanel;
import javax.swing.JSplitPane;
import javax.swing.JTextField;

public final class AddFingerViewForm extends JDialog implements KeyListener, ActionListener {

	// ==============================================
	// Private static fields
	// ==============================================

	private static final long serialVersionUID = -5831585798742221550L;

	// ==============================================
	// Private fields
	// ==============================================

	private int hSize;
	private int vSize;
	private int vResolution;
	private int hResolution;

	private JButton btnOk;
	private JButton btnCancel;
	private JLabel lblVSize;
	private JLabel lblVResolution;
	private JLabel lblHSize;
	private JLabel lblHResolution;
	private JTextField textVSize;
	private JTextField textHSize;
	private JTextField textVResolution;
	private JTextField textHResolution;
	private final MainFrame frame;

	// ==============================================
	// Public constructor
	// ==============================================

	public AddFingerViewForm(MainFrame owner, String title) {
		super();
		setResizable(false);
		initializeComponents();
		Dimension dim = Toolkit.getDefaultToolkit().getScreenSize();
		setLocation(dim.width / 2 - getSize().width / 2, dim.height / 2	- getSize().height / 2);
		setTitle(title);
		frame = owner;
	}

	// ==============================================
	// Private methods
	// ==============================================

	private void initializeComponents() {

		// btnOk
		btnOk = new JButton("OK");
		btnOk.addActionListener(this);

		// btnCancel
		btnCancel = new JButton("Cancel");
		btnCancel.addActionListener(this);

		// labels
		lblVSize = new JLabel("Vertical Size");
		lblHSize = new JLabel("Horizontal Size");
		lblVResolution = new JLabel("Vertical Resolution");
		lblHResolution = new JLabel("Horizontal Resolution");

		// textVSize
		textVSize = new JTextField();
		textVSize.addKeyListener(this);
		textVSize.setText("400");

		// textHSize
		textHSize = new JTextField();
		textHSize.addKeyListener(this);
		textHSize.setText("400");

		// textVResolution
		textVResolution = new JTextField();
		textVResolution.addKeyListener(this);
		textVResolution.setText("500");

		// textHResolution
		textHResolution = new JTextField();
		textHResolution.addKeyListener(this);
		textHResolution.setText("500");

		JSplitPane mainSplitPane = new JSplitPane(JSplitPane.VERTICAL_SPLIT);
		JPanel settings = new JPanel();
		settings.setBorder(BorderFactory.createEmptyBorder(5, 5, 5, 5));
		settings.setLayout(new GridLayout(2, 4, 5, 5));
		settings.add(lblVSize);
		settings.add(textVSize);
		settings.add(lblHSize);
		settings.add(textHSize);
		settings.add(lblVResolution);
		settings.add(textVResolution);
		settings.add(lblHResolution);
		settings.add(textHResolution);

		mainSplitPane.setLeftComponent(settings);

		JPanel buttons = new JPanel();

		buttons.setBorder(BorderFactory.createEmptyBorder(5, 5, 5, 5));
		buttons.setLayout(new GridLayout(1, 2, 5, 5));
		buttons.add(btnOk);
		buttons.add(btnCancel);

		mainSplitPane.setRightComponent(buttons);
		add(mainSplitPane);

		pack();
	}

	private String getVSize() {
		return textVSize.getText();
	}

	private String getHSize() {
		return textHSize.getText();
	}

	private String getVResolution() {
		return textVResolution.getText();
	}

	private String getHResolution() {
		return textHResolution.getText();
	}

	// ==============================================
	// Public methods
	// ==============================================

	public int getHorzSize() {
		return hSize;
	}

	public int getVertSize() {
		return vSize;
	}

	public int getVertResolution() {
		return vResolution;
	}

	public int getHorzResolution() {
		return hResolution;
	}


	@Override
	public void keyPressed(KeyEvent e) {
		// Do nothing.
	}

	@Override
	public  void keyReleased(KeyEvent e) {
		// Do nothing.
	}

	@Override
	public  void keyTyped(KeyEvent e) {
		char c = e.getKeyChar();
		if (((c < '0') || (c > '9')) && (c != KeyEvent.VK_BACK_SPACE)) {
			e.consume();
			Toolkit.getDefaultToolkit().beep();
		}
	}

	@Override
	public void actionPerformed(ActionEvent ev) {
		Object source = ev.getSource();
		if (source == btnOk) {
			try {
				hSize = Integer.parseInt(getHSize());
				vSize = Integer.parseInt(getVSize());
				vResolution = Integer.parseInt(getVResolution());
				hResolution = Integer.parseInt(getHResolution());
				frame.setDialogResultOK(true);
				dispose();
			} catch (NumberFormatException e) {
				e.printStackTrace();
				JOptionPane.showMessageDialog(this,	"Parameters can't be parsed!", "FMRecord Editor", JOptionPane.ERROR_MESSAGE);
			}
		} else if (source == btnCancel) {
			frame.setDialogResultOK(false);
			dispose();
		}

	}

}
