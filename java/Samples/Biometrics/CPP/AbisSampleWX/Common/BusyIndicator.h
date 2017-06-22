#ifndef BUSY_INDICATOR_H_INCLUDED
#define BUSY_INDICATOR_H_INCLUDED

namespace Neurotec { namespace Samples
{

	class BusyIndicator : public wxPanel
	{
	public:
		BusyIndicator(wxWindow *parent,
			wxWindowID id,
			const wxPoint& pos = wxDefaultPosition,
			const wxSize& size = wxDefaultSize,
			long style = wxTAB_TRAVERSAL | wxNO_BORDER,
			const wxString& name = wxControlNameStr);

		~BusyIndicator();

		void OnPaint(wxPaintEvent& event);

	private:
		wxRect GetRectangle(double angle);
		void OnNotified();

		class Timer : public wxTimer
		{
		public:
			Timer(BusyIndicator * pOwner);
			virtual void Notify();

		private:
			BusyIndicator * m_owner;
		};

	private:
		Timer timer;
		int currentAngle;

		DECLARE_EVENT_TABLE();
	};

}}

#endif
