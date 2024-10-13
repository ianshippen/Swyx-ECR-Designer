Public Class CDRClass
    Private readyToWrite As Boolean

    Public Sub New()
        readyToWrite = False
        callId = 0
    End Sub

    Protected Overrides Sub Finalize()
        If readyToWrite Then
            WriteToDatabase()
        End If
    End Sub

    Public Function GenerateNextCallId() As Integer
        Dim myTable As New DataTable

        ' Get the next Call Id value in the sequence
        If FillTableFromCommand(CreateConnectionString(Form1.settingsConfigDictionary), "select IsNull(max(abs(callId)), 0) + 1 from IpPbxCDR", myTable) Then
            If myTable.Rows.Count > 0 Then
                With myTable.Rows(0)
                    If .Item(0) IsNot DBNull.Value Then
                        If IsInteger(.Item(0)) Then
                            callId = CInt(.Item(0))
                        End If
                    End If
                End With
            End If
        End If

        ' Create a new CDR record with a negative Call Id
        callId = -callId
        WriteToDatabaseOld()
        callId = Math.Abs(callId)

        readyToWrite = True

        Return callId
    End Function

    Public Function GenerateNextCallIdOld() As Integer
        Dim myTable As New DataTable
        Dim result As Integer = 0

        callId = 0

        ' Is there a CDR record for callid = 0 ?
        If FillTableFromCommand(CreateConnectionString(Form1.settingsConfigDictionary), "select count(*) from IpPbxCDR where CallId = 0", myTable) Then
            If myTable.Rows.Count > 0 Then
                With myTable.Rows(0)
                    If Not .Item(0) Is DBNull.Value Then
                        If IsInteger(.Item(0)) Then
                            If CInt(.Item(0)) = 1 Then
                                ' Yes - delete the existing record and insert a new empty record with Call Id = 0
                                ExecuteNonQuery(CreateConnectionString(Form1.settingsConfigDictionary), "delete from IpPbxCDR where CallId = 0")
                            End If

                            ' No - insert a new empty record with Call Id = 0
                            WriteToDatabaseOld()
                        End If
                    End If
                End With
            End If
        End If

        myTable = New DataTable

        If FillTableFromCommand(CreateConnectionString(Form1.settingsConfigDictionary), "select IsNull(max(callId), 0) + 1 from IpPbxCDR", myTable) Then
            If myTable.Rows.Count > 0 Then
                With myTable.Rows(0)
                    If .Item(0) IsNot DBNull.Value Then
                        If IsInteger(.Item(0)) Then
                            result = CInt(.Item(0))
                        End If
                    End If
                End With
            End If
        End If

        callId = result
        readyToWrite = True

        Return result
    End Function

    Public Sub SetCalledNumber(ByRef p As String)
        Dim mySql As String = "update IpPbxCDR set CalledNumber = " & WrapInSingleQuotes(p) & " where CallId = " & (-callId)

        ExecuteNonQuery(CreateConnectionString(Form1.settingsConfigDictionary), mySql)
    End Sub

    Public Sub SetStartTIme()
        Dim mySql As String = "update IpPbxCDR set StartTime = " & CheckForNullDate(Now) & " where CallId = " & (-callId)

        ExecuteNonQuery(CreateConnectionString(Form1.settingsConfigDictionary), mySql)
    End Sub

    Public Sub SetEndTime()
        Dim mySql As String = "update IpPbxCDR set EndTime = " & CheckForNullDate(Now) & " where CallId = " & (-callId)

        ExecuteNonQuery(CreateConnectionString(Form1.settingsConfigDictionary), mySql)
    End Sub

    Public Sub SetOrig(ByRef p_extension As String, ByRef p_userName As String)
        Dim mySql As String = "update IpPbxCDR set OriginationNumber = " & WrapInSingleQuotes(p_extension) & ", OriginationName = " & WrapInSingleQuotes(p_userName) & " where CallId = " & (-callId)

        ExecuteNonQuery(CreateConnectionString(Form1.settingsConfigDictionary), mySql)
    End Sub

    Public Sub WriteToDatabase()
        Dim mySql As String = "update IpPbxCDR set CallId = " & callId & " where CallId = " & (-callId)

        ExecuteNonQuery(CreateConnectionString(Form1.settingsConfigDictionary), mySql)

        'MsgBox("CDR with CallId = " & callId & " has been written ..")
        'MyMsgBoxForm.TextBox1.Text = "CDR with CallId = " & callId & " has been written .."
        'MyMsgBoxForm.Show()
        mySimulatorFormRef.UpdateMessageDisplay("CDR with CallId = " & callId & " has been written ..")
    End Sub

    Public Sub WriteToDatabaseOld()
        Dim mySql As String = "insert into IpPbxCDR values (" & callId & ", " & WrapInSingleQuotes(OriginationNumber) & ", " & WrapInSingleQuotes(OriginationName)

        mySql &= ", " & WrapInSingleQuotes(CalledNumber) & ", " & WrapInSingleQuotes(CalledName) & ", " & WrapInSingleQuotes(DestinationNumber) & ", " & WrapInSingleQuotes(DestinationName)
        mySql &= ", " & CheckForNullDate(StartTime)
        mySql &= ", " & CheckForNullDate(ScriptConnectTime)
        mySql &= ", " & CheckForNullDate(DeliveredTime)
        mySql &= ", " & CheckForNullDate(ConnectTime)
        mySql &= ", " & CheckForNullDate(EndTime)
        mySql &= ", " & WrapInSingleQuotes(Currency)
        mySql &= ", " & WrapInSingleQuotes(Costs)
        mySql &= ", " & WrapInSingleQuotes(State)
        mySql &= ", " & WrapInSingleQuotes(PublicAccessPrefix)
        mySql &= ", " & WrapInSingleQuotes(LCRProvider)
        mySql &= ", " & WrapInSingleQuotes(ProjectNumber)
        mySql &= ", " & ConvertBoolToDatabaseFormat(AOC)
        mySql &= ", " & WrapInSingleQuotes(OriginationDevice)
        mySql &= ", " & WrapInSingleQuotes(DestinationDevice)
        mySql &= ", " & WrapInSingleQuotes(TransferredByNumber)
        mySql &= ", " & WrapInSingleQuotes(TransferredByName)
        mySql &= ", " & TransferredCallId1
        mySql &= ", " & TransferredCallId2
        mySql &= ", " & TransferredToCallId
        mySql &= ", " & CheckForNullDate(TransferTime)
        mySql &= ", " & WrapInSingleQuotes(DisconnectReason)
        mySql &= ")"

        ExecuteNonQuery(CreateConnectionString(Form1.settingsConfigDictionary), mySql)
        readyToWrite = False
    End Sub

    Private Function CheckForNullDate(ByRef p As Date) As String
        Dim result As String = "NULL"

        If p.Year > 1 Then
            result = p.Year & "-"

            If p.Month < 10 Then result &= "0"

            result &= p.Month & "-"

            If p.Day < 10 Then result &= "0"

            result &= p.Day & " " & p.ToString.Substring(p.ToString.Length - 8)
            result = WrapInSingleQuotes(result)
        End If

        Return result
    End Function

    Private Function ConvertBoolToDatabaseFormat(ByVal p As Boolean) As String
        Dim result As Integer = 0

        If p Then result = 1

        Return result
    End Function

    Private callId As Integer = 0
    Private OriginationNumber As String = ""
    Private OriginationName As String = ""
    Private CalledNumber As String = ""
    Private CalledName As String = ""
    Private DestinationNumber As String = ""
    Private DestinationName As String = ""
    Private StartTime As Date = Nothing
    Private ScriptConnectTime As Date = Nothing
    Private DeliveredTime As Date = Nothing
    Private ConnectTime As Date = Nothing
    Private EndTime As Date = Nothing
    Private Currency As String = ""
    Private Costs As String = ""
    Private State As String = ""
    Private PublicAccessPrefix As String = ""
    Private LCRProvider As String = ""
    Private ProjectNumber As String = ""
    Private AOC As Boolean = False
    Private OriginationDevice As String = ""
    Private DestinationDevice As String = ""
    Private TransferredByNumber As String = ""
    Private TransferredByName As String = ""
    Private TransferredCallId1 As Integer = 0
    Private TransferredCallId2 As Integer = 0
    Private TransferredToCallId As Integer = 0
    Private TransferTime As Date = Nothing
    Private DisconnectReason As String = ""
End Class
