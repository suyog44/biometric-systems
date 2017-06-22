Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Windows.Forms

Partial Public Class EditPhrasesForm
	Inherits Form
	#Region "Private fields"

	Private _phrases As List(Of Phrase)

	#End Region

	#Region "Public constructor"

	Public Sub New()
		InitializeComponent()
	End Sub

	#End Region

	#Region "Public properties"

	Public Property Phrases() As List(Of Phrase)
		Get
			Return _phrases
		End Get
		Set(ByVal value As List(Of Phrase))
			_phrases = value
			ListAllPhrases()
		End Set
	End Property

	#End Region

	#Region "Private form events"

	Private Sub BtnAddClick(ByVal sender As Object, ByVal e As EventArgs) Handles btnAdd.Click
		Dim phraseId As Integer

		If String.IsNullOrEmpty(tbPhraseId.Text) OrElse String.IsNullOrEmpty(tbPhrase.Text) Then
			Utilities.ShowInformation("One or more fields is empty. Please fill all of the fields.")
			Return
		End If

		If (Not Integer.TryParse(tbPhraseId.Text, phraseId)) Then
			Utilities.ShowError("Phrase Id should be integer (above 0)!")
			Return
		End If

		If phraseId <= 0 Then
			Utilities.ShowError("Phrase Id should be above 0!")
			Return
		End If

		If tbPhrase.Text.Contains("=") OrElse tbPhrase.Text.Contains(";") Then
			Utilities.ShowError("Phrase must not countain symbols: '=', ';'!")
			Return
		End If

		Dim phraseToAdd As New Phrase(phraseId, tbPhrase.Text)
		For Each phrase As Phrase In _phrases
			If phrase.Id = phraseToAdd.Id Then
				Utilities.ShowError("Another phrase with the same phrase id already exist in the list!")
				Return
			End If
		Next phrase

		_phrases.Add(phraseToAdd)
		ListAllPhrases()
	End Sub

	Private Sub BtnRemoveClick(ByVal sender As Object, ByVal e As EventArgs) Handles btnRemove.Click
		If lvPhrases.SelectedItems.Count = 1 Then
			Dim toRemove As ListViewItem = lvPhrases.SelectedItems(0)
			Dim phrase As Phrase = TryCast(toRemove.Tag, Phrase)
			If phrase IsNot Nothing Then
				_phrases.Remove(phrase)
			End If
			lvPhrases.Items.Remove(toRemove)
		Else
			Utilities.ShowInformation("No or more than one phrases are selected!")
		End If
	End Sub

	Private Sub BtnCloseClick(ByVal sender As Object, ByVal e As EventArgs) Handles btnClose.Click
		Close()
	End Sub

	#End Region

	#Region "Private methods"

	Private Sub ListAllPhrases()
		lvPhrases.Items.Clear()
		For Each phrase As Phrase In _phrases
			Dim item As New ListViewItem(New String() { phrase.Id.ToString(), phrase.String })
			item.Tag = phrase
			lvPhrases.Items.Add(item)
		Next phrase
		If lvPhrases.Items.Count > 0 Then
			lvPhrases.SelectedIndices.Add(0)
		End If
	End Sub

	#End Region
End Class
