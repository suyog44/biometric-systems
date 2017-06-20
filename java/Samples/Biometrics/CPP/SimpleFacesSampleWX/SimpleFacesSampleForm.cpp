#include "Precompiled.h"
#include "SimpleFacesSampleForm.h"
#include "SimpleFacesSampleWXVersionInfo.h"

#ifdef N_MAC_OSX_FRAMEWORKS
#include <NGui/Gui/Neurotechnology.xpm>
#else
#include <Gui/Neurotechnology.xpm>
#endif

using namespace Neurotec::Biometrics;
using namespace Neurotec::Gui;

namespace Neurotec
{
	namespace Samples
	{
		BEGIN_EVENT_TABLE(SimpleFacesSampleForm, wxFrame)
			EVT_NOTEBOOK_PAGE_CHANGED(wxID_ANY, SimpleFacesSampleForm::OnNbPageChange)
		END_EVENT_TABLE()

		SimpleFacesSampleForm::SimpleFacesSampleForm(wxWindow *parent, const wxWindowID id /*= 1*/, const wxString &title /*= wxEmptyString*/, const wxPoint &pos /*= wxDefaultPosition*/, const wxSize &size /*wxDefaultSize*/, long style /*= MainForm_STYLE*/)
			: wxFrame(parent, id, title, pos, size, style)
		{
			InitializeBiometricClient();
			CreateGUIControls();
		}

		SimpleFacesSampleForm::~SimpleFacesSampleForm()
		{
			if (!m_biometricClient.IsNull())
			{
				m_biometricClient.Cancel();
			}
		}

		void SimpleFacesSampleForm::InitializeBiometricClient()
		{
			m_biometricClient.SetUseDeviceManager(true);
			m_biometricClient.SetBiometricTypes(nbtFace);
			m_biometricClient.Initialize();
		}

		void SimpleFacesSampleForm::CreateGUIControls()
		{
			wxNotebook *nb = new wxNotebook(this, wxID_ANY, wxDefaultPosition, wxDefaultSize, wxNB_MULTILINE);

			nb->AddPage(new DetectFaces(nb, m_biometricClient), "Detect faces");
			nb->AddPage(new EnrollFromImage(nb, m_biometricClient), "Enroll from image");
			nb->AddPage(new EnrollFromCamera(nb, m_biometricClient), "Enroll from camera");
			nb->AddPage(new IdentifyFace(nb, m_biometricClient), "Identify face");
			nb->AddPage(new VerifyFace(nb, m_biometricClient), "Verify face");
			nb->AddPage(new MatchMultipleFaces(nb, m_biometricClient), "Match multiple faces");
			nb->AddPage(new CreateTokenFaceImage(nb, m_biometricClient), "Create token face image");
			nb->AddPage(new GeneralizeFaces(nb, m_biometricClient), "Generalize faces");
			nb->AddPage(new CaptureIcaoCompliantImage(nb, m_biometricClient), "Capture ICAO image");

			Layout();
			SetIcon(Neurotechnology_XPM);
			SetTitle(SIMPLE_FACES_SAMPLE_WX_TITLE);
			this->SetWindowStyleFlag(this->GetWindowStyleFlag() | wxFULL_REPAINT_ON_RESIZE);
			this->SetSize(1024, 768);
			this->Center();
		}

		void SimpleFacesSampleForm::OnNbPageChange(wxBookCtrlEvent& )
		{
			if (!m_biometricClient.IsNull())
			{
				m_biometricClient.Cancel();
				m_biometricClient.Reset();
			}
		}
	}
}
