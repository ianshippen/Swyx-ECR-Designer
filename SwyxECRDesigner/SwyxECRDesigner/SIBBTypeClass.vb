Public Class SIBBTypeClass
    Enum SIBBTYPE
        NULL
        TODDOW
        PA
        CONNECT
        GET_DTMF_DIGIT
        GET_DTMF_STRING
        VOICEMAIL
        LONGEST_WAITING_DIST
        GROUP_AVAILABLE
        HOLD
        PAUSE
        SLEEP
        END_CALL
        DAY_OF_WEEK
        TIME_OF_DAY
        START
        DONE
        SKIP
        ON_DISCONNECT
        VBSCRIPT
        HOLIDAY
        ADD_CALL_TO_QUEUE
        TOP_OF_QUEUE
        CHANGE_QUEUE_STATE
        QUEUE_PAUSE
        GET_POSITION_IN_QUEUE
        REMOVE_CALL_FROM_QUEUE
        GET_CURRENT_TIME
        HAS_TIME_ELAPSED
        GET_LENGTH_OF_QUEUE
        FIRST_TIME
        LOG_POINT
        LOG_AGENT_STATUS
        ACTIVATE_CALL
        SEND_EMAIL
    End Enum

    Private type As SIBBTYPE
    Private propertiesForm As Form
    Private canDelete As Boolean
    Private typeName As String
    Private footerTitle As String
    Private outputNames As New List(Of String)

    Public Sub New()
        type = SIBBTYPE.NULL
        propertiesForm = Nothing
        canDelete = True
        typeName = ""
        footerTitle = ""
    End Sub

    Public Sub AddFixedOutputName(ByRef p As String)
        outputNames.Add(p)
    End Sub

    Public Function GetDisconnectOutputIndex() As Integer
        Dim result As Integer = -1

        For i = 0 To outputNames.Count - 1
            If outputNames(i) = DesignerForm.DISCONNECTED_OUPUT_NAME Then
                result = i
                Exit For
            End If
        Next

        Return result
    End Function

    Public Function GetXMLName() As String
        Return typeName
    End Function

    Public Sub SetFooterTitle(ByRef p As String)
        footerTitle = p
    End Sub

    Public Function GetFooterTitle() As String
        Return footerTitle
    End Function

    Public Sub SetTypeName(ByRef p As String)
        typeName = p
    End Sub

    Public Function GetTypeName() As String
        Return typeName
    End Function

    Public Sub SetPropertiesForm(ByRef p_form As Form)
        propertiesForm = p_form
    End Sub

    Public Function GetPropertiesForm() As Form
        Return propertiesForm
    End Function

    Public Sub SetSIBBType(ByVal p As SIBBTypeClass.SIBBTYPE)
        type = p
    End Sub

    Public Function GetSIBBType() As SIBBTypeClass.SIBBTYPE
        Return type
    End Function

    Public Sub SetCanDelete(ByVal p As Boolean)
        canDelete = p
    End Sub

    Public Function GetCanDelete() As Boolean
        Return canDelete
    End Function

    Public Function GetNumberOfOutputNames() As Integer
        Return outputNames.Count
    End Function

    Public Function GetOutputName(ByVal p_index) As String
        Return outputNames(p_index)
    End Function
End Class
