﻿'------------------------------------------------------------------------------
' <auto-generated>
'     This code was generated by a tool.
'     Runtime Version:2.0.50727.5456
'
'     Changes to this file may cause incorrect behavior and will be lost if
'     the code is regenerated.
' </auto-generated>
'------------------------------------------------------------------------------


Imports Microsoft.VisualBasic
Imports System
Namespace My


	<Global.System.Runtime.CompilerServices.CompilerGeneratedAttribute(), Global.System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "9.0.0.0")> _
	Friend NotInheritable Partial Class Settings
		Inherits System.Configuration.ApplicationSettingsBase

		Private Shared defaultInstance As Settings = (CType(Global.System.Configuration.ApplicationSettingsBase.Synchronized(New Settings()), Settings))

		Public Shared ReadOnly Property [Default]() As Settings
			Get
				Return defaultInstance
			End Get
		End Property

		<Global.System.Configuration.UserScopedSettingAttribute(), Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.Configuration.DefaultSettingValueAttribute("")> _
		Public Property LastDirectory() As String
			Get
				Return (CStr(Me("LastDirectory")))
			End Get
			Set(ByVal value As String)
				Me("LastDirectory") = value
			End Set
		End Property

		<Global.System.Configuration.UserScopedSettingAttribute(), Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.Configuration.DefaultSettingValueAttribute("Standard")> _
		Public Property NewValidationLevel() As Global.Neurotec.Biometrics.Standards.ANValidationLevel
			Get
				Return (CType(Me("NewValidationLevel"), Global.Neurotec.Biometrics.Standards.ANValidationLevel))
			End Get
			Set(ByVal value As Neurotec.Biometrics.Standards.ANValidationLevel)
				Me("NewValidationLevel") = value
			End Set
		End Property

		<Global.System.Configuration.UserScopedSettingAttribute(), Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.Configuration.DefaultSettingValueAttribute("Standard")> _
		Public Property ValidationLevel() As Global.Neurotec.Biometrics.Standards.ANValidationLevel
			Get
				Return (CType(Me("ValidationLevel"), Global.Neurotec.Biometrics.Standards.ANValidationLevel))
			End Get
			Set(ByVal value As Neurotec.Biometrics.Standards.ANValidationLevel)
				Me("ValidationLevel") = value
			End Set
		End Property

		<Global.System.Configuration.UserScopedSettingAttribute(), Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.Configuration.DefaultSettingValueAttribute("False")> _
		Public Property NewUseNistMinutiaNeighbors() As Boolean
			Get
				Return (CBool(Me("NewUseNistMinutiaNeighbors")))
			End Get
			Set(ByVal value As Boolean)
				Me("NewUseNistMinutiaNeighbors") = value
			End Set
		End Property

		<Global.System.Configuration.UserScopedSettingAttribute(), Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.Configuration.DefaultSettingValueAttribute("False")> _
		Public Property UseNistMinutiaNeighbors() As Boolean
			Get
				Return (CBool(Me("UseNistMinutiaNeighbors")))
			End Get
			Set(ByVal value As Boolean)
				Me("UseNistMinutiaNeighbors") = value
			End Set
		End Property

        <Global.System.Configuration.UserScopedSettingAttribute(), Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.Configuration.DefaultSettingValueAttribute("True")> _
  Public Property NonStrictRead() As Boolean
            Get
                Return (CBool(Me("NonStrictRead")))
            End Get
            Set(ByVal value As Boolean)
                Me("NonStrictRead") = value
            End Set
        End Property

		<Global.System.Configuration.UserScopedSettingAttribute(), Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.Configuration.DefaultSettingValueAttribute("False")> _
		Public Property MergeDuplicateFields() As Boolean
			Get
				Return (CBool(Me("MergeDuplicateFields")))
			End Get
			Set(ByVal value As Boolean)
				Me("MergeDuplicateFields") = value
			End Set
		End Property

		<Global.System.Configuration.UserScopedSettingAttribute(), Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.Configuration.DefaultSettingValueAttribute("False")> _
		Public Property RecoverFromBinaryData() As Boolean
			Get
				Return (CBool(Me("RecoverFromBinaryData")))
			End Get
			Set(ByVal value As Boolean)
				Me("RecoverFromBinaryData") = value
			End Set
		End Property

		<Global.System.Configuration.UserScopedSettingAttribute(), Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.Configuration.DefaultSettingValueAttribute("False")> _
		Public Property LeaveInvalidRecordsUnvalidated() As Boolean
			Get
				Return (CBool(Me("LeaveInvalidRecordsUnvalidated")))
			End Get
			Set(ByVal value As Boolean)
				Me("LeaveInvalidRecordsUnvalidated") = value
			End Set
		End Property

		<Global.System.Configuration.UserScopedSettingAttribute(), Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.Configuration.DefaultSettingValueAttribute("")> _
		Public Property LastValidateDirectory() As String
			Get
				Return (CStr(Me("LastValidateDirectory")))
			End Get
			Set(ByVal value As String)
				Me("LastValidateDirectory") = value
			End Set
		End Property
	End Class
End Namespace
