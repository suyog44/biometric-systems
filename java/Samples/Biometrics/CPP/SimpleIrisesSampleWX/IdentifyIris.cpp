#include "Precompiled.h"
#include "IdentifyIris.h"
#include "Resources/OpenFolderIcon.xpm"

using namespace Neurotec::Biometrics;
using namespace Neurotec::Biometrics::Client;
using namespace Neurotec::Gui;
using namespace Neurotec::Biometrics::Gui;

#define IDENTIFICATION_REQUIRED_LICENSE_COMPONENTS "Biometrics.IrisExtraction,Biometrics.IrisMatching"
namespace Neurotec
{
	namespace Samples
	{
		DEFINE_EVENT_TYPE(wxEVT_IDENTIFYPAGE_ENROLL_COMPLETE)
			DEFINE_EVENT_TYPE(wxEVT_IDENTIFYPAGE_IDENTIFY_COMPLETE)
			DEFINE_EVENT_TYPE(wxEVT_IDENTIFYPAGE_CREATE_TEMPLATE_COMPLETE)
			BEGIN_EVENT_TABLE(IdentifyIris, wxPanel)
			EVT_BUTTON(ID_BUTTON_OPEN_TEMPLATE, IdentifyIris::OnButtonOpenTemplatesClick)
			EVT_BUTTON(ID_BUTTON_OPEN_IMAGE, IdentifyIris::OnButtonOpenTemplatesOrImageClick)
			EVT_BUTTON(ID_BUTTON_IDENTIFY, IdentifyIris::OnButtonIdentifyClick)
			EVT_BUTTON(ID_BUTTON_DEFAULT_FAR, IdentifyIris::OnButtonDefaultFARClick)
			EVT_COMBOBOX(ID_COMBOBOX_FAR, IdentifyIris::OnComboBoxFARChange)
			EVT_TEXT(ID_COMBOBOX_FAR, IdentifyIris::OnComboBoxFARChange)
			EVT_COMMAND(wxID_ANY, wxEVT_IDENTIFYPAGE_ENROLL_COMPLETE, IdentifyIris::OnEnrollCompleted)
			EVT_COMMAND(wxID_ANY, wxEVT_IDENTIFYPAGE_IDENTIFY_COMPLETE, IdentifyIris::OnIdentifyCompleted)
			EVT_COMMAND(wxID_ANY, wxEVT_IDENTIFYPAGE_CREATE_TEMPLATE_COMPLETE, IdentifyIris::OnCreateTemplateCompleted)
		END_EVENT_TABLE()

		IdentifyIris::IdentifyIris(wxWindow *parent, const NBiometricClient & biometricClient)
			: wxPanel(parent), m_hasEnrolledSubjects(false), m_biometricClient(biometricClient), m_subject(NULL)
		{
			m_defaultFar = m_biometricClient.GetMatchingThreshold();
			CreateGUIControls();
		}

		void IdentifyIris::CreateGUIControls()
		{
			wxImage imageOpen(openFolderIcon_xpm);

			wxBoxSizer *boxSizerMain = new wxBoxSizer(wxVERTICAL);
			LicensePanel *licencePanel = new LicensePanel(this);
			licencePanel->RefreshComponentsStatus(IDENTIFICATION_REQUIRED_LICENSE_COMPONENTS, wxEmptyString);
			boxSizerMain->Add(licencePanel, 0, wxEXPAND | wxALL, 2);

			wxStaticBoxSizer *staticBoxTemplateLoadning = new wxStaticBoxSizer(wxHORIZONTAL, this, "Templates loading");
			m_buttonOpenTemplate = new wxBitmapButton(this, ID_BUTTON_OPEN_TEMPLATE, imageOpen);
			m_buttonOpenTemplate->SetToolTip("Open tempale files (*.dat)");
			staticBoxTemplateLoadning->Add(m_buttonOpenTemplate, 0, wxALIGN_CENTER | wxALL, 2);
			wxStaticText *staticTextTemplate = new  wxStaticText(this, wxID_ANY, "Templates loaded :");
			staticBoxTemplateLoadning->Add(staticTextTemplate, 0, wxALIGN_CENTER | wxALL, 2);
			m_staticTextNumberOfTemplates = new  wxStaticText(this, wxID_ANY, "0");
			staticBoxTemplateLoadning->Add(m_staticTextNumberOfTemplates, 0, wxALIGN_CENTER | wxALL, 2);
			boxSizerMain->Add(staticBoxTemplateLoadning, 0, wxEXPAND | wxALL, 2);

			wxStaticBoxSizer *staticBoxSizerIrisView = new wxStaticBoxSizer(wxVERTICAL, this, "Image/template for identification");
			m_zoomSlider = new wxNViewZoomSlider(this);
			m_irisView = new wxNIrisView(this);
			staticBoxSizerIrisView->Add(m_irisView, 1, wxEXPAND | wxALL, 2);
			wxBoxSizer *boxSizerOpenImage = new wxBoxSizer(wxHORIZONTAL);
			m_buttonOpenImage = new wxButton(this, ID_BUTTON_OPEN_IMAGE, "Open", wxDefaultPosition, wxSize(-1, 25));
			m_buttonOpenImage->SetBitmap(imageOpen);
			m_buttonOpenImage->SetToolTip("Open iris image for identification");
			boxSizerOpenImage->Add(m_buttonOpenImage, 0, wxALIGN_CENTER | wxALL, 2);
			m_staticTextImageInfo = new wxStaticText(this, wxID_ANY, wxEmptyString);
			boxSizerOpenImage->Add(m_staticTextImageInfo, 0, wxALIGN_CENTER | wxALL, 2);
			boxSizerOpenImage->AddStretchSpacer();
			m_zoomSlider->SetView(m_irisView);
			boxSizerOpenImage->Add(m_zoomSlider, 0, wxEXPAND | wxALL, 2);
			staticBoxSizerIrisView->Add(boxSizerOpenImage, 0, wxEXPAND, 0);
			boxSizerMain->Add(staticBoxSizerIrisView, 1, wxEXPAND | wxALL, 2);

			wxStaticBoxSizer *staticBoxSizerIdentification = new wxStaticBoxSizer(wxVERTICAL, this, "Identification");
			wxBoxSizer *boxSizerIdentifyBtn = new wxBoxSizer(wxHORIZONTAL);
			m_buttonIdentify = new wxButton(this, ID_BUTTON_IDENTIFY, "Identify", wxDefaultPosition, wxSize(-1, 25));
			m_buttonIdentify->Enable(false);
			boxSizerIdentifyBtn->Add(m_buttonIdentify, 0, wxALIGN_BOTTOM | wxBOTTOM, 6);
			wxStaticBoxSizer *staticBoxSizerMatchingFAR = new wxStaticBoxSizer(wxHORIZONTAL, this, "Matching FAR");
			m_comboMatchingFAR = new wxComboBox(this, ID_COMBOBOX_FAR);
			m_comboMatchingFAR->Append(Utils::MatchingThresholdToString(36));
			m_comboMatchingFAR->Append(Utils::MatchingThresholdToString(48));
			m_comboMatchingFAR->Append(Utils::MatchingThresholdToString(60));
			staticBoxSizerMatchingFAR->Add(m_comboMatchingFAR, 0, wxALIGN_CENTER | wxALL, 2);
			m_buttonDefaultMatchingFAR = new wxButton(this, ID_BUTTON_DEFAULT_FAR, "Default", wxDefaultPosition, wxSize(-1, 25));
			staticBoxSizerMatchingFAR->Add(m_buttonDefaultMatchingFAR, 0, wxALIGN_CENTER | wxALL, 2);
			m_comboMatchingFAR->SetValue(Utils::MatchingThresholdToString(m_defaultFar));
			m_buttonDefaultMatchingFAR->Enable(false);
			boxSizerIdentifyBtn->AddStretchSpacer(1);
			boxSizerIdentifyBtn->Add(staticBoxSizerMatchingFAR, 0, wxALIGN_BOTTOM | wxBOTTOM, 2);
			staticBoxSizerIdentification->Add(boxSizerIdentifyBtn, 0, wxEXPAND | wxALL, 2);

			m_listCtrlResults = new wxListCtrl(this, wxID_ANY, wxDefaultPosition, wxSize(529, 127), wxLC_REPORT);
			m_listCtrlResults->AppendColumn("ID", wxLIST_FORMAT_LEFT, 300);
			m_listCtrlResults->AppendColumn("Score", wxLIST_FORMAT_LEFT, -1);
			m_listCtrlResults->Enable(true);
			staticBoxSizerIdentification->Add(m_listCtrlResults, 0, wxEXPAND | wxALL, 2);
			boxSizerMain->Add(staticBoxSizerIdentification, 0, wxEXPAND | wxALL, 2);

			SetBackgroundColour(GetParent()->GetBackgroundColour());
			SetSizerAndFit(boxSizerMain);
		}

		IdentifyIris::~IdentifyIris()
		{
		}

		void IdentifyIris::OnComboBoxFARChange(wxCommandEvent& WXUNUSED(event))
		{
			m_buttonDefaultMatchingFAR->Enable(m_comboMatchingFAR->GetSelection() != 1);
			m_buttonDefaultMatchingFAR->Enable(true);
		}

		void IdentifyIris::OnButtonDefaultFARClick(wxCommandEvent& WXUNUSED(event))
		{
			m_comboMatchingFAR->SetValue(Utils::MatchingThresholdToString(m_defaultFar));
			m_buttonDefaultMatchingFAR->Enable(false);
		}

		bool IdentifyIris::IsSubjectValid(const NSubject & subject)
		{
			return !subject.IsNull() && (subject.GetStatus() == nbsOk || (subject.GetStatus() == nbsNone && !subject.GetTemplateBuffer().IsNull()));
		}

		void IdentifyIris::OnButtonOpenTemplatesClick(wxCommandEvent& WXUNUSED(event))
		{
			m_buttonIdentify->Enable(false);
			m_staticTextNumberOfTemplates->SetLabelText("0");
			wxFileDialog openDialog(this, "Open Template Files", wxEmptyString, wxEmptyString, wxEmptyString, wxFD_OPEN | wxFD_MULTIPLE | wxFD_FILE_MUST_EXIST);
			if (openDialog.ShowModal() == wxID_OK)
			{
				NBiometricTask task = m_biometricClient.CreateTask(nboEnroll, NULL);
				wxArrayString selectedFiles;
				openDialog.GetPaths(selectedFiles);
				openDialog.GetFilenames(m_selectedFileNames);
				int numberOfFiles = selectedFiles.size();
				m_biometricClient.Clear();
				m_staticTextNumberOfTemplates->SetLabelText("0");
				try
				{
					for (int i = 0; i < numberOfFiles; i++)
					{
						NSubject enrollingSubject = NSubject::FromFile(selectedFiles[i]);
						enrollingSubject.SetId(m_selectedFileNames[i]);
						task.GetSubjects().Add(enrollingSubject);
					}
					if (task.GetSubjects().GetCount() > 0)
					{
						m_hasEnrolledSubjects = false;
						NAsyncOperation operation = m_biometricClient.PerformTaskAsync(task);
						operation.AddCompletedCallback(&IdentifyIris::OnEnrollCompletedCallback, this);
					}
				}
				catch (NError & ex)
				{
					wxExceptionDlg::Show(ex);
				}
			}
		}

		void IdentifyIris::OnEnrollCompletedCallback(const EventArgs & args)
		{
			IdentifyIris *identifyPanel = reinterpret_cast<IdentifyIris *>(args.GetParam());
			wxCommandEvent ev(wxEVT_IDENTIFYPAGE_ENROLL_COMPLETE);
			ev.SetClientData(args.GetObject<NAsyncOperation>().RefHandle());
			wxPostEvent(identifyPanel, ev);
		}

		void IdentifyIris::OnEnrollCompleted(wxCommandEvent& event)
		{
			NAsyncOperation operation(static_cast<HNObject>(event.GetClientData()), true);
			if (operation.GetError().IsNull())
			{
				NBiometricTask task = operation.GetResult().ToObject < NBiometricTask >();
				NBiometricStatus status = task.GetStatus();
				if (status == nbsOk)
				{
					m_hasEnrolledSubjects = true;
					m_buttonIdentify->Enable(IsSubjectValid(m_subject));
					m_staticTextNumberOfTemplates->SetLabelText(wxString::Format("%d", task.GetSubjects().GetCount()));
				}
				else
				{
					wxString statusString = NEnum::ToString(NBiometricTypes::NBiometricStatusNativeTypeOf(), status);
					wxMessageBox("Enrollment failed: " + statusString);
				}
			}
			else
			{
				wxExceptionDlg::Show(operation.GetError());
			}
		}

		void IdentifyIris::OnButtonOpenTemplatesOrImageClick(wxCommandEvent& WXUNUSED(event))
		{
			m_staticTextImageInfo->SetLabelText(wxEmptyString);
			m_buttonIdentify->Enable(false);

			wxFileDialog openDialog(this, "Open Image or Template File", wxEmptyString, wxEmptyString, wxEmptyString, wxFD_OPEN | wxFD_FILE_MUST_EXIST);
			if (openDialog.ShowModal() == wxID_OK)
			{
				m_listCtrlResults->DeleteAllItems();
				m_subject = NULL;
				m_staticTextImageInfo->SetLabelText(openDialog.GetPath());
				try
				{
					m_subject = NSubject::FromFile(openDialog.GetPath());
					m_buttonIdentify->Enable(m_hasEnrolledSubjects);
				}
				catch (NError & /*ex*/)
				{
				}
				if (m_subject.IsNull())
				{
					m_subject = NSubject();
					NIris iris = NIris();
					iris.SetFileName(openDialog.GetPath());
					m_subject.GetIrises().Add(iris);
					m_irisView->SetIris(iris);
					NBiometricTask task = m_biometricClient.CreateTask(nboCreateTemplate, m_subject);
					NAsyncOperation operation = m_biometricClient.PerformTaskAsync(task);
					operation.AddCompletedCallback(&IdentifyIris::OnCreateTemplateCompletedCallback, this);
				}
			}
		}

		void IdentifyIris::OnCreateTemplateCompletedCallback(const EventArgs & args)
		{
			IdentifyIris *identifyPanel = reinterpret_cast<IdentifyIris*>(args.GetParam());
			wxCommandEvent ev(wxEVT_IDENTIFYPAGE_CREATE_TEMPLATE_COMPLETE);
			ev.SetClientData(args.GetObject<NAsyncOperation>().RefHandle());
			wxPostEvent(identifyPanel, ev);
		}

		void IdentifyIris::OnCreateTemplateCompleted(wxCommandEvent& event)
		{
			NAsyncOperation operation(static_cast<HNObject>(event.GetClientData()), true);
			if (operation.GetError().IsNull())
			{
				if (m_subject.GetStatus() != nbsOk)
				{
					wxMessageBox(wxString::Format("Template was not extracted, Error: %s",
						wxString(NEnum::ToString(NBiometricTypes::NBiometricStatusNativeTypeOf(), m_subject.GetStatus()))),
						"Error", wxICON_ERROR);
					m_subject = NULL;
					m_buttonIdentify->Enable(false);
				}
				else
				{
					m_buttonIdentify->Enable(m_hasEnrolledSubjects);
				}
			}
			else
			{
				wxExceptionDlg::Show(operation.GetError());
				m_buttonIdentify->Enable(false);
			}
		}

		void IdentifyIris::OnButtonIdentifyClick(wxCommandEvent& WXUNUSED(event))
		{
			double threshold;
			try
			{
				threshold = Utils::MatchingThresholdFromString(m_comboMatchingFAR->GetValue());
			}
			catch (NError & /*ex*/)
			{
				wxMessageBox("FAR is not valid", "Error", wxICON_ERROR);
				return;
			}
			m_biometricClient.SetMatchingThreshold(threshold);
			NAsyncOperation operation = m_biometricClient.IdentifyAsync(m_subject);
			operation.AddCompletedCallback(&IdentifyIris::OnIdentifyCompletedCallback, this);
		}

		void IdentifyIris::OnIdentifyCompletedCallback(const EventArgs & args)
		{
			IdentifyIris *identifyPanel = reinterpret_cast<IdentifyIris*>(args.GetParam());
			wxCommandEvent ev(wxEVT_IDENTIFYPAGE_IDENTIFY_COMPLETE);
			ev.SetClientData(args.GetObject<NAsyncOperation>().RefHandle());
			wxPostEvent(identifyPanel, ev);
		}

		void IdentifyIris::OnIdentifyCompleted(wxCommandEvent& event)
		{
			NAsyncOperation operation(static_cast<HNObject>(event.GetClientData()), true);
			if (operation.GetError().IsNull())
			{
				NBiometricStatus status = m_subject.GetStatus();
				m_listCtrlResults->DeleteAllItems();

				if (status == nbsOk || status == nbsMatchNotFound)
				{
					NSubject::MatchingResultCollection results = m_subject.GetMatchingResults();
					for (int i = 0; i < results.GetCount();i++)
					{
						long index = m_listCtrlResults->InsertItem(i, m_subject.GetMatchingResults()[i].GetId());
						m_listCtrlResults->SetItem(index, 1, wxString::Format("%d", m_subject.GetMatchingResults()[i].GetScore()));
					}
					for (unsigned int j = 0; j < m_selectedFileNames.Count();j++)
					{
						bool isMatchingResult = false;
						for (int k = 0; k < results.GetCount(); k++)
						{
							if (m_selectedFileNames[j].IsSameAs( m_subject.GetMatchingResults()[k].GetId()))
							{
								isMatchingResult = true;
								break;
							}
						}
						if (!isMatchingResult)
						{
							long index = m_listCtrlResults->InsertItem(m_listCtrlResults->GetItemCount(), m_selectedFileNames[j]);
							m_listCtrlResults->SetItem(index, 1, wxString::Format("%d",0));
						}
					}
				}
				else
				{
					wxString statusString = NEnum::ToString(NBiometricTypes::NBiometricStatusNativeTypeOf(), status);
					wxMessageBox("Identification failed: " + statusString);
				}
			}
			else
			{
				wxExceptionDlg::Show(operation.GetError());
			}
		}
	}
}
