#include "Precompiled.h"
#include "EnrollFromScanner.h"
#include "Resources/SaveIcon.xpm"

using namespace Neurotec::Biometrics;
using namespace Neurotec::Biometrics::Client;
using namespace Neurotec::Devices;
using namespace Neurotec::Gui;
using namespace Neurotec::Biometrics::Gui;
using namespace Neurotec::IO;

#define ENROLL_FROM_SCANNER_REQUIRED_LICENSE_COMPONENTS "Biometrics.IrisExtraction,Devices.IrisScanners"
namespace Neurotec
{
	namespace Samples
	{
		DEFINE_EVENT_TYPE(wxEVT_ENROLLFROMSCANNERPAGE_CAPTURE_COMPLETE);
		BEGIN_EVENT_TABLE(EnrollFromScanner, wxPanel)
			EVT_BUTTON(ID_BUTTON_SAVE_IMAGE, EnrollFromScanner::OnButtonSaveImageClick)
			EVT_BUTTON(ID_BUTTON_SAVE_TEMPLATE, EnrollFromScanner::OnButtonSaveTemplateClick)
			EVT_BUTTON(ID_BUTTON_REFRESH, EnrollFromScanner::OnButtonRefreshClick)
			EVT_BUTTON(ID_BUTTON_SCAN, EnrollFromScanner::OnButtonScanClick)
			EVT_BUTTON(ID_BUTTON_CANCEL, EnrollFromScanner::OnButtonCancelClick)
			EVT_BUTTON(ID_SCANNAR_SELECTED, EnrollFromScanner::OnListScannerSelectionChange)
			EVT_BUTTON(ID_BUTTON_FORCE, EnrollFromScanner::OnButtonForceClick)
			EVT_COMMAND(wxID_ANY, wxEVT_ENROLLFROMSCANNERPAGE_CAPTURE_COMPLETE, EnrollFromScanner::OnCaptureCompleted)
		END_EVENT_TABLE()

		EnrollFromScanner::EnrollFromScanner(wxWindow *parent, const NBiometricClient & biometricClient)
			: wxPanel(parent), m_biometricClient(biometricClient), m_subject(NULL)
		{
			CreateGUIControls();
			UpdateScannersList();
			UpdateControlStatus(false);
		}

		void EnrollFromScanner::CreateGUIControls()
		{
			wxBoxSizer *boxSizerMain = new wxBoxSizer(wxVERTICAL);
			wxBoxSizer *boxSizerLicensePanel = new wxBoxSizer(wxHORIZONTAL);
			wxBoxSizer *boxSizerScannerList = new wxBoxSizer(wxHORIZONTAL);
			wxBoxSizer *boxSizerIrisSelection = new wxBoxSizer(wxVERTICAL);
			wxStaticBoxSizer *staticBxSizerScannerCtrl = new wxStaticBoxSizer(wxVERTICAL, this, "Scanners List");
			wxBoxSizer *boxSizerBtnContainer = new wxBoxSizer(wxHORIZONTAL);
			wxBoxSizer *boxSizerIrisView = new wxBoxSizer(wxHORIZONTAL);
			wxBoxSizer *boxSizerStatus = new wxBoxSizer(wxHORIZONTAL);

			LicensePanel *licencePanel = new LicensePanel(this);
			licencePanel->RefreshComponentsStatus(ENROLL_FROM_SCANNER_REQUIRED_LICENSE_COMPONENTS, wxEmptyString);
			boxSizerLicensePanel->Add(licencePanel, 1, wxEXPAND | wxALL, 2);

			m_listboxScanners = new wxListBox(this, ID_SCANNAR_SELECTED, wxDefaultPosition, wxDefaultSize, 0, NULL, wxBORDER_SIMPLE, wxDefaultValidator, wxEmptyString);
			boxSizerScannerList->Add(m_listboxScanners, 1, wxALL, 2);
			m_radioButtonLeftIris = new wxRadioButton(this, ID_RADIOBUTTON_RIGHT_IRIS, "Left Iris");
			m_radioButtonLeftIris->SetValue(true);
			boxSizerIrisSelection->Add(m_radioButtonLeftIris, 0, wxALL, 2);
			m_radioButtonRightIris = new wxRadioButton(this, ID_RADIOBUTTON_LEFT_IRIS, "Right Iris");
			boxSizerIrisSelection->Add(m_radioButtonRightIris, 0, wxALL, 2);
			boxSizerScannerList->Add(boxSizerIrisSelection, 0, wxALL, 2);

			m_buttonRefreshList = new wxButton(this, ID_BUTTON_REFRESH, "Refresh list", wxDefaultPosition, wxSize(-1, 25));
			boxSizerBtnContainer->Add(m_buttonRefreshList, 0, wxALIGN_CENTER | wxALL, 2);
			m_buttonScan = new wxButton(this, ID_BUTTON_SCAN, "Scan", wxDefaultPosition, wxSize(-1, 25));
			boxSizerBtnContainer->Add(m_buttonScan, 0, wxALIGN_CENTER | wxALL, 2);
			m_buttonCancel = new wxButton(this, ID_BUTTON_CANCEL, "Cancel", wxDefaultPosition, wxSize(-1, 25));
			boxSizerBtnContainer->Add(m_buttonCancel, 0, wxALIGN_CENTER | wxALL, 2);
			m_buttonForce = new wxButton(this, ID_BUTTON_FORCE, "Force", wxDefaultPosition, wxSize(-1, 25));
			boxSizerBtnContainer->Add(m_buttonForce, 0, wxALIGN_CENTER | wxALL, 2);
			m_checkBxScanAutomatically = new wxCheckBox(this, ID_CHECKBOX_AUTO_SCAN, "Scan automatically");
			boxSizerBtnContainer->Add(m_checkBxScanAutomatically, 0, wxALIGN_CENTER | wxALL, 2);
			m_checkBxScanAutomatically->SetValue(true);

			m_zoomSlider = new wxNViewZoomSlider(this);
			m_irisView = new wxNIrisView(this);
			boxSizerIrisView->Add(m_irisView, 1, wxEXPAND | wxALL, 2);

			m_staticTxtStatus = new wxStaticText(this, wxID_ANY, wxEmptyString);
			boxSizerStatus->Add(m_staticTxtStatus, 0, wxALIGN_CENTER | wxALL, 2);
			boxSizerStatus->AddStretchSpacer();
			m_zoomSlider->SetView(m_irisView);
			boxSizerStatus->Add(m_zoomSlider);
			m_buttonSaveImage = new wxButton(this, ID_BUTTON_SAVE_IMAGE, "Save Image", wxDefaultPosition, wxSize(-1, 25));
			m_buttonSaveImage->SetBitmap(wxImage(saveIcon_xpm));
			m_buttonSaveImage->SetToolTip("Save iris image to a file");
			boxSizerStatus->Add(m_buttonSaveImage, 0, wxALIGN_CENTER | wxALL, 2);
			m_buttonSaveTemplate = new wxButton(this, ID_BUTTON_SAVE_TEMPLATE, "Save Template", wxDefaultPosition, wxSize(-1, 25));
			m_buttonSaveTemplate->SetBitmap(wxImage(saveIcon_xpm));
			m_buttonSaveTemplate->SetToolTip("Save iris image to a file");
			boxSizerStatus->Add(m_buttonSaveTemplate, 0, wxALIGN_CENTER | wxALL, 2);

			staticBxSizerScannerCtrl->Add(boxSizerScannerList, 0, wxEXPAND | wxALL, 2);
			staticBxSizerScannerCtrl->Add(boxSizerBtnContainer, 0, wxALL, 2);
			boxSizerMain->Add(boxSizerLicensePanel, 0, wxALL | wxEXPAND, 2);
			boxSizerMain->Add(staticBxSizerScannerCtrl, 0, wxALL | wxEXPAND, 2);
			boxSizerMain->Add(boxSizerIrisView, 1, wxALL | wxEXPAND, 2);
			boxSizerMain->Add(boxSizerStatus, 0, wxALL | wxEXPAND, 2);

			SetBackgroundColour(GetParent()->GetBackgroundColour());
			SetSizerAndFit(boxSizerMain);
		}

		EnrollFromScanner::~EnrollFromScanner()
		{
			m_biometricClient.Cancel();
		}

		void EnrollFromScanner::UpdateScannersList()
		{
			try
			{
				m_listboxScanners->Clear();
				NArrayWrapper<NDevice> devices = m_biometricClient.GetDeviceManager().GetDevices().GetAll();
				for (int i = 0; i < devices.GetCount(); i++)
				{
					m_listboxScanners->Append(devices[i].GetDisplayName(), new wxStringClientData(devices[i].GetId()));
				}
				if (devices.GetCount() > 0)
				{
					if (m_biometricClient.GetIrisScanner().IsNull())
					{
						m_listboxScanners->SetSelection(0);
						m_biometricClient.SetIrisScanner(NObjectDynamicCast<NIrisScanner>(devices[0]));
					}
					else
					{
						m_listboxScanners->SetSelection(m_listboxScanners->FindString(m_biometricClient.GetIrisScanner().GetDisplayName()));
					}
				}
			}
			catch (NError & ex)
			{
				wxExceptionDlg::Show(ex);
			}
		}

		void EnrollFromScanner::OnListScannerSelectionChange(wxCommandEvent& WXUNUSED(event))
		{
			NDevice device = NULL;
			wxStringClientData* clientData = static_cast<wxStringClientData *>(m_listboxScanners->GetClientObject(m_listboxScanners->GetSelection()));
			if (clientData != NULL)
			{
				wxString scannerId = clientData->GetData();
				NDeviceManager::NDeviceCollection devices = m_biometricClient.GetDeviceManager().GetDevices();
				device = devices[scannerId];
			}
			m_biometricClient.SetIrisScanner(NObjectDynamicCast<NIrisScanner>(device));
		}

		void EnrollFromScanner::OnButtonRefreshClick(wxCommandEvent& WXUNUSED(event))
		{
			UpdateScannersList();
		}

		void EnrollFromScanner::OnButtonSaveTemplateClick(wxCommandEvent& WXUNUSED(event))
		{
			wxFileDialog saveDialog(this, "Save As", wxEmptyString, wxEmptyString, wxEmptyString, wxFD_SAVE | wxFD_OVERWRITE_PROMPT);
			if (saveDialog.ShowModal() == wxID_OK)
			{
				try
				{
					Neurotec::IO::NFile::WriteAllBytes(saveDialog.GetPath(), m_subject.GetTemplateBuffer());
				}
				catch (NError & ex)
				{
					wxExceptionDlg::Show(ex);
				}
			}
		}

		void EnrollFromScanner::OnButtonSaveImageClick(wxCommandEvent& WXUNUSED(event))
		{
			wxFileDialog dialog(this, "Save As", wxEmptyString, wxEmptyString, Common::GetSaveFileFilterString(), wxFD_SAVE | wxFD_OVERWRITE_PROMPT);
			if (dialog.ShowModal() == wxID_OK)
			{
				try
				{
					m_subject.GetIrises()[0].GetImage().Save(dialog.GetPath());
				}
				catch (NError & ex)
				{
					wxExceptionDlg::Show(ex);
				}
			}
		}

		void EnrollFromScanner::OnButtonScanClick(wxCommandEvent& WXUNUSED(event))
		{
			if (m_biometricClient.GetIrisScanner().IsNull())
			{
				wxMessageBox("Please select a scanner");
			}
			else
			{
				m_staticTxtStatus->SetLabelText(wxEmptyString);

				NIris iris;
				iris.SetPosition(m_radioButtonRightIris->GetValue() ? nepRight : nepLeft);
				if (!m_checkBxScanAutomatically->IsChecked())
				{
					iris.SetCaptureOptions(nbcoManual);
				}

				m_subject = NSubject();
				m_subject.GetIrises().Add(iris);
				m_irisView->SetIris(iris);

				NBiometricTask task = m_biometricClient.CreateTask((NBiometricOperations)(nboCapture | nboCreateTemplate), m_subject);
				NAsyncOperation operation = m_biometricClient.PerformTaskAsync(task);
				operation.AddCompletedCallback(&EnrollFromScanner::OnCaptureCompletedCallback, this);
				UpdateControlStatus(true);
			}
		}

		void EnrollFromScanner::OnCaptureCompletedCallback(const EventArgs & args)
		{
			EnrollFromScanner *enrollFrmScannerPanel = reinterpret_cast<EnrollFromScanner *>(args.GetParam());
			wxCommandEvent ev(wxEVT_ENROLLFROMSCANNERPAGE_CAPTURE_COMPLETE);
			ev.SetClientData(args.GetObject<NAsyncOperation>().RefHandle());
			wxPostEvent(enrollFrmScannerPanel, ev);
		}

		void EnrollFromScanner::OnCaptureCompleted(wxCommandEvent& event)
		{
			NAsyncOperation operation(static_cast<HNObject>(event.GetClientData()), true);
			NBiometricTask task = operation.GetResult().ToObject<NBiometricTask>();
			NBiometricStatus status = task.GetStatus();

			if (status != nbsCanceled)
			{
				if (operation.GetError().IsNull())
				{
					if (status == nbsOk)
					{
						m_staticTxtStatus->SetLabelText("OK");
					}
					else
					{

						wxString statusString = NEnum::ToString(NBiometricTypes::NBiometricStatusNativeTypeOf(), status);
						m_staticTxtStatus->SetLabelText("Capture Error");
						wxMessageBox("The template was not extracted. Status = " + statusString, "Capture Error", wxOK | wxCENTRE | wxICON_ERROR);
					}
				}
				else
				{
					wxExceptionDlg::Show(operation.GetError());
				}
			}
			else
			{
				m_staticTxtStatus->SetLabelText("Cancelled");
			}
			UpdateControlStatus(false);
		}

		void EnrollFromScanner::OnButtonCancelClick(wxCommandEvent& WXUNUSED(event))
		{
			m_irisView->SetIris(NULL);
			m_biometricClient.Cancel();
		}

		void EnrollFromScanner::UpdateControlStatus(bool capturing)
		{
			m_buttonCancel->Enable(capturing);
			m_buttonScan->Enable(!capturing);
			m_buttonRefreshList->Enable(!capturing);
			m_radioButtonRightIris->Enable(!capturing);
			m_radioButtonLeftIris->Enable(!capturing);
			m_buttonSaveImage->Enable(!capturing  && !m_subject.IsNull() && m_subject.GetIrises().GetCount() > 0 && !m_subject.GetIrises()[0].GetImage().IsNull());
			m_buttonSaveTemplate->Enable(!capturing && !m_subject.IsNull() && m_subject.GetStatus() == nbsOk);
			m_buttonForce->Enable(capturing && !m_checkBxScanAutomatically->IsChecked());
			m_checkBxScanAutomatically->Enable(!capturing);
		}

		void EnrollFromScanner::OnButtonForceClick(wxCommandEvent& WXUNUSED(event))
		{
			m_biometricClient.Force();
		}
	}
}
