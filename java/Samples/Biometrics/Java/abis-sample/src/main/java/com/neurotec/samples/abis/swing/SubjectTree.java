package com.neurotec.samples.abis.swing;

import java.awt.BorderLayout;
import java.awt.Component;
import java.awt.Dimension;
import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;
import java.awt.event.MouseAdapter;
import java.awt.event.MouseEvent;
import java.util.ArrayList;
import java.util.Arrays;
import java.util.EnumSet;
import java.util.Enumeration;
import java.util.List;

import javax.swing.JButton;
import javax.swing.JPanel;
import javax.swing.JScrollPane;
import javax.swing.JToolBar;
import javax.swing.JTree;
import javax.swing.SwingConstants;
import javax.swing.UIManager;
import javax.swing.event.TreeSelectionEvent;
import javax.swing.event.TreeSelectionListener;
import javax.swing.tree.DefaultMutableTreeNode;
import javax.swing.tree.DefaultTreeCellRenderer;
import javax.swing.tree.DefaultTreeModel;
import javax.swing.tree.MutableTreeNode;
import javax.swing.tree.TreePath;
import javax.swing.tree.TreeSelectionModel;

import com.neurotec.biometrics.NBiometric;
import com.neurotec.biometrics.NBiometricAttributes;
import com.neurotec.biometrics.NBiometricType;
import com.neurotec.biometrics.NBiometricTypes;
import com.neurotec.biometrics.NEAttributes;
import com.neurotec.biometrics.NFAttributes;
import com.neurotec.biometrics.NFace;
import com.neurotec.biometrics.NFinger;
import com.neurotec.biometrics.NFrictionRidge;
import com.neurotec.biometrics.NIris;
import com.neurotec.biometrics.NLAttributes;
import com.neurotec.biometrics.NPalm;
import com.neurotec.biometrics.NSAttributes;
import com.neurotec.biometrics.NSubject;
import com.neurotec.biometrics.NVoice;
import com.neurotec.samples.abis.subject.SubjectUtils;
import com.neurotec.samples.abis.event.NodeChangeEvent;
import com.neurotec.samples.abis.event.NodeChangeListener;
import com.neurotec.samples.abis.util.CollectionUtils;
import com.neurotec.samples.abis.util.SwingUtils;
import com.neurotec.util.NCollectionChangedAction;
import com.neurotec.util.NObjectCollection;
import com.neurotec.util.event.NCollectionChangeEvent;
import com.neurotec.util.event.NCollectionChangeListener;

public class SubjectTree extends JPanel implements NCollectionChangeListener, TreeSelectionListener, ActionListener {

	// ===========================================================
	// Nested classes
	// ===========================================================

	class TitledTreeNodeRenderer extends DefaultTreeCellRenderer {

		private static final long serialVersionUID = 1L;

		@Override
		public Component getTreeCellRendererComponent(JTree parentTree, Object value, boolean sel, boolean expanded, boolean leaf, int row, boolean hasFocus) {
			super.getTreeCellRendererComponent(parentTree, value, sel, expanded, leaf, row, hasFocus);
			if (value instanceof Node) {
				Node node = (Node) value;
				if (node.isEnabled()) {
					this.setEnabled(true);
				} else {
					this.setEnabled(false);
				}
				if ((node.getUserObject() instanceof NBiometric) || (NEW_NODE_NAME.equals(node.getText()))) {
					setIcon(UIManager.getIcon("Tree.leafIcon"));
				} else {
					setIcon(UIManager.getIcon("Tree.openIcon"));
				}
			}

			return this;
		}

	}

	// ===========================================================
	// Static fields
	// ===========================================================

	private static final long serialVersionUID = 1L;

	private static final String NEW_NODE_NAME = "New...";

	public static final String PROPERTY_CHANGE_SUBJECT = "Subject";
	public static final String PROPERTY_CHANGE_SELECTED_ITEM = "SelectedItem";

	// ===========================================================
	// Static fields
	// ===========================================================

	private boolean showBiometricsOnly;
	private NSubject subject;
	private final EnumSet<NBiometricType> shownTypes;
	private EnumSet<NBiometricType> allowedNewTypes;

	private final List<NodeChangeListener> nodeChangeListeners;
	private boolean updating;

	private JTree tree;

	private JButton btnRemoveSelected;
	private JScrollPane spTree;
	private JToolBar tbSubjectTree;

	// ===========================================================
	// Public constructor
	// ===========================================================

	public SubjectTree() {
		super();
		shownTypes = EnumSet.of(NBiometricType.FINGER, NBiometricType.FACE, NBiometricType.IRIS, NBiometricType.PALM, NBiometricType.VOICE);
		allowedNewTypes = EnumSet.of(NBiometricType.FINGER, NBiometricType.FACE, NBiometricType.IRIS, NBiometricType.PALM, NBiometricType.VOICE);
		nodeChangeListeners = new ArrayList<NodeChangeListener>();
		initGUI();
	}

	// ===========================================================
	// Private methods
	// ===========================================================

	private void initGUI() {
		tbSubjectTree = new JToolBar();
		btnRemoveSelected = new JButton();
		spTree = new JScrollPane();

		setLayout(new BorderLayout());

		tbSubjectTree.setFloatable(false);
		tbSubjectTree.setRollover(true);

		btnRemoveSelected.setText("Remove selected");
		btnRemoveSelected.setFocusable(false);
		btnRemoveSelected.setHorizontalTextPosition(SwingConstants.CENTER);
		btnRemoveSelected.setVerticalTextPosition(SwingConstants.BOTTOM);
		tbSubjectTree.add(btnRemoveSelected);

		add(tbSubjectTree, BorderLayout.NORTH);
		spTree.setPreferredSize(new Dimension(200, 0));
		add(spTree, BorderLayout.CENTER);

		tree = new JTree();
		tree.setLargeModel(true);
		tree.setModel(null);
		tree.setCellRenderer(new TitledTreeNodeRenderer());
		tree.addTreeSelectionListener(this);
		tree.getSelectionModel().setSelectionMode(TreeSelectionModel.SINGLE_TREE_SELECTION);
		tree.addMouseListener(new MouseAdapter() {

			@Override
			public void mousePressed(MouseEvent e) {
				super.mouseClicked(e);
				int row = tree.getClosestRowForLocation(e.getX(), e.getY());
				if (row == -1) {
					tree.clearSelection();
				}
			}

		});
		spTree.setViewportView(tree);

		btnRemoveSelected.addActionListener(this);
	}

	@SuppressWarnings("unchecked")
	private Node getNodeWithTag(Object tag) {
		if (tag == null) throw new NullPointerException("tag");
		Node root = (Node) tree.getModel().getRoot();
		for (Enumeration<Node> e = root.breadthFirstEnumeration(); e.hasMoreElements(); ) {
			Node node = e.nextElement();
			NodeTag t = (NodeTag) node.getUserObject();
			if (t.hasTag(tag) || t.belongsToNode(tag)) {
				return node;
			}
			Node n = getNodeWithTag(node, tag);
			if (n != null) {
				return n;
			}
		}
		return null;
	}

	@SuppressWarnings("unchecked")
	private Node getNodeWithTag(Node root, Object tag) {
		if (tag == null) throw new NullPointerException("tag");
		for (Enumeration<Node> e = root.children(); e.hasMoreElements(); ) {
			Node node = e.nextElement();
			NodeTag t = (NodeTag) node.getUserObject();
			if (t.hasTag(tag) || t.belongsToNode(tag)) {
				return node;
			}
			Node n = getNodeWithTag(node, tag);
			if (n != null) {
				return n;
			}
		}
		return null;
	}

	@SuppressWarnings("unchecked")
	private List<Node> getAllNodesWithTag(Object tag) {
		if (tag == null) throw new NullPointerException("tag");
		List<Node> nodes = new ArrayList<Node>(2);
		Node root = (Node) tree.getModel().getRoot();
		for (Enumeration<Node> e = root.breadthFirstEnumeration(); e.hasMoreElements(); ) {
			Node node = e.nextElement();
			NodeTag t = (NodeTag) node.getUserObject();
			if (t.hasTag(tag) || t.belongsToNode(tag)) {
				nodes.add(node);
			}
		}
		return nodes;
	}

	@SuppressWarnings("unchecked")
	private Node getLastNode(Object object)	{
		if (object == null) throw new NullPointerException("object");
		Node root = (Node) tree.getModel().getRoot();
		Node lastNode = null;
		for (Enumeration<Node> e = root.breadthFirstEnumeration(); e.hasMoreElements(); ) {
			Node node = e.nextElement();
			if (object.equals((node))) {
				lastNode = node;
			}
		}
		return lastNode;
	}

	// Reload model while retaining the selected item. Use "updating" flag to suppress unnecessary selection events.
	private void reloadModel() {
		updating = true;
		Node selected = getSelectedItem();
		((DefaultTreeModel) tree.getModel()).reload();
		if ((selected != null) && containsItem(selected)) {
			setSelectedItem(selected);
		}
		updating = false;
	}

	private void subjectChanged() {
		updating = true;
		if (subject == null) {
			tree.setModel(null);
		} else {
			Node selected = getSelectedItem();

			List<Node> fingerNodes = new ArrayList<Node>();
			List<Node> faceNodes = new ArrayList<Node>();
			List<Node> iriseNodes = new ArrayList<Node>();
			List<Node> palmNodes = new ArrayList<Node>();
			List<Node> voiceNodes = new ArrayList<Node>();

			boolean allowNewFingers = getAllowedNewTypes().contains(NBiometricType.FINGER);
			boolean allowNewFaces = getAllowedNewTypes().contains(NBiometricType.FACE);
			boolean allowNewIrises = getAllowedNewTypes().contains(NBiometricType.IRIS);
			boolean allowNewPalms = getAllowedNewTypes().contains(NBiometricType.PALM);
			boolean allowNewVoices = getAllowedNewTypes().contains(NBiometricType.VOICE);

			if (getShownTypes().contains(NBiometricType.FINGER)) {
				List<NFinger> allFingers = subject.getFingers();
				List<List<NFinger>> groups = SubjectUtils.getFingersGeneralizationGroups(allFingers);
				List<List<NFinger>> topLevelGroups = new ArrayList<List<NFinger>>();
				for (List<NFinger> g : groups) {
					boolean topLevel = true;
					for (NFinger f : g) {
						if (f.getSessionId() != -1 && f.getParentObject() != null) {
							topLevel = false;
						}
					}
					if (topLevel) {
						topLevelGroups.add(g);
					}
				}
				for (List<NFinger> g : topLevelGroups) {
					fingerNodes.add(createFingerNode(allFingers, g));
				}
				if (!isShowBiometricsOnly()) {
					Node node = new Node(new NodeTag(subject.getFingers(), true), NEW_NODE_NAME);
					node.setEnabled(allowNewFingers);
					fingerNodes.add(node);
				}
			}

			if (getShownTypes().contains(NBiometricType.FACE)) {
				List<NFace> allFaces = subject.getFaces();
				List<NFace[]> groups = SubjectUtils.getFaceGeneralizationGroups(allFaces);
				List<NFace[]> topLevelGroups = new ArrayList<NFace[]>();
				for (NFace[] g : groups) {
					boolean topLevel = true;
					for (NFace f : g) {
						if ((f == null) || ((f.getSessionId() != -1) && (f.getParentObject() != null))) {
							topLevel = false;
						}
					}
					if (topLevel) {
						topLevelGroups.add(g);
					}
				}
				for (NFace[] group : topLevelGroups) {
					faceNodes.add(createFaceNode(allFaces, Arrays.asList(group)));
				}
				if (!isShowBiometricsOnly()) {
					Node node = new Node(new NodeTag(subject.getFaces(), true), NEW_NODE_NAME);
					node.setEnabled(allowNewFaces);
					faceNodes.add(node);
				}
			}

			if (getShownTypes().contains(NBiometricType.IRIS)) {
				for (NIris iris : subject.getIrises()) {
					if (iris.getParentObject() == null) {
						iriseNodes.add(createIrisNode(iris));
					}
				}
				if (!isShowBiometricsOnly()) {
					Node node = new Node(new NodeTag(subject.getIrises(), true), NEW_NODE_NAME);
					node.setEnabled(allowNewIrises);
					iriseNodes.add(node);
				}
			}

			if (getShownTypes().contains(NBiometricType.VOICE)) {
				for (NVoice voice : subject.getVoices()) {
					if (voice.getParentObject() == null) {
						voiceNodes.add(createVoiceNode(voice));
					}
				}
				if (!isShowBiometricsOnly()) {
					Node node = new Node(new NodeTag(subject.getVoices(), true), NEW_NODE_NAME);
					node.setEnabled(allowNewVoices);
					voiceNodes.add(node);
				}
			}

			if (getShownTypes().contains(NBiometricType.PALM)) {
				for (List<NPalm> group : SubjectUtils.getPalmsGeneralizationGroups(subject)) {
					palmNodes.add(createPalmNode(group));
				}
				if (!isShowBiometricsOnly()) {
					Node node = new Node(new NodeTag(subject.getPalms(), true), NEW_NODE_NAME);
					node.setEnabled(allowNewPalms);
					palmNodes.add(node);
				}
			}

			Node subjectNode = new Node(new NodeTag(subject, true), "Subject");
			tree.setModel(new DefaultTreeModel(subjectNode));
			if (isShowBiometricsOnly()) {
				tree.setRootVisible(false);
				for (MutableTreeNode node: fingerNodes) {
					subjectNode.add(node);
				}
				for (MutableTreeNode node: faceNodes) {
					subjectNode.add(node);
				}
				for (MutableTreeNode node: iriseNodes) {
					subjectNode.add(node);
				}
				for (MutableTreeNode node: palmNodes) {
					subjectNode.add(node);
				}
				for (MutableTreeNode node: voiceNodes) {
					subjectNode.add(node);
				}
			} else {
				tree.setRootVisible(true);
				if (shownTypes.contains(NBiometricType.FINGER) && (allowNewFingers || (fingerNodes.size() > 1))) {
					Node node = new Node(new NodeTag(subject.getFingers(), true), "Fingers");
					node.setEnabled(allowNewFingers);
					for (MutableTreeNode fingerNode : fingerNodes) {
						node.add(fingerNode);
					}
					subjectNode.add(node);
				}
				if (shownTypes.contains(NBiometricType.FACE) && (allowNewFaces || (faceNodes.size() > 1))) {
					Node node = new Node(new NodeTag(subject.getFaces(), true), "Faces");
					node.setEnabled(allowNewFaces);
					for (MutableTreeNode faceNode : faceNodes) {
						node.add(faceNode);
					}
					subjectNode.add(node);
				}
				if (shownTypes.contains(NBiometricType.IRIS) && (allowNewIrises || (iriseNodes.size() > 1))) {
					Node node = new Node(new NodeTag(subject.getIrises(), true), "Irises");
					node.setEnabled(allowNewIrises);
					for (MutableTreeNode irisNode : iriseNodes) {
						node.add(irisNode);
					}
					subjectNode.add(node);
				}
				if (shownTypes.contains(NBiometricType.PALM) && (allowNewPalms || (palmNodes.size() > 1))) {
					Node node = new Node(new NodeTag(subject.getPalms(), true), "Palms");
					node.setEnabled(allowNewPalms);
					for (MutableTreeNode palmNode : palmNodes) {
						node.add(palmNode);
					}
					subjectNode.add(node);
				}
				if (shownTypes.contains(NBiometricType.VOICE) && (allowNewVoices || (voiceNodes.size() > 1))) {
					Node node = new Node(new NodeTag(subject.getVoices(), true), "Voices");
					node.setEnabled(allowNewVoices);
					for (MutableTreeNode voiceNode : voiceNodes) {
						node.add(voiceNode);
					}
					subjectNode.add(node);
				}
			}

			reloadModel();
			expandTree();
			if ((selected != null)) {
				setSelectedItem(getNodeFor(CollectionUtils.getFirst(selected.getItems())));
			}
			updating = false;
		}
	}

	private Node createFingerNode(List<NFinger> allFingers, List<NFinger> fingers) {
		NodeTag tag = new NodeTag(fingers);
		Node node = new Node(tag);
		NFinger first = CollectionUtils.getFirst(fingers);
		if (first != null && first.getObjects() != null) {
			for (NFAttributes attribute : first.getObjects()) {
				NFinger child = (NFinger)attribute.getChild();
				if (child != null && !fingers.contains(child) && !tag.belongsToNode(child)) {
					List<NFinger> grouped = SubjectUtils.getFingersInSameGroup(allFingers, child);
					node.add(createFingerNode(allFingers, grouped));
				}
			}
		}
		updateFingerNodeText(node);
		return node;
	}

	private void updateFingerNodeText(Node node) {
		NodeTag tag = (NodeTag) node.getUserObject();
		if (tag != null) {
			NFrictionRidge first = tag.getItems() != null ? (NFrictionRidge)CollectionUtils.getFirst(tag.getItems()) : null;
			if (first != null) {
				String rolled = "";
				if (first.getImpressionType().isRolled()) {
					rolled = ", Rolled";
				}
				String text = String.format("%s%s", first.getPosition(), rolled);
				if (tag.getItems().size() > 1) {
					text += ", Generalized";
				}
				node.setText(text);
			}
		}
	}

	private Node createFaceNode(List<NFace> allFaces, List<NFace> faces) {
		NodeTag tag = new NodeTag(faces);
		Node node = new Node(tag);
		NFace first = CollectionUtils.getFirst(faces);
		if (first != null && first.getObjects() != null) {
			for (NLAttributes attribute : first.getObjects()) {
				NFace child = (NFace) attribute.getChild();
				if (child != null && !faces.contains(child) && !tag.belongsToNode(child)) {
					List<NFace> grouped = SubjectUtils.getFacesInSameGroup(allFaces.toArray(new NFace[allFaces.size()]), child);
					node.add(createFaceNode(allFaces, grouped));
				}
			}
		}
		updateFaceNodeText(node);
		return node;
	}

	private Node createFaceNode(List<NFace> faces) {
		Node node = new Node(new NodeTag(faces));
		updateFaceNodeText(node);
		return node;
	}

	private void updateFaceNodeText(Node node) {
		NodeTag tag = (NodeTag)node.getUserObject();
		if (tag != null) {
			String text = "Face";
			NFace face = (NFace) tag.getItems().get(tag.getItems().size() - 1);
			if (face != null) {
				NLAttributes attributes = CollectionUtils.getFirst(face.getObjects());
				if (face.getParentObject() != null) 
					text += ", Segmented";
				else if (attributes != null && attributes.getQuality() != NBiometricTypes.QUALITY_UNKNOWN) 
					text += String.format(" (Quality=%d)", attributes.getQuality() & 0xFF);
				if (tag.getItems().size() > 1)
					text += ", Generalized";
				node.setText(text);
			}
		}
	}

	private Node createIrisNode(NIris iris) {
		Node node = new Node(new NodeTag(iris), String.format("Iris (%s)", iris.getPosition()));
		for (NEAttributes attribute : iris.getObjects()) {
			NIris child = (NIris) attribute.getChild();
			if (child != null) {
				node.add(createIrisNode(child));
			}
		}
		return node;
	}

	private Node createVoiceNode(NVoice voice) {
		String text = String.format("Voice (%s)", voice.getParentObject() != null ? "Segmented" : "Phrase id: " + voice.getPhraseID());
		Node node = new Node(new NodeTag(voice), text);
		for (NSAttributes attribute : (NSAttributes[]) voice.getObjects().toArray()) {
			NVoice child = (NVoice) attribute.getChild();
			if (child != null) {
				node.add(createVoiceNode(child));
			}
		}
		return node;
	}

	private Node createPalmNode(List<NPalm> palm) {
		Node node = new Node(new NodeTag(palm));
		updateFingerNodeText(node);
		return node;
	}

	private void onBiometricRemoved(NBiometric biometric) {
		for (Node node : getAllNodesWithTag(biometric)) {
			DefaultTreeModel model = (DefaultTreeModel) tree.getModel();
			DefaultMutableTreeNode parent = (DefaultMutableTreeNode) node.getParent();
			NodeTag tag = (NodeTag) node.getUserObject();
			tag.getItems().remove(biometric);
			if (tag.getItems().isEmpty()) {
				List<MutableTreeNode> childNodes = new ArrayList<MutableTreeNode>();
				for (int i = 0; i < model.getChildCount(node); i++) {
					childNodes.add((MutableTreeNode) model.getChild(node, i));
				}
				model.removeNodeFromParent(node);
				int offset;
				if (parent != null && parent.getChildCount() > 0 && ((DefaultMutableTreeNode) parent.getChildAt(parent.getChildCount() - 1)).getUserObject() instanceof NObjectCollection) {
					offset = 1;
				} else {
					offset = 0;
				}
				for (MutableTreeNode childNode : childNodes) {
					model.insertNodeInto(childNode, parent, parent.getChildCount() - offset);
				}
				reloadModel();
				expandTree();
				fireNodeRemoved(new NodeChangeEvent(this, node));
			}
			if (!showBiometricsOnly) {
				if (tag.getItems().isEmpty() && parent != null && parent.getChildCount() == 1) {
					tag = (NodeTag) parent.getUserObject();
					if (tag.getItems().isEmpty() && (!allowedNewTypes.containsAll(tag.getType()))) {
						model.removeNodeFromParent(parent);
					}
				}
			}
		}
	}

	private void onBiometricAdded(NBiometric biometric, int index) {
		Node node;
		Node parent = null;
		NBiometricAttributes parentObject = null;

		node = getNodeWithTag(biometric);

		if (node != null) {
			NodeTag tag = (NodeTag) node.getUserObject();
			if (!tag.getItems().contains(biometric)) {
				tag.getItems().add(biometric);
				if (tag.getType().contains(NBiometricType.FACE))
					updateFaceNodeText(node);
				else if (tag.getType().contains(NBiometricType.FINGER) || tag.getType().contains(NBiometricType.PALM))
					updateFingerNodeText(node);
			}
		} else {
			if (biometric.getBiometricType().contains(NBiometricType.FACE)) {
				node = createFaceNode(Arrays.asList((NFace) biometric));
				parentObject = biometric.getParentObject();
				if (parentObject != null) parent = getNodeWithTag(parentObject.getOwner());
				if (parentObject == null && !showBiometricsOnly) parent = getNodeWithTag(subject.getFaces());
			} else if (biometric.getBiometricType().contains(NBiometricType.FINGER)) {
				List<NFinger> fingers = subject.getFingers();
				List<NFinger> group = SubjectUtils.getFingersInSameGroup(fingers, (NFinger)biometric);
				node = createFingerNode(fingers, group);
				parentObject = biometric.getParentObject();
				if (parentObject != null) parent = getNodeWithTag(parentObject.getOwner());
				if (parentObject == null && !showBiometricsOnly) parent = getNodeWithTag(subject.getFingers());
			} else if (biometric.getBiometricType().contains(NBiometricType.IRIS)) {
				node = createIrisNode((NIris) biometric);
				parentObject = biometric.getParentObject();
				if (parentObject != null) parent = getNodeWithTag(parentObject.getOwner());
				if (parentObject == null && !showBiometricsOnly) parent = getNodeWithTag(subject.getIrises());
			} else if (biometric.getBiometricType().contains(NBiometricType.PALM)) {
				List<NPalm> palms = subject.getPalms();
				List<NPalm> group = SubjectUtils.getPalmsInSameGroup(palms, (NPalm)biometric);
				node = createPalmNode(group);
				if (!showBiometricsOnly) parent = getNodeWithTag(subject.getPalms());
			} else if (biometric.getBiometricType().contains(NBiometricType.VOICE)) {
				node = createVoiceNode((NVoice) biometric);
				parentObject = biometric.getParentObject();
				if (parentObject != null) parent = getNodeWithTag(parentObject.getOwner());
				if (parentObject == null && !showBiometricsOnly) parent = getNodeWithTag(subject.getVoices());
			} else {
				throw new IllegalArgumentException("Unsupported biometric type: " + biometric.getBiometricType());
			}
			if (parent == null) {
				addNode(node);
			} else {
				insertNode(parent, node, index);
			}
		}
	}

	private void addNode(Node node) {
		DefaultMutableTreeNode root = (DefaultMutableTreeNode) tree.getModel().getRoot();
		DefaultTreeModel model = (DefaultTreeModel) tree.getModel();
		model.insertNodeInto(node, root, root.getChildCount());
		expandTree();
		fireNodeAdded(new NodeChangeEvent(this, node));
	}

	private void insertNode(Node parent, Node node, int index) {
		DefaultTreeModel model = (DefaultTreeModel) tree.getModel();
		if (parent.getUserObject() instanceof NBiometric) {
			index = parent.getChildCount();
			NBiometric biometric = (NBiometric)parent.getUserObject();
			if (getAllowedNewTypes().retainAll(biometric.getBiometricType()) && index != 0) {
				index--;
			}
		}

		model.insertNodeInto(node, parent, parent.getChildCount());
		if (model.isLeaf(node)) {
			tree.expandPath(new TreePath(parent.getPath()));
		} else {
			tree.expandPath(new TreePath(node.getPath()));
		}
		fireNodeAdded(new NodeChangeEvent(this, node));
	}

	private void removeSelected() {
		Node selection = getSelectedItem();

		if (selection != null && selection.isBiometricNode()) {
			if (selection.getBiometricType().contains(NBiometricType.FACE)) {
				for (NBiometric item : selection.getGeneralizedItems()) {
					subject.getFaces().remove((NFace) item);
				}
				for (NBiometric item : selection.getItems()) {
					if (item.getParentObject() != null) {
						NLAttributes parentItem = (NLAttributes) item.getParentObject();
						subject.getFaces().remove(parentItem.getOwner());
					}
					subject.getFaces().remove(item);
				}
			} else if (selection.getBiometricType().contains(NBiometricType.FINGER)) {
				for (NBiometric item : selection.getGeneralizedItems()) {
					subject.getFingers().remove(item);
				}
				for (NBiometric item : selection.getItems()) {
					NFinger finger = (NFinger) item;
					NFAttributes parent = (NFAttributes) finger.getParentObject();
					if (parent != null) {
						NFinger owner = (NFinger) parent.getOwner();
						subject.getFingers().remove(owner);
					}
					subject.getFingers().remove(finger);
				}
			} else if (selection.getBiometricType().contains(NBiometricType.IRIS)) {
				NIris iris = (NIris) CollectionUtils.getFirst(selection.getItems());
				if (iris != null) {
					NEAttributes parent = (NEAttributes) iris.getParentObject();
					if (parent != null) {
						NIris owner = parent.getOwner();
						subject.getIrises().remove(owner);
					}
					subject.getIrises().remove(iris);
				}
			} else if (selection.getBiometricType().contains(NBiometricType.PALM)) {
				for (NBiometric item : selection.getGeneralizedItems()) {
					subject.getPalms().remove(item);
				}
				for (NBiometric item : selection.getItems()) {
					subject.getPalms().remove(item);
				}
			} else if (selection.getBiometricType().contains(NBiometricType.VOICE)) {
				NVoice voice = (NVoice) CollectionUtils.getFirst(selection.getAllItems());
				if (voice != null) {
					NVoice relatedVoice = null;
					if (voice.getParentObject() != null) {
						relatedVoice = (NVoice) voice.getParentObject().getOwner();
					} else {
						NSAttributes attributes = CollectionUtils.getFirst(voice.getObjects());
						if (attributes != null) {
							relatedVoice = (NVoice) attributes.getChild();
						}
					}
					subject.getVoices().remove(voice);
					subject.getVoices().remove(relatedVoice);
				}
			}
		}
	}

	private void fireNodeAdded(NodeChangeEvent ev) {
		for (NodeChangeListener l : nodeChangeListeners) {
			l.nodeAdded(ev);
		}
	}

	private void fireNodeRemoved(NodeChangeEvent ev) {
		for (NodeChangeListener l : nodeChangeListeners) {
			l.nodeRemoved(ev);
		}
	}

	// ===========================================================
	// Public methods
	// ===========================================================

	public void addNodeChangeListener(NodeChangeListener l) {
		nodeChangeListeners.add(l);
	}

	public void removeNodeChangeListener(NodeChangeListener l) {
		nodeChangeListeners.remove(l);
	}

	public NSubject getSubject() {
		return subject;
	}

	public void setSubject(NSubject subject) {
		NSubject oldSubject = this.subject;
		if (this.subject != subject) {
			if (this.subject != null) {
				this.subject.getFingers().removeCollectionChangeListener(this);
				this.subject.getFaces().removeCollectionChangeListener(this);
				this.subject.getIrises().removeCollectionChangeListener(this);
				this.subject.getVoices().removeCollectionChangeListener(this);
				this.subject.getPalms().removeCollectionChangeListener(this);
			}
			this.subject = subject;
			if (this.subject != null) {
				this.subject.getFingers().addCollectionChangeListener(this);
				this.subject.getFaces().addCollectionChangeListener(this);
				this.subject.getIrises().addCollectionChangeListener(this);
				this.subject.getVoices().addCollectionChangeListener(this);
				this.subject.getPalms().addCollectionChangeListener(this);
			}
			subjectChanged();
			firePropertyChange(PROPERTY_CHANGE_SUBJECT, oldSubject, this.subject);
		}
	}

	public Node getSelectedItem() {
		if (tree.getSelectionPath() == null) {
			return null;
		}
		return (Node) tree.getSelectionPath().getLastPathComponent();
	}

	public void setSelectedItem(Node value) {
		Node current = getSelectedItem();

		if (current != value) {
			if (value == null) {
				boolean changed = current != null;
				if (isEnabled() && changed) {
					tree.setSelectionPath(null);
				}
			} else {
				tree.setSelectionPath(new TreePath(getLastNode(value).getPath()));
			}
		}
	}

	public boolean containsItem(Object item) {
		return getNodeWithTag(item) != null;
	}

	public EnumSet<NBiometricType> getShownTypes() {
		return EnumSet.copyOf(shownTypes);
	}

	public void setShownTypes(EnumSet<NBiometricType> shownTypes) {
		if (!this.shownTypes.equals(shownTypes)) {
			this.shownTypes.clear();
			this.shownTypes.addAll(shownTypes);
			setSelectedItem(null);
			subjectChanged();
		}
	}

	public EnumSet<NBiometricType> getAllowedNewTypes() {
		return EnumSet.copyOf(allowedNewTypes);
	}

	public void setAllowedNewTypes(EnumSet<NBiometricType> allowedNewTypes) {
		if (!this.allowedNewTypes.equals(allowedNewTypes)) {
			this.allowedNewTypes.clear();
			this.allowedNewTypes.addAll(allowedNewTypes);
			setSelectedItem(null);
			subjectChanged();
		}
	}

	public boolean isShowRemoveButton() {
		return tbSubjectTree.isEnabled();
	}

	public void setShowRemoveButton(boolean showRemoveButton) {
		if (this.tbSubjectTree.isEnabled() != showRemoveButton) {
			this.tbSubjectTree.setVisible(showRemoveButton);;
		}
	}
	
	public boolean isShowBiometricsOnly() {
		return showBiometricsOnly;
	}

	public void setShowBiometricsOnly(boolean showBiometricsOnly) {
		if (this.showBiometricsOnly != showBiometricsOnly) {
			this.showBiometricsOnly = showBiometricsOnly;
			setSelectedItem(null);
			if (showBiometricsOnly) {
				allowedNewTypes = EnumSet.noneOf(NBiometricType.class);
			}
			subjectChanged();
		}
	}

	public void expandTree() {
		for (int i = 0; i < tree.getRowCount(); i++) {
			tree.expandRow(i);
		}
	}

	@Override
	public void setEnabled(boolean enabled) {
		super.setEnabled(enabled);
		tree.setEnabled(enabled);
		Node selected = getSelectedItem();
		btnRemoveSelected.setEnabled(enabled && selected != null && selected.isBiometricNode());
	}

	@Override
	public void collectionChanged(final NCollectionChangeEvent ev) {
		if ((ev.getSource() instanceof NObjectCollection) && subject.equals(((NObjectCollection<?>) ev.getSource()).getOwner())) {
			SwingUtils.runOnEDT(new Runnable() {
				@Override
				public void run() {
					if (ev.getAction() == NCollectionChangedAction.RESET) {
						// Remove all nodes before reseting subject to make sure NodeChangeListeners are notified.
						DefaultMutableTreeNode root = (DefaultMutableTreeNode) tree.getModel().getRoot();
						root.removeAllChildren();
						// Reload model directly to make sure selection listeners are notified.
						((DefaultTreeModel) tree.getModel()).reload();
						subjectChanged();
					} else if (ev.getAction() == NCollectionChangedAction.REMOVE) {
						for (Object item : ev.getOldItems()) {
							onBiometricRemoved((NBiometric) item);
						}
					} else if (ev.getAction() == NCollectionChangedAction.ADD) {
						for (Object item : ev.getNewItems()) {
							onBiometricAdded((NBiometric) item, ev.getNewIndex());
						}
					}
				}
			});
		}
	}

	@Override
	public void actionPerformed(ActionEvent ev) {
		if (ev.getSource().equals(btnRemoveSelected)) {
			removeSelected();
		}
	}

	@Override
	public void valueChanged(TreeSelectionEvent e) {
		if (!updating) {
			TreePath newPath = e.getNewLeadSelectionPath();
			Object newValue;
			if (newPath == null) {
				newValue = null;
			} else {
				newValue = newPath.getLastPathComponent();
			}
			Node current = null;
			if (newValue instanceof Node) {
				current = (Node) newValue;
				if (!current.isEnabled()) {
					tree.setSelectionPath(null);
					return;
				}
			}
			btnRemoveSelected.setEnabled(isEnabled() && current != null && current.isBiometricNode());

			TreePath oldPath = e.getOldLeadSelectionPath();
			Object oldValue;
			if (oldPath == null) {
				oldValue = null;
			} else {
				oldValue = oldPath.getLastPathComponent();
			}
			firePropertyChange(PROPERTY_CHANGE_SELECTED_ITEM, oldValue, newValue);
		}
	}

	public Node getBiometricNode(NBiometric biometric) {
		return getNodeFor(biometric);
	}

	public Node getNewNode(NBiometricType biometricType) {
		if (subject != null) {
			switch (biometricType) {
				case FACE: return getNodeFor(subject.getFaces());
				case FINGER: return getNodeFor(subject.getFingers());
				case IRIS: return  getNodeFor(subject.getIrises());
				case PALM: return getNodeFor(subject.getPalms());
				case VOICE: return getNodeFor(subject.getVoices());
				default: return null;
			}
		}
		return null;
	}

	public Node getSubjectNode() {
		return getNodeFor(subject);
	}

	public Node getNodeFor(Object param) {
		if (param instanceof Node) return (Node)param;
		if (param != null) {
			return getNodeWithTag(param);
		}
		return null;
	}

	public void updateTree() {
		subjectChanged();
	}

}
