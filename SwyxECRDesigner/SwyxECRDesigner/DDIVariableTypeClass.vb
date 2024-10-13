Public Class DDIVariableTypeClass
    Private m_key As String
    Private m_description As String
    Private m_swyxCommand As String

    Public Sub New(ByRef p_key As String, ByRef p_swyxCommand As String, ByRef p_description As String)
        m_key = p_key
        m_swyxCommand = p_swyxCommand
        m_description = p_description
    End Sub

    Public Function GetDescription() As String
        Return m_description
    End Function

    Public Function GetKey() As String
        Return m_key
    End Function

    Public Function GetSwyxCommand() As String
        Return m_swyxCommand
    End Function
End Class
