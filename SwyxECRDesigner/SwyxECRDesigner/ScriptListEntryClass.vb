Public Class ScriptListEntryClass
    Public myLineNumber As Integer = -1
    Public myCode As String = ""

    Public Sub New(ByVal p_lineNumber As Integer, ByRef p_code As String)
        myLineNumber = p_lineNumber
        myCode = p_code
    End Sub
End Class
