Public Class TestForm
    Public Const VARIABLES_DICTIONARY_NAME As String = "variablesDictionary"

    Private variablesDictionary As New Dictionary(Of String, String)

    Public Sub ClearDictionary()
        variablesDictionary.Clear()
    End Sub

    Public Function SafeGetItem(ByRef p_key As String) As String
        Dim result As String = "** Not Found **"

        If p_key = "" Then
            result = "** Empty Key **"
        Else
            If variablesDictionary.ContainsKey(p_key) Then result = variablesDictionary.Item(p_key)
        End If

        Return result
    End Function

    ' If variable exists then sets it to the new value, otherwise creates it with this value
    Public Sub SetVar(ByRef p_key, ByRef p_value)
        If variablesDictionary.ContainsKey(p_key) Then
            variablesDictionary.Item(p_key) = p_value
        Else
            variablesDictionary.Add(p_key, p_value)
        End If
    End Sub

    ' If variable exists then does nothing, otherwise creates it with this value
    Public Sub AddVar(ByRef p_key, ByRef p_value)
        If Not variablesDictionary.ContainsKey(p_key) Then variablesDictionary.Add(p_key, p_value)
    End Sub

    Public Function IsVar(ByRef p_key, ByRef p_value)
        Dim result : result = False

        If variablesDictionary.ContainsKey(p_key) Then
            If variablesDictionary.Item(p_key) = p_value Then result = True
        End If

        Return result
    End Function

    Function IsVarTrue(ByRef p_key)
        Dim result : result = False

        If variablesDictionary.ContainsKey(p_key) Then
            If LCase(variablesDictionary.Item(p_key)) = "true" Then result = True
        End If

        Return result
    End Function
End Class