#include "Precompiled.h"
#include "MultiChoiceProperty.h"

namespace Neurotec { namespace Samples { namespace CollectionEditor
{
	WX_PG_IMPLEMENT_PROPERTY_CLASS(MultiChoiceProperty, wxPGProperty,
		wxArrayInt, const wxArrayInt&, TextCtrlAndButton)

		MultiChoiceProperty::MultiChoiceProperty(const wxString& label,
		const wxString& name,
		const wxPGChoices& choices,
		const wxArrayString& value)
		: wxPGProperty(label, name)
	{
			m_choices.Assign(choices);
			SetValue(value);
		}

	MultiChoiceProperty::MultiChoiceProperty(const wxString& label,
		const wxString& name,
		const wxArrayString& strings,
		const wxArrayString& value)
		: wxPGProperty(label, name)
	{
		m_choices.Set(strings);
		SetValue(value);
	}

	MultiChoiceProperty::MultiChoiceProperty(const wxString& label,
		const wxString& name,
		const wxArrayString& value)
		: wxPGProperty(label, name)
	{
		wxArrayString strings;
		m_choices.Set(strings);
		SetValue(value);
	}

	MultiChoiceProperty::~MultiChoiceProperty()
	{
	}

	void MultiChoiceProperty::OnSetValue()
	{
		GenerateValueAsString(m_value, &m_display);
	}

	wxString MultiChoiceProperty::ValueToString(wxVariant& value,
		int argFlags) const
	{
		if (argFlags & wxPG_VALUE_IS_CURRENT)
			return m_display;

		wxString s;
		GenerateValueAsString(value, &s);
		return s;
	}

	void MultiChoiceProperty::GenerateValueAsString(wxVariant& value,
		wxString* target) const
	{
		wxArrayString strings;

		if (value.GetType() == wxPG_VARIANT_TYPE_ARRSTRING)
			strings = value.GetArrayString();

		wxString& tempStr = *target;
		unsigned int i;
		unsigned int itemCount = strings.size();

		tempStr.Empty();

		if (itemCount)
			tempStr.append(wxT("\""));

		for (i = 0; i < itemCount; i++)
		{
			tempStr.append(strings[i]);
			tempStr.append(wxT("\""));
			if (i < (itemCount - 1))
				tempStr.append(wxT(" \""));
		}
	}

	wxArrayInt MultiChoiceProperty::GetValueAsIndices() const
	{
		wxVariant variant = GetValue();
		const wxArrayInt& valueArr = wxArrayIntRefFromVariant(variant);
		unsigned int i;

		wxArrayInt selections;

		if (!m_choices.IsOk() || !m_choices.GetCount() || !(&valueArr))
		{
			for (i = 0; i < valueArr.size(); i++)
				selections.Add(-1);
		}
		else
		{
			for (i = 0; i < valueArr.size(); i++)
			{
				int sIndex = m_choices.Index(valueArr[i]);
				if (sIndex >= 0)
					selections.Add(sIndex);
			}
		}

		return selections;
	}

	bool MultiChoiceProperty::OnEvent(wxPropertyGrid* propgrid,
		wxWindow* WXUNUSED(primary),
		wxEvent& event)
	{
		if (propgrid->IsMainButtonEvent(event))
		{
			wxVariant useValue = propgrid->GetUncommittedPropertyValue();

			wxArrayString labels = m_choices.GetLabels();
			unsigned int choiceCount;

			if (m_choices.IsOk())
				choiceCount = m_choices.GetCount();
			else
				choiceCount = 0;

			wxMultiChoiceDialog dlg(propgrid,
				_("Make a selection:"),
				m_label,
				choiceCount,
				choiceCount ? &labels[0] : NULL,
				wxCHOICEDLG_STYLE);

			dlg.Move(propgrid->GetGoodEditorDialogPosition(this, dlg.GetSize()));

			wxArrayString strings = useValue.GetArrayString();
			wxArrayString extraStrings;

			dlg.SetSelections(m_choices.GetIndicesForStrings(strings, &extraStrings));

			if (dlg.ShowModal() == wxID_OK && choiceCount)
			{
				int userStringMode = GetAttributeAsLong(wxT("UserStringMode"), 0);

				wxArrayInt arrInt = dlg.GetSelections();

				wxVariant variant;

				wxArrayString value;
				unsigned int n;
				if (userStringMode == 1)
				{
					for (n = 0; n < extraStrings.size(); n++)
						value.push_back(extraStrings[n]);
				}

				unsigned int i;
				for (i = 0; i < arrInt.size(); i++)
				{
					if (arrInt.Item(i) != 0)
					{
						value.Add(m_choices.GetLabel(arrInt.Item(i)));
					}
				}

				if (arrInt.size() <= 1)
				{
					for (i = 0; i < arrInt.size(); i++)
					{
						if (arrInt.Item(i) == 0)
							value.Add(m_choices.GetLabel(0));
					}
					if (arrInt.size() == 0)
					{
						value.Add(m_choices.GetLabel(0));
					}
				}

				if (userStringMode == 2)
				{
					for (n = 0; n < extraStrings.size(); n++)
						value.push_back(extraStrings[n]);
				}

				variant = WXVARIANT(value);

				SetValueInEvent(variant);

				return true;
			}
		}
		return false;
	}

	bool MultiChoiceProperty::StringToValue(wxVariant& variant, const wxString& text, int) const
	{
		wxArrayString arr;

		int userStringMode = GetAttributeAsLong(wxT("UserStringMode"), 0);

		WX_PG_TOKENIZER2_BEGIN(text, wxT('"'))
		if (userStringMode > 0 || (m_choices.IsOk() && m_choices.Index(token) != wxNOT_FOUND))
			arr.Add(token);
		WX_PG_TOKENIZER2_END()

			wxVariant v(WXVARIANT(arr));
		variant = v;

		return true;
	}
}}}
