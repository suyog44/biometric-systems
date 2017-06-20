#include "Precompiled.h"
#include "IdentifyFace.h"
#include "Resources/OpenFolderIcon.xpm"
#include "LicensePanel.h"

using namespace Neurotec::Biometrics::Gui;
using namespace Neurotec::Biometrics;
using namespace Neurotec::Gui;
using namespace Neurotec::Biometrics::Client;

namespace Neurotec
{
	namespace Samples
	{
		DEFINE_EVENT_TYPE(EVT_IDENTIFYPAGE_ENROLL_COMPLETED);
		DEFINE_EVENT_TYPE(EVT_IDENTIFYPAGE_EXTRACT_COMPLETED);
		DEFINE_EVENT_TYPE(EVT_IDENTIFYPAGE_IDENTIFY_COMPLETED);
		BEGIN_EVENT_TABLE(IdentifyFace, wxPanel)
			EVT_BUTTON(ID_BUTTON_OPEN_TEMPLATE, IdentifyFace::OnButtonOpenTemplatesClick)
			EVT_BUTTON(ID_BUTTON_OPEN_IMAGE, IdentifyFace::OnButtonOpenImageClick)
			EVT_BUTTON(ID_BUTTON_IDENTIFY, IdentifyFace::OnButtonIdentifyClick)
			EVT_BUTTON(ID_BUTTON_DEFAULT_FAR, IdentifyFace::OnButtonDefaultFarClick)
			EVT_COMBOBOX(ID_COMBOBOX_FAR, IdentifyFace::OnComboBoxFarChange)
			EVT_TEXT(ID_COMBOBOX_FAR, IdentifyFace::OnComboBoxFarChange)
			EVT_COMMAND(wxID_ANY, EVT_IDENTIFYPAGE_ENROLL_COMPLETED, IdentifyFace::OnEnrollCompleted)
			EVT_COMMAND(wxID_ANY, EVT_IDENTIFYPAGE_EXTRACT_COMPLETED, IdentifyFace::OnExtractCompleted)
			EVT_COMMAND(wxID_ANY, EVT_IDENTIFYPAGE_IDENTIFY_COMPLETED, IdentifyFace::OnIdentifyCompleted)
		END_EVENT_TABLE()

		IdentifyFace::IdentifyFace(wxWindow *parent, NBiometricClient &biometricClient) : wxPanel(parent), m_biometricClient(biometricClient), m_subject(NULL), m_enrollTask(NULL)
		{
			CreateGUIControls();
			InitializeBiometricParams();
		}

		void IdentifyFace::CreateGUIControls()
		{
			wxBoxSizer *mainBox = new wxBoxSizer(wxVERTICAL);

			LicensePanel *licensePanel = new LicensePanel(this);
			licensePanel->RefreshComponentsStatus("Biometrics.FaceExtraction,Biometrics.FaceMatching", wxEmptyString);
			mainBox->Add(licensePanel, 0, wxEXPAND | wxALL, 2);

			wxStaticBoxSizer *staticBoxSizerLoading = new wxStaticBoxSizer(wxHORIZONTAL, this, "Templates loading");
			m_buttonOpenTemplate = new wxBitmapButton(this, ID_BUTTON_OPEN_TEMPLATE, openFolderIcon_xpm);
			m_buttonOpenTemplate->SetToolTip("Open template files (*.dat)");
			staticBoxSizerLoading->Add(m_buttonOpenTemplate, 0, wxALIGN_CENTER | wxALL, 2);
			wxStaticText *staticTextTemplatesLoadedLabel = new wxStaticText(this, wxID_ANY, "Templates loaded:");
			staticBoxSizerLoading->Add(staticTextTemplatesLoadedLabel, 0, wxALIGN_CENTER | wxALL, 2);
			m_staticTextTemplatesCount = new wxStaticText(this, wxID_ANY, "0");
			staticBoxSizerLoading->Add(m_staticTextTemplatesCount, 0, wxALIGN_CENTER | wxALL, 2);
			mainBox->Add(staticBoxSizerLoading, 0, wxEXPAND | wxALL, 2);

			//Image template box
			wxStaticBoxSizer *staticBoxSizerImageView = new wxStaticBoxSizer(wxVERTICAL, this, "Image / template for identification");
			wxNViewZoomSlider *zoomslider = new wxNViewZoomSlider(this);
			m_faceView = new wxNFaceView(this);
			m_faceView->SetBackgroundColour(GetBackgroundColour());
			zoomslider->SetView(m_faceView);
			staticBoxSizerImageView->Add(m_faceView, 1, wxEXPAND | wxALL, 2);
			wxBoxSizer *boxSizerImageViewBottom = new wxBoxSizer(wxHORIZONTAL);
			m_buttonOpenImage = new wxButton(this, ID_BUTTON_OPEN_IMAGE, "Open");
			m_buttonOpenImage->SetBitmap(openFolderIcon_xpm);
			m_buttonOpenImage->SetToolTip("Open face image or template for identification");
			m_staticTextFileName = new wxStaticText(this, wxID_ANY, wxEmptyString);
			boxSizerImageViewBottom->Add(m_buttonOpenImage, 0, wxALIGN_CENTER | wxALL, 2);
			boxSizerImageViewBottom->Add(m_staticTextFileName, 0, wxALIGN_CENTER | wxALL, 2);
			boxSizerImageViewBottom->AddStretchSpacer();
			boxSizerImageViewBottom->Add(zoomslider, 0, wxALIGN_CENTER | wxALL, 2);
			staticBoxSizerImageView->Add(boxSizerImageViewBottom, 0, wxEXPAND | wxALL, 2);
			mainBox->Add(staticBoxSizerImageView, 1, wxEXPAND | wxALL, 2);

			wxStaticBoxSizer* staticBoxSizerIdentification = new wxStaticBoxSizer(wxVERTICAL, this, "Identification");
			wxBoxSizer* boxSizerIdentificationControls = new wxBoxSizer(wxHORIZONTAL);
			m_buttonIdentify = new wxButton(this, ID_BUTTON_IDENTIFY, "Identify");
			m_buttonIdentify->Enable(false);
			wxStaticBoxSizer* staticBoxSizerFAR = new wxStaticBoxSizer(wxHORIZONTAL, this, "Matching FAR");
			m_comboBoxFar = new wxComboBox(this, ID_COMBOBOX_FAR, wxEmptyString);
			m_buttonDefaultFar = new wxButton(this, ID_BUTTON_DEFAULT_FAR, "Default");
			m_buttonDefaultFar->Enable(false);
			m_comboBoxFar->Append(Utils::MatchingThresholdToString(36));
			m_comboBoxFar->Append(Utils::MatchingThresholdToString(48));
			m_comboBoxFar->Append(Utils::MatchingThresholdToString(60));
			staticBoxSizerFAR->Add(m_comboBoxFar, 0, wxALIGN_CENTER | wxALL, 2);
			staticBoxSizerFAR->Add(m_buttonDefaultFar, 0, wxALIGN_CENTER | wxALL, 2);
			boxSizerIdentificationControls->Add(m_buttonIdentify, 0, wxALIGN_BOTTOM | wxBOTTOM, 10);
			boxSizerIdentificationControls->Add(staticBoxSizerFAR, 0, wxALIGN_CENTER | wxALL, 2);
			staticBoxSizerIdentification->Add(boxSizerIdentificationControls, 0, wxALL, 2);
			m_listCtrlListView = new wxListCtrl(this, wxID_ANY, wxDefaultPosition, wxDefaultSize, wxLC_REPORT);
			// Add first column
			wxListItem col0;
			col0.SetId(0);
			col0.SetText("ID");
			col0.SetWidth(200);
			m_listCtrlListView->InsertColumn(0, col0);
			// Add second column
			wxListItem col1;
			col1.SetId(1);
			col1.SetText("Score");
			m_listCtrlListView->InsertColumn(1, col1);

			staticBoxSizerIdentification->Add(m_listCtrlListView, 1, wxEXPAND | wxALL, 2);
			mainBox->Add(staticBoxSizerIdentification, 0, wxEXPAND | wxALL, 2);
			SetBackgroundColour(GetParent()->GetBackgroundColour());
			SetSizer(mainBox);
		}

		void IdentifyFace::InitializeBiometricParams()
		{
			m_defaultFar = m_biometricClient.GetMatchingThreshold();
			m_comboBoxFar->SetStringSelection(Utils::MatchingThresholdToString(m_defaultFar));
		}

		void IdentifyFace::OnButtonOpenTemplatesClick(wxCommandEvent& WXUNUSED(event))
		{
			m_buttonIdentify->Enable(false);
			int templateCount = 0;
			m_enrollTask = NULL;
			wxFileDialog wxfdOpenFile(this, "Open Templates Files", wxEmptyString, wxEmptyString,
				wxFileSelectorDefaultWildcardStr, wxFD_OPEN | wxFD_MULTIPLE | wxFD_FILE_MUST_EXIST);
			if (wxfdOpenFile.ShowModal() == wxID_OK)
			{
				m_listCtrlListView->DeleteAllItems();
				try
				{
					wxArrayString files;
					wxfdOpenFile.GetPaths(files);
					m_enrollTask = NBiometricTask(nboEnroll);
					// Create subjects from selected templates
					for (size_t i = 0; i < files.GetCount(); i++)
					{
						NSubject subject = NSubject::FromFile(files[i]);
						subject.SetId(files[i]);
						m_enrollTask.GetSubjects().Add(subject);
					}
					templateCount = m_enrollTask.GetSubjects().GetCount();
					m_buttonIdentify->Enable(!m_subject.IsNull());
				}
				catch (NError& e)
				{
					m_enrollTask = NULL;
					wxExceptionDlg::Show(e);
				}
			}
			m_staticTextTemplatesCount->SetLabel(wxString::Format("%i", templateCount));
		}

		void IdentifyFace::OnButtonOpenImageClick(wxCommandEvent& WXUNUSED(event))
		{
			m_buttonIdentify->Enable(false);
			wxFileDialog wxfdOpenFile(this, "Open Image File Or Template", wxEmptyString,
				wxEmptyString, wxFileSelectorDefaultWildcardStr, wxFD_OPEN | wxFD_FILE_MUST_EXIST);
			if (wxfdOpenFile.ShowModal() == wxID_OK)
			{
				m_subject = NULL;
				m_faceView->SetFace(NULL);
				m_listCtrlListView->DeleteAllItems();
				m_staticTextFileName->SetLabel(wxEmptyString);
				wxString path = wxfdOpenFile.GetPath();
				m_staticTextFileName->SetLabel(path);
				// Check if given file is a template
				try
				{
					m_subject = NSubject::FromFile(path);
					m_buttonIdentify->Enable((!m_enrollTask.IsNull()) && m_enrollTask.GetSubjects().GetCount() > 0);
				}
				catch (NError&)
				{
				}

				if (m_subject.IsNull())
				{
					// If file is not a template, try to load it as image
					m_subject = NSubject();
					NFace face;
					face.SetFileName(path);
					m_subject.GetFaces().Add(face);
					NAsyncOperation operation = m_biometricClient.CreateTemplateAsync(m_subject);
					operation.AddCompletedCallback(&IdentifyFace::OnExtractCompletedCallback, this);
					m_faceView->SetFace(face);
				}
			}
		}

		void IdentifyFace::OnExtractCompletedCallback(EventArgs args)
		{
			IdentifyFace* panel = reinterpret_cast<IdentifyFace*>(args.GetParam());
			wxCommandEvent ev(EVT_IDENTIFYPAGE_EXTRACT_COMPLETED);
			ev.SetClientData(args.GetObject<NAsyncOperation>().RefHandle());
			wxPostEvent(panel, ev);
		}

		void IdentifyFace::OnExtractCompleted(wxCommandEvent& event)
		{
			NAsyncOperation operation(static_cast<HNObject>(event.GetClientData()), true);
			if (operation.GetError().IsNull())
			{
				NBiometricStatus status = m_subject.GetStatus();
				if (status != nbsOk)
				{
					wxString statusString = NEnum::ToString(NBiometricTypes::NBiometricStatusNativeTypeOf(), status);
					wxMessageBox(wxString::Format("Template was not extracted: %s", statusString), wxMessageBoxCaptionStr, wxOK | wxICON_ERROR);
					m_subject = NULL;
					m_buttonIdentify->Enable(false);
				}
				else
				{
					m_buttonIdentify->Enable((!m_enrollTask.IsNull()) && m_enrollTask.GetSubjects().GetCount() > 0);
				}
			}
			else
			{
				wxExceptionDlg::Show(operation.GetError());
				m_subject = NULL;
				m_buttonIdentify->Enable(false);
			}
		}

		void IdentifyFace::OnButtonIdentifyClick(wxCommandEvent& WXUNUSED(event))
		{
			m_listCtrlListView->DeleteAllItems();
			if (!m_subject.IsNull() && !m_enrollTask.IsNull())
			{
				double threshold;
				try
				{
					threshold = Utils::MatchingThresholdFromString(m_comboBoxFar->GetValue());
				}
				catch (NError&)
				{
					wxMessageBox("FAR is not valid", "Error", wxICON_ERROR);
					return;
				}
				m_biometricClient.SetMatchingThreshold(threshold);
				m_comboBoxFar->SetValue(Utils::MatchingThresholdToString(m_biometricClient.GetMatchingThreshold()));
				m_biometricClient.SetMatchingWithDetails(true);
				m_biometricClient.Clear();
				NAsyncOperation operation = m_biometricClient.PerformTaskAsync(m_enrollTask);
				operation.AddCompletedCallback(&IdentifyFace::OnEnrollCompletedCallback, this);
			}
		}

		void IdentifyFace::OnEnrollCompletedCallback(EventArgs args)
		{
			IdentifyFace* panel = reinterpret_cast<IdentifyFace*>(args.GetParam());
			wxCommandEvent ev(EVT_IDENTIFYPAGE_ENROLL_COMPLETED);
			ev.SetClientData(args.GetObject<NAsyncOperation>().RefHandle());
			wxPostEvent(panel, ev);
		}

		void IdentifyFace::OnEnrollCompleted(wxCommandEvent& event)
		{
			NAsyncOperation operation(static_cast<HNObject>(event.GetClientData()), true);
			if (operation.GetError().IsNull())
			{
				NBiometricStatus status = m_enrollTask.GetStatus();
				if (status == nbsOk)
				{
					NAsyncOperation operationIdentify = m_biometricClient.IdentifyAsync(m_subject);
					operationIdentify.AddCompletedCallback(&IdentifyFace::OnIdentifyCompletedCallback, this);
				}
				else
				{
					wxString statusString = NEnum::ToString(NBiometricTypes::NBiometricStatusNativeTypeOf(), status);
					wxMessageBox(wxString::Format("Enrollment failed: %s", statusString));
				}
			}
			else
			{
				wxExceptionDlg::Show(operation.GetError());
			}
		}

		void IdentifyFace::OnIdentifyCompletedCallback(EventArgs args)
		{
			IdentifyFace* panel = static_cast<IdentifyFace*>(args.GetParam());
			wxCommandEvent ev(EVT_IDENTIFYPAGE_IDENTIFY_COMPLETED);
			ev.SetClientData(args.GetObject<NAsyncOperation>().RefHandle());
			wxPostEvent(panel, ev);
		}

		void IdentifyFace::OnIdentifyCompleted(wxCommandEvent& event)
		{
			NAsyncOperation operation(static_cast<HNObject>(event.GetClientData()), true);
			if (operation.GetError().IsNull())
			{
				NBiometricStatus status = m_subject.GetStatus();
				if (status == nbsOk || status == nbsMatchNotFound)
				{
					// Matching subjects
					NSubject::MatchingResultCollection results = m_subject.GetMatchingResults();
					int count = results.GetCount();
					for (int j = 0; j < count; j++)
					{
						NMatchingResult result = results.Get(j);
						m_listCtrlListView->InsertItem(j, result.GetId());
						m_listCtrlListView->SetItem(j, 0, wxFileNameFromPath(result.GetId()));
						m_listCtrlListView->SetItem(j, 1, wxString::Format("%i", result.GetScore()));
					}
					for (int i = 0, index = count; i < m_enrollTask.GetSubjects().GetCount(); i++)
					{
						bool isMatchingResult = false;
						for (int j = 0; j < count; j++)
						{
							if (m_enrollTask.GetSubjects().Get(i).GetId() == results.Get(j).GetId())
							{
								isMatchingResult = true;
								break;
							}
						}
						if (!isMatchingResult)
						{
							NSubject subject = m_enrollTask.GetSubjects().Get(i);
							m_listCtrlListView->InsertItem(index, subject.GetId());
							m_listCtrlListView->SetItem(index, 0, wxFileNameFromPath(subject.GetId()));
							m_listCtrlListView->SetItem(index, 1, "0");
							index++;
						}
					}
				}
				else
				{
					wxString statusString = NEnum::ToString(NBiometricTypes::NBiometricStatusNativeTypeOf(), status);
					wxMessageBox(wxString::Format("Identification failed: %s", statusString));
				}
			}
			else
			{
				wxExceptionDlg::Show(operation.GetError());
			}
		}

		void IdentifyFace::OnButtonDefaultFarClick(wxCommandEvent& WXUNUSED(event))
		{
			m_comboBoxFar->SetValue(Utils::MatchingThresholdToString(m_defaultFar));
			m_buttonDefaultFar->Enable(false);
		}

		void IdentifyFace::OnComboBoxFarChange(wxCommandEvent &WXUNUSED(event))
		{
			m_buttonDefaultFar->Enable(true);
		}
	}
}
