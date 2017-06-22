#ifndef GENERALIZE_PROGRESS_VIEW_H_INCLUDED
#define GENERALIZE_PROGRESS_VIEW_H_INCLUDED

#include <Common/IcaoWarningsView.h>

namespace Neurotec { namespace Samples
{

wxDECLARE_EVENT(wxEVT_GEN_SELECTED_ITEM_CHANGED, wxCommandEvent);

class GeneralizeProgressView : public wxWindow
{
private:
	class ItemStatus
	{
	public:
		ItemStatus();
		bool HitTest(wxPoint p);
	public:
		wxString text;
		bool fill;
		wxColor color;
		::Neurotec::Biometrics::NBiometric biometric;
		wxRect hitBox;
		bool selected;
	};
public:
	GeneralizeProgressView(wxWindow * parent, int winid = wxID_ANY);
	~GeneralizeProgressView();

	void Clear();
	void SetView( ::Neurotec::Gui::wxNView * view);
	void SetIcaoView( ::Neurotec::Samples::IcaoWarningsView * view);
	void SetEnableMouseSelection(bool value);

	template <typename T>
	void SetBiometrics(::Neurotec::NArrayWrapper<T> & value)
	{
		for (std::vector< ::Neurotec::Biometrics::NBiometric>::iterator it = m_biometrics.begin(); it != m_biometrics.end(); it++)
		{
			it->RemovePropertyChangedCallback(&GeneralizeProgressView::OnPropertyChangedCallback, this);
		}

		m_biometrics.clear();
		for (typename ::Neurotec::NArrayWrapper<T>::iterator it = value.begin(); it != value.end(); it++)
		{
			m_biometrics.push_back(*it);
			it->AddPropertyChangedCallback(&GeneralizeProgressView::OnPropertyChangedCallback, this);
		}
		OnDataChanged();
	}

	template <typename T>
	void SetBiometrics( std::vector<T> & value)
	{
		for (std::vector< ::Neurotec::Biometrics::NBiometric>::iterator it = m_biometrics.begin(); it != m_biometrics.end(); it++)
		{
			it->RemovePropertyChangedCallback(&GeneralizeProgressView::OnPropertyChangedCallback, this);
		}

		m_biometrics.clear();
		for (typename std::vector<T>::iterator it = value.begin(); it != value.end(); it++)
		{
			m_biometrics.push_back(*it);
			it->AddPropertyChangedCallback(&GeneralizeProgressView::OnPropertyChangedCallback, this);
		}
		OnDataChanged();
	}

	template <typename T>
	void SetGeneralized( std::vector<T> & value)
	{
		for (std::vector< ::Neurotec::Biometrics::NBiometric>::iterator it = m_generalized.begin(); it != m_generalized.end(); it++)
		{
			it->RemovePropertyChangedCallback(&GeneralizeProgressView::OnPropertyChangedCallback, this);
		}

		m_generalized.clear();
		for (typename std::vector<T>::iterator it = value.begin(); it != value.end(); it++)
		{
			m_generalized.push_back(*it);
			it->AddPropertyChangedCallback(&GeneralizeProgressView::OnPropertyChangedCallback, this);
		}
		OnDataChanged();
	}

	template <typename T>
	void SetGeneralized(T & value)
	{
		std::vector<T> vec;
		if (!value.IsNull())
			vec.push_back(value);
		SetGeneralized(vec);
	}

	template <typename T>
	void SetSelected(T & value)
	{
		bool newValue = !NObject::Equals(m_selected, value);
		m_selected = value;
		SetBiometricToView();
		for (std::vector<ItemStatus>::iterator it = m_drawings.begin(); it != m_drawings.end(); it++)
		{
			it->selected = value == it->biometric;
		}
		Refresh();

		if (newValue)
		{
			wxCommandEvent evt(wxEVT_GEN_SELECTED_ITEM_CHANGED, GetId());
			wxPostEvent(this, evt);
		}
	}

	::Neurotec::Biometrics::NBiometric GetSelected() { return m_selected; };
private:
	void OnPaint(wxPaintEvent & event);
	void OnDraw(wxGraphicsContext *gc);
	void OnDataChanged();
	void SetBiometricToView();
	void UpdateBiometricsStatus();
	static void OnPropertyChangedCallback(::Neurotec::NObject::PropertyChangedEventArgs args);
	void OnPropertyChanged(wxCommandEvent & event);
	void OnMouseMove(wxMouseEvent & event);
	void OnMouseDown(wxMouseEvent & event);

private:
	::Neurotec::Gui::wxNView * m_view;
	::Neurotec::Samples::IcaoWarningsView * m_icaoView;
	std::vector< ::Neurotec::Biometrics::NBiometric> m_biometrics;
	std::vector< ::Neurotec::Biometrics::NBiometric> m_generalized;
	::Neurotec::Biometrics::NBiometric m_selected;
	std::vector<ItemStatus> m_drawings;
	bool m_enableMouseSelection;

private:
	DECLARE_EVENT_TABLE();
};

}}

#endif
