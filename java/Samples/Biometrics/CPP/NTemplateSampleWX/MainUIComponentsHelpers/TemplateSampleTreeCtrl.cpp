#include "Precompiled.h"
#include "TemplateSampleTreeCtrl.h"
#include "TreeNodeData.h"

using namespace Neurotec;
using namespace Neurotec::Biometrics;

namespace Neurotec { namespace Samples { namespace MainUIComponentsHelpers
{
	TemplateSampleTreeCtrl::TemplateSampleTreeCtrl(wxWindow *parent, wxWindowID id, const wxPoint& pos, const wxSize& size, long style) :
		wxTreeCtrl(parent, id, pos, size, style)
	{
		m_fileName = N_TMPL_NTMPL;
	}

	TemplateSampleTreeCtrl::~TemplateSampleTreeCtrl()
	{
	}

	void TemplateSampleTreeCtrl::UpdateTreeView()
	{
		DeleteAllItems();

		if (!m_template.IsNull())
		{
			AddRootNode();
			AddFingers();
			AddPalms();
			AddFaces();
			AddIrises();
			AddVoices();

			ExpandAllChildren(m_rootId);
		}
	}

	void TemplateSampleTreeCtrl::AddRootNode()
	{
		wxString rootText = wxIsEmpty(m_fileName) ? m_template.GetNativeType().GetName() : m_fileName;
		m_rootId = AddRoot(rootText, 0, 0);
		SetItemData(m_rootId, new TreeNodeData(m_template, 0));
		SelectItem(m_rootId, true);
	}

	void TemplateSampleTreeCtrl::AddFingers()
	{
		if (!m_template.GetFingers().IsNull())
		{
			m_subRootId = AppendItem(m_rootId, N_TMPL_FINGERS, 0);
			SetItemData(m_subRootId, new TreeNodeData(m_template.GetFingers(), 0));

			AddFingerChildNodes();
		}
	}

	void TemplateSampleTreeCtrl::AddPalms()
	{
		if (!m_template.GetPalms().IsNull())
		{
			m_subRootId = AppendItem(m_rootId, N_TMPL_PALMS, 0);
			SetItemData(m_subRootId, new TreeNodeData(m_template.GetPalms(), 1));

			AddPalmChildNodes();
		}
	}

	void TemplateSampleTreeCtrl::AddFaces()
	{
		if (!m_template.GetFaces().IsNull())
		{
			m_subRootId = AppendItem(m_rootId, N_TMPL_FACES, 0);
			SetItemData(m_subRootId, new TreeNodeData(m_template.GetFaces(), 2));

			AddFaceChildNodes();
		}
	}

	void TemplateSampleTreeCtrl::AddIrises()
	{
		if (!m_template.GetIrises().IsNull())
		{
			m_subRootId = AppendItem(m_rootId, N_TMPL_IRISES, 0);
			SetItemData(m_subRootId, new TreeNodeData(m_template.GetIrises(), 3));

			AddIrisChildNodes();
		}
	}

	void TemplateSampleTreeCtrl::AddVoices()
	{
		if (!m_template.GetVoices().IsNull())
		{
			m_subRootId = AppendItem(m_rootId, N_TMPL_VOICES, 0);
			SetItemData(m_subRootId, new TreeNodeData(m_template.GetVoices(), 4));

			AddVoiceChildNodes();
		}
	}

	void TemplateSampleTreeCtrl::AddFingerChildNodes()
	{
		for (int i = 0; i < m_template.GetFingers().GetRecords().GetCount(); i++)
		{
			wxString itemName = N_TMPL_FINGER + wxString::Format(wxT("%i"), i);
			m_childId = AppendItem(m_subRootId, itemName, 0);
			SetItemData(m_childId, new TreeNodeData(m_template.GetFingers().GetRecords().Get(i), i));

			if (i + 1 > m_prvFingerCount) SelectItem(m_childId, true);
		}

		m_prvFingerCount = m_template.GetFingers().GetRecords().GetCount();
	}

	void TemplateSampleTreeCtrl::AddPalmChildNodes()
	{
		for (int i = 0; i < m_template.GetPalms().GetRecords().GetCount(); i++)
		{
			wxString itemName = N_TMPL_PALM + wxString::Format(wxT("%i"), i);
			m_childId = AppendItem(m_subRootId, N_TMPL_PALM + wxString::Format(wxT("%i"), i), 0);
			SetItemData(m_childId, new TreeNodeData(m_template.GetPalms().GetRecords().Get(i), i));

			if (i + 1 > m_prvPalmCount) SelectItem(m_childId, true);
		}

		m_prvPalmCount = m_template.GetPalms().GetRecords().GetCount();
	}

	void TemplateSampleTreeCtrl::AddFaceChildNodes()
	{
		for (int i = 0; i < m_template.GetFaces().GetRecords().GetCount(); i++)
		{
			wxString itemName = N_TMPL_FACE + wxString::Format(wxT("%i"), i);
			m_childId = AppendItem(m_subRootId, N_TMPL_FACE + wxString::Format(wxT("%i"), i), 0);
			SetItemData(m_childId, new TreeNodeData(m_template.GetFaces().GetRecords().Get(i), i));

			if (i + 1 > m_prvFaceCount) SelectItem(m_childId, true);
		}

		m_prvFaceCount = m_template.GetFaces().GetRecords().GetCount();
	}

	void TemplateSampleTreeCtrl::AddIrisChildNodes()
	{
		for (int i = 0; i < m_template.GetIrises().GetRecords().GetCount(); i++)
		{
			wxString itemName = N_TMPL_IRIS + wxString::Format(wxT("%i"), i);
			m_childId = AppendItem(m_subRootId, N_TMPL_IRIS + wxString::Format(wxT("%i"), i), 0);
			SetItemData(m_childId, new TreeNodeData(m_template.GetIrises().GetRecords().Get(i), i));

			if (i + 1 > m_prvIrisCount) SelectItem(m_childId, true);
		}

		m_prvIrisCount = m_template.GetIrises().GetRecords().GetCount();
	}

	void TemplateSampleTreeCtrl::AddVoiceChildNodes()
	{
		for (int i = 0; i < m_template.GetVoices().GetRecords().GetCount(); i++)
		{
			wxString itemName = N_TMPL_VOICE + wxString::Format(wxT("%i"), i);
			m_childId = AppendItem(m_subRootId, N_TMPL_VOICE + wxString::Format(wxT("%i"), i), 0);
			SetItemData(m_childId, new TreeNodeData(m_template.GetVoices().GetRecords().Get(i), i));

			if (i + 1 > m_prvVoiceCount) SelectItem(m_childId, true);
		}

		m_prvVoiceCount = m_template.GetVoices().GetRecords().GetCount();
	}

	NObject TemplateSampleTreeCtrl::GetCurrentSelectionData(wxTreeItemId itemId)
	{
		TreeNodeData *item = dynamic_cast<TreeNodeData*>(GetItemData(itemId));
		return item->GetObject();
	}

	NObject TemplateSampleTreeCtrl::GetCurrentSelectionParentData(wxTreeItemId itemId)
	{
		wxTreeItemId parentId = GetItemParent(itemId);
		if(parentId == NULL)
			return NULL;

		TreeNodeData *item = dynamic_cast<TreeNodeData*>(GetItemData(parentId));
		return item->GetObject();
	}

	int TemplateSampleTreeCtrl::GetCurrentSelectionId(wxTreeItemId itemId)
	{
		TreeNodeData *item = dynamic_cast<TreeNodeData*>(GetItemData(itemId));
		return item->GetObjectId();
	}

	void TemplateSampleTreeCtrl::SetTemplate(NTemplate nTemplate)
	{
		m_template = nTemplate;
	}

	void TemplateSampleTreeCtrl::SetFilename(wxString filename)
	{
		m_fileName = filename;
	}
}}}
