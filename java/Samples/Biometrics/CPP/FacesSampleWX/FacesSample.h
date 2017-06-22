#ifndef FACES_SAMPLE_H_INCLUDED
#define FACES_SAMPLE_H_INCLUDED

namespace Neurotec { namespace Samples
{

class FacesSampleApp : public wxApp
{
public:
	bool OnInit();
	virtual int OnExit();

	static wxString GetUserDir();
};

}}

#endif
