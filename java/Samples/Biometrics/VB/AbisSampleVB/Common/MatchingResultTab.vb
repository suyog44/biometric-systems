Imports Microsoft.VisualBasic
Imports System
Imports System.Drawing
Imports System.Linq
Imports System.Windows.Forms
Imports Neurotec.Biometrics
Imports Neurotec.Biometrics.Client
Imports Neurotec.Biometrics.Gui
Imports Neurotec.Images
Imports Neurotec.IO

Partial Public Class MatchingResultTab
	Inherits Neurotec.Samples.TabPageContentBase
	#Region "Private types"

	Private Class MatchedFinger
		Inherits MatchedPair(Of NFrictionRidge, NFMatchingDetails)
		Public Sub New(ByVal f1 As NFrictionRidge, ByVal f2 As NFrictionRidge, ByVal dt As NFMatchingDetails)
			MyBase.New(f1, f2, dt)
		End Sub

		Public Overrides Function ToString() As String
			Return String.Format("Probe finger({0}) matched with gallery finger({1}). Score = {2}", First.Position, Second.Position, Details.Score)
		End Function
	End Class

	Private Class MatchedIris
		Inherits MatchedPair(Of NIris, NEMatchingDetails)
		Public Sub New(ByVal i1 As NIris, ByVal i2 As NIris, ByVal dt As NEMatchingDetails)
			MyBase.New(i1, i2, dt)
		End Sub

		Public Overrides Function ToString() As String
			Return String.Format("Probe iris({0}) matched with gallery iris({1}). Score = {2}", First.Position, Second.Position, Details.Score)
		End Function
	End Class

	Private Class MatchedFace
		Inherits MatchedPair(Of NFace, NLMatchingDetails)
		Public Sub New(ByVal f1 As NFace, ByVal f2 As NFace, ByVal dt As NLMatchingDetails)
			MyBase.New(f1, f2, dt)
		End Sub

		Public Overrides Function ToString() As String
			Return String.Format("Matched faces. Score = {0}", Details.Score)
		End Function
	End Class

	Private Class MatchedVoice
		Inherits MatchedPair(Of NVoice, NSMatchingDetails)
		Public Sub New(ByVal v1 As NVoice, ByVal v2 As NVoice, ByVal dt As NSMatchingDetails)
			MyBase.New(v1, v2, dt)
		End Sub

		Public Overrides Function ToString() As String
			Return String.Format("Matched probe voice(PhraseId={0}) with gallery voice(PhraseId={1}). Score={2}", First.PhraseId, Second.PhraseId, Details.Score)
		End Function
	End Class

	Private Class MatchedPair(Of T1, T2)
		Public Sub New(ByVal first As T1, ByVal second As T1, ByVal details As T2)
			Me.First = first
			Me.Second = second
			Me.Details = details
		End Sub

		Private privateFirst As T1
		Public Property First() As T1
			Get
				Return privateFirst
			End Get
			Set(ByVal value As T1)
				privateFirst = value
			End Set
		End Property
		Private privateSecond As T1
		Public Property Second() As T1
			Get
				Return privateSecond
			End Get
			Set(ByVal value As T1)
				privateSecond = value
			End Set
		End Property
		Private privateDetails As T2
		Public Property Details() As T2
			Get
				Return privateDetails
			End Get
			Set(ByVal value As T2)
				privateDetails = value
			End Set
		End Property
	End Class

	#End Region

	#Region "Public constructor"

	Public Sub New()
		InitializeComponent()
	End Sub

	#End Region

	#Region "Private fields"

	Private _biometricClient As NBiometricClient
	Private _probeSubject As NSubject
	Private _galerySubject As NSubject
	Private _matchingResult As NMatchingResult
	Private _mediaPlayer1 As MediaPlayerControl
	Private _mediaPlayer2 As MediaPlayerControl

	#End Region

	#Region "Public methods"

	Public Overrides Sub SetParams(ParamArray ByVal parameters() As Object)
		If parameters Is Nothing OrElse parameters.Length <> 2 Then
			Throw New ArgumentException("parameters")
		End If

		_probeSubject = CType(parameters(0), NSubject)
		_galerySubject = CType(parameters(1), NSubject)
		_matchingResult = _probeSubject.MatchingResults.First(Function(x) x.Id = _galerySubject.Id)
		_biometricClient = TabController.Client

		TabName = String.Format("Matching result: {0}", _galerySubject.Id)
		If TabName.Length > 30 Then
			TabName = TabName.Substring(0, 30) & "..."
		End If

		MyBase.SetParams(parameters)
	End Sub

	Public Overrides Sub OnTabAdded()
		lblInfo.Text = String.Format("Subject: '{0}'{1}Score: {2}", _galerySubject.Id, Environment.NewLine, _matchingResult.Score)
		_biometricClient.BeginGet(_galerySubject, AddressOf OnGetCompleted, Nothing)

		MyBase.OnTabAdded()
	End Sub

	Public Overrides Sub OnTabLeave()
		StopMediaPlayers()

		MyBase.OnTabLeave()
	End Sub

	#End Region

	#Region "Private methods"

	Private Function RecordIndexToFaceIndex(ByVal index As Integer, ByVal recordCouns As Integer()) As Integer
		If recordCouns IsNot Nothing Then
			Dim sum As Integer = 0
			Dim faceIndex As Integer = 0
			For Each item In recordCouns
				If index >= sum AndAlso index < sum + item Then
					Return faceIndex
				End If
				sum = sum + item
				faceIndex = faceIndex + 1
			Next item
		End If
		Return 0
	End Function

	Private Sub OnGetCompleted(ByVal result As IAsyncResult)
		If InvokeRequired Then
			BeginInvoke(New AsyncCallback(AddressOf OnGetCompleted), result)
		Else
			Try
				If _biometricClient.EndGet(result) = NBiometricStatus.Ok Then
					Dim schema As SampleDbSchema = SettingsManager.CurrentSchema
					Dim hasSchema = Not schema.IsEmpty
					Dim thumbnailName = schema.ThumbnailDataName
					Dim hasThumbnail As Boolean = hasSchema AndAlso Not String.IsNullOrEmpty(thumbnailName) AndAlso _galerySubject.Properties.ContainsKey(thumbnailName)
					If hasThumbnail Then
						Using buffer As NBuffer = _galerySubject.GetProperty(Of NBuffer)(thumbnailName)
							Using image As NImage = NImage.FromMemory(buffer)
								pbThumbnail.Image = image.ToBitmap()
							End Using
						End Using
					Else
						pbThumbnail.Visible = False
					End If

					Dim galeryRecordCounts As Integer() = Nothing
					If hasSchema Then
						Dim bag As New NPropertyBag()
						_galerySubject.CaptureProperties(bag)

						If (Not String.IsNullOrEmpty(schema.EnrollDataName) AndAlso bag.ContainsKey(schema.EnrollDataName)) Then
							Dim templateBuffer As NBuffer = _galerySubject.GetTemplateBuffer()
							Dim enrollData As NBuffer = CType(bag(schema.EnrollDataName), NBuffer)
							_galerySubject = EnrollDataSerializer.Deserialize(templateBuffer, enrollData, galeryRecordCounts)
						End If
						If Not String.IsNullOrEmpty(schema.GenderDataName) AndAlso bag.ContainsKey(schema.GenderDataName) Then
							Dim genderString As String = CStr(bag(schema.GenderDataName))
							bag(schema.GenderDataName) = System.Enum.Parse(GetType(NGender), genderString)
						End If
						propertyGrid.SelectedObject = New SchemaPropertyGridAdapter(schema, bag) With {.IsReadOnly = True}
					Else
						propertyGrid.Visible = False
					End If

					Dim hasDetails As Boolean = _matchingResult.MatchingDetails IsNot Nothing
					If hasDetails Then
						lblStatus.Visible = False
						Dim threshold As Integer = _biometricClient.MatchingThreshold

						Dim details = _matchingResult.MatchingDetails

						Dim flattenedFingers = _probeSubject.Fingers.ToArray().Where(Function(x) x.Objects.Count = 1).ToArray()
						For index As Integer = 0 To details.Fingers.Count - 1
							Dim item As NFMatchingDetails = details.Fingers(index)
							If item.MatchedIndex <> -1 AndAlso item.Score >= threshold Then
								cbMatched.Items.Add(New MatchedFinger(flattenedFingers(index), _galerySubject.Fingers(item.MatchedIndex), item))
							End If
						Next

						Dim faces = _probeSubject.Faces.ToArray().ToArray()
						Dim recordCounts = faces.Select(Function(x) x.Objects.First().Template.Records.Count).ToArray()
						For index As Integer = 0 To details.Faces.Count - 1
							Dim item As NLMatchingDetails = details.Faces(index)
							If item.MatchedIndex <> -1 AndAlso item.Score >= threshold Then
								cbMatched.Items.Add(New MatchedFace(faces(RecordIndexToFaceIndex(index, recordCounts)), _galerySubject.Faces(RecordIndexToFaceIndex(item.MatchedIndex, galeryRecordCounts)), item))
							End If
						Next

						Dim flattenedIrises = _probeSubject.Irises.ToArray().Where(Function(x) x.Objects.Count = 1).ToArray()
						For index As Integer = 0 To details.Irises.Count - 1
							Dim item As NEMatchingDetails = details.Irises(index)
							If item.MatchedIndex <> -1 AndAlso item.Score >= threshold Then
								cbMatched.Items.Add(New MatchedIris(flattenedIrises(index), _galerySubject.Irises(item.MatchedIndex), item))
							End If
						Next

						Dim palms = _probeSubject.Palms.ToArray().ToArray()
						For index As Integer = 0 To details.Palms.Count - 1
							Dim item As NFMatchingDetails = details.Palms(index)
							If item.MatchedIndex <> -1 AndAlso item.Score >= threshold Then
								cbMatched.Items.Add(New MatchedFinger(palms(index), _galerySubject.Palms(item.MatchedIndex), item))
							End If
						Next

						Dim flattenedVoices = _probeSubject.Voices.ToArray().Where(Function(x) x.Objects.Count = 1 AndAlso x.Objects.First().Child Is Nothing).ToArray()
						For index As Integer = 0 To details.Voices.Count - 1
							Dim item As NSMatchingDetails = details.Voices(index)
							If item.MatchedIndex <> -1 AndAlso item.Score >= threshold Then
								cbMatched.Items.Add(New MatchedVoice(flattenedVoices(index), _galerySubject.Voices(item.MatchedIndex), item))
							End If
						Next
						cbMatched.SelectedIndex = 0
					Else
						lblStatus.Text = "Enable 'Return matching details' in settings to see more details in this tab"
						lblProbeInfo.Visible = False
						lblGaleryInfo.Visible = False
						tlpSelection.Visible = False
						panelGaleryView.Visible = False
						panelProbeView.Visible = False
						lblProbeSubject.Visible = False
						lblGalerySubject.Visible = False
					End If
				End If
			Catch ex As Exception
			lblStatus.Visible = True
			lblStatus.Text = String.Format("Failed to get subject: {0}", ex.Message)
			lblStatus.BackColor = Color.Red
		End Try
		End If
	End Sub

	Private Function ShowFinger(ByVal target As NFrictionRidge, ByVal panelView As Panel, ByVal lblInfo As Label) As NFingerView
		Dim view As New NFingerView() With {.Dock = DockStyle.Fill, .AutoScroll = True, .Finger = target, .ShowTree = True}

		panelView.Controls.Clear()
		panelView.Controls.Add(view)

		Dim attributes As NFAttributes = target.Objects.ToArray().FirstOrDefault()
		If attributes IsNot Nothing Then
			lblInfo.Text = String.Format("Position={0}, Quality={1}", target.Position, attributes.Quality)
		Else
			lblInfo.Text = "Position=" & target.Position
		End If

		Return view
	End Function

	Private Function ShowIris(ByVal target As NIris, ByVal panelView As Panel, ByVal lblInfo As Label) As NIrisView
		Dim view As New NIrisView() With {.Dock = DockStyle.Fill, .AutoScroll = True, .Iris = target}

		panelView.Controls.Clear()
		panelView.Controls.Add(view)

		Dim attributes As NEAttributes = target.Objects.ToArray().FirstOrDefault()
		If attributes IsNot Nothing Then
			lblInfo.Text = String.Format("Position={0}, Quality={1}", target.Position, attributes.Quality)
		Else
			lblInfo.Text = "Position=" & target.Position
		End If

		Return view
	End Function

	Private Function ShowFace(ByVal target As NFace, ByVal panelView As Panel, ByVal lblInfo As Label) As NFaceView
		Dim view As New NFaceView() With {.Dock = DockStyle.Fill, .AutoScroll = True, .Face = target}

		panelView.Controls.Clear()
		panelView.Controls.Add(view)

		Dim attributes As NLAttributes = target.Objects.ToArray().FirstOrDefault()
		If attributes IsNot Nothing Then
			lblInfo.Text = String.Format("Quality={0}", attributes.Quality)
		Else
			lblInfo.Text = String.Empty
		End If

		Return view
	End Function

	Private Function ShowVoice(ByVal target As NVoice, ByVal panelView As Panel, ByVal lblInfo As Label) As MediaPlayerControl
		Dim tlp As New TableLayoutPanel() With {.Dock = DockStyle.Fill}
		tlp.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 100))
		tlp.RowStyles.Add(New RowStyle(SizeType.AutoSize, 0))
		tlp.RowStyles.Add(New RowStyle(SizeType.Percent, 100))

		panelView.Controls.Clear()
		panelView.Controls.Add(tlp)

		Dim view As New NVoiceView() With {.Dock = DockStyle.Fill, .Voice = target, .Size = New Size(100, 55)}

		Dim player As New MediaPlayerControl() With {.Dock = DockStyle.Fill}
		If target.SoundBuffer IsNot Nothing Then
			player.SoundBuffer = target.SoundBuffer.Save()
		End If

		tlp.Controls.Add(view, 0, 0)
		tlp.Controls.Add(player, 0, 1)

		Dim attributes As NSAttributes = target.Objects.ToArray().FirstOrDefault()
		If attributes IsNot Nothing Then
			lblInfo.Text = String.Format("Quality={0}", attributes.Quality)
		Else
			lblInfo.Text = String.Empty
		End If

		Return player
	End Function

	Dim view1 As NFingerView
	Dim view2 As NFingerView

	Private Sub OnFingerView1SelectedTreeMinutiaIndexChanged(ByVal sender As Object, ByVal a As EventArgs)
		Dim args = TryCast(a, TreeMinutiaEventArgs)
		If args IsNot Nothing Then
			view2.SelectedMinutiaIndex = args.Index
		End If
	End Sub

	Private Sub OnFingerView2SelectedTreeMinutiaIndexChanged(ByVal sender As Object, ByVal a As EventArgs)
		Dim args = TryCast(a, TreeMinutiaEventArgs)
		If args IsNot Nothing Then
			view1.SelectedMinutiaIndex = args.Index
		End If
	End Sub

	Private Sub CbMatchedSelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cbMatched.SelectedIndexChanged
		Dim selected = cbMatched.SelectedItem
		panelGaleryView.Controls.Clear()
		panelProbeView.Controls.Clear()
		lblGaleryInfo.Text = String.Empty
		lblProbeInfo.Text = String.Empty
		If selected IsNot Nothing Then
			StopMediaPlayers()
			If TypeOf selected Is MatchedFinger Then
				Dim mf As MatchedFinger = CType(selected, MatchedFinger)
				view1 = ShowFinger(mf.First, panelProbeView, lblProbeInfo)
				view2 = ShowFinger(mf.Second, panelGaleryView, lblGaleryInfo)

				Dim matchedPairs As NIndexPair() = mf.Details.GetMatedMinutiae()
				view1.MatedMinutiaIndex = 0
				view1.MatedMinutiae = matchedPairs
				view2.MatedMinutiaIndex = 1
				view2.MatedMinutiae = matchedPairs
				view1.PrepareTree()
				view2.Tree = view1.Tree

				AddHandler view1.SelectedTreeMinutiaIndexChanged, AddressOf OnFingerView1SelectedTreeMinutiaIndexChanged
				AddHandler view2.SelectedTreeMinutiaIndexChanged, AddressOf OnFingerView2SelectedTreeMinutiaIndexChanged
			ElseIf TypeOf selected Is MatchedFace Then
				Dim mf As MatchedFace = CType(selected, MatchedFace)
				ShowFace(mf.First, panelProbeView, lblProbeInfo)
				ShowFace(mf.Second, panelGaleryView, lblGaleryInfo)
			ElseIf TypeOf selected Is MatchedIris Then
				Dim mi As MatchedIris = CType(selected, MatchedIris)
				ShowIris(mi.First, panelProbeView, lblProbeInfo)
				ShowIris(mi.Second, panelGaleryView, lblGaleryInfo)
			ElseIf TypeOf selected Is MatchedVoice Then
				Dim mv As MatchedVoice = CType(selected, MatchedVoice)
				_mediaPlayer1 = (ShowVoice(mv.First, panelProbeView, lblProbeInfo))
				_mediaPlayer2 = ShowVoice(mv.Second, panelGaleryView, lblGaleryInfo)
			End If
		End If
	End Sub

	Private Sub StopMediaPlayers()
		If _mediaPlayer1 IsNot Nothing Then
			_mediaPlayer1.Stop()
		End If
		If _mediaPlayer2 IsNot Nothing Then
			_mediaPlayer2.Stop()
		End If
	End Sub

#End Region
End Class
