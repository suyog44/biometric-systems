package com.neurotec.samples.abis.tabbedview;

import java.awt.BorderLayout;
import java.awt.Dimension;
import java.awt.FlowLayout;
import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;

import javax.swing.BorderFactory;
import javax.swing.JButton;
import javax.swing.JDialog;
import javax.swing.JLabel;
import javax.swing.JPanel;
import javax.swing.JTextField;

import com.neurotec.samples.util.Utils;

public class GetSubjectDialog extends JDialog implements ActionListener {

	private static final long serialVersionUID = 1L;

	private DialogAction action;

	private JButton btnCancel;
	private JButton btnOk;
	private JLabel lblSubjectId;
	private JPanel panelBottom;
	private JPanel panelTop;
	private JTextField tfSubjectId;

	public GetSubjectDialog() {
		action = DialogAction.NONE;
		initGUI();
	}

	private void initGUI() {
		panelTop = new JPanel();
		lblSubjectId = new JLabel();
		tfSubjectId = new JTextField();
		panelBottom = new JPanel();
		btnOk = new JButton();
		btnCancel = new JButton();

		setLayout(new BorderLayout());

		panelTop.setBorder(BorderFactory.createEmptyBorder(5, 5, 1, 5));
		panelTop.setLayout(new BorderLayout(5, 5));

		lblSubjectId.setText("Subject ID:");
		panelTop.add(lblSubjectId, BorderLayout.WEST);

		tfSubjectId.setPreferredSize(new Dimension(200, 20));
		panelTop.add(tfSubjectId, BorderLayout.CENTER);

		add(panelTop, BorderLayout.NORTH);

		panelBottom.setBorder(BorderFactory.createEmptyBorder(1, 5, 5, 5));
		panelBottom.setLayout(new FlowLayout(FlowLayout.TRAILING));

		btnOk.setText("OK");
		btnOk.setPreferredSize(new Dimension(70, 23));
		panelBottom.add(btnOk);

		btnCancel.setText("Cancel");
		btnCancel.setPreferredSize(new Dimension(70, 23));
		panelBottom.add(btnCancel);

		add(panelBottom, BorderLayout.SOUTH);

		setIconImage(Utils.createIconImage("images/Logo16x16.png"));
		pack();

		btnOk.addActionListener(this);
		btnCancel.addActionListener(this);
	}

	public String getSubjectId() {
		return tfSubjectId.getText();
	}

	public DialogAction getAction() {
		return action;
	}

	@Override
	public void actionPerformed(ActionEvent ev) {
		if (ev.getSource().equals(btnOk)) {
			action = DialogAction.OK;
			dispose();
		} else if (ev.getSource().equals(btnCancel)) {
			action = DialogAction.CANCEL;
			dispose();
		}
	}

}
