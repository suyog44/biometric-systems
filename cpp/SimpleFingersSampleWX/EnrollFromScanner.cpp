#include "Precompiled.h"
#include "Resources/SaveIcon.xpm"
#include "EnrollFromScanner.h"
#include "LicensePanel.h"

using namespace Neurotec::Biometrics;
using namespace Neurotec::Biometrics::Client;
using namespace Neurotec::Biometrics::Gui;
using namespace Neurotec::Images;
using namespace Neurotec::Devices;
using namespace Neurotec::Gui;
using namespace Neurotec::IO;

namespace Neurotec
{
	namespace Samples
	{
		DEFINE_EVENT_TYPE(wxEVT_ENROLLFROMSCANNERPAGE_CAPTURE_COMPLETE)
		DEFINE_EVENT_TYPE(wxEVT_ENROLLFROMSCANNERPAGE_ATTRIBUTE_PROPERTY_CHANGED)
		BEGIN_EVENT_TABLE(EnrollFromScanner, wxPanel)
			EVT_BUTTON(ID_BUTTON_REFRESH_SCANNERS, EnrollFromScanner::OnButtonRefreshScannersClick)
			EVT_LISTBOX(ID_LISTBOX_SCANNERS, EnrollFromScanner::OnScannerChanged)
			EVT_BUTTON(ID_BUTTON_SCAN, EnrollFromScanner::OnButtonScanClick)
			EVT_BUTTON(ID_BUTTON_CANCEL, EnrollFromScanner::OnButtonCancelClick)
			EVT_CHECKBOX(ID_CHECKBOX_SHOW_BINARIZED_IMAGE, EnrollFromScanner::OnCheckBoxShowBinarizedImageClick)
			EVT_CHECKBOX(ID_CHECKBOX_EXTRACT_AUTOMATICALLY, EnrollFromScanner::OnCheckBoxExtractAutomaticallyClick)
			EVT_BUTTON(ID_BUTTON_SAVE_IMAGE, EnrollFromScanner::OnButtonSaveImageClick)
			EVT_BUTTON(ID_BUTTON_SAVE_TEMPLATE, EnrollFromScanner::OnButtonSaveTemplateClick)
			EVT_BUTTON(ID_BUTTON_FORCE, EnrollFromScanner::OnButtonForceClick)
			EVT_COMMAND(wxID_ANY, wxEVT_ENROLLFROMSCANNERPAGE_CAPTURE_COMPLETE, EnrollFromScanner::OnCaptureCompleted)
			EVT_COMMAND(wxID_ANY, wxEVT_ENROLLFROMSCANNERPAGE_ATTRIBUTE_PROPERTY_CHANGED, EnrollFromScanner::OnFingerPropertyChanged)
		END_EVENT_TABLE()

		EnrollFromScanner::EnrollFromScanner(wxWindow *parent, const NBiometricClient & biometricClient, wxWindowID id, const wxPoint & pos, const wxSize & size, long style, const wxString & name)
		: wxPanel(parent, id, pos, size, style, name), m_biometricClient(biometricClient), m_subject(NULL), m_finger(NULL)
		{
			CreateGUIControls();
			RefreshScannerList();
			EnableControls(false);
		}

		EnrollFromScanner::~EnrollFromScanner()
		{
			m_biometricClient.Cancel();
		}

		void EnrollFromScanner::CreateGUIControls()
		{
			wxBoxSizer *mainSizer = new wxBoxSizer(wxVERTICAL);
			wxString licences = "Biometrics.FingerExtraction,Devices.FingerScanners";
			wxString licencesOptional = "Images.WSQ";
			LicensePanel *licencePanel;
			licencePanel = new LicensePanel(this, wxID_ANY, wxDefaultPosition, wxDefaultSize, wxBORDER_SIMPLE, wxEmptyString);
			licencePanel->RefreshComponentsStatus(licences, licencesOptional);
			mainSizer->Add(licencePanel, 0, wxALL | wxEXPAND, 2);

			wxStaticBoxSizer *staticBoxSizerScaners = new wxStaticBoxSizer(wxVERTICAL, this, "Scanner List");
			m_listBoxScaners = new wxListBox(this, ID_LISTBOX_SCANNERS, wxDefaultPosition, wxDefaultSize);
			wxBoxSizer *boxSizerScannerButtons = new wxBoxSizer(wxHORIZONTAL);
			m_buttonRefresh = new wxButton(this, ID_BUTTON_REFRESH_SCANNERS, "Refresh List", wxDefaultPosition, wxSize(-1, 25));
			m_buttonScan = new wxButton(this, ID_BUTTON_SCAN, "Scan", wxDefaultPosition, wxSize(-1, 25));
			m_buttonCancel = new wxButton(this, ID_BUTTON_CANCEL, "Cancel", wxDefaultPosition, wxSize(-1, 25));
			m_buttonForce = new wxButton(this, ID_BUTTON_FORCE, "Force", wxDefaultPosition, wxSize(-1, 25));
			m_checkBoxScanAuto = new wxCheckBox(this, ID_CHECKBOX_EXTRACT_AUTOMATICALLY, "Scan automatically");
			m_checkBoxScanAuto->SetValue(true);
			boxSizerScannerButtons->Add(m_buttonRefresh, 0, wxALIGN_CENTER | wxALL, 2);
			boxSizerScannerButtons->Add(m_buttonScan, 0, wxALIGN_CENTER | wxALL, 2);
			boxSizerScannerButtons->Add(m_buttonCancel, 0, wxALIGN_CENTER | wxALL, 2);
			boxSizerScannerButtons->Add(m_buttonForce, 0, wxALIGN_CENTER | wxALL, 2);
			boxSizerScannerButtons->Add(m_checkBoxScanAuto, 0, wxALL | wxALIGN_CENTER, 2);
			staticBoxSizerScaners->Add(m_listBoxScaners, 0, wxEXPAND | wxALL, 2);
			staticBoxSizerScaners->Add(boxSizerScannerButtons, 0, wxALL, 0);
			mainSizer->Add(staticBoxSizerScaners, 0, wxEXPAND | wxALL, 2);

			wxNViewZoomSlider *zoomSlider = new wxNViewZoomSlider(this);
			m_fingerView = new wxNFingerView(this, wxID_ANY);
			m_staticTextFingerQuality = new wxStaticText(this, wxID_ANY, wxEmptyString);
			mainSizer->Add(m_fingerView, 1, wxEXPAND | wxALL, 2);
			mainSizer->Add(m_staticTextFingerQuality, 0, wxALIGN_CENTER | wxEXPAND | wxALL, 2);

			wxBoxSizer *boxSizerLowerActionButtons = new wxBoxSizer(wxHORIZONTAL);
			wxImage saveImage(saveIcon_xpm);
			m_buttonSaveImage = new wxButton(this, ID_BUTTON_SAVE_IMAGE, "Save image", wxDefaultPosition, wxSize(-1, 25));
			m_buttonSaveImage->SetBitmap(saveImage);
			m_buttonSaveImage->SetToolTip("Save fingerprint image to a file");
			m_buttonSaveTemplate = new wxButton(this, ID_BUTTON_SAVE_TEMPLATE, "Save Template", wxDefaultPosition, wxSize(-1, 25));
			m_buttonSaveTemplate->SetBitmap(saveImage);
			m_buttonSaveTemplate->SetToolTip("Save extracted fingerprint template to a file");
			m_buttonSaveTemplate->Enable(false);
			m_checkBoxShowBinarizedImage = new wxCheckBox(this, ID_CHECKBOX_SHOW_BINARIZED_IMAGE, "Show binarized image");
			m_checkBoxShowBinarizedImage->SetValue(false);
			zoomSlider->SetView(m_fingerView);
			boxSizerLowerActionButtons->Add(m_buttonSaveImage, 0, wxALIGN_CENTER | wxALL, 2);
			boxSizerLowerActionButtons->Add(m_buttonSaveTemplate, 0, wxALIGN_CENTER | wxALL, 2);
			boxSizerLowerActionButtons->Add(m_checkBoxShowBinarizedImage, 0, wxALIGN_CENTER | wxALL, 2);
			boxSizerLowerActionButtons->AddStretchSpacer();
			boxSizerLowerActionButtons->Add(zoomSlider, 0, wxALIGN_CENTER | wxALL, 2);
			mainSizer->Add(boxSizerLowerActionButtons, 0, wxEXPAND | wxALL, 2);
			SetBackgroundColour(GetParent()->GetBackgroundColour());
			SetSizer(mainSizer);
		}

		void EnrollFromScanner::EnableControls(bool isCapturing)
		{
			m_buttonScan->Enable(!isCapturing);
			m_buttonCancel->Enable(isCapturing);
			m_listBoxScaners->Enable(!isCapturing);
			m_buttonForce->Enable(isCapturing && !m_checkBoxScanAuto->GetValue());
			m_checkBoxScanAuto->Enable(!isCapturing);
			m_buttonRefresh->Enable(!isCapturing);
			m_buttonSaveImage->Enable(!isCapturing && !m_finger.IsNull()
				&& !m_finger.GetImage().IsNull());
			m_checkBoxShowBinarizedImage->Enable(!isCapturing && (!m_finger.IsNull()) && (!m_finger.GetBinarizedImage ().IsNull()));
		}

		void EnrollFromScanner::OnButtonRefreshScannersClick(wxCommandEvent& WXUNUSED(event))
		{
			RefreshScannerList();
		}

		void EnrollFromScanner::OnScannerChanged(wxCommandEvent& WXUNUSED(event))
		{
			NDevice device = NULL;
			wxStringClientData* clientData = reinterpret_cast<wxStringClientData *>(m_listBoxScaners->GetClientObject(m_listBoxScaners->GetSelection()));
			if (clientData != NULL)
			{
				wxString scannerId = clientData->GetData();
				NDeviceManager::NDeviceCollection devices = m_biometricClient.GetDeviceManager().GetDevices();
				device = devices.Get(scannerId);
			}
			m_biometricClient.SetFingerScanner(NObjectDynamicCast<NFScanner>(device));
		}

		void EnrollFromScanner::RefreshScannerList()
		{
			m_listBoxScaners->Clear();
			for (int i = 0; i < m_biometricClient.GetDeviceManager().GetDevices().GetCount(); i++)
			{
				NDevice device = m_biometricClient.GetDeviceManager().GetDevices().Get(i);
				m_listBoxScaners->Append(device.GetDisplayName(), new wxStringClientData(device.GetId()));
			}
		}

		void EnrollFromScanner::OnCheckBoxExtractAutomaticallyClick(wxCommandEvent& WXUNUSED(event))
		{
			EnableControls(false);
		}

		void EnrollFromScanner::OnButtonScanClick(wxCommandEvent& WXUNUSED(event))
		{
			m_fingerView->SetFinger(NULL);
			m_finger = NULL;
			m_buttonSaveTemplate->Enable(false);
			if (!m_biometricClient.GetFingerScanner().IsNull())
			{
				m_subject = NSubject();
				m_finger = NFinger();
				m_subject.GetFingers().Add(m_finger);
				m_fingerView->SetFinger(m_finger);
				m_finger.SetCaptureOptions(m_checkBoxScanAuto->GetValue() ? nbcoNone : nbcoManual);
				m_finger.AddPropertyChangedCallback(&EnrollFromScanner::OnFingerPropertyChangedCallback, this);
				NBiometricTask task = m_biometricClient.CreateTask((NBiometricOperations)(nboCreateTemplate | nboCapture), m_subject);
				NAsyncOperation operation = m_biometricClient.PerformTaskAsync(task);
				operation.AddCompletedCallback(&EnrollFromScanner::OnCaptureCompletedCallback, this);
				EnableControls(true);
			}
			else
			{
				wxMessageBox("Please select the Scanner from the list");
			}
		}

		void EnrollFromScanner::OnCaptureCompletedCallback(const EventArgs & args)
		{
			EnrollFromScanner *panel = reinterpret_cast<EnrollFromScanner*>(args.GetParam());
			wxCommandEvent ev(wxEVT_ENROLLFROMSCANNERPAGE_CAPTURE_COMPLETE);
			ev.SetClientData(args.GetObject<NAsyncOperation>().RefHandle());
			wxPostEvent(panel, ev);
		}

		void EnrollFromScanner::OnCaptureCompleted(wxCommandEvent& event)
		{
			NBiometricStatus status = m_subject.GetStatus();
			if (status != nbsCanceled)
			{
				NAsyncOperation operation(static_cast<HNObject>(event.GetClientData()), true);
				if (!operation.GetError().IsNull())
				{
					wxExceptionDlg::Show(operation.GetError());
					return;
				}
				if (status == nbsOk)
				{
					int quality = m_finger.GetObjects()[0].GetQuality();
					m_staticTextFingerQuality->SetLabel(wxString::Format("Quality: %d", quality));
					m_buttonSaveTemplate->Enable(true);
				}
				else
				{
					wxString sStatus = NEnum::ToString(NBiometricTypes::NBiometricStatusNativeTypeOf(), status);
					wxMessageBox(wxString::Format("The template was not extracted: %s.", sStatus), "Error", wxICON_ERROR);
				}
			}
			EnableControls(false);
		}

		void EnrollFromScanner::OnFingerPropertyChangedCallback(const NFinger::PropertyChangedEventArgs & evtArg)
		{
			if (evtArg.GetPropertyName().Equals("Status"))
			{
				EnrollFromScanner *panel = reinterpret_cast<EnrollFromScanner*>(evtArg.GetParam());
				wxCommandEvent ev(wxEVT_ENROLLFROMSCANNERPAGE_ATTRIBUTE_PROPERTY_CHANGED);
				wxPostEvent(panel, ev);
			}
		}

		void EnrollFromScanner::OnFingerPropertyChanged(wxCommandEvent& WXUNUSED(event))
		{
			wxString statusString = NEnum::ToString(NBiometricTypes::NBiometricStatusNativeTypeOf(), m_finger.GetStatus());
			m_staticTextFingerQuality->SetLabelText(statusString);
		}

		void EnrollFromScanner::OnButtonForceClick(wxCommandEvent& WXUNUSED(event))
		{
			m_biometricClient.Force();
		}

		void EnrollFromScanner::OnButtonCancelClick(wxCommandEvent& WXUNUSED(event))
		{
			m_biometricClient.Cancel();
		}

		void EnrollFromScanner::OnCheckBoxShowBinarizedImageClick(wxCommandEvent& WXUNUSED(event))
		{
			m_fingerView->SetShownImage(m_checkBoxShowBinarizedImage->IsChecked() ? wxNFrictionRidgeView::PROCESSED_IMAGE : wxNFrictionRidgeView::ORIGINAL_IMAGE);
		}

		void EnrollFromScanner::OnButtonSaveImageClick(wxCommandEvent& WXUNUSED(event))
		{
			try
			{
				wxFileDialog dialog(this, "Save Image File", wxEmptyString, wxEmptyString, Common::GetSaveFileFilterString(), wxFD_SAVE | wxFD_OVERWRITE_PROMPT);
				if (dialog.ShowModal() == wxID_OK)
				{
					if (m_fingerView->GetShownImage() == wxNFingerView::ORIGINAL_IMAGE)
					{
						m_finger.GetImage().Save(dialog.GetPath());
					}
					else
					{
						m_finger.GetBinarizedImage ().Save(dialog.GetPath());
					}
				}
			}
			catch (NError& ex)
			{
				wxExceptionDlg::Show(ex);
			}
		}

		void EnrollFromScanner::OnButtonSaveTemplateClick(wxCommandEvent& WXUNUSED(event))
		{
			wxFileDialog saveDialog(this, "Save Template File", wxEmptyString, wxEmptyString, wxEmptyString, wxFD_SAVE | wxFD_OVERWRITE_PROMPT);
			try
			{
				if (saveDialog.ShowModal() == wxID_OK)
				{
					NFile::WriteAllBytes(saveDialog.GetPath(), m_subject.GetTemplateBuffer());
				}
			}
			catch (NError& ex)
			{
				wxExceptionDlg::Show(ex);
			}
		}
	}
}
