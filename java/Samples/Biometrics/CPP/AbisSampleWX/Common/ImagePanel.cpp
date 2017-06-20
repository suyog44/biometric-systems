#include "Precompiled.h"

#include <Common/ImagePanel.h>

namespace Neurotec { namespace Samples
{

BEGIN_EVENT_TABLE(ImagePanel, wxPanel)
	EVT_PAINT(ImagePanel::OnPaint)
END_EVENT_TABLE()

ImagePanel::ImagePanel(wxWindow *parent, wxBitmap bitmap, wxWindowID id, const wxPoint& pos, const wxSize& size, long style)
	: wxPanel(parent, id, pos, size, style),
	image(bitmap)
{
}

void ImagePanel::OnPaint(wxPaintEvent & /*event*/)
{
	wxPaintDC g(this);
	g.DrawBitmap(image, 0, 0, true);
}

}}
