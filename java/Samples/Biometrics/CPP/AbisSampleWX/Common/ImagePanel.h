#ifndef IMAGE_PANEL_H_INCLUDED
#define IMAGE_PANEL_H_INCLUDED

namespace Neurotec { namespace Samples
{

class ImagePanel : public wxPanel
{
public:
	ImagePanel(wxWindow *parent, wxBitmap bitmap, wxWindowID id, const wxPoint& pos = wxDefaultPosition,
		const wxSize& size = wxDefaultSize, long style = wxTAB_TRAVERSAL | wxNO_BORDER);

	void OnPaint(wxPaintEvent& event);

private:
	wxBitmap image;

	DECLARE_EVENT_TABLE();
};

}}

#endif
