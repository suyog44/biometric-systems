Imports Microsoft.VisualBasic
Imports System
Imports System.Drawing
Imports System.IO
Imports System.Windows.Forms
Imports Neurotec.Devices
Imports Neurotec.Images
Imports Neurotec.Samples.Forms

Namespace Controls
	Partial Public Class InfoPanel
		Inherits UserControl
		#Region "Public constructor"

		Public Sub New()
			InitializeComponent()

			OnModelChanged()
		End Sub

		#End Region

		#Region "Private fields"

		Private _model As DataModel
		Private _deviceManager As NDeviceManager

		#End Region

		#Region "Public properties"

		Public Property Model() As DataModel
			Get
				Return _model
			End Get
			Set(ByVal value As DataModel)
				_model = value
				OnModelChanged()
			End Set
		End Property

		Public Property DeviceManager() As NDeviceManager
			Get
				Return _deviceManager
			End Get
			Set(ByVal value As NDeviceManager)
				_deviceManager = value
			End Set
		End Property

		#End Region

		#Region "Private methods"

		Private Sub OnModelChanged()
			If DesignMode Then
				Return
			End If

			Dim showThumbnail As Boolean = False
			If Model IsNot Nothing Then
				propertyGrid.SelectedObject = Model.Info

				Dim thumbnail As InfoField = Nothing
				For Each item As InfoField In Model.Info
					If item.ShowAsThumbnail Then
						thumbnail = item
						Exit For
					End If
				Next item
				If thumbnail IsNot Nothing Then
					pictureBoxThumbnail.Image = Nothing
					pictureBoxThumbnail.Tag = thumbnail
					lblThumbnailKey.Text = thumbnail.Key
					showThumbnail = True
					If thumbnail.Value IsNot Nothing AndAlso thumbnail.Value.GetType() Is GetType(Byte()) Then
						Using stream As New MemoryStream(CType(thumbnail.Value, Byte()))
							pictureBoxThumbnail.Image = Image.FromStream(stream)
						End Using
					End If
				End If
			Else
				propertyGrid.SelectedObject = Nothing
			End If

			If showThumbnail Then
				tableLayoutPanelMain.ColumnStyles(0) = New ColumnStyle(SizeType.Percent, 33)
				panelThumnail.Visible = True
			Else
				panelThumnail.Visible = False
				tableLayoutPanelMain.ColumnStyles(0) = New ColumnStyle(SizeType.AutoSize, 33)
			End If
		End Sub

		Private Sub BtnOpenClick(ByVal sender As Object, ByVal e As EventArgs) Handles btnOpen.Click
			openFileDialog.Filter = NImages.GetOpenFileFilterString(True, False)
			If openFileDialog.ShowDialog() = DialogResult.OK Then
				Try
					Dim thumbnail As InfoField = TryCast(pictureBoxThumbnail.Tag, InfoField)
					Dim image As NImage = NImage.FromFile(openFileDialog.FileName)
					thumbnail.Value = image
					pictureBoxThumbnail.Image = image.ToBitmap()
				Catch ex As Exception
					Utilities.ShowError(ex)
				End Try
			End If
		End Sub

		Private Sub BtnCaptureClick(ByVal sender As Object, ByVal e As EventArgs) Handles btnCapture.Click
			Dim cameraFound As Boolean = False
			For Each item As NDevice In DeviceManager.Devices
				If TypeOf item Is NCamera Then
					cameraFound = True
					Exit For
				End If
			Next item

			If (Not cameraFound) Then
				Utilities.ShowInformation("No cameras connected. Please connect camera and try again")
				Return
			End If

			Using form As New PictureCaptureForm()
				form.DeviceManager = DeviceManager
				If form.ShowDialog() <> DialogResult.OK Then
					Return
				End If
				Dim thumbnail As InfoField = TryCast(pictureBoxThumbnail.Tag, InfoField)
				If thumbnail.Value IsNot Nothing Then
					Dim image As NImage = TryCast(thumbnail.Value, NImage)
					If image IsNot Nothing Then
						image.Dispose()
					End If
				End If
				thumbnail.Value = form.Image

				pictureBoxThumbnail.Image = form.Image.ToBitmap()
			End Using
		End Sub

		#End Region

		Private Sub InfoPanelLoad(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
			btnCapture.Visible = Neurotec.Licensing.NLicense.IsComponentActivated("Devices.Cameras")
		End Sub
	End Class
End Namespace
