#include "Precompiled.h"
#include "SimpleIrisesSampleForm.h"
#include "EnrollFromImage.h"
#include "EnrollFromScanner.h"
#include "VerifyIris.h"
#include "IdentifyIris.h"
#include "SegmentIris.h"

#ifdef N_MAC_OSX_FRAMEWORKS
#include <NGui/Gui/Neurotechnology.xpm>
#else
#include <Gui/Neurotechnology.xpm>
#endif

using namespace Neurotec::Biometrics;
using namespace Neurotec::Licensing;
using namespace Neurotec::Gui;

namespace Neurotec
{
	namespace Samples
	{
		BEGIN_EVENT_TABLE(SimpleIrisesSampleForm, wxFrame)
			EVT_NOTEBOOK_PAGE_CHANGED(wxID_ANY, SimpleIrisesSampleForm::OnNoteBookPageChange)
		END_EVENT_TABLE()

		SimpleIrisesSampleForm::SimpleIrisesSampleForm(wxWindow *parent, int id, const wxString &title, const wxPoint &position, const wxSize& size, long style)
			: wxFrame(parent, id, title, position, size, style)
		{
			m_biometricClient.SetUseDeviceManager(true);
			m_biometricClient.SetBiometricTypes(nbtIris);
			m_biometricClient.Initialize();
			CreateGUIControls();
		}

		SimpleIrisesSampleForm::~SimpleIrisesSampleForm()
		{
		}

		void SimpleIrisesSampleForm::CreateGUIControls()
		{
			m_notebook = new wxNotebook(this, wxID_ANY, wxDefaultPosition);
			EnrollFromImage* enrollFromImageTab = new EnrollFromImage(m_notebook, m_biometricClient);
			m_notebook->AddPage(enrollFromImageTab, "Enroll From Image", true, 0);
			EnrollFromScanner* enrollFromScannerTab = new EnrollFromScanner(m_notebook, m_biometricClient);
			m_notebook->AddPage(enrollFromScannerTab, "Enroll From Scanner", true, 0);
			VerifyIris* VerifyIrisTab = new VerifyIris(m_notebook, m_biometricClient);
			m_notebook->AddPage(VerifyIrisTab, "Verify Irises", true, 0);
			IdentifyIris* identifyIrisTab = new IdentifyIris(m_notebook, m_biometricClient);
			m_notebook->AddPage(identifyIrisTab, "Identify Irises", true, 0);
			SegmentIris* segmentIrisTab = new SegmentIris(m_notebook, m_biometricClient);
			m_notebook->AddPage(segmentIrisTab, "Segment Iris", true, 0);
			m_notebook->SetSelection(0);
			this->SetIcon(Neurotechnology_XPM);
			this->SetTitle("Simple Irises Sample");
			this->SetWindowStyleFlag(this->GetWindowStyleFlag() | wxFULL_REPAINT_ON_RESIZE);
			this->SetSize(1024, 768);
			this->Fit();
			this->Center();
		}

		void SimpleIrisesSampleForm::OnNoteBookPageChange(wxBookCtrlEvent & WXUNUSED(event))
		{
			m_biometricClient.Cancel();
			m_biometricClient.Reset();
		}
	}
}
