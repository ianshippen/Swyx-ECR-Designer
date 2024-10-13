Public Class DebugForm
    Enum DebugModeEnum
        NULL_MODE
        TRACE_MODE
        DEBUG_MODE
        TEST_MODE
    End Enum

    Public DebugMode As DebugModeEnum

    Private replayIndex As Integer = -1
    Private replayTable As New DataTable
    Private myToolTip As ToolTip = Nothing
    Private replayCallId As Integer = -1
    Private myStartTime As String = ""
    Private oldDesignerFormHeadingText As String = ""

    Public Function ScanEventTable(ByRef p_tableDest As DataTable, ByVal p_callId As Integer) As Boolean
        Dim result As Boolean = False
        Dim mySQL As String = "exec sp_scaneventtable " & p_callId

        replayCallId = -1

        If FillTableFromCommand(CreateConnectionString(Form1.settingsConfigDictionary), mySQL, p_tableDest) Then
            result = True

            If p_tableDest.Rows.Count = 0 Then
                MsgBox("No rows returned when scanning the event table - was the Call Id correct ?")
            Else
                replayCallId = CInt(p_tableDest.Rows(0).Item("callId"))
            End If
        Else
            MsgBox("Error in running stored procedure: SP_ScanEventTable. Please make sure it is installed")
        End If

        Return result
    End Function

    Private Function FillReplayTable(ByVal p_callId As Integer) As Boolean
        replayTable = New DataTable

        Return ScanEventTable(replayTable, p_callId)
    End Function

    Private Function ConvertTimestamp(ByRef p_table As DataTable, ByVal p_rowIndex As Integer, ByVal p_asIso As Boolean) As String
        Dim myTimestamp As String = ""

        With p_table.Rows(p_rowIndex)
            Dim myDateTime As DateTime = Convert.ToDateTime(CDate(.Item("timestamp")))
            Dim msString As String = myDateTime.Millisecond

            'MsgBox(myDateTime.ToString)
            myTimestamp = .Item("timestamp")
            'myTimestamp = "5/4/2019 8:13:48 PM.320"

            ' Fix for Ecourier new system not localising
            'If myTimestamp.EndsWith("M") Then
            ' Convert from "5/4/2019 8:13:48 PM" type of format
            myTimestamp = ""

            If myDateTime.Day < 10 Then myTimestamp &= "0"

            myTimestamp &= myDateTime.Day & "/"

            If myDateTime.Month < 10 Then myTimestamp &= "0"

            myTimestamp &= myDateTime.Month & "/" & myDateTime.Year & " "

            If myDateTime.Hour < 10 Then myTimestamp &= "0"

            myTimestamp &= myDateTime.Hour & ":"

            If myDateTime.Minute < 10 Then myTimestamp &= "0"

            myTimestamp &= myDateTime.Minute & ":"

            If myDateTime.Second < 10 Then myTimestamp &= "0"

            myTimestamp &= myDateTime.Second
            'End If

            If p_asIso Then
                If myTimestamp(2) = "/" Then
                    myTimestamp = myTimestamp.Substring(6, 4) & "-" & myTimestamp.Substring(3, 2) & "-" & myTimestamp.Substring(0, 2) & myTimestamp.Substring(10)
                End If
            End If

            While msString.Length < 3
                msString = "0" & msString
            End While

            myTimestamp &= "." & msString
        End With

        Return myTimestamp
    End Function

    Private Function GetReplayData(ByVal p_index As Integer, ByRef p_node As Integer, ByRef p_output As Integer, ByRef p_data As String, ByRef p_timeStamp As String, ByRef p_timeRangeStart As String, ByRef p_timeRangeEnd As String, ByRef p_sourceData As String, ByRef p_nodeTypeName As String) As Boolean
        Dim result As Boolean = False

        If p_index >= 0 And p_index < replayTable.Rows.Count Then
            With replayTable.Rows(p_index)
                p_node = CInt(.Item("node"))
                p_data = ""
                p_sourceData = ""

                If Not .Item("data") Is DBNull.Value Then p_data = .Item("data")
                If Not .Item(8) Is DBNull.Value Then p_sourceData = .Item(8)

                p_output = 0

                If .Item("output") IsNot DBNull.Value Then p_output = CInt(.Item("output")) + 1

                p_timeStamp = ConvertTimestamp(replayTable, p_index, False)

                If p_index Mod 2 = 0 Then
                    p_timeRangeStart = ConvertTimestamp(replayTable, p_index, True)

                    If p_index + 1 < replayTable.Rows.Count Then p_timeRangeEnd = ConvertTimestamp(replayTable, p_index + 1, True)
                Else
                    If p_index - 1 < replayTable.Rows.Count Then p_timeRangeStart = ConvertTimestamp(replayTable, p_index - 1, True)

                    p_timeRangeEnd = ConvertTimestamp(replayTable, p_index, True)
                End If

                If Not .Item("nodeType") Is DBNull.Value Then p_nodeTypeName = .Item("nodeType")
                result = True
            End With
        End If

        Return result
    End Function

    Public Sub ReplayStepHandler(ByVal p_direction As Integer)
        Dim mySIBBIndex, myOutput As Integer
        Dim myTimestamp As String = ""
        Dim myData As String = ""
        Dim showData As Boolean = True
        Dim myStart As String = ""
        Dim myEnd As String = ""
        Dim mySourceData As String = ""
        Dim myNodeTypeName As String = ""

        ' Check if we are in reset mode
        If replayIndex = -1 Then
            ' Yes, ask the user for a callid
            If ShowDialog() = Windows.Forms.DialogResult.OK Then
                Dim myCallId As Integer = 0

                If IsInteger(callIdComboBox.Text) Then
                    myCallId = CInt(callIdComboBox.Text)

                    If Not callIdComboBox.Items.Contains(callIdComboBox.Text) Then callIdComboBox.Items.Add(callIdComboBox.Text)
                End If

                FillReplayTable(myCallId)
                oldDesignerFormHeadingText = DesignerForm.Text
            End If
        End If

        ' Calculate the replayIndex from its current value plus direction
        replayIndex += p_direction

        ' Unselect any selected node
        UnselectAnySelectedNodes()

        If GetReplayData(replayIndex, mySIBBIndex, myOutput, myData, myTimestamp, myStart, myEnd, mySourceData, myNodeTypeName) Then
            With sibbList(mySIBBIndex)
                Dim mySql As String = "select convert(varchar, StartTime, 120) from IpPbxCDR where CallId = " & replayCallId
                Dim showMyData As Boolean = True
                Dim myTable As New DataTable
                Dim myToolTipCaption As String = ""
                Dim swyxDatabaseName As String = Form1.settingsConfigDictionary.GetItem([Enum].GetName(GetType(Form1.OurConfigItems), Form1.OurConfigItems.swyxDatabaseName))
                Dim targetList As New List(Of String)
                Dim myFooter As String = .GetFooterTitle
                Dim cdrStartTime As String = ""

                If FillTableFromCommand(CreateConnectionString(Form1.settingsConfigDictionary), mySql, myTable) Then
                    If myTable.Rows.Count > 0 Then
                        With myTable.Rows(0)
                            If Not .Item(0) Is DBNull.Value Then cdrStartTime = .Item(0)
                        End With
                    End If
                End If

                If cdrStartTime = "" Then
                    MsgBox("No CDR found for callId = " & replayCallId)

                    Return
                End If

                myTable = New DataTable

                If myFooter = "Start" Then myToolTipCaption = "Start Time = " & cdrStartTime & vbCrLf

                Dim myTimeString As String = myTimestamp.Substring(myTimestamp.IndexOf(" ") + 1)

                If myTimeString.Contains(".") Then myTimeString = myTimeString.Substring(0, myTimeString.IndexOf("."))

                myToolTipCaption &= "Node Type = " & myNodeTypeName & ", Node Index = " & mySIBBIndex & vbCrLf
                myToolTipCaption &= "Time = " & myTimeString

                Dim myElapsedSeconds As Integer = DateDiff(DateInterval.Second, CDate(cdrStartTime), CDate(myTimestamp))
                Dim ts As TimeSpan = TimeSpan.FromSeconds(myElapsedSeconds)
                Dim mydate As DateTime = New DateTime(ts.Ticks)

                myToolTipCaption &= ", Call Duration = " & mydate.ToString(("HH:mm:ss"))

                mySql = "select timestamp, dbo.MapUserStatus(userState) as [Status], name as [Name], target from AgentDataMasterTable as a "
                mySql &= "left join AgentDataTable as b on a.myKey = b.myKey "
                mySql &= "left join [" & swyxDatabaseName & "].[dbo].[Users] as c on b.UserId = c.UserId "
                'mySql &= " left join ServiceBuilderEventTable as d on "
                mySql &= "where callId = " & replayCallId & " and timeStamp >= " & WrapInSingleQuotes(myStart) & " and timeStamp <= " & WrapInSingleQuotes(myEnd)
                mySql &= " and b.myKey IS NOT NULL"
                mySql &= " order by timestamp"

                If replayIndex = 0 Then myStartTime = myTimestamp

                myElapsedSeconds = DateDiff(DateInterval.Second, CDate(myStartTime), CDate(myTimestamp))
                ts = TimeSpan.FromSeconds(myElapsedSeconds)
                mydate = New DateTime(ts.Ticks)

                myToolTipCaption &= ", Script Duration = " & mydate.ToString(("HH:mm:ss"))

                If replayIndex Mod 2 = 1 Then mySql &= " desc"

                mySql &= ",[Status], c.Name"

                If DebugMode = DebugModeEnum.DEBUG_MODE And myOutput = 0 Then
                    sibbList(mySIBBIndex).DebugStepSelect(True)
                Else
                    sibbList(mySIBBIndex).DebugSelect(True, myOutput - 1)
                End If

                Select Case .GetTypeName
                    Case "SIBB_Start", "SIBB_VBScript"
                        showMyData = False

                    Case "SIBB_GroupAvailable", "SIBB_TimeOfDay", "SIBB_Hold", "SIBB_PlayAnnouncement", "SIBB_LongestWaiting"
                        myToolTipCaption &= vbCrLf & vbCrLf & "Data in = " & mySourceData
                        myToolTipCaption &= vbCrLf & "Data Resolved = " & myData
                End Select

                If showData Then
                    DesignerForm.Text = "Debug: " & "Type = " & .GetTypeName & "   Title = " & .GetNodeTitle

                    If showMyData Then DesignerForm.Text &= "   Data = " & WrapInQuotes(myData)

                    DesignerForm.Text &= "   Timestamp = " & myTimestamp
                End If

                If FillTableFromCommand(CreateConnectionString(Form1.settingsConfigDictionary), mySql, myTable) Then
                    Dim lastStatus As String = ""
                    Dim nameCount As Integer = 0
                    Dim lineLength As Integer = 0
                    Dim lastName As String = ""
                    Dim results As New Dictionary(Of String, List(Of String))
                    Dim statusList As New List(Of String)
                    Dim nameList As New List(Of String)

                    ' Get all the targets for this call id and time period
                    For i = 0 To myTable.Rows.Count - 1
                        With myTable.Rows(i)
                            Dim currentStatus As String = ""
                            Dim currentName As String = ""

                            If .Item("Status") IsNot DBNull.Value Then currentStatus = .Item("Status")
                            If .Item("Name") IsNot DBNull.Value Then currentName = .Item("Name")

                            If currentStatus <> "" And currentName <> "" Then
                                If Not nameList.Contains(currentName) Then
                                    nameList.Add(currentName)

                                    If results.ContainsKey(currentStatus) Then
                                        With results.Item(currentStatus)
                                            If Not .Contains(currentName) Then .Add(currentName)
                                        End With
                                    Else
                                        results.Add(currentStatus, New List(Of String))
                                        results.Item(currentStatus).Add(currentName)
                                        statusList.Add(currentStatus)
                                    End If
                                End If
                            End If

                            If .Item("target") IsNot DBNull.Value Then
                                Dim myTarget As String = .Item("target")

                                If myTarget <> "" Then
                                    If Not targetList.Contains(myTarget) Then targetList.Add(myTarget)
                                End If
                            End If
                        End With
                    Next

                    targetList.Sort()
                    statusList.Sort()

                    Dim targetString As String = ""

                    If targetList.Count > 0 Then
                        For i = 0 To targetList.Count - 1
                            If targetString <> "" Then targetString &= ", "

                            targetString &= targetList(i)
                        Next

                        targetString = "Target = [ " & targetString & " ]"
                    End If

                    If targetString <> "" Then myToolTipCaption &= vbCrLf & vbCrLf & targetString

                    Dim firstAgentStatusRow As Boolean = True

                    For j = 0 To statusList.Count - 1
                        Dim mystatus As String = statusList(j)

                        If firstAgentStatusRow Then
                            myToolTipCaption &= vbCrLf & vbCrLf
                            firstAgentStatusRow = False
                        End If

                        myToolTipCaption &= "["
                        lineLength = 0

                        With results.Item(mystatus)
                            .Sort()

                            For i = 0 To .Count - 1
                                If i > 0 Then myToolTipCaption &= ","

                                If lineLength > 80 Then
                                    lineLength = 0
                                    myToolTipCaption &= vbCrLf
                                Else
                                    myToolTipCaption &= " "
                                End If

                                myToolTipCaption &= .Item(i)
                                lineLength += .Item(i).Length
                            Next

                            myToolTipCaption &= " ] "

                            If .Count > 1 Then
                                myToolTipCaption &= "are "
                            Else
                                myToolTipCaption &= "is "
                            End If

                            myToolTipCaption &= mystatus
                        End With

                        If j < statusList.Count - 1 Then myToolTipCaption &= vbCrLf
                    Next
                End If

                ' Expand empty info into whitespace to allow the bubble to appear if no info
                If myToolTipCaption = "" Then myToolTipCaption = " "

                Dim xOffset As Integer = (replayIndex Mod 2) * GetSIBBWidth()
                Dim a As New AbsoluteLocationClass(sibbList(mySIBBIndex).GetXPos + xOffset, sibbList(mySIBBIndex).GetYPos)
                Dim z As ScreenLocationClass = AbsoluteToScreenPosition(a)
                Dim myToolTipTitle As String = "Time = " & myTimestamp.Substring(myTimestamp.IndexOf(" ") + 1)

                myElapsedSeconds = DateDiff(DateInterval.Second, CDate(myStartTime), CDate(myTimestamp))
                ts = TimeSpan.FromSeconds(myElapsedSeconds)
                mydate = New DateTime(ts.Ticks)

                If myToolTipTitle.Contains(".") Then myToolTipTitle = myToolTipTitle.Substring(0, myToolTipTitle.IndexOf("."))

                myToolTipTitle &= ", Duration = " & mydate.ToString(("HH:mm:ss"))

                If myFooter = "Start" Then myToolTipCaption &= vbCrLf & "Call Id = " & replayCallId

                If myToolTip Is Nothing Then
                    ' No tool tip - create it and display it
                    myToolTip = New ToolTip

                    With myToolTip
                        .IsBalloon = True
                        '.ToolTipTitle = myToolTipTitle
                        '.ToolTipIcon = ToolTipIcon.Info
                        .Show(myToolTipCaption, DesignerForm, z.GetX, z.GetY)
                        .Tag = mySIBBIndex
                        .ForeColor = Color.FromArgb(64, 64, 64)
                    End With
                Else
                    ' Is the tag for this sibb ?
                    'If myToolTip.Tag = sibbIndex Then
                    If False Then
                        ' Yes - keep it displayed but update the timestamp
                        myToolTip.ToolTipTitle = myToolTipTitle
                    Else
                        ' No. Remove it and redisplay it
                        RemoveToolTip()
                        myToolTip = New ToolTip

                        With myToolTip
                            .IsBalloon = True
                            '.ToolTipTitle = myToolTipTitle
                            '.ToolTipIcon = ToolTipIcon.Info
                            .Show(myToolTipCaption, DesignerForm, z.GetX, z.GetY)
                            .Tag = mySIBBIndex
                            .ForeColor = Color.FromArgb(64, 64, 64)
                        End With
                    End If
                End If

            End With
        Else
            replayIndex = -1
        End If

        MyRefresh()
    End Sub

    Public Sub UnselectAnySelectedNodes()
        For i = 0 To sibbList.Count - 1
            With sibbList(i)
                If .IsDebugSelected Then
                    .DebugSelect(False)
                    Exit For
                End If

                If .IsDebugHistorySelected Then
                    .DebugHistorySelect(False)
                End If

                If .IsStepDebugSelected Then
                    .DebugStepSelect(False)
                    Exit For
                End If

                For j = 0 To .outputs.Count - 1
                    .outputs(j).debugSelected = False
                Next
            End With
        Next
    End Sub

    Public Sub RemoveToolTip()
        If myToolTip IsNot Nothing Then
            myToolTip.Dispose()
            myToolTip = Nothing
        End If
    End Sub

    Private Sub DebugForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        callIdComboBox.Text = "Last Call"
        myToolTip = Nothing
        replayCallId = -1
        myStartTime = ""
    End Sub

    Public Sub ResetDebug()
        replayIndex = -1
        replayCallId = -1
        myStartTime = ""
        DesignerForm.Text = oldDesignerFormHeadingText
    End Sub
End Class