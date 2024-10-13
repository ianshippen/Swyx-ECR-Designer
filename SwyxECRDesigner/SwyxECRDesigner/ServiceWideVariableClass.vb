Public Class ServiceWideVariableClass
    Private name As String
    Private value As String
    Private type As DesignerForm.VariableTypes
    Private deletable As Boolean
    Private allowEmptyString As Boolean
    Private sanityCheckForNonEmpty As Boolean

    Public Sub New()
        name = ""
        value = ""
        type = DesignerForm.VariableTypes.NULL
        deletable = True
        allowEmptyString = False
        sanityCheckForNonEmpty = False
    End Sub

    Public Function GetName() As String
        Return name
    End Function

    Public Sub SetName(ByRef p_name)
        name = p_name
    End Sub

    Public Function GetValue() As String
        Return value
    End Function

    Public Sub SetValue(ByRef p_value)
        value = p_value
    End Sub

    Public Function GetMyType() As String
        Return type
    End Function

    Public Sub SetMyType(ByRef p_type As DesignerForm.VariableTypes)
        type = p_type
    End Sub

    Public Sub SetMyType(ByRef p_typeString As String)
        type = DesignerForm.GetVariableTypeFromName(p_typeString)
    End Sub

    Public Function GetDeletable() As Boolean
        Return deletable
    End Function

    Public Sub SetDeletable(ByVal p_deletable As Boolean)
        deletable = p_deletable
    End Sub

    Public Function GetAllowEmptyString() As Boolean
        Return allowEmptyString
    End Function

    Public Sub SetAllowEmptyString(ByVal p_allowEmptyString As Boolean)
        allowEmptyString = p_allowEmptyString
    End Sub

    Public Function GetSanityCheckForNonEmpty() As Boolean
        Return sanityCheckForNonEmpty
    End Function

    Public Sub SetSanityCheckForNonEmpty(ByVal p_sanityCheckForNonEmpty As Boolean)
        sanityCheckForNonEmpty = p_sanityCheckForNonEmpty
    End Sub

    Public Function GetFlags() As Integer
        Dim flags As Integer = 0

        If deletable Then flags += 1
        If allowEmptyString Then flags += 2
        If sanityCheckForNonEmpty Then flags += 4

        Return flags
    End Function

    Public Sub SetFlags(ByVal p_flags As Integer)
        deletable = False
        allowEmptyString = False
        sanityCheckForNonEmpty = False

        If (p_flags And 1) = 1 Then deletable = True
        If (p_flags And 2) = 2 Then allowEmptyString = True
        If (p_flags And 4) = 4 Then sanityCheckForNonEmpty = True
    End Sub
End Class
