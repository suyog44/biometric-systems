#include "Precompiled.h"
#include <Common/SchemaPropertyGridCtrl.h>

using namespace Neurotec;
using namespace Neurotec::Biometrics;
using namespace Neurotec::IO;

namespace Neurotec { namespace Samples
{

SchemaPropertyGrid::SchemaPropertyGrid(wxWindow * parent, int id)
: wxPanel(parent, id), m_schema(SampleDbSchema::GetEmpty()), m_isReadOnly(false), m_showBlobs(true)
{
	CreateGui();

	m_propertyGrid->Connect(wxEVT_PG_CHANGED, wxPropertyGridEventHandler(SchemaPropertyGrid::OnPropertyChanged), NULL, this);
}

SchemaPropertyGrid::~SchemaPropertyGrid()
{
	m_propertyGrid->Disconnect(wxEVT_PG_CHANGED, wxPropertyGridEventHandler(SchemaPropertyGrid::OnPropertyChanged), NULL, this);
}

void SchemaPropertyGrid::CreateGui()
{
	wxBoxSizer* bSizer1;
	bSizer1 = new wxBoxSizer(wxVERTICAL);

	m_propertyGrid = new wxPropertyGrid(this, wxID_ANY, wxDefaultPosition, wxDefaultSize, wxPG_DEFAULT_STYLE|wxPG_HIDE_CATEGORIES|wxTAB_TRAVERSAL|wxPG_SPLITTER_AUTO_CENTER);
	bSizer1->Add(m_propertyGrid, 1, wxALL|wxEXPAND, 5);

	this->SetSizer(bSizer1);
	this->Layout();
}

void SchemaPropertyGrid::OnPropertyChanged(wxPropertyGridEvent & event)
{
	wxString name = event.GetPropertyName();
	wxVariant value = event.GetPropertyValue();
	wxPGProperty * property = event.GetProperty();
	NullableIntProperty * intProperty = dynamic_cast<NullableIntProperty*>(property);
	wxStringProperty * stringProperty = dynamic_cast<wxStringProperty*>(property);
	wxEnumProperty * enumProperty = dynamic_cast<wxEnumProperty*>(property);

	if (value.IsNull())
		m_propertyBag.Remove(name);
	else
	{
		if (intProperty)
		{
			if (value.GetType() != wxT("string"))
				m_propertyBag.Set(name, NValue((NInt)value.GetInteger()));
			else
				m_propertyBag.Remove(name);
		}
		else if (stringProperty)
		{
			m_propertyBag.Set(name, NValue::FromString(value.GetString()));
		}
		else if (enumProperty)
		{
			NInt v = value.GetInteger();
			if (v != -1)
			{
				NGender gender = (NGender)v;
				m_propertyBag.Set(name, NValue::FromValue(NBiometricTypes::NGenderNativeTypeOf(), &gender, sizeof(gender)));
			}
			else
			{
				m_propertyBag.Remove(name);
			}
		}
	}
}

wxPGProperty * SchemaPropertyGrid::CreateProperty(NDBType dbType, const wxString & name)
{
	NValue value = NULL;
	bool hasValue = m_propertyBag.TryGet(name, &value);

	wxPGProperty * pResult = NULL;
	switch(dbType)
	{
	case ndbtInteger:
		pResult = new NullableIntProperty(name, name, hasValue ? value.ToInt32() : 0, hasValue);
		break;
	case ndbtString:
		{
			if (name == m_schema.genderDataName)
			{
				wxArrayString strings;
				wxArrayInt ints;

				strings.Add(wxEmptyString);
				ints.Add(-1);

				NArrayWrapper<NInt> values = NEnum::GetValues(NBiometricTypes::NGenderNativeTypeOf());
				for (NArrayWrapper<NInt>::iterator it = values.begin(); it != values.end(); it++)
				{
					ints.Add(*it);
					strings.Add(NEnum::ToString(NBiometricTypes::NGenderNativeTypeOf(), *it));
				}
				pResult = new wxEnumProperty(name, name, strings, ints, hasValue ? value.ToInt32() : -1);
			}
			else
			{
				pResult = new wxStringProperty(name, name, hasValue ? (wxString)value.ToString() : (wxString)wxEmptyString);
			}
			break;
		}
	case ndbtBlob:
		break;
	default:
		NThrowNotSupportedException();
	}

	if (pResult && m_isReadOnly) pResult->SetFlagRecursively(wxPG_PROP_READONLY, true);
	return pResult;
}

void SchemaPropertyGrid::UpdatePropertyGrid()
{
	m_propertyGrid->Clear();

	NBiographicDataSchema::ElementCollection elements = m_schema.biographicData.GetElements();
	for (NBiographicDataSchema::ElementCollection::iterator it = elements.begin(); it != elements.end(); it++)
	{
		wxPGProperty * property = CreateProperty(it->dbType, it->GetName());
		if (property)
			m_propertyGrid->Append(property);
	}

	elements = m_schema.customData.GetElements();
	for (NBiographicDataSchema::ElementCollection::iterator it = elements.begin(); it != elements.end(); it++)
	{
		wxPGProperty * property = CreateProperty(it->dbType, it->GetName());
		if (property)
			m_propertyGrid->Append(property);
	}

	m_propertyGrid->CenterSplitter(true);
}

void SchemaPropertyGrid::SetSchema(const SampleDbSchema & value)
{
	m_schema = value;
	m_propertyBag = NPropertyBag();
	UpdatePropertyGrid();
}

SampleDbSchema SchemaPropertyGrid::GetSchema() const
{
	return m_schema;
}

void SchemaPropertyGrid::SetValues(const NPropertyBag & value)
{
	m_propertyBag = value;
	UpdatePropertyGrid();
}

NPropertyBag SchemaPropertyGrid::GetValues() const
{
	return m_propertyBag;
}

void SchemaPropertyGrid::SetGender(NGender value)
{
	if (!m_propertyBag.IsNull() && m_schema.genderDataName != wxEmptyString)
	{
		NValue nvalue = NValue::FromValue(NBiometricTypes::NGenderNativeTypeOf(), &value, sizeof(value));
		m_propertyBag.Set(m_schema.genderDataName, nvalue);
		wxEnumProperty * property = static_cast<wxEnumProperty*>(m_propertyGrid->GetProperty(m_schema.genderDataName));
		property->SetValue(wxVariant((NInt)value));
	}
}

NGender SchemaPropertyGrid::GetGender() const
{
	if (!m_propertyBag.IsNull() && m_schema.genderDataName != wxEmptyString)
	{
		NValue value = NULL;
		if (m_propertyBag.TryGet(m_schema.genderDataName, &value) && !value.IsNull())
		{
			return (NGender)value.ToInt32();
		}
	}
	return ngUnspecified;
}

void SchemaPropertyGrid::SetIsReadOnly(bool value)
{
	m_isReadOnly = value;
	UpdatePropertyGrid();
}

bool SchemaPropertyGrid::GetIsReadOnly() const
{
	return m_isReadOnly;
}

void SchemaPropertyGrid::SetShowBlobs(bool value)
{
	m_showBlobs = value;
}

bool SchemaPropertyGrid::GetShowBlobs() const
{
	return m_showBlobs;
}

void SchemaPropertyGrid::ApplyTo(NSubject & subject)
{
	NPropertyBag bag = m_propertyBag.Clone<NPropertyBag>();
	NArrayWrapper<NString> keys = bag.GetKeys().GetAll();
	for (NArrayWrapper<NString>::iterator it = keys.begin(); it != keys.end(); it++)
	{
		NValue value = bag.Get(*it);
		NTypeCode valueType = value.GetTypeCode();
		bool remove = value.IsNull();
		if (!remove && valueType == ntcString)
		{
			wxString str = value.ToString();
			remove = str == wxEmptyString;
		}
		else if (!remove && valueType == ntcBuffer)
		{
			NBuffer buffer = value.ToObject<NBuffer>();
			remove = buffer.GetSize() == 0;
		}

		if (remove)
			bag.Remove(*it);
	}

	bag.ApplyTo(subject);
}

NullableIntProperty::NullableIntProperty(const wxString& label, const wxString& name, long value, bool hasValue)
	: wxPGProperty(label, name)
{
	SetValue(hasValue ? wxVariant(value) : wxNullVariant);
}

bool NullableIntProperty::StringToValue(wxVariant &variant, const wxString &text, int /*argFlags*/) const
{
	if (text == wxEmptyString)
	{
		variant = wxVariant(wxT("NullValue"));
		
	}
	else
	{
		NInt value;
		if (NTypes::Int32TryParse(text, -1, NULL, &value))
		{
			variant = wxVariant(value);
		}
		else
			return false;
	}
	return true;
}

wxString NullableIntProperty::ValueToString (wxVariant &value, int /*argFlags*/) const
{
	if (value.GetType() == wxT("string"))
		return wxEmptyString;
	else
	{
		NInt v = value.GetInteger();
		return NTypes::Int32ToString(v);
	}
}

}}
