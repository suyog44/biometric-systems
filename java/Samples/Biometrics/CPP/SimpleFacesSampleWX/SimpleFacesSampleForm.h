#ifndef SIMPLE_FACES_SAMPLE_FORM_H_INCLUDED
#define SIMPLE_FACES_SAMPLE_FORM_H_INCLUDED

#include "DetectFaces.h"
#include "EnrollFromImage.h"
#include "EnrollFromCamera.h"
#include "IdentifyFace.h"
#include "VerifyFace.h"
#include "MatchMultipleFaces.h"
#include "CreateTokenFaceImage.h"
#include "GeneralizeFaces.h"
#include "CaptureIcaoCompliantImage.h"

namespace Neurotec
{
	namespace Samples
	{
		#define MainForm_STYLE wxCAPTION | wxRESIZE_BORDER | wxSYSTEM_MENU | wxMINIMIZE_BOX | wxMAXIMIZE_BOX | wxCLOSE_BOX
		class SimpleFacesSampleForm : public wxFrame
		{
		public:
			SimpleFacesSampleForm(wxWindow *parent, const wxWindowID id = 1, const wxString &title = wxEmptyString, const wxPoint &pos = wxDefaultPosition, const wxSize &size = wxDefaultSize, long style = MainForm_STYLE);
			~SimpleFacesSampleForm();

		private:
			Neurotec::Biometrics::Client::NBiometricClient m_biometricClient;

			void InitializeBiometricClient();
			void CreateGUIControls();
			void OnNbPageChange(wxBookCtrlEvent&);
			DECLARE_EVENT_TABLE();
		};
	}
}

#endif
