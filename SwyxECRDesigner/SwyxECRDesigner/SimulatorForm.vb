Imports System.Net.Sockets
Imports System.Xml, System.IO

Public Class SimulatorForm
    Const SIMULATOR_AGENT_TABLE_NAME As String = "SimulatorAgentTable"
    Const SIMULATOR_MEMBERSHIP_TABLE_NAME As String = "SimulatorMembershipTable"
    Const SIMULATOR_GROUP_TABLE_NAME As String = "SimulatorGroupTable"
    Const USING_SINGLE_SOCKET As Boolean = True

    Const MY_PATH As String = "C:\Documents and Settings\Ian\My Documents\Visual Studio 2008\Projects\SwyxECRDesigner\SwyxECRDesigner\bin\Debug\SimulatorFiles"

    Public myCDR As New CDRClass
    Private myClient As UdpClient = Nothing
    Private mySocket As Integer = 1234
    Private myReceiveThread As System.Threading.Thread
    Private myEndPoint As New System.Net.IPEndPoint(System.Net.IPAddress.Any, 0)
    Private Delegate Sub StringDelegate(ByRef p As String)
    Private allowAgentTimeoutUpdate As Boolean = False
    Private msgBoxColour As Color = Color.Blue

    Private Sub UdpInit()
        myClient = New UdpClient(mySocket)
        myReceiveThread = New System.Threading.Thread(AddressOf UDPReceiveMsgHander)
        myReceiveThread.Start()
    End Sub

    Private Sub UDPReceiveMsgHander()
        Try
            Dim myBuffer() As Byte = myClient.Receive(myEndPoint)

            HandleUdpMsg(myBuffer)
            myReceiveThread = New System.Threading.Thread(AddressOf UDPReceiveMsgHander)
            myReceiveThread.Start()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub HandleUdpMsg(ByRef p_data() As Byte)
        Dim myString As String = ""

        For i = 0 To p_data.Length - 1
            Dim x As Integer = p_data(i)

            If x <> 10 And x <> 13 Then
                myString &= Chr(x)
            End If
        Next

        If myString <> "" Then
            Dim myDoc As New XmlDocument
            Dim myRecord As XmlNode = Nothing
            Dim errorEncountered As Boolean = False

            Try
                myDoc.LoadXml(myString)
            Catch ex As Exception
                errorEncountered = True
                MsgBox(ex.Message)
            End Try

            If Not errorEncountered Then
                Dim operationName As String = ""
                Dim userName As String = ""
                Dim digits As String = ""
                Dim callingNumber As String = ""
                Dim callId As String = ""
                Dim status As String = ""

                ' Loop over each parameter
                For Each myRecord In myDoc("FromPhone")
                    Dim recordName As String = myRecord.Name
                    Dim recordData As String = ""

                    If myRecord.HasChildNodes Then
                        recordData = myRecord.FirstChild.Value

                        Select Case recordName.ToLower
                            Case "operation"
                                operationName = recordData

                            Case "username"
                                userName = recordData

                            Case "digits"
                                digits = recordData

                            Case "callingnumber"
                                callingNumber = recordData

                            Case "callid"
                                callId = recordData

                            Case "status"
                                status = recordData
                        End Select
                    End If
                Next

                Select Case operationName
                    Case "Login"
                        'LoginHandler(userName, callingNumber)

                    Case "Logout"
                        'LogoutHandler(userName)

                    Case "Dial"
                        DialHandler(userName, digits, callingNumber)

                    Case "Release"
                        ReleaseHandler(callId)

                    Case "Status_Changed"
                        'StatusChangedHandler(userName, status)
                End Select
            End If
        End If
    End Sub

    Public Sub UdpSendString(ByRef p_text As String, ByRef p_destIPAddress As String, ByVal p_port As Integer)
        Dim myArray(p_text.Length + 1) As Byte

        For i = 0 To p_text.Length - 1
            myArray(i) = Asc(p_text(i))
        Next

        myArray(p_text.Length) = 13
        myArray(p_text.Length + 1) = 10

        UdpSend(myArray, p_destIPAddress, p_port)
    End Sub

    Public Sub UdpSend(ByRef p_data() As Byte, ByRef p_destIPAddress As String, ByVal p_port As Integer)
        Dim myUdpClient As UdpClient = Nothing
        Dim myIPAddress As System.Net.IPAddress = System.Net.IPAddress.Parse(p_destIPAddress)

        Try
            myUdpClient = New UdpClient(mySocket + 1)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

        myUdpClient.Connect(myIPAddress, p_port)
        myUdpClient.Send(p_data, p_data.Length)
        myUdpClient.Close()
        myUdpClient = Nothing
        myIPAddress = Nothing
    End Sub

    Private Sub RunToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RunToolStripMenuItem.Click
        Dim x As String = startBlockTextBox.Text

        x &= vbCrLf & insertScriptCodeTextBox.Text

        x &= vbCrLf
        ' x &= vbCrLf & "If variablesDictionary.Exists("$returnCode") Then"
        x &= vbCrLf & "Else"

        x &= vbCrLf & "End If"
    End Sub

    Private Sub runButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles runButton.Click
        'BootstrapSimulator("Simulator Test Script")

        If startBlockListBox.SelectedIndex >= 0 Then
            RunSimulation()
        Else
            MsgBox("Please select a Start Block file")
        End If
    End Sub

    Private Sub RunSimulation()
        If ListBox1.SelectedItem IsNot Nothing Then
            Dim myScriptName As String = ListBox1.SelectedItem
            Dim myInsertScriptCodeName As String = ""

            If insertScriptCodeListBox.SelectedIndex >= 0 Then myInsertScriptCodeName = insertScriptCodeListBox.SelectedItem

            'RunScript("Bootstrap", "Bootstrap " & WrapInQuotes(myScriptName), myScriptName, "DevBase 3.0.txt", "InsertScriptCode.txt")
            'RunScript("Bootstrap", "Bootstrap " & WrapInQuotes(myScriptName), myScriptName, "EcourierBigBikesStartRule.txt", "EcourierBigBikesInsertScriptCode.txt")
            RunScript("Bootstrap", "Bootstrap " & WrapInQuotes(myScriptName), myScriptName, startBlockListBox.SelectedItem, myInsertScriptCodeName)
        Else
            MsgBox("Please select a script to run")
        End If
    End Sub

    Private Sub SimulatorForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim mySql As String = "select distinct(scriptName) from " & DesignerForm.SERVICEBUILDER_TABLE_NAME & " order by scriptName"
        Dim myTable As New DataTable

        mySimulatorFormRef = Me

        ListBox1.Items.Clear()

        If FillTableFromCommand(CreateConnectionString(Form1.settingsConfigDictionary), mySql, myTable) Then
            For i = 0 To myTable.Rows.Count - 1
                With myTable.Rows(i)
                    If .Item(0) IsNot DBNull.Value Then ListBox1.Items.Add(.Item(0))
                End With
            Next
        End If

        ' Get the available Start Block files
        Dim myFiles() As String = IO.Directory.GetFiles(MY_PATH, "*.txt")

        startBlockListBox.Items.Clear()

        For Each myFileName As String In myFiles
            startBlockListBox.Items.Add(myFileName.Substring(myFileName.LastIndexOf("\") + 1))
        Next

        ' Get the available Insert Script Block files
        myFiles = IO.Directory.GetFiles(MY_PATH, "*.txt")

        insertScriptCodeListBox.Items.Clear()

        For Each myFileName As String In myFiles
            insertScriptCodeListBox.Items.Add(myFileName.Substring(myFileName.LastIndexOf("\") + 1))
        Next

        targetGroupsListBox.Items.Clear()
        mySql = "select name from SimulatorGroupTable order by name"
        myTable = New DataTable

        If FillTableFromCommand(CreateConnectionString(Form1.settingsConfigDictionary), mySql, myTable) Then
            For i = 0 To myTable.Rows.Count - 1
                With myTable.Rows(i)
                    If .Item(0) IsNot DBNull.Value Then targetGroupsListBox.Items.Add(.Item(0))
                End With
            Next
        End If

        If targetGroupsListBox.Items.Count > 0 Then targetGroupsListBox.SelectedIndex = 0

        availableAgentsListBox.Items.Clear()
        mySql = "select name + ' ' + extension from SimulatorAgentTable order by name"
        myTable = New DataTable

        If FillTableFromCommand(CreateConnectionString(Form1.settingsConfigDictionary), mySql, myTable) Then
            For i = 0 To myTable.Rows.Count - 1
                With myTable.Rows(i)
                    If .Item(0) IsNot DBNull.Value Then availableAgentsListBox.Items.Add(.Item(0))
                End With
            Next
        End If

        UdpInit()

        'Dim myAgentStatusChangedThread As New Threading.Thread(AddressOf AgentStatusChangedThread)

        'myAgentStatusChangedThread.Start()
        'UdpSendString("hello")
    End Sub

    Dim myLocalClient As UdpClient = Nothing
    Dim myLocalReceiveThread As Threading.Thread = Nothing

    Private Sub AgentStatusChangedThread()
        Dim running As Boolean = True

        myLocalClient = New UdpClient(1111)
        myLocalReceiveThread = New System.Threading.Thread(AddressOf LocalUDPReceiveMsgHander)
        myLocalReceiveThread.Start()

        While running

        End While
    End Sub

    Private Sub LocalUDPReceiveMsgHander()
        Try
            Dim myBuffer() As Byte = myLocalClient.Receive(myEndPoint)

            'HandleUdpMsg(myBuffer)
            myLocalReceiveThread = New System.Threading.Thread(AddressOf LocalUDPReceiveMsgHander)
            myLocalReceiveThread.Start()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub ListBox1_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ListBox1.DoubleClick
        RunSimulation()
    End Sub

    Private Sub SimulatorForm_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        myClient.Close()
        myClient = Nothing
    End Sub

    Private Sub LoginHandler(ByRef p_userName As String, ByRef p_extension As String)
        ' Try and get an existing record for this user
        Dim myTable As New DataTable
        Dim mySql As String = "select * from " & SIMULATOR_AGENT_TABLE_NAME & " where name = " & WrapInSingleQuotes(p_userName)

        If FillTableFromCommand(CreateConnectionString(Form1.settingsConfigDictionary), mySql, myTable) Then
            If myTable.Rows.Count > 0 Then
                ' Update the existing entry in the table
                mySql = "update " & SIMULATOR_AGENT_TABLE_NAME & " set status = 2, ipAddress = " & WrapInSingleQuotes(myEndPoint.Address.ToString) & ", port = " & (myEndPoint.Port - 1) & " where name = " & WrapInSingleQuotes(p_userName)
            Else
                ' Make a new entry in the table
                mySql = "insert into " & SIMULATOR_AGENT_TABLE_NAME & " values (" & WrapInSingleQuotes(p_userName) & ", " & WrapInSingleQuotes(p_extension) & ", 2," & WrapInSingleQuotes(myEndPoint.Address.ToString) & ", " & (myEndPoint.Port - 1) & ")"
            End If

            If Not ExecuteNonQuery(CreateConnectionString(Form1.settingsConfigDictionary), mySql) Then
                MsgBox("Could not add/update login to " & SIMULATOR_AGENT_TABLE_NAME)
            End If
        End If

        Dim myXMLWriter As New XMLWriterClass
        Dim myNodes(10) As XmlElement

        myNodes(0) = myXMLWriter.AddChild(Nothing, "ToPhone")
        myXMLWriter.AddChild(myNodes(0), "Operation", "Login_Ack")
        myXMLWriter.AddChild(myNodes(0), "UserName", p_userName)

        UdpSendString(myXMLWriter.AsString, myEndPoint.Address.ToString, myEndPoint.Port - 1)
    End Sub

    Private Sub LogoutHandler(ByRef p_userName As String)
        Dim mySql As String = "update " & SIMULATOR_AGENT_TABLE_NAME & " set status = 1 where name = " & WrapInSingleQuotes(p_userName)
        Dim myTable As New DataTable

        ExecuteNonQuery(CreateConnectionString(Form1.settingsConfigDictionary), mySql)

        mySql = "select ipAddress, port from " & SIMULATOR_AGENT_TABLE_NAME & " where name = " & WrapInSingleQuotes(p_userName)

        If FillTableFromCommand(CreateConnectionString(Form1.settingsConfigDictionary), mySql, myTable) Then
            Dim myXMLWriter As New XMLWriterClass
            Dim myNodes(10) As XmlElement

            myNodes(0) = myXMLWriter.AddChild(Nothing, "ToPhone")
            myXMLWriter.AddChild(myNodes(0), "Operation", "Logout_Ack")
            myXMLWriter.AddChild(myNodes(0), "UserName", p_userName)

            UdpSendString(myXMLWriter.AsString, myTable.Rows(0).Item(0), myTable.Rows(0).Item(1))
        End If
    End Sub

    Private Sub DialHandler(ByRef p_userName As String, ByRef p_digits As String, ByRef p_callingNumber As String)
        Dim connected As Boolean = GenericDialHandler(0, p_digits, p_callingNumber, p_userName)
    End Sub

    Public Function GenericDialHandler(ByVal p_phoneIndex As Integer, ByRef p_calledNumber As String, ByRef p_callingNumber As String, ByRef p_userName As String) As Boolean
        Dim connected As Boolean = False
        Dim myTable As New DataTable

        If msgBoxColour = Color.Blue Then
            msgBoxColour = Color.Green
        Else
            msgBoxColour = Color.Blue
        End If

        UpdateMessageDisplay("Call Start: Originator = " & p_userName & " (" & p_callingNumber & "), Dialled Digits = " & p_calledNumber)

        With myCDR
            ' Insert this data into the VBScript file
            Devbase.UpdateCallingNumber(p_phoneIndex, p_callingNumber)
            Devbase.UpdateCalledNumber(p_phoneIndex, p_calledNumber)
            Devbase.UpdateOrigIPAddress(p_phoneIndex, myEndPoint.Address)
            Devbase.UpdateOrigIPPort(p_phoneIndex, myEndPoint.Port)

            Dim myCallId As Integer = .GenerateNextCallId() ' This will write a fresh CDR with a negative version of the Call Id. The value in myCallId is the real positive version

            Devbase.UpdateCallId(p_phoneIndex, myCallId)

            If Devbase.myCodeFileName <> "" Then Devbase.SaveMyCode(p_phoneIndex, Devbase.myCodeFileName)

            ' Set the OriginationNumber, OriginationName, CalledNumber, and StartTime in the negative callid version of the CDR
            .SetCalledNumber(p_calledNumber)
            .SetStartTIme()
            .SetOrig(p_callingNumber, p_userName)

            ' Send the call id back to the originating phone
            Dim myXMLWriter As New XMLWriterClass
            Dim myNodes(10) As XmlElement

            myNodes(0) = myXMLWriter.AddChild(Nothing, "ToPhone")
            myXMLWriter.AddChild(myNodes(0), "Operation", "Dial_Ack")
            myXMLWriter.AddChild(myNodes(0), "CallId", myCallId)

            Dim myDestPort As Integer = myEndPoint.Port - 1

            If USING_SINGLE_SOCKET Then myDestPort = myEndPoint.Port

            UdpSendString(myXMLWriter.AsString, myEndPoint.Address.ToString, myDestPort)

            Dim myErrorString As String = ExecuteGlobal(Devbase.myCodeArray(p_phoneIndex))

            ' Check if the call connected
            If FillTableFromCommand(CreateConnectionString(Form1.settingsConfigDictionary), "select ConnectTime from IpPbxCDR where callid = " & (-myCallId), myTable) Then
                If myTable.Rows.Count >= 0 Then
                    With myTable.Rows(0)
                        If .Item(0) IsNot DBNull.Value Then connected = True
                    End With
                End If
            End If

            If connected Then
            Else
                .SetEndTime()
                .WriteToDatabase()
            End If

            If myErrorString <> "" Then MsgBox(myErrorString)
        End With

        'MsgBox("VBScript completed for this call")
        UpdateMessageDisplay("VBScript completed for this call")

        Return connected
    End Function

    Private Sub ReleaseHandler(ByRef p_callId As String)
        If IsInteger(p_callId) Then
            Dim myCallId As Integer = CInt(p_callId)

            If myCallId >= 0 Then
                Dim mySql As String = "select * from IpPbxCDR where callid = " & (-myCallId)
                Dim myTable As New DataTable

                ' Check if there is a temporary CDR record in the database
                If FillTableFromCommand(CreateConnectionString(Form1.settingsConfigDictionary), mySql, myTable) Then
                    If myTable.Rows.Count > 0 Then
                        With myCDR
                            .SetEndTime()
                            .WriteToDatabase()
                        End With

                        With myTable.Rows(0)
                            If Not .Item("ConnectTime") Is DBNull.Value Then
                                Dim myDestName As String = ""

                                If Not .Item("DestinationName") Is DBNull.Value Then myDestName = .Item("DestinationName")

                                If myDestName <> "" Then
                                    ' Clear down the connected destination
                                    mySql = "update " & SIMULATOR_AGENT_TABLE_NAME & " set state = 2 where name = " & WrapInSingleQuotes(SingleQuoteCheck(myDestName))
                                    ExecuteNonQuery(CreateConnectionString(Form1.settingsConfigDictionary), mySql)

                                    Dim myXMLWriter As New XMLWriterClass
                                    Dim myNodes(10) As XmlElement

                                    mySql = "select ipAddress, port from SimulatorAgentTable where name = " & WrapInSingleQuotes(myDestName)
                                    myTable = New DataTable
                                    myNodes(0) = myXMLWriter.AddChild(Nothing, "ToPhone")
                                    myXMLWriter.AddChild(myNodes(0), "Operation", "Release")

                                    If FillTableFromCommand(CreateConnectionString(Form1.settingsConfigDictionary), mySql, myTable) Then
                                        If myTable.Rows.Count > 0 Then
                                            With myTable.Rows(0)
                                                UdpSendString(myXMLWriter.AsString, .Item("ipAddress"), .Item("port"))
                                            End With
                                        End If
                                    End If
                                End If
                            End If
                        End With
                    End If
                End If
            End If
        End If
    End Sub

    Private Sub StatusChangedHandler(ByRef p_agentName As String, ByRef p_status As String)
        If IsInteger(p_status) Then
            Dim myStatus As Integer = CInt(p_status)
            Dim mySql As String = "update " & SIMULATOR_AGENT_TABLE_NAME & " set status = " & myStatus & " where name = " & WrapInSingleQuotes(p_agentName)

            ExecuteNonQuery(CreateConnectionString(Form1.settingsConfigDictionary), mySql)
        End If
    End Sub

    Private Sub SendCallIdToOrigPhone(ByVal p_callId As Integer, ByRef p_userName As String)
        Dim myXMLWriter As New XMLWriterClass
        Dim myNodes(10) As XmlElement
        Dim myTable As New DataTable

        If FillTableFromCommand(CreateConnectionString(Form1.settingsConfigDictionary), "select ipAddress, port from " & SIMULATOR_AGENT_TABLE_NAME & " where name = " & WrapInSingleQuotes(p_userName), myTable) Then
            If myTable.Rows.Count > 0 Then
                myNodes(0) = myXMLWriter.AddChild(Nothing, "ToPhone")
                myXMLWriter.AddChild(myNodes(0), "Operation", "Dial_Ack")
                myXMLWriter.AddChild(myNodes(0), "CallId", p_callId)

                UdpSendString(myXMLWriter.AsString, myTable.Rows(0).Item(0), myTable.Rows(0).Item(1))
            End If
        End If
    End Sub

    Private Sub startBlockListBox_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles startBlockListBox.SelectedIndexChanged
        Dim myFileName As String = MY_PATH & "\" & startBlockListBox.SelectedItem
        Dim myReader As New IO.StreamReader(myFileName)
        Dim reading As Boolean = True

        With startBlockTextBox
            .Clear()

            While reading
                Dim myLine As String = myReader.ReadLine

                If myLine Is Nothing Then
                    reading = False
                Else
                    If .Text <> "" Then .Text &= vbCrLf

                    .Text &= myLine
                End If
            End While
        End With

        myReader.Close()
    End Sub

    Private Sub insertScriptCodeListBox_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles insertScriptCodeListBox.SelectedIndexChanged
        Dim myFileName As String = MY_PATH & "\" & insertScriptCodeListBox.SelectedItem
        Dim myReader As New IO.StreamReader(myFileName)
        Dim reading As Boolean = True

        With insertScriptCodeTextBox
            .Clear()

            While reading
                Dim myLine As String = myReader.ReadLine

                If myLine Is Nothing Then
                    reading = False
                Else
                    If .Text <> "" Then .Text &= vbCrLf

                    .Text &= myLine
                End If
            End While
        End With

        myReader.Close()
    End Sub

    Private Sub targetGroupsListBox_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles targetGroupsListBox.SelectedIndexChanged
        Dim mySql As String = "select type, callingTimeout from " & SIMULATOR_GROUP_TABLE_NAME & " where name = " & WrapInSingleQuotes(targetGroupsListBox.SelectedItem)
        Dim myTable As New DataTable

        If FillTableFromCommand(CreateConnectionString(Form1.settingsConfigDictionary), mySql, myTable) Then
            If myTable.Rows.Count > 0 Then
                With myTable.Rows(0)
                    ComboBox1.SelectedItem = .Item(0)

                    allowAgentTimeoutUpdate = False
                    TextBox2.Text = ""
                    If Not .Item(1) Is DBNull.Value Then TextBox2.Text = .Item(1)
                    allowAgentTimeoutUpdate = True
                End With
            End If
        End If

        UpdateAgentsInGroup()
    End Sub

    Private Sub UpdateAgentsInGroup()
        Dim mySql As String = "select c.name + ' ' + c.extension from SimulatorGroupTable as a left join SimulatorMembershipTable as b on a.groupId = b.groupId left join SimulatorAgentTable as c on b.userId = c.userId where a.name = " & WrapInSingleQuotes(targetGroupsListBox.SelectedItem) & " order by position"
        Dim myTable As New DataTable

        agentsInGroupListBox.Items.Clear()

        If FillTableFromCommand(CreateConnectionString(Form1.settingsConfigDictionary), mySql, myTable) Then
            For i = 0 To myTable.Rows.Count - 1
                With myTable.Rows(i)
                    If .Item(0) IsNot DBNull.Value Then agentsInGroupListBox.Items.Add(.Item(0))
                End With
            Next
        End If
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If availableAgentsListBox.SelectedItem IsNot Nothing Then AddAgentToGroup(availableAgentsListBox.SelectedItem)
    End Sub

    Private Function GetUserId(ByRef p As String) As Integer
        Dim mySql As String = "select userId from SimulatorAgentTable where name = " & WrapInSingleQuotes(p)
        Dim myTable As New DataTable
        Dim myUserId As Integer = -1

        If FillTableFromCommand(CreateConnectionString(Form1.settingsConfigDictionary), mySql, myTable) Then
            If myTable.Rows.Count >= 0 Then
                With myTable.Rows(0)
                    If .Item(0) IsNot DBNull.Value Then myUserId = CInt(.Item(0))
                End With
            End If
        End If

        Return myUserId
    End Function

    Private Function GetGroupId(ByRef p As String) As Integer
        Dim mySql As String = "select groupId from SimulatorGroupTable where name = " & WrapInSingleQuotes(p)
        Dim myTable As New DataTable
        Dim myGroupId As Integer = -1

        If FillTableFromCommand(CreateConnectionString(Form1.settingsConfigDictionary), mySql, myTable) Then
            If myTable.Rows.Count >= 0 Then
                With myTable.Rows(0)
                    If .Item(0) IsNot DBNull.Value Then myGroupId = CInt(.Item(0))
                End With
            End If
        End If

        Return myGroupId
    End Function

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        If Not agentsInGroupListBox.SelectedItem Is Nothing Then RemoveAgentFromGroup(agentsInGroupListBox.SelectedItem)
    End Sub

    Public Sub UpdateMessageDisplay(ByRef p As String)
        If Me.InvokeRequired Then
            Me.Invoke(New StringDelegate(AddressOf InvokeUpdateMessageDisplay), New Object() {p})
        Else
            InvokeUpdateMessageDisplay(p)
        End If
    End Sub

    Private Sub InvokeUpdateMessageDisplay(ByRef p As String)
        With msgTextBox
            Dim myText As String = "[" & Now.ToString.Substring(11) & "] " & p
            Dim myStartIndex = .TextLength

            .SelectionLength = 0
            .SelectionColor = msgBoxColour

            If .Text <> "" Then myText = vbCrLf & myText

            .AppendText(myText)
            .SelectionColor = .ForeColor
        End With
    End Sub

    Private Sub availableAgentsListBox_MouseDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles availableAgentsListBox.MouseDoubleClick
        AddAgentToGroup(availableAgentsListBox.SelectedItem)
    End Sub

    Private Sub AddAgentToGroup(ByRef p_agent As String)
        If Not agentsInGroupListBox.Items.Contains(p_agent) Then
            agentsInGroupListBox.Items.Add(p_agent)
            UpdateGroupMembersInDatabase(targetGroupsListBox.SelectedItem)
        End If
    End Sub

    Private Sub RemoveAgentFromGroup(ByRef p_agent As String)
        agentsInGroupListBox.Items.Remove(p_agent)
        UpdateGroupMembersInDatabase(targetGroupsListBox.SelectedItem)
    End Sub

    Private Sub UpdateGroupMembersInDatabase(ByRef p_groupName As String)
        Dim myGroupId As Integer = GetGroupId(targetGroupsListBox.SelectedItem)
        Dim mySql As String = "delete from " & SIMULATOR_MEMBERSHIP_TABLE_NAME & " where groupId = " & myGroupId

        ExecuteNonQuery(CreateConnectionString(Form1.settingsConfigDictionary), mySql)

        For i = 0 To agentsInGroupListBox.Items.Count - 1
            Dim myAgent As String = agentsInGroupListBox.Items(i).split(" ")(0)

            mySql = "insert into " & SIMULATOR_MEMBERSHIP_TABLE_NAME & " values(" & GetUserId(myAgent) & ", " & myGroupId & ", " & i & ")"
            ExecuteNonQuery(CreateConnectionString(Form1.settingsConfigDictionary), mySql)
        Next
    End Sub

    Private Sub agentsInGroupListBox_MouseDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles agentsInGroupListBox.MouseDoubleClick
        RemoveAgentFromGroup(agentsInGroupListBox.SelectedItem)
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        With agentsInGroupListBox
            If Not .SelectedItem Is Nothing Then
                If .SelectedIndex > 0 Then
                    Dim myTemp As String = .Items(.SelectedIndex - 1)

                    .Items(.SelectedIndex - 1) = .Items(.SelectedIndex)
                    .Items(.SelectedIndex) = myTemp
                    .SelectedIndex -= 1

                    UpdateGroupMembersInDatabase(targetGroupsListBox.SelectedItem)
                End If
            End If
        End With
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        With agentsInGroupListBox
            If Not .SelectedItem Is Nothing Then
                If .SelectedIndex < (agentsInGroupListBox.Items.Count - 1) Then
                    Dim myTemp As String = .Items(.SelectedIndex + 1)

                    .Items(.SelectedIndex + 1) = .Items(.SelectedIndex)
                    .Items(.SelectedIndex) = myTemp
                    .SelectedIndex += 1

                    UpdateGroupMembersInDatabase(targetGroupsListBox.SelectedItem)
                End If
            End If
        End With
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox1.SelectedIndexChanged
        Dim mySql As String = "update " & SIMULATOR_GROUP_TABLE_NAME & " set type = " & WrapInSingleQuotes(ComboBox1.SelectedItem) & " where groupId = " & GetGroupId(targetGroupsListBox.SelectedItem)

        ExecuteNonQuery(CreateConnectionString(Form1.settingsConfigDictionary), mySql)
    End Sub

    Private Sub TextBox2_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox2.TextChanged
        If allowAgentTimeoutUpdate Then
            Dim mySql As String = ""

            If TextBox2.Text = "" Then
                mySql = "update " & SIMULATOR_GROUP_TABLE_NAME & " set callingTimeout = NULL where groupId = " & GetGroupId(targetGroupsListBox.SelectedItem)
            Else
                mySql = "update " & SIMULATOR_GROUP_TABLE_NAME & " set callingTimeout = " & CInt(TextBox2.Text) & " where groupId = " & GetGroupId(targetGroupsListBox.SelectedItem)
            End If

            ExecuteNonQuery(CreateConnectionString(Form1.settingsConfigDictionary), mySql)
        End If
    End Sub
End Class