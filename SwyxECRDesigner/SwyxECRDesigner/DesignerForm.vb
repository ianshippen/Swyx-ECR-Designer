Imports System.Xml
Imports System.ComponentModel
Imports System.Windows.Forms

Public Class DesignerForm
    Enum VariableTypes
        NULL
        ALPHA_STRING
        [BOOLEAN]
        [DATE]
        DATE_TIME_ISO
        FILENAME
        [INTEGER]
        NUMBER_STRING
        TIME
    End Enum

    Public Const SERVICEBUILDER_TABLE_NAME As String = "ServiceBuilderTable"
    Public Const SERVICEBUILDER_EVENT_TABLE_NAME As String = "ServiceBuilderEventTable"
    Public Const SIBB_NEEDS_TABLE_NAME As String = "SIBBNeedsTable"
    Private Const TRACE_SLEEP_TIME_MS As Integer = 500
    Private Const TRACE_NODE_PAUSE_TIME_MS As Integer = 200
    Private Const TRACE_DELAY_MS As Integer = 150

    Const SIBB_GAP As Integer = 40
    Const SIBB_LEFT_BORDER As Integer = 10
    Const SIBB_TOP_BORDER As Integer = 10
    Const TRACE_NODE_MULTIPLIER As Integer = 1024
    Public Const DISCONNECTED_OUPUT_NAME As String = "Disconnected"
    Private Const ON_DISCONNECT_NODE_NUMBER As Integer = 1
    Private Const TEST_SCRIPT_NAME As String = "MY_TEST_SCRIPT"

    Public myViewBitmap, myMasterBitmap As Bitmap
    Public sibbTypeList As New List(Of SIBBTypeClass)
    Private canvasX, canvasY As Integer
    Private allowResize As Boolean = False
    Private HScrollX, HScrollY, VScrollX, VScrollY As Integer
    Public lineList As New LineListClass
    Public openFilename As String = ""
    Private serviceWideVariablesList As New List(Of ServiceWideVariableClass)
    'Private ddiVariablesDictionary As New Dictionary(Of String, Dictionary(Of String, String))
    Private ddiVariables, backup_ddiVariables As New Dictionary(Of String, DDIKeyEntryClass)
    Public ddiVariableTypes As New List(Of DDIVariableTypeClass)

    Private canvasOptions() As String = {"Cut", ContextTypes.CUT, "Copy", ContextTypes.COPY, "Paste", ContextTypes.PASTE, "Delete", ContextTypes.DELETE}
    Private myContextMenuStrip As ContextMenuStrip = Nothing

    Public Function GetVariableTypeNameFromType(ByVal p_type As VariableTypes) As String
        Dim result As String = ""

        Select Case p_type
            Case VariableTypes.NULL
                result = "Null"

            Case VariableTypes.ALPHA_STRING
                result = "Alpha String"

            Case VariableTypes.BOOLEAN
                result = "Boolean"

            Case VariableTypes.DATE
                result = "Date"

            Case VariableTypes.DATE_TIME_ISO
                result = "Date Time ISO"

            Case VariableTypes.FILENAME
                result = "Filename"

            Case VariableTypes.INTEGER
                result = "Integer"

            Case VariableTypes.NUMBER_STRING
                result = "Number String"

            Case VariableTypes.TIME
                result = "Time"
        End Select

        Return result
    End Function

    Public Function GetVariableTypeFromName(ByRef p_name As String) As VariableTypes
        Dim result As VariableTypes = VariableTypes.NULL

        Select Case p_name
            Case GetVariableTypeNameFromType(VariableTypes.ALPHA_STRING)
                result = VariableTypes.ALPHA_STRING

            Case GetVariableTypeNameFromType(VariableTypes.BOOLEAN)
                result = VariableTypes.BOOLEAN

            Case GetVariableTypeNameFromType(VariableTypes.DATE)
                result = VariableTypes.DATE

            Case GetVariableTypeNameFromType(VariableTypes.DATE_TIME_ISO)
                result = VariableTypes.DATE_TIME_ISO

            Case GetVariableTypeNameFromType(VariableTypes.FILENAME)
                result = VariableTypes.FILENAME

            Case GetVariableTypeNameFromType(VariableTypes.INTEGER)
                result = VariableTypes.INTEGER

            Case GetVariableTypeNameFromType(VariableTypes.NUMBER_STRING)
                result = VariableTypes.NUMBER_STRING

            Case GetVariableTypeNameFromType(VariableTypes.TIME)
                result = VariableTypes.TIME
        End Select

        Return result
    End Function

    Private Sub BindSIBBType(ByVal p_type As SIBBTypeClass.SIBBTYPE, ByRef p_formTarget As Form, ByVal p_typeName As String, ByRef p_outputNames As String, Optional ByVal p_footerTitle As String = "")
        Dim x As New SIBBTypeClass

        If p_footerTitle = "" Then p_footerTitle = p_typeName

        If Not p_typeName.ToLower.StartsWith("sibb_") Then p_typeName = "SIBB_" & p_typeName

        x.SetSIBBType(p_type)
        x.SetPropertiesForm(p_formTarget)
        x.SetTypeName(p_typeName)
        x.SetFooterTitle(p_footerTitle)

        If p_outputNames <> "" Then
            If p_outputNames.Contains(",") Then
                Dim myArray As String() = p_outputNames.Split(",")

                For i = 0 To myArray.Count - 1
                    x.AddFixedOutputName(myArray(i).Trim)
                Next
            Else
                x.AddFixedOutputName(p_outputNames.Trim)
            End If
        End If

        ' Add the Disconnect output except for special cases
        If p_type <> SIBBTypeClass.SIBBTYPE.DONE And p_type <> SIBBTypeClass.SIBBTYPE.NULL And p_type <> SIBBTypeClass.SIBBTYPE.SKIP And p_type <> SIBBTypeClass.SIBBTYPE.START And p_type <> SIBBTypeClass.SIBBTYPE.ON_DISCONNECT Then
            x.AddFixedOutputName(DISCONNECTED_OUPUT_NAME)
        End If

        sibbTypeList.Add(x)
    End Sub

    Private Sub AddNotes(ByVal p_type As SIBBTypeClass.SIBBTYPE, ByRef p_notes As String)
        Dim myIndex As Integer = -1

        For i = 0 To sibbTypeList.Count - 1
            If sibbTypeList(i).GetSIBBType = p_type Then
                myIndex = i
                Exit For
            End If
        Next

        If myIndex >= 0 Then
            Dim myFormRef As MyBaseForm = sibbTypeList(myIndex).GetPropertiesForm

            myFormRef.notesRichTextBox.AppendText(p_notes)
        End If
    End Sub

    Private Sub BindSIBBTypes()
        BindSIBBType(SIBBTypeClass.SIBBTYPE.TODDOW, TODDOWSIBBForm, "TODDOW", "")
        BindSIBBType(SIBBTypeClass.SIBBTYPE.PA, MyBaseForm, "PlayAnnouncement", "Continue, DTMF Pressed", "Play Announcement")
        BindSIBBType(SIBBTypeClass.SIBBTYPE.CONNECT, MyBaseForm, "Connect", "Connected, No Answer, Busy, Not Available")
        BindSIBBType(SIBBTypeClass.SIBBTYPE.GET_DTMF_STRING, MyBaseForm, "GetDTMFString", "String Stored, Nothing Entered, Delimiter Hit", "Get DTMF String")
        BindSIBBType(SIBBTypeClass.SIBBTYPE.VOICEMAIL, VoicemailForm, "Voicemail", "Timeout, Recorded")
        BindSIBBType(SIBBTypeClass.SIBBTYPE.LONGEST_WAITING_DIST, MyBaseForm, "LongestWaiting", "Connected, No Answer, Busy, Not Available", "Longest Waiting Agent Distributor")
        BindSIBBType(SIBBTypeClass.SIBBTYPE.GROUP_AVAILABLE, MyBaseForm, "GroupAvailable", "Busy, Available", "Group Available")
        BindSIBBType(SIBBTypeClass.SIBBTYPE.HOLD, MyBaseForm, "Hold", "Continue")
        BindSIBBType(SIBBTypeClass.SIBBTYPE.PAUSE, MyBaseForm, "Pause", "Busy, Available")
        BindSIBBType(SIBBTypeClass.SIBBTYPE.SLEEP, MyBaseForm, "Sleep", "Awake")
        BindSIBBType(SIBBTypeClass.SIBBTYPE.END_CALL, MyBaseForm, "EndCall", "Call Ended", "End Call")
        BindSIBBType(SIBBTypeClass.SIBBTYPE.DAY_OF_WEEK, MyBaseForm, "DayOfWeek", "Monday, Tuesday, Wednesday, Thursday, Friday, Saturday, Sunday", "Day Of Week")
        BindSIBBType(SIBBTypeClass.SIBBTYPE.TIME_OF_DAY, MyBaseForm, "TimeOfDay", "In Hours, Out Of Hours", "Time Of Day")

        BindSIBBType(SIBBTypeClass.SIBBTYPE.START, MyBaseForm, "Start", "Go")
        sibbTypeList(sibbTypeList.Count - 1).SetCanDelete(False)

        BindSIBBType(SIBBTypeClass.SIBBTYPE.DONE, MyBaseForm, "Done", "")
        sibbTypeList(sibbTypeList.Count - 1).SetCanDelete(False)

        BindSIBBType(SIBBTypeClass.SIBBTYPE.SKIP, MyBaseForm, "Skip", "")
        sibbTypeList(sibbTypeList.Count - 1).SetCanDelete(False)

        BindSIBBType(SIBBTypeClass.SIBBTYPE.ON_DISCONNECT, MyBaseForm, "OnDisconnect", "On Disconnect")
        sibbTypeList(sibbTypeList.Count - 1).SetCanDelete(False)

        BindSIBBType(SIBBTypeClass.SIBBTYPE.VBSCRIPT, MyBaseForm, "VBScript", "Default, Return Code 1, Return Code 2, Return Code 3, Return Code 4, Return Code 5, Return Code 6, Return Code 7, Return Code 8, Return Code 9", "VB Script")
        BindSIBBType(SIBBTypeClass.SIBBTYPE.GET_DTMF_DIGIT, MyBaseForm, "GetDTMFDigit", "Key 0, Key 1, Key 2, Key 3, Key 4, Key 5, Key 6, Key 7, Key 8, Key 9, Key *, Key #, Timeout", "Get DTMF Digit")
        BindSIBBType(SIBBTypeClass.SIBBTYPE.HOLIDAY, MyBaseForm, "Holiday", "Not A Holiday, Bank Holiday, Other Holiday", "Holiday Routing")
        BindSIBBType(SIBBTypeClass.SIBBTYPE.ADD_CALL_TO_QUEUE, MyBaseForm, "AddCallToQueue", "Done", "Add Call To Queue")
        BindSIBBType(SIBBTypeClass.SIBBTYPE.TOP_OF_QUEUE, MyBaseForm, "TopOfQueue", "Yes, No, Left Queue", "Top Of Queue ?")
        BindSIBBType(SIBBTypeClass.SIBBTYPE.CHANGE_QUEUE_STATE, MyBaseForm, "ChangeQueueState", "Done", "Change Queue State")
        BindSIBBType(SIBBTypeClass.SIBBTYPE.QUEUE_PAUSE, MyBaseForm, "QueuePause", "Connect, Long Timeout, Left Queue", "Queue Pause")
        BindSIBBType(SIBBTypeClass.SIBBTYPE.REMOVE_CALL_FROM_QUEUE, MyBaseForm, "RemoveCallFromQueue", "Done", "Remove Call From Queue")
        BindSIBBType(SIBBTypeClass.SIBBTYPE.GET_POSITION_IN_QUEUE, MyBaseForm, "GetPositionInQueue", "In Queue, Left Queue", "Get Position In Queue")
        BindSIBBType(SIBBTypeClass.SIBBTYPE.GET_CURRENT_TIME, MyBaseForm, "GetCurrentTime", "Done", "Get Current Time")
        BindSIBBType(SIBBTypeClass.SIBBTYPE.HAS_TIME_ELAPSED, MyBaseForm, "HasTimeElapsed", "Yes, No", "Has Time Elapsed")
        BindSIBBType(SIBBTypeClass.SIBBTYPE.GET_LENGTH_OF_QUEUE, MyBaseForm, "GetQueueLength", "< Threshold, = Threshold, > Threshold", "Get Queue Length")
        BindSIBBType(SIBBTypeClass.SIBBTYPE.FIRST_TIME, MyBaseForm, "FirstTime", "First Time, Not First Time", "First Time")
        BindSIBBType(SIBBTypeClass.SIBBTYPE.LOG_POINT, MyBaseForm, "LogPoint", "Done", "Log Point")
        BindSIBBType(SIBBTypeClass.SIBBTYPE.LOG_AGENT_STATUS, MyBaseForm, "LogAgentStatus", "Done", "Log Agent Status")
        BindSIBBType(SIBBTypeClass.SIBBTYPE.ACTIVATE_CALL, MyBaseForm, "ActivateCall", "Activated", "Activate Call")
        BindSIBBType(SIBBTypeClass.SIBBTYPE.SEND_EMAIL, MyBaseForm, "SendEmail", "Email Sent")
    End Sub

    Private Sub AddSIBBNotes()
        'AddNotes(SIBBTypeClass.SIBBTYPE.GET_POSITION_IN_QUEUE, "Hello")
    End Sub

    Private Sub AddContextMenuHandlers()
        With Me
            ' Remove any old handlers
            .ContextMenuStrip = Nothing

            ' Add handlers
            BuildContextMenuStrip(canvasOptions)
            .ContextMenuStrip = ContextMenuStrip1
        End With
    End Sub

    Private Sub BuildContextMenuStrip(ByRef p_array() As String)
        With ContextMenuStrip1
            .Items.Clear()

            For i = 0 To p_array.Count - 1 Step 2
                .Items.Add(p_array(i), Nothing, AddressOf MyContextMenuStripEventHandler)
                .Items(i \ 2).Tag = p_array(i + 1)
            Next
        End With
    End Sub

    Sub MyContextMenuStripEventHandler(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If Not sender.tag Is Nothing Then
            Dim myTag As String = sender.tag.ToString
            Dim myFields() As String = myTag.Split(",")

            If myFields.Count = 3 Then
                Dim myOperation As Integer = CInt(myFields(0))
                Dim myNodeIndex As Integer = CInt(myFields(1))
                Dim myOutputIndex As Integer = CInt(myFields(2))

                MyContextHandler(myOperation, myNodeIndex, myOutputIndex)
            End If
        End If
    End Sub

    Public Function GetIndexForType(ByVal p_sibbType As SIBBTypeClass.SIBBTYPE)
        Dim result As Integer = -1

        For i = 0 To sibbTypeList.Count - 1
            If sibbTypeList(i).GetSIBBType = p_sibbType Then
                result = i
                Exit For
            End If
        Next

        Return result
    End Function

    Private Sub TODDOWToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TODDOWToolStripMenuItem.Click
        ' Create an instance of a TODDOW SIBB
        Dim x As New TODDOWSIBBClass(myMasterBitmap)

        CommonNodeAdd(x)
    End Sub

    Private Sub DesignerForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.CenterToScreen()
        realTimeCallTraceBackgroundWorker.WorkerReportsProgress = True
        realTimeCallTraceBackgroundWorker.WorkerSupportsCancellation = True

        VariablesForm.ClearVariables()
        sibbList.Clear()
        sibbTypeList.Clear()
        myViewBitmap = New Bitmap(canvasPictureBox.Size.Width, canvasPictureBox.Size.Height)
        canvasPictureBox.Image = myViewBitmap

        ' Default size of master bitmap is our canvas size
        myMasterBitmap = New Bitmap(canvasPictureBox.Size.Width, canvasPictureBox.Size.Height)

        ClearMasterBitmap()
        BindSIBBTypes()
        AddSIBBNotes()
        CreateDefaultBlocks()
        CreateDefaultVariables()

        canvasX = Size.Width - canvasPictureBox.Size.Width
        canvasY = Size.Height - canvasPictureBox.Size.Height
        HScrollX = Size.Width - HScrollBar1.Size.Width
        HScrollY = Size.Height - HScrollBar1.Location.Y
        VScrollX = Size.Width - VScrollBar1.Location.X
        VScrollY = Size.Height - VScrollBar1.Size.Height
        MyRefresh()
        allowResize = True
        openFilename = ""
        lastFramePos.SetToNull()
        SetGUIState(GUIStateType.NULL)
        Text = "Service Designer - New Project"
        AddContextMenuHandlers()

        ddiVariableTypes.Clear()
        ddiVariableTypes.Add(New DDIVariableTypeClass("CALLED_NUMBER", "CalledNumber()", "Called Number"))
        ddiVariableTypes.Add(New DDIVariableTypeClass("CALLING_NUMBER", "IpPbx.CallingNumber", "Calling Number"))

        StartToolStripMenuItem1.Enabled = True
        StopToolStripMenuItem.Enabled = False

        With EditVarForm.varTypeComboBox
            .Items.Clear()

            .Items.Add(GetVariableTypeNameFromType(VariableTypes.ALPHA_STRING))
            .Items.Add(GetVariableTypeNameFromType(VariableTypes.BOOLEAN))
            .Items.Add(GetVariableTypeNameFromType(VariableTypes.DATE))
            .Items.Add(GetVariableTypeNameFromType(VariableTypes.DATE_TIME_ISO))
            .Items.Add(GetVariableTypeNameFromType(VariableTypes.FILENAME))
            .Items.Add(GetVariableTypeNameFromType(VariableTypes.INTEGER))
            .Items.Add(GetVariableTypeNameFromType(VariableTypes.NUMBER_STRING))
            .Items.Add(GetVariableTypeNameFromType(VariableTypes.TIME))
        End With
    End Sub

    Public Sub ClearMasterBitmap()
        Dim myGraphics As Graphics = Graphics.FromImage(myMasterBitmap)

        myGraphics.FillRectangle(Brushes.WhiteSmoke, New Rectangle(0, 0, myMasterBitmap.Width, myMasterBitmap.Height))
        myGraphics.Dispose()
        lineList.Clear()
    End Sub

    Private Sub canvasPictureBox_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles canvasPictureBox.DoubleClick
        ' Which button was clicked ?
        Dim m As MouseEventArgs = e

        If m.Button = Windows.Forms.MouseButtons.Left Then
            Dim mySIBBIndex As Integer = -1
            Dim mousePosition As New ScreenLocationClass(m.X, m.Y)

            ' Find the SIBB we have double clicked
            For i = 0 To sibbList.Count - 1
                If sibbList(i).MouseInRange(mousePosition) Then
                    mySIBBIndex = i
                    Exit For
                End If
            Next

            If mySIBBIndex >= 0 Then
                ' Cancel the single click selection
                sibbList(mySIBBIndex).Hilight(False)

                ' Get the index of this SIBB type
                Dim typeIndex As Integer = GetIndexForType(sibbList(mySIBBIndex).GetSIBBType)

                If typeIndex >= 0 Then
                    ' Bind to the properties form for this SIBB type. tell it which instance we are, and open the properties page for this SIBB
                    With sibbTypeList(typeIndex).GetPropertiesForm
                        .Tag = mySIBBIndex
                        .ShowDialog()
                        sibbList(mySIBBIndex).PaintYourself()
                        MyRefresh()
                    End With
                End If
            End If
        End If
    End Sub

    Private Sub PlayAnnouncementToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PlayAnnouncementToolStripMenuItem.Click
        Dim x As New PASIBBClass(myMasterBitmap)

        CommonNodeAdd(x)
        x.outputs(PASIBBClass.DTMF_PRESSED_INDEX).visible = False
    End Sub

    Private Sub CalculateNextSIBBPosition(ByRef p_sibb As SIBBClass)
        Dim maxX As Integer = SIBB_LEFT_BORDER - (GetSIBBWidth() + SIBB_GAP)

        For i = 0 To sibbList.Count - 1
            If sibbList(i).GetXPos() > maxX Then maxX = sibbList(i).GetXPos()
        Next

        p_sibb.SetPos(maxX + GetSIBBWidth() + SIBB_GAP, SIBB_TOP_BORDER)
    End Sub

    Private Sub ConnectToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ConnectToolStripMenuItem.Click
        Dim x As New ConnectSIBBClass(myMasterBitmap)

        CommonNodeAdd(x)
    End Sub

    Private Sub canvasPictureBox_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles canvasPictureBox.MouseDown
        MouseDownHandler(e)
    End Sub

    Private Sub canvasPictureBox_MouseMove(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles canvasPictureBox.MouseMove
        MouseMoveHandler(e)
    End Sub

    Private Sub canvasPictureBox_MouseUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles canvasPictureBox.MouseUp
        MouseUpHandler(e)
    End Sub

    Private Sub DrawSelection(ByRef p_topLeft As AbsoluteLocationClass, ByRef p_bottomRight As AbsoluteLocationClass)
        Dim g As Graphics = canvasPictureBox.CreateGraphics

        g.DrawRectangle(New Pen(Color.Black, 1), p_topLeft.GetX, p_topLeft.GetY, p_bottomRight.GetX - p_topLeft.GetX, p_bottomRight.GetY - p_topLeft.GetY)

        g.Dispose()
    End Sub

    Private Sub ExportToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExportToolStripMenuItem.Click
        If CheckForExport() Then ExportProjectToDatabase(False)
    End Sub

    Private Sub ExportProjectToDatabase(ByVal p_testing As Boolean)
        Dim okToExportProject As Boolean = False
        Dim scriptName As String = ""
        Dim tableName As String = SERVICEBUILDER_TABLE_NAME
        Dim exportEnabled As Boolean = True
        Dim y As New SQLStatementClass
        Dim myTable As New DataTable
        Dim preferredName As String = ""
        Dim newCallFlow As Boolean = False
        Dim oldLines As Integer = 0

        If p_testing Then
            scriptName = TEST_SCRIPT_NAME
            okToExportProject = True
            DeleteScriptFromDatabase(scriptName)
        Else
            'Do any sanity checks here
            For i = 0 To serviceWideVariablesList.Count - 1
                With serviceWideVariablesList(i)
                    If .GetSanityCheckForNonEmpty Then
                        If .GetValue <> "" Then
                            Select Case MsgBox("About to deploy with Service Wide Variable " & WrapInQuotes(.GetName) & " with a non-empty value of " & WrapInQuotes(.GetValue) & vbCrLf _
                                   & "Are you sure you want to proceed ?", MsgBoxStyle.YesNoCancel)

                                Case MsgBoxResult.Cancel, MsgBoxResult.No
                                    Return
                            End Select
                        End If
                    End If
                End With
            Next

            ' Have we already saved the source ?
            If openFilename = "" Then
                ' No.
                SaveHandler(False)
            End If

            ExportForm.infoTextBox.Visible = False

            ' Populate the drop down with all existing script names in the database. Ignore backup scripts surrounding by "** .... **"
            y.SetPrimaryTable(tableName)
            y.AddSelectString("DISTINCT scriptName", "")
            y.AddCondition("scriptName NOT LIKE '** % **'")

            ExportForm.scriptNameComboBox.Items.Clear()

            ' Get the script name to export as. Start by getting all the possible script names from the database, and putting them into the combo box
            If FillTableFromCommand(CreateConnectionString(Form1.settingsConfigDictionary), y.GetSQLStatement, myTable) Then
                For i = 0 To myTable.Rows.Count - 1
                    With myTable.Rows(i)
                        If Not .Item(0) Is DBNull.Value Then
                            ExportForm.scriptNameComboBox.Items.Add(.Item(0))
                        End If
                    End With
                Next
            End If

            myTable.Rows.Clear()
            myTable.Columns.Clear()

            ' Get the preferred target that already references this source script, if available
            If FillTableFromCommand(CreateConnectionString(Form1.settingsConfigDictionary), "select scriptName from " & tableName & " where nodeNumber = 0 AND data like '%Source = " & WrapInQuotes(openFilename) & "%' AND scriptName not like '** % **'", myTable) Then
                If myTable.Rows.Count > 0 Then
                    If myTable.Rows.Count = 1 Then
                        With myTable.Rows(0)
                            If myTable.Columns.Count > 0 Then
                                If Not .Item(0) Is DBNull.Value Then preferredName = .Item(0)
                            End If
                        End With
                    Else
                        ExportForm.infoTextBox.Visible = True
                        ExportForm.infoTextBox.Text = "There are multiple targets for this script:" & vbCrLf

                        For i = 0 To myTable.Rows.Count - 1
                            With myTable.Rows(i)
                                If Not .Item(0) Is DBNull.Value Then ExportForm.infoTextBox.Text &= vbCrLf & .Item(0)
                            End With
                        Next
                    End If
                End If
            End If

            myTable.Rows.Clear()
            myTable.Columns.Clear()

            If preferredName <> "" Then ExportForm.scriptNameComboBox.Text = preferredName

            If ExportForm.ShowDialog = Windows.Forms.DialogResult.OK Then
                If ExportForm.scriptNameComboBox.Text = "" Then
                    MsgBox("No target selected - operation cancelled")
                Else
                    ' Remove all database entries for this script name
                    Dim dictName As String = "variablesDictionary"
                    Dim myCount As Integer = 0
                    Dim mySource As String = ""
                    Dim myTimeStamp As String = ""

                    scriptName = ExportForm.scriptNameComboBox.Text

                    ' Does this call flow already exist ?
                    If FillTableFromCommand(CreateConnectionString(Form1.settingsConfigDictionary), "select count(*) from " & tableName & " where scriptName = " & WrapInSingleQuotes(scriptName), myTable) Then
                        If myTable.Rows.Count > 0 Then
                            With myTable.Rows(0)
                                If myTable.Columns.Count > 0 Then

                                    If Not .Item(0) Is DBNull.Value Then myCount = CInt(.Item(0))
                                End If
                            End With
                        End If
                    End If

                    If myCount > 0 Then
                        BackupForm.backupScriptTextBox.Text = "** " & scriptName & " " & Now & " **"

                        If BackupForm.ShowDialog = Windows.Forms.DialogResult.OK Then
                            If ExecuteNonQuery(CreateConnectionString(Form1.settingsConfigDictionary), "insert into " & tableName & " select " & WrapInSingleQuotes(BackupForm.backupScriptTextBox.Text) & ", nodeNumber, nodeType, data, outputs, title, internalReference from " & tableName & " where scriptName = " & WrapInSingleQuotes(scriptName)) Then
                                Dim x As MessageBoxButtons

                                MessageBox.Show("Backup successful", "", x)
                            Else
                                If MsgBox("Backup failed - do you want to continue with the export ?", MsgBoxStyle.YesNoCancel) <> MsgBoxResult.Ok Then Return
                            End If
                        Else
                            Return
                        End If
                    End If

                    myTable.Rows.Clear()
                    myTable.Columns.Clear()

                    Dim scriptNames As New List(Of String)

                    scriptNames.Add(scriptName)

                    If GetBuildInfoFromDatabase(mySource, myTimeStamp, newCallFlow, scriptNames) Then
                        If newCallFlow Then
                            If MsgBox("You have selected a brand new callflow " & WrapInQuotes(scriptName) & vbCrLf & "Are you sure you want to continue ?", MsgBoxStyle.YesNoCancel, "Warning") <> MsgBoxResult.Yes Then exportEnabled = False
                        Else
                            If mySource = "" Then
                                If MsgBox("No source file info available for this call flow " & WrapInQuotes(scriptName) & vbCrLf & "                    Do you want to continue ?", MsgBoxStyle.YesNoCancel, "Warning") <> MsgBoxResult.Yes Then exportEnabled = False
                            Else
                                ' Is the source file the same as previously used ?
                                If mySource = openFilename Then
                                    ' Is the source older than the previous source ?
                                    If myTimeStamp <> "" Then
                                        Dim myFileInfo As New IO.FileInfo(openFilename)
                                        Dim myDateTime As DateTime = myTimeStamp

                                        If myFileInfo.LastWriteTime < myDateTime Then
                                            If MsgBox("Source is older than source used for last export - do you want to continue ?", MsgBoxStyle.YesNoCancel, "Warning") <> MsgBoxResult.Yes Then exportEnabled = False
                                        End If
                                    End If
                                Else
                                    ' No. Issue a warning and prompt continue
                                    Dim myText As String = "No existing source saved for this project"
                                    exportEnabled = False

                                    If openFilename <> "" Then myText = "Source file ( " & WrapInQuotes(openFilename) & " ) " & vbCrLf & "does not match source for previous build ( " & WrapInQuotes(mySource) & " )"

                                    If MsgBox(myText & vbCrLf & "Are you sure you want to continue ?", MsgBoxStyle.YesNoCancel, "Warning") = MsgBoxResult.Yes Then
                                        If MsgBox("Are you absolutely sure you want to continue with the export ?", MsgBoxStyle.YesNoCancel, "Warning") = MsgBoxResult.Yes Then
                                            exportEnabled = True
                                        End If
                                    End If
                                End If
                            End If
                        End If
                    End If

                    If exportEnabled Then
                        ' Count lines in existing call flow
                        Dim myCommand As String = "select count(*) from " & tableName & " where scriptName = " & WrapInSingleQuotes(scriptName)
                        oldLines = GetLinesInScriptFromDB(scriptName)

                        ' Delete the existing call flow
                        myCommand = "delete from " & tableName & " where scriptName = " & WrapInSingleQuotes(scriptName)

                        ' If delete was successful then export the project
                        If ExecuteNonQuery(CreateConnectionString(Form1.settingsConfigDictionary), myCommand) Then
                            okToExportProject = True
                        Else
                            MsgBox("Database error")
                        End If
                    End If
                End If
            End If
        End If

        If okToExportProject Then
            For i = 0 To sibbList.Count - 1
                ' Add each node to the database
                Dim x As New SQLStatementClass
                Dim myList As String = ""

                With sibbList(i)
                    Dim myInfo As String = ""

                    x.SetInsertIntoTable(tableName)
                    x.AddValue(WrapInSingleQuotes(scriptName))
                    x.AddValue(i)
                    x.AddValue(WrapInSingleQuotes(.GetTypeName))

                    .PackData()

                    ' Is it a Start SIBB ?
                    If i = 0 Then
                        Dim modifiedString As String = Now

                        If openFilename <> "" Then
                            Dim myFileInfo As New IO.FileInfo(openFilename)

                            modifiedString = myFileInfo.LastWriteTime
                        End If

                        myInfo = "'Build Info : Source = " & WrapInQuotes(openFilename) & " : Built at " & Now & " : Source modified " & modifiedString & vbCrLf & vbCrLf

                        With VariablesForm
                            ' Add any DDI specific variable values to the pre-existing variables. Do it this way to allow a Swyx ECR start block to overide the values
                            myInfo &= "'DDI Variables here. These will overwrite anything defined in the Swyx ECR block that calls us" & vbCrLf & vbCrLf

                            For Each ddiEntryKey As String In ddiVariables.Keys.ToList
                                myInfo &= "Select Case " & ddiVariables.Item(ddiEntryKey).GetSwyxVariableExpression & vbCrLf

                                With ddiVariables.Item(ddiEntryKey)
                                    For Each matchValue As String In .matchValues.Keys.ToList
                                        If matchValue <> "" Then
                                            myInfo &= vbCrLf & "  Case " & WrapInQuotes(matchValue) & vbCrLf

                                            With .matchValues.Item(matchValue).variables
                                                For Each varName As String In .Keys.ToList()
                                                    myInfo &= "    SetVar " & WrapInQuotes(varName) & ", " & WrapInQuotes(.Item(varName)) & vbCrLf
                                                Next
                                            End With
                                        End If
                                    Next
                                End With

                                myInfo &= vbCrLf & "End Select" & vbCrLf
                            Next

                            ' Add any service wide variables to the VB Script area of the Start Block. AddVar will not overwrite an existing variable customised
                            ' in a VB ECR block, or with the DDI specific handler above
                            myInfo &= vbCrLf & "'Service Wide Variables here. These cannot change the value anything already defined as a DDI Variable, or setup in the Swyx ECR block that calls us." & vbCrLf
                            myInfo &= "'These are the default values for these variables" & vbCrLf & vbCrLf

                            For j = 0 To GetNumberOfServiceWideVariables() - 1
                                If GetServiceWideVariableName(j) <> "" Then
                                    Dim myKey As String = GetServiceWideVariableName(j)
                                    Dim myValue As String = GetServiceWideVariableValue(j)

                                    myInfo &= "AddVar " & WrapInQuotes(myKey) & ", " & WrapInQuotes(myValue) & vbCrLf
                                End If
                            Next

                            myInfo &= vbCrLf
                        End With
                    End If

                    x.AddValue(WrapInSingleQuotes(SingleQuoteCheck(myInfo & .GetData)))

                    For j = 0 To .GetOutputCount - 1
                        If myList <> "" Then myList &= ","

                        myList &= .GetOutputNextNode(j)
                    Next

                    x.AddValue(WrapInSingleQuotes(myList))

                    x.AddValue(WrapInSingleQuotes(.GetNodeTitle))
                    x.AddValue(WrapInSingleQuotes(.GetInternalReference))
                End With

                If ExecuteNonQuery(CreateConnectionString(Form1.settingsConfigDictionary), x.GetSQLStatement) Then
                Else
                    MsgBox("Error in processing SQL statement " & WrapInQuotes(x.GetSQLStatement) & " with connection string " & WrapInQuotes(CreateConnectionString(Form1.settingsConfigDictionary)))
                    Exit For
                End If
            Next
        End If

        If okToExportProject Then
            If p_testing Then
            Else
                Dim newLines As Integer = GetLinesInScriptFromDB(scriptName)

                If newCallFlow Then
                    MsgBox("Export complete for " & WrapInQuotes(scriptName) & vbCrLf & "No previous version" & vbCrLf & newLines & " lines in new version")
                Else
                    MsgBox("Export complete for " & WrapInQuotes(scriptName) & vbCrLf & oldLines & " lines in old version" & vbCrLf & newLines & " lines in new version")
                End If
            End If
        End If
    End Sub

    Private Function GetLinesInScriptFromDB(ByRef p_scriptName As String) As Integer
        Dim result As Integer = -1
        Dim myCommand As String = "select count(*) from " & SERVICEBUILDER_TABLE_NAME & " where scriptName = " & WrapInSingleQuotes(p_scriptName)
        Dim myTable As New DataTable

        If DatabaseAccess.FillTableFromCommand(CreateConnectionString(Form1.settingsConfigDictionary), myCommand, myTable) Then
            If myTable.Rows.Count = 1 Then
                If myTable.Columns.Count = 1 Then
                    If myTable.Rows(0).Item(0) IsNot DBNull.Value Then
                        If IsInteger(myTable.Rows(0).Item(0)) Then result = CInt(myTable.Rows(0).Item(0))
                    End If
                End If
            End If
        End If

        Return result
    End Function

    Private Sub CopyFunctionToDatabaseToolStripMenuItem_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CopyFunctionToDatabaseToolStripMenuItem.Click
        CopyFunctionToDatabaseHandler()
    End Sub

    Private Sub CopyFunctionToDatabaseHandler()
        Dim x As New OpenFileDialog
        Dim copying As Boolean = True
        Dim myLastSourceFolder As String = Form1.settingsConfigDictionary.GetItem([Enum].GetName(GetType(Form1.OurConfigItems), Form1.OurConfigItems.lastSourceFolder))

        If myLastSourceFolder <> "" Then x.InitialDirectory = myLastSourceFolder

        While copying
            If x.ShowDialog = Windows.Forms.DialogResult.OK Then
                If CopyFunctionToDatabase(x.FileName, False, True, True) Then
                    MsgBox("Function copied to database")
                Else
                    MsgBox("Database error")
                End If

                Dim myIndex As Integer = x.FileName.LastIndexOf("\")

                If myIndex >= 0 Then
                    Dim mySourceFolder As String = x.FileName.Substring(0, myIndex)

                    If mySourceFolder <> myLastSourceFolder Then
                        Form1.settingsConfigDictionary.SetItem([Enum].GetName(GetType(Form1.OurConfigItems), Form1.OurConfigItems.lastSourceFolder), mySourceFolder)
                        GenericXMLConfigSaver(Form1.CONFIG_FILENAME, Form1.settingsConfigDictionary)
                    End If
                End If
            Else
                copying = False
            End If
        End While
    End Sub

    Public Function CopyFunctionToDatabase(ByRef p_functionFileName As String, ByVal p_showBindingInfo As Boolean, ByVal p_promptForUpdateNeeds As Boolean, ByVal p_showStatus As Boolean) As Boolean
        Dim result As Boolean = False
        Dim tableName As String = "Scripts"
        Dim myFileInfo As New IO.FileInfo(p_functionFileName)
        Dim myInfoString As String = myFileInfo.LastWriteTime
        Dim myFunctionName As String = myFileInfo.Name.Substring(0, myFileInfo.Name.LastIndexOf("."))
        Dim updateNeeds As Boolean = True
        Dim backupTableName As String = tableName & "_history"
        Dim tableList As New List(Of String)
        Dim tableDefs As New List(Of String)
        Dim myTimeStamp As String = WrapInSingleQuotes(Now.ToString("yyyy-MM-dd HH:mm:ss.fff"))
        Dim myTable As New DataTable
        Dim functionAlreadyExists As Boolean = False

        ' Does this function already exist in the database ?
        If FillTableFromCommand(CreateConnectionString(Form1.settingsConfigDictionary), "select * from " & tableName & " where FunctionName = " & WrapInSingleQuotes(myFunctionName), myTable) Then
            If myTable.Rows.Count > 0 Then functionAlreadyExists = True
        Else
            MsgBox("Error checking for " & WrapInQuotes(p_functionFileName) & " in table " & WrapInQuotes(tableName))

            Return result
        End If

        If functionAlreadyExists Then
            ' Does the backup table exist ?
            tableList.Add(backupTableName)
            tableDefs.Add("FunctionName varchar(256), LineNumber int, Code varchar(4096), Class varchar(256), TimeStamp datetime")
            TableCheck(p_functionFileName, tableList, tableDefs)

            ' Backup any current version of this function
            If BackupFunction(myFunctionName) Then
                MsgBox("Backup of " & WrapInQuotes(p_functionFileName) & " successful")
            Else
                MsgBox(MsgBoxStyle.Critical, "Backup of " & WrapInQuotes(p_functionFileName) & " failed")
            End If
        Else
            If p_showStatus Then MsgBox("Function " & WrapInQuotes(p_functionFileName) & " not currently in database" & vbCrLf & "Adding for the first time" & vbCrLf & "No history being made")
        End If

        ' Remove any lines for this function already in live DB
        If DeleteFunctionFromLiveDB(myFunctionName) Then
            Dim y As New IO.StreamReader(p_functionFileName)
            Dim reading As Boolean = True
            Dim line As Integer = 1

            result = True

            While reading
                Dim myLine As String

                If line = 1 Then
                    myLine = "' Last Modified: " & myFileInfo.LastWriteTime
                Else
                    myLine = y.ReadLine
                End If

                If myLine Is Nothing Then
                    reading = False
                Else
                    myLine = myLine.Trim

                    If myLine <> "" Then
                        If (Not myLine.StartsWith("'")) Or (line = 1) Then
                            Dim mySql As New SQLStatementClass

                            mySql.SetInsertIntoTable(tableName)
                            mySql.AddValue(WrapInSingleQuotes(myFunctionName))
                            mySql.AddValue(line)
                            mySql.AddValue(WrapInSingleQuotes(SingleQuoteCheck(myLine)))
                            mySql.AddValue("NULL")

                            ExecuteNonQuery(CreateConnectionString(Form1.settingsConfigDictionary), mySql.GetSQLStatement)
                            line += 1
                        End If
                    End If
                End If
            End While

            y.Close()

            If p_promptForUpdateNeeds Then
                updateNeeds = False

                If MsgBox("Update Needs Table (Normally Yes)", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then updateNeeds = True
            End If

            If updateNeeds Then
                If UpdateNeedsTable(myFunctionName, p_showBindingInfo, p_showStatus) Then
                    If p_showBindingInfo Then MsgBox("SIBB Needs Table has been updated")
                End If
            End If
        End If

        Return result
    End Function

    Private Function CheckForExport() As Boolean
        Dim result As Boolean = True

        For i = 0 To sibbList.Count - 1
            With sibbList(i)
                For j = 0 To .GetOutputCount - 1
                    If .OutputIsVisible(j) Then
                        If .GetOutputNextNode(j) = -1 Then
                            Dim x As MessageBoxButtons

                            MessageBox.Show("Error - Node " & WrapInQuotes(.GetNodeTitle) & " - Output " & j & " " & WrapInQuotes(.GetOutputName(j)) & " not connected" & vbCrLf & "Export failed", "", x)
                            result = False
                        End If
                    End If
                Next
            End With
        Next

        Return result
    End Function

    Private Sub SaveToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaveToolStripMenuItem.Click
        SaveHandler(False)
    End Sub

    Private Sub SaveHandler(ByVal p_saveAs As Boolean)
        Dim useDialog As Boolean = True

        If p_saveAs Then

        Else
            ' If we know the filename already then save it directly here
            If openFilename <> "" Then
                If Save(openFilename) Then useDialog = False
            End If
        End If

        ' Launch the Save Dialog if required
        If useDialog Then
            Dim x As New SaveFileDialog
            Dim saving As Boolean = True

            ' Check if it is a Save As operation
            If p_saveAs Then
                ' If we already have a filename then take this as the default path
                If openFilename <> "" Then x.FileName = openFilename.Substring(openFilename.LastIndexOf("\") + 1)
            End If

            x.Filter = "XML Files (*.xml)|*.xml"

            While saving
                If x.ShowDialog = Windows.Forms.DialogResult.OK Then
                    If Save(x.FileName) Then
                        saving = False
                        openFilename = x.FileName
                        Text = x.FileName
                    End If
                Else
                    saving = False
                End If
            End While
        End If
    End Sub

    Private Function Save(ByVal p_filename As String) As Boolean
        Dim myXMLWriter As New XMLWriterClass
        Dim myNodes(10) As XmlElement
        Dim myTimestamp As String = Now
        Dim filenameSafeTimestamp As String = myTimestamp.Substring(6, 4) & myTimestamp.Substring(3, 2) & myTimestamp.Substring(0, 2) & "-" & myTimestamp.Substring(11).Replace(":", "")
        Dim myBackupName As String = p_filename.Substring(0, p_filename.Length - ".xml".Length) & " " & filenameSafeTimestamp & ".xml"
        Dim myFileAttributes As IO.FileAttributes

        ' Create a backup of this file if it already exists
        If IO.File.Exists(p_filename) Then
            IO.File.Copy(p_filename, myBackupName, True)

            ' Get current attributes of file
            myFileAttributes = IO.File.GetAttributes(p_filename)

            ' Set existing file to write enable
            IO.File.SetAttributes(p_filename, myFileAttributes And (&HFFFF - IO.FileAttributes.ReadOnly))
        End If

        myNodes(0) = myXMLWriter.AddChild(Nothing, "CallFlow")
        myXMLWriter.AddChild(myNodes(0), "Scale", GetScale())

        ' Save each node in the call flow
        For i = 0 To sibbList.Count - 1
            With sibbList(i)
                .PackData()
                myNodes(1) = myXMLWriter.AddChild(myNodes(0), "Node")
                myXMLWriter.AddChild(myNodes(1), "NodeNumber", i)
                myXMLWriter.AddChild(myNodes(1), "XPos", .absoluteLocation.GetX)
                myXMLWriter.AddChild(myNodes(1), "YPos", .absoluteLocation.GetY)
                myXMLWriter.AddChild(myNodes(1), "NodeType", .GetTypeName())
                myXMLWriter.AddChild(myNodes(1), "Title", .GetNodeTitle)
                myXMLWriter.AddChild(myNodes(1), "InternalReference", .GetInternalReference)
                myXMLWriter.AddChild(myNodes(1), "Data", .GetDataAsString)
                myXMLWriter.AddChild(myNodes(1), "ShowInCallTrace", .GetShowInCallTrace())

                myNodes(2) = myXMLWriter.AddChild(myNodes(1), "Outputs")

                For j = 0 To .GetOutputCount - 1
                    myNodes(3) = myXMLWriter.AddChild(myNodes(2), "Output")
                    myXMLWriter.AddChild(myNodes(3), "Name", .GetOutputName(j))
                    myXMLWriter.AddChild(myNodes(3), "NextNode", .GetOutputNextNode(j))
                    myXMLWriter.AddChild(myNodes(3), "Visible", .OutputIsVisible(j))
                    myXMLWriter.AddChild(myNodes(3), "Order", .GetOutputOrder(j))
                Next
            End With
        Next

        With VariablesForm
            ' Save the Service Wide variables
            For i = 0 To GetNumberOfServiceWideVariables() - 1
                If GetServiceWideVariableName(i) <> "" Then
                    myNodes(1) = myXMLWriter.AddChild(myNodes(0), "Variable")
                    myXMLWriter.AddChild(myNodes(1), "Name", GetServiceWideVariableName(i))
                    myXMLWriter.AddChild(myNodes(1), "DefaultValue", GetServiceWideVariableValue(i))
                    myXMLWriter.AddChild(myNodes(1), "Type", GetServiceWideVariableType(i))
                    myXMLWriter.AddChild(myNodes(1), "Flags", serviceWideVariablesList(i).GetFlags)
                End If
            Next

            ' Save the DDI variables
            myNodes(1) = myXMLWriter.AddChild(myNodes(0), "DDIKeyEntries")

            Dim myDDIKeyEntries As List(Of String) = GetListOfDDIKeyEntries()

            For Each myDDIKeyEntryKey As String In myDDIKeyEntries
                Dim myDDIKeys As List(Of String) = GetListOfDDIKeysForThisEntry(myDDIKeyEntryKey)

                myNodes(2) = myXMLWriter.AddChild(myNodes(1), "DDIKeyEntry")
                myXMLWriter.AddChild(myNodes(2), "Key", myDDIKeyEntryKey)
                ' myXMLWriter.AddChild(myNodes(2), "Value", GetDDIKeyEntryValue(myDDIKeyEntryKey))
                'myXMLWriter.AddChild(myNodes(2), "Description", GetDDIKeyEntryDescription(myDDIKeyEntryKey))

                myNodes(3) = myXMLWriter.AddChild(myNodes(2), "DDIKeys")

                For Each myDDIKey As String In myDDIKeys
                    Dim myDDIVariables As List(Of String) = GetListOfDDIVariables(myDDIKeyEntryKey, myDDIKey)

                    myNodes(4) = myXMLWriter.AddChild(myNodes(3), "DDIKey")
                    myXMLWriter.AddChild(myNodes(4), "Value", myDDIKey)
                    myXMLWriter.AddChild(myNodes(4), "Description", GetDDIKeyDescription(myDDIKeyEntryKey, myDDIKey))

                    myNodes(5) = myXMLWriter.AddChild(myNodes(4), "DDIVariables")

                    For Each ddiVariableName As String In myDDIVariables
                        If ddiVariableName <> "" Then
                            If ddiVariableName <> "" Then
                                myNodes(6) = myXMLWriter.AddChild(myNodes(5), "DDIVariable")
                                myXMLWriter.AddChild(myNodes(6), "Name", ddiVariableName)
                                myXMLWriter.AddChild(myNodes(6), "Value", GetDDIVariableValue(myDDIKeyEntryKey, myDDIKey, ddiVariableName))
                            End If
                        End If
                    Next
                Next
            Next
        End With

        Dim rc As Boolean = myXMLWriter.Save(p_filename)

        ' Set file to read-only
        myFileAttributes = IO.File.GetAttributes(p_filename)
        IO.File.SetAttributes(p_filename, myFileAttributes Or IO.FileAttributes.ReadOnly)

        Return rc
    End Function

    Private Sub LoadToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LoadToolStripMenuItem.Click
        LoadProjectFromDialogBox(False)
    End Sub

    Private Sub LoadProjectFromDialogBox(ByVal p_readOnly As Boolean)
        Dim myOpenDialog As New OpenFileDialog
        Dim myLastProjectFolder As String = Form1.settingsConfigDictionary.GetItem([Enum].GetName(GetType(Form1.OurConfigItems), Form1.OurConfigItems.lastProjectFolder))

        If myLastProjectFolder <> "" Then myOpenDialog.InitialDirectory = myLastProjectFolder

        myOpenDialog.Filter = "XML Files (*.xml)|*.xml"

        If myOpenDialog.ShowDialog = Windows.Forms.DialogResult.OK Then
            Dim myProjectFolder As String = myOpenDialog.FileName
            Dim myIndex As Integer = myProjectFolder.LastIndexOf("\")

            If myIndex >= 0 Then
                myProjectFolder = myProjectFolder.Substring(0, myIndex)

                If myProjectFolder <> myLastProjectFolder Then
                    Form1.settingsConfigDictionary.SetItem([Enum].GetName(GetType(Form1.OurConfigItems), Form1.OurConfigItems.lastProjectFolder), myProjectFolder)
                    GenericXMLConfigSaver(Form1.CONFIG_FILENAME, Form1.settingsConfigDictionary)
                End If
            End If

            LoadProject(myOpenDialog.FileName, p_readOnly)
        End If
    End Sub

    Private Sub LoadProject(ByRef p_filename As String, ByVal p_readonly As Boolean)
        ' Check that the file exists
        If FileExists(p_filename) Then
            Dim myDoc As New XmlDocument
            Dim myRecord As XmlNode = Nothing

            DebugForm.ResetDebug()
            serviceWideVariablesList.Clear()

            If Not p_readonly Then
                Dim myBackupName As String = p_filename.Substring(0, p_filename.Length - ".xml".Length) & "_backup.xml"
                Dim backupFileAttributes As IO.FileAttributes

                If FileExists(myBackupName) Then
                    ' Modify current attributes to allow write
                    backupFileAttributes = IO.File.GetAttributes(myBackupName)
                    IO.File.SetAttributes(myBackupName, backupFileAttributes And (&HFFFF - IO.FileAttributes.ReadOnly))
                End If

                ' Create a backup of this file, first change the attributes to allow write
                System.IO.File.Copy(p_filename, myBackupName, True)
            End If

            If XMLLoader(p_filename, myDoc) Then
                Dim maxX As Integer = 0
                Dim maxY As Integer = 0

                SetGUIState(GUIStateType.NULL)
                openFilename = p_filename
                Text = "Service Designer - " & openFilename.Substring(openFilename.LastIndexOf("\") + 1)
                sibbList.Clear()
                VariablesForm.ClearVariables()

                ' Loop over each parameter
                For Each myRecord In myDoc("CallFlow")
                    Dim recordName As String = myRecord.Name
                    Dim recordData As String = ""

                    Select Case recordName
                        Case "Scale"
                            SetScale(myRecord.FirstChild.Value)

                        Case "Node"
                            Dim myNodeType, myTitle, myData, myInternalReference As String
                            Dim myOutputs As New List(Of SIBBOutputClass)
                            Dim myX, myY As Integer
                            Dim myShowInCallTrace As Boolean

                            myNodeType = ""
                            myTitle = ""
                            myInternalReference = ""
                            myX = 0
                            myY = 0
                            myData = ""
                            myShowInCallTrace = True

                            For Each z As XmlNode In myRecord.ChildNodes
                                Select Case z.Name
                                    Case "XPos"
                                        myX = CInt(z.FirstChild.Value)

                                    Case "YPos"
                                        myY = CInt(z.FirstChild.Value)

                                    Case "NodeType"
                                        myNodeType = ConvertFromXML(z)

                                    Case "Title"
                                        myTitle = ConvertFromXML(z)

                                    Case "InternalReference"
                                        myInternalReference = ConvertFromXML(z)

                                    Case "Data"
                                        myData = ConvertFromXML(z)

                                    Case "ShowInCallTrace"
                                        myShowInCallTrace = CBool(z.FirstChild.Value)

                                    Case "Outputs"
                                        For Each w As XmlNode In z.ChildNodes
                                            If w.Name = "Output" Then
                                                Dim myOutput As SIBBOutputClass
                                                Dim myName As String = ""
                                                Dim myNextNode As Integer = -1
                                                Dim myVisible As Boolean = True
                                                Dim myOrder As Integer = -1

                                                For Each y As XmlNode In w.ChildNodes
                                                    Select Case y.Name
                                                        Case "Name"
                                                            myName = ConvertFromXML(y)

                                                        Case "NextNode"
                                                            myNextNode = CInt(y.FirstChild.Value)

                                                        Case "Visible"
                                                            myVisible = CBool(y.FirstChild.Value)

                                                        Case "Order"
                                                            myOrder = CInt(y.FirstChild.Value)
                                                    End Select
                                                Next

                                                myOutput = New SIBBOutputClass(myName)
                                                myOutput.nextNodeIndex = myNextNode
                                                myOutput.visible = myVisible
                                                myOutput.order = myOrder
                                                myOutputs.Add(myOutput)
                                            End If
                                        Next
                                End Select
                            Next

                            Dim myRef As SIBBClass = GenerateDerivedClass(myNodeType, myMasterBitmap)

                            If myRef IsNot Nothing Then
                                Dim xx, yy, disconnectIndex As Integer

                                myRef.SetNodeTitle(myTitle)
                                myRef.SetInternalReference(myInternalReference)
                                myRef.SetDataFromString(myData)
                                myRef.UnpackData()
                                myRef.absoluteLocation.Set(myX, myY)
                                myRef.SetShowInCallTrace(myShowInCallTrace)

                                For i = 0 To myOutputs.Count - 1
                                    myRef.SetOutputName(i, myOutputs(i).name)
                                    myRef.SetOutputNextNode(i, myOutputs(i).nextNodeIndex)
                                    myRef.SetOutputVisible(i, myOutputs(i).visible)
                                    myRef.SetOutputOrder(i, myOutputs(i).order)
                                Next

                                disconnectIndex = myRef.GetDisconnectOutputIndex()

                                If disconnectIndex >= 0 Then
                                    myRef.SetOutputNextNode(disconnectIndex, ON_DISCONNECT_NODE_NUMBER)
                                    myRef.SetOutputVisible(disconnectIndex, False)
                                End If

                                myRef.SetNodeIndex(sibbList.Count)
                                sibbList.Add(myRef)
                                xx = myRef.absoluteLocation.GetX + GetSIBBWidth()
                                yy = myRef.absoluteLocation.GetY + myRef.GetHeight()

                                maxX = GreatestOf(xx, maxX)
                                maxY = GreatestOf(yy, maxY)
                            End If

                        Case "Variable"
                            ' Load any service wide variables
                            Dim myServiceWideVariable As New ServiceWideVariableClass

                            With myServiceWideVariable
                                For Each z As XmlNode In myRecord.ChildNodes
                                    Select Case z.Name
                                        Case "Name"
                                            If z.FirstChild IsNot Nothing Then .SetName(z.FirstChild.Value)

                                        Case "DefaultValue"
                                            If z.FirstChild IsNot Nothing Then .SetValue(z.FirstChild.Value)

                                        Case "Type"
                                            If z.FirstChild IsNot Nothing Then
                                                ' Check for legacy format where type is stored as string name
                                                If IsInteger(z.FirstChild.Value) Then
                                                    .SetMyType(z.FirstChild.Value)
                                                Else
                                                    .SetMyType(GetVariableTypeFromName(z.FirstChild.Value))
                                                End If
                                            End If

                                        Case "Flags"
                                            If z.FirstChild IsNot Nothing Then .SetFlags(z.FirstChild.Value)
                                    End Select
                                Next
                            End With

                            serviceWideVariablesList.Add(myServiceWideVariable)

                            ' Legacy implementation
                        Case "DDIKeyType"
                            Dim myKey As String = ""

                            For Each ddiKeyElement As XmlNode In myRecord.ChildNodes
                                Select Case ddiKeyElement.Name
                                    Case "Key"
                                        ' myKey = ddiKeyElement.FirstChild.Value

                                    Case "Value"
                                        'myValue = ddiKeyElement.FirstChild.Value
                                        myKey = ddiKeyElement.FirstChild.Value

                                        If myKey = "CallingNumber" Then myKey = "CALLING_NUMBER"
                                        If myKey = "CalledNumber" Then myKey = "CALLED_NUMBER"

                                    Case "Description"
                                        ' myDescription = ddiKeyElement.FirstChild.Value
                                End Select
                            Next

                            AddNewDDIEntry(myKey)

                            ' Legacy implementation
                        Case "DDIKey"
                            ' Load any DDI key data
                            Dim myValue As String = ""
                            Dim myDescription As String = ""
                            Dim myDDIEntryKeys As List(Of String) = GetListOfDDIKeyEntries()

                            For Each z As XmlNode In myRecord.ChildNodes
                                Select Case z.Name
                                    Case "Value"
                                        myValue = z.FirstChild.Value

                                    Case "Description"
                                        myDescription = z.FirstChild.Value

                                        If myDDIEntryKeys.Count = 1 Then AddNewDDIKey(myDDIEntryKeys(0), myValue, myDescription)

                                    Case "DDIVariables"
                                        For Each y As XmlNode In z.ChildNodes
                                            Select Case y.Name
                                                Case "DDIVariable"
                                                    Dim myVariableName As String = ""
                                                    Dim myVariableValue As String = ""

                                                    For Each w As XmlNode In y.ChildNodes
                                                        Select Case w.Name
                                                            Case "Name"
                                                                myVariableName = w.FirstChild.Value

                                                            Case "Value"
                                                                myVariableValue = w.FirstChild.Value
                                                        End Select
                                                    Next

                                                    If myDDIEntryKeys.Count = 1 Then AddDDIVariable(myDDIEntryKeys(0), myValue, myVariableName, myVariableValue)
                                            End Select
                                        Next
                                End Select
                            Next

                            ' New implementation
                        Case "DDIKeyEntries"
                            For Each a As XmlNode In myRecord.ChildNodes
                                Select Case a.Name
                                    Case "DDIKeyEntry"
                                        Dim myDDIEntryKey As String = ""

                                        For Each b As XmlNode In a.ChildNodes
                                            Select Case b.Name
                                                Case "Key"
                                                    myDDIEntryKey = b.FirstChild.Value
                                                    AddNewDDIEntry(myDDIEntryKey)

                                                Case "DDIKeys"
                                                    For Each c As XmlNode In b.ChildNodes
                                                        Select Case c.Name
                                                            Case "DDIKey"
                                                                Dim myDDIKey As String = ""

                                                                For Each d As XmlNode In c.ChildNodes
                                                                    Select Case d.Name
                                                                        Case "Value"
                                                                            myDDIKey = d.FirstChild.Value

                                                                        Case "Description"
                                                                            AddNewDDIKey(myDDIEntryKey, myDDIKey, d.FirstChild.Value)

                                                                        Case "DDIVariables"
                                                                            For Each f As XmlNode In d.ChildNodes
                                                                                Select Case f.Name
                                                                                    Case "DDIVariable"
                                                                                        Dim myName As String = ""

                                                                                        For Each g As XmlNode In f.ChildNodes
                                                                                            Select Case g.Name
                                                                                                Case "Name"
                                                                                                    myName = g.FirstChild.Value

                                                                                                Case "Value"
                                                                                                    AddDDIVariable(myDDIEntryKey, myDDIKey, myName, SafeGetXMLValue(g.FirstChild))
                                                                                            End Select
                                                                                        Next
                                                                                End Select
                                                                            Next
                                                                    End Select
                                                                Next
                                                        End Select
                                                    Next
                                            End Select
                                        Next
                                End Select
                            Next
                    End Select
                Next

                maxX += 100
                maxY += 100

                If maxX > myMasterBitmap.Width Or maxY > myMasterBitmap.Height Then ResizeMasterBitmap(GreatestOf(maxX, myMasterBitmap.Width), GreatestOf(maxY, myMasterBitmap.Height))

                RepaintAll()

                If Not p_readonly Then
                    CloseProjectToolStripMenuItem.Enabled = True
                    SaveToolStripMenuItem.Enabled = True
                    SaveProjectAsToolStripMenuItem.Enabled = True
                    ExportToolStripMenuItem.Enabled = True
                End If
            End If
        End If
    End Sub

    Private Function SafeGetXMLValue(ByRef p_xmlNode As XmlNode) As String
        Dim result As String = ""

        If p_xmlNode IsNot Nothing Then
            If p_xmlNode.Value IsNot Nothing Then result = p_xmlNode.Value
        End If

        Return result
    End Function

    Private Sub NewProjectToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NewProjectToolStripMenuItem.Click
        openFilename = ""
        Text = "Service Designer - New Project"
        SetGUIState(GUIStateType.NULL)
        sibbList.Clear()
        ClearMasterBitmap()
        CreateDefaultBlocks()
        CreateDefaultVariables()
        MyRefresh()
    End Sub

    Private Sub CreateDefaultBlocks()
        Dim x As New StartClass(myMasterBitmap)
        Dim y As New SkipClass(myMasterBitmap)
        Dim z As New DoneClass(myMasterBitmap)
        Dim w As New OnDisconnectClass(myMasterBitmap)
        Dim p As New AbsoluteLocationClass

        CommonNodeAdd(x, p)
        CommonNodeAdd(w, p)
        CommonNodeAdd(y, p)
        w.SetOutput(0, CommonNodeAdd(z, p))
        w.PaintYourself()
    End Sub

    Private Sub CreateDefaultVariables()
        serviceWideVariablesList.Clear()
        ddiVariables.Clear()

        Dim x As New ServiceWideVariableClass

        x.SetMyType(VariableTypes.DATE_TIME_ISO)
        x.SetName("$testDateTime")
        x.SetDeletable(False)
        x.SetAllowEmptyString(True)
        x.SetSanityCheckForNonEmpty(True)

        serviceWideVariablesList.Add(x)

        x = New ServiceWideVariableClass

        x.SetMyType(VariableTypes.BOOLEAN)
        x.SetName("$wildCardSwyxGroupEnabled")
        x.SetDeletable(False)
        x.SetAllowEmptyString(False)
        x.SetSanityCheckForNonEmpty(True)
        x.SetValue("False")

        serviceWideVariablesList.Add(x)
    End Sub

    Private Sub DeleteToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DeleteToolStripMenuItem.Click
        DeleteHandler()
    End Sub

    Private Sub GetDTMFStringToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GetDTMFStringToolStripMenuItem.Click
        Dim x As New GetDTMFStringClass(myMasterBitmap)

        CommonNodeAdd(x)
    End Sub

    Private Sub VoicemailToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles VoicemailToolStripMenuItem.Click
        Dim x As New VoicemailSIBBClass(myMasterBitmap)

        CommonNodeAdd(x)
    End Sub

    Private Sub LongestWaitingAgentToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LongestWaitingAgentToolStripMenuItem.Click
        Dim x As New LongestWaitingSIBBClass(myMasterBitmap)

        CommonNodeAdd(x)
        CheckAgentDataTable()
    End Sub

    Private Sub GroupAvailableToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GroupAvailableToolStripMenuItem.Click
        Dim x As New GroupAvailableSIBBClass(myMasterBitmap)

        CommonNodeAdd(x)
        CheckAgentDataTable()
    End Sub

    Private Sub HoldToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles HoldToolStripMenuItem.Click
        Dim x As New HoldSIBBClass(myMasterBitmap)

        CommonNodeAdd(x)
    End Sub

    Private Sub PauseToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PauseToolStripMenuItem.Click
        Dim x As New PauseSIBBClass(myMasterBitmap)

        CommonNodeAdd(x)
    End Sub

    Private Function UpdateNeedsTable(ByRef p_functionName As String, ByVal p_showBindingInfo As Boolean, ByVal p_showStatus As Boolean) As Boolean
        ' Find the entry in our component list
        Dim myIndex As Integer = AddComponentsForm.GetIndexOfComponentFromFilename(p_functionName)
        Dim myIndexList As New List(Of Integer)
        Dim result As Boolean = False

        If myIndex >= 0 Then
            Dim tableName As String = SIBB_NEEDS_TABLE_NAME
            Dim backupTableName As String = tableName & "_history"
            Dim mySql As String = "select * into " & backupTableName & " from " & tableName
            Dim tableList As New List(Of String)
            Dim tableDefs As New List(Of String)
            Dim myTimeStamp As String = WrapInSingleQuotes(Now.ToString("yyyy-MM-dd HH:mm:ss.fff"))

            LocalDigNeeds(Form1.componentList(myIndex).needs, myIndexList)

            ' Make a backup of the current table
            ' Does the backup table exist ?
            tableList.Add(backupTableName)
            tableDefs.Add("TimeStamp datetime, SIBB varchar(256), Needs varchar(256)")
            TableCheck(Nothing, tableList, tableDefs)

            ' Backup any current version of this table
            If BackupSIBBNeeds() Then
                If p_showStatus Then MsgBox("Backup of " & tableName & " successful")
            Else
                MsgBox(MsgBoxStyle.Critical, "Backup of " & tableName & " failed")
            End If

            ' Add our list to the database, removing existing entries first
            If ExecuteNonQuery(CreateConnectionString(Form1.settingsConfigDictionary), "delete from " & tableName & " where SIBB = " & WrapInSingleQuotes(p_functionName)) Then
                result = True

                For i = 0 To myIndexList.Count - 1
                    Dim x As New SQLStatementClass

                    x.SetInsertIntoTable(tableName)
                    x.AddValue(WrapInSingleQuotes(p_functionName))
                    x.AddValue(WrapInSingleQuotes(Form1.componentList(myIndexList(i)).filename))
                    ExecuteNonQuery(CreateConnectionString(Form1.settingsConfigDictionary), x.GetSQLStatement)
                Next
            End If
        Else
            If p_showBindingInfo Then MsgBox("Warning: File " & WrapInQuotes(p_functionName) & " is not bound")
        End If

        Return result
    End Function

    Private Sub LocalDigNeeds(ByRef p_needs As List(Of ComponentClass.IDType), ByRef p_list As List(Of Integer))
        For i = 0 To p_needs.Count - 1
            Dim myIndex As Integer = AddComponentsForm.GetIndexOfComponentFromId(p_needs(i))

            If myIndex >= 0 Then LocalDigNeeds(Form1.componentList(myIndex).needs, p_list)

            If Not p_list.Contains(myIndex) Then p_list.Add(myIndex)
        Next
    End Sub

    Private Sub SleepToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SleepToolStripMenuItem.Click
        Dim x As New SleepSIBBClass(myMasterBitmap)

        CommonNodeAdd(x)
    End Sub

    Private Sub TraceStartMenuHandler()
        DebugForm.DebugMode = DebugForm.DebugModeEnum.TRACE_MODE
        Text = "Tracing - Waiting for trigger .."
        StartTrace()
    End Sub

    Private Sub StartTrace()
        If Not realTimeCallTraceBackgroundWorker.IsBusy Then realTimeCallTraceBackgroundWorker.RunWorkerAsync()
    End Sub

    Private Sub realTimeCallTraceBackgroundWorker_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles realTimeCallTraceBackgroundWorker.DoWork
        Const CALLID_FIELD_NAME As String = "callId"
        Const TRANSFERRED_TO_CALLID_FIELD_NAME As String = "transferredToCallId"
        Dim worker As BackgroundWorker = TryCast(sender, BackgroundWorker)
        Dim mySql As New SQLStatementClass
        Dim tableName As String = "ServiceBuilderEventTable"
        Dim cdrTableName As String = "IpPbxCDR"
        Dim myTable As New DataTable
        Dim myCallId As Integer = 1
        Dim myCdrCallId As Integer = -1
        Dim currentRow As Integer = -1

        ' Get the highest Call ID so far
        mySql.SetPrimaryTable(tableName)
        mySql.AddSelectString("TOP 1 callId", "")
        mySql.AddOrderByString("callId DESC")

        If FillTableFromCommand(CreateConnectionString(Form1.settingsConfigDictionary), mySql.GetSQLStatement, myTable) Then
            If myTable.Rows.Count >= 1 Then
                If Not myTable.Rows(0).Item(0) Is Nothing Then myCallId = CInt(myTable.Rows(0).Item(0))
            End If
        End If

        If myCallId > 0 Then
            Dim workerRunning As Boolean = True
            Dim waitingForNextCall As Boolean = True
            Dim myScript As String = ""
            Dim myNode As Integer = -1
            Dim sleepTimeMs As Integer = TRACE_SLEEP_TIME_MS
            Dim nodePauseTime As Integer = TRACE_NODE_PAUSE_TIME_MS
            Dim myWorkerRunningStopTime As Date = Nothing
            Dim myWorkerRunningStopTimeFlag As Boolean = False

            If DebugForm.DebugMode = DebugForm.DebugModeEnum.DEBUG_MODE Then
                sleepTimeMs = 250
                nodePauseTime = 250
            End If

            While (workerRunning)
                If worker.CancellationPending Then
                    e.Cancel = True
                    workerRunning = False
                Else
                    If waitingForNextCall Then
                        ' Wait for the next call to start
                        mySql.Clear()

                        mySql.SetPrimaryTable(tableName)
                        mySql.AddSelectString("*", "")
                        mySql.AddCondition("callId > " & myCallId)

                        myTable.Rows.Clear()
                        myTable.Columns.Clear()

                        ' Get all the details for this call so we can trace it
                        If FillTableFromCommand(CreateConnectionString(Form1.settingsConfigDictionary), mySql.GetSQLStatement, myTable) Then
                            If myTable.Rows.Count >= 1 Then
                                If Not myTable.Rows(0).Item(0) Is Nothing Then
                                    myCallId = CInt(myTable.Rows(0).Item(CALLID_FIELD_NAME))
                                    myCdrCallId = myCallId
                                    myScript = myTable.Rows(0).Item("scriptName")
                                    myNode = CInt(myTable.Rows(0).Item("node"))
                                    waitingForNextCall = False
                                    worker.ReportProgress(myNode)
                                End If
                            End If
                        End If
                    Else
                        ' Has the call finished ?
                        mySql.Clear()
                        mySql.SetPrimaryTable(cdrTableName)
                        mySql.AddSelectString(CALLID_FIELD_NAME, "")
                        mySql.AddSelectString(TRANSFERRED_TO_CALLID_FIELD_NAME, "")
                        mySql.AddCondition(CALLID_FIELD_NAME & " = " & myCdrCallId)

                        ' Look for a CDR for this call which means it has finished, unless thers is a transferredToCallId value > 0, in which case wait for that call to have a CDR written
                        If FillTableFromCommand(CreateConnectionString(Form1.settingsConfigDictionary), mySql.GetSQLStatement, myTable) Then
                            If myTable.Rows.Count >= 1 Then
                                If Not myTable.Rows(0).Item(CALLID_FIELD_NAME) Is DBNull.Value Then
                                    If CInt(myTable.Rows(0).Item(CALLID_FIELD_NAME)) = myCdrCallId Then
                                        ' There is a CDR entry written for this call
                                        workerRunning = False

                                        If Not myTable.Rows(0).Item(TRANSFERRED_TO_CALLID_FIELD_NAME) Is DBNull.Value Then
                                            If CInt(myTable.Rows(0).Item(TRANSFERRED_TO_CALLID_FIELD_NAME)) > 0 Then
                                                ' This call has a transfer leg
                                                myCdrCallId = CInt(myTable.Rows(0).Item(TRANSFERRED_TO_CALLID_FIELD_NAME))
                                                workerRunning = True
                                            End If
                                        End If
                                    End If
                                End If
                            End If
                        End If

                        If Not workerRunning Then
                            ' Keep it running long enough to flush all the events out and into the display
                            If Not myWorkerRunningStopTimeFlag Then
                                myWorkerRunningStopTime = Now
                                myWorkerRunningStopTimeFlag = True
                                workerRunning = True
                            Else
                                If DateDiff(DateInterval.Second, myWorkerRunningStopTime, Now) < 5 Then workerRunning = True
                            End If
                        End If

                        ' The main loop
                        ' Get the latest SIBB node
                        Dim latestNode As Integer = -1

                        myTable.Rows.Clear()
                        myTable.Columns.Clear()

                        ' Get all the events for this call in time order
                        If DebugForm.ScanEventTable(myTable, myCallId) Then
                            Dim latestRow As Integer = myTable.Rows.Count - 1

                            While currentRow < latestRow
                                ' Process each new row
                                currentRow += 1

                                With myTable.Rows(currentRow)
                                    Dim myData As String = ""

                                    If Not .Item("data") Is DBNull.Value Then myData = .Item("data")

                                    If .Item("output") Is DBNull.Value Then
                                        ' Entering the node - hilite the heading
                                        latestNode = CInt(.Item("node"))
                                        worker.ReportProgress(TRACE_NODE_MULTIPLIER * latestNode, myData)
                                    Else
                                        If workerRunning Then
                                            Dim myOutput As Integer = CInt(.Item("output"))

                                            ' Exiting the node - keep the existing node hilited, but also hilite the selected output
                                            latestNode = CInt(.Item("node"))
                                            worker.ReportProgress((TRACE_NODE_MULTIPLIER * latestNode) + (myOutput + 1), myData)
                                        End If
                                    End If

                                    System.Threading.Thread.Sleep(TRACE_DELAY_MS)
                                End With
                            End While
                        End If
                    End If
                End If

                System.Threading.Thread.Sleep(sleepTimeMs)
            End While
        End If
    End Sub

    Private Sub realTimeCallTraceBackgroundWorker_RunWorkerCompleted(ByVal sender As System.Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles realTimeCallTraceBackgroundWorker.RunWorkerCompleted
        If e.Cancelled Then
            '  resultLabel.Text = "Canceled!"
        ElseIf e.[Error] IsNot Nothing Then
            ' resultLabel.Text = "Error: " & e.[Error].Message
        Else
            ' resultLabel.Text = "Done!"
        End If

        ' Unselect any selected node
        For i = 0 To sibbList.Count - 1
            If sibbList(i).IsDebugSelected Then
                sibbList(i).DebugSelect(False)
                Exit For
            End If

            If sibbList(i).IsStepDebugSelected Then
                sibbList(i).DebugStepSelect(False)
                Exit For
            End If
        Next

        Text = "Service Designer"

        ExecuteNonQuery(CreateConnectionString(Form1.settingsConfigDictionary), "delete from servicebuilderdebugtable")
        MyRefresh()
    End Sub

    ' Called when ReportProgress() is called
    Private Sub realTimeCallTraceBackgroundWorker_ProgressChanged(ByVal sender As System.Object, ByVal e As System.ComponentModel.ProgressChangedEventArgs) Handles realTimeCallTraceBackgroundWorker.ProgressChanged
        Dim sibbIndex As Integer = e.ProgressPercentage \ TRACE_NODE_MULTIPLIER
        Dim myOutput As Integer = e.ProgressPercentage Mod TRACE_NODE_MULTIPLIER
        Dim showData As Boolean = True

        If DebugForm.DebugMode = DebugForm.DebugModeEnum.TRACE_MODE Then
            '  Text = "Tracing started .."
        End If

        ' Unselect any selected node
        For i = 0 To sibbList.Count - 1
            If sibbList(i).IsDebugSelected Then
                sibbList(i).DebugHistorySelect(True)
                sibbList(i).DebugSelect(False)
                Exit For
            End If

            If sibbList(i).IsStepDebugSelected Then
                sibbList(i).DebugStepSelect(False)
                Exit For
            End If
        Next

        With sibbList(sibbIndex)
            If DebugForm.DebugMode = DebugForm.DebugModeEnum.DEBUG_MODE And myOutput = 0 Then
                sibbList(sibbIndex).DebugStepSelect(True)
            Else
                ' Here for real time call trace mode - subtract 1 from myOutput as zero is not used
                sibbList(sibbIndex).DebugHistorySelect(False)
                sibbList(sibbIndex).DebugSelect(True, myOutput - 1)
            End If

            If showData Then
                Dim myData As String = e.UserState

                Text = "Debug: " & "Type = " & .GetTypeName & "   Title = " & .GetNodeTitle & "   Data = " & WrapInQuotes(myData)
            End If
        End With

        MyRefresh()
    End Sub

    Private Sub EndCallToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EndCallToolStripMenuItem.Click
        Dim x As New EndCallClass(myMasterBitmap)

        CommonNodeAdd(x)
    End Sub

    Private Sub DayOfWeekToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DayOfWeekToolStripMenuItem.Click
        Dim x As New DayOfWeekClass(myMasterBitmap)

        CommonNodeAdd(x)
    End Sub

    Private Sub TimeOfDayToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TimeOfDayToolStripMenuItem.Click
        Dim x As New TimeOfDayClass(myMasterBitmap)

        CommonNodeAdd(x)
    End Sub

    Public Function CommonNodeAdd(ByRef p_node As SIBBClass, Optional ByRef p_location As AbsoluteLocationClass = Nothing) As Integer
        Dim maxX, maxY As Integer

        If p_location Is Nothing Then
            ' Clicked on new node from menu
            SetGUIState(GUIStateType.DROPPING_NEW_NODE)
            lastFramePos.Set(0, 0)
            p_node.DrawFrame(lastFramePos)
            MyRefresh()
        Else
            If p_location.IsNull Then
                ' Default nodes being added
                CalculateNextSIBBPosition(p_node)
            Else
                ' Nodes being added from project file with position already determined
                p_node.absoluteLocation.Set(p_location.GetX, p_location.GetY)
            End If
        End If

        If p_node.GetDisconnectOutputIndex() >= 0 Then p_node.SetOutputNextNode(p_node.GetDisconnectOutputIndex, ON_DISCONNECT_NODE_NUMBER)

        p_node.SetNodeIndex(sibbList.Count)
        sibbList.Add(p_node)

        If GetGUIState() <> GUIStateType.DROPPING_NEW_NODE Then
            maxX = p_node.absoluteLocation.GetX + GetSIBBWidth()
            maxY = p_node.absoluteLocation.GetY + p_node.GetHeight

            If maxX > myMasterBitmap.Width Or maxY > myMasterBitmap.Height Then ResizeMasterBitmap(GreatestOf(maxX, myMasterBitmap.Width), GreatestOf(maxY, myMasterBitmap.Height))

            p_node.PaintYourself()
            MyRefresh()
        End If

        Return sibbList.Count - 1
    End Function

    Private Sub DesignerForm_ResizeEnd(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.ResizeEnd
        ResizeHandler()
    End Sub

    Private Sub ResizeHandler()
        If Not Me.WindowState = FormWindowState.Minimized Then
            If allowResize Then
                ' Resize the canvas picture box
                canvasPictureBox.Size = New Point(Size.Width - canvasX, Size.Height - canvasY)

                ' Create a new view bitmap of the canvas size
                myViewBitmap.Dispose()
                myViewBitmap = Nothing
                myViewBitmap = New Bitmap(canvasPictureBox.Size.Width, canvasPictureBox.Size.Height)
                canvasPictureBox.Image = myViewBitmap

                ' Make sure the master bitmap is at least as big as the view bitmap
                If myMasterBitmap.Width < myViewBitmap.Width Or myMasterBitmap.Height < myViewBitmap.Height Then ResizeMasterBitmap(GreatestOf(myViewBitmap.Width, myMasterBitmap.Width), GreatestOf(myViewBitmap.Height, myMasterBitmap.Height))

                If myViewBitmap.Width < myMasterBitmap.Width Or myViewBitmap.Height < myMasterBitmap.Height Then
                    Dim myNewMasterWidth As Integer = GreatestOf(myViewBitmap.Width, GetRequiredWidth())
                    Dim myNewMasterHeight As Integer = GreatestOf(myViewBitmap.Height, GetRequiredHeight())

                    ResizeMasterBitmap(myNewMasterWidth, myNewMasterHeight)
                End If

                HScrollBar1.Size = New Point(Size.Width - HScrollX, HScrollBar1.Size.Height)
                HScrollBar1.Location = New Point(HScrollBar1.Location.X, Size.Height - HScrollY)
                VScrollBar1.Size = New Point(VScrollBar1.Size.Width, Size.Height - VScrollY)
                VScrollBar1.Location = New Point(Size.Width - VScrollX, VScrollBar1.Location.Y)

                'Text = "Master(" & myMasterBitmap.Width & ", " & myMasterBitmap.Height & ")   View(" & myViewBitmap.Width & ", " & myViewBitmap.Height & "), Scroll = " & HScrollBar1.Value
                HScrollBar1.Value = 0
                VScrollBar1.Value = 0
                RepaintAll()
            End If
        End If
    End Sub

    Private Function GetRequiredWidth() As Integer
        Dim result As Integer = 0

        For i = 0 To sibbList.Count - 1
            result = GreatestOf(result, sibbList(i).absoluteLocation.GetX)
        Next

        Return result + GetSIBBWidth() + 20
    End Function

    Private Function GetRequiredHeight() As Integer
        Dim result As Integer = 0

        For i = 0 To sibbList.Count - 1
            result = GreatestOf(result, sibbList(i).absoluteLocation.GetY + sibbList(i).GetHeight)
        Next

        Return result + 20
    End Function

    Public Sub ResizeMasterBitmap(ByVal p_width As Integer, ByVal p_height As Integer)
        myMasterBitmap.Dispose()
        myMasterBitmap = Nothing
        myMasterBitmap = New Bitmap(p_width, p_height)

        For i = 0 To sibbList.Count - 1
            sibbList(i).SetBitmapRef(myMasterBitmap)
        Next
    End Sub

    Private Sub VBScriptToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles VBScriptToolStripMenuItem.Click
        Dim x As New VBScriptClass(myMasterBitmap)

        x.linesOfCode.Add("myUseExit = 0")
        CommonNodeAdd(x)
    End Sub

    Private Sub GetDTMFDigitToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GetDTMFDigitToolStripMenuItem.Click
        Dim x As New GetDTMFDigitClass(myMasterBitmap)

        CommonNodeAdd(x)
    End Sub

    Private Sub HScrollBar1_Scroll(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ScrollEventArgs) Handles HScrollBar1.Scroll
        MyRefresh()
    End Sub

    Private Sub VScrollBar1_Scroll(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ScrollEventArgs) Handles VScrollBar1.Scroll
        MyRefresh()
    End Sub

    Private Sub ExitToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExitToolStripMenuItem.Click
        Close()
    End Sub

    Private Sub GenerateScriptToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GenerateScriptToolStripMenuItem.Click
        ScriptForm.ShowDialog()
    End Sub

    Private Sub CalculateChecksumToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    End Sub

    Private Sub FormatVBS(ByRef p_textBox As RichTextBox, ByVal p_code As String, ByVal p_colour As Color)
        Dim crBefore As Boolean = False
        Dim crAfter As Boolean = False
        Dim tabDelta As Integer = 0
        Dim tabString As String = ""
        Dim myArray As String() = p_textBox.Tag.split(",")
        Dim previousTab As Integer = CInt(myArray(0))
        Dim currentTab As Integer = CInt(myArray(1))

        p_textBox.SelectionColor = p_colour

        If p_code.ToLower.StartsWith("else") Then tabDelta = -1
        If p_code.ToLower.StartsWith("end if") Then tabDelta = -1
        If p_code.ToLower.StartsWith("end function") Then tabDelta = -1
        If p_code.ToLower.StartsWith("end select") Then tabDelta = -2
        If p_code.ToLower.StartsWith("case") Then tabDelta = -1

        If tabDelta < 0 Then currentTab = currentTab + tabDelta

        '      If ReferenceEquals(p_textBox, DeltaForm.fileTextBox) Then MsgBox(previousTab & ", " & currentTab & " : " & p_code)

        If p_code.ToLower.StartsWith("function") Then tabDelta = 1

        If p_code.ToLower.StartsWith("if") Then
            If p_code.ToLower.EndsWith("then") Then
                tabDelta = 1
            Else
                crAfter = True
            End If

            If currentTab = previousTab Then crBefore = True
        End If

        If p_code.ToLower.StartsWith("select") Then
            tabDelta = 2

            If currentTab = previousTab Then crBefore = True
        End If

        If p_code.ToLower.StartsWith("case") Then
            tabDelta = 1
        End If

        If p_code.ToLower.StartsWith("else") Then tabDelta = 1


        If crBefore Then p_textBox.AppendText(vbCrLf)

        previousTab = currentTab

        For i = 0 To currentTab - 1
            tabString &= "  "
        Next

        p_textBox.AppendText(tabString & p_code & vbCrLf)

        If crAfter Then p_textBox.AppendText(vbCrLf)

        If tabDelta > 0 Then currentTab = previousTab + tabDelta

        p_textBox.Tag = previousTab & "," & currentTab
    End Sub

    Private Sub CopyToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CopyToolStripMenuItem.Click
        myCopyList.Clear()

        ' Any nodes hilighted ?
        For i = 0 To sibbList.Count - 1
            If sibbList(i).IsHilighted Then
                If sibbList(i).CanDelete Then
                    myCopyList.Add(i)
                End If
            End If
        Next
    End Sub

    Private Sub PasteToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PasteToolStripMenuItem.Click
        PasteHandler()
    End Sub

    Private Sub CutToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CutToolStripMenuItem.Click

    End Sub

    Private Sub DesignerForm_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        KeyPressedHandler(e)
        e.Handled = True
    End Sub

    Private Sub DesignerForm_SizeChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.SizeChanged
        'ResizeHandler()
    End Sub

    Private Sub HolidayRoutingToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles HolidayRoutingToolStripMenuItem.Click
        Dim x As New HolidayClass(myMasterBitmap)
        Dim tableList As New List(Of String)
        Dim defList As New List(Of String)

        CommonNodeAdd(x)

        ' Our database schema has 2 extra fields: customeOpeningTime TIME(7) and customClosingTime TIME(7) not used for Ecourier - probably used for Infront
        tableList.Add("BankHolidayTable")
        defList.Add("date varchar(256), description varchar(256)")
        TableCheck("HolidayRouting", tableList, defList)
    End Sub

    Private Sub DebugCallToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DebugCallToolStripMenuItem.Click
        Dim mySql As New SQLStatementClass
        Dim tableName As String = "ServiceBuilderDebugTable"

        If ExecuteNonQuery(CreateConnectionString(Form1.settingsConfigDictionary), "delete from " & tableName) Then
            If ExecuteNonQuery(CreateConnectionString(Form1.settingsConfigDictionary), "insert into " & tableName & " values (NULL, 0, 'STEP')") Then
                DebugForm.DebugMode = DebugForm.DebugModeEnum.DEBUG_MODE
                StartTrace()
            End If
        End If
    End Sub

    Private Sub DesignerForm_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        KeyDownHandler(e)
    End Sub

    Private Sub InfoToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles InfoToolStripMenuItem.Click
        InfoForm.ShowDialog()
    End Sub

    Private Sub CreateCallMachineNode(ByRef p_title As String, ByRef p_code As String)
        Dim x As New VBScriptClass(myMasterBitmap)

        x.SetNodeTitle(p_title)
        x.linesOfCode.Add("myCallMachine." & p_code & ".Run")
        CommonNodeAdd(x)
    End Sub

    Private Sub InHoursToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles InHoursToolStripMenuItem.Click
        CreateCallMachineNode("On In Hours", "onInHours")
    End Sub

    Private Sub OutOfHoursToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OutOfHoursToolStripMenuItem.Click
        CreateCallMachineNode("On Out Of Hours", "onOutOfHours")
    End Sub

    Private Sub ConnectToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ConnectToolStripMenuItem1.Click
        CreateCallMachineNode("On Connect", "onCallConnect")
    End Sub

    Private Sub DeliveredToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DeliveredToolStripMenuItem.Click
        CreateCallMachineNode("On Delivered", "onCallDelivered")
    End Sub

    Private Sub DisconnectedToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DisconnectedToolStripMenuItem.Click
        CreateCallMachineNode("On Disconnect", "onCallDisconnect")
    End Sub

    Private Sub StartToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles StartToolStripMenuItem.Click
        CreateCallMachineNode("On Start", "onCallStart")
    End Sub

    Private Sub VariablesToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles VariablesToolStripMenuItem.Click
        ' Copy the Service Wide variables into the datagridview
        VariablesForm.serviceWideVariablesDataGridView.Rows.Clear()

        For i = 0 To serviceWideVariablesList.Count - 1
            With serviceWideVariablesList(i)
                VariablesForm.serviceWideVariablesDataGridView.Rows.Add(.GetName, .GetValue, GetVariableTypeNameFromType(.GetMyType), .GetDeletable, .GetAllowEmptyString, .GetSanityCheckForNonEmpty)
            End With
        Next

        ' Make a copy of the current DDI Variables in case we cancel any changes
        CopyDDIVariables(ddiVariables, backup_ddiVariables)

        If VariablesForm.ShowDialog() = Windows.Forms.DialogResult.OK Then
            ' Copy Service Wide variables back from the datagridview
            serviceWideVariablesList.Clear()

            With VariablesForm.serviceWideVariablesDataGridView
                For i = 0 To .RowCount - 1
                    With .Rows(i)
                        Dim x As New ServiceWideVariableClass

                        x.SetName(.Cells(0).Value)
                        x.SetValue(.Cells(1).Value)
                        x.SetMyType(GetVariableTypeFromName(.Cells(2).Value))
                        x.SetDeletable(.Cells(3).Value)
                        x.SetAllowEmptyString(.Cells(4).Value)
                        x.SetSanityCheckForNonEmpty(.Cells(5).Value)

                        serviceWideVariablesList.Add(x)
                    End With
                Next
            End With
        Else
            ' Copy the existing DDI Variables back from the backup
            CopyDDIVariables(backup_ddiVariables, ddiVariables)
        End If
    End Sub

    Private Sub ReplayCallToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ReplayCallToolStripMenuItem.Click
    End Sub

    Private Sub RunToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RunToolStripMenuItem.Click
        Dim myTable As New DataTable
        Dim showData As Boolean = True

        If DebugForm.ScanEventTable(myTable, 0) Then
            For j = 0 To myTable.Rows.Count - 1
                With myTable.Rows(j)
                    Dim sibbIndex As Integer = .Item("node")
                    Dim myOutput As Integer = 0

                    ' Unselect any selected node
                    For i = 0 To sibbList.Count - 1
                        If sibbList(i).IsDebugSelected Then
                            sibbList(i).DebugSelect(False)
                            Exit For
                        End If

                        If sibbList(i).IsStepDebugSelected Then
                            sibbList(i).DebugStepSelect(False)
                            Exit For
                        End If
                    Next

                    If Not .Item("output") Is DBNull.Value Then myOutput = .Item("output") + 1

                    With sibbList(sibbIndex)
                        If DebugForm.DebugMode = DebugForm.DebugModeEnum.DEBUG_MODE And myOutput = 0 Then
                            sibbList(sibbIndex).DebugStepSelect(True)
                        Else
                            sibbList(sibbIndex).DebugSelect(True, myOutput - 1)
                        End If

                        If showData Then
                            Dim myData As String = myTable.Rows(j).Item("data")

                            Text = "Debug: " & "Type = " & .GetTypeName & "   Title = " & .GetNodeTitle & "   Data = " & WrapInQuotes(myData)
                        End If
                    End With

                    MyRefresh()
                    System.Threading.Thread.Sleep(2000)
                End With
            Next
        End If
    End Sub

    Private Sub RestoreToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RestoreToolStripMenuItem.Click
        ' Find all the scripts in the database which have a backup to restore to
        Dim myTable As New DataTable
        Dim myCommand As String = "select distinct a.scriptName, b.scriptName AS [backupScriptName], " _
        & "CONVERT(datetime, SUBSTRING(b.scriptName, Len(a.scriptName) + 11, 4) " _
        & "+ '-' + substring(b.scriptName, len(a.scriptName) + 8, 2) " _
        & "+ '-' + substring(b.scriptName, len(a.scriptName) + 5, 2) " _
        & "+ ' ' + SUBSTRING(b.scriptName, len(a.scriptName) + 16, 8), 120) AS [backupDateTime] " _
        & "from ServiceBuilderTable as a left join ServiceBuilderTable as b on b.scriptName like '** ' + a.scriptName + ' __/__/____ __:__:__ **' " _
        & "where a.scriptName not like '** % **' and b.scriptName is not null " _
        & "order by a.scriptName, " _
        & "CONVERT(datetime, SUBSTRING(b.scriptName, Len(a.scriptName) + 11, 4) " _
        & "+ '-' + substring(b.scriptName, len(a.scriptName) + 8, 2) " _
        & "+ '-' + substring(b.scriptName, len(a.scriptName) + 5, 2) " _
        & "+ ' ' + SUBSTRING(b.scriptName, len(a.scriptName) + 16, 8), 120) DESC"

        RestoreForm.myBackups.Clear()
        RestoreForm.scriptNameComboBox.Items.Clear()

        If FillTableFromCommand(CreateConnectionString(Form1.settingsConfigDictionary), myCommand, myTable) Then
            If myTable.Columns.Count = 3 Then
                For i = 0 To myTable.Rows.Count - 1
                    With myTable.Rows(i)
                        If .Item("scriptName") IsNot DBNull.Value Then
                            If .Item("backupScriptName") IsNot DBNull.Value Then
                                Dim myScriptName As String = .Item("scriptName")
                                Dim myBackupScriptName As String = .Item("backupScriptName")

                                If Not RestoreForm.myBackups.ContainsKey(myScriptName) Then
                                    RestoreForm.myBackups.Add(myScriptName, New List(Of String))
                                    RestoreForm.scriptNameComboBox.Items.Add(myScriptName)
                                End If

                                RestoreForm.myBackups.Item(myScriptName).Add(myBackupScriptName)
                            End If
                        End If
                    End With
                Next
            End If
        End If

        With RestoreForm
            If .scriptNameComboBox.Items.Count > 0 Then .scriptNameComboBox.SelectedIndex = 0
        End With

        If RestoreForm.ShowDialog = Windows.Forms.DialogResult.OK Then
            Dim myScriptName As String = RestoreForm.scriptNameComboBox.Text
            Dim myBackupScriptName As String = RestoreForm.backupComboBox.Text

            If MsgBox("You have chosen to restore script " & WrapInQuotes(myScriptName) & " from " & WrapInQuotes(myBackupScriptName) & vbCrLf & "Do you want to proceed ?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                ' Backup the existing script
                Dim newBackupScriptName = "** " & myScriptName & " " & Now & " **"

                myCommand = "insert into " & SERVICEBUILDER_TABLE_NAME & " select " & WrapInSingleQuotes(newBackupScriptName) & ", nodeNumber, nodeType, data, outputs, title, internalReference from " & SERVICEBUILDER_TABLE_NAME & " where scriptName = " & WrapInSingleQuotes(myScriptName)

                If ExecuteNonQuery(CreateConnectionString(Form1.settingsConfigDictionary), myCommand) Then
                    ' Erase the current script from the database
                    myCommand = "delete from " & SERVICEBUILDER_TABLE_NAME & " where scriptName = " & WrapInSingleQuotes(myScriptName)

                    If ExecuteNonQuery(CreateConnectionString(Form1.settingsConfigDictionary), myCommand) Then
                        ' Activate the restored script
                        myCommand = "insert into " & SERVICEBUILDER_TABLE_NAME & " select " & WrapInSingleQuotes(myScriptName) & ", nodeNumber, nodeType, data, outputs, title, internalReference from " & SERVICEBUILDER_TABLE_NAME & " where scriptName = " & WrapInSingleQuotes(myBackupScriptName)

                        If ExecuteNonQuery(CreateConnectionString(Form1.settingsConfigDictionary), myCommand) Then
                            MsgBox("Restore completed ..")
                        Else
                            MsgBox("Error: Could not activate the restored script " & WrapInQuotes(myScriptName))
                        End If
                    Else
                        MsgBox("Error: Could not delete script " & WrapInQuotes(myScriptName) & vbCrLf & "Restore aborted ..")
                    End If
                Else
                    MsgBox("Error: Could not backup script " & WrapInQuotes(myScriptName) & vbCrLf & "Restore aborted ..")
                End If
            End If
        End If
    End Sub

    Private Sub SaveProjectAsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaveProjectAsToolStripMenuItem.Click
        SaveHandler(True)
    End Sub

    Private Sub TestCallFlowToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TestCallFlowToolStripMenuItem.Click
        Dim myIndex As Integer = 0
        Dim running As Boolean = True
        Dim myKey As String = ""

        If TestForm.ShowDialog = Windows.Forms.DialogResult.OK Then
            DebugForm.DebugMode = DebugForm.DebugModeEnum.TRACE_MODE

            ' Build the variablesDictionary
            With TestForm
                ' ****
                Dim myDescription As String = VariablesForm.swyxVariableNameComboBox.SelectedItem

                .ClearDictionary()

                ' Get the DDI key if present
                myKey = .ddiKeyTextBox.Text

                ' Add any entries from the DDI specific script first
                For i = 0 To .ecrScriptRichTextBox.Lines.Length - 1
                    Dim x As String = .ecrScriptRichTextBox.Lines(i).Trim

                    If x.StartsWith(TestForm.VARIABLES_DICTIONARY_NAME) Then
                        x = x.Substring(TestForm.VARIABLES_DICTIONARY_NAME.Length)

                        If x.StartsWith(".") Then
                            x = x.Substring(".".Length)

                            If x.ToLower.StartsWith("add ") Then
                                x = x.Substring("add ".Length)

                                Dim myArray() As String = x.Split(",")

                                ' Get the variable name
                                .SetVar(myArray(0).Trim.Trim(Chr(34)), myArray(1).Trim.Trim(Chr(34)))
                            End If
                        End If
                    End If
                Next

                ' Now add from the global variables section of the call flow
                For i = 0 To GetNumberOfServiceWideVariables() - 1
                    .SetVar(GetServiceWideVariableName(i), GetServiceWideVariableValue(i))
                Next

                ' Now add any DDI specific variables or values
                Dim myDDIVariablesNames As List(Of String) = GetDDIVariableNameList(MapDescriptionToKey(myDescription), myKey)

                For Each myDDIVariableName As String In myDDIVariablesNames
                    .SetVar(myDDIVariableName, GetDDIVariableValue(MapDescriptionToKey(myDescription), myKey, myDDIVariableName))
                Next
            End With

            While running
                With sibbList(myIndex)
                    Dim myData As String = ""

                    myData = .GetData
                    .DebugStepSelect(True)

                    MyRefresh()
                    System.Threading.Thread.Sleep(2000)

                    Dim outputIndex As Integer = .Run(myData)
                    .DebugSelect(True, outputIndex)

                    MyRefresh()
                    System.Threading.Thread.Sleep(2000)

                    'Deselect previous node
                    .DebugStepSelect(False)
                    .DebugSelect(False, outputIndex)

                    If outputIndex = -1 Then
                        running = False
                    Else
                        myIndex = .outputs(outputIndex).nextNodeIndex

                        If myIndex = -1 Then running = False
                    End If
                End With
            End While

            MyRefresh()
            DebugForm.DebugMode = DebugForm.DebugModeEnum.NULL_MODE
        End If
    End Sub

    Public Function GetBuildInfoFromDatabase(ByRef p_source As String, ByRef p_timestamp As String, ByRef p_newCallFlow As Boolean, ByRef p_scriptNames As List(Of String)) As Boolean
        Dim result As Boolean = False
        Dim tableName As String = SERVICEBUILDER_TABLE_NAME
        Dim mySql As New SQLStatementClass
        Dim myTable As New DataTable
        Dim buildText As String = "'Build Info : Source = "

        mySql.AddSelectString("scriptName", "")
        mySql.AddSelectString("data", "")
        mySql.SetPrimaryTable(tableName)
        mySql.AddCondition("nodeNumber = 0")
        p_timestamp = ""
        p_newCallFlow = False

        If p_scriptNames.Count > 0 Then mySql.AddCondition("scriptName = " & WrapInSingleQuotes(p_scriptNames(0)))

        If Not p_source = "" Then
            mySql.AddCondition("data like " & WrapInSingleQuotes(buildText & WrapInQuotes(p_source) & "%"))
            mySql.AddCondition("scriptName not like '** %'")
        End If

        If FillTableFromCommand(CreateConnectionString(Form1.settingsConfigDictionary), mySql.GetSQLStatement, myTable) Then
            result = True

            If p_source = "" Then
                If myTable.Rows.Count > 0 Then
                    With myTable.Rows(0)
                        If myTable.Columns.Count > 1 Then
                            Dim myData As String = ""

                            If Not .Item(1) Is DBNull.Value Then myData = .Item(1)

                            If myData <> "" Then
                                ' Can we extract any previous build info ?
                                If myData.StartsWith(buildText) Then
                                    myData = myData.Substring(myData.IndexOf(Chr(34)) + 1)

                                    p_source = myData.Substring(0, myData.IndexOf(Chr(34)))
                                End If

                                If myData.Contains("Source modified ") Then p_timestamp = myData.Substring(myData.IndexOf("Source modified ") + "Source modified ".Length, 20)
                            End If
                        End If
                    End With
                Else
                    p_newCallFlow = True
                End If
            Else
                For i = 0 To myTable.Rows.Count - 1
                    With myTable.Rows(i)
                        If Not .Item(0) Is DBNull.Value Then p_scriptNames.Add(.Item(0))
                    End With
                Next
            End If
        End If

        Return result
    End Function

    Public Sub RemoveDDIKey(ByRef p_ddiEntryKey As String, ByRef p_ddiKey As String)
        ddiVariables.Item(p_ddiEntryKey).matchValues.Remove(p_ddiKey)
    End Sub

    Public Function DoesDDIKeyExist(ByRef p_ddiEntryKey As String, ByRef p_ddiKey As String) As Boolean
        Dim result As Boolean = False

        If ddiVariables.ContainsKey(p_ddiEntryKey) Then
            If ddiVariables.Item(p_ddiEntryKey).matchValues.ContainsKey(p_ddiKey) Then
                result = ddiVariables.Item(p_ddiEntryKey).matchValues.ContainsKey(p_ddiKey)
            End If
        End If

        Return result
    End Function

    Public Sub AddNewDDIKey(ByRef p_ddiEntryKey As String, ByRef p_ddiKey As String, ByRef p_description As String)
        ' Do we have an entry for this type of Swyx Variable ?
        If Not ddiVariables.ContainsKey(p_ddiEntryKey) Then ddiVariables.Add(p_ddiEntryKey, New DDIKeyEntryClass(p_ddiEntryKey))

        ddiVariables.Item(p_ddiEntryKey).matchValues.Add(p_ddiKey, New ValueExpressionClass(p_description))
    End Sub

    Public Function GetDDIVariableNameList(ByRef p_ddiEntryKey As String, ByRef p_ddiKey As String) As List(Of String)
        Dim myList As List(Of String) = Nothing

        If ddiVariables.ContainsKey(p_ddiEntryKey) Then
            If ddiVariables.Item(p_ddiEntryKey).matchValues.ContainsKey(p_ddiKey) Then
                myList = ddiVariables.Item(p_ddiEntryKey).matchValues.Item(p_ddiKey).variables.Keys.ToList
            End If
        End If

        If myList Is Nothing Then myList = New List(Of String)

        Return myList
    End Function

    Public Function DoesDDIVariableExist(ByRef p_ddiEntryKey As String, ByRef p_ddiKey As String, ByRef p_ddiVariableName As String) As Boolean
        Return ddiVariables.Item(p_ddiEntryKey).matchValues.Item(p_ddiKey).variables.ContainsKey(p_ddiVariableName)
    End Function

    Public Function GetDDIVariableValue(ByRef p_ddiEntryKey As String, ByVal p_ddiKey As String, ByRef p_ddiVariableName As String) As String
        Return ddiVariables.Item(p_ddiEntryKey).matchValues.Item(p_ddiKey).variables.Item(p_ddiVariableName)
    End Function

    Public Sub RemoveDDIVariable(ByRef p_ddiEntryKey As String, ByRef p_ddiKey As String, ByRef p_ddiVariableName As String)
        ddiVariables.Item(p_ddiEntryKey).matchValues.Item(p_ddiKey).variables.Remove(p_ddiVariableName)
    End Sub

    Public Sub SetDDIVariable(ByRef p_ddiEntryKey As String, ByRef p_ddiKey As String, ByRef p_ddiVariableName As String, ByRef p_ddiVariableValue As String)
        With ddiVariables.Item(p_ddiEntryKey).matchValues.Item(p_ddiKey).variables
            If .ContainsKey(p_ddiVariableName) Then
                .Item(p_ddiVariableName) = p_ddiVariableValue
            Else
                .Add(p_ddiVariableName, p_ddiVariableValue)
            End If
        End With
    End Sub

    Public Function AddDDIVariable(ByRef p_ddiTypeKey As String, ByRef p_ddiKey As String, ByRef p_ddiVariableName As String, ByRef p_ddiVariableValue As String) As Boolean
        Dim result As Boolean = True

        If Not ddiVariables.ContainsKey(p_ddiTypeKey) Then ddiVariables.Add(p_ddiTypeKey, New DDIKeyEntryClass(p_ddiTypeKey))

        With ddiVariables.Item(p_ddiTypeKey).matchValues
            If Not .ContainsKey(p_ddiKey) Then .Add(p_ddiKey, New ValueExpressionClass(""))

            If .Item(p_ddiKey).variables.ContainsKey(p_ddiVariableName) Then
                result = False
            Else
                .Item(p_ddiKey).variables.Add(p_ddiVariableName, p_ddiVariableValue)
            End If
        End With

        Return result
    End Function

    Public Sub ClearDDIVariables()
        ddiVariables.Clear()
    End Sub

    Public Function GetNumberOfDDIKeyEntries() As Integer
        Return ddiVariables.Count
    End Function

    Private Sub CopyDDIVariables(ByRef p_source As Dictionary(Of String, DDIKeyEntryClass), ByRef p_dest As Dictionary(Of String, DDIKeyEntryClass))
        p_dest.Clear()

        For Each ddiTypeKey As String In p_source.Keys.ToList
            p_dest.Add(ddiTypeKey, New DDIKeyEntryClass(ddiTypeKey))

            With p_source.Item(ddiTypeKey)
                p_dest.Item(ddiTypeKey).SetDisplayedExpression(.GetDisplayedExpression)
                p_dest.Item(ddiTypeKey).SetSwyxVariableExpression(.GetSwyxVariableExpression)

                For Each ddiKey As String In .matchValues.Keys.ToList
                    With .matchValues.Item(ddiKey)
                        Dim x As New ValueExpressionClass(.GetDescription)

                        For Each variableName As String In .variables.Keys.ToList
                            x.variables.Add(variableName, .variables.Item(variableName))
                        Next

                        p_dest.Item(ddiTypeKey).matchValues.Add(ddiKey, x)
                    End With
                Next
            End With
        Next
    End Sub

    Public Function GetListOfDDIKeyEntries() As List(Of String)
        Return ddiVariables.Keys.ToList
    End Function

    Public Function GetDDIKeyEntryValue(ByRef p_key As String) As String
        Return ddiVariables.Item(p_key).GetSwyxVariableExpression
    End Function

    Public Function GetDDIKeyEntryDescription(ByRef p_key As String) As String
        Return ddiVariables.Item(p_key).GetDisplayedExpression
    End Function

    Public Function GetListOfDDIKeysForThisEntry(ByRef p_key As String) As List(Of String)
        Dim x As List(Of String) = Nothing

        If ddiVariables.ContainsKey(p_key) Then
            x = ddiVariables.Item(p_key).matchValues.Keys.ToList
        Else
            x = New List(Of String)
        End If

        Return x
    End Function

    Public Function GetDDIKeyDescription(ByRef p_key As String, ByRef p_subKey As String) As String
        Return ddiVariables.Item(p_key).matchValues.Item(p_subKey).GetDescription
    End Function

    Public Function GetListOfDDIVariables(ByRef p_key As String, ByRef p_subKey As String) As List(Of String)
        Dim result As List(Of String) = Nothing

        If ddiVariables.ContainsKey(p_key) Then
            If ddiVariables.Item(p_key).matchValues.ContainsKey(p_subKey) Then result = ddiVariables.Item(p_key).matchValues.Item(p_subKey).variables.Keys.ToList
        End If

        If result Is Nothing Then result = New List(Of String)

        Return result
    End Function

    Public Function MapDescriptionToKey(ByRef p_description As String) As String
        Dim result As String = ""

        For i = 0 To ddiVariableTypes.Count - 1
            If ddiVariableTypes(i).GetDescription = p_description Then
                result = ddiVariableTypes(i).GetKey
                Exit For
            End If
        Next

        Return result
    End Function

    Public Sub AddNewDDIEntry(ByRef p_ddiEntryKey)
        Dim myIndex As Integer = -1

        ' Look for match on p_ddiEntryKey in ddiVariableTypes
        For i = 0 To ddiVariableTypes.Count - 1
            If ddiVariableTypes(i).GetKey = p_ddiEntryKey Then
                myIndex = i
                Exit For
            End If
        Next

        If myIndex >= 0 Then
            Dim x As New DDIKeyEntryClass(p_ddiEntryKey)

            With ddiVariableTypes(myIndex)
                x.SetSwyxVariableExpression(.GetSwyxCommand)
                x.SetDisplayedExpression(.GetDescription)
            End With

            ddiVariables.Add(p_ddiEntryKey, x)
        End If
    End Sub

    Public Function GetNumberOfServiceWideVariables() As Integer
        Return serviceWideVariablesList.Count
    End Function

    Public Function GetServiceWideVariableName(ByVal p_index As Integer) As String
        Return serviceWideVariablesList(p_index).GetName
    End Function

    Public Function GetServiceWideVariableValue(ByVal p_index As Integer) As String
        Return serviceWideVariablesList(p_index).GetValue
    End Function

    Public Function GetServiceWideVariableValue(ByRef p_name As String) As String
        Dim myValue As String = ""

        For i = 0 To GetNumberOfServiceWideVariables() - 1
            If GetServiceWideVariableName(i) = p_name Then
                myValue = GetServiceWideVariableValue(i)
                Exit For
            End If
        Next

        Return myValue
    End Function

    Public Function GetServiceWideVariableType(ByVal p_index As Integer) As String
        Return serviceWideVariablesList(p_index).GetMyType
    End Function

    Public Function GetServiceWideVariableType(ByRef p_name As String) As String
        Dim myType As String = ""

        For i = 0 To GetNumberOfServiceWideVariables() - 1
            If GetServiceWideVariableName(i) = p_name Then
                myType = GetVariableTypeNameFromType(GetServiceWideVariableType(i))
                Exit For
            End If
        Next

        Return myType
    End Function

    Public Function GetServiceWideVariableDeletable(ByVal p_index As Integer) As Boolean
        Return serviceWideVariablesList(p_index).GetDeletable
    End Function

    Public Function GetServiceWideVariableDeletable(ByRef p_name As String) As String
        Dim result As Boolean = False

        For i = 0 To GetNumberOfServiceWideVariables() - 1
            If GetServiceWideVariableName(i) = p_name Then
                result = GetServiceWideVariableDeletable(i)
                Exit For
            End If
        Next

        Return result
    End Function

    Public Function GetServiceWideVariableAllowEmptyString(ByVal p_index As Integer) As Boolean
        Return serviceWideVariablesList(p_index).GetAllowEmptyString
    End Function

    Public Function GetServiceWideVariableAllowEmptyString(ByRef p_name As String) As String
        Dim result As Boolean = False

        For i = 0 To GetNumberOfServiceWideVariables() - 1
            If GetServiceWideVariableName(i) = p_name Then
                result = GetServiceWideVariableAllowEmptyString(i)
                Exit For
            End If
        Next

        Return result
    End Function

    Public Function GetServiceWideVariableSanityCheckForNonEmptyValue(ByVal p_index As Integer) As Boolean
        Return serviceWideVariablesList(p_index).GetSanityCheckForNonEmpty
    End Function

    Public Function GetServiceWideVariableSanityCheckForNonEmptyValue(ByRef p_name As String) As String
        Dim result As Boolean = False

        For i = 0 To GetNumberOfServiceWideVariables() - 1
            If GetServiceWideVariableName(i) = p_name Then
                result = GetServiceWideVariableSanityCheckForNonEmptyValue(i)
                Exit For
            End If
        Next

        Return result
    End Function

    Private Sub WalkthroughToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles WalkthroughToolStripMenuItem.Click
        WalkthroughForm.ShowDialog()
    End Sub

    Private Sub CDRSearchToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        CDRSearchForm.Show()
    End Sub

    Private Sub ShowStartBlockToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ShowStartBlockToolStripMenuItem.Click
        ShowCode(True)
    End Sub

    Private Sub ShowCode(ByRef p_justStartBlock As Boolean)
        Dim myScripts As List(Of String) = GetScriptsInDatabase()

        With SelectScriptForm
            .ComboBox1.Items.Clear()
            .CheckBox1.Checked = False

            For Each myScript As String In myScripts
                .ComboBox1.Items.Add(myScript)
            Next

            If .ComboBox1.Items.Count > 0 Then
                .ComboBox1.SelectedIndex = 0

                If .ShowDialog = Windows.Forms.DialogResult.OK Then
                    Dim myScript As String = ""

                    If .CheckBox1.Checked Then
                        myScript = TEST_SCRIPT_NAME
                        ExportProjectToDatabase(True)
                    Else
                        myScript = .ComboBox1.Text
                    End If

                    ShowCodeSub(myScript, p_justStartBlock)

                    If .CheckBox1.Checked Then
                        DeleteScriptFromDatabase(myScript)
                    End If
                End If
            End If
        End With
    End Sub

    Private Sub DeleteScriptFromDatabase(ByRef p As String)
        Dim myCommand As String = "delete from " & SERVICEBUILDER_TABLE_NAME & " where scriptName = " & WrapInSingleQuotes(p)

        If ExecuteNonQuery(CreateConnectionString(Form1.settingsConfigDictionary), myCommand) Then
        End If
    End Sub

    Private Function GetScriptsInDatabase() As List(Of String)
        Dim myCommand As String = "select distinct scriptName, case when scriptName like '** %' then " _
        & "CONVERT(datetime, " _
        & "SUBSTRING(scriptName, Len(scriptName) - 15, 4)" _
        & " + '-' + substring(scriptName, len(scriptName) - 18, 2)" _
        & " + '-' + substring(scriptName, len(scriptName) - 21, 2)" _
        & " + ' ' + SUBSTRING(scriptName, len(scriptName) - 10, 8)" _
        & ", 120) else GETDATE() end AS [timestamp]," _
        & " case when scriptName like '** %' then SUBSTRING(scriptName, 4, LEN(scriptName) - 26) else scriptName end as [rawName]" _
        & " from " & SERVICEBUILDER_TABLE_NAME & " order by rawname, timestamp desc"

        Dim myTable As New DataTable
        Dim myList As New List(Of String)

        If FillTableFromCommand(CreateConnectionString(Form1.settingsConfigDictionary), myCommand, myTable) Then
            If myTable.Columns.Count = 3 Then
                For i = 0 To myTable.Rows.Count - 1
                    With myTable.Rows(i)
                        If .Item("scriptName") IsNot DBNull.Value Then myList.Add(.Item(0))
                    End With
                Next
            End If
        End If

        Return myList
    End Function

    Private Sub ShowCodeSub(ByRef p_scriptName As String, ByVal p_justStartBlock As Boolean)
        Dim myCommand As String = "select data, title from " & SERVICEBUILDER_TABLE_NAME & " where scriptName = " & WrapInSingleQuotes(p_scriptName) & " and nodeType = "
        Dim myTable As New DataTable

        If p_justStartBlock Then
            myCommand &= "'SIBB_Start'"
            ShowVariablesCodeForm.Text = "Show Start Block Code for " & WrapInQuotes(p_scriptName)
            ShowVariablesCodeForm.okButton.Text = "OK"
        Else
            myCommand &= "'SIBB_VBScript'"
            ShowVariablesCodeForm.Text = "Show VB Script Block Code for " & WrapInQuotes(p_scriptName)
            ShowVariablesCodeForm.okButton.Text = "Next .."
        End If

        If FillTableFromCommand(CreateConnectionString(Form1.settingsConfigDictionary), myCommand, myTable) Then
            If myTable.Columns.Count = 2 Then
                For i = 0 To myTable.Rows.Count - 1
                    With myTable.Rows(i)
                        If .Item(0) IsNot DBNull.Value Then
                            Dim myData As String = .Item(0)
                            Dim myLines() As String = myData.Split(vbCrLf)
                            Dim myTitle As String = ""
                            Dim oldTitle As String = ""

                            If .Item(1) IsNot DBNull.Value Then myTitle = .Item(1)

                            ClearRichTextBox(ShowVariablesCodeForm.showVariablesCodeRichTextBox)

                            For Each myLine As String In myLines
                                Dim myColour As Color = Color.Black

                                If myLine.Length > 0 Then
                                    If Asc(myLine(0)) = 10 Then myLine = myLine.Substring(1)
                                End If

                                If myLine.StartsWith("Select") Then myColour = Color.Blue
                                If myLine.StartsWith("End Select") Then myColour = Color.Blue
                                If myLine.StartsWith("Case") Then myColour = Color.Red
                                If myLine.StartsWith("SetVar") Then myColour = Color.Green
                                If myLine.StartsWith("AddVar") Then myColour = Color.Purple

                                If myLine.Contains("'") Then
                                    AddLineSpecialForVBSComment(myLine, myColour, Color.Yellow)
                                Else
                                    AddLine(myLine, myColour)
                                End If
                            Next

                            If Not p_justStartBlock Then
                                oldTitle = ShowVariablesCodeForm.Text
                                ShowVariablesCodeForm.Text = oldTitle & " for Node " & WrapInQuotes(myTitle)
                            End If

                            ShowVariablesCodeForm.showVariablesCodeRichTextBox.SelectionStart = 0

                            If ShowVariablesCodeForm.ShowDialog() = Windows.Forms.DialogResult.Cancel Then Exit For

                            If Not p_justStartBlock Then ShowVariablesCodeForm.Text = oldTitle
                        End If
                    End With
                Next
            End If
        End If
    End Sub

    Public Sub AddLine(ByRef p As String, ByVal p_colour As Color, Optional ByRef p_target As RichTextBox = Nothing)
        Dim myPtr As RichTextBox = ShowVariablesCodeForm.showVariablesCodeRichTextBox

        If p_target IsNot Nothing Then myPtr = p_target

        With myPtr
            .SelectionStart = .TextLength
            .SelectionLength = 0
            .SelectionColor = p_colour
            .SelectionBackColor = .BackColor
            .AppendText(p & vbCrLf)
            .SelectionColor = .ForeColor
        End With
    End Sub

    Public Sub AddLineSpecialForVBSComment(ByRef p As String, ByVal p_colour As Color, ByVal p_backgroundColour As Color, Optional ByRef p_target As RichTextBox = Nothing)
        Dim myPtr As RichTextBox = ShowVariablesCodeForm.showVariablesCodeRichTextBox

        If p_target IsNot Nothing Then myPtr = p_target

        With myPtr
            .SelectionStart = .TextLength
            .SelectionLength = 0
            .SelectionColor = p_colour
            .SelectionBackColor = p_backgroundColour
            .AppendText(p & vbCrLf)
            .SelectionColor = .ForeColor
        End With
    End Sub

    Private Sub AddLineDelta(ByRef p_heading As String, ByRef p_lhs As String, ByRef p_rhs As String)
        With DBDeltaForm.RichTextBox
            .SelectionBackColor = .BackColor
            .SelectedText = p_heading & " "

            .SelectionBackColor = Color.LightPink
            .SelectedText = p_lhs

            .SelectionBackColor = .BackColor
            .SelectedText = " vs "

            .SelectionBackColor = Color.LightBlue
            .SelectedText = p_rhs & vbCrLf & vbCrLf
        End With
    End Sub

    Private Sub ShowVBScriptBlocksCodeToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ShowVBScriptBlocksCodeToolStripMenuItem.Click
        ShowCode(False)
    End Sub

    Private Sub DeltaToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DeltaToolStripMenuItem.Click
        Dim myScripts As List(Of String) = GetScriptsInDatabase()

        With DBDeltaForm
            .okButton.Text = "Go .."
            .ComboBox1.Items.Clear()
            .ComboBox2.Items.Clear()
            ClearRichTextBox(.RichTextBox)
            .CheckBox1.Checked = False

            For Each myScript As String In myScripts
                .ComboBox1.Items.Add(myScript)
                .ComboBox2.Items.Add(myScript)
            Next

            If .ComboBox1.Items.Count > 0 Then .ComboBox1.SelectedIndex = 0
            If .ComboBox2.Items.Count > 0 Then .ComboBox2.SelectedIndex = 0

            If .ShowDialog = Windows.Forms.DialogResult.OK Then
                Dim firstScript As String = ""
                Dim secondScript As String = .ComboBox2.Text

                If .CheckBox1.Checked Then
                    firstScript = TEST_SCRIPT_NAME
                    ExportProjectToDatabase(True)
                Else
                    firstScript = .ComboBox1.Text
                End If

                If firstScript = secondScript Then
                    MsgBox("Cannot delta against the same script")
                Else
                    Dim myCommand As String = "select * from " & SERVICEBUILDER_TABLE_NAME & " where scriptName = " & WrapInSingleQuotes(firstScript) & " order by nodeNumber"
                    Dim myTable1 As New DataTable

                    If FillTableFromCommand(CreateConnectionString(Form1.settingsConfigDictionary), myCommand, myTable1) Then
                        Dim myTable2 As New DataTable

                        myCommand = "select * from " & SERVICEBUILDER_TABLE_NAME & " where scriptName = " & WrapInSingleQuotes(secondScript) & " order by nodeNumber"

                        If FillTableFromCommand(CreateConnectionString(Form1.settingsConfigDictionary), myCommand, myTable2) Then
                            Dim nodeIndex As Integer = 0
                            Dim running As Boolean = True

                            While running
                                If nodeIndex >= myTable1.Rows.Count Then
                                    ' Script 1 has no more nodes, what about Script 2 ?
                                    If nodeIndex >= myTable2.Rows.Count Then
                                        ' Script 2 also has no more nodes, so finish here
                                        running = False
                                    Else
                                        ' Script 2 has more nodes so finish here with a message
                                        running = False
                                        .RichTextBox.AppendText("Script 2 has more nodes after Script 1 has finished" & vbCrLf)
                                    End If
                                Else
                                    ' Script 1 has this node, does Script 2 ?
                                    If nodeIndex >= myTable2.Rows.Count Then
                                        ' No. Script 1 has more nodes so finish here with a message
                                        running = False
                                        .RichTextBox.AppendText("Script 1 has more nodes after Script 2 has finished" & vbCrLf)
                                    Else
                                        ' Yes. Compare all the data, except scriptName in first column
                                        For i = 1 To myTable1.Columns.Count - 1
                                            CompareData(myTable1, myTable2, nodeIndex, myTable1.Columns(i).ColumnName)
                                        Next
                                    End If
                                End If

                                nodeIndex += 1
                            End While

                            .RichTextBox.AppendText("Delta finished ..")
                            .okButton.Text = "OK"
                            DBDeltaForm.ShowDialog()
                        End If
                    End If
                End If

                If .CheckBox1.Checked Then
                    DeleteScriptFromDatabase(TEST_SCRIPT_NAME)
                End If
            End If
        End With
    End Sub

    Private Sub CompareData(ByRef p_table1 As DataTable, ByRef p_table2 As DataTable, ByVal p_nodeIndex As Integer, ByRef p_fieldName As String)
        With DBDeltaForm
            ' Check if this is the data field which needs analysing line by line
            If p_fieldName = "data" Then
                Dim fields1() As String = p_table1.Rows(p_nodeIndex).Item(p_fieldName).ToString.Split(vbCrLf)
                Dim fields2() As String = p_table2.Rows(p_nodeIndex).Item(p_fieldName).ToString.Split(vbCrLf)
                Dim myLineIndex As Integer = 0
                Dim running As Boolean = True

                For i = 0 To fields1.Count - 1
                    If fields1(i).Length > 0 Then
                        If fields1(i).Chars(0) = Chr(10) Then fields1(i) = fields1(i).Substring(1)
                    End If
                Next

                For i = 0 To fields2.Count - 1
                    If fields2(i).Length > 0 Then
                        If fields2(i).Chars(0) = Chr(10) Then fields2(i) = fields2(i).Substring(1)
                    End If
                Next

                While running
                    If myLineIndex >= fields2.Count Then
                        running = False
                    Else
                        If myLineIndex >= fields1.Count Then
                            running = False
                        Else
                            If fields1(myLineIndex) <> fields2(myLineIndex) Then
                                Dim myText As String = "Node Number " & p_table1.Rows(p_nodeIndex).Item("nodeNumber")

                                myText &= " : " & "Node Type " & WrapInQuotes(p_table1.Rows(p_nodeIndex).Item("nodeType"))
                                myText &= " : " & "Title " & WrapInQuotes(p_table1.Rows(p_nodeIndex).Item("title"))
                                myText &= " : Line Number " & myLineIndex
                                myText &= " : " & p_fieldName & " mismatch = "

                                AddLineDelta(myText, fields1(myLineIndex), fields2(myLineIndex))
                            End If

                            myLineIndex += 1
                        End If
                    End If
                End While
            Else
                If p_table1.Rows(p_nodeIndex).Item(p_fieldName) <> p_table2.Rows(p_nodeIndex).Item(p_fieldName) Then
                    Dim myText As String = "Node Number " & p_table1.Rows(p_nodeIndex).Item("nodeNumber")

                    myText &= " : " & "Node Type " & WrapInQuotes(p_table1.Rows(p_nodeIndex).Item("nodeType"))
                    myText &= " : " & "Title " & WrapInQuotes(p_table1.Rows(p_nodeIndex).Item("title"))
                    myText &= " : " & p_fieldName & " mismatch = "

                    AddLineDelta(myText, p_table1.Rows(p_nodeIndex).Item(p_fieldName), p_table2.Rows(p_nodeIndex).Item(p_fieldName))
                End If
            End If
        End With
    End Sub

    Public Sub ClearRichTextBox(ByRef p As RichTextBox)
        p.SelectAll()
        p.SelectedText = ""
    End Sub

    Public Sub AddNewServiceWideVariable(ByRef p As ServiceWideVariableClass)
        Dim alreadyThere As Boolean = False

        For Each x As ServiceWideVariableClass In serviceWideVariablesList
            If x.GetName = p.GetName Then
                alreadyThere = True
                Exit For
            End If
        Next

        If Not alreadyThere Then serviceWideVariablesList.Add(p)
    End Sub

    Private Sub CheckScriptFileAgainstDatabaseToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckScriptFileAgainstDatabaseToolStripMenuItem.Click
        Dim myOpenFileDialog As New OpenFileDialog

        If myOpenFileDialog.ShowDialog = Windows.Forms.DialogResult.OK Then
            Dim tableName As String = "Scripts"
            Dim functionName As String = myOpenFileDialog.SafeFileName.Substring(0, myOpenFileDialog.SafeFileName.LastIndexOf("."))
            Dim mySql As New SQLStatementClass
            Dim myTable As New DataTable
            Dim fileCheckSum As Integer = 0
            Dim dbCheckSum As Integer = 0
            Dim myErrorText As String = ""
            Dim fileNumberOfLines = 0

            With NameForm
                .TextBox1.Text = functionName

                If .ShowDialog <> Windows.Forms.DialogResult.OK Then Return

                functionName = .TextBox1.Text
            End With

            ' Look for this function in the database
            mySql.SetPrimaryTable(tableName)
            mySql.AddSelectString("code", "")
            mySql.AddCondition("FunctionName = " & WrapInSingleQuotes(functionName))
            mySql.AddOrderByString("lineNumber")
            mySql.AddCondition("code not like '''%'")
            DeltaForm.fileTextBox.Clear()
            DeltaForm.databaseTextBox.Clear()
            DeltaForm.Text = "Delta for " & functionName
            DeltaForm.fileTextBox.Tag = "0,0"
            DeltaForm.databaseTextBox.Tag = "0,0"

            If FillTableFromCommand(CreateConnectionString(Form1.settingsConfigDictionary), mySql.GetSQLStatement, myTable, myErrorText) Then
                If myTable.Rows.Count = 0 Then MsgBox("Function " & WrapInQuotes(functionName) & " not found in database")
            Else
                MsgBox("Error looking for function " & WrapInQuotes(functionName) & " in database" & vbCrLf & myErrorText)
            End If

            Dim myStreamReader As New IO.StreamReader(myOpenFileDialog.FileName)
            Dim reading As Boolean = True
            Dim line As Integer = 1
            Dim matched As Boolean = True

            While reading
                Dim myLine As String = myStreamReader.ReadLine

                If myLine Is Nothing Then
                    reading = False
                Else
                    myLine = myLine.Trim

                    If myLine <> "" Then
                        If Not myLine.StartsWith("'") Then
                            Dim myColour As Color = Color.Black

                            ' Compare to database, if code is available for this line
                            If myTable.Rows.Count >= line Then
                                Dim dbLine As String = myTable.Rows(line - 1).Item("Code").ToString

                                For i = 0 To dbLine.Length - 1
                                    dbCheckSum += Asc(dbLine.Chars(i))
                                Next

                                dbCheckSum = dbCheckSum Mod 65536

                                If dbLine <> myLine Then
                                    matched = False
                                    'dbLine = "***" & dbLine
                                    myColour = Color.Red
                                End If

                                FormatVBS(DeltaForm.databaseTextBox, dbLine, myColour)
                            Else
                                matched = False
                            End If

                            ' Add data to delta form
                            For i = 0 To myLine.Length - 1
                                fileCheckSum += Asc(myLine.Chars(i))
                            Next

                            fileCheckSum = fileCheckSum Mod 65536

                            FormatVBS(DeltaForm.fileTextBox, myLine, myColour)

                            line += 1
                        End If
                    End If
                End If
            End While

            myStreamReader.Close()

            ' Copy any remaining lines from the database version into the form
            If myTable.Rows.Count >= line Then
                matched = False

                For j = line - 1 To myTable.Rows.Count - 1
                    Dim dbLine As String = myTable.Rows(j).Item("Code").ToString

                    For i = 0 To dbLine.Length - 1
                        dbCheckSum += Asc(dbLine.Chars(i))
                    Next

                    dbCheckSum = dbCheckSum Mod 65536

                    FormatVBS(DeltaForm.databaseTextBox, "*** " & dbLine, Color.Green)
                Next
            End If

            DeltaForm.fileContentsLabel.Text = "File contents - checksum = " & fileCheckSum & ", non-blank lines = " & line - 1
            DeltaForm.databaseContentsLabel.Text = "Database contents - checksum = " & dbCheckSum & ", non-blank lines = " & myTable.Rows.Count

            If matched Then
                If MsgBox("Database matches file", MsgBoxStyle.OkCancel) = MsgBoxResult.Ok Then
                    DeltaForm.fileContentsLabel.ForeColor = Color.Green
                    DeltaForm.databaseContentsLabel.ForeColor = Color.Green
                    DeltaForm.ShowDialog()
                End If
            Else
                If MsgBox("Database does not match file", MsgBoxStyle.OkCancel) = MsgBoxResult.Ok Then
                    DeltaForm.fileContentsLabel.ForeColor = Color.Red
                    DeltaForm.databaseContentsLabel.ForeColor = Color.Red
                    DeltaForm.ShowDialog()
                End If
            End If
        End If
    End Sub

    Public Function GetScriptNamesInDatabase() As List(Of String)
        Dim mySql As New SQLStatementClass
        Dim myTable As New DataTable
        Dim myScriptList As New List(Of String)

        mySql.SetPrimaryTable("Scripts")
        mySql.AddSelectString("distinct functionName", "")
        mySql.AddOrderByString("functionName")

        If FillTableFromCommand(CreateConnectionString(Form1.settingsConfigDictionary), mySql.GetSQLStatement, myTable) Then
            If myTable.Columns.Count > 0 Then
                For i = 0 To myTable.Rows.Count - 1
                    With myTable.Rows(i)
                        If .Item(0) IsNot DBNull.Value Then myScriptList.Add(.Item(0))
                    End With
                Next
            End If
        End If

        Return myScriptList
    End Function

    Public Function GetScriptsFolder() As String
        Dim x As New FolderBrowserDialog
        Dim myLastSourceFolder As String = Form1.settingsConfigDictionary.GetItem([Enum].GetName(GetType(Form1.OurConfigItems), Form1.OurConfigItems.lastSourceFolder))
        Dim myPath As String = ""

        If myLastSourceFolder = "" Then
            x.SelectedPath = Environment.CurrentDirectory

            If Not x.SelectedPath.EndsWith("\") Then x.SelectedPath &= "\"

            x.SelectedPath &= "VBScripts"
        Else
            x.SelectedPath = myLastSourceFolder
        End If
        'MsgBox(x.SelectedPath)
        If x.ShowDialog = Windows.Forms.DialogResult.OK Then
            ' If the folder has changed then save it
            If x.SelectedPath <> myLastSourceFolder Then
                Form1.settingsConfigDictionary.SetItem([Enum].GetName(GetType(Form1.OurConfigItems), Form1.OurConfigItems.lastSourceFolder), x.SelectedPath)
                GenericXMLConfigSaver(Form1.CONFIG_FILENAME, Form1.settingsConfigDictionary)
            End If

            myPath = x.SelectedPath
        End If

        Return myPath
    End Function

    Public Function FindScriptFile(ByRef p_sourcePath As String, ByRef p_scriptName As String) As String
        Dim myFileName As String = p_sourcePath.TrimEnd("\") & "\" & p_scriptName & ".txt"
        Dim foundFile As Boolean = False

        If FileExists(myFileName) Then
            foundFile = True
        Else
            ' Try any sub directories
            Dim myFolderInfo As New IO.DirectoryInfo(p_sourcePath)

            For Each subFolder As IO.DirectoryInfo In myFolderInfo.GetDirectories
                myFileName = subFolder.FullName.TrimEnd("\") & "\" & p_scriptName & ".txt"

                If FileExists(myFileName) Then
                    foundFile = True
                    Exit For
                End If
            Next
        End If

        If Not foundFile Then myFileName = ""

        Return myFileName
    End Function

    Public Function GetRelativeScriptPath(ByRef p_sourcePath As String, ByRef p_scriptPath As String) As String
        Return p_scriptPath.Substring((p_sourcePath.TrimEnd("\") & "\").Length)
    End Function

    Public Function GetScriptFromDatabase(ByRef p_scriptName As String) As List(Of String)
        Dim mySql As New SQLStatementClass
        Dim myTable As New DataTable
        Dim myDatabaseCode As New List(Of String)

        mySql.SetPrimaryTable("Scripts")
        mySql.AddSelectString("Code", "")
        mySql.AddCondition("functionName = " & WrapInSingleQuotes(p_scriptName))
        mySql.AddOrderByString("lineNumber")

        If FillTableFromCommand(CreateConnectionString(Form1.settingsConfigDictionary), mySql.GetSQLStatement, myTable) Then
            If myTable.Columns.Count > 0 Then
                For j = 0 To myTable.Rows.Count - 1
                    With myTable.Rows(j)
                        If .Item(0) IsNot DBNull.Value Then
                            ' Ignore any commented line
                            If Not .Item(0).ToString.StartsWith("'") Then myDatabaseCode.Add(.Item(0))
                        End If

                    End With
                Next
            End If
        End If

        Return myDatabaseCode
    End Function

    Private Sub CheckAllScriptsInDatabaseAgainstSourceToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckAllScriptsInDatabaseAgainstSourceToolStripMenuItem.Click
        ' Get names of all scripts in database Scripts table
        Dim mySql As New SQLStatementClass
        Dim myTable As New DataTable
        Dim myScriptList As List(Of String) = GetScriptNamesInDatabase()
        Dim sourceFolder As String = GetScriptsFolder()

        If sourceFolder <> "" Then
            ' Look for the source file for each script in the database
            For i = 0 To myScriptList.Count - 1
                Dim myScriptName As String = myScriptList(i)
                Dim mySourceCode As New List(Of String)
                Dim myDatabaseCode As New List(Of String)
                Dim myFileName As String = FindScriptFile(sourceFolder, myScriptName)

                If myFileName <> "" Then
                    Dim y As New IO.StreamReader(myFileName)
                    Dim reading As Boolean = True

                    While reading
                        Dim myLine As String = y.ReadLine

                        If myLine Is Nothing Then
                            reading = False
                        Else
                            ' Get rid of any leading or trailing white space
                            myLine = myLine.Trim

                            ' Ignore any empty lines or those that start with a comment
                            Dim ignore As Boolean = False

                            If myLine.StartsWith("'") Then ignore = True
                            If myLine = "" Then ignore = True

                            If Not ignore Then mySourceCode.Add(myLine)
                        End If
                    End While

                    y.Close()

                    ' Compare mySourceCode to the database
                    myDatabaseCode = GetScriptFromDatabase(myScriptName)

                    If mySourceCode.Count = myDatabaseCode.Count Then
                        ' Line count is the same, compare each line
                        For j = 0 To mySourceCode.Count - 1
                            If mySourceCode(j) <> myDatabaseCode(j) Then
                                Select Case MsgBox("Mismatch on Line " & j + 1 & " for " & myScriptName & " vs" & vbCrLf & myFileName & vbCrLf & "Do you want to view the delta ?", MsgBoxStyle.YesNoCancel)
                                    Case MsgBoxResult.Cancel
                                        Exit For

                                    Case MsgBoxResult.Yes
                                        With DeltaForm
                                            .Text = "Delta for " & myScriptName
                                            .fileContentsLabel.Text = "File contents for: " & GetRelativeScriptPath(sourceFolder, myFileName)
                                            .databaseContentsLabel.Text = "Database contents for: " & myScriptName

                                            With .fileTextBox
                                                .Clear()

                                                For k = 0 To mySourceCode.Count - 1
                                                    .AppendText(mySourceCode(k))

                                                    If k < mySourceCode.Count - 1 Then .AppendText(vbCrLf)
                                                Next
                                            End With

                                            With .databaseTextBox
                                                .Clear()

                                                For k = 0 To myDatabaseCode.Count - 1
                                                    .AppendText(myDatabaseCode(k))

                                                    If k < myDatabaseCode.Count - 1 Then .AppendText(vbCrLf)
                                                Next
                                            End With

                                            .ShowDialog()
                                            .Text = "Delta"
                                            .fileContentsLabel.Text = "File Contents"
                                            .databaseContentsLabel.Text = "Database Contents"
                                        End With
                                End Select
                            End If
                        Next
                    Else
                        Select Case MsgBox("Different number of lines between " & myFileName & " (" & mySourceCode.Count & ")" & vbCrLf & "and " & myScriptName & " in Scripts database table (" & myDatabaseCode.Count & ")" & vbCrLf & "Do you want to view the delta ?", MsgBoxStyle.YesNoCancel)
                            Case MsgBoxResult.Cancel
                                Exit For

                            Case MsgBoxResult.Yes
                                With DeltaForm
                                    .Text = "Delta for " & myScriptName
                                    .fileContentsLabel.Text = "File contents for: " & GetRelativeScriptPath(sourceFolder, myFileName)
                                    .databaseContentsLabel.Text = "Database contents for: " & myScriptName

                                    With .fileTextBox
                                        .Clear()

                                        For j = 0 To mySourceCode.Count - 1
                                            .AppendText(mySourceCode(j))

                                            If j < mySourceCode.Count - 1 Then .AppendText(vbCrLf)
                                        Next
                                    End With

                                    With .databaseTextBox
                                        .Clear()

                                        For j = 0 To myDatabaseCode.Count - 1
                                            .AppendText(myDatabaseCode(j))

                                            If j < myDatabaseCode.Count - 1 Then .AppendText(vbCrLf)
                                        Next
                                    End With

                                    .ShowDialog()
                                    .Text = "Delta"
                                    .fileContentsLabel.Text = "File Contents"
                                    .databaseContentsLabel.Text = "Database Contents"
                                End With
                        End Select
                    End If
                Else
                    Select Case MsgBox("Cannot find source file: " & myScriptName & vbCrLf & "Continue ?", MsgBoxStyle.YesNo)
                        Case MsgBoxResult.No
                            Exit For
                    End Select
                End If
            Next

            MsgBox("Done")
        End If
    End Sub

    Private Sub CopyDatabaseScriptToFileToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CopyDatabaseScriptToFileToolStripMenuItem.Click
        CopyFunctionFromDatabaseForm.ShowDialog()
    End Sub

    Private Sub AddCallToQueueToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AddCallToQueueToolStripMenuItem.Click
        Dim x As New AddCallToQueueSIBBClass(myMasterBitmap)
        Dim tableList As New List(Of String)
        Dim defList As New List(Of String)
        Dim mySP As String = "SP_ADD_CALL_TO_QUEUE"

        CommonNodeAdd(x)

        tableList.Add("CallQueueTable")
        ' QueueId type change from VARCHAR(128) to INT as per our database schema and Ecourier schema
        defList.Add("CallId int, Timestamp datetime, QueueId int, Tag varchar(128), State varchar(16), Rings int, LastHeartBeat datetime")
        TableCheck("AddCallToQueue", tableList, defList)

        If Not DoesDatabaseFunctionExist(Form1.settingsConfigDictionary, mySP, True) Then
            Dim myCancelled As Boolean = False

            InstallMsg("This SIBB requires " & mySP, mySP, Form1.settingsConfigDictionary, True, "", myCancelled)
        End If
    End Sub

    Private Sub TableCheck(ByRef p_componentName As String, ByRef p_tables As List(Of String), ByRef p_defs As List(Of String))
        For i = 0 To p_tables.Count - 1
            Dim myTable As String = p_tables(i)
            Dim myText As String = "This operation"

            If Not p_componentName Is Nothing Then myText = p_componentName

            If Not DoesDatabaseTableExist.DoesDatabaseTableExist(myTable) Then
                If MsgBox(myText & " requires the " & myTable & " database table which does not currently exist" & vbCrLf & "Do you want to create this table now ?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                    Dim myCommand As String = "create table " & myTable & " (" & p_defs(i) & ")"

                    If ExecuteNonQuery(CreateConnectionString(Form1.settingsConfigDictionary), myCommand) Then
                        MsgBox("Table created OK")
                    Else
                        MsgBox("Could not create table")
                    End If
                End If
            End If
        Next
    End Sub

    Private Sub TopOfQueueToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TopOfQueueToolStripMenuItem.Click
        Dim x As New TopOfQueueSIBBClass(myMasterBitmap)
        Dim tableList As New List(Of String)
        Dim defList As New List(Of String)
        Dim mySP As String = "SP_GET_POSITION_IN_QUEUE_EX"

        CommonNodeAdd(x)

        tableList.Add("CallQueueTable")
        defList.Add("CallId int, Timestamp datetime, QueueId varchar(256), Tag varchar(128), State varchar(16), Rings int, LastHeartBeat datetime")
        TableCheck("TopOfQueue", tableList, defList)

        If Not DoesDatabaseFunctionExist(Form1.settingsConfigDictionary, mySP, True) Then
            Dim myCancelled As Boolean = False

            InstallMsg("This SIBB requires " & mySP, mySP, Form1.settingsConfigDictionary, True, "", myCancelled)
        End If
    End Sub

    Private Sub ChangeQueueStateToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChangeQueueStateToolStripMenuItem.Click
        Dim x As New ChangeQueueStateSIBBClass(myMasterBitmap)
        Dim tableList As New List(Of String)
        Dim defList As New List(Of String)
        Dim mySP As String = "SP_CHANGE_QUEUE_STATE"

        CommonNodeAdd(x)

        tableList.Add("CallQueueTable")
        defList.Add("CallId int, Timestamp datetime, QueueId varchar(256), Tag varchar(128), State varchar(16), Rings int, LastHeartBeat datetime")
        TableCheck("ChangeQueueState", tableList, defList)

        If Not DoesDatabaseFunctionExist(Form1.settingsConfigDictionary, mySP, True) Then
            Dim myCancelled As Boolean = False

            InstallMsg("This SIBB requires " & mySP, mySP, Form1.settingsConfigDictionary, True, "", myCancelled)
        End If
    End Sub

    Private Sub RemoveCallFromQueueToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RemoveCallFromQueueToolStripMenuItem.Click
        Dim x As New RemoveCallFromQueueSIBBClass(myMasterBitmap)
        Dim tableList As New List(Of String)
        Dim defList As New List(Of String)
        Dim mySP As String = "SP_CHANGE_QUEUE_STATE"

        CommonNodeAdd(x)

        tableList.Add("CallQueueTable")
        defList.Add("CallId int, Timestamp datetime, QueueId varchar(256), Tag varchar(128), State varchar(16), Rings int, LastHeartBeat datetime")
        TableCheck("ChangeQueueState", tableList, defList)

        If Not DoesDatabaseFunctionExist(Form1.settingsConfigDictionary, mySP, True) Then
            Dim myCancelled As Boolean = False

            InstallMsg("This SIBB requires " & mySP, mySP, Form1.settingsConfigDictionary, True, "", myCancelled)
        End If
    End Sub

    Private Sub GetPositionInQueueToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GetPositionInQueueToolStripMenuItem.Click
        Dim x As New GetPositionInQueueSIBBClass(myMasterBitmap)
        Dim tableList As New List(Of String)
        Dim defList As New List(Of String)
        Dim mySP As String = "SP_GET_POSITION_IN_QUEUE_EX"

        CommonNodeAdd(x)

        tableList.Add("CallQueueTable")
        defList.Add("CallId int, Timestamp datetime, QueueId varchar(256), Tag varchar(128), State varchar(16), Rings int, LastHeartBeat datetime")
        TableCheck("ChangeQueueState", tableList, defList)

        If Not DoesDatabaseFunctionExist(Form1.settingsConfigDictionary, mySP, True) Then
            Dim myCancelled As Boolean = False

            InstallMsg("This SIBB requires " & mySP, mySP, Form1.settingsConfigDictionary, True, "", myCancelled)
        End If
    End Sub

    Private Sub QueuePauseToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles QueuePauseToolStripMenuItem.Click
        Dim x As New QueuePauseSIBBClass(myMasterBitmap)
        Dim tableList As New List(Of String)
        Dim defList As New List(Of String)

        CommonNodeAdd(x)

        tableList.Add("CallQueueTable")
        defList.Add("CallId int, Timestamp datetime, QueueId varchar(256), Tag varchar(128), State varchar(16), Rings int, LastHeartBeat datetime")
        TableCheck("ChangeQueueState", tableList, defList)
    End Sub

    Private Sub GetCurrentTimeToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GetCurrentTimeToolStripMenuItem.Click
        Dim x As New GetCurrentTimeSIBBClass(myMasterBitmap)

        CommonNodeAdd(x)
    End Sub

    Private Sub HasTimeElapsedToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles HasTimeElapsedToolStripMenuItem.Click
        Dim x As New HasTimeElapsedSIBBClass(myMasterBitmap)

        CommonNodeAdd(x)
    End Sub

    Private Sub GetLengthOfQueueToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GetLengthOfQueueToolStripMenuItem.Click
        Dim x As New GetQueueLength(myMasterBitmap)
        Dim tableList As New List(Of String)
        Dim defList As New List(Of String)
        Dim mySP As String = "SP_GET_QUEUE_LENGTH"

        CommonNodeAdd(x)

        tableList.Add("CallQueueTable")
        defList.Add("CallId int, Timestamp datetime, QueueId varchar(256), Tag varchar(128), State varchar(16), Rings int, LastHeartBeat datetime")
        TableCheck("GetQueueLength", tableList, defList)

        If Not DoesDatabaseFunctionExist(Form1.settingsConfigDictionary, mySP, True) Then
            Dim myCancelled As Boolean = False

            InstallMsg("This SIBB requires " & mySP, mySP, Form1.settingsConfigDictionary, True, "", myCancelled)
        End If
    End Sub

    Private Sub FirstTimeToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FirstTimeToolStripMenuItem.Click
        Dim x As New FirstTimeSIBBClass(myMasterBitmap)

        CommonNodeAdd(x)
    End Sub

    Private Sub LogPointToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LogPointToolStripMenuItem.Click
        Dim x As New LogPointSIBBClass(myMasterBitmap)
        Dim tableList As New List(Of String)
        Dim defList As New List(Of String)
        Dim mySP As String = "SP_UPDATE_SB_LOG_POINT_TABLE"
        Dim numberOfLogPoints As Integer = 5
        Dim myDefs As String = ""

        CommonNodeAdd(x)

        If Not DoesDatabaseTableExist.DoesDatabaseTableExist("ServiceBuilderLogPointTable") Then
            tableList.Add("ServiceBuilderLogPointTable")

            With QueryForm
                .Text = "Log Point Details For New Log Point Table"
                .GroupBox1.Text = "Number of Log Points ?"
                .ShowDialog()

                If .TextBox1.Text <> "" Then
                    If IsInteger(.TextBox1.Text) Then numberOfLogPoints = CInt(.TextBox1.Text)
                End If
            End With

            myDefs = "callId int"

            For i = 0 To numberOfLogPoints - 1
                myDefs &= ", logPoint_" & i & " datetime"
            Next

            defList.Add(myDefs)
            TableCheck("LogPointSIBBClass", tableList, defList)
        End If

        If Not DoesDatabaseFunctionExist(Form1.settingsConfigDictionary, mySP, True) Then
            Dim myCancelled As Boolean = False

            InstallMsg("This SIBB requires " & mySP, mySP, Form1.settingsConfigDictionary, True, "", myCancelled)
        End If
    End Sub

    Private Sub LogAgentStatusToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LogAgentStatusToolStripMenuItem.Click
        Dim x As New LogAgentStatusSIBBClass(myMasterBitmap)

        CommonNodeAdd(x)
        CheckAgentDataTable()
    End Sub

    Private Sub CheckAgentDataTable()
        Dim tableList As New List(Of String)
        Dim defList As New List(Of String)

        tableList.Add("AgentDataMasterTable")
        defList.Add("myKey int identity(1, 1), timestamp datetime, callId int, calledNumber varchar(256), target varchar(256)")

        tableList.Add("AgentDataTable")
        defList.Add("myKey int, userId int, userState int")

        TableCheck("LogAgentStatusSIBBClass", tableList, defList)
    End Sub

    Private Sub CDRSeartToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CDRSeartToolStripMenuItem.Click
        CDRSearchForm.Show()
    End Sub

    Private Sub ExportFunctionFromDatabaseToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExportFunctionFromDatabaseToolStripMenuItem.Click
        Dim mySql As String = "select distinct functionName from Scripts order by functionName"
        Dim myTable As New DataTable

        ExportFunctionForm.ListBox1.Items.Clear()
        ExportFunctionForm.TextBox1.Clear()

        If FillTableFromCommand(CreateConnectionString(Form1.settingsConfigDictionary), mySql, myTable) Then
            If myTable.Columns.Count > 0 Then
                For i = 0 To myTable.Rows.Count - 1
                    With myTable.Rows(i)
                        If Not .Item(0) Is DBNull.Value Then
                            ExportFunctionForm.ListBox1.Items.Add(.Item(0))
                        End If
                    End With
                Next

                If ExportFunctionForm.ShowDialog = Windows.Forms.DialogResult.OK Then

                End If
            End If
        End If
    End Sub

    Private Sub SmallToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SmallToolStripMenuItem.Click
        SetToSmall()
        RepaintAll()
    End Sub

    Private Sub NormalToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NormalToolStripMenuItem.Click
        SetToNormal()
        RepaintAll()
    End Sub

    Private Sub RestoreScriptsToPreviousLoadToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RestoreScriptsToPreviousLoadToolStripMenuItem.Click
        ' Find all the scripts in the database which have a backup to restore to
        Dim myTable As New DataTable
        Dim myCommand As String = "select distinct timestamp, functionName from scripts_history order by timestamp desc, functionName"

        With RestoreScriptsForm
            .functionNameListBox.Items.Clear()
            .CheckBox1.Checked = True
        End With

        If FillTableFromCommand(CreateConnectionString(Form1.settingsConfigDictionary), myCommand, myTable) Then
            If myTable.Columns.Count = 2 Then
                For i = 0 To myTable.Rows.Count - 1
                    With myTable.Rows(i)
                        If .Item("timestamp") IsNot DBNull.Value And .Item("functionName") IsNot DBNull.Value Then
                            Dim myDateTime As DateTime = .Item("timestamp")
                            Dim myTimeStamp As String = myDateTime.ToString("yyyy-MM-dd HH:mm:ss.fff")

                            RestoreScriptsForm.functionNameListBox.Items.Add(myTimeStamp & "      " & .Item("functionName"))
                        End If
                    End With
                Next
            End If
        End If

        RestoreScriptsForm.UpdateSIBBNeedsList()
        RestoreScriptsForm.ShowDialog()
        Return

        With RestoreForm
            If .scriptNameComboBox.Items.Count > 0 Then .scriptNameComboBox.SelectedIndex = 0
        End With

        If RestoreForm.ShowDialog = Windows.Forms.DialogResult.OK Then
            Dim myScriptName As String = RestoreForm.scriptNameComboBox.Text
            Dim myBackupScriptName As String = RestoreForm.backupComboBox.Text

            If MsgBox("You have chosen to restore script " & WrapInQuotes(myScriptName) & " from " & WrapInQuotes(myBackupScriptName) & vbCrLf & "Do you want to proceed ?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                ' Backup the existing script
                Dim newBackupScriptName = "** " & myScriptName & " " & Now & " **"

                myCommand = "insert into " & SERVICEBUILDER_TABLE_NAME & " select " & WrapInSingleQuotes(newBackupScriptName) & ", nodeNumber, nodeType, data, outputs, title, internalReference from " & SERVICEBUILDER_TABLE_NAME & " where scriptName = " & WrapInSingleQuotes(myScriptName)

                If ExecuteNonQuery(CreateConnectionString(Form1.settingsConfigDictionary), myCommand) Then
                    ' Erase the current script from the database
                    myCommand = "delete from " & SERVICEBUILDER_TABLE_NAME & " where scriptName = " & WrapInSingleQuotes(myScriptName)

                    If ExecuteNonQuery(CreateConnectionString(Form1.settingsConfigDictionary), myCommand) Then
                        ' Activate the restored script
                        myCommand = "insert into " & SERVICEBUILDER_TABLE_NAME & " select " & WrapInSingleQuotes(myScriptName) & ", nodeNumber, nodeType, data, outputs, title, internalReference from " & SERVICEBUILDER_TABLE_NAME & " where scriptName = " & WrapInSingleQuotes(myBackupScriptName)

                        If ExecuteNonQuery(CreateConnectionString(Form1.settingsConfigDictionary), myCommand) Then
                            MsgBox("Restore completed ..")
                        Else
                            MsgBox("Error: Could not activate the restored script " & WrapInQuotes(myScriptName))
                        End If
                    Else
                        MsgBox("Error: Could not delete script " & WrapInQuotes(myScriptName) & vbCrLf & "Restore aborted ..")
                    End If
                Else
                    MsgBox("Error: Could not backup script " & WrapInQuotes(myScriptName) & vbCrLf & "Restore aborted ..")
                End If
            End If
        End If
    End Sub

    Public Function BackupFunction(ByRef p_function As String) As Boolean
        Dim tableName As String = "Scripts"
        Dim backupTableName As String = tableName & "_history"
        Dim myTimeStamp As String = WrapInSingleQuotes(Now.ToString("yyyy-MM-dd HH:mm:ss.fff"))
        Dim result As Boolean = False
        Dim myTable As New DataTable
        Dim lineCount As Integer = -1

        ' Get the number of lines in the code
        If FillTableFromCommand(CreateConnectionString(Form1.settingsConfigDictionary), "select * from " & tableName & " where FunctionName = " & WrapInSingleQuotes(p_function), myTable) Then
            lineCount = myTable.Rows.Count

            If lineCount > 0 Then
                If ExecuteNonQuery(CreateConnectionString(Form1.settingsConfigDictionary), "insert into " & backupTableName & " select *, " & myTimeStamp & " from " & tableName & " where FunctionName = " & WrapInSingleQuotes(p_function)) Then
                    ' Check that the function has been backed up
                    myTable = New DataTable

                    If FillTableFromCommand(CreateConnectionString(Form1.settingsConfigDictionary), "select * from " & backupTableName & " where FunctionName = " & WrapInSingleQuotes(p_function) & " and timestamp = " & myTimeStamp, myTable) Then
                        If myTable.Rows.Count = lineCount Then result = True
                    End If
                End If
            Else
            End If
        End If

        Return result
    End Function

    Public Function DeleteFunctionFromLiveDB(ByRef p_function As String) As Boolean
        Dim tableName As String = "Scripts"

        Return ExecuteNonQuery(CreateConnectionString(Form1.settingsConfigDictionary), "delete from " & tableName & " where FunctionName = " & WrapInSingleQuotes(p_function))
    End Function

    Public Function RestoreFunction(ByRef p_function As String, ByRef p_timestamp As String) As Boolean
        Dim tableName As String = "Scripts"
        Dim backupTableName As String = tableName & "_history"
        Dim result As Boolean = False
        Dim myTable As New DataTable
        Dim lineCount As Integer = -1

        ' Get the number of lines in the code
        If FillTableFromCommand(CreateConnectionString(Form1.settingsConfigDictionary), "select * from " & backupTableName & " where FunctionName = " & WrapInSingleQuotes(p_function) & " and TimeStamp = " & WrapInSingleQuotes(p_timestamp), myTable) Then
            lineCount = myTable.Rows.Count

            If lineCount > 0 Then
                If ExecuteNonQuery(CreateConnectionString(Form1.settingsConfigDictionary), "insert into " & tableName & " select FunctionName, LineNumber, Code, Class from " & backupTableName & " where FunctionName = " & WrapInSingleQuotes(p_function) & " and TimeStamp = " & WrapInSingleQuotes(p_timestamp)) Then
                    ' Check that the function has been backed up
                    myTable = New DataTable

                    If FillTableFromCommand(CreateConnectionString(Form1.settingsConfigDictionary), "select * from " & tableName & " where FunctionName = " & WrapInSingleQuotes(p_function), myTable) Then
                        If myTable.Rows.Count = lineCount Then result = True
                    End If
                End If
            Else
            End If
        End If

        Return result
    End Function

    Public Function BackupSIBBNeeds() As Boolean
        Dim tableName As String = SIBB_NEEDS_TABLE_NAME
        Dim backupTableName As String = tableName & "_history"
        Dim myTimeStamp As String = WrapInSingleQuotes(Now.ToString("yyyy-MM-dd HH:mm:ss.fff"))
        Dim result As Boolean = False
        Dim myTable As New DataTable
        Dim lineCount As Integer = -1

        ' Get the number of lines in the code
        If FillTableFromCommand(CreateConnectionString(Form1.settingsConfigDictionary), "select * from " & tableName, myTable) Then
            lineCount = myTable.Rows.Count

            If lineCount = 0 Then
                result = True
            Else
                If ExecuteNonQuery(CreateConnectionString(Form1.settingsConfigDictionary), "insert into " & backupTableName & " select " & myTimeStamp & ", * from " & tableName) Then
                    myTable = New DataTable

                    If FillTableFromCommand(CreateConnectionString(Form1.settingsConfigDictionary), "select * from " & backupTableName & " where timestamp = " & myTimeStamp, myTable) Then
                        If myTable.Rows.Count = lineCount Then result = True
                    End If
                End If
            End If
        End If

        Return result
    End Function

    Public Function ClearSIBBNeeds() As Boolean
        Dim tableName As String = SIBB_NEEDS_TABLE_NAME

        Return ExecuteNonQuery(CreateConnectionString(Form1.settingsConfigDictionary), "delete from " & tableName)
    End Function

    Public Function RestoreSIBBNeeds(ByRef p_timestamp As String) As Boolean
        Dim tableName As String = SIBB_NEEDS_TABLE_NAME
        Dim backupTableName As String = tableName & "_history"
        Dim result As Boolean = False
        Dim myTable As New DataTable
        Dim lineCount As Integer = -1

        ' Get the number of lines in the code
        If FillTableFromCommand(CreateConnectionString(Form1.settingsConfigDictionary), "select * from " & backupTableName & " where TimeStamp = " & WrapInSingleQuotes(p_timestamp), myTable) Then
            lineCount = myTable.Rows.Count

            If lineCount > 0 Then

                If ExecuteNonQuery(CreateConnectionString(Form1.settingsConfigDictionary), "insert into " & tableName & " select SIBB, Needs from " & backupTableName & " where TimeStamp = " & WrapInSingleQuotes(p_timestamp)) Then
                    ' Check that the function has been backed up
                    myTable = New DataTable

                    If FillTableFromCommand(CreateConnectionString(Form1.settingsConfigDictionary), "select * from " & tableName, myTable) Then
                        If myTable.Rows.Count = lineCount Then result = True
                    End If
                End If
            Else
            End If
        End If

        Return result
    End Function

    Private Sub OpenProjectReadOnlyToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OpenProjectReadOnlyToolStripMenuItem.Click
        LoadProjectFromDialogBox(True)
    End Sub

    Private Sub ContextMenuStrip1_Opening(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles ContextMenuStrip1.Opening
        Dim callTypeIndex, row, subIndex As Integer
        Dim disableAll As Boolean = False
        Dim enablePaste As Boolean = False
        Dim mySuffix As String = ""
        Dim iconClicked As Boolean
        Dim mouseX As Integer = lastMousePosition.GetX
        Dim mouseY As Integer = lastMousePosition.GetY
        Dim myScreenLocation As New ScreenLocationClass(mouseX, mouseY)
        Dim myNodeIndex As Integer = -1
        Dim myOutputIndex As Integer = -1

        For i = sibbList.Count - 1 To 0 Step -1
            With sibbList(i)
                If .MouseInRange(myScreenLocation) Then
                    myNodeIndex = i

                    For j = 0 To .outputs.Count - 1
                        myOutputIndex = .MouseInRangeOfOutput(myScreenLocation)

                        If myOutputIndex >= 0 Then Exit For
                    Next

                    If myNodeIndex >= 0 Then Exit For
                Else
                    If .MouseInRangeOfFooter(myScreenLocation) Then
                        myNodeIndex = i
                        Exit For
                    End If
                End If
            End With
        Next

        If myNodeIndex = -1 Then
            mySuffix = ""
        Else
            If myOutputIndex = -1 Then
                mySuffix = "Node"
            Else
                mySuffix = "Link"
            End If
        End If

        If mySuffix = "" Then
            ContextMenuStrip1.Enabled = False
        Else
            ContextMenuStrip1.Enabled = True

            For i = 0 To ContextMenuStrip1.Items.Count - 1
                With ContextMenuStrip1.Items(i)
                    Dim myIndex = .Text.IndexOf(" ")

                    If myIndex >= 0 Then .Text = .Text.Substring(0, myIndex)

                    .Text &= " " & mySuffix
                    .Tag = BuildTag(i, myNodeIndex, myOutputIndex)
                End With
            Next
        End If
    End Sub

    Private Sub FindToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FindToolStripMenuItem.Click
        With ScriptNameToFileNameForm
            If .ShowDialog() = Windows.Forms.DialogResult.OK Then
                ' Load the script file
                LoadProject(.filenameTextBox.Text, False)
            End If
        End With
    End Sub

    Private Sub DesignerForm_Resize(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Resize
        If Me.WindowState = FormWindowState.Maximized Then
            ResizeHandler()
        End If
    End Sub

    Private Sub SimulatorToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SimulatorToolStripMenuItem.Click
        SimulatorForm.Show()
    End Sub

    Private Sub StartToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles StartToolStripMenuItem1.Click
        TraceStartMenuHandler()
        StartToolStripMenuItem1.Enabled = False
        StopToolStripMenuItem.Enabled = True

        For i = 0 To sibbList.Count - 1
            sibbList(i).DebugHistorySelect(False)
            sibbList(i).DebugSelect(False)
        Next
    End Sub

    Private Sub StopToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles StopToolStripMenuItem.Click
        Text = ""
        StartToolStripMenuItem1.Enabled = True
        StopToolStripMenuItem.Enabled = False

        If realTimeCallTraceBackgroundWorker.WorkerSupportsCancellation Then realTimeCallTraceBackgroundWorker.CancelAsync()
    End Sub

    Private Sub ActivateCallToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ActivateCallToolStripMenuItem.Click
        Dim x As New ActivateCallSIBBClass(myMasterBitmap)

        CommonNodeAdd(x)
    End Sub

    Private Sub SendEmailToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SendEmailToolStripMenuItem.Click
        Dim x As New SendEmailSIBBClass(myMasterBitmap)

        CommonNodeAdd(x)
    End Sub

    Private Sub CompareXMLDatabaseFilesToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CompareXMLDatabaseFilesToolStripMenuItem.Click
        Const LAST_MODIFIED_TEXT As String = "'' Last Modified: "
        Dim firstScripts As ScriptListClass = GetCodeFromXMLFile()
        Dim secondScripts As ScriptListClass = GetCodeFromXMLFile()
        Dim ignoreTimestamps As Boolean = False
        Dim cancelled As Boolean = False

        If MsgBox("Do you want to ignore timestamps in delta checks ?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then ignoreTimestamps = True

        For Each myScriptName As String In firstScripts.myData.Keys
            Dim showDelta As Boolean = False

            ' Check if script exists in second scripts
            If secondScripts.myData.ContainsKey(myScriptName) Then
                ' Script exists in both - check if we have a missing "Last Updated" line at the start from legacy versions
                If firstScripts.myData.Item(myScriptName).Count > 0 Then
                    If firstScripts.myData.Item(myScriptName).Item(0).myCode.StartsWith(LAST_MODIFIED_TEXT) Then
                        If secondScripts.myData.Item(myScriptName).Count > 0 Then
                            If Not secondScripts.myData.Item(myScriptName).Item(0).myCode.StartsWith(LAST_MODIFIED_TEXT) Then
                                secondScripts.myData.Item(myScriptName).Insert(0, New ScriptListEntryClass(0, LAST_MODIFIED_TEXT & "Not present .."))
                            End If
                        End If
                    End If
                End If

                If secondScripts.myData.Item(myScriptName).Count > 0 Then
                    If secondScripts.myData.Item(myScriptName).Item(0).myCode.StartsWith(LAST_MODIFIED_TEXT) Then
                        If firstScripts.myData.Item(myScriptName).Count > 0 Then
                            If Not firstScripts.myData.Item(myScriptName).Item(0).myCode.StartsWith(LAST_MODIFIED_TEXT) Then
                                firstScripts.myData.Item(myScriptName).Insert(0, New ScriptListEntryClass(0, LAST_MODIFIED_TEXT & "Not present .."))
                            End If
                        End If
                    End If
                End If

                Dim a As List(Of ScriptListEntryClass) = firstScripts.myData.Item(myScriptName)
                Dim b As List(Of ScriptListEntryClass) = secondScripts.myData.Item(myScriptName)

                ' Check if line count matches
                If a.Count = b.Count Then
                    ' Yes - Check that each code line matches
                    For i = 0 To a.Count - 1
                        If Not a(i).myCode = b(i).myCode Then
                            Dim ignoreDelta As Boolean = False

                            If ignoreTimestamps Then
                                If a(i).myCode.StartsWith(LAST_MODIFIED_TEXT) And b(i).myCode.StartsWith(LAST_MODIFIED_TEXT) Then ignoreDelta = True
                            End If

                            If Not ignoreDelta Then
                                Select Case MsgBox("Mismatch in code for Script: " & WrapInQuotes(myScriptName) & " from Line " & a(i).myLineNumber & vbCrLf & "Do you want to display the delta ?", MsgBoxStyle.YesNoCancel)
                                    Case MsgBoxResult.Yes
                                        showDelta = True

                                    Case MsgBoxResult.Cancel
                                        cancelled = True
                                End Select

                                Exit For
                            End If
                        End If
                    Next
                Else
                    Select Case MsgBox("Mismatch in line count for Script: " & WrapInQuotes(myScriptName) & vbCrLf & "Do you want to display the delta ?", MsgBoxStyle.YesNoCancel)
                        Case MsgBoxResult.Yes
                            showDelta = True

                        Case MsgBoxResult.Cancel
                            cancelled = True
                    End Select
                End If
            Else
                Select Case MsgBox("Script: " & WrapInQuotes(myScriptName) & " is in " & firstScripts.myFilename & " but not in " & secondScripts.myFilename, MsgBoxStyle.OkCancel)
                    Case MsgBoxResult.Cancel
                        cancelled = True
                End Select
            End If

            If showDelta Then DisplayScripts(myScriptName, firstScripts, secondScripts)

            If cancelled Then Exit For
        Next

        If Not cancelled Then
            ' Check for any scripts in the second file and not in the first ..
            For Each myScriptName As String In secondScripts.myData.Keys
                If Not firstScripts.myData.ContainsKey(myScriptName) Then
                    Select Case MsgBox("Script: " & WrapInQuotes(myScriptName) & " is in " & secondScripts.myFilename & " but not in " & firstScripts.myFilename, MsgBoxStyle.OkCancel)
                        Case MsgBoxResult.Cancel
                            Exit For
                    End Select

                End If
            Next
        End If
    End Sub

    Private Sub WriteToFile(ByRef p_scriptName As String, ByRef p As ScriptListClass, ByRef p_filename As String)
        Dim x As New IO.StreamWriter(p_filename)

        With p.myData.Item(p_scriptName)
            For i = 0 To .Count - 1
                x.WriteLine(.Item(i).myCode)
            Next
        End With

        x.Close()
    End Sub

    Private Sub DisplayScripts(ByRef p_scriptName As String, ByRef p_a As ScriptListClass, ByRef p_b As ScriptListClass)
        Dim myProcess As New System.Diagnostics.Process()

        WriteToFile(p_scriptName, p_a, "c:\f1.txt")
        WriteToFile(p_scriptName, p_b, "c:\f2.txt")

        myProcess.StartInfo.FileName = "c:\program files\winmerge\winmergeu.exe"
        myProcess.StartInfo.Arguments = "/e /u /dl " & WrapInQuotes(p_a.myFilename & "::" & p_scriptName) & " /dr " & WrapInQuotes(p_b.myFilename & "::" & p_scriptName) & " c:\f1.txt c:\f2.txt"
        myProcess.Start()

        Return
        With DeltaForm
            .fileTextBox.Text = ""
            .databaseTextBox.Text = ""
            .fileContentsLabel.Text = p_a.myFilename
            .databaseContentsLabel.Text = p_b.myFilename
            .Text = "Code for Script: " & WrapInQuotes(p_scriptName)

            With .fileTextBox
                For i = 0 To p_a.myData(p_scriptName).Count - 1
                    Dim showDelta As Boolean = False

                    If i > 0 Then .AppendText(vbCrLf)

                    If i < p_b.myData(p_scriptName).Count Then
                        If p_a.myData.Item(p_scriptName).Item(i).myCode <> p_b.myData.Item(p_scriptName).Item(i).myCode Then showDelta = True
                    Else
                        showDelta = True
                    End If

                    .SelectionStart = .TextLength
                    .SelectionLength = 0

                    If showDelta Then
                        .SelectionColor = Color.FromArgb(255, 0, 0)
                    Else
                        .SelectionColor = Color.Black
                    End If

                    .AppendText(p_a.myData.Item(p_scriptName).Item(i).myCode)
                Next
            End With

            With .databaseTextBox
                For i = 0 To p_b.myData(p_scriptName).Count - 1
                    Dim showDelta As Boolean = False

                    If i > 0 Then .AppendText(vbCrLf)

                    If i < p_a.myData(p_scriptName).Count Then
                        If p_b.myData.Item(p_scriptName).Item(i).myCode <> p_a.myData.Item(p_scriptName).Item(i).myCode Then showDelta = True
                    Else
                        showDelta = True
                    End If

                    .SelectionStart = .TextLength
                    .SelectionLength = 0

                    If showDelta Then
                        .SelectionColor = Color.FromArgb(255, 0, 0)
                    Else
                        .SelectionColor = Color.Black
                    End If

                    .AppendText(p_b.myData.Item(p_scriptName).Item(i).myCode)
                Next
            End With

            .ShowDialog()
        End With
    End Sub

    Private Function GetCodeFromXMLFile() As ScriptListClass
        Dim myDoc As New XmlDocument
        Dim myOpenFileDialog As New OpenFileDialog
        Dim myFilename As String = ""
        Dim myScripts As New ScriptListClass

        If myOpenFileDialog.ShowDialog = Windows.Forms.DialogResult.OK Then
            myFilename = myOpenFileDialog.FileName
            myScripts.myFilename = myFilename
        End If

        XMLLoader(myFilename, myDoc)

        ' Loop over each parameter
        For Each myRecord In myDoc("DatabaseTable")
            Select Case myRecord.Name
                Case "Rows"
                    For Each myRow As XmlNode In myRecord.ChildNodes
                        Select Case myRow.Name
                            Case "Row"
                                Dim myIndex As Integer = 0
                                Dim myFunctionName As String = ""
                                Dim myLineNumber As Integer = -1

                                For Each rowData As XmlNode In myRow.ChildNodes
                                    Select Case rowData.Name
                                        Case "RowItem"
                                            Dim myValue As String = rowData.FirstChild.Value.TrimEnd("'")

                                            ' Use this instead of Trim() to preserve second ' for commented out lines
                                            If myValue.StartsWith("'") Then myValue = myValue.Substring(1)

                                            Select Case myIndex
                                                Case 0
                                                    ' FunctionName
                                                    myFunctionName = myValue

                                                    If Not myScripts.myData.ContainsKey(myValue) Then myScripts.myData.Add(myFunctionName, New List(Of ScriptListEntryClass))

                                                Case 1
                                                    ' LineNumber
                                                    myLineNumber = CInt(myValue)

                                                Case 2
                                                    ' Code
                                                    'If Not myValue.StartsWith("'") Then
                                                    Dim myListRef As List(Of ScriptListEntryClass) = myScripts.myData.Item(myFunctionName)
                                                    Dim myListEntry As New ScriptListEntryClass(myLineNumber, myValue)
                                                    Dim codeInserted As Boolean = False

                                                    For i = 0 To myListRef.Count - 1
                                                        Dim currentLineNumber As Integer = myListRef(i).myLineNumber

                                                        If myLineNumber < currentLineNumber Then
                                                            myListRef.Insert(i, myListEntry)
                                                            codeInserted = True
                                                            Exit For
                                                        End If
                                                    Next

                                                    If Not codeInserted Then myListRef.Add(myListEntry)
                                                    'End If
                                            End Select

                                            myIndex += 1
                                    End Select
                                Next
                        End Select
                    Next
            End Select
        Next

        Return myScripts
    End Function

    Private Sub CompareXMLToDatabaseToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CompareXMLToDatabaseToolStripMenuItem.Click
        Dim firstScripts As ScriptListClass = GetCodeFromXMLFile()
        Dim myScriptName As String = "Bootstrap"
        Dim myXmlRef As List(Of ScriptListEntryClass) = firstScripts.myData.Item(myScriptName)
        Dim myTable As New DataTable
        Dim mySql As String = "select * from Scripts_history where functionName = " & WrapInSingleQuotes(myScriptName) & " order by timestamp, lineNumber"

        For Each x As String In firstScripts.myData.Keys
            MsgBox(x)
        Next
        If FillTableFromCommand(CreateConnectionString(Form1.settingsConfigDictionary), mySql, myTable) Then
            ' Compare XML data against each historical version ..
            Dim myTimestamps As New List(Of String)

            For i = 0 To myTable.Rows.Count - 1
                With myTable.Rows(i)
                    Dim dbTimestamp As String = .Item("timestamp")

                    If Not myTimestamps.Contains(dbTimestamp) Then myTimestamps.Add(dbTimestamp)
                End With
            Next

            For i = 0 To myTimestamps.Count - 1
                Dim dbTimestamp As String = myTimestamps(i)
                Dim myCode As New List(Of String)
                Dim mySecondTable As New DataTable
                Dim codeMatched As Boolean = True

                mySql = "select code from Scripts_history where timestamp = " & WrapInSingleQuotes(dbTimestamp) & " order by lineNumber"

                If FillTableFromCommand(CreateConnectionString(Form1.settingsConfigDictionary), mySql, mySecondTable) Then
                    If mySecondTable.Rows.Count = myXmlRef.Count Then
                        For j = 0 To mySecondTable.Rows.Count - 1
                            With mySecondTable.Rows(j)
                                If myXmlRef.Item(j).myCode <> .Item("code") Then
                                    codeMatched = False
                                    Exit For
                                End If
                            End With
                        Next
                    Else
                        codeMatched = False
                    End If
                End If

                If codeMatched Then MsgBox(dbTimestamp)
            Next
        End If
    End Sub

    Private Sub CloseProjectToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CloseProjectToolStripMenuItem.Click
        openFilename = ""
        Text = ""
        SetGUIState(GUIStateType.NULL)
        sibbList.Clear()
        VariablesForm.ClearVariables()
        ClearMasterBitmap()
        MyRefresh()
    End Sub
End Class