Imports System
Imports System.Collections
Imports System.Collections.Generic
Imports System.Collections.Specialized
Imports System.ComponentModel
Imports System.Drawing
Imports System.Linq
Imports System.Windows.Forms
Imports Neurotec.Biometrics

Partial Public Class SubjectTreeControl
	Inherits UserControl
	Implements INotifyPropertyChanged
#Region "Private types"

	Private Class NodeTag
#Region "Public constructors"

		Public Sub New(ByVal tag As Object)
			Items = New List(Of NBiometric)()
			ObjectTag = tag
			SessionId = -1
			If TypeOf tag Is NSubject.FingerCollection Then
				Type = NBiometricType.Finger
			ElseIf TypeOf tag Is NSubject.FaceCollection Then
				Type = NBiometricType.Face
			ElseIf TypeOf tag Is NSubject.IrisCollection Then
				Type = NBiometricType.Iris
			ElseIf TypeOf tag Is NSubject.PalmCollection Then
				Type = NBiometricType.Palm
			ElseIf TypeOf tag Is NSubject.VoiceCollection Then
				Type = NBiometricType.Voice
			End If
		End Sub

		Public Sub New(ByVal ParamArray biometrics() As NBiometric)
			Items = New List(Of NBiometric)(biometrics)
			Dim first = biometrics.First()
			Type = first.BiometricType
			SessionId = first.SessionId
			If Type = NBiometricType.Finger OrElse Type = NBiometricType.Palm Then
				Dim ridge As NFrictionRidge = CType(first, NFrictionRidge)
				Position = ridge.Position
				ImpressionType = ridge.ImpressionType
			End If
		End Sub

#End Region

#Region "Public properties"

		Private privateObjectTag As Object
		Public Property ObjectTag() As Object
			Get
				Return privateObjectTag
			End Get
			Set(ByVal value As Object)
				privateObjectTag = value
			End Set
		End Property
		Private privateItems As List(Of NBiometric)
		Public Property Items() As List(Of NBiometric)
			Get
				Return privateItems
			End Get
			Set(ByVal value As List(Of NBiometric))
				privateItems = value
			End Set
		End Property
		Private privateType As NBiometricType
		Public Property Type() As NBiometricType
			Get
				Return privateType
			End Get
			Set(ByVal value As NBiometricType)
				privateType = value
			End Set
		End Property
		Private privateSessionId As Integer
		Public Property SessionId() As Integer
			Get
				Return privateSessionId
			End Get
			Set(ByVal value As Integer)
				privateSessionId = value
			End Set
		End Property
		Private privatePosition As NFPosition
		Public Property Position() As NFPosition
			Get
				Return privatePosition
			End Get
			Set(ByVal value As NFPosition)
				privatePosition = value
			End Set
		End Property
		Private privateImpressionType As NFImpressionType
		Public Property ImpressionType() As NFImpressionType
			Get
				Return privateImpressionType
			End Get
			Set(ByVal value As NFImpressionType)
				privateImpressionType = value
			End Set
		End Property

#End Region

#Region "Public methods"

		Public Function HasTag(ByVal tag As Object) As Boolean
			Return Object.Equals(ObjectTag, tag) OrElse (Items IsNot Nothing AndAlso TypeOf tag Is NBiometric AndAlso Items.Contains(CType(tag, NBiometric)))
		End Function

		Public Function BelongsToNode(ByVal tag As Object) As Boolean
			Dim item As NBiometric = TryCast(tag, NBiometric)
			If item IsNot Nothing AndAlso item.BiometricType = Type Then
				If Type = NBiometricType.Finger OrElse Type = NBiometricType.Palm Then
					Dim frictionRidge As NFrictionRidge = CType(item, NFrictionRidge)
					If Position <> frictionRidge.Position OrElse ImpressionType <> frictionRidge.ImpressionType Then
						Return False
					End If
				End If
				If Type = NBiometricType.Face AndAlso Items.Any() Then
					If SessionId = item.SessionId Then
						If (item.ParentObject Is Nothing) Xor (Items.First().ParentObject Is Nothing) Then
							Return False
						End If
					End If
				End If
				If SessionId = -1 AndAlso item.SessionId = -1 Then
					Return Items.Contains(item)
				End If
				If SessionId = item.SessionId Then
					Return True
				End If
				If SessionId <> -1 AndAlso item.SessionId = -1 Then
					Dim parentObject As NBiometricAttributes = item.ParentObject
					If parentObject IsNot Nothing Then
						Dim parent As NBiometric = CType(parentObject.Owner, NBiometric)
						Return Items.Contains(parent)
					End If
				End If
			End If
			Return False
		End Function

#End Region
	End Class

#End Region

#Region "Public types"

	Public Class Node
#Region "Public properties"

		Private privateIsNewNode As Boolean
		Public Property IsNewNode() As Boolean
			Get
				Return privateIsNewNode
			End Get
			Friend Set(ByVal value As Boolean)
				privateIsNewNode = value
			End Set
		End Property
		Private privateIsSubjectNode As Boolean
		Public Property IsSubjectNode() As Boolean
			Get
				Return privateIsSubjectNode
			End Get
			Friend Set(ByVal value As Boolean)
				privateIsSubjectNode = value
			End Set
		End Property
		Private privateIsBiometricNode As Boolean
		Public Property IsBiometricNode() As Boolean
			Get
				Return privateIsBiometricNode
			End Get
			Friend Set(ByVal value As Boolean)
				privateIsBiometricNode = value
			End Set
		End Property
		Private privateIsGeneralizedNode As Boolean
		Public Property IsGeneralizedNode() As Boolean
			Get
				Return privateIsGeneralizedNode
			End Get
			Set(ByVal value As Boolean)
				privateIsGeneralizedNode = value
			End Set
		End Property

		Private privateBiometricType As NBiometricType
		Public Property BiometricType() As NBiometricType
			Get
				Return privateBiometricType
			End Get
			Private Set(ByVal value As NBiometricType)
				privateBiometricType = value
			End Set
		End Property
		Private privateAllItems As NBiometric()
		Public Property AllItems() As NBiometric()
			Get
				Return privateAllItems
			End Get
			Private Set(ByVal value As NBiometric())
				privateAllItems = value
			End Set
		End Property
		Private privateItems As NBiometric()
		Public Property Items() As NBiometric()
			Get
				Return privateItems
			End Get
			Private Set(ByVal value As NBiometric())
				privateItems = value
			End Set
		End Property
		Private privateGeneralizedItems As NBiometric()
		Public Property GeneralizedItems() As NBiometric()
			Get
				Return privateGeneralizedItems
			End Get
			Private Set(ByVal value As NBiometric())
				privateGeneralizedItems = value
			End Set
		End Property

#End Region

#Region "Internal fields"
		Friend _node As TreeNode
#End Region

#Region "Internal constructor"
		Friend Sub New(ByVal node As TreeNode)
			Dim tag As NodeTag = TryCast(node.Tag, NodeTag)

			_node = node

			BiometricType = tag.Type
			IsSubjectNode = TypeOf tag.ObjectTag Is NSubject
			IsBiometricNode = tag.Items.Count > 0
			IsGeneralizedNode = tag.Items.Count > 1
			AllItems = tag.Items.ToArray()
			Items = tag.Items.Where(Function(x) (Not SubjectUtils.IsBiometricGeneralizationResult(x))).ToArray()
			GeneralizedItems = tag.Items.Where(Function(x) SubjectUtils.IsBiometricGeneralizationResult(x)).ToArray()
		End Sub
#End Region

#Region "Public methods"

		Public Function GetParent() As Node
			Dim parent As TreeNode = _node.Parent
			Return If(parent IsNot Nothing, New Node(parent), Nothing)
		End Function

		Public Function GetChildren() As Node()
			Return ( _
			  From n As TreeNode In _node.Nodes _
			  Select New Node(n)).ToArray()
		End Function

		Public Function GetAllGeneralized() As NBiometric()
			Dim result As New List(Of NBiometric)()
			result.AddRange(GeneralizedItems)
			For Each ch In GetChildren()
				result.AddRange(ch.GetAllGeneralized())
			Next ch
			Return result.Distinct().ToArray()
		End Function

		Public Overloads Overrides Function Equals(ByVal obj As Object) As Boolean
			Dim target As Node = CType(obj, Node)
			Return target IsNot Nothing AndAlso _node Is target._node
		End Function

		Public Overrides Function GetHashCode() As Integer
			Return _node.GetHashCode()
		End Function

#End Region
	End Class

#End Region

#Region "Public constructor"
	Public Sub New()
		InitializeComponent()
		DoubleBuffered = True
		AddHandler EnabledChanged, AddressOf SubjectTreeControlEnabledChanged
	End Sub
#End Region

#Region "Private fields"

	Private _showBiometricsOnly As Boolean = False
	Private _allowRemove As Boolean = True
	Private _subject As NSubject
	Private _shownTypes As NBiometricType = NBiometricType.Finger Or NBiometricType.Face Or NBiometricType.Iris Or NBiometricType.Voice Or NBiometricType.Palm
	Private _allowNew As NBiometricType = NBiometricType.Finger Or NBiometricType.Face Or NBiometricType.Iris Or NBiometricType.Voice Or NBiometricType.Palm

#End Region

#Region "Public properties"

	Public Property Subject() As NSubject
		Get
			Return _subject
		End Get
		Set(ByVal value As NSubject)
			If _subject IsNot value Then
				If _subject IsNot Nothing Then
					RemoveHandler _subject.Fingers.CollectionChanged, AddressOf OnSubjectCollectionChanged
					RemoveHandler _subject.Faces.CollectionChanged, AddressOf OnSubjectCollectionChanged
					RemoveHandler _subject.Palms.CollectionChanged, AddressOf OnSubjectCollectionChanged
					RemoveHandler _subject.Irises.CollectionChanged, AddressOf OnSubjectCollectionChanged
					RemoveHandler _subject.Voices.CollectionChanged, AddressOf OnSubjectCollectionChanged
				End If
				_subject = value
				If _subject IsNot Nothing Then
					AddHandler _subject.Fingers.CollectionChanged, AddressOf OnSubjectCollectionChanged
					AddHandler _subject.Faces.CollectionChanged, AddressOf OnSubjectCollectionChanged
					AddHandler _subject.Palms.CollectionChanged, AddressOf OnSubjectCollectionChanged
					AddHandler _subject.Irises.CollectionChanged, AddressOf OnSubjectCollectionChanged
					AddHandler _subject.Voices.CollectionChanged, AddressOf OnSubjectCollectionChanged
				End If
				OnSubjectChanged()
			End If
		End Set
	End Property

	Public Property ShownTypes() As NBiometricType
		Get
			Return _shownTypes
		End Get
		Set(ByVal value As NBiometricType)
			If _shownTypes <> value Then
				_shownTypes = value
				SelectedItem = Nothing
				OnSubjectChanged()
			End If
		End Set
	End Property

	Public Property AllowNew() As NBiometricType
		Get
			Return _allowNew
		End Get
		Set(ByVal value As NBiometricType)
			If _allowNew <> value Then
				_allowNew = value
				If _allowNew <> NBiometricType.None Then
					_showBiometricsOnly = False
				End If
				SelectedItem = Nothing
				OnSubjectChanged()
			End If
		End Set
	End Property

	Public Property ShowBiometricsOnly() As Boolean
		Get
			Return _showBiometricsOnly
		End Get
		Set(ByVal value As Boolean)
			If _showBiometricsOnly <> value Then
				_showBiometricsOnly = value
				SelectedItem = Nothing
				If _showBiometricsOnly Then
					_allowNew = NBiometricType.None
				End If
				OnSubjectChanged()
			End If
		End Set
	End Property

	Public Property SelectedItem() As Node
		Get
			Dim node As TreeNode = treeView.SelectedNode
			Return If(node IsNot Nothing, New Node(node), Nothing)
		End Get
		Set(ByVal value As Node)
			Dim node As TreeNode = treeView.SelectedNode
			Dim current As Node = If(node IsNot Nothing, New Node(node), Nothing)
			If current IsNot value Then
				If value Is Nothing Then
					Dim changed As Boolean = treeView.SelectedNode IsNot Nothing
					treeView.SelectedNode = Nothing
					If Enabled AndAlso changed AndAlso PropertyChangedEvent IsNot Nothing Then
						RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("SelectedItem"))
					End If
				Else
					treeView.SelectedNode = value._node
				End If
				tsbRemove.Enabled = current IsNot Nothing AndAlso current.IsBiometricNode
			End If
		End Set
	End Property

	Public Property AllowRemove() As Boolean
		Get
			Return _allowRemove
		End Get
		Set(ByVal value As Boolean)
			If value <> _allowRemove Then
				_allowRemove = value
				If value Then
					tableLayoutPanel.RowStyles(0).SizeType = SizeType.AutoSize
				Else
					tableLayoutPanel.RowStyles(0).SizeType = SizeType.Absolute
					tableLayoutPanel.RowStyles(0).Height = 0
				End If
			End If
		End Set
	End Property

	Public ReadOnly Property Nodes() As Node()
		Get
			Return ( _
			  From n As TreeNode In treeView.Nodes _
			  Select New Node(n)).ToArray()
		End Get
	End Property

#End Region

#Region "Private methods"

	Private Function GetNodeWithTag(ByVal tag As Object) As TreeNode
		For Each node As TreeNode In treeView.Nodes
			Dim t As NodeTag = TryCast(node.Tag, NodeTag)
			If t.HasTag(tag) OrElse t.BelongsToNode(tag) Then
				Return node
			End If

			Dim n As TreeNode = GetNodeWithTag(node, tag)
			If n IsNot Nothing Then
				Return n
			End If
		Next node
		Return Nothing
	End Function

	Private Function GetNodeWithTag(ByVal rootNode As TreeNode, ByVal tag As Object) As TreeNode
		For Each node As TreeNode In rootNode.Nodes
			Dim t As NodeTag = TryCast(node.Tag, NodeTag)
			If t.HasTag(tag) OrElse t.BelongsToNode(tag) Then
				Return node
			End If

			Dim n As TreeNode = GetNodeWithTag(node, tag)
			If n IsNot Nothing Then
				Return n
			End If
		Next node
		Return Nothing
	End Function

	Private Function CreateFingerNode(ByVal allFingers() As NFinger, ByVal ParamArray fingers() As NFinger) As TreeNode
		Dim tag As New NodeTag(fingers)
		Dim node As New TreeNode(String.Empty) With {.Tag = tag}
		Dim first As NFinger = fingers.First()
		For Each attribute As NFAttributes In first.Objects.ToArray()
			Dim child As NFinger = CType(attribute.Child, NFinger)
			If child IsNot Nothing AndAlso (Not fingers.Contains(child)) AndAlso (Not tag.BelongsToNode(child)) Then
				Dim grouped = SubjectUtils.GetFingersInSameGroup(allFingers, child).ToArray()
				node.Nodes.Add(CreateFingerNode(allFingers, grouped))
			End If
		Next attribute
		UpdateFingerNodeText(node)
		Return node
	End Function

	Private Sub UpdateFingerNodeText(ByVal node As TreeNode)
		Dim tag As NodeTag = TryCast(node.Tag, NodeTag)
		If tag IsNot Nothing Then
			Dim first As NFrictionRidge = TryCast(tag.Items.FirstOrDefault(), NFrictionRidge)
			If first IsNot Nothing Then
				Dim format As String = "{0}{1}"
				Dim rolled As String = String.Empty
				If NBiometricTypes.IsImpressionTypeRolled(first.ImpressionType) Then
					rolled = ", Rolled"
				End If
				Dim text As String = String.Format(format, first.Position, rolled)
				If tag.Items.Count > 1 Then
					text &= ", Generalized"
				End If
				node.Text = text
			End If
		End If
	End Sub

	Private Function CreateFaceNode(ByVal allFaces As NFace(), ByVal ParamArray faces() As NFace) As TreeNode
		Dim tag As New NodeTag(faces)
		Dim node As New TreeNode(String.Empty) With {.Tag = tag}
		Dim first As NFace = faces.First()
		For Each attributes As NLAttributes In first.Objects.ToArray()
			Dim child As NFace = CType(attributes.Child, NFace)
			If child IsNot Nothing AndAlso (Not faces.Contains(child)) AndAlso (Not tag.BelongsToNode(child)) Then
				Dim grouped = SubjectUtils.GetFacesInSameGroup(allFaces, child).ToArray()
				node.Nodes.Add(CreateFaceNode(allFaces, grouped))
			End If
		Next attributes
		UpdateFaceNodeText(node)
		Return node
	End Function

	Private Sub UpdateFaceNodeText(ByVal node As TreeNode)
		Dim tag As NodeTag = TryCast(node.Tag, NodeTag)
		If tag IsNot Nothing Then
			Dim text As String = "Face"
			Dim face As NFace = TryCast(tag.Items.Last(), NFace)
			If face IsNot Nothing Then
				Dim attributes As NLAttributes = face.Objects.FirstOrDefault()
				Dim generalized As Boolean = tag.Items.Any(Function(f) f.SessionId <> -1)
				Dim segmented As Boolean = tag.Items.All(Function(f) f.ParentObject IsNot Nothing)
				If (Not segmented) AndAlso attributes IsNot Nothing AndAlso attributes.Quality <> NBiometricTypes.QualityUnknown Then
					text &= String.Format(" (Quality={0})", attributes.Quality)
				End If
				If segmented Then
					text &= ", Segmented"
				End If
				If generalized Then
					text &= ", Generalized"
				End If
				node.Text = text
			End If
		End If
	End Sub

	Private Function CreateIrisNode(ByVal iris As NIris) As TreeNode
		Dim node As New TreeNode(String.Format("Iris ({0})", iris.Position)) With {.Tag = New NodeTag(iris)}
		For Each attribute As NEAttributes In iris.Objects.ToArray()
			Dim child As NIris = CType(attribute.Child, NIris)
			If child IsNot Nothing Then
				node.Nodes.Add(CreateIrisNode(child))
			End If
		Next attribute
		Return node
	End Function

	Private Function CreateVoiceNode(ByVal voice As NVoice) As TreeNode
		Dim text As String = String.Format("Voice ({0})", If(voice.ParentObject IsNot Nothing, "Segmented", "Phrase id: " & voice.PhraseId))
		Dim node As New TreeNode(text) With {.Tag = New NodeTag(voice)}
		For Each attribute As NSAttributes In voice.Objects.ToArray()
			Dim child As NVoice = CType(attribute.Child, NVoice)
			If child IsNot Nothing Then
				node.Nodes.Add(CreateVoiceNode(child))
			End If
		Next attribute
		Return node
	End Function

	Private Function CreatePalmNode(ByVal ParamArray palms() As NPalm) As TreeNode
		Dim node As New TreeNode(String.Empty) With {.Tag = New NodeTag(palms)}
		UpdateFingerNodeText(node)
		Return node
	End Function

	Private Sub OnSubjectChanged()
		Dim selected As Node = SelectedItem
		treeView.BeginUpdate()
		Try
			treeView.Nodes.Clear()
			If _subject IsNot Nothing Then
				Dim fingerNodes As New List(Of TreeNode)()
				Dim faceNodes As New List(Of TreeNode)()
				Dim irisNodes As New List(Of TreeNode)()
				Dim palmNodes As New List(Of TreeNode)()
				Dim voiceNodes As New List(Of TreeNode)()

				Dim allowNewFingers As Boolean = (_allowNew And NBiometricType.Finger) = NBiometricType.Finger
				Dim allowNewFaces As Boolean = (_allowNew And NBiometricType.Face) = NBiometricType.Face
				Dim allowNewIrises As Boolean = (_allowNew And NBiometricType.Iris) = NBiometricType.Iris
				Dim allowNewVoices As Boolean = (_allowNew And NBiometricType.Voice) = NBiometricType.Voice
				Dim allowNewPalms As Boolean = (_allowNew And NBiometricType.Palm) = NBiometricType.Palm

				If (_shownTypes And NBiometricType.Finger) = NBiometricType.Finger Then
					Dim allFingers = _subject.Fingers.ToArray()
					Dim groups = SubjectUtils.GetFingersGeneralizationGroups(allFingers)
					Dim topLevelGroups = groups.Where(Function(g) g.All(Function(x) x.SessionId = -1 OrElse x.ParentObject Is Nothing)).ToArray()
					For Each g In topLevelGroups
						fingerNodes.Add(CreateFingerNode(allFingers, g))
					Next g
					If (Not _showBiometricsOnly) Then
						fingerNodes.Add(New TreeNode("New...") With {.Tag = New NodeTag(_subject.Fingers), .ForeColor = If(allowNewFingers, Color.Black, SystemColors.GrayText)})
					End If
				End If
				If (_shownTypes And NBiometricType.Face) = NBiometricType.Face Then
					Dim allFaces = _subject.Faces.ToArray()
					Dim groups = SubjectUtils.GetFaceGeneralizationGroups(allFaces)
					Dim topLevelGroups = groups.Where(Function(g) g.All(Function(x) x.SessionId = -1 OrElse x.ParentObject Is Nothing)).ToArray()
					For Each g In topLevelGroups
						faceNodes.Add(CreateFaceNode(allFaces, g))
					Next g
					If (Not _showBiometricsOnly) Then
						faceNodes.Add(New TreeNode("New...") With {.Tag = New NodeTag(_subject.Faces), .ForeColor = If(allowNewFaces, Color.Black, SystemColors.GrayText)})
					End If
				End If
				If (_shownTypes And NBiometricType.Iris) = NBiometricType.Iris Then
					For Each item In _subject.Irises.ToArray().Where(Function(x) x.ParentObject Is Nothing)
						irisNodes.Add(CreateIrisNode(item))
					Next item
					If (Not _showBiometricsOnly) Then
						irisNodes.Add(New TreeNode("New...") With {.Tag = New NodeTag(_subject.Irises), .ForeColor = If(allowNewIrises, Color.Black, SystemColors.GrayText)})
					End If
				End If
				If (_shownTypes And NBiometricType.Palm) = NBiometricType.Palm Then
					For Each group In SubjectUtils.GetPalmsGeneralizationGroups(_subject)
						palmNodes.Add(CreatePalmNode(group))
					Next group
					If (Not _showBiometricsOnly) Then
						palmNodes.Add(New TreeNode("New...") With {.Tag = New NodeTag(_subject.Palms), .ForeColor = If(allowNewPalms, Color.Black, SystemColors.GrayText)})
					End If
				End If
				If (_shownTypes And NBiometricType.Voice) = NBiometricType.Voice Then
					For Each item In _subject.Voices.ToArray().Where(Function(x) x.ParentObject Is Nothing)
						voiceNodes.Add(CreateVoiceNode(item))
					Next item
					If (Not _showBiometricsOnly) Then
						voiceNodes.Add(New TreeNode("New...") With {.Tag = New NodeTag(_subject.Voices), .ForeColor = If(allowNewVoices, Color.Black, SystemColors.GrayText)})
					End If
				End If

				If ShowBiometricsOnly Then
					treeView.Nodes.AddRange(fingerNodes.ToArray())
					treeView.Nodes.AddRange(faceNodes.ToArray())
					treeView.Nodes.AddRange(irisNodes.ToArray())
					treeView.Nodes.AddRange(palmNodes.ToArray())
					treeView.Nodes.AddRange(voiceNodes.ToArray())
				Else
					Dim subjectNode As New TreeNode("Subject") With {.Tag = New NodeTag(_subject)}
					If (_shownTypes And NBiometricType.Finger) = NBiometricType.Finger Then
						If allowNewFingers OrElse fingerNodes.Count > 1 Then
							subjectNode.Nodes.Add(New TreeNode("Fingers", fingerNodes.ToArray()) With {.Tag = New NodeTag(_subject.Fingers), .ForeColor = If(allowNewFingers, Color.Black, SystemColors.GrayText)})
						End If
					End If
					If (_shownTypes And NBiometricType.Face) = NBiometricType.Face Then
						If allowNewFaces OrElse faceNodes.Count > 1 Then
							subjectNode.Nodes.Add(New TreeNode("Faces", faceNodes.ToArray()) With {.Tag = New NodeTag(_subject.Faces), .ForeColor = If(allowNewFaces, Color.Black, SystemColors.GrayText)})
						End If
					End If
					If (_shownTypes And NBiometricType.Iris) = NBiometricType.Iris AndAlso irisNodes.Count <> 0 Then
						If allowNewIrises OrElse irisNodes.Count > 1 Then
							subjectNode.Nodes.Add(New TreeNode("Irises", irisNodes.ToArray()) With {.Tag = New NodeTag(_subject.Irises), .ForeColor = If(allowNewIrises, Color.Black, SystemColors.GrayText)})
						End If
					End If
					If (_shownTypes And NBiometricType.Palm) = NBiometricType.Palm Then
						If allowNewPalms OrElse palmNodes.Count > 1 Then
							subjectNode.Nodes.Add(New TreeNode("Palms", palmNodes.ToArray()) With {.Tag = New NodeTag(_subject.Palms), .ForeColor = If(allowNewPalms, Color.Black, SystemColors.GrayText)})
						End If
					End If
					If (_shownTypes And NBiometricType.Voice) = NBiometricType.Voice Then
						If allowNewVoices OrElse voiceNodes.Count > 1 Then
							subjectNode.Nodes.Add(New TreeNode("Voices", voiceNodes.ToArray()) With {.Tag = New NodeTag(_subject.Voices), .ForeColor = If(allowNewVoices, Color.Black, SystemColors.GrayText)})
						End If
					End If
					treeView.Nodes.Add(subjectNode)
				End If
				treeView.ExpandAll()

				If selected IsNot Nothing Then
					SelectedItem = GetNodeFor(selected.Items.First())
				End If
			End If
		Finally
			treeView.EndUpdate()
		End Try
	End Sub

	Private Sub OnBiometricRemoved(ByVal biometric As NBiometric)
		Dim node As TreeNode = GetNodeWithTag(biometric)
		If node IsNot Nothing Then
			Dim index As Integer = node.Index
			Dim parent As TreeNode = node.Parent
			Dim tag As NodeTag = CType(node.Tag, NodeTag)
			tag.Items.Remove(biometric)
			If tag.Items.Count = 0 Then
				Dim subNodes = ( _
				  From item As TreeNode In node.Nodes _
				  Select item).ToArray()
				node.Nodes.Clear()
				node.Remove()
				For Each item As TreeNode In subNodes
					If parent IsNot Nothing Then
						parent.Nodes.Insert(index, item)
						index += 1
					Else
						treeView.Nodes.Insert(index, item)
						index += 1
					End If
				Next item
			End If
			If Not _showBiometricsOnly Then
				If tag.Items.Count = 0 AndAlso parent IsNot Nothing AndAlso parent.Nodes.Count = 1 Then
					tag = CType(parent.Tag, NodeTag)
					If tag.Items.Count = 0 AndAlso (tag.Type And _allowNew) = NBiometricType.None Then
						parent.Remove()
					End If
				End If
			End If
		End If
	End Sub

	Private Sub OnBiometricAdded(ByVal biometric As NBiometric)
		Dim node As TreeNode = Nothing
		Dim parent As TreeNode = Nothing
		Dim parentObject As NBiometricAttributes = Nothing

		node = GetNodeWithTag(biometric)
		If node IsNot Nothing Then
			Dim tag As NodeTag = CType(node.Tag, NodeTag)
			If (Not tag.Items.Contains(biometric)) Then
				tag.Items.Add(biometric)
				If tag.Type = NBiometricType.Face Then
					UpdateFaceNodeText(node)
				ElseIf tag.Type = NBiometricType.Finger OrElse tag.Type = NBiometricType.Palm Then
					UpdateFingerNodeText(node)
				End If
			End If
		Else
			Select Case biometric.BiometricType
				Case NBiometricType.Face
					Dim allFaces = _subject.Faces.ToArray()
					Dim group = SubjectUtils.GetFacesInSameGroup(allFaces, CType(biometric, NFace)).ToArray()
					node = CreateFaceNode(allFaces, group)
					parentObject = biometric.ParentObject
					If parentObject IsNot Nothing Then
						parent = GetNodeWithTag(parentObject.Owner)
					End If
					If parentObject Is Nothing AndAlso (Not ShowBiometricsOnly) Then
						parent = GetNodeWithTag(_subject.Faces)
					End If
					Exit Select
				Case NBiometricType.Finger
					Dim fingers = _subject.Fingers.ToArray()
					Dim group = SubjectUtils.GetFingersInSameGroup(fingers, CType(biometric, NFinger)).ToArray()
					node = CreateFingerNode(fingers, group)
					parentObject = biometric.ParentObject
					If parentObject IsNot Nothing Then
						parent = GetNodeWithTag(parentObject.Owner)
					End If
					If parentObject Is Nothing AndAlso (Not ShowBiometricsOnly) Then
						parent = GetNodeWithTag(_subject.Fingers)
					End If
					Exit Select
				Case NBiometricType.Iris
					node = CreateIrisNode(CType(biometric, NIris))
					parentObject = biometric.ParentObject
					If parentObject IsNot Nothing Then
						parent = GetNodeWithTag(parentObject.Owner)
					End If
					If parentObject Is Nothing AndAlso (Not ShowBiometricsOnly) Then
						parent = GetNodeWithTag(_subject.Irises)
					End If
					Exit Select
				Case NBiometricType.Palm
					Dim palms = _subject.Palms.ToArray()
					Dim group = SubjectUtils.GetPalmsInSameGroup(palms, CType(biometric, NPalm)).ToArray()
					node = CreatePalmNode(group)
					If (Not ShowBiometricsOnly) Then
						parent = GetNodeWithTag(_subject.Palms)
					End If
					Exit Select
				Case NBiometricType.Voice
					node = CreateVoiceNode(CType(biometric, NVoice))
					parentObject = biometric.ParentObject
					If parentObject IsNot Nothing Then
						parent = GetNodeWithTag(parentObject.Owner)
					End If
					If parentObject Is Nothing AndAlso (Not ShowBiometricsOnly) Then
						parent = GetNodeWithTag(_subject.Voices)
					End If
					Exit Select
			End Select

			If parent Is Nothing Then
				treeView.Nodes.Add(node)
				treeView.ExpandAll()
			Else
				Dim index As Integer = parent.Nodes.Count
				If (_allowNew And biometric.BiometricType) = biometric.BiometricType AndAlso index <> 0 Then
					index -= 1
				End If
				parent.Nodes.Insert(index, node)
				parent.ExpandAll()
			End If
		End If
	End Sub

	Private Sub OnSubjectCollectionChanged(ByVal sender As Object, ByVal e As NotifyCollectionChangedEventArgs)
		If IsHandleCreated Then
			If e.Action = NotifyCollectionChangedAction.Reset Then
				BeginInvoke(New MethodInvoker(AddressOf OnSubjectChanged))
			ElseIf e.Action = NotifyCollectionChangedAction.Remove Then
				BeginInvoke(New Action(Of IList)(AddressOf OnBiometricsRemoved), e.OldItems)
			ElseIf e.Action = NotifyCollectionChangedAction.Add Then
				BeginInvoke(New Action(Of IList)(AddressOf OnBiometricsAdded), e.NewItems)
			End If
		End If
	End Sub
	Private Sub OnBiometricsRemoved(ByVal oldItems As Object)
		treeView.BeginUpdate()
		Try
			For Each item As NBiometric In oldItems
				OnBiometricRemoved(item)
			Next item
		Finally
			treeView.EndUpdate()
		End Try
	End Sub
	Private Sub OnBiometricsAdded(ByVal newItems As Object)
		treeView.BeginUpdate()
		Try
			For Each item As NBiometric In newItems
				OnBiometricAdded(item)
			Next item
		Finally
			treeView.EndUpdate()
		End Try
	End Sub

	Private Sub TreeViewAfterSelect(ByVal sender As Object, ByVal e As TreeViewEventArgs) Handles treeView.AfterSelect
		Dim selected = SelectedItem
		tsbRemove.Enabled = selected IsNot Nothing AndAlso selected.IsBiometricNode
		If Enabled AndAlso PropertyChangedEvent IsNot Nothing Then
			RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("SelectedItem"))
		End If
	End Sub

	Private Sub TsbRemoveClick(ByVal sender As Object, ByVal e As EventArgs) Handles tsbRemove.Click
		Dim selection = SelectedItem
		SelectedItem = Nothing
		tsbRemove.Enabled = False
		If selection.IsBiometricNode Then
			Select Case selection.BiometricType
				Case NBiometricType.Face
					Dim parent = selection.GetParent()
					Dim allItems() As NBiometric = selection.AllItems
					If parent IsNot Nothing AndAlso parent.IsBiometricNode Then
						allItems = Enumerable.Union(parent.Items, allItems).ToArray()
					End If

					For Each face In allItems
						_subject.Faces.Remove(CType(face, NFace))
					Next face
				Case NBiometricType.Palm
					For Each item In selection.AllItems
						_subject.Palms.Remove(TryCast(item, NPalm))
					Next item
				Case NBiometricType.Voice
					Dim voice As NVoice = TryCast(selection.AllItems.First(), NVoice)
					Dim relatedVoice As NVoice = Nothing
					If voice.ParentObject IsNot Nothing Then
						relatedVoice = CType(voice.ParentObject.Owner, NVoice)
					Else
						Dim attributes As NSAttributes = voice.Objects.FirstOrDefault()
						If attributes IsNot Nothing Then
							relatedVoice = TryCast(attributes.Child, NVoice)
						End If
					End If
					_subject.Voices.Remove(voice)
					_subject.Voices.Remove(relatedVoice)
					Exit Select
				Case NBiometricType.Finger
					For Each item In selection.Items
						Dim finger As NFinger = TryCast(item, NFinger)
						Dim parentObject As NFAttributes = CType(finger.ParentObject, NFAttributes)
						If parentObject IsNot Nothing Then
							Dim ownerFinger As NFinger = CType(parentObject.Owner, NFinger)
							_subject.Fingers.Remove(ownerFinger)
						End If
						_subject.Fingers.Remove(finger)
					Next item
					For Each item In selection.GeneralizedItems
						_subject.Fingers.Remove(CType(item, NFinger))
					Next item
					Exit Select
				Case NBiometricType.Iris
					Dim iris As NIris = TryCast(selection.Items.First(), NIris)
					Dim parentObject As NEAttributes = CType(iris.ParentObject, NEAttributes)
					If parentObject IsNot Nothing Then
						Dim ownerIris As NIris = CType(parentObject.Owner, NIris)
						_subject.Irises.Remove(ownerIris)
					End If
					_subject.Irises.Remove(iris)
					Exit Select
			End Select
		End If
	End Sub

	Private Sub TreeViewBeforeSelect(ByVal sender As Object, ByVal e As TreeViewCancelEventArgs) Handles treeView.BeforeSelect
		e.Cancel = e.Node.ForeColor = SystemColors.GrayText
	End Sub

	Private Sub SubjectTreeControlEnabledChanged(ByVal sender As Object, ByVal e As EventArgs) Handles treeView.EnabledChanged
		Dim selected = SelectedItem
		tsbRemove.Enabled = selected IsNot Nothing AndAlso selected.IsBiometricNode
	End Sub

#End Region

#Region "Protected methods"

	Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
		Subject = Nothing
		If disposing AndAlso (components IsNot Nothing) Then
			components.Dispose()
		End If
		MyBase.Dispose(disposing)
	End Sub

#End Region

#Region "Public methods"

	Public Sub UpdateTree()
		OnSubjectChanged()
	End Sub

	Public Function GetBiometricNode(ByVal biometric As NBiometric) As Node
		Return GetNodeFor(biometric)
	End Function

	Public Function GetNewNode(ByVal biometricType As NBiometricType) As Node
		If _subject IsNot Nothing Then
			Select Case biometricType
				Case NBiometricType.Face
					Return GetNodeFor(_subject.Faces)
				Case NBiometricType.Finger
					Return GetNodeFor(_subject.Fingers)
				Case NBiometricType.Iris
					Return GetNodeFor(_subject.Irises)
				Case NBiometricType.Palm
					Return GetNodeFor(_subject.Palms)
				Case NBiometricType.Voice
					Return GetNodeFor(_subject.Voices)
			End Select
		End If
		Return Nothing
	End Function

	Public Function GetSubjectNode() As Node
		Return GetNodeFor(_subject)
	End Function

	Public Function GetNodeFor(ByVal param As Object) As Node
		If TypeOf param Is Node Then
			Return CType(param, Node)
		End If
		If param IsNot Nothing Then
			Dim nodeToSelect As TreeNode = GetNodeWithTag(param)
			If nodeToSelect IsNot Nothing Then
				Return New Node(nodeToSelect)
			End If
		End If
		Return Nothing
	End Function

#End Region

#Region "INotifyPropertyChanged Members"

	Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

#End Region
End Class
