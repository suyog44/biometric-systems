#include "Precompiled.h"
#include <CustomProperty.h>
#include <CollectionEditor.h>
#include <StructEditor.h>

using namespace Neurotec::Collections;
using namespace Neurotec::ComponentModel;
using namespace Neurotec::Samples::CommonUIHelpers;
using namespace Neurotec::Reflection;

namespace Neurotec
{
	namespace Samples
	{
		CustomProperty::CustomProperty(const NPropertyDescriptor &descriptor, const NObject &object) :
			wxPGProperty(descriptor.GetName(), descriptor.GetName()), m_name(descriptor.GetName()), m_type(NULL), m_attributes(naNone), m_currentValue((NValue::HandleType)NULL), m_object(object), m_propertyDiscriptor(descriptor), m_collection(NULL), m_index(-1)
		{
			try
			{
				m_type = descriptor.GetPropertyType();
				m_attributes = descriptor.GetAttributes();
				m_currentValue = descriptor.GetValue(object);
				InitProperty();
			}
			catch (NError &ex)
			{
				SetValue((wxString)ex.GetMessage());
				ChangeFlag(wxPG_PROP_READONLY, true);
			}
		}

		CustomProperty::CustomProperty(std::vector<NValue> * const collection, int index, const NType &type, const NAttributes &attributes) :
			wxPGProperty("Value", "Name"), m_name("value"), m_type(type), m_attributes(attributes), m_currentValue((NValue::HandleType)NULL), m_object(NULL), m_propertyDiscriptor(NULL), m_collection(collection), m_index(index)
		{
			try
			{
				m_currentValue = (*collection)[index];
				InitProperty();
			}
			catch (NError &ex)
			{
				SetValue((wxString)ex.ToString());
				ChangeFlag(wxPG_PROP_READONLY, true);
			}
		}

		CustomProperty::CustomProperty(const wxString &propertyName, const NValue &object) :
			wxPGProperty(propertyName, propertyName), m_name(propertyName), m_type(NULL), m_attributes(naNone), m_currentValue((NValue::HandleType)NULL), m_object(object), m_propertyDiscriptor(NULL), m_collection(NULL), m_index(-1)
		{
			try
			{
				m_currentValue = object.GetProperty(propertyName);
				m_type = m_currentValue.GetValueType();
				InitProperty();
			}
			catch (NError &ex)
			{
				SetValue((wxString)ex.ToString());
				ChangeFlag(wxPG_PROP_READONLY, true);
			}
		}

		void CustomProperty::InitProperty()
		{
			wxString displayValue = wxEmptyString;
			if (!m_currentValue.IsNull())
			{
				displayValue = m_currentValue.ToString();
			}

			if ((m_attributes & naArray) == naArray)
			{
				SetEditor(wxPGEditor_TextCtrlAndButton);
			}
			else
			{
				switch (m_type.GetTypeCode())
				{
				case ntcBoolean:
				{
					wxPGChoices values;
					values.Add("True");
					values.Add("False");
					m_choices = values;
					SetEditor(wxPGEditor_Choice);
				}
					break;

				case ntcCollection:
					SetEditor(wxPGEditor_TextCtrlAndButton);
					break;

				case ntcInt16:
				case ntcInt32:
				case ntcByte:
					if (!m_propertyDiscriptor.IsNull() && (m_propertyDiscriptor.GetAttributes() & naStdValues) == naStdValues)
					{
						NPropertyDescriptor::StdValueCollection stdValuesCollection = m_propertyDiscriptor.GetStdValues();
						if ((m_propertyDiscriptor.GetAttributes() & naSet) != naSet)
						{
							wxPGChoices choices = wxPGChoices();
							if ((m_attributes & naNullable) == naNullable)
							{
								choices.Add(wxEmptyString, -1);
							}

							for (NPropertyDescriptor::StdValueCollection::iterator it = stdValuesCollection.begin(); it != stdValuesCollection.end(); it++)
							{
								NNameValuePair nameValuePair = *it;
								wxString choiceName = nameValuePair.GetName();
								NValue choiceValue = nameValuePair.GetValue();
								choices.Add(choiceName, choiceValue.ToInt32());
								if (choiceValue.ToInt32() == m_currentValue.ToInt32())
								{
									SetValue(choiceName);
								}
							}
							SetChoices(choices);
							SetEditor(wxPGEditor_Choice);
						}
						else
						{
							NInt currentValue = m_currentValue.ToInt32();
							for (NPropertyDescriptor::StdValueCollection::iterator it = stdValuesCollection.begin(); it != stdValuesCollection.end(); it++)
							{
								NNameValuePair nameValuePair = *it;
								wxString choiceName = nameValuePair.GetName();
								NInt choiceValue = nameValuePair.GetValue().ToUInt32();
								wxBoolProperty* childProperty = new wxBoolProperty(choiceName, choiceName, (currentValue & choiceValue) == choiceValue);
								childProperty->SetEditor(wxPGEditor_CheckBox);
								childProperty->SetAttribute("EnumValue", choiceValue);
								childProperty->SetAttribute("StringValue", choiceName);
								if ((m_attributes & naNoWrite) == naNoWrite)
								{
									childProperty->Enable(false);
								}
								AddPrivateChild(childProperty);
							}
							break;
						}
					}
					else
					{
						SetValidator(wxNumericPropertyValidator(wxNumericPropertyValidator::Signed));
						SetEditor(wxPGEditor_TextCtrl);
					}
					break;

				case ntcRational:
				case ntcURational:
					if (m_type.IsStruct())
					{
						SetEditor(wxPGEditor_TextCtrlAndButton);
					}
					break;
				case ntcUInt16:
				case ntcUInt32:
					if (!m_propertyDiscriptor.IsNull() && (m_propertyDiscriptor.GetAttributes() & naStdValues) == naStdValues)
					{
						NPropertyDescriptor::StdValueCollection stdValuesCollection = m_propertyDiscriptor.GetStdValues();
						if ((m_propertyDiscriptor.GetAttributes() & naSet) != naSet)
						{
							wxPGChoices choices = wxPGChoices();
							if ((m_attributes & naNullable) == naNullable)
							{
								choices.Add(wxEmptyString, -1);
							}
							for (NPropertyDescriptor::StdValueCollection::iterator it = stdValuesCollection.begin(); it != stdValuesCollection.end(); it++)
							{
								NNameValuePair nameValuePair = *it;
								wxString choiceName = nameValuePair.GetName();
								NValue choiceValue = nameValuePair.GetValue();
								choices.Add(choiceName, choiceValue.ToUInt32());
								if (choiceValue.ToUInt32() == m_currentValue.ToUInt32())
								{
									displayValue = choiceName;
								}
							}
							SetChoices(choices);
							SetEditor(wxPGEditor_Choice);
						}
						else
						{
							NInt currentValue = m_currentValue.ToInt32();
							for (NPropertyDescriptor::StdValueCollection::iterator it = stdValuesCollection.begin(); it != stdValuesCollection.end(); it++)
							{
								NNameValuePair nameValuePair = *it;
								wxString choiceName = nameValuePair.GetName();
								NInt choiceValue = nameValuePair.GetValue().ToUInt32();
								wxBoolProperty* childProperty = new wxBoolProperty(choiceName, choiceName, (currentValue & choiceValue) == choiceValue);
								childProperty->SetEditor(wxPGEditor_CheckBox);
								childProperty->SetAttribute("EnumValue", choiceValue);
								childProperty->SetAttribute("StringValue", choiceName);
								if ((m_attributes & naNoWrite) == naNoWrite)
								{
									childProperty->Enable(false);
								}
								AddPrivateChild(childProperty);
							}
							break;
						}
					}
					else
					{
						SetValidator(wxNumericPropertyValidator(wxNumericPropertyValidator::Unsigned));
						SetEditor(wxPGEditor_TextCtrl);
					}
					break;

				case ntcInt64:
				case ntcSingle:
				case ntcDouble:
					SetValidator(wxNumericPropertyValidator(wxNumericPropertyValidator::Signed));
					SetEditor(wxPGEditor_TextCtrl);
					break;

				case ntcUInt64:
					SetValidator(wxNumericPropertyValidator(wxNumericPropertyValidator::Unsigned));
					SetEditor(wxPGEditor_TextCtrl);
					break;

				case ntcOther:
					if (m_type.IsFlagsEnum())
					{
						const NArrayWrapper<NInt> values = NEnum::GetValues(m_type);
						NInt currentValue = m_currentValue.ToInt32();
						for (int i = 0; i < values.GetCount(); i++)
						{
							NInt enumValue = values.Get(i);
							wxBoolProperty* childProperty = new wxBoolProperty(NEnum::ToString(m_type, enumValue), NEnum::ToString(m_type, enumValue), (currentValue & enumValue) == enumValue);
							childProperty->SetEditor(wxPGEditor_CheckBox);
							childProperty->SetAttribute("EnumValue", enumValue);
							if ((m_attributes & naNoWrite) == naNoWrite)
							{
								childProperty->Enable(false);
							}
							AddPrivateChild(childProperty);
						}
						SetValue((wxLongLong)currentValue);
						break;
					}
					else if (m_type.IsEnum())
					{
						wxPGChoices choices = wxPGChoices();
						const NArrayWrapper<NInt> values = NEnum::GetValues(m_type);

						if ((m_attributes & naNullable) == naNullable)
						{
							choices.Add(wxEmptyString, -1);
						}
						for (int i = 0; i < values.GetCount(); i++)
						{
							NInt enumValue = values.Get(i);
							choices.Add(NEnum::ToString(m_type, enumValue), enumValue);
						}
						m_choices = choices;
						SetEditor(wxPGEditor_Choice);
						break;
					}
					else if (m_type.IsStruct())
					{
						SetEditor(wxPGEditor_TextCtrlAndButton);
						break;
					}
					else if (!m_type.IsObject())
					{
						break;
					}
				case ntcObject:
					if (!m_currentValue.IsNull())
					{
						NObject object = m_currentValue.ToObject(m_type);
						NArrayWrapper<NPropertyDescriptor> objectProperties = NTypeDescriptor::GetProperties(object);
						for (NArrayWrapper<NPropertyDescriptor>::iterator it = objectProperties.begin(); it != objectProperties.end(); it++)
						{
							AppendChild(new CustomProperty(*it, object));
						}
					}
					break;
				default:
					SetEditor(wxPGEditor_TextCtrl);
				}
			}
			if (!m_type.IsFlagsEnum())
			{
				SetValue(displayValue);
			}
			if ((m_attributes & naNoWrite) == naNoWrite && !m_type.IsObject())
			{
				ChangeFlag(wxPG_PROP_READONLY, true);
			}
			SetExpanded(false);
		}

		CustomProperty::~CustomProperty()
		{
		}

		wxString CustomProperty::ValueToString(wxVariant &value, int WXUNUSED(argFlags)) const
		{
			if (m_type.IsFlagsEnum())
			{
				return NEnum::ToString(m_type, value.GetInteger());
			}
			else if (((m_attributes & naStdValues) == naStdValues && (m_attributes & naSet) == naSet))
			{
				wxString displayValue = wxEmptyString;
				bool firstValue = true;
				for (unsigned int i = 0; i < GetChildCount(); i++)
				{
					if (Item(i)->GetValue())
					{
						if (firstValue)
						{
							firstValue = false;
						}
						else
						{
							displayValue.append(",");
						}
						wxString selectionName = Item(i)->GetAttribute("StringValue").GetString();
						displayValue.append(selectionName);
					}
				}
				return displayValue;
			}
			return value.GetString();
		}

		bool CustomProperty::StringToValue(wxVariant &variant, const wxString &text, int WXUNUSED(argFlags)) const
		{
			if (!m_type.IsFlagsEnum())
			{
				NValue propertyValue = NValue::FromString(text);
				if (!m_propertyDiscriptor.IsNull())
				{
					try
					{
						m_propertyDiscriptor.SetValue(m_object, propertyValue);
					}
					catch (NError &ex)
					{
						wxMessageBox("Error in setting value for " + m_propertyDiscriptor.GetName() + " Value " + propertyValue.ToString() + "\nError: " + ex.ToString());
						return false;
					}
				}
				else if (m_collection != NULL)
				{
					m_collection->at(m_index) = propertyValue;
				}
				else
				{
					NObjectDynamicCast<NValue>(m_object).SetProperty(m_name, propertyValue);
				}
				variant = text;
			}
			return true;
		}

		bool CustomProperty::IntToValue(wxVariant& variant, int intVal, int WXUNUSED(argFlags)) const
		{
			wxString stringValue = m_choices[intVal].GetText();
			NValue propertyValue = NValue::FromString(stringValue);
			if (!m_propertyDiscriptor.IsNull())
			{
				try
				{
					m_propertyDiscriptor.SetValue(m_object, propertyValue);
				}
				catch (NError &ex)
				{
					wxMessageBox("Error in setting value for " + m_propertyDiscriptor.GetName() + " Value " + propertyValue.ToString() + "\nError: " + ex.ToString());
					return false;
				}
			}
			else if (m_collection != NULL)
			{
				m_collection->at(m_index) = propertyValue;
			}
			else
			{
				NObjectDynamicCast<NValue>(m_object).SetProperty(m_name, propertyValue);
			}
			variant = m_choices[intVal].GetText();
			return true;
		}

		bool CustomProperty::OnEvent(wxPropertyGrid *propGrid, wxWindow *WXUNUSED(window), wxEvent &event)
		{
			if (propGrid->IsMainButtonEvent(event))
			{
				if ((m_attributes & naArray) == naArray)
				{
					std::vector<NValue> valueList;
					if (!m_currentValue.IsNull())
					{
						NArray valueArray = m_currentValue.ToObject<NArray>();
						int size = valueArray.GetLength();
						for (int i = 0; i < size; i++)
						{
							valueList.push_back(valueArray.GetValue(i));
						}
					}

					CollectionEditor dlg(propGrid, valueList, m_type, m_label, wxAEDIALOG_STYLE, wxDefaultPosition, wxDefaultSize);
					if (dlg.ShowModal() == wxID_OK)
					{
						valueList = dlg.GetValue();
						try
						{
							NValue typeArray = NULL;
							if (m_type.IsObject())
							{
								std::list<NObject> objectList;
								for (std::vector<NValue>::iterator it = valueList.begin(); it != valueList.end(); it++)
								{
									objectList.push_back(it->ToObject<NObject>());
								}
								typeArray = NArray::FromArray(objectList.begin(), objectList.end());
							}
							else
							{
								NArray valuesArray = NArray::FromArray(valueList.begin(), valueList.end());
								typeArray = NValue::ChangeType(valuesArray, NTypes::NStringNativeTypeOf(), m_propertyDiscriptor.GetAttributes());
							}
							m_propertyDiscriptor.SetValue(m_object, typeArray);
							SetValue((wxString)typeArray.ToString());
							m_currentValue = typeArray;
						}
						catch (NError &ex)
						{
							wxMessageBox("Error in setting value for " + m_propertyDiscriptor.GetName() + " Error " + ex.ToString());
						}
						return true;
					}
				}
				else if (m_type.GetTypeCode() == ntcCollection)
				{
					std::vector<NValue> values;
					NCollection collection = m_currentValue.ToObject<NCollection>();
					for (NCollection::iterator iterator = collection.begin(); iterator != collection.end(); iterator++)
					{
						values.push_back(*iterator);
					}
					NCollectionInfo collectionInfo = NObjectDynamicCast<NCollectionInfo>(collection.GetObjectPartInfo());
					NType targetType = collectionInfo.GetItemType();

					CollectionEditor dlg(propGrid, values, targetType, m_label, wxAEDIALOG_STYLE, wxDefaultPosition, wxDefaultSize);
					if (dlg.ShowModal() == wxID_OK)
					{
						std::vector<NValue> listValues = dlg.GetValue();
						collection.Clear();
						for (std::vector<NValue>::iterator it = listValues.begin(); it != listValues.end(); it++)
						{
							collection.Add(*it);
						}
						try
						{
							m_propertyDiscriptor.SetValue(m_object, collection);
							SetValue((wxString)collection.ToString());
						}
						catch (NError &/*ex*/)
						{
							wxMessageBox("Error in setting value for " + m_propertyDiscriptor.GetName());
						}
						return true;
					}
				}
				else if (m_type.IsStruct())
				{
					StructEditor dlg(propGrid, m_type, m_currentValue, (m_attributes & naNullable) == naNullable);
					if (dlg.ShowModal() == wxID_OK)
					{
						NValue value = dlg.GetValue();
						try
						{
							m_propertyDiscriptor.SetValue(m_object, value);
							if (value.IsNull())
							{
								SetValue(wxEmptyString);
							}
							else
							{
								SetValue((wxString)value.ToString());
							}
						}
						catch (NError &ex)
						{
							wxMessageBox("Error in setting value for " + m_propertyDiscriptor.GetName() + " Error " + ex.ToString());
						}
					}
				}
			}
			event.Skip();
			return false;
		}

		wxVariant CustomProperty::ChildChanged(wxVariant& WXUNUSED(thisValue), int childIndex, wxVariant& childValue) const
		{
			if (m_type.IsFlagsEnum() || ((m_attributes & naStdValues) == naStdValues && (m_attributes & naSet) == naSet))
			{
				unsigned int selection = Item(childIndex)->GetAttribute("EnumValue").GetInteger();
				unsigned int newValue = m_value.GetInteger();
				if (childValue.GetBool())
				{
					newValue |= selection;
				}
				else
				{
					newValue &= ~(selection);
				}

				NValue propertyValue = NValue::FromValue(m_type, &newValue, sizeof(unsigned int));
				if (!m_propertyDiscriptor.IsNull())
				{
					try
					{
						m_propertyDiscriptor.SetValue(m_object, propertyValue);
					}
					catch (NError &ex)
					{
						wxMessageBox("Error in setting value for " + m_propertyDiscriptor.GetName() + " Value " + propertyValue.ToString() + "\nError: " + ex.ToString());
						return m_value;
					}
				}
				else if (m_collection != NULL)
				{
					m_collection->at(m_index) = propertyValue;
				}
				else
				{
					NObjectDynamicCast<NValue>(m_object).SetProperty(m_name, propertyValue);
				}
				return wxVariant((int)newValue);
			}
			return (wxString)m_currentValue.ToString();
		}

		void CustomProperty::RefreshChildren()
		{
			if (m_type.IsFlagsEnum() || ((m_attributes & naStdValues) == naStdValues && (m_attributes & naSet) == naSet))
			{
				unsigned int value = m_value.GetInteger();
				for (unsigned int i = 0; i < GetChildCount(); i++)
				{
					unsigned int flag = Item(i)->GetAttribute("EnumValue").GetInteger();
					if (value & flag)
					{
						Item(i)->SetValue(true);
					}
					else
					{
						Item(i)->SetValue(false);
					}
				}
			}
		}
	}
}
