#include <Precompiled.h>

#include <Resources/DeleteIcon.xpm>

#include <Common/SubjectTreeWidget.h>
#include <Common/SubjectUtils.h>

using namespace ::Neurotec;
using namespace ::Neurotec::Biometrics;
using namespace ::Neurotec::Biometrics::Client;

namespace Neurotec { namespace Samples
{

Node::Node(wxTreeCtrl * treeCtrl, NBiometricType type) :
	m_treeCtrl(treeCtrl),
	m_isNewNode(type != nbtNone),
	m_isSubjectNode(type == nbtNone),
	m_isBiometricNode(false),
	m_isGeneralizedNode(false),
	m_biometricType(type),
	m_sessionId(-1),
	m_position(nfpUnknown),
	m_impression(nfitUnknown)
{
}

Node::Node(wxTreeCtrl * treeCtrl, const NBiometric & biometric) :
	m_treeCtrl(treeCtrl),
	m_isNewNode(false),
	m_isSubjectNode(false),
	m_isBiometricNode(true),
	m_isGeneralizedNode(false),
	m_biometricType(nbtNone),
	m_sessionId(-1),
	m_position(nfpUnknown),
	m_impression(nfitUnknown)
{
	m_biometricType = biometric.GetBiometricType();
	m_sessionId = biometric.GetSessionId();
	m_isGeneralizedNode = false;
	m_items.push_back(biometric);
	if (m_biometricType == nbtFinger || m_biometricType == nbtPalm)
	{
		NFrictionRidge ridge = NObjectDynamicCast<NFrictionRidge>(biometric);
		m_position = ridge.GetPosition();
		m_impression = ridge.GetImpressionType();
	}
}

std::vector<NBiometric> Node::GetAllItems()
{
	return m_items;
}

std::vector<NBiometric> Node::GetItems()
{
	std::vector<NBiometric> result;
	for (std::vector<NBiometric>::iterator it = m_items.begin(); it != m_items.end(); it++)
	{
		if (!SubjectUtils::IsBiometricGeneralizationResult(*it))
		{
			result.push_back(*it);
		}
	}
	return result;
}

std::vector<NBiometric> Node::GetGeneralizedItems()
{
	std::vector<NBiometric> result;
	for (std::vector<NBiometric>::iterator it = m_items.begin(); it != m_items.end(); it++)
	{
		if (SubjectUtils::IsBiometricGeneralizationResult(*it))
		{
			result.push_back(*it);
		}
	}
	return result;
}

std::vector<NBiometric> Node::GetAllGeneralized()
{
	std::vector<NBiometric> result = GetGeneralizedItems();
	std::vector<Node*> children = GetChildren();
	for (std::vector<Node*>::iterator it = children.begin(); it != children.end(); it++)
	{
		std::vector<NBiometric> gen = (*it)->GetAllGeneralized();
		result.insert(result.end(), gen.begin(), gen.end());
	}
	return result;
}

Node * Node::GetParent()
{
	wxTreeItemId id = GetId();
	if (id.IsOk())
	{
		wxTreeItemId parent = m_treeCtrl->GetItemParent(id);
		if (parent.IsOk())
		{
			return reinterpret_cast<Node*>(m_treeCtrl->GetItemData(parent));
		}
	}
	return NULL;
}

std::vector<Node*> Node::GetChildren()
{
	std::vector<Node*> children;
	wxTreeItemId id = GetId();
	if (id.IsOk())
	{
		wxTreeItemIdValue cookie;
		wxTreeItemId child = m_treeCtrl->GetFirstChild(id, cookie);
		if (child.IsOk())
		{
			children.push_back(reinterpret_cast<Node*>(m_treeCtrl->GetItemData(child)));
			child = m_treeCtrl->GetNextChild(id, cookie);
			while(child.IsOk())
			{
				children.push_back(reinterpret_cast<Node*>(m_treeCtrl->GetItemData(child)));
				child = m_treeCtrl->GetNextChild(id, cookie);
			}
		}
	}
	return children;
}

bool Node::HasBiometric(const NBiometric & biometric)
{
	std::vector<NBiometric>::iterator it = std::find(m_items.begin(), m_items.end(), biometric);
	return it != m_items.end();
}

bool Node::BelongsToNode(const NBiometric & biometric)
{
	NBiometricType type = biometric.GetBiometricType();
	if (type == m_biometricType)
	{
		NInt sessionId = biometric.GetSessionId();
		if (type == nbtFinger || type == nbtPalm)
		{
			NFrictionRidge ridge = NObjectDynamicCast<NFrictionRidge>(biometric);
			if (m_position != ridge.GetPosition() || m_impression != ridge.GetImpressionType()) return false;
		}
		if (type == nbtFace && !m_items.empty())
		{
			if (m_sessionId == sessionId)
			{
				if ((biometric.GetParentObject().IsNull()) ^ (m_items[0].GetParentObject().IsNull())) return false;
			}
		}
		if (m_sessionId == -1 && sessionId == -1) return HasBiometric(biometric);
		if (m_sessionId == sessionId) return true;
		if (m_sessionId != -1 && sessionId == -1)
		{
			NBiometricAttributes parentObject = biometric.GetParentObject();
			if (!parentObject.IsNull())
			{
				NBiometric parent = parentObject.GetOwner<NBiometric>();
				if (!parent.IsNull()) return HasBiometric(parent);
			}
		}
	}
	return false;
}

Node::~Node()
{
	m_items.clear();
}

wxDECLARE_EVENT(wxEVT_SUBJECT_TREE_THREAD, wxCommandEvent);
wxDEFINE_EVENT(wxEVT_SUBJECT_TREE_THREAD, wxCommandEvent);
wxDEFINE_EVENT(wxEVT_TREE_SELECTED_ITEM_CHANGED, wxCommandEvent);

SubjectTreeWidget::SubjectTreeWidget(wxWindow *parent, wxWindowID id) :
	wxPanel(parent, id),
	m_isInUpdate(false),
	m_showBiometricsOnly(false),
	m_shownTypes((NBiometricType)(nbtFinger | nbtFace | nbtIris | nbtVoice | nbtPalm)),
	m_allowNew((NBiometricType)(nbtFinger | nbtFace | nbtIris | nbtVoice | nbtPalm)),
	m_subject(NULL)
{
	CreateGuiElements();
	RegisterGuiEvents();
}

SubjectTreeWidget::~SubjectTreeWidget()
{
	UnregisterGuiEvents();

	NSubject nullSubject = NULL;
	SetSubject(nullSubject);
}

void SubjectTreeWidget::SetSubject(const NSubject & subject)
{
	UnsetCallbacks();
	m_subject = subject;
	SetCallbacks();
	OnSubjectChanged();
}

NSubject SubjectTreeWidget::GetSubject()
{
	return m_subject;
}
void SubjectTreeWidget::SetShownTypes(NBiometricType value)
{
	if (m_shownTypes != value)
	{
		m_shownTypes = value;
		SetSelectedItem(NULL);
		OnSubjectChanged();
	}
}

NBiometricType SubjectTreeWidget::GetShownTypes()
{
	return m_shownTypes;
}

void SubjectTreeWidget::SetAllowNew(NBiometricType value)
{
	if (value != m_allowNew)
	{
		m_allowNew = value;
		if (m_allowNew != nbtNone) m_showBiometricsOnly = false;
		SetSelectedItem(NULL);
		OnSubjectChanged();
	}
}

NBiometricType SubjectTreeWidget::GetAllowNew()
{
	return m_allowNew;
}

void SubjectTreeWidget::SetAllowRemove(bool value)
{
	m_btnRemove->Show(value);
	Layout();
}

bool SubjectTreeWidget::GetAllowRemove() const
{
	return m_btnRemove->IsShown();
}

void SubjectTreeWidget::SetShowBiometricsOnly(bool value)
{
	if (m_showBiometricsOnly != value)
	{
		m_showBiometricsOnly = value;
		SetSelectedItem(NULL);
		if (m_showBiometricsOnly) m_allowNew = nbtNone;
		OnSubjectChanged();
	}
}

bool SubjectTreeWidget::GetShowBiometricsOnly()
{
	return m_showBiometricsOnly;
}

void SubjectTreeWidget::SetSelectedItem(Node * value)
{
	Node * current = GetSelectedItem();
	bool enabled = IsEnabled();
	if (current != value)
	{
		if (!value)
		{
			wxTreeItemId id = m_treeCtrl->GetSelection();
			bool changed = current != NULL;
			if (id.IsOk()) m_treeCtrl->SelectItem(id, false);
			if (enabled && changed && !m_isInUpdate)
			{
				wxTreeEvent evt(wxEVT_TREE_SEL_CHANGED, m_treeCtrl, id);
				wxPostEvent(m_treeCtrl, evt);
			}
		}
		else
		{
			wxTreeItemId id = value->GetId();
			if (id.IsOk())
			{
				m_treeCtrl->SelectItem(id);
				if (enabled && !m_isInUpdate)
				{
					wxTreeEvent evt(wxEVT_TREE_SEL_CHANGED, m_treeCtrl, id);
					wxPostEvent(m_treeCtrl, evt);
				}
			}
		}
	}
}

Node * SubjectTreeWidget::GetSelectedItem()
{
	wxTreeItemId id = m_treeCtrl->GetSelection();
	if (id.IsOk())
	{
		return reinterpret_cast<Node*>(m_treeCtrl->GetItemData(id));
	}
	return NULL;
}

void SubjectTreeWidget::UpdateTree()
{
	OnSubjectChanged();
}

Node * SubjectTreeWidget::GetBiometricNode(NBiometric & biometric)
{
	return GetNodeForBiometric(biometric);
}

Node * SubjectTreeWidget::GetNewNode(NBiometricType biometricType)
{
	return GetNodeForNonBiometric(biometricType);
}

Node * SubjectTreeWidget::GetSubjectNode()
{
	return GetNodeForNonBiometric(nbtNone);
}

Node * SubjectTreeWidget::GetNodeForBiometricInternal(const wxTreeItemId & id, const NBiometric & biometric)
{
	if (id.IsOk())
	{
		Node * n = reinterpret_cast<Node*>(m_treeCtrl->GetItemData(id));
		if (n && (n->HasBiometric(biometric) || n->BelongsToNode(biometric))) return n;
		else
		{
			wxTreeItemIdValue cookie;
			wxTreeItemId childId = m_treeCtrl->GetFirstChild(id, cookie);
			if (childId.IsOk())
			{
				n = GetNodeForBiometricInternal(childId, biometric);
				if (n) return n;
				childId = m_treeCtrl->GetNextChild(id, cookie);
				while(childId.IsOk())
				{
					n = GetNodeForBiometricInternal(childId, biometric);
					if (n) return n;
					childId = m_treeCtrl->GetNextChild(id, cookie);
				}
			}
		}
	}
	return NULL;
}

Node * SubjectTreeWidget::GetNodeForBiometric(const NBiometric & biometric)
{
	return GetNodeForBiometricInternal(m_treeCtrl->GetRootItem(), biometric);
}

Node * SubjectTreeWidget::GetNodeForNonBiometricInternal(const wxTreeItemId & id, NBiometricType type)
{
	if (id.IsOk())
	{
		Node * n = reinterpret_cast<Node*>(m_treeCtrl->GetItemData(id));
		if (n && n->GetBiometricType() == type) return n;
		else
		{
			wxTreeItemIdValue cookie;
			wxTreeItemId childId = m_treeCtrl->GetFirstChild(id, cookie);
			if (childId.IsOk())
			{
				n = GetNodeForNonBiometricInternal(childId, type);
				if (n) return n;
				childId = m_treeCtrl->GetNextChild(id, cookie);
				while(childId.IsOk())
				{
					n = GetNodeForNonBiometricInternal(childId, type);
					if (n) return n;
					childId = m_treeCtrl->GetNextChild(id, cookie);
				}
			}
		}
	}
	return NULL;
}

Node * SubjectTreeWidget::GetNodeForNonBiometric(NBiometricType type)
{
	return GetNodeForNonBiometricInternal(m_treeCtrl->GetRootItem(), type);
}

void SubjectTreeWidget::UpdateNodeText(Node * node)
{
	if (node->IsBiometricNode())
	{
		wxTreeItemId id = node->GetId();
		if (id.IsOk())
		{
			wxString msg = wxEmptyString;
			switch(node->GetBiometricType())
			{
			case nbtFinger:
			case nbtPalm:
				{
					NFrictionRidge first = NObjectDynamicCast<NFrictionRidge>(node->m_items[0]);
					msg = NEnum::ToString(NBiometricTypes::NFPositionNativeTypeOf(), first.GetPosition());
					wxString rolled = wxEmptyString;
					if (NBiometricTypes::IsImpressionTypeRolled(first.GetImpressionType())) msg += wxT(", Rolled");
					if ((int)node->m_items.size() > 1) msg += wxT(", Generalized");
					break;
				}
			case nbtFace:
				{
					int count = (int)node->m_items.size();
					NFace last = NObjectDynamicCast<NFace>(node->m_items[count - 1]);
					NLAttributes attributes = NULL;
					if (last.GetObjects().GetCount() > 0)
						attributes = last.GetObjects()[0];
					bool generalized = false;
					bool segmented = true;
					for (std::vector<NBiometric>::iterator it = node->m_items.begin(); it != node->m_items.end(); it++)
					{
						if (it->GetSessionId() != -1)
						{
							generalized = true;
							break;
						}
					}
					for (std::vector<NBiometric>::iterator it = node->m_items.begin(); it != node->m_items.end(); it++)
					{
						if (it->GetParentObject().IsNull())
						{
							segmented = false;
							break;
						}
					}
					msg = wxT("Face");
					if (!segmented && !attributes.IsNull() && attributes.GetQuality() != 254)
						msg += wxString::Format(wxT(" (Quality=%d)"), attributes.GetQuality());
					if (segmented)
						msg += wxT(", Segmented");
					if (generalized)
						msg += wxT(", Generalized");
					break;
				}
			case nbtIris:
				{
					NIris iris = NObjectDynamicCast<NIris>(node->m_items[0]);
					msg = NEnum::ToString(NBiometricTypes::NEPositionNativeTypeOf(), iris.GetPosition());
					msg = wxT("Iris (") + msg + wxT(")");
					break;
				}
			case nbtVoice:
				{
					NVoice voice = NObjectDynamicCast<NVoice>(node->m_items[0]);
					if (voice.GetParentObject().IsNull())
						msg = wxString::Format(wxT("Voice (Phrase id: %d)"), voice.GetPhraseId());
					else
						msg = wxT("Voice (Segmented)");
					break;
				}
			default:
				break;
			};

			m_treeCtrl->SetItemText(id, msg);
		}
	}
}

void SubjectTreeWidget::OnBiometricAdded(const NBiometric & biometric)
{
	NBiometricAttributes parentObject = NULL;

	Node * parent = NULL;
	Node * existingNode = GetNodeForBiometric(biometric);
	std::auto_ptr<Node> node;
	if (existingNode)
	{
		if (!existingNode->HasBiometric(biometric))
		{
			existingNode->m_items.push_back(biometric);
			existingNode->m_isGeneralizedNode = (int)existingNode->m_items.size() > 1;
			UpdateNodeText(existingNode);
		}
	}
	else
	{
		NBiometricType type = biometric.GetBiometricType();
		switch(type)
		{
		case nbtFinger:
		case nbtIris:
		case nbtVoice:
		case nbtFace:
			{
				node.reset(new Node(m_treeCtrl, biometric));
				parentObject = biometric.GetParentObject();
				if (!parentObject.IsNull())
				{
					NBiometric parentBiometric = parentObject.GetOwner<NBiometric>();
					if (!parentBiometric.IsNull())
						parent = GetNodeForBiometric(parentBiometric);
				}
				if (!parent && !m_showBiometricsOnly) parent = GetNodeForNonBiometric(type);
				break;
			}
		case nbtPalm:
			{
				node.reset(new Node(m_treeCtrl, biometric));
				if (!m_showBiometricsOnly) parent = GetNodeForNonBiometric(nbtPalm);
				break;
			}
		default:
			break;
		};

		existingNode = node.get();
		if (parent)
		{
			wxTreeItemId id = parent->GetId();
			wxTreeItemId insertId = m_treeCtrl->GetLastChild(id);
			if (insertId.IsOk())
			{
				insertId = m_treeCtrl->GetPrevSibling(insertId);
				m_treeCtrl->InsertItem(id, insertId, wxEmptyString, -1, -1, node.release());
			}
			else
			{
				m_treeCtrl->AppendItem(id, wxEmptyString, -1, -1, node.release());
			}
		}
		else
		{
			m_treeCtrl->AppendItem(m_treeCtrl->GetRootItem(), wxEmptyString, -1, -1, node.release());
		}
		UpdateNodeText(existingNode);

		m_treeCtrl->ExpandAll();
	}
}

void SubjectTreeWidget::OnBiometricRemoved(const NBiometric & biometric)
{
	Node * node = GetNodeForBiometric(biometric);
	if (node)
	{
		std::vector<NBiometric>::iterator f = std::find(node->m_items.begin(), node->m_items.end(), biometric);
		if (f != node->m_items.end())
		node->m_items.erase(f);
		if (node->m_items.empty())
		{
			wxTreeItemId id = node->GetId();
			wxTreeItemId parent = m_treeCtrl->GetItemParent(id);
			wxTreeItemIdValue cookie;
			wxTreeItemId child = m_treeCtrl->GetFirstChild(id, cookie);
			while(child.IsOk())
			{
				wxTreeItemId newId;
				wxString text = m_treeCtrl->GetItemText(child);
				wxColor color = m_treeCtrl->GetItemTextColour(child);
				wxTreeItemData * data = m_treeCtrl->GetItemData(child);
				m_treeCtrl->SetItemData(child, NULL);
				if (parent.IsOk())
				{
					newId = m_treeCtrl->InsertItem(parent, id, text, -1, -1, data);
				}
				else
				{
					newId = m_treeCtrl->InsertItem(m_treeCtrl->GetRootItem(), id, text, -1, -1, data);
				}
				m_treeCtrl->SetItemTextColour(newId, color);
				child = m_treeCtrl->GetNextChild(id, cookie);
			}
			m_treeCtrl->Delete(id);
		}
	}
}

void SubjectTreeWidget::OnSubjectChanged()
{
	Node * selected = GetSelectedItem();
	bool hadSelected = false;
	std::vector<NBiometric> selectedItems;
	NBiometricType type = nbtNone;
	wxColor grey(0xd3, 0xd3, 0xd3);

	m_isInUpdate = true;
	if (selected)
	{
		selectedItems = selected->GetAllItems();
		type = selected->GetBiometricType();
		hadSelected = true;
	}

	m_treeCtrl->DeleteChildren(m_treeCtrl->GetRootItem());
	if (!m_subject.IsNull())
	{
		std::vector<Node*> fingerNodes;
		std::vector<Node*> faceNodes;
		std::vector<Node*> irisNodes;
		std::vector<Node*> palmNodes;
		std::vector<Node*> voiceNodes;

		bool allowNewFingers = (m_allowNew & nbtFinger) == nbtFinger;
		bool allowNewFaces = (m_allowNew & nbtFace) == nbtFace;
		bool allowNewIrises = (m_allowNew & nbtIris) == nbtIris;
		bool allowNewVoices = (m_allowNew & nbtVoice) == nbtVoice;
		bool allowNewPalms = (m_allowNew & nbtPalm) == nbtPalm;

		wxTreeItemId id;
		if (!m_showBiometricsOnly) id = m_treeCtrl->AppendItem(m_treeCtrl->GetRootItem(), wxT("Subject"), -1, -1, new Node(m_treeCtrl, nbtNone));
		wxTreeItemId idc = id;

		if ((m_shownTypes & nbtFinger) == nbtFinger)
		{
			NArrayWrapper<NFinger> allFingers = m_subject.GetFingers().GetAll();
			if (allowNewFingers || allFingers.GetCount() > 0)
			{
				if (!m_showBiometricsOnly)
				{
					idc = m_treeCtrl->AppendItem(id, wxT("Fingers"), -1, -1, new Node(m_treeCtrl, nbtFinger));
					if (!allowNewFingers) m_treeCtrl->SetItemTextColour(idc, grey);
				}
				for (NArrayWrapper<NFinger>::iterator it = allFingers.begin(); it != allFingers.end(); it++)
				{
					OnBiometricAdded(*it);
				}
				if (!m_showBiometricsOnly)
				{
					wxTreeItemId newId = m_treeCtrl->AppendItem(idc, wxT("New..."), -1, -1, new Node(m_treeCtrl, nbtFinger));
					if (!allowNewFingers) m_treeCtrl->SetItemTextColour(newId, grey);
				}
			}
		}
		if ((m_shownTypes & nbtFace) == nbtFace)
		{
			NArrayWrapper<NFace> allFaces = m_subject.GetFaces().GetAll();
			if (allowNewFaces || allFaces.GetCount() > 0)
			{
				if (!m_showBiometricsOnly)
				{
					idc = m_treeCtrl->AppendItem(id, wxT("Faces"), -1, -1, new Node(m_treeCtrl, nbtFace));
					if (!allowNewFaces) m_treeCtrl->SetItemTextColour(idc, grey);
				}
				for (NArrayWrapper<NFace>::iterator it = allFaces.begin(); it != allFaces.end(); it++)
				{
					OnBiometricAdded(*it);
				}
				if (!m_showBiometricsOnly)
				{
					wxTreeItemId newId = m_treeCtrl->AppendItem(idc, wxT("New..."), -1, -1, new Node(m_treeCtrl, nbtFace));
					if (!allowNewFaces) m_treeCtrl->SetItemTextColour(newId, grey);
				}
			}
		}
		if ((m_shownTypes & nbtIris) == nbtIris)
		{
			NArrayWrapper<NIris> allIrises = m_subject.GetIrises().GetAll();
			if (allowNewIrises || allIrises.GetCount() > 0)
			{
				if (!m_showBiometricsOnly)
				{
					idc = m_treeCtrl->AppendItem(id, wxT("Irises"), -1, -1, new Node(m_treeCtrl, nbtIris));
					if (!allowNewIrises) m_treeCtrl->SetItemTextColour(idc, grey);
				}
				for (NArrayWrapper<NIris>::iterator it = allIrises.begin(); it != allIrises.end(); it++)
				{
					OnBiometricAdded(*it);
				}
				if (!m_showBiometricsOnly)
				{
					wxTreeItemId newId = m_treeCtrl->AppendItem(idc, wxT("New..."), -1, -1, new Node(m_treeCtrl, nbtIris));
					if (!allowNewIrises) m_treeCtrl->SetItemTextColour(newId, grey);
				}
			}
		}
		if ((m_shownTypes & nbtPalm) == nbtPalm)
		{
			NArrayWrapper<NPalm> allPalms = m_subject.GetPalms().GetAll();
			if (allowNewPalms || allPalms.GetCount() > 0)
			{
				if (!m_showBiometricsOnly)
				{
					idc = m_treeCtrl->AppendItem(id, wxT("Palms"), -1, -1, new Node(m_treeCtrl, nbtPalm));
					if (!allowNewPalms) m_treeCtrl->SetItemTextColour(idc, grey);
				}
				for (NArrayWrapper<NPalm>::iterator it = allPalms.begin(); it != allPalms.end(); it++)
				{
					OnBiometricAdded(*it);
				}
				if (!m_showBiometricsOnly)
				{
					wxTreeItemId newId = m_treeCtrl->AppendItem(idc, wxT("New..."), -1, -1, new Node(m_treeCtrl, nbtPalm));
					if (!allowNewPalms) m_treeCtrl->SetItemTextColour(newId, grey);
				}
			}
		}
		if ((m_shownTypes & nbtVoice) == nbtVoice)
		{
			NArrayWrapper<NVoice> allVoices = m_subject.GetVoices().GetAll();
			if (allowNewVoices || allVoices.GetCount() > 0)
			{
				if (!m_showBiometricsOnly)
				{
					idc = m_treeCtrl->AppendItem(id, wxT("Voices"), -1, -1, new Node(m_treeCtrl, nbtVoice));
					if (!allowNewVoices) m_treeCtrl->SetItemTextColour(idc, grey);
				}
				for (NArrayWrapper<NVoice>::iterator it = allVoices.begin(); it != allVoices.end(); it++)
				{
					OnBiometricAdded(*it);
				}
				if (!m_showBiometricsOnly)
				{
					wxTreeItemId newId = m_treeCtrl->AppendItem(idc, wxT("New..."), -1, -1, new Node(m_treeCtrl, nbtVoice));
					if (!allowNewVoices) m_treeCtrl->SetItemTextColour(newId, grey);
				}
			}
		}

		if (hadSelected)
		{
			Node * node;
			if (selectedItems.empty())
				node = GetNodeForNonBiometric(type);
			else
			{
				NBiometric first = selectedItems[0];
				node = GetNodeForBiometric(first);
			}
			SetSelectedItem(node);
		}
	}

	m_isInUpdate = false;

	m_treeCtrl->ExpandAll();
}

void SubjectTreeWidget::OnSelectedItemChanging(wxTreeEvent & event)
{
	wxTreeItemId id = event.GetItem();
	if (id.IsOk())
	{
		Node * node = reinterpret_cast<Node*>(m_treeCtrl->GetItemData(id));
		if (node && node->IsNewNode() && (m_allowNew & node->GetBiometricType()) == nbtNone)
		{
			event.Veto();
		}
	}
}

void SubjectTreeWidget::OnSelectedItemChanged(wxTreeEvent & /*event*/)
{
	Node * selected = GetSelectedItem();
	m_btnRemove->Enable(selected && selected->IsBiometricNode());
	if (IsEnabled() && !m_isInUpdate)
	{
		wxCommandEvent evt(wxEVT_TREE_SELECTED_ITEM_CHANGED, GetId());
		wxPostEvent(this, evt);
	}
}

void SubjectTreeWidget::OnThread(wxCommandEvent &event)
{
	int id = event.GetId();
	switch(id)
	{
	case ID_ADD_BIOMETRIC:
		{
			NBiometric biometric(reinterpret_cast<HNBiometric>(event.GetClientData()), true);
			if (!biometric.IsNull())
			{
				OnBiometricAdded(biometric);
			}
			break;
		}
	case ID_REMOVE_BIOMETRIC:
		{
			NBiometric biometric(reinterpret_cast<HNBiometric>(event.GetClientData()), true);
			if (!biometric.IsNull())
			{
				OnBiometricRemoved(biometric);
			}
			break;
		}
	case ID_RESET:
		OnSubjectChanged();
		break;
	default:
		break;
	};
}

void SubjectTreeWidget::SetCallbacks()
{
	if (!m_subject.IsNull())
	{
		m_subject.GetFingers().AddCollectionChangedCallback(&SubjectTreeWidget::FingerCollectionChangedCallback, this);
		m_subject.GetFaces().AddCollectionChangedCallback(&SubjectTreeWidget::FaceCollectionChangedCallback, this);
		m_subject.GetIrises().AddCollectionChangedCallback(&SubjectTreeWidget::IrisCollectionChangedCallback, this);
		m_subject.GetPalms().AddCollectionChangedCallback(&SubjectTreeWidget::PalmCollectionChangedCallback, this);
		m_subject.GetVoices().AddCollectionChangedCallback(&SubjectTreeWidget::VoiceCollectionChangedCallback, this);
	}
}

void SubjectTreeWidget::UnsetCallbacks()
{
	if (!m_subject.IsNull())
	{
		m_subject.GetFingers().RemoveCollectionChangedCallback(&SubjectTreeWidget::FingerCollectionChangedCallback, this);
		m_subject.GetFaces().RemoveCollectionChangedCallback(&SubjectTreeWidget::FaceCollectionChangedCallback, this);
		m_subject.GetIrises().RemoveCollectionChangedCallback(&SubjectTreeWidget::IrisCollectionChangedCallback, this);
		m_subject.GetPalms().RemoveCollectionChangedCallback(&SubjectTreeWidget::PalmCollectionChangedCallback, this);
		m_subject.GetVoices().RemoveCollectionChangedCallback(&SubjectTreeWidget::VoiceCollectionChangedCallback, this);
	}
}

template <class T>
void SubjectTreeWidget::BiometricCollectionChangedCallback(Collections::CollectionChangedEventArgs<T> args)
{
	SubjectTreeWidget *subjectTree = reinterpret_cast<SubjectTreeWidget *>(args.GetParam());
	switch(args.GetAction())
	{
	case Neurotec::Collections::nccaAdd:
		{
			for (int i = 0; i < args.GetNewItems().GetCount(); i++)
			{
				wxCommandEvent event(wxEVT_SUBJECT_TREE_THREAD, ID_ADD_BIOMETRIC);
				event.SetClientData(args.GetNewItems()[i].RefHandle());
				wxPostEvent(subjectTree, event);
			}
			break;
		}
	case Neurotec::Collections::nccaReset:
		{
			wxCommandEvent event(wxEVT_SUBJECT_TREE_THREAD, ID_RESET);
			wxPostEvent(subjectTree, event);
			break;
		}
	case Neurotec::Collections::nccaRemove:
		{
			for (int i = 0; i < args.GetOldItems().GetCount(); i++)
			{
				wxCommandEvent event(wxEVT_SUBJECT_TREE_THREAD, ID_REMOVE_BIOMETRIC);
				event.SetClientData(args.GetOldItems()[i].RefHandle());
				wxPostEvent(subjectTree, event);
			}
			break;
		}
	default:
		break;
	};
}

void SubjectTreeWidget::FingerCollectionChangedCallback(Collections::CollectionChangedEventArgs<NFinger> args)
{
	SubjectTreeWidget::BiometricCollectionChangedCallback<NFinger>(args);
}

void SubjectTreeWidget::FaceCollectionChangedCallback(Collections::CollectionChangedEventArgs<NFace> args)
{
	SubjectTreeWidget::BiometricCollectionChangedCallback<NFace>(args);
}

void SubjectTreeWidget::IrisCollectionChangedCallback(Collections::CollectionChangedEventArgs<NIris> args)
{
	SubjectTreeWidget::BiometricCollectionChangedCallback<NIris>(args);
}

void SubjectTreeWidget::PalmCollectionChangedCallback(Collections::CollectionChangedEventArgs<NPalm> args)
{
	SubjectTreeWidget::BiometricCollectionChangedCallback<NPalm>(args);
}

void SubjectTreeWidget::VoiceCollectionChangedCallback(Collections::CollectionChangedEventArgs<NVoice> args)
{
	SubjectTreeWidget::BiometricCollectionChangedCallback<NVoice>(args);
}

void SubjectTreeWidget::OnRemoveClick(wxCommandEvent&)
{
	Node * selection = GetSelectedItem();
	SetSelectedItem(NULL);
	if (selection && selection->IsBiometricNode())
	{
		switch (selection->GetBiometricType())
		{
			case nbtFace:
				{
					std::vector<NBiometric> allItems;
					Node * parent = selection->GetParent();
					if (parent && parent->IsBiometricNode())
					{
						allItems = parent->GetItems();
						for (std::vector<NBiometric>::iterator it = allItems.begin(); it != allItems.end(); it++)
						{
							NFace face = NObjectDynamicCast<NFace>(*it);
							NInt index = m_subject.GetFaces().IndexOf(face);
							if (index != -1) m_subject.GetFaces().RemoveAt(index);
						}
					}

					allItems = selection->GetAllItems();
					for (std::vector<NBiometric>::iterator it = allItems.begin(); it != allItems.end(); it++)
					{
						NFace face = NObjectDynamicCast<NFace>(*it);
						NInt index = m_subject.GetFaces().IndexOf(face);
						if (index != -1) m_subject.GetFaces().RemoveAt(index);
					}
					break;
				}
			case nbtPalm:
				{
					std::vector<NBiometric> allItems = selection->GetAllItems();
					for (std::vector<NBiometric>::iterator it = allItems.begin(); it != allItems.end(); it++)
					{
						NPalm palm = NObjectDynamicCast<NPalm>(*it);
						NInt index = m_subject.GetPalms().IndexOf(palm);
						if (index != -1) m_subject.GetPalms().RemoveAt(index);
					}
					break;
				}
			case nbtVoice:
				{
					NVoice voice = NObjectDynamicCast<NVoice>(selection->m_items[0]);
					NVoice relatedVoice = NULL;
					NBiometricAttributes parentObject = voice.GetParentObject();
					if (!parentObject.IsNull())
						relatedVoice = parentObject.GetOwner<NVoice>();
					else
					{
						NSAttributes attributes = NULL;
						if (voice.GetObjects().GetCount() > 0)
							attributes = voice.GetObjects()[0];
						if (!attributes.IsNull())
							relatedVoice = NObjectDynamicCast<NVoice>(attributes.GetChild());
					}

					int index = m_subject.GetVoices().IndexOf(voice);
					if (index != -1) m_subject.GetVoices().RemoveAt(index);
					index = -1;
					if (!relatedVoice.IsNull()) index = m_subject.GetVoices().IndexOf(relatedVoice);
					if (index != -1) m_subject.GetVoices().RemoveAt(index);
					break;
				}
			case nbtFinger:
				{
					std::vector<NBiometric> items = selection->GetItems();
					std::vector<NBiometric> generalized = selection->GetGeneralizedItems();
					for (std::vector<NBiometric>::iterator it = items.begin(); it != items.end(); it++)
					{
						NInt index;
						NFinger finger = NObjectDynamicCast<NFinger>(*it);
						NBiometricAttributes parentObject = finger.GetParentObject();
						if (!parentObject.IsNull())
						{
							NFinger ownerFinger = parentObject.GetOwner<NFinger>();
							if (!ownerFinger.IsNull())
							{
								index = m_subject.GetFingers().IndexOf(ownerFinger);
								if (index != -1) m_subject.GetFingers().RemoveAt(index);
							}
						}
						index = m_subject.GetFingers().IndexOf(finger);
						if (index != -1) m_subject.GetFingers().RemoveAt(index);
					}
					for (std::vector<NBiometric>::iterator it = generalized.begin(); it != generalized.end(); it++)
					{
						NInt index = m_subject.GetFingers().IndexOf(NObjectDynamicCast<NFinger>(*it));
						if (index != -1) m_subject.GetFingers().RemoveAt(index);
					}
					break;
				}
			case nbtIris:
				{
					NInt index;
					NIris iris = NObjectDynamicCast<NIris>(selection->m_items[0]);
					NBiometricAttributes parentObject = iris.GetParentObject();
					if (!parentObject.IsNull())
					{
						NIris ownerIris = parentObject.GetOwner<NIris>();
						index = m_subject.GetIrises().IndexOf(ownerIris);
						if (index != -1) m_subject.GetIrises().RemoveAt(index);
					}
					index = m_subject.GetIrises().IndexOf(iris);
					if (index != -1) m_subject.GetIrises().RemoveAt(index);
					break;
				}
			default:
				break;
		};
	}
}

void SubjectTreeWidget::RegisterGuiEvents()
{
	Bind(wxEVT_SUBJECT_TREE_THREAD, &SubjectTreeWidget::OnThread, this);
	Bind(wxEVT_TREE_SEL_CHANGED, &SubjectTreeWidget::OnSelectedItemChanged, this);
	Bind(wxEVT_TREE_SEL_CHANGING, &SubjectTreeWidget::OnSelectedItemChanging, this);
	m_btnRemove->Connect(wxEVT_BUTTON, wxCommandEventHandler(SubjectTreeWidget::OnRemoveClick), NULL, this);
}

void SubjectTreeWidget::UnregisterGuiEvents()
{
	Unbind(wxEVT_SUBJECT_TREE_THREAD, &SubjectTreeWidget::OnThread, this);
	Unbind(wxEVT_TREE_SEL_CHANGED, &SubjectTreeWidget::OnSelectedItemChanged, this);
	Unbind(wxEVT_TREE_SEL_CHANGING, &SubjectTreeWidget::OnSelectedItemChanging, this);
	m_btnRemove->Disconnect(wxEVT_BUTTON, wxCommandEventHandler(SubjectTreeWidget::OnRemoveClick), NULL, this);
}

void SubjectTreeWidget::CreateGuiElements()
{
	wxBoxSizer *sizer = new wxBoxSizer(wxVERTICAL);
	this->SetSizer(sizer);

	m_btnRemove = new wxButton(this, wxID_ANY, wxT("Remove selected"), wxDefaultPosition, wxDefaultSize, 0 | wxNO_BORDER);
	m_btnRemove->SetBitmap(wxImage(wxImage(deleteIcon_xpm)));
	m_btnRemove->Fit();
	sizer->Add(m_btnRemove, 0, wxALL);
	m_btnRemove->Enable(false);

	m_treeCtrl = new wxTreeCtrl(this, wxID_ANY, wxDefaultPosition, wxDefaultSize, wxTR_HIDE_ROOT | wxTR_HAS_BUTTONS);
	m_treeCtrl->AddRoot(wxT("Invisible"), -1, -1, NULL);
	sizer->Add(m_treeCtrl, 1 , wxALL | wxEXPAND);

	this->Layout();
}

}}

