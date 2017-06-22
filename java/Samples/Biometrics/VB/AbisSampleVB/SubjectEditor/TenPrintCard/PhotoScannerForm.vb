Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Runtime.InteropServices
Imports System.Windows.Forms
Imports System.Drawing
Imports Neurotec.Images

Public Class NPhotoScannerForm
	Inherits Form
	Implements IMessageFilter
	Private Const InchesPerMeter As Double = 39.3700787

	Private _tw As Twain
	Private _bmi As BITMAPINFOHEADER
	Private _scanning As Boolean
	Private _image As NImage

	Private _bmpptr As IntPtr
	Private _pixptr As IntPtr
	Private _dibhand As IntPtr

	Public Sub New(ByVal parent As Form)
		InitializeComponent()

		Owner = parent
		_tw = New Twain()

		Dim handle As IntPtr = IntPtr.Zero
		If parent IsNot Nothing Then
			handle = parent.Handle
		End If
		If (Not _tw.Init(handle)) Then
			Throw New Exception("Twain library failed to initialize")
		End If
	End Sub

	Private Sub Clearmem()
		If _dibhand <> IntPtr.Zero Then
			GlobalFree(_dibhand)
			_dibhand = IntPtr.Zero
		End If
	End Sub

	Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
		If disposing Then
			_tw.Finish()
		End If

		Clearmem()

		MyBase.Dispose(disposing)
	End Sub

	Public Sub SelectDevice()
		_tw.Select()
	End Sub

	Public Function Scan() As NImage
		Owner.Enabled = False

		Try
			_scanning = True
			Application.AddMessageFilter(Me)

			If (Not _tw.Acquire()) Then
				Return Nothing
			End If

			Do While _scanning
				Application.DoEvents()
				System.Threading.Thread.Sleep(50)
			Loop
		Finally
			EndingScan()
			Owner.Enabled = True
		End Try

		If _image Is Nothing Then
			Clearmem()
			Return Nothing
		End If

		Dim img As NImage = _image
		img.HorzResolution = CSng(Math.Round(_bmi.biXPelsPerMeter / InchesPerMeter))
		img.VertResolution = CSng(Math.Round(_bmi.biYPelsPerMeter / InchesPerMeter))
		Clearmem()

		Return img
	End Function

	Private Function PreFilterMessage(ByRef m As Message) As Boolean Implements IMessageFilter.PreFilterMessage
		Dim cmd As TwainCommand = _tw.PassMessage(m)
		If cmd = TwainCommand.Not Then
			Return False
		End If

		Select Case cmd
			Case TwainCommand.CloseRequest
				EndingScan()
				_tw.CloseSrc()
				Exit Select
			Case TwainCommand.CloseOk
				EndingScan()
				_tw.CloseSrc()
				Exit Select
			Case TwainCommand.DeviceEvent
				Exit Select
			Case TwainCommand.TransferReady
				Dim pics As List(Of Twain.TransferedPicture) = _tw.TransferPictures()
				EndingScan()
				_tw.CloseSrc()

				If pics.Count = 0 Then
					Exit Select
				End If

				' only use first scanned image, free others
				For i As Integer = 1 To pics.Count - 1
					Dim img As IntPtr = pics(i).hBitmap

					If img <> IntPtr.Zero Then
						GlobalFree(img)
					End If
				Next i

				SetImage(pics(0).hBitmap, pics(0).xResolution, pics(0).yResolution)

				Exit Select
		End Select

		Return True
	End Function

	Private Sub EndingScan()
		If _scanning Then
			Application.RemoveMessageFilter(Me)
			_scanning = False
		End If
	End Sub

	Private Sub SetImage(ByVal img As IntPtr, ByVal xResolution As Integer, ByVal yResolution As Integer)
		_dibhand = img
		_bmpptr = GlobalLock(_dibhand)
		_pixptr = GetPixelInfo(_bmpptr)

		Using bmp As New Bitmap(_bmi.biWidth, _bmi.biHeight)
			If _bmi.biXPelsPerMeter <> 0 AndAlso _bmi.biYPelsPerMeter <> 0 Then
				bmp.SetResolution(CInt(Fix(Math.Round(_bmi.biXPelsPerMeter / InchesPerMeter))), CInt(Fix(Math.Round(_bmi.biYPelsPerMeter / InchesPerMeter))))
			Else
				bmp.SetResolution(xResolution, yResolution)
			End If

			Dim graphics As Graphics = graphics.FromImage(bmp)

			Dim hdc As IntPtr = graphics.GetHdc()

			SetDIBitsToDevice(hdc, 0, 0, _bmi.biWidth, _bmi.biHeight, 0, 0, 0, _bmi.biHeight, _pixptr, _bmpptr, 0)

			graphics.ReleaseHdc(hdc)

			_image = NImage.FromBitmap(bmp)
		End Using
	End Sub

#Region "Windows API stuff"

	Private _bmprect As Rectangle

	Private Function GetPixelInfo(ByVal bmpptr As IntPtr) As IntPtr
		_bmi = New BITMAPINFOHEADER()
		Marshal.PtrToStructure(bmpptr, _bmi)

		_bmprect.Y = 0
		_bmprect.X = _bmprect.Y
		_bmprect.Width = _bmi.biWidth
		_bmprect.Height = _bmi.biHeight

		If _bmi.biSizeImage = 0 Then
			_bmi.biSizeImage = ((((_bmi.biWidth * _bmi.biBitCount) + 31) And (Not 31)) >> 3) * _bmi.biHeight
		End If

		Dim p As Integer = _bmi.biClrUsed

		If (p = 0) AndAlso (_bmi.biBitCount <= 8) Then
			p = 1 << _bmi.biBitCount
		End If

		p = (p * 4) + _bmi.biSize + CInt(Fix(bmpptr))

		Return New IntPtr(p)
	End Function

	<StructLayout(LayoutKind.Sequential, Pack:=2)> _
	Friend Class BITMAPINFOHEADER
		Public biSize As Integer
		Public biWidth As Integer
		Public biHeight As Integer
		Public biPlanes As Short
		Public biBitCount As Short
		Public biCompression As Integer
		Public biSizeImage As Integer
		Public biXPelsPerMeter As Integer
		Public biYPelsPerMeter As Integer
		Public biClrUsed As Integer
		Public biClrImportant As Integer
	End Class

	<DllImport("gdi32.dll", ExactSpelling:=True)> _
	Friend Shared Function SetDIBitsToDevice(ByVal hdc As IntPtr, ByVal xdst As Integer, ByVal ydst As Integer, ByVal width As Integer, ByVal height As Integer, ByVal xsrc As Integer, ByVal ysrc As Integer, ByVal start As Integer, ByVal lines As Integer, ByVal bitsptr As IntPtr, ByVal bmiptr As IntPtr, ByVal color As Integer) As Integer
	End Function

	<DllImport("kernel32.dll", ExactSpelling:=True)> _
	Friend Shared Function GlobalLock(ByVal handle As IntPtr) As IntPtr
	End Function

	<DllImport("kernel32.dll", ExactSpelling:=True)> _
	Friend Shared Function GlobalFree(ByVal handle As IntPtr) As IntPtr
	End Function

#End Region

	Private Sub InitializeComponent()
		Dim resources As New System.ComponentModel.ComponentResourceManager(GetType(NPhotoScannerForm))
		SuspendLayout()
		' 
		' NPhotoScannerForm
		' 
		ClientSize = New Size(284, 262)
		Icon = (CType(resources.GetObject("$this.Icon"), Icon))
		Name = "NPhotoScannerForm"
		ResumeLayout(False)
	End Sub
End Class
