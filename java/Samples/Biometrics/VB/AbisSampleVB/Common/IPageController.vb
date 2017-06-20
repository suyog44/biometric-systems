Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text

Public Interface IPageController
	Sub NavigateToPage(ByVal pageType As Type, ByVal navigationParam As Object, ParamArray ByVal arguments() As Object)
	Sub NavigateToStartPage()
	Property TabController() As ITabController
End Interface
