Imports System.Data.OleDb
Public Class SettingsForm
    Private tableDefinitions As New List(Of TableDefinitionClass)

    Private ForScriptsTable() As String = {"Base", "Bootstrap", _
                                          "CallMachineClass", "ConvertDateToISO", _
"DBExecute", "DBReadScalar", "EndCall", "ExpandGroupRanges", "GetCompositeGroupMembers", "GetGroupMembers", "GroupAvailable", "Hold", "IsDigit", _
"LeastUsedAgentDistributor", "Pause", "Queue Routines", "Scripts", _
"SIBB_Connect", "SIBB_DayOfWeek", "SIBB_Done", "SIBB_EndCall", "SIBB_GetDTMFDigit", "SIBB_GetDTMFString", "SIBB_GroupAvailable", "SIBB_Hold", "SIBB_Holiday", _
"SIBB_LongestWaiting", "SIBB_OnDisconnect", "SIBB_Pause", "SIBB_PlayAnnouncement", "SIBB_Skip", "SIBB_Sleep", "SIBB_Start", "SIBB_TimeOfDay", "SIBB_VBScript", "SIBB_Voicemail", _
"Sleep", "TODDOW", "UserInfoListClass", "WildcardSwyxGroup"}

    Private Sub TestButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TestButton.Click

        Dim myConnectionString As String = CreateConnectionString(Form1.settingsConfigDictionary)
        Dim dbConnection As New OleDbConnection(myConnectionString)
        Dim objCommand As New OleDbCommand
        Dim errorString As String = ""

        ' Try to connect to the specified database
        Try
            dbConnection.Open()
        Catch ex As OleDbException
            errorString = "Cannot connect to database with connection string: " & myConnectionString
            errorString &= vbCrLf & ex.Message
        End Try

        If errorString = "" Then
            ' We have connected to the database
            Try
                objCommand.Connection = dbConnection
            Catch ex As Exception
                errorString = "Error in creating OleDbDataAdapter"
                errorString &= vbCrLf & ex.Message
            End Try

            If errorString = "" Then
                Dim allTablesPresent As Boolean = True

                ' Check if each table exists
                For i = 0 To tableDefinitions.Count - 1
                Next

                If allTablesPresent Then
                    MsgBox("All tables present")
                Else
                    MsgBox("Some tables are missing", MsgBoxStyle.Critical)
                End If
            Else
                MsgBox("CRM database connection problem" & vbCrLf & errorString, MsgBoxStyle.Critical)
                Logutil.LogError("CRMSettingsForm::Test()", "CRM database connection problem" & vbCrLf & errorString)
            End If
        Else
            MsgBox(errorString)
        End If
    End Sub

    Private Sub SettingsForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ' Build the table definitions
        Dim scriptsDefs As New TableDefinitionClass

        With scriptsDefs
            .tableName = "Scripts"
            .AddDeclaration("FunctionName", TableDefinitionClass.VARCHAR_256)
            .AddDeclaration("LineNumber", TableDefinitionClass.INT)
            .AddDeclaration("Code", TableDefinitionClass.VARCHAR_4096)
            .AddDeclaration("Class", TableDefinitionClass.VARCHAR_256)
        End With

        tableDefinitions.Add(scriptsDefs)

        Dim serviceBuilderDebugTableDefs As New TableDefinitionClass

        With serviceBuilderDebugTableDefs
            .tableName = "ServiceBuilderDebugTable"
            .AddDeclaration("scriptName", TableDefinitionClass.VARCHAR_256)
            .AddDeclaration("callId", TableDefinitionClass.INT)
            .AddDeclaration("state", TableDefinitionClass.VARCHAR_256)
        End With

        tableDefinitions.Add(serviceBuilderDebugTableDefs)

        Dim servicerBuilderEventTableDefs As New TableDefinitionClass

        With servicerBuilderEventTableDefs
            .tableName = "ServiceBuilderEventTable"
            .AddDeclaration("timestamp", TableDefinitionClass.DATETIME, False)
            .AddDeclaration("scriptName", TableDefinitionClass.VARCHAR_256, False)
            .AddDeclaration("callId", TableDefinitionClass.INT, False)
            .AddDeclaration("node", TableDefinitionClass.INT, False)
            .AddDeclaration("data", TableDefinitionClass.VARCHAR_4096)
            .AddDeclaration("output", TableDefinitionClass.INT)
            .AddDeclaration("nextNode", TableDefinitionClass.INT)
        End With

        tableDefinitions.Add(servicerBuilderEventTableDefs)

        Dim servicerBuilderTableDefs As New TableDefinitionClass

        With servicerBuilderTableDefs
            .tableName = "ServiceBuilderTable"
            .AddDeclaration("scriptName", TableDefinitionClass.VARCHAR_256)
            .AddDeclaration("nodeNumber", TableDefinitionClass.INT)
            .AddDeclaration("nodeType", TableDefinitionClass.VARCHAR_256)
            .AddDeclaration("data", TableDefinitionClass.VARCHAR_MAX)
            .AddDeclaration("outputs", TableDefinitionClass.VARCHAR_256)
            .AddDeclaration("title", TableDefinitionClass.VARCHAR_256)
            .AddDeclaration("internalReference", TableDefinitionClass.VARCHAR_256)
        End With

        tableDefinitions.Add(servicerBuilderTableDefs)

        Dim sibbNeedsTableDefs As New TableDefinitionClass

        With sibbNeedsTableDefs
            .tableName = DesignerForm.SIBB_NEEDS_TABLE_NAME
            .AddDeclaration("SIBB", TableDefinitionClass.VARCHAR_256, False)
            .AddDeclaration("Needs", TableDefinitionClass.VARCHAR_256, False)
        End With

        tableDefinitions.Add(sibbNeedsTableDefs)
    End Sub

    Private Sub SetupTablesToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SetupTablesToolStripMenuItem.Click
        Dim x As New FolderBrowserDialog
        Dim result As Boolean = True

        If x.ShowDialog Then
            For i = 0 To ForScriptsTable.Length - 1
                Dim myFilename As String = x.SelectedPath & "\" & ForScriptsTable(i) & ".txt"

                If Not DesignerForm.CopyFunctionToDatabase(myFilename, False, False, False) Then
                    result = False
                    MsgBox("Failure in writing " & myFilename & " to database")
                End If
            Next

            If result Then MsgBox("All functions written to database OK")
        End If
    End Sub
End Class