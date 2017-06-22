#ifndef SUBJECT_PAGE_H_INCLUDED
#define SUBJECT_PAGE_H_INCLUDED

#include <Common/ImageView.h>
#include <Common/StatusPanel.h>
#include <Common/SchemaPropertyGridCtrl.h>
#include <SubjectEditor/ModalityPage.h>

namespace Neurotec { namespace Samples
{

class SubjectPage : public ModalityPage
{
public:
	SubjectPage(::Neurotec::Biometrics::Client::NBiometricClient& biometricClient, ::Neurotec::Biometrics::NSubject& subject,
		SubjectEditorPageInterface& subjectEditorPageInterface, wxWindow *parent, wxWindowID winid = wxID_ANY);

	virtual ~SubjectPage();

	void Reset();

private:

	void OnEnrollClick(wxCommandEvent &event);
	void OnEnrollWithDuplicateCheckClick(wxCommandEvent &event);
	void OnIdentifyClick(wxCommandEvent &event);
	void OnVerifyClick(wxCommandEvent &event);
	void OnSaveTemplateClick(wxCommandEvent &event);
	void OnOpenImageClick(wxCommandEvent &event);
	void OnUpdateClick(wxCommandEvent &event);
	void OnThread(wxCommandEvent &event);

	virtual void OnNavigatedTo();
	virtual void OnNavigatingFrom();

	void CreateGUIControls();
	void SetCallbacks();
	void UnsetCallbacks();

	void UpdateSchemaControls();
	void TryFillGenderField();

	void GetThumbnail();
	void SetSubjectProperties();
	void DisableActions();
	void EnableActions();
	bool IsSubjectEmpty();
	void PerformOperation(::Neurotec::Biometrics::NBiometricOperations operation);
	void Update();

	static void SerializeEnrollData(void * param);

	static void FingerCollectionChangedCallback(::Neurotec::Collections::CollectionChangedEventArgs< ::Neurotec::Biometrics::NFinger> args);
	static void FaceCollectionChangedCallback(::Neurotec::Collections::CollectionChangedEventArgs< ::Neurotec::Biometrics::NFace> args);
	static void IrisCollectionChangedCallback(::Neurotec::Collections::CollectionChangedEventArgs< ::Neurotec::Biometrics::NIris> args);
	static void PalmCollectionChangedCallback(::Neurotec::Collections::CollectionChangedEventArgs< ::Neurotec::Biometrics::NPalm> args);
	static void VoiceCollectionChangedCallback(::Neurotec::Collections::CollectionChangedEventArgs< ::Neurotec::Biometrics::NVoice> args);
	static void BiometricCollectionChangedCallback(void *pParam);

private:
	enum
	{
		ID_BUTTON_ENROLL,
		ID_BUTTON_ENROLL_WITH_DUPLICATE_CHECK,
		ID_BUTTON_IDENTIFY,
		ID_BUTTON_VERIFY,
		ID_BUTTON_SAVE_TEMPLATE,
		ID_BUTTON_OPEN_IMAGE,
		ID_BUTTON_UPDATE,
		ID_CHK_ENROLL_THUMBNAIL_TO_DATABASE
	};

	enum
	{
		ID_UPDATE_STATUS
	};

	wxButton * m_btnEnroll;
	wxButton * m_btnEnrollWithDuplicateCheck;
	wxButton * m_btnIdentify;
	wxButton * m_btnVerify;
	wxButton * m_btnSaveTemplate;
	wxButton * m_btnOpenImage;
	wxButton * m_btnUpdate;
	wxTextCtrl * m_txtSubjectId;
	wxTextCtrl * m_txtQuery;
	StatusPanel * m_statusPanel;
	ImageView * m_pictureBox;
	::Neurotec::Images::NImage m_thumbnail;
	wxStaticBoxSizer * m_enrollDataSizer;
	wxStaticBoxSizer * m_thumbnailSizer;
	SchemaPropertyGrid * m_propertyGrid;

private:
	DECLARE_EVENT_TABLE();
};

}}

#endif
