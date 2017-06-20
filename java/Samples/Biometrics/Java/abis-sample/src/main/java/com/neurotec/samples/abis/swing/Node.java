package com.neurotec.samples.abis.swing;

import java.util.ArrayList;
import java.util.EnumSet;
import java.util.Enumeration;
import java.util.List;

import javax.swing.tree.DefaultMutableTreeNode;

import com.neurotec.biometrics.NBiometric;
import com.neurotec.biometrics.NBiometricType;
import com.neurotec.biometrics.NSubject;
import com.neurotec.samples.abis.subject.SubjectUtils;

public final class Node extends DefaultMutableTreeNode {

	// ===========================================================
	// Private static fields
	// ===========================================================

	private static final long serialVersionUID = 1L;

	// ===========================================================
	// Private fields
	// ===========================================================

	private boolean isNewNode;
	private boolean isSubjectNode;

	private EnumSet<NBiometricType> biometricType;
	private List<? extends NBiometric> allItems;

	private String text;
	private boolean enabled;

	// ===========================================================
	// Public constructor
	// ===========================================================

	public Node(NodeTag tag) {
		this(tag, null);
	}

	public Node(NodeTag tag, String text) {
		super(tag);
		biometricType = tag.getType();
		isSubjectNode = tag.getObjectTag() instanceof NSubject;
		allItems = tag.getItems();
		this.text = text;
		enabled = true;
	}

	// ===========================================================
	// Public methods
	// ===========================================================

	public List<Node> getChildren() {
		List<Node> childNodes = new ArrayList<Node>();
		Enumeration childrenEnumeration = children();
		while (childrenEnumeration.hasMoreElements()) {
			Object child = childrenEnumeration.nextElement();
			if (child instanceof Node) {
				childNodes.add((Node) child);
			}
		}
		return childNodes;
	}

	public List<NBiometric> getAllGeneralized() {
		List<NBiometric> result = new ArrayList<NBiometric>();
		result.addAll(getGeneralizedItems());
		for (Node child : getChildren()) {
			result.addAll(child.getAllGeneralized());
		}
		return result;
	}

	public boolean isNewNode() {
		return isNewNode;
	}

	public boolean isSubjectNode() {
		return isSubjectNode;
	}

	public boolean isBiometricNode() {
		return getAllItems().size() > 0;
	}

	public boolean isGeneralizedNode() {
		return allItems.size() > 1;
	}

	public EnumSet<NBiometricType> getBiometricType() {
		return biometricType;
	}

	public List<? extends NBiometric> getAllItems() {
		return allItems;
	}

	public List<? extends NBiometric> getItems() {
		List<NBiometric> items = new ArrayList<NBiometric>();
		for (NBiometric item : getAllItems()) {
			if (!SubjectUtils.isBiometricGeneralizationResult(item)) {
				items.add(item);
			}
		}
		return items;
	}

	public List<? extends NBiometric> getGeneralizedItems() {
		List<NBiometric> generalizedItems = new ArrayList<NBiometric>();
		for (NBiometric item : getAllItems()) {
			if (SubjectUtils.isBiometricGeneralizationResult(item)) {
				generalizedItems.add(item);
			}
		}
		return generalizedItems;
	}

	public String getText() {
		if (text == null) {
			return super.toString();
		} else {
			return text;
		}
	}

	public void setText(String text) {
		this.text = text;
	}

	public boolean isEnabled() {
		return enabled;
	}

	public void setEnabled(boolean enabled) {
		this.enabled = enabled;
	}

	@Override
	public String toString() {
		return getText();
	}

	@Override
	public boolean equals(Object obj) {
		if (this == obj) {
			return true;
		}
		if (!(obj instanceof Node)) {
			return false;
		}
		Node node = (Node) obj;
		if (text == null) {
			if (node.text != null) {
				return false;
			}
		} else if (!text.equals(node.text)) {
			return false;
		}
		if (getUserObject() == null) {
			if (node.getUserObject() != null) {
				return false;
			}
		} else if (!getUserObject().equals(node.getUserObject())) {
			return false;
		}
		return true;
	}

	@Override
	public int hashCode() {
		int result = 17;
		int c = 0;
		if (text != null) {
			c = text.hashCode();
		}
		result = 31 * result + c;
		if (getUserObject() == null) {
			c = 0;
		} else {
			c = getUserObject().hashCode();
		}
		return 31 * result + c;
	}

}
