Imports Microsoft.VisualBasic
Imports System
Imports System.Runtime.InteropServices
Imports System.Windows.Forms
Imports System.Collections.Generic

Public Enum TwainCommand
	[Not] = -1
	Null = 0
	TransferReady = 1
	CloseRequest = 2
	CloseOk = 3
	DeviceEvent = 4
End Enum

Public Class Twain
	Private Const CountryUSA As Short = 1
	Private Const LanguageUSA As Short = 13

	Public Sub New()
		_appid = New TwIdentity()
		_appid.Id = IntPtr.Zero
		_appid.Version.MajorNum = 1
		_appid.Version.MinorNum = 1
		_appid.Version.Language = LanguageUSA
		_appid.Version.Country = CountryUSA
		_appid.Version.Info = "1.0"
		_appid.ProtocolMajor = TwProtocol.Major
		_appid.ProtocolMinor = TwProtocol.Minor
		_appid.SupportedGroups = CInt(Fix(TwDG.Image Or TwDG.Control))
		_appid.Manufacturer = "Neurotechnology"
		_appid.ProductFamily = "SDK"
		_appid.ProductName = "AbisSampleVB"

		_srcds = New TwIdentity()
		_srcds.Id = IntPtr.Zero

		_evtmsg.EventPtr = Marshal.AllocHGlobal(Marshal.SizeOf(_winmsg))
	End Sub

	Protected Overrides Sub Finalize()
		Marshal.FreeHGlobal(_evtmsg.EventPtr)
	End Sub

	Public Function Init(ByVal hwndp As IntPtr) As Boolean
		Finish()
		Dim rc As TwRC = DSMparent(_appid, IntPtr.Zero, TwDG.Control, TwDAT.Parent, TwMSG.OpenDSM, hwndp)
		If rc = TwRC.Success Then
			rc = DSMident(_appid, IntPtr.Zero, TwDG.Control, TwDAT.Identity, TwMSG.GetDefault, _srcds)
			If rc = TwRC.Success Then
				_hwnd = hwndp
			Else
				rc = DSMparent(_appid, IntPtr.Zero, TwDG.Control, TwDAT.Parent, TwMSG.CloseDSM, hwndp)
			End If

			If rc = TwRC.Success Then
				Return True
			End If
		Else
			Return False
		End If

		Return True
	End Function

	Public Function [Select]() As Boolean
		CloseSrc()
		If _appid.Id = IntPtr.Zero Then
			Init(_hwnd)
			If _appid.Id = IntPtr.Zero Then
				Return False
			End If
		End If
		Dim rc As TwRC = DSMident(_appid, IntPtr.Zero, TwDG.Control, TwDAT.Identity, TwMSG.UserSelect, _srcds)

		Return rc = TwRC.Success
	End Function

	Public Function Acquire() As Boolean
		CloseSrc()
		If _appid.Id = IntPtr.Zero Then
			Init(_hwnd)
			If _appid.Id = IntPtr.Zero Then
				Return False
			End If
		End If
		Dim rc As TwRC = DSMident(_appid, IntPtr.Zero, TwDG.Control, TwDAT.Identity, TwMSG.OpenDS, _srcds)
		If rc <> TwRC.Success Then
			Return False
		End If

		Dim cap As New TwCapability(TwCap.XferCount, 1)
		rc = DScap(_appid, _srcds, TwDG.Control, TwDAT.Capability, TwMSG.Set, cap)
		If rc <> TwRC.Success Then
			CloseSrc()
			Return False
		End If

		Dim guif As New TwUserInterface()
		guif.ShowUI = 1
		guif.ModalUI = 1
		guif.ParentHand = _hwnd
		rc = DSuserif(_appid, _srcds, TwDG.Control, TwDAT.UserInterface, TwMSG.EnableDS, guif)
		If rc <> TwRC.Success Then
			CloseSrc()
			Return False
		End If

		Return True
	End Function

	Public Class TransferedPicture
		Public Sub New(ByVal bitmap As IntPtr, ByVal xres As Integer, ByVal yres As Integer)
			hBitmap = bitmap
			xResolution = xres
			yResolution = yres
		End Sub

		Public hBitmap As IntPtr
		Public xResolution As Integer
		Public yResolution As Integer
	End Class

	Public Function TransferPictures() As List(Of TransferedPicture)
		Dim pics As New List(Of TransferedPicture)()
		If _srcds.Id = IntPtr.Zero Then
			Return pics
		End If

		Dim rc As TwRC
		Dim pxfr As New TwPendingXfers()

		Do
			pxfr.Count = 0
			Dim hbitmap As IntPtr = IntPtr.Zero

			Dim iinf As New TwImageInfo()
			rc = DSiinf(_appid, _srcds, TwDG.Image, TwDAT.ImageInfo, TwMSG.Get, iinf)
			If rc <> TwRC.Success Then
				CloseSrc()
				Return pics
			End If

			rc = DSixfer(_appid, _srcds, TwDG.Image, TwDAT.ImageNativeXfer, TwMSG.Get, hbitmap)
			If rc <> TwRC.XferDone Then
				CloseSrc()
				Return pics
			End If

			rc = DSpxfer(_appid, _srcds, TwDG.Control, TwDAT.PendingXfers, TwMSG.EndXfer, pxfr)
			If rc <> TwRC.Success Then
				CloseSrc()
				Return pics
			End If

			pics.Add(New TransferedPicture(hbitmap, iinf.XResolution, iinf.YResolution))
		Loop While pxfr.Count <> 0

		rc = DSpxfer(_appid, _srcds, TwDG.Control, TwDAT.PendingXfers, TwMSG.Reset, pxfr)
		If rc <> TwRC.Success Then
			CloseSrc()
			Return pics
		End If
		Return pics
	End Function

	Public Function PassMessage(ByRef m As Message) As TwainCommand
		If _srcds.Id = IntPtr.Zero Then
			Return TwainCommand.Not
		End If

		Dim pos As Integer = GetMessagePos()

		_winmsg.hwnd = m.HWnd
		_winmsg.message = m.Msg
		_winmsg.wParam = m.WParam
		_winmsg.lParam = m.LParam
		_winmsg.time = GetMessageTime()
		_winmsg.x = CShort(Fix(pos))
		_winmsg.y = CShort(Fix(pos >> 16))

		Marshal.StructureToPtr(_winmsg, _evtmsg.EventPtr, False)
		_evtmsg.Message = 0
		Dim rc As TwRC = DSevent(_appid, _srcds, TwDG.Control, TwDAT.Event, TwMSG.ProcessEvent, _evtmsg)
		If rc = TwRC.NotDSEvent Then
			Return TwainCommand.Not
		End If
		If _evtmsg.Message = CShort(Fix(TwMSG.XFerReady)) Then
			Return TwainCommand.TransferReady
		End If
		If _evtmsg.Message = CShort(Fix(TwMSG.CloseDSReq)) Then
			Return TwainCommand.CloseRequest
		End If
		If _evtmsg.Message = CShort(Fix(TwMSG.CloseDSOK)) Then
			Return TwainCommand.CloseOk
		End If
		If _evtmsg.Message = CShort(Fix(TwMSG.DeviceEvent)) Then
			Return TwainCommand.DeviceEvent
		End If

		Return TwainCommand.Null
	End Function

	Public Sub CloseSrc()
		Dim rc As TwRC
		If _srcds.Id <> IntPtr.Zero Then
			Dim guif As New TwUserInterface()
			rc = DSuserif(_appid, _srcds, TwDG.Control, TwDAT.UserInterface, TwMSG.DisableDS, guif)
			rc = DSMident(_appid, IntPtr.Zero, TwDG.Control, TwDAT.Identity, TwMSG.CloseDS, _srcds)
		End If
	End Sub

	Public Sub Finish()
		Dim rc As TwRC
		CloseSrc()
		If _appid.Id <> IntPtr.Zero Then
			rc = DSMparent(_appid, IntPtr.Zero, TwDG.Control, TwDAT.Parent, TwMSG.CloseDSM, _hwnd)
		End If
		_appid.Id = IntPtr.Zero
	End Sub

	Private _hwnd As IntPtr
	Private ReadOnly _appid As TwIdentity
	Private ReadOnly _srcds As TwIdentity
	Private _evtmsg As TwEvent
	Private _winmsg As WINMSG

	' ------ DSM entry point DAT_ variants:
	<DllImport("twain_32.dll", EntryPoint:="#1")> _
	Private Shared Function DSMparent(<[In](), Out()> ByVal origin As TwIdentity, ByVal zeroptr As IntPtr, ByVal dg As TwDG, ByVal dat As TwDAT, ByVal msg As TwMSG, ByRef refptr As IntPtr) As TwRC
	End Function

	<DllImport("twain_32.dll", EntryPoint:="#1")> _
	Private Shared Function DSMident(<[In](), Out()> ByVal origin As TwIdentity, ByVal zeroptr As IntPtr, ByVal dg As TwDG, ByVal dat As TwDAT, ByVal msg As TwMSG, <[In](), Out()> ByVal idds As TwIdentity) As TwRC
	End Function

	<DllImport("twain_32.dll", EntryPoint:="#1")> _
	Private Shared Function DSMstatus(<[In](), Out()> ByVal origin As TwIdentity, ByVal zeroptr As IntPtr, ByVal dg As TwDG, ByVal dat As TwDAT, ByVal msg As TwMSG, <[In](), Out()> ByVal dsmstat As TwStatus) As TwRC
	End Function

	' ------ DSM entry point DAT_ variants to DS:
	<DllImport("twain_32.dll", EntryPoint:="#1")> _
	Private Shared Function DSuserif(<[In](), Out()> ByVal origin As TwIdentity, <[In](), Out()> ByVal dest As TwIdentity, ByVal dg As TwDG, ByVal dat As TwDAT, ByVal msg As TwMSG, ByVal guif As TwUserInterface) As TwRC
	End Function

	<DllImport("twain_32.dll", EntryPoint:="#1")> _
	Private Shared Function DSevent(<[In](), Out()> ByVal origin As TwIdentity, <[In](), Out()> ByVal dest As TwIdentity, ByVal dg As TwDG, ByVal dat As TwDAT, ByVal msg As TwMSG, ByRef evt As TwEvent) As TwRC
	End Function

	<DllImport("twain_32.dll", EntryPoint:="#1")> _
	Private Shared Function DSstatus(<[In](), Out()> ByVal origin As TwIdentity, <[In]()> ByVal dest As TwIdentity, ByVal dg As TwDG, ByVal dat As TwDAT, ByVal msg As TwMSG, <[In](), Out()> ByVal dsmstat As TwStatus) As TwRC
	End Function

	<DllImport("twain_32.dll", EntryPoint:="#1")> _
	Private Shared Function DScap(<[In](), Out()> ByVal origin As TwIdentity, <[In]()> ByVal dest As TwIdentity, ByVal dg As TwDG, ByVal dat As TwDAT, ByVal msg As TwMSG, <[In](), Out()> ByVal capa As TwCapability) As TwRC
	End Function

	<DllImport("twain_32.dll", EntryPoint:="#1")> _
	Private Shared Function DSiinf(<[In](), Out()> ByVal origin As TwIdentity, <[In]()> ByVal dest As TwIdentity, ByVal dg As TwDG, ByVal dat As TwDAT, ByVal msg As TwMSG, <[In](), Out()> ByVal imginf As TwImageInfo) As TwRC
	End Function

	<DllImport("twain_32.dll", EntryPoint:="#1")> _
	Private Shared Function DSixfer(<[In](), Out()> ByVal origin As TwIdentity, <[In]()> ByVal dest As TwIdentity, ByVal dg As TwDG, ByVal dat As TwDAT, ByVal msg As TwMSG, ByRef hbitmap As IntPtr) As TwRC
	End Function

	<DllImport("twain_32.dll", EntryPoint:="#1")> _
	Private Shared Function DSpxfer(<[In](), Out()> ByVal origin As TwIdentity, <[In]()> ByVal dest As TwIdentity, ByVal dg As TwDG, ByVal dat As TwDAT, ByVal msg As TwMSG, <[In](), Out()> ByVal pxfr As TwPendingXfers) As TwRC
	End Function

	<DllImport("kernel32.dll", ExactSpelling:=True)> _
	Friend Shared Function GlobalAlloc(ByVal flags As Integer, ByVal size As Integer) As IntPtr
	End Function
	<DllImport("kernel32.dll", ExactSpelling:=True)> _
	Friend Shared Function GlobalLock(ByVal handle As IntPtr) As IntPtr
	End Function
	<DllImport("kernel32.dll", ExactSpelling:=True)> _
	Friend Shared Function GlobalUnlock(ByVal handle As IntPtr) As Boolean
	End Function
	<DllImport("kernel32.dll", ExactSpelling:=True)> _
	Friend Shared Function GlobalFree(ByVal handle As IntPtr) As IntPtr
	End Function

	<DllImport("user32.dll", ExactSpelling:=True)> _
	Private Shared Function GetMessagePos() As Integer
	End Function
	<DllImport("user32.dll", ExactSpelling:=True)> _
	Private Shared Function GetMessageTime() As Integer
	End Function

	<DllImport("gdi32.dll", ExactSpelling:=True)> _
	Private Shared Function GetDeviceCaps(ByVal hDC As IntPtr, ByVal nIndex As Integer) As Integer
	End Function

	<DllImport("gdi32.dll", CharSet:=CharSet.Auto)> _
	Private Shared Function CreateDC(ByVal szdriver As String, ByVal szdevice As String, ByVal szoutput As String, ByVal devmode As IntPtr) As IntPtr
	End Function

	<DllImport("gdi32.dll", ExactSpelling:=True)> _
	Private Shared Function DeleteDC(ByVal hdc As IntPtr) As Boolean
	End Function

	Public Shared ReadOnly Property ScreenBitDepth() As Integer
		Get
			Dim screenDc As IntPtr = CreateDC("DISPLAY", Nothing, Nothing, IntPtr.Zero)
			Dim bitDepth As Integer = GetDeviceCaps(screenDc, 12)
			bitDepth *= GetDeviceCaps(screenDc, 14)
			DeleteDC(screenDc)
			Return bitDepth
		End Get
	End Property

	<StructLayout(LayoutKind.Sequential, Pack:=4)> _
	Friend Structure WINMSG
		Public hwnd As IntPtr
		Public message As Integer
		Public wParam As IntPtr
		Public lParam As IntPtr
		Public time As Integer
		Public x As Integer
		Public y As Integer
	End Structure
End Class ' class Twain
