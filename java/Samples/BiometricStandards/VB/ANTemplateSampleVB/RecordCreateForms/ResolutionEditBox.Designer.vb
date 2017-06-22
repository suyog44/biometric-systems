Imports Microsoft.VisualBasic
Imports System

Namespace RecordCreateForms
	Partial Public Class ResolutionEditBox
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
			Me.tbValue = New System.Windows.Forms.TextBox()
			Me.cbScaleUnits = New System.Windows.Forms.ComboBox()
			Me.SuspendLayout()
			' 
			' tbValue
			' 
			Me.tbValue.Anchor = (CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles))
			Me.tbValue.Location = New System.Drawing.Point(0, 3)
			Me.tbValue.Name = "tbValue"
			Me.tbValue.Size = New System.Drawing.Size(124, 20)
			Me.tbValue.TabIndex = 0
			' 
			' cbUnit
			' 
			Me.cbScaleUnits.Anchor = (CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles))
			Me.cbScaleUnits.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
			Me.cbScaleUnits.FormattingEnabled = True
			Me.cbScaleUnits.Location = New System.Drawing.Point(130, 3)
			Me.cbScaleUnits.Name = "cbUnit"
			Me.cbScaleUnits.Size = New System.Drawing.Size(80, 21)
			Me.cbScaleUnits.TabIndex = 1
			'			Me.cbScaleUnits.SelectedIndexChanged += New System.EventHandler(Me.CbScaleUnitsSelectedIndexChanged);
			' 
			' ResolutionEditBox
			' 
			Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0F, 13.0F)
			Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
			Me.Controls.Add(Me.cbScaleUnits)
			Me.Controls.Add(Me.tbValue)
			Me.Name = "ResolutionEditBox"
			Me.Size = New System.Drawing.Size(213, 30)
			Me.ResumeLayout(False)
			Me.PerformLayout()

		End Sub

#End Region

		Private tbValue As System.Windows.Forms.TextBox
		Private WithEvents cbScaleUnits As System.Windows.Forms.ComboBox
	End Class
End Namespace