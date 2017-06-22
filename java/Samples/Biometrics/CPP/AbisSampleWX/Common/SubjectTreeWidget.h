#ifndef SUBJECT_TREE_WIDGET_H_INCLUDED
#define SUBJECT_TREE_WIDGET_H_INCLUDED

#include <Common/SubjectUtils.h>

namespace Neurotec { namespace Samples
{

wxDECLARE_EVENT(wxEVT_TREE_SELECTED_ITEM_CHANGED, wxCommandEvent);

class SubjectTreeWidget;
class Node : public wxTreeItemData
{
public:
	~Node();
public:
	bool IsNewNode() const { return m_isNewNode; }
	bool IsSubjectNode() const { return m_isSubjectNode; }
	bool IsBiometricNode() const { return m_isBiometricNode; }
	bool IsGeneralizedNode() const { return m_isGeneralizedNode; }
	::Neurotec::Biometrics::NBiometricType const GetBiometricType() { return m_biometricType; }
	std::vector< ::Neurotec::Biometrics::NBiometric> GetAllItems();
	std::vector< ::Neurotec::Biometrics::NBiometric> GetItems();
	std::vector< ::Neurotec::Biometrics::NBiometric> GetGeneralizedItems();
	std::vector< ::Neurotec::Biometrics::NBiometric> GetAllGeneralized();

	Node * GetParent();
	std::vector<Node*> GetChildren();

protected:
	wxTreeCtrl * m_treeCtrl;
	bool m_isNewNode;
	bool m_isSubjectNode;
	bool m_isBiometricNode;
	bool m_isGeneralizedNode;
	std::vector< ::Neurotec::Biometrics::NBiometric> m_items;

	::Neurotec::Biometrics::NBiometricType m_biometricType;
	::Neurotec::NInt m_sessionId;
	::Neurotec::Biometrics::NFPosition m_position;
	::Neurotec::Biometrics::NFImpressionType m_impression;
protected:
	Node(wxTreeCtrl * treeCtrl, ::Neurotec::Biometrics::NBiometricType type);
	Node(wxTreeCtrl * treeCtrl, const ::Neurotec::Biometrics::NBiometric & biometric);

	bool HasBiometric(const ::Neurotec::Biometrics::NBiometric & biometric);
	bool BelongsToNode(const ::Neurotec::Biometrics::NBiometric & biometric);

	friend class SubjectTreeWidget;
};

class SubjectTreeWidget: public wxPanel
{
public:
	SubjectTreeWidget(wxWindow *parent, wxWindowID id);
	virtual ~SubjectTreeWidget();
private:
	void OnRemoveClick(wxCommandEvent& event);
	void CreateGuiElements();
	void RegisterGuiEvents();
	void UnregisterGuiEvents();

public:
	void SetSubject(const ::Neurotec::Biometrics::NSubject & subject);
	::Neurotec::Biometrics::NSubject GetSubject();
	void SetShownTypes(::Neurotec::Biometrics::NBiometricType value);
	::Neurotec::Biometrics::NBiometricType GetShownTypes();
	void SetAllowNew(::Neurotec::Biometrics::NBiometricType value);
	::Neurotec::Biometrics::NBiometricType GetAllowNew();
	void SetAllowRemove(bool value);
	bool GetAllowRemove() const;
	void SetShowBiometricsOnly(bool value);
	bool GetShowBiometricsOnly();
	void SetSelectedItem(Node * value);
	Node * GetSelectedItem();
public:
	void UpdateTree();
	Node * GetBiometricNode(::Neurotec::Biometrics::NBiometric & biometric);
	Node * GetNewNode(::Neurotec::Biometrics::NBiometricType biometricType);
	Node * GetSubjectNode();
private:
	void OnThread(wxCommandEvent &event);
	void SetCallbacks();
	void UnsetCallbacks();

	void OnBiometricAdded(const ::Neurotec::Biometrics::NBiometric & biometric);
	void OnBiometricRemoved(const ::Neurotec::Biometrics::NBiometric & biometric);
	void UpdateNodeText(Node * node);

	static void FingerCollectionChangedCallback(::Neurotec::Collections::CollectionChangedEventArgs< ::Neurotec::Biometrics::NFinger> args);
	static void FaceCollectionChangedCallback(::Neurotec::Collections::CollectionChangedEventArgs< ::Neurotec::Biometrics::NFace> args);
	static void IrisCollectionChangedCallback(::Neurotec::Collections::CollectionChangedEventArgs< ::Neurotec::Biometrics::NIris> args);
	static void PalmCollectionChangedCallback(::Neurotec::Collections::CollectionChangedEventArgs< ::Neurotec::Biometrics::NPalm> args);
	static void VoiceCollectionChangedCallback(::Neurotec::Collections::CollectionChangedEventArgs< ::Neurotec::Biometrics::NVoice> args);

	template <class T>
	static void BiometricCollectionChangedCallback(::Neurotec::Collections::CollectionChangedEventArgs<T> args);

private:
	void OnSubjectChanged();
	void OnSelectedItemChanging(wxTreeEvent & event);
	void OnSelectedItemChanged(wxTreeEvent & event);
	Node * GetNodeForBiometric(const ::Neurotec::Biometrics::NBiometric & biometric);
	Node * GetNodeForNonBiometric(::Neurotec::Biometrics::NBiometricType type);

	Node * GetNodeForBiometricInternal(const wxTreeItemId & id, const ::Neurotec::Biometrics::NBiometric & biometric);
	Node * GetNodeForNonBiometricInternal(const wxTreeItemId & id, ::Neurotec::Biometrics::NBiometricType type);
private:
	bool m_isInUpdate;
	bool m_showBiometricsOnly;
	::Neurotec::Biometrics::NBiometricType m_shownTypes;
	::Neurotec::Biometrics::NBiometricType m_allowNew;
	::Neurotec::Biometrics::NSubject m_subject;

	wxButton * m_btnRemove;
	wxTreeCtrl * m_treeCtrl;

	enum
	{
		ID_ADD_BIOMETRIC,
		ID_REMOVE_BIOMETRIC,
		ID_RESET
	};
};

}}

#endif
