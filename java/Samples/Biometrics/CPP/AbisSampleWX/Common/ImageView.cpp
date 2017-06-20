#include "Precompiled.h"

#include <Common/ImageView.h>

using namespace Neurotec::Images;

namespace Neurotec { namespace Samples
{

ImageView::ImageView(wxWindow *parent, wxWindowID winid) :
	wxNView(parent, winid),
	m_image(NULL)
{
}

ImageView::~ImageView()
{
}

void ImageView::SetImage(NImage image)
{
	m_image = image;
	if (!image.IsNull())
	{
		wxImage image = m_image.ToBitmap();

		m_bitmap = wxImage(image);
		SetViewSize(m_bitmap.GetWidth(), m_bitmap.GetHeight());
	}
	else
	{
		Clear();
	}

	Refresh(true, NULL);
}

void ImageView::OnDraw(wxGraphicsContext *gc)
{
	if (m_bitmap.GetRefData())
	{
		gc->DrawBitmap(m_bitmap, 0, 0, m_bitmap.GetWidth(), m_bitmap.GetHeight());
	}
}

}}

