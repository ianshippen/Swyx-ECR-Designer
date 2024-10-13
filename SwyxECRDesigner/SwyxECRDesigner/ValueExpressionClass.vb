Public Class ValueExpressionClass
    Private description As String
    Public variables As New Dictionary(Of String, String)

    Public Sub New(ByRef p_description As String)
        description = p_description
    End Sub

    Public Function GetDescription() As String
        Return description
    End Function

    Public Sub SetDescription(ByRef p As String)
        description = p
    End Sub
End Class
