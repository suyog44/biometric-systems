#include "Precompiled.h"
#include "SimpleFingersSampleForm.h"
#include "EnrollFromImage.h"
#include "EnrollFromScanner.h"
#include "IdentifyFinger.h"
#include "VerifyFinger.h"
#include "SegmentFingerprints.h"
#include "GeneralizeFinger.h"

using namespace Neurotec::Biometrics::Client;
using namespace Neurotec::Licensing;
using namespace Neurotec::Biometrics;

#ifdef N_MAC_OSX_FRAMEWORKS
#include <NGui/Gui/Neurotechnology.xpm>
#else
#include <Gui/Neurotechnology.xpm>
#endif

namespace Neurotec
{
	namespace Samples
	{
		BEGIN_EVENT_TABLE(SimpleFingersSampleForm, wxFrame)
			EVT_NOTEBOOK_PAGE_CHANGED(wxID_ANY, SimpleFingersSampleForm::OnNoteBookPageChange)
		END_EVENT_TABLE()

		SimpleFingersSampleForm::SimpleFingersSampleForm(wxWindow *parent, wxWindowID id, const wxString & title, const wxPoint & pos, const wxSize & size, long style, const wxString & name)
			: wxFrame(parent, id, title, pos, size, style, name)
		{
			SetIcon(Neurotechnology_XPM);
			SetLabel("Simple Fingers Sample");
			m_biometricClient.SetBiometricTypes(nbtFinger);
			m_biometricClient.SetFingersReturnBinarizedImage (true);
			m_biometricClient.SetUseDeviceManager(true);
			m_biometricClient.Initialize();
			CreateGUIControls();
		}

		SimpleFingersSampleForm::~SimpleFingersSampleForm()
		{
		}

		void SimpleFingersSampleForm::CreateGUIControls()
		{
			wxNotebook *nb = new wxNotebook(this, wxID_ANY, wxDefaultPosition, wxDefaultSize);
			nb->AddPage(new EnrollFromImage(nb, m_biometricClient, wxID_ANY, wxDefaultPosition, wxDefaultSize, wxTAB_TRAVERSAL), "Enroll from Image", true);
			nb->AddPage(new EnrollFromScanner(nb, m_biometricClient, wxID_ANY, wxDefaultPosition, wxDefaultSize, wxTAB_TRAVERSAL), "Enroll from Scanner");
			nb->AddPage(new IdentifyFinger(nb, m_biometricClient, wxID_ANY, wxDefaultPosition, wxDefaultSize, wxTAB_TRAVERSAL), "Identify Finger");
			nb->AddPage(new VerifyFinger(nb, m_biometricClient, wxID_ANY, wxDefaultPosition, wxDefaultSize, wxTAB_TRAVERSAL), "Verify Finger");
			nb->AddPage(new SegmentFingerprints(nb, m_biometricClient, wxID_ANY, wxDefaultPosition, wxDefaultSize, wxTAB_TRAVERSAL), "Segment Fingers");
			nb->AddPage(new GeneralizeFinger(nb, m_biometricClient, wxID_ANY, wxDefaultPosition, wxDefaultSize, wxTAB_TRAVERSAL), "Generalize Fingers");
			Layout();
			SetWindowStyleFlag(this->GetWindowStyleFlag() | wxFULL_REPAINT_ON_RESIZE);
			SetSize(1024, 768);
			Center();
		}

		void SimpleFingersSampleForm::OnNoteBookPageChange(wxBookCtrlEvent& WXUNUSED(event))
		{
			if (!m_biometricClient.IsNull())
			{
				m_biometricClient.Cancel();
				m_biometricClient.Reset();
				m_biometricClient.SetFingersReturnBinarizedImage (true);
			}
		}
	}
}
