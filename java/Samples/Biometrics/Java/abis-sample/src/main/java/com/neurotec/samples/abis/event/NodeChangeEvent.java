package com.neurotec.samples.abis.event;

import java.util.EventObject;

import javax.swing.tree.TreeNode;

public class NodeChangeEvent extends EventObject {

	private static final long serialVersionUID = 1L;

	private final TreeNode node;

	public NodeChangeEvent(Object source, TreeNode node) {
		super(source);
		this.node = node;
	}

	public TreeNode getNode() {
		return node;
	}

}
