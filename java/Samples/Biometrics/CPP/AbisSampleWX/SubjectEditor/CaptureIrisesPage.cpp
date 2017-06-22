#include "Precompiled.h"

#include <SubjectEditor/CaptureIrisesPage.h>

#include <Resources/OpenFolderIcon.xpm>

using namespace Neurotec::Biometrics;
using namespace Neurotec::Biometrics::Gui;
using namespace Neurotec::Biometrics::Client;
using namespace Neurotec::Devices;
using namespace Neurotec::Gui;
using namespace Neurotec::Images;

namespace Neurotec { namespace Samples
{

wxDECLARE_EVENT(wxEVT_CAPTURE_IRIS_THREAD, wxCommandEvent);
wxDEFINE_EVENT(wxEVT_CAPTURE_IRIS_THREAD, wxCommandEvent);

CaptureIrisesPage::CaptureIrisesPage(NBiometricClient& biometricClient, NSubject& subject, SubjectEditorPageInterface& subjectEditorPageInterface, wxWindow *parent, wxWindowID winid) :
	ModalityPage(biometricClient, subject, subjectEditorPageInterface, parent, winid),
	m_newSubject(NULL),
	m_iris(NULL),
	m_irisView(NULL)
{
	CreateGUIControls();
	RegisterGuiEvents();
}

CaptureIrisesPage::~CaptureIrisesPage()
{
	m_zoomSlider->SetView(NULL);
}

void CaptureIrisesPage::SetIsBusy(bool value)
{
	if (value)
	{
		m_isIdle.Reset();
	}
	else
	{
		m_isIdle.Set();
	}
}

void CaptureIrisesPage::OnThread(wxCommandEvent &event)
{
	int id = event.GetId();

	switch(id)
	{
	case ID_EVENT_CAPTURE_FINISHED:
		{
			NAsyncOperation operation(static_cast<HNObject>(event.GetClientData()), true);
			bool updateStatus = true;
			if (!operation.IsCanceled())
			{
				NError exception = operation.GetError();
				if (!exception.IsNull())
				{
					UpdateStatus(nbsInternalError);
					updateStatus = false;
					wxExceptionDlg::Show(exception);
				}
			}

			m_iris.RemovePropertyChangedCallback(&CaptureIrisesPage::OnIrisPropertyChangedCallback, this);
			if (updateStatus) UpdateStatus(m_iris.GetStatus());

			if (m_iris.GetStatus() != nbsOk)
			{
				m_newSubject = NULL;
				m_iris = NULL;
			}

			EnableControls();
			m_btnCancel->Disable();
			m_btnForce->Disable();

			SetIsBusy(false);
			break;
		}
	case ID_EVENT_SCANNER_CHANGED:
		{
			bool isScanner = m_radioScanner->GetValue();

			if (ResetScanner())
			{
				m_radioScanner->Enable(!IsBusy());
			}
			else
			{
				m_radioScanner->Disable();

				if (isScanner)
				{
					m_radioScanner->SetValue(true);
				}
			}

			wxPostEvent(m_radioFile, wxCommandEvent(wxEVT_RADIOBUTTON, m_radioFile->GetId()));

			this->Layout();

			break;
		}
	default:
		break;
	};
}

void CaptureIrisesPage::CaptureAsyncCompletedCallback(EventArgs args)
{
	CaptureIrisesPage *page = reinterpret_cast<CaptureIrisesPage *>(args.GetParam());
	wxCommandEvent event(wxEVT_CAPTURE_IRIS_THREAD, ID_EVENT_CAPTURE_FINISHED);
	event.SetClientData(args.GetObject<NAsyncOperation>().RefHandle());
	wxPostEvent(page, event);
}

void CaptureIrisesPage::OnBiometricClientPropertyChangedCallback(NObject::PropertyChangedEventArgs args)
{
	if (args.GetPropertyName() == N_T("IrisScanner"))
	{
		wxPostEvent(static_cast<CaptureIrisesPage *>(args.GetParam()), wxCommandEvent(wxEVT_CAPTURE_IRIS_THREAD, ID_EVENT_SCANNER_CHANGED));
	}
}

void CaptureIrisesPage::OnIrisPropertyChangedCallback(NObject::PropertyChangedEventArgs args)
{
	NIris iris = args.GetObject<NIris>();
	CaptureIrisesPage *page = reinterpret_cast<CaptureIrisesPage *>(args.GetParam());

	if (args.GetPropertyName().Equals(N_T("Status")))
	{
		page->UpdateStatus(iris.GetStatus());
	}
}

void CaptureIrisesPage::OnForceClick(wxCommandEvent&)
{
	m_biometricClient.Force();
}

void CaptureIrisesPage::OnCancelClick(wxCommandEvent&)
{
	m_biometricClient.Cancel();
}

void CaptureIrisesPage::OnFinishClick(wxCommandEvent&)
{
	SelectFirstPage();
}

void CaptureIrisesPage::OnCaptureClick(wxCommandEvent&)
{
	wxString filePath = wxEmptyString;

	if (m_radioFile->GetValue())
	{
		wxFileDialog openFileDialog(this, wxT("Choose iris image to extract"), wxEmptyString, wxEmptyString,
			Common::GetOpenFileFilterString(true, true), wxFD_OPEN | wxFD_FILE_MUST_EXIST);

		if ((openFileDialog.ShowModal() != wxID_OK) || (openFileDialog.GetPath() == wxEmptyString))
			return;

		filePath = openFileDialog.GetPath();
	}

	DisableControls();

	m_btnCancel->Enable(true);
	m_btnForce->Enable(!m_chbCaptureAutomatically->GetValue());
	m_statusPanel->Show(true);

	this->Layout();

	m_iris = NIris();
	m_newSubject = NSubject();

	m_iris.SetFileName(filePath);

	if (m_radioScanner->GetValue())
	{
		m_iris.SetPosition((NEPosition)NEnum::Parse(NBiometricTypes::NEPositionNativeTypeOf(), m_choiceScannerPosition->GetStringSelection()));
		m_iris.SetCaptureOptions(m_chbCaptureAutomatically->GetValue() ? nbcoNone : nbcoManual);
	}
	else
		m_iris.SetPosition((NEPosition)NEnum::Parse(NBiometricTypes::NEPositionNativeTypeOf(), m_choicePosition->GetStringSelection()));

	m_newSubject.GetIrises().Add(m_iris);

	m_irisView->SetIris(m_iris);
	m_iris.AddPropertyChangedCallback(&CaptureIrisesPage::OnIrisPropertyChangedCallback, this);

	NAsyncOperation operation = m_biometricClient.CreateTemplateAsync(m_newSubject);
	SetIsBusy(true);
	operation.AddCompletedCallback(&CaptureIrisesPage::CaptureAsyncCompletedCallback, this);
}

void CaptureIrisesPage::OnSourceChange(wxCommandEvent&)
{
	if (m_radioScanner->GetValue())
	{
		try
		{
			SelectScannerSource();
		}
		catch (NError& ex)
		{
			wxExceptionDlg::Show(ex);
			SelectFileSource();
		}
	}
	else
	{
		SelectFileSource();
	}
}

void CaptureIrisesPage::UpdateStatus(::Neurotec::Biometrics::NBiometricStatus status)
{
	wxString strStatus = NEnum::ToString(NBiometricTypes::NBiometricStatusNativeTypeOf(), status);
	m_statusPanel->SetMessage(wxString::Format(wxT("Extraction status: %s"), strStatus),
		(status == nbsOk || status == nbsNone)? StatusPanel::SUCCESS_MESSAGE : StatusPanel::ERROR_MESSAGE);
}

bool CaptureIrisesPage::ResetScanner()
{
	bool isScannerOk = false;

	NIrisScanner scanner = m_biometricClient.GetIrisScanner();

	if (!scanner.IsNull())
	{
		wxString scannerName = wxEmptyString;

		try
		{
			scannerName = scanner.GetDisplayName();

			isScannerOk = true;
		}
		catch(NError&)
		{
			isScannerOk = false;
		}

		if (isScannerOk)
		{
			m_radioScanner->SetLabelText(wxT("Scanner (") + scannerName + wxT(")"));
		}
	}

	if (!isScannerOk)
	{
		m_radioScanner->SetLabelText(wxT("Scanner (Not connected)"));
	}

	return isScannerOk;
}

void CaptureIrisesPage::EnableControls()
{
	m_btnCapture->Enable(true);
	m_btnOpenImage->Enable(true);
	m_btnForce->Enable(true);
	m_btnCancel->Enable(true);
	m_radioFile->Enable(true);
	m_choicePosition->Enable(true);
	m_choiceScannerPosition->Enable(true);
	m_chbCaptureAutomatically->Enable(true);

	if (ResetScanner())
	{
		m_radioScanner->Enable(true);
	}
	else
	{
		m_radioScanner->Disable();
		m_radioFile->SetValue(true);
	}

	wxPostEvent(m_radioFile, wxCommandEvent(wxEVT_RADIOBUTTON, m_radioFile->GetId()));

	IsBusy() ? m_busyIndicator->Show() : m_busyIndicator->Hide();
	m_statusSizer->RecalcSizes();

	this->Layout();
}

void CaptureIrisesPage::DisableControls()
{
	bool isScaner = m_radioScanner->GetValue();
	bool isFile = m_radioFile->GetValue();

	m_btnCapture->Enable(false);
	m_btnForce->Enable(false);
	m_btnCancel->Enable(false);
	m_radioFile->Enable(false);
	m_radioScanner->Enable(false);
	m_choicePosition->Enable(false);
	m_choiceScannerPosition->Enable(false);
	m_chbCaptureAutomatically->Enable(false);

	m_radioScanner->SetValue(isScaner);
	m_radioFile->SetValue(isFile);

	this->Layout();
}

void CaptureIrisesPage::OnNavigatedTo()
{
	m_radioScanner->SetValue(true);
	EnableControls();

	m_biometricClient.AddPropertyChangedCallback(&CaptureIrisesPage::OnBiometricClientPropertyChangedCallback, this);
	m_statusPanel->Hide();
	m_btnForce->Enable(false);
	m_btnCancel->Enable(false);
	m_chbCaptureAutomatically->SetValue(true);
	m_irisView->SetIris(NULL);

	this->Layout();

	ModalityPage::OnNavigatedTo();
}

void CaptureIrisesPage::OnNavigatingFrom()
{
	Cancel();

	m_biometricClient.RemovePropertyChangedCallback(&CaptureIrisesPage::OnBiometricClientPropertyChangedCallback, this);

	if (!m_newSubject.IsNull())
	{
		NArrayWrapper<NIris> irises = m_newSubject.GetIrises().GetAll();
		m_newSubject.GetIrises().Clear();
		m_newSubject = NULL;

		for (NArrayWrapper<NIris>::iterator it = irises.begin(); it != irises.end(); it++)
		{
			m_subject.GetIrises().Add(*it);
		}
	}

	m_iris = NULL;
	m_irisView->SetIris(NULL);

	ModalityPage::OnNavigatingFrom();
}

void CaptureIrisesPage::SelectScannerSource()
{
	NIrisScanner scanner = m_biometricClient.GetIrisScanner();

	if (scanner.IsNull())
		return;

	wxArrayString strPositions;

	try
	{
		NArrayWrapper<NEPosition> positions = scanner.GetSupportedPositions();

		for (int i = 0; i < positions.GetCount(); ++i)
		{
			strPositions.Add(NEnum::ToString(NBiometricTypes::NEPositionNativeTypeOf(), positions[i]));
		}
	}
	catch(NError& ex)
	{
		wxExceptionDlg::Show(ex);

		strPositions.Add(NEnum::ToString(NBiometricTypes::NEPositionNativeTypeOf(), nepUnknown));
	}

	m_radioScanner->SetValue(true);
	m_choicePosition->Hide();
	m_btnOpenImage->Hide();
	m_choiceScannerPosition->Show(true);
	m_btnCapture->Show(true);
	m_btnForce->Show(true);
	m_chbCaptureAutomatically->Show(true);

	m_choiceScannerPosition->Set(strPositions);
	m_choiceScannerPosition->SetSelection(0);

	this->Layout();
}

void CaptureIrisesPage::SelectFileSource()
{
	m_radioFile->SetValue(true);
	m_choicePosition->Show(true);
	m_btnOpenImage->Show(true);
	m_choiceScannerPosition->Hide();
	m_btnCapture->Hide();
	m_btnForce->Hide();
	m_chbCaptureAutomatically->Hide();

	this->Layout();
}

void CaptureIrisesPage::RegisterGuiEvents()
{
	this->Bind(wxEVT_CAPTURE_IRIS_THREAD, &CaptureIrisesPage::OnThread, this);
	m_radioScanner->Connect(wxEVT_RADIOBUTTON, wxCommandEventHandler(CaptureIrisesPage::OnSourceChange), NULL, this);
	m_radioFile->Connect(wxEVT_RADIOBUTTON, wxCommandEventHandler(CaptureIrisesPage::OnSourceChange), NULL, this);
	m_btnCapture->Connect(wxEVT_BUTTON, wxCommandEventHandler(CaptureIrisesPage::OnCaptureClick), NULL, this);
	m_btnOpenImage->Connect(wxEVT_BUTTON, wxCommandEventHandler(CaptureIrisesPage::OnCaptureClick), NULL, this);
	m_btnForce->Connect(wxEVT_BUTTON, wxCommandEventHandler(CaptureIrisesPage::OnForceClick), NULL, this);
	m_btnCancel->Connect(wxEVT_BUTTON, wxCommandEventHandler(CaptureIrisesPage::OnCancelClick), NULL, this);
	m_btnFinish->Connect(wxEVT_BUTTON, wxCommandEventHandler(CaptureIrisesPage::OnFinishClick), NULL, this);
}

void CaptureIrisesPage::UnregisterGuiEvents()
{
	m_radioScanner->Disconnect(wxEVT_RADIOBUTTON, wxCommandEventHandler(CaptureIrisesPage::OnSourceChange), NULL, this);
	m_radioFile->Disconnect(wxEVT_RADIOBUTTON, wxCommandEventHandler(CaptureIrisesPage::OnSourceChange), NULL, this);
	m_btnCapture->Disconnect(wxEVT_BUTTON, wxCommandEventHandler(CaptureIrisesPage::OnCaptureClick), NULL, this);
	m_btnOpenImage->Disconnect(wxEVT_BUTTON, wxCommandEventHandler(CaptureIrisesPage::OnCaptureClick), NULL, this);
	m_btnForce->Connect(wxEVT_BUTTON, wxCommandEventHandler(CaptureIrisesPage::OnForceClick), NULL, this);
	m_btnCancel->Disconnect(wxEVT_BUTTON, wxCommandEventHandler(CaptureIrisesPage::OnCancelClick), NULL, this);
	m_btnFinish->Disconnect(wxEVT_BUTTON, wxCommandEventHandler(CaptureIrisesPage::OnFinishClick), NULL, this);
	this->Unbind(wxEVT_CAPTURE_IRIS_THREAD, &CaptureIrisesPage::OnThread, this);
}

void CaptureIrisesPage::CreateGUIControls()
{
	wxBoxSizer *sizer = new wxBoxSizer(wxVERTICAL);
	this->SetSizer(sizer, true);

	wxStaticBoxSizer *szOptions = new wxStaticBoxSizer(new wxStaticBox(this, wxID_ANY, wxT("Capture options")), wxVERTICAL);
	sizer->Add(szOptions, 0, wxALL, 5);

	wxBoxSizer *szBox = new wxBoxSizer(wxVERTICAL);
	szOptions->Add(szBox, 1, wxALL | wxEXPAND);

	m_radioScanner = new wxRadioButton(this, wxID_ANY, wxT("Scanner"));
	szBox->Add(m_radioScanner, 0, wxALL, 5);

	m_radioFile = new wxRadioButton(this, wxID_ANY, wxT("File"));
	szBox->Add(m_radioFile, 0, wxALL, 5);

	m_chbCaptureAutomatically = new wxCheckBox(this, wxID_ANY, wxT("Capture automatically"));
	szBox->Add(m_chbCaptureAutomatically, 0, wxALL, 5);

	wxBoxSizer *szBoxNew = new wxBoxSizer(wxHORIZONTAL);
	szBox->Add(szBoxNew, 0, wxALL);

	wxStaticText *text = new wxStaticText(this, wxID_ANY, wxT("Position:"));
	szBoxNew->Add(text, 0, wxALL | wxALIGN_CENTER_VERTICAL, 5);

	wxArrayString positions;
	positions.Add(NEnum::ToString(NBiometricTypes::NEPositionNativeTypeOf(), nepLeft));
	positions.Add(NEnum::ToString(NBiometricTypes::NEPositionNativeTypeOf(), nepRight));
	positions.Add(NEnum::ToString(NBiometricTypes::NEPositionNativeTypeOf(), nepUnknown));

	m_choicePosition = new wxChoice(this, wxID_ANY);
	m_choicePosition->Set(positions);
	m_choicePosition->SetSelection(0);
	szBoxNew->Add(m_choicePosition, 0, wxALL | wxALIGN_CENTER_VERTICAL, 5);

	m_choiceScannerPosition = new wxChoice(this, wxID_ANY);
	szBoxNew->Add(m_choiceScannerPosition, 0, wxALL | wxALIGN_CENTER_VERTICAL, 5);

	m_btnCapture = new wxButton(this, wxID_ANY, wxT("Capture"));
	szBoxNew->Add(m_btnCapture, 0, wxALL | wxALIGN_CENTER_VERTICAL, 5);

	m_btnOpenImage = new wxButton(this, wxID_ANY, wxT("Open image"));
	m_btnOpenImage->SetBitmap(wxImage(wxImage(openFolderIcon_xpm)));
	m_btnOpenImage->Fit();
	szBoxNew->Add(m_btnOpenImage, 0, wxALL | wxALIGN_CENTER_VERTICAL, 5);

	m_irisView = new wxNIrisView(this, wxID_ANY);
	m_irisView->EnableContextMenu(false);
	m_irisView->SetWindowStyle(wxSIMPLE_BORDER);
	m_irisView->SetBackgroundColour(wxNullColour);
	sizer->Add(m_irisView, 1, wxALL | wxEXPAND, 5);

	m_statusSizer = new wxBoxSizer(wxHORIZONTAL);
	sizer->Add(m_statusSizer, 0, wxLEFT | wxRIGHT | wxEXPAND, 5);

	m_busyIndicator = new BusyIndicator( this, wxID_ANY, wxDefaultPosition, wxSize(14, 14) );
	m_busyIndicator->Hide();
	m_statusSizer->Add( m_busyIndicator, 0, wxRIGHT, 5 );

	m_statusPanel = new StatusPanel(this, wxID_ANY);
	m_statusSizer->Add( m_statusPanel, 1, wxEXPAND, 5 );

	szBox = new wxBoxSizer(wxHORIZONTAL);
	sizer->Add(szBox, 0, wxALL | wxEXPAND, 5);

	wxBoxSizer *szControls = new wxBoxSizer(wxHORIZONTAL);
	szBox->Add(szControls, 1, wxALL | wxEXPAND);

	m_zoomSlider = new wxNViewZoomSlider(this);
	m_zoomSlider->SetView(m_irisView);
	szControls->Add(m_zoomSlider, 0, wxALL);

	szControls->AddSpacer(1);

	szControls = new wxBoxSizer(wxHORIZONTAL);
	szBox->Add(szControls, 0, wxALL | wxEXPAND);

	m_btnForce = new wxButton(this, wxID_ANY, wxT("Force"), wxDefaultPosition, wxDefaultSize);
	szControls->Add(m_btnForce, 0, wxALL | wxALIGN_CENTER_VERTICAL, 5);

	m_btnCancel = new wxButton(this, wxID_ANY, wxT("Cancel"), wxDefaultPosition, wxDefaultSize);
	szControls->Add(m_btnCancel, 0, wxRIGHT | wxALIGN_CENTER_VERTICAL, 5);

	m_btnFinish = new wxButton(this, wxID_ANY, wxT("Finish"), wxDefaultPosition, wxDefaultSize);
	szControls->Add(m_btnFinish, 0, wxALL | wxALIGN_CENTER_VERTICAL);

	this->Layout();
}

}}

