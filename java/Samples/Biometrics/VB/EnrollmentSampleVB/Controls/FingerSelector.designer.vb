Imports Microsoft.VisualBasic
Imports System
Namespace Controls
	Public NotInheritable Partial Class FingerSelector
		''' <summary> 
		''' Required designer variable.
		''' </summary>
		Private components As System.ComponentModel.IContainer = Nothing

		''' <summary> 
		''' Clean up any resources being used.
		''' </summary>
		''' <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		Protected Overrides Sub Dispose(ByVal disposing As Boolean)
			If disposing AndAlso (components IsNot Nothing) Then
				components.Dispose()
			End If
			MyBase.Dispose(disposing)
		End Sub

		#Region "Component Designer generated code"

		''' <summary> 
		''' Required method for Designer support - do not modify 
		''' the contents of this method with the code editor.
		''' </summary>
		Private Sub InitializeComponent()
			Me.components = New System.ComponentModel.Container()
			Me.timer = New System.Windows.Forms.Timer(Me.components)
			Me.SuspendLayout()
			' 
			' timer
			' 
'			Me.timer.Tick += New System.EventHandler(Me.TimerTick);
			' 
			' FingerSelector
			' 
			Me.Size = New System.Drawing.Size(650, 300)
'			Me.MouseMove += New System.Windows.Forms.MouseEventHandler(Me.FingerSelectorMouseMove);
'			Me.MouseClick += New System.Windows.Forms.MouseEventHandler(Me.FingerSelectorMouseClick);
			Me.ResumeLayout(False)

		End Sub

		#End Region

		Private WithEvents timer As System.Windows.Forms.Timer

	End Class
End Namespace
