#ifndef IMAGE_VIEW_H_INCLUDED
#define IMAGE_VIEW_H_INCLUDED

namespace Neurotec { namespace Samples
{

class ImageView : public Neurotec::Gui::wxNView
{
public:
	ImageView(wxWindow *parent, wxWindowID winid = wxID_ANY);

	~ImageView();

	void SetImage(::Neurotec::Images::NImage image);

	virtual void OnDraw(wxGraphicsContext *gc);

private:
	Neurotec::Images::NImage m_image;
	wxImage m_bitmap;
};

}}

#endif
