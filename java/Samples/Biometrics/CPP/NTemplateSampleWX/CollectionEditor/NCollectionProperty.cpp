#include "Precompiled.h"
#include "NCollectionProperty.h"
#include "CollectionEditorDialog.h"
#include "CoreCollectionAdapter.h"
#include "DeltaCollectionAdapter.h"
#include "DoubleCoreCollectionAdapter.h"
#include "MinutiaeCollectionAdapter.h"
#include "MinutiaeNeighborsCollectionAdapter.h"
#include "PossiblePositionCollectionAdapter.h"
#include "FingerCollectionAdapter.h"
#include "PalmCollectionAdapter.h"
#include "FaceCollectionAdapter.h"
#include "IrisCollectionAdapter.h"
#include "VoiceCollectionAdapter.h"

using namespace Neurotec::Biometrics;

namespace Neurotec { namespace Samples { namespace CollectionEditor
{
	WX_PG_IMPLEMENT_PROPERTY_CLASS(NCollectionProperty, wxPGProperty, wxArrayString, const wxArrayString&, TextCtrlAndButton)

		NCollectionProperty::NCollectionProperty(const wxString& label, const wxString& name, const wxArrayString& array, const NTemplate & templ, int collectionName, int selection) :
		wxPGProperty(label, name)
	{
		m_precision = -1;
		m_delimiter = ',';
		SetValue(WXVARIANT(array));
		m_collectionName = collectionName;
		m_template = templ;
		m_recordNum = selection;
	}

	NCollectionProperty::~NCollectionProperty() { }

	bool NCollectionProperty::OnEvent(wxPropertyGrid* propGrid, wxWindow*, wxEvent& event)
	{
		if (propGrid->IsMainButtonEvent(event))
		{
			wxVariant useValue = propGrid->GetUncommittedPropertyValue();

			CollectionBaseAdapter *adaptor = 0;
			CollectionEditorDialog *dlg = 0;

			if (m_collectionName == FINGER_CORE_COLLECTION)
			{
				adaptor = new CoreCollectionAdapter(m_template.GetFingers().GetRecords().Get(m_recordNum));
				adaptor->SetCollectionName(m_collectionName);
				adaptor->SetListViewString(wxT("{X=0, Y=0, Angle=0}"));

				dlg = new CollectionEditorDialog(propGrid, wxEmptyString, m_label, adaptor, wxAEDIALOG_STYLE, wxDefaultPosition, wxDefaultSize);

				if (dlg->ShowModal() == wxID_OK)
				{
					if (dlg->GetAddedRecordCount() != 0)
					{
						SetValueInEvent(wxT("SubCollectionChanged"));
					}
					delete adaptor;
					delete dlg;
					return true;
				}
			}
			else if (m_collectionName == PALM_CORE_COLLECTION)
			{
				adaptor = new CoreCollectionAdapter(m_template.GetPalms().GetRecords().Get(m_recordNum));
				adaptor->SetCollectionName(m_collectionName);
				adaptor->SetListViewString(wxT("{X=0, Y=0, Angle=0}"));

				dlg = new CollectionEditorDialog(propGrid, wxEmptyString, m_label, adaptor, wxAEDIALOG_STYLE, wxDefaultPosition, wxDefaultSize);

				if (dlg->ShowModal() == wxID_OK)
				{
					if (dlg->GetAddedRecordCount() != 0)
					{
						SetValueInEvent(wxT("SubCollectionChanged"));
					}
					delete adaptor;
					delete dlg;
					return true;
				}
			}
			else if (m_collectionName == FINGER_DELTA_COLLECTION)
			{
				adaptor = new DeltaCollectionAdaptor(m_template.GetFingers().GetRecords().Get(m_recordNum));
				adaptor->SetCollectionName(m_collectionName);
				adaptor->SetListViewString(wxT("{X=0, Y=0, Angle1=0, Angle2=0, Angle3=0}"));

				dlg = new CollectionEditorDialog(propGrid, wxEmptyString, m_label, adaptor, wxAEDIALOG_STYLE, wxDefaultPosition, wxDefaultSize);

				if (dlg->ShowModal() == wxID_OK)
				{
					if (dlg->GetAddedRecordCount() != 0)
					{
						SetValueInEvent(wxT("SubCollectionChanged"));
					}
					delete adaptor;
					delete dlg;
					return true;
				}
			}
			else if (m_collectionName == PALM_DELTA_COLLECTION)
			{
				adaptor = new DeltaCollectionAdaptor(m_template.GetPalms().GetRecords().Get(m_recordNum));
				adaptor->SetCollectionName(m_collectionName);
				adaptor->SetListViewString(wxT("{X=0, Y=0, Angle1=0, Angle2=0, Angle3=0}"));

				dlg = new CollectionEditorDialog(propGrid, wxEmptyString, m_label, adaptor, wxAEDIALOG_STYLE, wxDefaultPosition, wxDefaultSize);

				if (dlg->ShowModal() == wxID_OK)
				{
					if (dlg->GetAddedRecordCount() != 0)
					{
						SetValueInEvent(wxT("SubCollectionChanged"));
					}
					delete adaptor;
					delete dlg;
					return true;
				}
			}
			else if (m_collectionName == FINGER_DOUBLE_CORE_COLLECTION)
			{
				adaptor = new DoubleCoreCollectionAdapter(m_template.GetFingers().GetRecords().Get(m_recordNum));
				adaptor->SetCollectionName(m_collectionName);
				adaptor->SetListViewString(wxT("{X=0, Y=0}"));

				dlg = new CollectionEditorDialog(propGrid, wxEmptyString, m_label, adaptor, wxAEDIALOG_STYLE, wxDefaultPosition, wxDefaultSize);

				if (dlg->ShowModal() == wxID_OK)
				{
					if (dlg->GetAddedRecordCount() != 0)
					{
						SetValueInEvent(wxT("SubCollectionChanged"));
					}
					delete adaptor;
					delete dlg;
					return true;
				}
			}
			else if (m_collectionName == PALM_DOUBLE_CORE_COLLECTION)
			{
				adaptor = new DoubleCoreCollectionAdapter(m_template.GetPalms().GetRecords().Get(m_recordNum));
				adaptor->SetCollectionName(m_collectionName);
				adaptor->SetListViewString(wxT("{X=0, Y=0}"));

				dlg = new CollectionEditorDialog(propGrid, wxEmptyString, m_label, adaptor, wxAEDIALOG_STYLE, wxDefaultPosition, wxDefaultSize);

				if (dlg->ShowModal() == wxID_OK)
				{
					if (dlg->GetAddedRecordCount() != 0)
					{
						SetValueInEvent(wxT("SubCollectionChanged"));
					}
					delete adaptor;
					delete dlg;
					return true;
				}
			}
			else if (m_collectionName == FINGER_MINUTIAE_COLLECTION)
			{
				adaptor = new MinutiaeCollectionAdapter(m_template.GetFingers().GetRecords().Get(m_recordNum));
				adaptor->SetCollectionName(m_collectionName);
				adaptor->SetListViewString(wxT("{X=0, Y=0, Type=0, Angle=0, Quality=0, Curvature=0, G=0}"));

				dlg = new CollectionEditorDialog(propGrid, wxEmptyString, m_label, adaptor, wxAEDIALOG_STYLE, wxDefaultPosition, wxDefaultSize);

				if (dlg->ShowModal() == wxID_OK)
				{
					if (dlg->GetAddedRecordCount() != 0)
					{
						SetValueInEvent(wxT("SubCollectionChanged"));
					}
					delete adaptor;
					delete dlg;
					return true;
				}
			}
			else if (m_collectionName == PALM_MINUTIAE_COLLECTION)
			{
				adaptor = new MinutiaeCollectionAdapter(m_template.GetPalms().GetRecords().Get(m_recordNum));
				adaptor->SetCollectionName(m_collectionName);
				adaptor->SetListViewString(wxT("{X=0, Y=0, Type=0, Angle=0, Quality=0, Curvature=0, G=0}"));

				dlg = new CollectionEditorDialog(propGrid, wxEmptyString, m_label, adaptor, wxAEDIALOG_STYLE, wxDefaultPosition, wxDefaultSize);

				if (dlg->ShowModal() == wxID_OK)
				{
					if (dlg->GetAddedRecordCount() != 0)
					{
						SetValueInEvent(wxT("SubCollectionChanged"));
					}
					delete adaptor;
					delete dlg;
					return true;
				}
			}
			else if (m_collectionName == FINGER_MINUTIAE_NEIGHBOUR_COLLECTION)
			{
				adaptor = new MinutiaeNeighborsAdapter(m_template.GetFingers().GetRecords().Get(m_recordNum));
				adaptor->SetCollectionName(m_collectionName);
				adaptor->SetListViewString(N_TMPL_MINUT_NGB_ARR);

				dlg = new CollectionEditorDialog(propGrid, wxEmptyString, m_label, adaptor, wxAEDIALOG_STYLE, wxDefaultPosition, wxDefaultSize);

				dlg->btnAdd->Enable(false);
				dlg->btnRemove->Enable(false);
				dlg->btnUp->Enable(false);
				dlg->btnUp->Enable(false);

				if (dlg->ShowModal() == wxID_OK)
				{
					if (dlg->GetAddedRecordCount() != 0)
					{
						SetValueInEvent(wxT("SubCollectionChanged"));
					}
					delete adaptor;
					delete dlg;
					return true;
				}
			}
			else if (m_collectionName == PALM_MINUTIAE_NEIGHBOUR_COLLECTION)
			{
				adaptor = new MinutiaeNeighborsAdapter(m_template.GetPalms().GetRecords().Get(m_recordNum));
				adaptor->SetCollectionName(m_collectionName);
				adaptor->SetListViewString(N_TMPL_MINUT_NGB_ARR);

				dlg = new CollectionEditorDialog(propGrid, wxEmptyString, m_label, adaptor, wxAEDIALOG_STYLE, wxDefaultPosition, wxDefaultSize);

				dlg->btnAdd->Enable(false);
				dlg->btnRemove->Enable(false);
				dlg->btnUp->Enable(false);
				dlg->btnUp->Enable(false);

				if (dlg->ShowModal() == wxID_OK)
				{
					if (dlg->GetAddedRecordCount() != 0)
					{
						SetValueInEvent(wxT("SubCollectionChanged"));
					}
					delete adaptor;
					delete dlg;
					return true;
				}
			}
			else if (m_collectionName == FINGER_POSSIBLE_POSITION_COLLECTION)
			{
				adaptor = new PossiblePositionAdapter(m_template.GetFingers().GetRecords().Get(m_recordNum));
				adaptor->SetCollectionName(m_collectionName);
				adaptor->SetListViewString(N_TMPL_UNKNOWN);

				dlg = new CollectionEditorDialog(propGrid, wxEmptyString, m_label, adaptor, wxAEDIALOG_STYLE, wxDefaultPosition, wxDefaultSize);

				if (dlg->ShowModal() == wxID_OK)
				{
					if (dlg->GetAddedRecordCount() != 0)
					{
						SetValueInEvent(wxT("SubCollectionChanged"));
					}
					delete adaptor;
					delete dlg;
					return true;
				}
			}
			else if (m_collectionName == PALM_POSSIBLE_POSITION_COLLECTION)
			{
				adaptor = new PossiblePositionAdapter(m_template.GetPalms().GetRecords().Get(m_recordNum));
				adaptor->SetCollectionName(m_collectionName);
				adaptor->SetListViewString(N_TMPL_UNKNOWN);

				dlg = new CollectionEditorDialog(propGrid, wxEmptyString, m_label, adaptor, wxAEDIALOG_STYLE, wxDefaultPosition, wxDefaultSize);

				if (dlg->ShowModal() == wxID_OK)
				{
					if (dlg->GetAddedRecordCount() != 0)
					{
						SetValueInEvent(wxT("SubCollectionChanged"));
					}
					delete adaptor;
					delete dlg;
					return true;
				}
			}
			else if (m_collectionName == FINGER_COLLECTION)
			{
				adaptor = new FingerCollectionAdapter(m_template.GetFingers());
				adaptor->SetCollectionName(m_collectionName);
				adaptor->SetListViewString(N_TMPL_NFREC);

				dlg = new CollectionEditorDialog(propGrid, wxEmptyString, m_label, adaptor, wxAEDIALOG_STYLE, wxDefaultPosition, wxDefaultSize);

				if (dlg->ShowModal() == wxID_OK)
				{
					if (dlg->GetAddedRecordCount() != 0)
					{
						SetValueInEvent(wxT("CollectionChanged"));
					}
					delete adaptor;
					delete dlg;
					return true;
				}
			}
			else if (m_collectionName == PALM_COLLECTION)
			{
				adaptor = new PalmCollectionAdapter(m_template.GetPalms());
				adaptor->SetCollectionName(m_collectionName);
				adaptor->SetListViewString(N_TMPL_NFREC);

				dlg = new CollectionEditorDialog(propGrid, wxEmptyString, m_label, adaptor, wxAEDIALOG_STYLE, wxDefaultPosition, wxDefaultSize);

				if (dlg->ShowModal() == wxID_OK)
				{
					if (dlg->GetAddedRecordCount() != 0)
					{
						SetValueInEvent(wxT("CollectionChanged"));
					}
					delete adaptor;
					delete dlg;
					return true;
				}
			}
			else if (m_collectionName == FACE_COLLECTION)
			{
				adaptor = new FaceCollectionAdapter(m_template.GetFaces());
				adaptor->SetCollectionName(m_collectionName);
				adaptor->SetListViewString(N_TMPL_NLREC);

				dlg = new CollectionEditorDialog(propGrid, wxEmptyString, m_label, adaptor, wxAEDIALOG_STYLE, wxDefaultPosition, wxDefaultSize);

				if (dlg->ShowModal() == wxID_OK)
				{
					if (dlg->GetAddedRecordCount() != 0)
					{
						SetValueInEvent(wxT("CollectionChanged"));
					}
					delete adaptor;
					delete dlg;
					return true;
				}
			}
			else if (m_collectionName == IRIS_COLLECTION)
			{
				adaptor = new IrisCollectionAdapter(m_template.GetIrises());
				adaptor->SetCollectionName(m_collectionName);
				adaptor->SetListViewString(N_TMPL_NEREC);

				dlg = new CollectionEditorDialog(propGrid, wxEmptyString, m_label, adaptor, wxAEDIALOG_STYLE, wxDefaultPosition, wxDefaultSize);

				if (dlg->ShowModal() == wxID_OK)
				{
					if (dlg->GetAddedRecordCount() != 0)
					{
						SetValueInEvent(wxT("CollectionChanged"));
					}
					delete adaptor;
					delete dlg;
					return true;
				}
			}
			else if (m_collectionName == VOICE_COLLECTION)
			{
				adaptor = new VoiceCollectionAdapter(m_template.GetVoices());
				adaptor->SetCollectionName(m_collectionName);
				adaptor->SetListViewString(N_TMPL_NSREC);

				dlg = new CollectionEditorDialog(propGrid, wxEmptyString, m_label, adaptor, wxAEDIALOG_STYLE, wxDefaultPosition, wxDefaultSize);

				if (dlg->ShowModal() == wxID_OK)
				{
					if (dlg->GetAddedRecordCount() != 0)
					{
						SetValueInEvent(wxT("CollectionChanged"));
					}
					delete adaptor;
					delete dlg;
					return true;
				}
			}
			delete adaptor;
			delete dlg;
			return false;
		}
		return false;
	}

	void NCollectionProperty::OnSetValue()
	{
		GenerateValueAsString();
	}

	void NCollectionProperty::ConvertArrayToString(const wxArrayString&, wxString*, const wxUniChar&) const
	{
	}

	wxString NCollectionProperty::ValueToString(wxVariant& WXUNUSED(value), int) const
	{
		return wxT("(Collection)");
	}

	void NCollectionProperty::ArrayStringToString(wxString&, const wxArrayString&, wxUniChar, int)
	{
	}

	void NCollectionProperty::GenerateValueAsString()
	{
	}

	bool NCollectionProperty::OnCustomStringEdit(wxWindow*, wxString&)
	{
		return false;
	}

	bool NCollectionProperty::StringToValue(wxVariant&, const wxString&, int) const
	{
		return true;
	}

	bool NCollectionProperty::DoSetAttribute(const wxString&, wxVariant&)
	{
		return true;
	}
}}}
