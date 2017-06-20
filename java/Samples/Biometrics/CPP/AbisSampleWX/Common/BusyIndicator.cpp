#include "Precompiled.h"

#include <Common/BusyIndicator.h>
#include <cmath>

namespace Neurotec { namespace Samples
{

	const double pi = 3.1415926535897932384626433832795;
	const int radius = 100;
	const int circleRadius = 33;
	const int borderMargin = 4;
	const int center = borderMargin + circleRadius + radius;
	const float combinedSize = center * 2;

	BEGIN_EVENT_TABLE(BusyIndicator, wxPanel)
		EVT_PAINT(BusyIndicator::OnPaint)
	END_EVENT_TABLE()

	BusyIndicator::BusyIndicator(wxWindow *parent, wxWindowID id, const wxPoint& pos,
			const wxSize& size, long style, const wxString& name)
		: wxPanel(parent, id, pos, size, style, name),
		timer(this),
		currentAngle(0)
	{
		timer.Start(33);
	}

	BusyIndicator::~BusyIndicator()
	{
		timer.Stop();
	}

	void BusyIndicator::OnPaint(wxPaintEvent & /*event*/)
	{
		wxPaintDC g(this);

		wxSize sz = GetSize();
		float zw = sz.GetWidth() / combinedSize;
		float zh = sz.GetHeight() / combinedSize;
		float zoom = zw < zh ? zw : zh;

		double rad = currentAngle * pi / 180;
		float dx = sz.GetWidth() / 2;
		float dy = sz.GetHeight() / 2;

		wxAffineMatrix2D m = g.GetTransformMatrix();
		m.Scale(zoom, zoom);
		m.Rotate(rad);

		wxMatrix2D rotM;
		wxPoint2DDouble transM;
		m.Get(&rotM, &transM);
		transM.m_x = dx + dx * sin(rad) - dy * cos(rad);
		transM.m_y = dy - dx * cos(rad) - dy * sin(rad);
		m.Set(rotM, transM);

		g.SetTransformMatrix(m);
		g.SetPen(*wxTRANSPARENT_PEN);

		char value = 0;
		for (double angle = 0; angle < 2 * pi; angle += pi / 4)
		{
			value += 25;
			g.SetBrush(wxBrush(wxColor(value, value, value)));
			g.DrawEllipse(GetRectangle(angle));
		}
	}

	wxRect BusyIndicator::GetRectangle(double angle)
	{
		double x = center + radius * cos(angle);
		double y = center + radius * sin(angle);

		return wxRect(x - circleRadius, y - circleRadius, circleRadius * 2, circleRadius * 2);
	}

	void BusyIndicator::OnNotified()
	{
		currentAngle = (currentAngle + 10) % 360;
		Refresh();
	}

	BusyIndicator::Timer::Timer(BusyIndicator * pOwner)
		: m_owner(pOwner)
	{
	}

	void BusyIndicator::Timer::Notify()
	{
		m_owner->OnNotified();
	}

}}
