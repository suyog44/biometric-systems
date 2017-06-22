#ifndef SCHEMA_BUILDER_DIALOG_H_INCLUDED
#define SCHEMA_BUILDER_DIALOG_H_INCLUDED

#include <Settings/SettingsManager.h>

namespace Neurotec { namespace Samples
{

DECLARE_EVENT_TYPE(EVT_LOAD, wxCommandEvent);

class SchemaBuilderDialog : public wxDialog
{
public:
	SchemaBuilderDialog( wxWindow* parent, wxWindowID id = wxID_ANY, const wxString& title = wxT("Schema Builder"), const wxPoint& pos = wxDefaultPosition, const wxSize& size = wxSize(650, 450), long style = wxDEFAULT_DIALOG_STYLE | wxRESIZE_BORDER);
	~SchemaBuilderDialog();

	void SetSchema(const SampleDbSchema & schema);
	SampleDbSchema GetSchema() const;

private:
	enum ColumnType
	{
		Unknown = -1,
		BiographicDataString,
		BiographicDataInteger,
		Gender,
		Thumbnail,
		EnrollData,
		CustomDataString,
		CustomDataInteger
	};

	void OnBtnAddClick(wxCommandEvent & event);
	void OnBtnDeleteClick(wxCommandEvent & event);
	void OnBtnOkClick(wxCommandEvent & event);
	void OnBtnCancelClick(wxCommandEvent & event);
	void OnDataViewSelectionChanged(wxDataViewEvent & event);
	void OnLoad(wxCommandEvent & event);

	bool CheckNameDoesNotConflict(const wxString & name);
	bool CheckIsUniqueName(const wxString & value);
	bool CheckIsUniqueDbColumn(const wxString & value);

	ColumnType GetColumnType(const ::Neurotec::Biometrics::NBiographicDataElement & element);
	int GetInsertIndex(ColumnType type);
	int AddElement(const ::Neurotec::Biometrics::NBiographicDataElement & element);
	void CreateGui();

private:
	wxChoice * m_cbType;
	wxTextCtrl * m_tbName;
	wxTextCtrl * m_tbColumn;
	wxButton * m_btnAdd;
	wxBitmapButton * m_btnDelete;
	wxDataViewListCtrl * m_dataView;
	wxDataViewColumn * m_dataViewTypeColumn;
	wxDataViewColumn * m_dataViewNameColumn;
	wxDataViewColumn * m_dataViewDbColumn;
	wxButton * m_btnOk;
	wxButton * m_btnCancel;

	wxArrayString m_typeValues;
	SampleDbSchema m_schema;

	DECLARE_EVENT_TABLE();
};

}}

#endif
