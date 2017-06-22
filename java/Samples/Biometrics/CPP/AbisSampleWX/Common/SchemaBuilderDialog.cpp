#include "Precompiled.h"
#include <Common/SchemaBuilderDialog.h>

#include <Resources/DeleteIcon.xpm>

using namespace Neurotec;
using namespace Neurotec::Biometrics;
using namespace Neurotec::ComponentModel;

namespace Neurotec { namespace Samples
{

DEFINE_EVENT_TYPE(EVT_LOAD)

BEGIN_EVENT_TABLE(SchemaBuilderDialog, wxDialog)
	EVT_BUTTON(wxID_OK, SchemaBuilderDialog::OnBtnOkClick)
	EVT_BUTTON(wxID_CANCEL, SchemaBuilderDialog::OnBtnCancelClick)
	EVT_BUTTON(wxID_ADD, SchemaBuilderDialog::OnBtnAddClick)
	EVT_BUTTON(wxID_DELETE, SchemaBuilderDialog::OnBtnDeleteClick)
	EVT_DATAVIEW_SELECTION_CHANGED(wxID_ANY, SchemaBuilderDialog::OnDataViewSelectionChanged)
	EVT_COMMAND(wxID_ANY, EVT_LOAD, SchemaBuilderDialog::OnLoad)
END_EVENT_TABLE()

SchemaBuilderDialog::SchemaBuilderDialog(wxWindow* parent, wxWindowID id, const wxString& title, const wxPoint& pos, const wxSize& size, long style )
: wxDialog(parent, id, title, pos, size, style), m_schema(SampleDbSchema::GetEmpty())
{
	m_typeValues.Add(wxT("BiographicDataString"));
	m_typeValues.Add(wxT("BiographicDataInteger"));
	m_typeValues.Add(wxT("Gender"));
	m_typeValues.Add(wxT("Thumbnail"));
	m_typeValues.Add(wxT("EnrollData"));
	m_typeValues.Add(wxT("CustomDataString"));
	m_typeValues.Add(wxT("CustomDataInteger"));

	CreateGui();

	wxCommandEvent evt(EVT_LOAD, wxID_ANY);
	wxPostEvent(this, evt);
}

SchemaBuilderDialog::~SchemaBuilderDialog()
{
}

void SchemaBuilderDialog::CreateGui()
{
	SetSizeHints(wxDefaultSize, wxDefaultSize);

	wxBoxSizer* bSizer1;
	bSizer1 = new wxBoxSizer(wxVERTICAL);

	wxBoxSizer* bSizer2;
	bSizer2 = new wxBoxSizer(wxHORIZONTAL);

	wxStaticText * staticText = new wxStaticText(this, wxID_ANY, wxT("Type:"), wxDefaultPosition, wxDefaultSize, 0);
	staticText->Wrap(-1);
	bSizer2->Add(staticText, 0, wxALIGN_CENTER_VERTICAL|wxALL, 5);

	m_cbType = new wxChoice(this, wxID_ANY, wxDefaultPosition, wxDefaultSize, m_typeValues);
	m_cbType->SetSelection(0);
	bSizer2->Add(m_cbType, 0, wxALIGN_CENTER|wxALL, 5);

	staticText = new wxStaticText(this, wxID_ANY, wxT("Name:"), wxDefaultPosition, wxDefaultSize, 0);
	staticText->Wrap(-1);
	bSizer2->Add(staticText, 0, wxALIGN_CENTER|wxALL, 5);

	m_tbName = new wxTextCtrl(this, wxID_ANY, wxEmptyString, wxDefaultPosition, wxDefaultSize, 0);
	bSizer2->Add(m_tbName, 0, wxALIGN_CENTER|wxALL, 5);

	staticText = new wxStaticText(this, wxID_ANY, wxT("Db Column:"), wxDefaultPosition, wxDefaultSize, 0);
	staticText->Wrap(-1);
	bSizer2->Add(staticText, 0, wxALIGN_CENTER|wxALL, 5);

	m_tbColumn = new wxTextCtrl(this, wxID_ANY, wxEmptyString, wxDefaultPosition, wxDefaultSize, 0);
	bSizer2->Add(m_tbColumn, 0, wxALIGN_CENTER|wxALL, 5);

	m_btnAdd = new wxButton(this, wxID_ADD, wxT("Add"), wxDefaultPosition, wxDefaultSize, 0);
	bSizer2->Add(m_btnAdd, 0, wxALIGN_CENTER|wxALL, 5);

	bSizer1->Add(bSizer2, 0, wxEXPAND, 5);

	wxBoxSizer* bSizer3;
	bSizer3 = new wxBoxSizer(wxHORIZONTAL);

	m_btnDelete = new wxBitmapButton(this, wxID_DELETE, wxImage(wxImage(deleteIcon_xpm)), wxDefaultPosition, wxDefaultSize, wxBU_AUTODRAW);
	bSizer3->Add(m_btnDelete, 0, wxALL, 5);

	m_dataView = new wxDataViewListCtrl(this, wxID_ANY, wxDefaultPosition, wxDefaultSize, 0);
	m_dataViewTypeColumn = m_dataView->AppendTextColumn(wxT("Type"), wxDATAVIEW_CELL_INERT, 180);
	m_dataViewNameColumn = m_dataView->AppendTextColumn(wxT("Name"), wxDATAVIEW_CELL_INERT, 180);
	m_dataViewDbColumn = m_dataView->AppendTextColumn(wxT("Db Column"),wxDATAVIEW_CELL_INERT, 180);
	bSizer3->Add(m_dataView, 1, wxALL|wxEXPAND, 5);

	bSizer1->Add(bSizer3, 1, wxEXPAND, 5);

	wxBoxSizer* bSizer4;
	bSizer4 = new wxBoxSizer(wxHORIZONTAL);

	bSizer4->Add(0, 0, 1, wxEXPAND, 5);

	m_btnOk = new wxButton(this, wxID_OK, wxT("Ok"), wxDefaultPosition, wxDefaultSize, 0);
	bSizer4->Add(m_btnOk, 0, wxALL, 5);

	m_btnCancel = new wxButton(this, wxID_CANCEL, wxT("Cancel"), wxDefaultPosition, wxDefaultSize, 0);
	bSizer4->Add(m_btnCancel, 0, wxALL, 5);

	bSizer1->Add(bSizer4, 0, wxEXPAND, 5);

	this->SetSizer(bSizer1);
	this->Layout();

	this->Centre(wxBOTH);
}

void SchemaBuilderDialog::SetSchema(const SampleDbSchema & schema)
{
	m_schema = schema;
}

SampleDbSchema SchemaBuilderDialog::GetSchema() const
{
	return m_schema;
}

SchemaBuilderDialog::ColumnType SchemaBuilderDialog::GetColumnType(const NBiographicDataElement & element)
{
	wxString name = element.GetName();
	if (name == m_schema.genderDataName)
		return Gender;
	else if (name == m_schema.enrollDataName)
		return EnrollData;
	else if (name == m_schema.thumbnailDataName)
		return Thumbnail;
	else
	{
		NDBType dbType = element.dbType;
		bool isCustom = false;
		NBiographicDataSchema::ElementCollection elements = m_schema.customData.GetElements();
		for (NBiographicDataSchema::ElementCollection::iterator it = elements.begin(); it != elements.end(); it++)
		{
			if ((wxString)it->GetName() == name)
			{
				isCustom = true;
				break;
			}
		}
		if (dbType == ndbtString) return isCustom ? CustomDataString : BiographicDataString;
		if (dbType == ndbtInteger) return isCustom ? CustomDataInteger : BiographicDataInteger;
	}
	return Unknown;
}

bool SchemaBuilderDialog::CheckNameDoesNotConflict(const wxString & name)
{
	NArrayWrapper<NPropertyDescriptor> properties = NTypeDescriptor::GetProperties(NSubject::NativeTypeOf());
	for (NArrayWrapper<NPropertyDescriptor>::iterator it = properties.begin(); it != properties.end(); it++)
	{
		if ((wxString)it->GetName() == name)
			return false;
	}
	return true;
}

bool SchemaBuilderDialog::CheckIsUniqueName(const wxString & value)
{
	NBiographicDataSchema::ElementCollection elements = m_schema.biographicData.GetElements();
	for (NBiographicDataSchema::ElementCollection::iterator it = elements.begin(); it != elements.end(); it++)
	{
		if (value.CmpNoCase((wxString)it->GetName()) == 0 || value.CmpNoCase((wxString)it->GetDbColumn()) == 0)
			return false;
	}

	elements = m_schema.customData.GetElements();
	for (NBiographicDataSchema::ElementCollection::iterator it = elements.begin(); it != elements.end(); it++)
	{
		if (value.CmpNoCase((wxString)it->GetName()) == 0 || value.CmpNoCase((wxString)it->GetDbColumn()) == 0)
			return false;
	}
	return true;
}

bool SchemaBuilderDialog::CheckIsUniqueDbColumn(const wxString & value)
{
	if (value != wxEmptyString)
	{
		NBiographicDataSchema::ElementCollection elements = m_schema.biographicData.GetElements();
		for (NBiographicDataSchema::ElementCollection::iterator it = elements.begin(); it != elements.end(); it++)
		{
			if (value.CmpNoCase((wxString)it->GetDbColumn()) == 0 || value.CmpNoCase((wxString)it->GetName()) == 0)
				return false;
		}

		elements = m_schema.customData.GetElements();
		for (NBiographicDataSchema::ElementCollection::iterator it = elements.begin(); it != elements.end(); it++)
		{
			if (value.CmpNoCase((wxString)it->GetDbColumn()) == 0 || value.CmpNoCase((wxString)it->GetName()) == 0)
				return false;
		}
	}
	return true;
}

int SchemaBuilderDialog::GetInsertIndex(ColumnType type)
{
	int priority = 0;
	switch(type)
	{
	case Gender:
	case Thumbnail:
	case EnrollData:
		priority = 3;
		break;
	case BiographicDataString:
	case BiographicDataInteger:
		priority = 2;
		break;
	case CustomDataString:
	case CustomDataInteger:
		priority = 1;
		break;
	default:
		break;
	}

	int rowCount = m_dataView->GetItemCount();
	for (int r = rowCount - 1; r >= 0; r--)
	{
		wxVariant v;
		m_dataView->GetValue(v, r, 0);
		wxString value = v.GetString();

		int rowPriority = 1000;
		if (value.StartsWith(wxT("Gender")) || value.StartsWith(wxT("EnrollData")) || value.StartsWith(wxT("Thumbnail")))
			rowPriority = 3;
		else if (value.StartsWith("Biographic"))
			rowPriority = 2;
		else
			rowPriority = 1;

		if (priority <= rowPriority)
			return r + 1;
	}

	return 0;
}

int SchemaBuilderDialog::AddElement(const NBiographicDataElement & element)
{
	int index = -1;
	ColumnType type = GetColumnType(element);
	wxString typeString = m_typeValues[(int)type];
	if (type == Gender)
		typeString += wxT(" (String)");
	else if (type == Thumbnail || type == EnrollData)
		typeString += wxT("(Blob)");

	wxVector<wxVariant> data;
	data.push_back(wxVariant(typeString));
	data.push_back(wxVariant((wxString)element.GetName()));
	data.push_back(wxVariant((wxString)element.GetDbColumn()));

	index = GetInsertIndex(type);
	m_dataView->InsertItem(index, data);

	return index;
}

void SchemaBuilderDialog::OnBtnAddClick(wxCommandEvent & /*event*/)
{
	wxString name = m_tbName->GetValue().Trim();
	wxString dbColumn = m_tbColumn->GetValue().Trim();
	if (name == wxEmptyString)
	{
		wxMessageBox(wxT("Name field can not be empty"));
		m_tbName->SetFocus();
	}
	else if (!CheckNameDoesNotConflict(name))
	{
		wxMessageBox(wxT("Name can not be same as NSubject property name"));
		m_tbName->SetFocus();
	}
	else
	{
		ColumnType type = (ColumnType)m_cbType->GetSelection();
		bool isCustom = type != BiographicDataInteger && type != BiographicDataString && type != Gender;
		NDBType dbType = ndbtString;
		if (type == EnrollData || type == Thumbnail)
			dbType = ndbtBlob;
		else if (type == BiographicDataInteger || type == CustomDataInteger)
			dbType = ndbtInteger;

		NBiographicDataElement element(name, dbColumn, dbType);
		if (!CheckIsUniqueName(name))
		{
			wxMessageBox(wxT("Item with same name or db column already exists"));
			m_tbName->SetFocus();
			return;
		}
		else if (dbColumn != wxEmptyString && !CheckIsUniqueDbColumn(dbColumn))
		{
			wxMessageBox(wxT("Item with same name or db column already exists"));
			m_tbColumn->SetFocus();
			return;
		}
		else
		{
			if (type == Gender && m_schema.genderDataName != wxEmptyString)
			{
				wxMessageBox(wxT("Gender field already exists"));
				return;
			}
			else if (type == EnrollData && m_schema.enrollDataName != wxEmptyString)
			{
				wxMessageBox(wxT("Enroll data field already exists"));
				return;
			}
			else if (type == Thumbnail && m_schema.thumbnailDataName != wxEmptyString)
			{
				wxMessageBox(wxT("Thumbnail data field already exists"));
				return;
			}
		}

		if (isCustom)
			m_schema.customData.GetElements().Add(element);
		else
			m_schema.biographicData.GetElements().Add(element);

		if (type == Gender)
			m_schema.genderDataName = name;
		else if (type == Thumbnail)
			m_schema.thumbnailDataName = name;
		else if (type == EnrollData)
			m_schema.enrollDataName = name;

		int index = AddElement(element);
		m_dataView->SelectRow(index);

		m_tbColumn->SetLabel(wxEmptyString);
		m_tbName->SetLabel(wxEmptyString);
	}
}

void SchemaBuilderDialog::OnBtnDeleteClick(wxCommandEvent & /*event*/)
{
	int index = m_dataView->GetSelectedRow();
	if (index != -1)
	{
		wxVariant value;
		wxString name;
		m_dataView->GetValue(value, index, 1);
		name = value.GetString();

		bool found = false;
		NBiographicDataSchema::ElementCollection elements = m_schema.biographicData.GetElements();
		for (int i = 0; i < elements.GetCount(); i++)
		{
			if (name == (wxString)elements[i].GetName())
			{
				elements.RemoveAt(i);
				found = true;
				break;
			}
		}

		if (!found)
		{
			elements = m_schema.customData.GetElements();
			for (int i = 0; i < elements.GetCount(); i++)
			{
				if (name == (wxString)elements[i].GetName())
				{
					elements.RemoveAt(i);
					break;
				}
			}
		}

		if (name == m_schema.thumbnailDataName)
			m_schema.thumbnailDataName = wxEmptyString;
		else if (name == m_schema.enrollDataName)
			m_schema.enrollDataName = wxEmptyString;
		else if (name == m_schema.genderDataName)
			m_schema.genderDataName = wxEmptyString;

		m_dataView->DeleteItem(index);
		m_btnDelete->Enable(false);
	}
}

void SchemaBuilderDialog::OnBtnOkClick(wxCommandEvent & /*event*/)
{
	EndModal(wxID_OK);
}

void SchemaBuilderDialog::OnBtnCancelClick(wxCommandEvent & /*event*/)
{
	EndModal(wxID_CANCEL);
}

void SchemaBuilderDialog::OnDataViewSelectionChanged(wxDataViewEvent & event)
{
	m_btnDelete->Enable(event.GetSelection() != -1);
}

void SchemaBuilderDialog::OnLoad(wxCommandEvent & /*event*/)
{
	if (m_schema.IsEmpty()) NThrowArgumentException(wxT("Schema"));

	NBiographicDataSchema::ElementCollection elements = m_schema.biographicData.GetElements();
	for (NBiographicDataSchema::ElementCollection::iterator it = elements.begin(); it != elements.end(); it++)
	{
		AddElement(*it);
	}

	elements = m_schema.customData.GetElements();
	for (NBiographicDataSchema::ElementCollection::iterator it = elements.begin(); it != elements.end(); it++)
	{
		AddElement(*it);
	}
}

}}

