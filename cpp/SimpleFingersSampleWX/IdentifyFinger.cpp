#include "Precompiled.h"
#include "Resources/OpenFolderIcon.xpm"
#include "IdentifyFinger.h"
#include "LicensePanel.h"

using namespace Neurotec::Biometrics::Client;
using namespace Neurotec::Biometrics;
using namespace Neurotec::Gui;
using namespace Neurotec::Biometrics::Gui;

namespace Neurotec
{
	namespace Samples
	{
		DEFINE_EVENT_TYPE(wxEVT_IDENTIFYPAGE_ENROLL_COMPLETE)
		DEFINE_EVENT_TYPE(wxEVT_IDENTIFYPAGE_EXTRACTION_COMPLETE)
		DEFINE_EVENT_TYPE(wxEVT_IDENTIFYPAGE_IDENTIFY_COMPLETE)
		BEGIN_EVENT_TABLE(IdentifyFinger, wxPanel)
			EVT_BUTTON(ID_BUTTON_OPEN_TEMPLATE, IdentifyFinger::OnButtonOpenTemplatesClicked)
			EVT_BUTTON(ID_BUTTON_OPEN_IMAGE, IdentifyFinger::OnButtonOpenTemplateOrImageClicked)
			EVT_BUTTON(ID_BUTTON_DEFAULT_FAR, IdentifyFinger::OnButtonDefaultFARClicked)
			EVT_BUTTON(ID_BUTTON_DEFAULT_FINGER_QUALITY_THRESHOLD, IdentifyFinger::OnButtonDefaultThresholdClicked)
			EVT_BUTTON(ID_BUTTON_IDENTYFY, IdentifyFinger::OnButtonIdentifyClick)
			EVT_CHECKBOX(ID_CHECKBOX_SHOW_BINARIZED_IMAGE, IdentifyFinger::OnCkeckBoxShowBinarizedImageClick)
			EVT_COMMAND(ID_SPINCTRL_FINGER_QUALITY_THRESHOLD, wxEVT_SPINCTRL, IdentifyFinger::OnSpinControlThresholdChange)
			EVT_TEXT(ID_SPINCTRL_FINGER_QUALITY_THRESHOLD, IdentifyFinger::OnSpinControlThresholdChange)
			EVT_COMBOBOX(ID_COMBOBOX_FAR, IdentifyFinger::OnComboBoxFARChange)
			EVT_TEXT(ID_COMBOBOX_FAR, IdentifyFinger::OnComboBoxFARChange)
			EVT_COMMAND(wxID_ANY, wxEVT_IDENTIFYPAGE_ENROLL_COMPLETE, IdentifyFinger::OnEnrollCompleted)
			EVT_COMMAND(wxID_ANY, wxEVT_IDENTIFYPAGE_EXTRACTION_COMPLETE, IdentifyFinger::OnExtractionCompleted)
			EVT_COMMAND(wxID_ANY, wxEVT_IDENTIFYPAGE_IDENTIFY_COMPLETE, IdentifyFinger::OnIdentifyCompleted)
		END_EVENT_TABLE()

		IdentifyFinger::IdentifyFinger(wxWindow *parent, const NBiometricClient & biometricClient, wxWindowID id, const wxPoint & pos, const wxSize & size, long style, const wxString & name)
			: wxPanel(parent, id, pos, size, style, name), m_biometricClient(biometricClient), m_subjectToIdentify(NULL), m_enrollTask(NULL)
		{
			m_defaultQualityThreshold = m_biometricClient.GetFingersQualityThreshold();
			m_defaultFar = m_biometricClient.GetMatchingThreshold();
			CreateGUIControls();
		}

		IdentifyFinger::~IdentifyFinger()
		{
		}

		void IdentifyFinger::CreateGUIControls()
		{
			wxBoxSizer *mainSizer = new wxBoxSizer(wxVERTICAL);
			LicensePanel *licencePanel;
			licencePanel = new LicensePanel(this, wxID_ANY, wxDefaultPosition, wxDefaultSize, wxBORDER_SIMPLE);
			wxString licences = "Biometrics.FingerExtraction,Biometrics.FingerMatching";
			wxString licencesOptional = "Images.WSQ";
			licencePanel->RefreshComponentsStatus(licences, licencesOptional);
			mainSizer->Add(licencePanel, 0, wxEXPAND | wxALL, 2);

			wxImage openImage(openFolderIcon_xpm);
			wxBitmapButton *buttonOpenTemplates = new wxBitmapButton(this, ID_BUTTON_OPEN_TEMPLATE, openImage);
			buttonOpenTemplates->SetToolTip("Open template files (*.dat)");
			m_staticTextTemplatesLoaded = new wxStaticText(this, wxID_ANY, "Templates Loaded: 0", wxDefaultPosition, wxDefaultSize, 0, wxStaticTextNameStr);
			wxStaticBoxSizer *staticBoxSizerTemplateLoading = new wxStaticBoxSizer(wxHORIZONTAL, this, "Templates loading");
			staticBoxSizerTemplateLoading->Add(buttonOpenTemplates, 0, wxALIGN_CENTER | wxALL, 2);
			staticBoxSizerTemplateLoading->Add(m_staticTextTemplatesLoaded, 0, wxALIGN_CENTER | wxALL, 2);
			mainSizer->Add(staticBoxSizerTemplateLoading, 0, wxEXPAND | wxALL, 2);

			wxNViewZoomSlider *zoomSlider = new wxNViewZoomSlider(this);
			m_fingerView = new wxNFingerView(this);
			m_buttonOpenImage = new wxButton(this, ID_BUTTON_OPEN_IMAGE, "Open", wxDefaultPosition, wxSize(-1, 25));
			m_buttonOpenImage->SetBitmap(openImage);
			m_buttonOpenImage->SetToolTip("Open fingerprint image for identification");

			wxStaticText *labelThreshold = new wxStaticText(this,wxID_ANY,"Threshold:");
			m_spinCtrlThreshold = new wxSpinCtrl(this, ID_SPINCTRL_FINGER_QUALITY_THRESHOLD, wxEmptyString, wxDefaultPosition, wxSize(50, 25), wxSP_ARROW_KEYS, 0, 100, 0, "wxSpinCtrl");
			m_spinCtrlThreshold->SetValue(m_defaultQualityThreshold);
			m_buttonDefaultThreshold = new wxButton(this, ID_BUTTON_DEFAULT_FINGER_QUALITY_THRESHOLD, "Default", wxDefaultPosition, wxSize(-1, 25));
			m_buttonDefaultThreshold->Enable(false);

			m_checkBoxShowBinarizedImage = new wxCheckBox(this, ID_CHECKBOX_SHOW_BINARIZED_IMAGE, "Show binarized image");
			m_checkBoxShowBinarizedImage->SetValue(true);
			m_fingerView->SetShownImage(wxNFrictionRidgeView::PROCESSED_IMAGE);
			m_checkBoxShowBinarizedImage->Enable(false);
			zoomSlider->SetView(m_fingerView);

			wxStaticBoxSizer *staticBoxSizerFingerView = new wxStaticBoxSizer(wxVERTICAL, this, "Image/Template for identification");
			wxBoxSizer *boxSizerFingerViewActions = new wxBoxSizer(wxHORIZONTAL);
			boxSizerFingerViewActions->Add(m_buttonOpenImage, 0, wxALIGN_CENTER | wxALL, 2);
			boxSizerFingerViewActions->Add(labelThreshold, 0, wxALL | wxALIGN_CENTER,4);
			boxSizerFingerViewActions->Add(m_spinCtrlThreshold, 0, wxALIGN_CENTER | wxALL, 0);
			boxSizerFingerViewActions->Add(m_buttonDefaultThreshold, 0, wxALIGN_CENTER | wxALL, 4);
			boxSizerFingerViewActions->AddStretchSpacer();
			boxSizerFingerViewActions->Add(m_checkBoxShowBinarizedImage, 0, wxALIGN_BOTTOM | wxALL, 14);
			boxSizerFingerViewActions->Add(zoomSlider, 0, wxALIGN_BOTTOM, 0);
			staticBoxSizerFingerView->Add(m_fingerView, 1, wxEXPAND | wxALL, 2);
			staticBoxSizerFingerView->Add(boxSizerFingerViewActions, 0, wxEXPAND | wxALL, 2);
			mainSizer->Add(staticBoxSizerFingerView, 1, wxEXPAND | wxALL, 2);

			m_buttonIdentify = new wxButton(this, ID_BUTTON_IDENTYFY, "Identify", wxDefaultPosition, wxSize(-1, 25));
			m_buttonIdentify->Enable(false);

			m_buttonDefaultFAR = new wxButton(this, ID_BUTTON_DEFAULT_FAR, "Default", wxDefaultPosition, wxSize(-1, 25));
			m_buttonDefaultFAR->Enable(false);

			wxStaticText *labelMatchingFAR = new wxStaticText(this, wxID_ANY, "Matching FAR:");
			m_comboBoxFAR = new wxComboBox(this, ID_COMBOBOX_FAR, wxEmptyString, wxDefaultPosition, wxDefaultSize);
			wxChar separator = wxNumberFormatter::GetDecimalSeparator();
			m_comboBoxFAR->AppendString(wxString::Format("0%c1%%", separator));
			m_comboBoxFAR->AppendString(wxString::Format("0%c01%%", separator));
			m_comboBoxFAR->AppendString(wxString::Format("0%c001%%", separator));
			m_comboBoxFAR->SetSelection(1);
			m_listResults = new wxListCtrl(this, wxID_ANY, wxDefaultPosition, wxDefaultSize, wxLC_REPORT);
			m_listResults->AppendColumn("ID", wxLIST_FORMAT_LEFT, 100);
			m_listResults->AppendColumn("Score", wxLIST_FORMAT_LEFT, 100);
			wxStaticBoxSizer *staticBoxSizerIdentification = new wxStaticBoxSizer(wxVERTICAL, this, "Identification");
			wxBoxSizer *boxSizerTopOfIdentificationBox = new wxBoxSizer(wxHORIZONTAL);

			boxSizerTopOfIdentificationBox->Add(m_buttonIdentify, 0, wxALIGN_CENTER);
			boxSizerTopOfIdentificationBox->AddStretchSpacer();
			boxSizerTopOfIdentificationBox->Add(labelMatchingFAR, 0, wxALIGN_CENTER | wxALL, 2);
			boxSizerTopOfIdentificationBox->Add(m_comboBoxFAR, 0, wxALIGN_CENTER | wxALL, 2);
			boxSizerTopOfIdentificationBox->Add(m_buttonDefaultFAR, 0, wxALIGN_CENTER | wxALL, 2);

			staticBoxSizerIdentification->Add(boxSizerTopOfIdentificationBox, 0, wxEXPAND | wxALL, 2);
			staticBoxSizerIdentification->Add(m_listResults, 0, wxEXPAND | wxALL, 2);
			mainSizer->Add(staticBoxSizerIdentification, 0, wxEXPAND | wxALL, 2);
			SetBackgroundColour(GetParent()->GetBackgroundColour());
			SetSizer(mainSizer);
		}

		bool IdentifyFinger::IsSubjectValid(const NSubject & subject)
		{
			return !subject.IsNull() && (subject.GetStatus() == nbsOk || (subject.GetStatus() == nbsNone && !subject.GetTemplateBuffer().IsNull()));
		}

		void IdentifyFinger::OnButtonOpenTemplatesClicked(wxCommandEvent& WXUNUSED(event))
		{
			wxFileDialog openFileDialog(this, "Open Templates Files", wxEmptyString, wxEmptyString, wxEmptyString, wxFD_OPEN | wxFD_FILE_MUST_EXIST | wxFD_MULTIPLE);
			if (openFileDialog.ShowModal() == wxID_OK)
			{
				m_buttonIdentify->Enable(false);
				m_biometricClient.Clear();
				m_subjectIds.Clear();
				m_staticTextTemplatesLoaded->SetLabelText(wxString::Format("Templates Loaded: %d", 0));
				wxArrayString filePaths;
				openFileDialog.GetPaths(filePaths);
				m_enrollTask = m_biometricClient.CreateTask(nboEnroll, NULL);
				int templateCount = 0;
				bool successful = false;
				try
				{
					for (size_t i = 0; i < filePaths.Count(); i++)
					{
						wxString path = filePaths[i];
						wxFileName fileName(path);
						NSubject subject = NSubject::FromFile(path);
						subject.SetId(fileName.GetFullName());
						m_enrollTask.GetSubjects().Add(subject);
						templateCount++;
						m_subjectIds.Add(subject.GetId());
					}
					successful = true;
				}
				catch (NError & ex)
				{
					wxExceptionDlg::Show(ex);
				}
				if (successful && templateCount > 0)
				{
					NAsyncOperation operation = m_biometricClient.PerformTaskAsync(m_enrollTask);
					operation.AddCompletedCallback(&IdentifyFinger::OnEnrollCompletedCallback, this);
				}
			}
		}

		void IdentifyFinger::OnEnrollCompletedCallback(const EventArgs & args)
		{
			IdentifyFinger *panel = reinterpret_cast<IdentifyFinger*>(args.GetParam());
			wxCommandEvent ev(wxEVT_IDENTIFYPAGE_ENROLL_COMPLETE);
			ev.SetClientData(args.GetObject<NAsyncOperation>().RefHandle());
			wxPostEvent(panel, ev);
		}

		void IdentifyFinger::OnEnrollCompleted(wxCommandEvent& event)
		{
			NAsyncOperation operation(static_cast<HNObject>(event.GetClientData()), true);
			NError exception = operation.GetError();
			if (exception.IsNull())
			{
				NBiometricStatus status = m_enrollTask.GetStatus();
				if (status == nbsOk)
				{
					m_staticTextTemplatesLoaded->SetLabelText(wxString::Format("Templates Loaded: %d", m_subjectIds.Count()));
					m_buttonIdentify->Enable(IsSubjectValid(m_subjectToIdentify));
				}
				else
				{
					wxString statusString = NEnum::ToString(NBiometricTypes::NBiometricStatusNativeTypeOf(), status);
					wxMessageBox(wxString::Format("Enrollment failed: %s", statusString), "Error");
				}
			}
			else
			{
				wxExceptionDlg::Show(exception);
			}
		}

		void IdentifyFinger::OnButtonOpenTemplateOrImageClicked(wxCommandEvent & WXUNUSED(event))
		{
			wxFileDialog openFileDialog(this, "Open Image File Or Template", wxEmptyString, wxEmptyString, wxEmptyString, wxFD_OPEN | wxFD_FILE_MUST_EXIST);
			if (openFileDialog.ShowModal() == wxID_OK)
			{
				m_buttonOpenImage->Enable(false);
				m_buttonIdentify->Enable(false);
				m_checkBoxShowBinarizedImage->Enable(false);
				m_subjectToIdentify = NULL;
				try
				{
					m_subjectToIdentify = NSubject::FromFile((openFileDialog.GetPath()));
				}
				catch (NError& /*ex*/)
				{
				}
				if (!m_subjectToIdentify.IsNull() && m_subjectToIdentify.GetFingers().GetCount() > 0)
				{
					m_fingerView->SetFinger(m_subjectToIdentify.GetFingers().Get(0));
					m_buttonIdentify->Enable(m_subjectIds.Count() > 0);
					m_buttonOpenImage->Enable(true);
				}
				else
				{
					wxArrayString filename;
					m_subjectToIdentify = NSubject();
					NFinger finger;
					finger.SetFileName(openFileDialog.GetPath());
					m_subjectToIdentify.GetFingers().Add(finger);
					m_fingerView->SetFinger(finger);
					m_biometricClient.SetFingersQualityThreshold(m_spinCtrlThreshold->GetValue());
					NAsyncOperation operation = m_biometricClient.CreateTemplateAsync(m_subjectToIdentify);
					operation.AddCompletedCallback(&IdentifyFinger::OnExtractionCompletedCallback, this);
				}
			}
		}

		void IdentifyFinger::OnExtractionCompletedCallback(const EventArgs & args)
		{
			IdentifyFinger *panel = static_cast<IdentifyFinger*>(args.GetParam());
			wxCommandEvent ev(wxEVT_IDENTIFYPAGE_EXTRACTION_COMPLETE);
			ev.SetClientData(args.GetObject<NAsyncOperation>().RefHandle());
			wxPostEvent(panel, ev);
		}

		void IdentifyFinger::OnExtractionCompleted(wxCommandEvent& event)
		{
			NAsyncOperation operation(static_cast<HNObject>(event.GetClientData()), true);
			if (operation.GetError().IsNull())
			{
				NBiometricStatus status = m_subjectToIdentify.GetStatus();
				if (status == nbsOk)
				{
					m_buttonIdentify->Enable(m_subjectIds.Count() > 0);
					m_checkBoxShowBinarizedImage->Enable(true);
				}
				else
				{
					wxString statusString = NEnum::ToString(NBiometricTypes::NBiometricStatusNativeTypeOf(), status);
					wxMessageBox(wxString::Format("Template was not extracted: %s ", statusString), "Error", wxICON_ERROR);
					m_subjectToIdentify = NULL;
					m_buttonIdentify->Enable(false);
				}
			}
			else
			{
				wxExceptionDlg::Show(operation.GetError());
				m_subjectToIdentify = NULL;
				m_buttonIdentify->Enable(false);
			}
			m_buttonOpenImage->Enable(true);
		}

		void IdentifyFinger::OnButtonIdentifyClick(wxCommandEvent& WXUNUSED(event))
		{
			double threshold;
			try
			{
				threshold = Utils::MatchingThresholdFromString(m_comboBoxFAR->GetValue());
			}
			catch (NError ex)
			{
				wxMessageBox("FAR is not valid", "Error", wxICON_ERROR);
				return;
			}
			m_biometricClient.SetMatchingThreshold(threshold);
			m_comboBoxFAR->SetValue(Utils::MatchingThresholdToString(m_biometricClient.GetMatchingThreshold()));
			m_listResults->DeleteAllItems();
			NAsyncOperation operation = m_biometricClient.IdentifyAsync(m_subjectToIdentify);
			operation.AddCompletedCallback(&IdentifyFinger::OnIdentifyCompletedCallback, this);
			m_buttonIdentify->Enable(false);
		}

		void IdentifyFinger::OnIdentifyCompletedCallback(const EventArgs & args)
		{
			IdentifyFinger *panel = reinterpret_cast<IdentifyFinger*>(args.GetParam());
			wxCommandEvent ev(wxEVT_IDENTIFYPAGE_IDENTIFY_COMPLETE);
			ev.SetClientData(args.GetObject<NAsyncOperation>().RefHandle());
			wxPostEvent(panel, ev);
		}

		void IdentifyFinger::OnIdentifyCompleted(wxCommandEvent& event)
		{
			NAsyncOperation operation(static_cast<HNObject>(event.GetClientData()), true);
			if (operation.GetError().IsNull())
			{
				NBiometricStatus status = m_subjectToIdentify.GetStatus();
				if (status == nbsOk || status == nbsMatchNotFound)
				{
					wxArrayString m_matchinSubjectIds;
					for (int i = 0; i < m_subjectToIdentify.GetMatchingResults().GetCount(); i++)
					{
						NMatchingResult mr = m_subjectToIdentify.GetMatchingResults().Get(i);
						int mc = m_listResults->GetItemCount();
						m_listResults->InsertItem(mc, wxString::Format(" %s", wxString(mr.GetId())));
						m_listResults->SetItem(mc, 1, wxString::Format(" %d", mr.GetScore()));
						m_matchinSubjectIds.Add(mr.GetId());
					}
					// Non-matching subjects
					for (size_t i = 0; i < m_subjectIds.Count(); i++)
					{
						wxString subjectId = m_subjectIds[i];
						int rc = m_listResults->GetItemCount();
						if (m_matchinSubjectIds.Index(subjectId) == wxNOT_FOUND)
						{
							m_listResults->InsertItem(rc, wxString::Format(" %s", subjectId));
							m_listResults->SetItem(rc, 1, wxString::Format(" %d", 0));
						}
					}
				}
			}
			else
			{
				wxExceptionDlg::Show(operation.GetError());
			}
			m_buttonIdentify->Enable(true);
		}

		void IdentifyFinger::OnButtonDefaultThresholdClicked(wxCommandEvent& WXUNUSED(event))
		{
			m_spinCtrlThreshold->SetValue(m_defaultQualityThreshold);
			m_buttonDefaultThreshold->Enable(false);
		}

		void IdentifyFinger::OnButtonDefaultFARClicked(wxCommandEvent& WXUNUSED(event))
		{
			m_comboBoxFAR->SetValue(Utils::MatchingThresholdToString(m_defaultFar));
			m_buttonDefaultFAR->Enable(false);
		}

		void IdentifyFinger::OnSpinControlThresholdChange(wxCommandEvent& WXUNUSED(event))
		{
			m_buttonDefaultThreshold->Enable(true);
		}

		void IdentifyFinger::OnComboBoxFARChange(wxCommandEvent& WXUNUSED(event))
		{
			m_buttonDefaultFAR->Enable(true);
		}

		void IdentifyFinger::OnCkeckBoxShowBinarizedImageClick(wxCommandEvent& WXUNUSED(event))
		{
			m_fingerView->SetShownImage(m_checkBoxShowBinarizedImage->IsChecked() ? wxNFrictionRidgeView::PROCESSED_IMAGE : wxNFrictionRidgeView::ORIGINAL_IMAGE);
		}
	}
}
