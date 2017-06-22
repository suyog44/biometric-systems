#ifndef CUSTOM_PROPERTY_H
#define CUSTOM_PROPERTY_H

namespace Neurotec
{
	namespace Samples
	{
		class CustomProperty : public wxPGProperty
		{
		public:
			CustomProperty(const Neurotec::ComponentModel::NPropertyDescriptor &descriptor, const NObject &object);
			CustomProperty(std::vector<NValue> * collection, int index, const NType &type, const NAttributes &attributes);
			CustomProperty(const wxString & propertyName, const NValue &object);

			virtual ~CustomProperty();
			virtual wxString ValueToString(wxVariant &value, int argFlags) const;
			virtual bool StringToValue(wxVariant &variant, const wxString &text, int argFlags) const;
			virtual bool IntToValue(wxVariant& variant, int intVal, int argFlags) const;
			virtual bool OnEvent(wxPropertyGrid *propgrid, wxWindow *primary, wxEvent &event);
			virtual wxVariant ChildChanged(wxVariant& thisValue, int childIndex, wxVariant& childValue) const;
			virtual void RefreshChildren();

		private:
			void InitProperty();
			wxString m_name;
			NType m_type;
			NAttributes m_attributes;
			NValue m_currentValue;
			NObject m_object;
			Neurotec::ComponentModel::NPropertyDescriptor m_propertyDiscriptor;
			std::vector<NValue> * m_collection;
			int m_index;
		};
	}
}

#endif
