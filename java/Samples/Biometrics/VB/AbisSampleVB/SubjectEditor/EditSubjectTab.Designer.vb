Imports Microsoft.VisualBasic
Imports System
Partial Public Class EditSubjectTab
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

	#Region "Windows Form Designer generated code"

	''' <summary>
	''' Required method for Designer support - do not modify
	''' the contents of this method with the code editor.
	''' </summary>
	Private Sub InitializeComponent()
		Me.splitContainer1 = New System.Windows.Forms.SplitContainer
		Me.subjectTree = New Neurotec.Samples.SubjectTreeControl
		Me.pagePanel = New System.Windows.Forms.Panel
		Me.splitContainer1.Panel1.SuspendLayout()
		Me.splitContainer1.Panel2.SuspendLayout()
		Me.splitContainer1.SuspendLayout()
		Me.SuspendLayout()
		'
		'splitContainer1
		'
		Me.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
		Me.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
		Me.splitContainer1.Location = New System.Drawing.Point(0, 0)
		Me.splitContainer1.Name = "splitContainer1"
		'
		'splitContainer1.Panel1
		'
		Me.splitContainer1.Panel1.Controls.Add(Me.subjectTree)
		'
		'splitContainer1.Panel2
		'
		Me.splitContainer1.Panel2.Controls.Add(Me.pagePanel)
		Me.splitContainer1.Size = New System.Drawing.Size(582, 402)
		Me.splitContainer1.SplitterDistance = 227
		Me.splitContainer1.TabIndex = 0
		'
		'subjectTree
		'
		Me.subjectTree.AllowNew = CType(((((Neurotec.Biometrics.NBiometricType.Face Or Neurotec.Biometrics.NBiometricType.Voice) _
					Or Neurotec.Biometrics.NBiometricType.Fingerprint) _
					Or Neurotec.Biometrics.NBiometricType.Iris) _
					Or Neurotec.Biometrics.NBiometricType.PalmPrint), Neurotec.Biometrics.NBiometricType)
		Me.subjectTree.AllowRemove = True
		Me.subjectTree.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.subjectTree.Dock = System.Windows.Forms.DockStyle.Fill
		Me.subjectTree.Location = New System.Drawing.Point(0, 0)
		Me.subjectTree.Name = "subjectTree"
		Me.subjectTree.SelectedItem = Nothing
		Me.subjectTree.ShowBiometricsOnly = False
		Me.subjectTree.ShownTypes = CType(((((Neurotec.Biometrics.NBiometricType.Face Or Neurotec.Biometrics.NBiometricType.Voice) _
					Or Neurotec.Biometrics.NBiometricType.Fingerprint) _
					Or Neurotec.Biometrics.NBiometricType.Iris) _
					Or Neurotec.Biometrics.NBiometricType.PalmPrint), Neurotec.Biometrics.NBiometricType)
		Me.subjectTree.Size = New System.Drawing.Size(227, 402)
		Me.subjectTree.Subject = Nothing
		Me.subjectTree.TabIndex = 0
		'
		'pagePanel
		'
		Me.pagePanel.Dock = System.Windows.Forms.DockStyle.Fill
		Me.pagePanel.Location = New System.Drawing.Point(0, 0)
		Me.pagePanel.Name = "pagePanel"
		Me.pagePanel.Size = New System.Drawing.Size(351, 402)
		Me.pagePanel.TabIndex = 0
		'
		'EditSubjectTab
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
		Me.Controls.Add(Me.splitContainer1)
		Me.Name = "EditSubjectTab"
		Me.Size = New System.Drawing.Size(582, 402)
		Me.splitContainer1.Panel1.ResumeLayout(False)
		Me.splitContainer1.Panel2.ResumeLayout(False)
		Me.splitContainer1.ResumeLayout(False)
		Me.ResumeLayout(False)

	End Sub

	#End Region

	Private splitContainer1 As System.Windows.Forms.SplitContainer
	Private subjectTree As SubjectTreeControl
	Private pagePanel As System.Windows.Forms.Panel
End Class
