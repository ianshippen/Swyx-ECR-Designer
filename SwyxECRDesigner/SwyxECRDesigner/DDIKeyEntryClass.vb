Public Class DDIKeyEntryClass
    Private swyxVariableExpression As String
    Private displayedExpression As String
    Public matchValues As New Dictionary(Of String, ValueExpressionClass)

    Public Sub New(ByVal p_ddiKeyType As String)
        swyxVariableExpression = ""
        displayedExpression = ""
    End Sub

    Public Function GetSwyxVariableExpression() As String
        Return swyxVariableExpression
    End Function

    Public Sub SetSwyxVariableExpression(ByRef p As String)
        swyxVariableExpression = p
    End Sub

    Public Function GetDisplayedExpression() As String
        Return displayedExpression
    End Function

    Public Sub SetDisplayedExpression(ByRef p As String)
        displayedExpression = p
    End Sub
End Class
