Public Class Form1
    Public Const CONFIG_FILENAME As String = "ECRDesignerSettings.xml"

    Public componentList As New List(Of ComponentClass)
    Public Shared settingsConfigDictionary As New DictionaryClass("Config")
    Private oldDefaultScriptsTableContents() As String = {"Base", "Bootstrap", "CallConnect", "CallDelivered", "CallDisconnect", "CallMachineClass", "CallStart", _
"ConvertDateToISO", "DBExecute", "DBReadScalar", "DBReturnResults", "EndCall", "ExpandGroupRanges", _
"GetCompositeGroupMembers", "GetGroupMembers", "GroupAvailable", "Hold", "InHours", "IsDigit", "LeastUsedAgentDistributor", "Pause", "Scripts", _
"SIBB_Connect", "SIBB_DayOfWeek", "SIBB_Done", "SIBB_EndCall", "SIBB_GetDTMFDigit", "SIBB_GetDTMFString", "SIBB_GroupAvailable", "SIBB_Hold", _
"SIBB_Holiday", "SIBB_LongestWaiting", "SIBB_OnDisconnect", "SIBB_Pause", "SIBB_PlayAnnouncement", "SIBB_Skip", "SIBB_Sleep", "SIBB_Start", "SIBB_TimeOfDay", "SIBB_VBScript", _
"SIBB_Voicemail", "Sleep", "TODDOW", "UserInfoListClass", "VMDone", "WildcardSwyxGroup"}

    Private unusedDefaultScriptsTableContents() As String = {"Base", "Bootstrap", "ConvertDateToISO", "DBExecute", "DBReadScalar", "DBReturnResults", "EndCall", "ExpandGroupRanges", _
"GetCompositeGroupMembers", "GetGroupMembers", "GroupAvailable", "Hold", "IsDigit", "LeastUsedAgentDistributor", "Pause", "Scripts", _
"SIBB_Connect", "SIBB_DayOfWeek", "SIBB_Done", "SIBB_EndCall", "SIBB_GetDTMFDigit", "SIBB_GetDTMFString", "SIBB_GroupAvailable", "SIBB_Hold", _
"SIBB_Holiday", "SIBB_LongestWaiting", "SIBB_OnDisconnect", "SIBB_Pause", "SIBB_PlayAnnouncement", "SIBB_Skip", "SIBB_Sleep", "SIBB_Start", "SIBB_TimeOfDay", "SIBB_VBScript", _
"SIBB_Voicemail", "Sleep", "TODDOW", "UserInfoListClass", "WildcardSwyxGroup"}

    Private tableDefs As New Dictionary(Of String, String)

    Enum OurConfigItems
        lastSourceFolder
        lastProjectFolder
        swyxDatabaseName
    End Enum

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.CenterToScreen()
        InitConfig()

        If FileExists(CONFIG_FILENAME) Then
            GenericXMLConfigLoader(CONFIG_FILENAME, settingsConfigDictionary)
        Else
            EditDatabaseSettings()
        End If

        LoadComponents()
        Label1.Text = ""
        Label2.Visible = False
        ProgressBar1.Visible = False
    End Sub

    Private Sub InitConfig()
        SetParentDatabaseConfigDictionary(settingsConfigDictionary)

        settingsConfigDictionary.Add([Enum].GetName(GetType(OurConfigItems), OurConfigItems.lastSourceFolder), "")
        settingsConfigDictionary.Add([Enum].GetName(GetType(OurConfigItems), OurConfigItems.lastProjectFolder), "")
        settingsConfigDictionary.Add([Enum].GetName(GetType(OurConfigItems), OurConfigItems.swyxDatabaseName), "IpPbx")

        settingsConfigDictionary.SetItem([Enum].GetName(GetType(ParentDatabaseConfigItems), ParentDatabaseConfigItems.databaseName), "OpenQueue")
        settingsConfigDictionary.SetItem([Enum].GetName(GetType(ParentDatabaseConfigItems), ParentDatabaseConfigItems.databaseTable), "")
        InitLogutilConfig()
    End Sub

    Private Sub LoadComponents()
        Dim x As New ComponentClass

        x.name = "GenerateDatabaseConnectionString()"
        x.id = ComponentClass.IDType.GENERATE_DATABASE_CONNECTION_STRING
        x.filename = "GenerateDatabaseConnectionString"
        x.needs.Add(ComponentClass.IDType.CONSTANTS)
        componentList.Add(x)

        x = New ComponentClass
        x.name = "DBReadScalar(ByRef p_statement, ByRef p_fieldName)"
        x.id = ComponentClass.IDType.DB_READ_SCALAR
        x.filename = "DBReadScalar"
        x.needs.Add(ComponentClass.IDType.GENERATE_DATABASE_CONNECTION_STRING)
        x.needs.Add(ComponentClass.IDType.WRAP_IN_DOUBLE_QUOTES)
        componentList.Add(x)

        x = New ComponentClass
        x.name = "IsTodayBankHoliday()"
        x.id = ComponentClass.IDType.IS_TODAY_BANK_HOLIDAY
        x.filename = "IsTodayBankHoliday"
        x.needs.Add(ComponentClass.IDType.DB_READ_SCALAR)
        componentList.Add(x)

        x = New ComponentClass
        x.name = "DBExecute(ByRef p_statement)"
        x.id = ComponentClass.IDType.DB_EXECUTE
        x.filename = "DBExecute"
        x.needs.Add(ComponentClass.IDType.GENERATE_DATABASE_CONNECTION_STRING)
        componentList.Add(x)

        x = New ComponentClass
        x.name = "DeltaTableField(byref p_table, byref p_key, byref p_column. byref p_delta)"
        x.id = ComponentClass.IDType.DELTA_TABLE_VALUE
        x.filename = "DeltaTableField"
        x.needs.Add(ComponentClass.IDType.DB_EXECUTE)
        x.needs.Add(ComponentClass.IDType.WRAP_IN_SINGLE_QUOTES)
        componentList.Add(x)

        x = New ComponentClass
        x.name = "WrapInSingleQuotes(ByRef p)"
        x.id = ComponentClass.IDType.WRAP_IN_SINGLE_QUOTES
        x.filename = "WrapInSingleQuotes"
        componentList.Add(x)

        x = New ComponentClass
        x.name = ".Constants"
        x.id = ComponentClass.IDType.CONSTANTS
        x.filename = "Constants"
        componentList.Add(x)

        x = New ComponentClass
        x.name = "DBReturnResults(ByRef p_statement, ByRef p_rs)"
        x.id = ComponentClass.IDType.DB_RETURN_RESULTS
        x.filename = "DBReturnResults"
        x.needs.Add(ComponentClass.IDType.GENERATE_DATABASE_CONNECTION_STRING)
        x.needs.Add(ComponentClass.IDType.WRAP_IN_DOUBLE_QUOTES)
        componentList.Add(x)

        x = New ComponentClass
        x.name = "CF_Lookup(ByRef p_callingNumber, ByRef p_customerName, ByRef p_agentName)"
        x.id = ComponentClass.IDType.CF_LOOKUP
        x.filename = "CF_Lookup"
        x.needs.Add(ComponentClass.IDType.DB_RETURN_RESULTS)
        x.needs.Add(ComponentClass.IDType.WRAP_IN_SINGLE_QUOTES)
        componentList.Add(x)

        x = New ComponentClass
        x.name = "DebugLogError(ByRef p_error)"
        x.id = ComponentClass.IDType.DEBUG_LOG_ERROR
        x.filename = "DebugLogError"
        componentList.Add(x)

        x = New ComponentClass
        x.name = "End Call"
        x.id = ComponentClass.IDType.END_CALL
        x.filename = "EndCall"
        x.needs.Add(ComponentClass.IDType.WRAP_IN_DOUBLE_QUOTES)
        componentList.Add(x)

        x = New ComponentClass
        x.name = "TargetLogError(ByRef p_error)"
        x.id = ComponentClass.IDType.TARGET_LOG_ERROR
        x.filename = "TargetLogError"
        componentList.Add(x)

        x = New ComponentClass
        x.name = "WrapInQuotes(ByRef p)"
        x.id = ComponentClass.IDType.WRAP_IN_DOUBLE_QUOTES
        x.filename = "WrapInQuotes"
        componentList.Add(x)

        x = New ComponentClass
        x.name = "UpdateDeliveredTimeTable(ByRef p_callId)"
        x.id = ComponentClass.IDType.UPDATE_DELIVERED_TIME_TABLE
        x.filename = "UpdateDeliveredTimeTable"
        x.needs.Add(ComponentClass.IDType.DB_EXECUTE)
        componentList.Add(x)

        x = New ComponentClass
        x.name = "Timestamp(ByRef p_table, ByRef p_myKey, ByVal p_callId, ByRef p_fieldName)"
        x.id = ComponentClass.IDType.TIMESTAMP
        x.filename = "Timestamp"
        x.needs.Add(ComponentClass.IDType.DB_EXECUTE)
        x.needs.Add(ComponentClass.IDType.WRAP_IN_SINGLE_QUOTES)
        x.needs.Add(ComponentClass.IDType.WRAP_IN_DOUBLE_QUOTES)
        x.notes = "Needs Stored Procedure: SP_TIMESTAMP"
        componentList.Add(x)

        x = New ComponentClass
        x.name = "TimestampEx(p_table AS STRING, p_myKey AS STRING, p_callId AS INT, p_fieldName AS STRING, p_ivrPath AS STRING, p_cascadeLevel AS INT, p_attempt AS INT)"
        x.id = ComponentClass.IDType.TIMESTAMP_EX
        x.filename = "TimestampEx"
        x.needs.Add(ComponentClass.IDType.DB_EXECUTE)
        x.needs.Add(ComponentClass.IDType.WRAP_IN_SINGLE_QUOTES)
        x.needs.Add(ComponentClass.IDType.WRAP_IN_DOUBLE_QUOTES)
        x.notes = "Needs Stored Procedure: SP_TIMESTAMP_EX"
        componentList.Add(x)

        x = New ComponentClass
        x.name = "IsAgentFree(p)"
        x.id = ComponentClass.IDType.IS_AGENT_FREE
        x.filename = "IsAgentFree"
        x.needs.Add(ComponentClass.IDType.WRAP_IN_DOUBLE_QUOTES)
        x.needs.Add(ComponentClass.IDType.GET_USER_BY_ADDRESS_WRAPPER)
        componentList.Add(x)

        x = New ComponentClass
        x.name = "Queue Routines"
        x.id = ComponentClass.IDType.QUEUE_ROUTINES
        x.filename = "Queue Routines"
        x.needs.Add(ComponentClass.IDType.DB_EXECUTE)
        x.needs.Add(ComponentClass.IDType.WRAP_IN_SINGLE_QUOTES)
        x.needs.Add(ComponentClass.IDType.DB_READ_SCALAR)
        x.needs.Add(ComponentClass.IDType.IS_AGENT_FREE)

        x.notes = "Needs Stored Procedures: SP_GET_POSITION_IN_QUEUE, SP_ADD_CALL_TO_QUEUE, SP_CHANGE_QUEUE_STATE and SP_REMOVE_CALL_FROM_QUEUE" & vbCrLf & "AddCallToQueue(callId [0 = auto], queueId, tag)" & vbCrLf & "RemoveCallFromQueue(callId [0 = auto])" & vbCrLf & "ChangeQueueState(callId [0 = auto], state)"
        componentList.Add(x)

        x = New ComponentClass
        x.name = "Least Used Agent Distributor"
        x.id = ComponentClass.IDType.LEAST_USED_AGENT_DISTRIBUTOR
        x.filename = "LeastUsedAgentDistributor"
        x.needs.Add(ComponentClass.IDType.DB_READ_SCALAR)
        x.needs.Add(ComponentClass.IDType.WRAP_IN_SINGLE_QUOTES)
        x.needs.Add(ComponentClass.IDType.USERINFOLISTCLASS)
        x.needs.Add(ComponentClass.IDType.WRAP_IN_DOUBLE_QUOTES)
        x.needs.Add(ComponentClass.IDType.DB_EXECUTE)
        'x.needs.Add(ComponentClass.IDType.WILDCARDSWYXGROUP)
        x.needs.Add(ComponentClass.IDType.CONSTANTS)
        x.needs.Add(ComponentClass.IDType.GROUP_AVAILABLE)
        'x.needs.Add(ComponentClass.IDType.GET_COMPOSITE_GROUP_MEMBERS)
        x.needs.Add(ComponentClass.IDType.SIBB_PAUSE)
        x.needs.Add(ComponentClass.IDType.SINGLE_QUOTE_CHECK)

        x.notes = "Needs Stored Procedure: SP_CHECK_AGENTS"
        componentList.Add(x)

        x = New ComponentClass
        x.name = "UserInfoListClass"
        x.id = ComponentClass.IDType.USERINFOLISTCLASS
        x.filename = "UserInfoListClass"
        componentList.Add(x)

        x = New ComponentClass
        x.name = "WildcardSwyxGroup"
        x.id = ComponentClass.IDType.WILDCARDSWYXGROUP
        x.filename = "WildcardSwyxGroup"
        x.needs.Add(ComponentClass.IDType.DB_READ_SCALAR)
        x.needs.Add(ComponentClass.IDType.WRAP_IN_SINGLE_QUOTES)
        x.needs.Add(ComponentClass.IDType.WRAP_IN_DOUBLE_QUOTES)
        componentList.Add(x)

        x = New ComponentClass
        x.name = "Scripts"
        x.id = ComponentClass.IDType.SCRIPTS
        x.filename = "Scripts"
        x.needs.Add(ComponentClass.IDType.DB_RETURN_RESULTS)
        x.needs.Add(ComponentClass.IDType.WRAP_IN_SINGLE_QUOTES)
        x.needs.Add(ComponentClass.IDType.WRAP_IN_DOUBLE_QUOTES)
        x.needs.Add(ComponentClass.IDType.CONVERT_DATE_TO_ISO)
        x.needs.Add(ComponentClass.IDType.DB_EXECUTE)
        x.needs.Add(ComponentClass.IDType.SINGLE_QUOTE_CHECK)
        componentList.Add(x)

        x = New ComponentClass
        x.name = "Infront Pegging"
        x.id = ComponentClass.IDType.INFRONT_PEGGING
        x.filename = "InfrontPegging"
        x.needs.Add(ComponentClass.IDType.DELTA_STATS_FIELD)
        x.needs.Add(ComponentClass.IDType.SET_VALUE_FOR_CALL_ID)
        componentList.Add(x)

        x = New ComponentClass
        x.name = "DeltaStatsField"
        x.id = ComponentClass.IDType.DELTA_STATS_FIELD
        x.filename = "DeltaStatsField"
        x.needs.Add(ComponentClass.IDType.GENERATE_DATABASE_CONNECTION_STRING)
        x.needs.Add(ComponentClass.IDType.WRAP_IN_SINGLE_QUOTES)
        x.needs.Add(ComponentClass.IDType.WRAP_IN_DOUBLE_QUOTES)
        x.notes = "Needs Stored Procedure: SP_DAILY_UPDATE_TABLE"
        componentList.Add(x)

        x = New ComponentClass
        x.name = "SetValueForCallId"
        x.id = ComponentClass.IDType.SET_VALUE_FOR_CALL_ID
        x.filename = "SetValueForCallId"
        x.needs.Add(ComponentClass.IDType.GENERATE_DATABASE_CONNECTION_STRING)
        x.needs.Add(ComponentClass.IDType.WRAP_IN_SINGLE_QUOTES)
        componentList.Add(x)

        x = New ComponentClass
        x.name = "AgentAvailable(agent)"
        x.id = ComponentClass.IDType.AGENT_AVAILABLE
        x.filename = "AgentAvailable"
        x.needs.Add(ComponentClass.IDType.CONSTANTS)
        componentList.Add(x)

        x = New ComponentClass
        x.name = "GroupAvailable(group, useYellow)"
        x.id = ComponentClass.IDType.GROUP_AVAILABLE
        x.filename = "GroupAvailable"
        x.needs.Add(ComponentClass.IDType.CONSTANTS)
        x.needs.Add(ComponentClass.IDType.USERINFOLISTCLASS)
        x.needs.Add(ComponentClass.IDType.GET_COMPOSITE_GROUP_MEMBERS)
        componentList.Add(x)

        x = New ComponentClass
        x.name = "GetGroupMembers"
        x.id = ComponentClass.IDType.GET_GROUP_MEMBERS
        x.filename = "GetGroupMembers"
        'x.needs.Add(ComponentClass.IDType.WILDCARDSWYXGROUP)
        x.needs.Add(ComponentClass.IDType.WRAP_IN_DOUBLE_QUOTES)
        x.needs.Add(ComponentClass.IDType.AGENT_DATA)
        x.needs.Add(ComponentClass.IDType.LOG_TRACE_DB)
        x.needs.Add(ComponentClass.IDType.GET_USER_BY_ADDRESS_WRAPPER)
        componentList.Add(x)

        x = New ComponentClass
        x.name = "ExpandGroupRanges"
        x.id = ComponentClass.IDType.EXPAND_GROUP_RANGES
        x.filename = "ExpandGroupRanges"
        x.needs.Add(ComponentClass.IDType.WRAP_IN_DOUBLE_QUOTES)
        x.needs.Add(ComponentClass.IDType.IS_DIGIT)
        componentList.Add(x)

        x = New ComponentClass
        x.name = "GetCompositeGroupMembers"
        x.id = ComponentClass.IDType.GET_COMPOSITE_GROUP_MEMBERS
        x.filename = "GetCompositeGroupMembers"
        x.needs.Add(ComponentClass.IDType.WRAP_IN_DOUBLE_QUOTES)
        x.needs.Add(ComponentClass.IDType.GET_GROUP_MEMBERS)
        x.needs.Add(ComponentClass.IDType.EXPAND_GROUP_RANGES)
        componentList.Add(x)

        x = New ComponentClass
        x.name = "IsDigit"
        x.id = ComponentClass.IDType.IS_DIGIT
        x.filename = "IsDigit"
        componentList.Add(x)

        x = New ComponentClass
        x.name = "Infront Variables"
        x.id = ComponentClass.IDType.INFRONT_VARIABLES
        x.filename = "InfrontVariables"
        componentList.Add(x)

        x = New ComponentClass
        x.name = "CallMachineClass"
        x.id = ComponentClass.IDType.CALL_MACHINE_CLASS
        x.filename = "CallMachineClass"
        x.needs.Add(ComponentClass.IDType.CALL_MACHINE_TARGET_CLASS)
        componentList.Add(x)

        x = New ComponentClass
        x.name = "CallMachineTargetClass"
        x.id = ComponentClass.IDType.CALL_MACHINE_TARGET_CLASS
        x.filename = "CallMachineTargetClass"
        componentList.Add(x)

        x = New ComponentClass
        x.name = "ConvertDateToISO"
        x.id = ComponentClass.IDType.CONVERT_DATE_TO_ISO
        x.filename = "ConvertDateToISO"
        componentList.Add(x)

        x = New ComponentClass
        x.name = "Hold"
        x.id = ComponentClass.IDType.HOLD
        x.filename = "Hold"
        componentList.Add(x)

        x = New ComponentClass
        x.name = "Sleep"
        x.id = ComponentClass.IDType.SLEEP
        x.filename = "Sleep"
        componentList.Add(x)

        x = New ComponentClass
        x.name = "Pause"
        x.id = ComponentClass.IDType.PAUSE
        x.filename = "Pause"
        x.needs.Add(ComponentClass.IDType.WRAP_IN_DOUBLE_QUOTES)
        x.needs.Add(ComponentClass.IDType.GROUP_AVAILABLE)

        componentList.Add(x)

        x = New ComponentClass
        x.name = "Bootstrap"
        x.id = ComponentClass.IDType.BOOTSTRAP
        x.filename = "Bootstrap"
        x.needs.Add(ComponentClass.IDType.WRAP_IN_SINGLE_QUOTES)
        x.needs.Add(ComponentClass.IDType.DB_RETURN_RESULTS)
        x.needs.Add(ComponentClass.IDType.SIBB_CLASS)
        x.needs.Add(ComponentClass.IDType.CONVERT_DATE_TO_ISO)
        x.needs.Add(ComponentClass.IDType.SINGLE_QUOTE_CHECK)
        componentList.Add(x)

        x = New ComponentClass
        x.name = "Single Quote Check"
        x.id = ComponentClass.IDType.SINGLE_QUOTE_CHECK
        x.filename = "SingleQuoteCheck"
        componentList.Add(x)

        x = New ComponentClass
        x.name = "Manual Status"
        x.id = ComponentClass.IDType.MANUAL_STATUS
        x.filename = "ManualStatus"
        x.needs.Add(ComponentClass.IDType.DB_EXECUTE)
        x.needs.Add(ComponentClass.IDType.WRAP_IN_SINGLE_QUOTES)
        x.needs.Add(ComponentClass.IDType.SINGLE_QUOTE_CHECK)
        componentList.Add(x)

        x = New ComponentClass
        x.name = "SIBB Class"
        x.id = ComponentClass.IDType.SIBB_CLASS
        x.filename = "SIBBClass"
        x.needs.Add(ComponentClass.IDType.WRAP_IN_DOUBLE_QUOTES)
        componentList.Add(x)

        x = New ComponentClass
        x.name = "SIBB Connect"
        x.id = ComponentClass.IDType.SIBB_CONNECT
        x.filename = "SIBB_Connect"
        x.needs.Add(ComponentClass.IDType.WRAP_IN_DOUBLE_QUOTES)
        componentList.Add(x)

        x = New ComponentClass
        x.name = "SIBB Pause"
        x.id = ComponentClass.IDType.SIBB_PAUSE
        x.filename = "SIBB_Pause"
        x.needs.Add(ComponentClass.IDType.WRAP_IN_DOUBLE_QUOTES)
        x.needs.Add(ComponentClass.IDType.PAUSE)
        componentList.Add(x)

        x = New ComponentClass
        x.name = "SIBB Play Announcement"
        x.id = ComponentClass.IDType.SIBB_PA
        x.filename = "SIBB_PlayAnnouncement"
        x.needs.Add(ComponentClass.IDType.WRAP_IN_DOUBLE_QUOTES)
        componentList.Add(x)

        x = New ComponentClass
        x.name = "SIBB Longest Waiting"
        x.id = ComponentClass.IDType.SIBB_LONGEST_WAIT
        x.filename = "SIBB_LongestWaiting"
        x.needs.Add(ComponentClass.IDType.WRAP_IN_DOUBLE_QUOTES)
        x.needs.Add(ComponentClass.IDType.LEAST_USED_AGENT_DISTRIBUTOR)
        componentList.Add(x)

        x = New ComponentClass
        x.name = "SIBB Get DTMF String"
        x.id = ComponentClass.IDType.SIBB_GET_DTMF_STRING
        x.filename = "SIBB_GetDTMFString"
        x.needs.Add(ComponentClass.IDType.WRAP_IN_DOUBLE_QUOTES)
        componentList.Add(x)

        x = New ComponentClass
        x.name = "SIBB End Call"
        x.id = ComponentClass.IDType.SIBB_END_CALL
        x.filename = "SIBB_EndCall"
        x.needs.Add(ComponentClass.IDType.END_CALL)
        componentList.Add(x)

        x = New ComponentClass
        x.name = "SIBB Group Available"
        x.id = ComponentClass.IDType.SIBB_GROUP_AVAILABLE
        x.filename = "SIBB_GroupAvailable"
        x.needs.Add(ComponentClass.IDType.WRAP_IN_DOUBLE_QUOTES)
        x.needs.Add(ComponentClass.IDType.GROUP_AVAILABLE)
        componentList.Add(x)

        x = New ComponentClass
        x.name = "SIBB Hold"
        x.id = ComponentClass.IDType.SIBB_HOLD
        x.filename = "SIBB_Hold"
        x.needs.Add(ComponentClass.IDType.WRAP_IN_DOUBLE_QUOTES)
        x.needs.Add(ComponentClass.IDType.HOLD)
        componentList.Add(x)

        x = New ComponentClass
        x.name = "SIBB Sleep"
        x.id = ComponentClass.IDType.SIBB_SLEEP
        x.filename = "SIBB_Sleep"
        x.needs.Add(ComponentClass.IDType.WRAP_IN_DOUBLE_QUOTES)
        x.needs.Add(ComponentClass.IDType.SLEEP)
        componentList.Add(x)

        x = New ComponentClass
        x.name = "SIBB Holiday"
        x.id = ComponentClass.IDType.SIBB_HOLIDAY
        x.filename = "SIBB_Holiday"
        x.needs.Add(ComponentClass.IDType.DB_READ_SCALAR)
        componentList.Add(x)

        x = New ComponentClass
        x.name = "Test Cases"
        x.id = ComponentClass.IDType.TEST_CASES
        x.filename = "Testcases"
        componentList.Add(x)

        x = New ComponentClass(True)
        x.name = "Base"
        x.id = ComponentClass.IDType.BASE
        x.filename = "Base"
        componentList.Add(x)

        x = New ComponentClass
        x.name = "LogTraceDB"
        x.id = ComponentClass.IDType.LOG_TRACE_DB
        x.filename = "LogTraceDB"
        x.needs.Add(ComponentClass.IDType.DB_EXECUTE)
        'x.needs.Add(ComponentClass.IDType.BOOTSTRAP)
        x.needs.Add(ComponentClass.IDType.WRAP_IN_SINGLE_QUOTES)
        x.needs.Add(ComponentClass.IDType.WRAP_IN_DOUBLE_QUOTES)
        componentList.Add(x)

        x = New ComponentClass
        x.name = "GetUserByAddressWrapper"
        x.id = ComponentClass.IDType.GET_USER_BY_ADDRESS_WRAPPER
        x.filename = "GetUserByAddressWrapper"
        componentList.Add(x)

        x = New ComponentClass
        x.name = "AgentData"
        x.id = ComponentClass.IDType.AGENT_DATA
        x.filename = "AgentData"
        x.needs.Add(ComponentClass.IDType.DB_READ_SCALAR)
        x.needs.Add(ComponentClass.IDType.WRAP_IN_SINGLE_QUOTES)
        componentList.Add(x)

        x = New ComponentClass
        x.name = "SIBB Add Call To Queue"
        x.id = ComponentClass.IDType.SIBB_ADD_CALL_TO_QUEUE
        x.filename = "SIBB_AddCallToQueue"
        x.needs.Add(ComponentClass.IDType.WRAP_IN_DOUBLE_QUOTES)
        x.needs.Add(ComponentClass.IDType.QUEUE_ROUTINES)
        componentList.Add(x)

        x = New ComponentClass
        x.name = "SIBB Change Queue State"
        x.id = ComponentClass.IDType.SIBB_CHANGE_QUEUE_STATE
        x.filename = "SIBB_ChangeQueueState"
        x.needs.Add(ComponentClass.IDType.WRAP_IN_DOUBLE_QUOTES)
        x.needs.Add(ComponentClass.IDType.QUEUE_ROUTINES)
        componentList.Add(x)

        x = New ComponentClass
        x.name = "SIBB Get Position In Queue"
        x.id = ComponentClass.IDType.SIBB_GET_POSITION_IN_QUEUE
        x.filename = "SIBB_GetPositionQueue"
        x.needs.Add(ComponentClass.IDType.WRAP_IN_DOUBLE_QUOTES)
        x.needs.Add(ComponentClass.IDType.QUEUE_ROUTINES)
        componentList.Add(x)

        x = New ComponentClass
        x.name = "SIBB Remove Call From Queue"
        x.id = ComponentClass.IDType.SIBB_REMOVE_CALL_FROM_QUEUE
        x.filename = "SIBB_RemoveCallFromQueue"
        x.needs.Add(ComponentClass.IDType.WRAP_IN_DOUBLE_QUOTES)
        x.needs.Add(ComponentClass.IDType.QUEUE_ROUTINES)
        componentList.Add(x)

        x = New ComponentClass
        x.name = "SIBB Queue Pause"
        x.id = ComponentClass.IDType.SIBB_QUEUE_PAUSE
        x.filename = "SIBB_QueuePause"
        x.needs.Add(ComponentClass.IDType.WRAP_IN_DOUBLE_QUOTES)
        x.needs.Add(ComponentClass.IDType.QUEUE_ROUTINES)
        componentList.Add(x)

        x = New ComponentClass
        x.name = "SIBB Top Of Queue"
        x.id = ComponentClass.IDType.SIBB_TOP_OF_QUEUE
        x.filename = "SIBB_TopOfQueue"
        x.needs.Add(ComponentClass.IDType.WRAP_IN_DOUBLE_QUOTES)
        x.needs.Add(ComponentClass.IDType.QUEUE_ROUTINES)
        componentList.Add(x)

        x = New ComponentClass
        x.name = "SIBB Log Point"
        x.id = ComponentClass.IDType.SIBB_LOG_POINT
        x.filename = "SIBB_LogPoint"
        x.needs.Add(ComponentClass.IDType.WRAP_IN_DOUBLE_QUOTES)
        x.needs.Add(ComponentClass.IDType.WRAP_IN_SINGLE_QUOTES)
        componentList.Add(x)

        x = New ComponentClass
        x.name = "SIBB Log Agent Status"
        x.id = ComponentClass.IDType.LOG_AGENT_STATUS
        x.filename = "SIBB_LogAgentStatus"
        x.needs.Add(ComponentClass.IDType.WRAP_IN_DOUBLE_QUOTES)
        x.needs.Add(ComponentClass.IDType.GET_GROUP_MEMBERS)
        componentList.Add(x)

        x = New ComponentClass
        x.name = "IntToFileName"
        x.id = ComponentClass.IDType.INT_TO_FILENAME
        x.filename = "IntToFileName"
        componentList.Add(x)

        x = New ComponentClass
        x.name = "AreWeUsingOverflow"
        x.id = ComponentClass.IDType.ARE_WE_USING_OVERFLOW
        x.filename = "AreWeUsingOverflow"
        x.needs.Add(ComponentClass.IDType.BASE)
        componentList.Add(x)

        ' Check references
        If Not CheckReferences() Then Close()

        ' Populate the Available Components list view on the Add Components form
        With AddComponentsForm.availableComponentsListView.Items
            .Clear()

            For i = 0 To componentList.Count - 1
                .Add(componentList(i).name)

                ' Does anybody need us ?
                Dim flag As Boolean = False

                For j = 0 To componentList.Count - 1
                    If j <> i Then
                        If componentList(j).needs.Contains(componentList(i).id) Then
                            flag = True
                            Exit For
                        End If
                    End If
                Next

                If flag Then
                    ' Somebody needs us. Do we need anybody else ?
                    If componentList(i).needs.Count > 0 Then .Item(i).ForeColor = Color.RoyalBlue
                Else
                    ' Nobody needs us
                    .Item(i).ForeColor = Color.Fuchsia
                End If
            Next
        End With
    End Sub

    Private Sub AddComponenetsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AddComponenetsToolStripMenuItem.Click
        AddComponentsForm.ShowDialog()
    End Sub

    Private Function CheckReferences() As Boolean
        Dim result As Boolean = True

        For i = 0 To componentList.Count - 1
            With componentList(i)
                For j = 0 To .needs.Count - 1
                    Dim myNeedsId As ComponentClass.IDType = .needs(j)
                    Dim myIndex As Integer = AddComponentsForm.GetIndexOfComponentFromId(myNeedsId)

                    If myIndex = -1 Then
                        MsgBox("Error: Component " & WrapInQuotes(.name) & " needs copmponent " & myNeedsId.ToString & " which is not bound")
                        result = False
                        Exit For
                    End If
                Next
            End With

            If result = False Then Exit For
        Next

        Return result
    End Function

    Public Function ListViewContainsText(ByRef p_listView As ListView, ByRef p_text As String) As Boolean
        Dim result As Boolean = False

        For i = 0 To p_listView.Items.Count - 1
            If p_listView.Items(i).Text = p_text Then
                result = True
                Exit For
            End If
        Next

        Return result
    End Function

    Public Sub RemoveFromListView(ByRef p_listView As ListView, ByRef p As String)
        Dim myIndex As Integer = -1

        For i = 0 To p_listView.Items.Count - 1
            If p_listView.Items(i).Text = p Then
                myIndex = i
                Exit For
            End If
        Next

        If myIndex >= 0 Then p_listView.Items.RemoveAt(myIndex)
    End Sub

    Private Sub RenderCode(ByRef p_list As List(Of String), ByVal p_asNativeVbs As Boolean)
        Dim itemToRemove As String = ""
        Dim itemToReAdd As String = ""
        Dim vbsLogName As String = "DebugLogError(ByRef p_error)"
        Dim swyxLogName As String = "TargetLogError(ByRef p_error)"
        Dim checksumLineIndex As Integer = -1
        Dim checksumValue As Integer = 0
        Dim baseList As New List(Of String)

        p_list.Clear()

        p_list.Add("' ReachAll ECR Designer for Swyx")
        p_list.Add("' Autogenerated code")

        If AddComponentsForm.fromFile <> "" Then
            Dim myString As String = ""

            If AddComponentsForm.modifiedFlag Then myString = " (Modified)"

            p_list.Add("' From config file: " & WrapInQuotes(AddComponentsForm.fromFile) & myString)
        End If

        p_list.Add("' Copyright Ian Shippen " & Now.Year)
        p_list.Add("' " & Now.ToString)

        p_list.Add("' Source Modules") ' For list of source modules

        If p_asNativeVbs Then
            If ListViewContainsText(AddComponentsForm.selectedComponentsListView, swyxLogName) Then
                ' Remove it for now and re-add it later
                RemoveFromListView(AddComponentsForm.selectedComponentsListView, swyxLogName)
                itemToReAdd = swyxLogName

                For i = 0 To AddComponentsForm.selectedComponentsListView.Items.Count - 1
                    '   MsgBox(AddComponentsForm.selectedComponentsListView.Items(i).Text)
                Next
            End If

            If Not ListViewContainsText(AddComponentsForm.selectedComponentsListView, vbsLogName) Then
                AddComponentsForm.selectedComponentsListView.Items.Add(vbsLogName)
                itemToRemove = vbsLogName
            End If
        Else
            If ListViewContainsText(AddComponentsForm.selectedComponentsListView, vbsLogName) Then
                ' Remove it for now and re-add it later
                RemoveFromListView(AddComponentsForm.selectedComponentsListView, vbsLogName)
                itemToReAdd = vbsLogName
            End If

            If Not ListViewContainsText(AddComponentsForm.selectedComponentsListView, swyxLogName) Then
                AddComponentsForm.selectedComponentsListView.Items.Add(swyxLogName)
                itemToRemove = swyxLogName
            End If
        End If

        ' Create the base list
        Dim mySortedList As New List(Of String)

        For i = 0 To AddComponentsForm.selectedComponentsListView.Items.Count - 1
            Dim myIndex As Integer = AddComponentsForm.GetIndexOfComponentFromName(AddComponentsForm.selectedComponentsListView.Items(i).Text)

            If myIndex >= 0 Then
                Dim myFilename As String = componentList(myIndex).filename

                If myFilename <> "" Then mySortedList.Add(myFilename)
            End If
        Next

        mySortedList.Sort()

        For Each myFilename As String In mySortedList
            baseList.Add("baseUsesArrayList.Add " & WrapInQuotes(myFilename))
            p_list.Add("'  " & WrapInQuotes(myFilename))
        Next

        p_list.Add("") ' For checksum
        checksumLineIndex = p_list.Count - 1

        p_list.Add("")

        For i = 0 To AddComponentsForm.selectedComponentsListView.Items.Count - 1
            Dim myIndex As Integer = AddComponentsForm.GetIndexOfComponentFromName(AddComponentsForm.selectedComponentsListView.Items(i).Text)

            If myIndex >= 0 Then
                Dim myFilename As String = componentList(myIndex).filename

                If myFilename <> "" Then
                    Dim z As IO.FileStream = Nothing
                    Dim path As New WindowsPathClass(System.AppDomain.CurrentDomain.BaseDirectory())
                    Dim errorOccured As Boolean = False

                    If Not myFilename.ToLower.EndsWith(".txt") Then myFilename = myFilename & ".txt"

                    myFilename = path.CreateFullName("VBScripts\" & myFilename)

                    Try
                        z = New IO.FileStream(myFilename, IO.FileMode.Open)
                    Catch ex As Exception
                        errorOccured = True
                        MsgBox(ex.Message)
                    End Try

                    If Not errorOccured Then
                        Dim myReader As New IO.StreamReader(z)
                        Dim reading As Boolean = True

                        If i > 0 Then p_list.Add("")

                        While reading
                            Dim myLine As String = myReader.ReadLine

                            If myLine Is Nothing Then
                                reading = False
                            Else
                                If p_asNativeVbs Then
                                    myLine = myLine.Replace("PBXScript.", "WScript.")

                                    If myLine.ToLower.Contains("mypbxconfig.initialize ") Then myLine = ""
                                Else
                                    If myLine.EndsWith("KEEP AS WSCRIPT") Then
                                        myLine = myLine.Substring(0, myLine.IndexOf("KEEP AS WSCRIPT")).TrimEnd.TrimEnd("'").TrimEnd
                                    Else
                                        myLine = myLine.Replace("WScript.", "PbxScript.")
                                    End If
                                End If

                                ' If myLine.Contains("Class SIBBClass") Then MsgBox(myLine)
                                p_list.Add(myLine)
                            End If
                        End While

                        myReader.Close()

                        z.Close()

                        p_list.Add("")

                        If componentList(myIndex).id = ComponentClass.IDType.BASE Then
                            For Each myLine As String In baseList
                                p_list.Add(myLine)
                            Next
                        End If
                    End If
                Else
                    MsgBox("Error: No source filename defined for " & componentList(myIndex).name)
                End If
            End If
        Next

        If itemToRemove <> "" Then RemoveFromListView(AddComponentsForm.selectedComponentsListView, itemToRemove)

        For i = checksumLineIndex + 1 To p_list.Count - 1
            With p_list(i)
                For j = 0 To .Length - 1
                    checksumValue += Asc(p_list(i)(j))
                Next
            End With

            checksumValue = checksumValue Mod 65536
        Next

        p_list(checksumLineIndex) = "' " & p_list.Count - (checksumLineIndex + 1) & " Lines - Checksum = " & checksumValue
    End Sub

    Private Sub RenderCodeToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RenderCodeToolStripMenuItem.Click
        Dim x As New SaveFileDialog
        Dim nativeVbs As Boolean = False

        x.Filter = "Text Files (*.txt)|*.txt|Vbs Files (*.vbs)|*.vbs"

        If x.ShowDialog = Windows.Forms.DialogResult.OK Then
            Dim y As New IO.FileStream(x.FileName, IO.FileMode.Create)
            Dim myWriter As New IO.StreamWriter(y)
            Dim myList As New List(Of String)

            RenderCode(myList, x.FileName.ToLower.EndsWith(".vbs"))

            For Each myLine As String In myList
                myWriter.WriteLine(myLine)
            Next

            myWriter.Close()
            y.Close()

            MsgBox("Done ..")
        End If
    End Sub

    Private Sub ViewCodeToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ViewCodeToolStripMenuItem.Click
        Dim myList As New List(Of String)

        RenderCode(myList, False)

        Dim myLines(myList.Count - 1) As String

        For i = 0 To myList.Count - 1
            myLines(i) = myList(i)
        Next

        ViewCodeForm.viewCodeTextBox.Lines = myLines
        ViewCodeForm.ShowDialog()
    End Sub

    Private Sub DesignerToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DesignerToolStripMenuItem.Click
        DesignerForm.ShowDialog()
    End Sub

    Private Sub AboutToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AboutToolStripMenuItem.Click
        AboutForm.ShowDialog()
    End Sub

    Private Sub BuildTablesToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BuildTablesToolStripMenuItem.Click
        Dim result As Boolean = True
        Dim allTablesExist As Boolean = True

        tableDefs.Clear()

        tableDefs.Add("Scripts", "functionName varchar(256) NOT NULL, lineNumber int NOT NULL, code varchar(MAX), class varchar(256)")
        tableDefs.Add("Scripts_History", "functionName varchar(256) NOT NULL, lineNumber int NOT NULL, code varchar(MAX), class varchar(256), timestamp datetime")
        tableDefs.Add("ServiceBuilderDebugTable", "scriptName varchar(256), callid int, state varchar(256)")
        tableDefs.Add("ServiceBuilderEventTable", "timeStamp datetime NOT NULL, scriptName varchar(256) NOT NULL, callid int NOT NULL, node int NOT NULL, data varchar(4096), output int, nextNode int")
        tableDefs.Add("ServiceBuilderLogPointTable", "callId int, logPoint_0 datetime, logPoint_1 datetime, logPoint_2 datetime, logPoint_3 datetime, logPoint_4 datetime, logPoint_5 datetime, logPoint_6 datetime, logPoint_7 datetime, logPoint_8 datetime, logPoint_9 datetime")
        tableDefs.Add("ServiceBuilderTable", "scriptName varchar(256), nodeNumber int, nodeType varchar(256), data varchar(MAX), outputs varchar(256), title varchar(256), internalReference varchar(256)")
        tableDefs.Add("ServiceBuilderTraceTable", "timestamp datetime, callId int, module varchar(256), status int, info varchar(256)")
        tableDefs.Add("SIBBDebugOutputTable", "sibbIndex int, outputIndex int, nextNodeIndex int, points varchar(4096), filteredPoints varchar(4096)")
        tableDefs.Add("SIBBDebugTable", "timestamp datetime, sibbHeading varchar(256), sibbFooter varchar(256), sibbIndex int")
        tableDefs.Add("SIBBNeedsTable", "SIBB varchar(256), Needs varchar(256)")
        tableDefs.Add("SIBBNeedsTable_History", "timestamp datetime, SIBB varchar(256), Needs varchar(256)")
        tableDefs.Add("AgentDataMasterTable", "myKey int identity(1, 1), timestamp datetime, callId int, calledNumber varchar(256), target varchar(256)")
        tableDefs.Add("AgentDataTable", "myKey int, userId int, userState int")

        ' Our database schema has 2 extra fields: customeOpeningTime TIME(7) and customClosingTime TIME(7) not used for Ecourier - probably used for Infront
        tableDefs.Add("BankHolidayTable", "date varchar(256), description varchar(256)")

        ' QueueId used to have the type VARCHAR(128) instead of INT - our database schema and Ecourier use INT
        tableDefs.Add("CallQueueTable", "CallId int, Timestamp datetime, QueueId varchar(256), Tag varchar(128), State varchar(16), Rings int, LastHeartBeat datetime")

        ' Check if all tables already exist
        For Each myTableName As String In tableDefs.Keys
            If Not DatabaseAccess.DoesDatabaseTableExist(settingsConfigDictionary, myTableName) Then
                allTablesExist = False
                Exit For
            End If
        Next

        If allTablesExist Then
            MsgBox("All the tables already exist ..")
        Else
            For Each myTableName As String In tableDefs.Keys
                Dim myDefs As String = tableDefs(myTableName)

                If Not CreateTable(myTableName, myDefs, False) Then result = False
            Next

            If result Then MsgBox("All tables created successfully")
        End If

        Select Case MsgBox("Do you want to populate the Scripts table ?", MsgBoxStyle.YesNo)
            Case MsgBoxResult.Yes
                ' Check if it already has data in it ..
                Dim myTable As New DataTable
                Dim mySql As String = "select * from Scripts"

                If FillTableFromCommand(CreateConnectionString(settingsConfigDictionary), mySql, myTable) Then
                    If myTable.Rows.Count > 0 Then
                        Select Case MsgBox("Scripts table is already populated - do you want to clear it first ?", MsgBoxStyle.YesNoCancel)
                            Case MsgBoxResult.Yes
                                mySql = "delete from Scripts"
                                ExecuteNonQuery(CreateConnectionString(settingsConfigDictionary), mySql)

                            Case MsgBoxResult.Cancel
                                Return
                        End Select
                    End If

                    Dim mySourceFolder As String = DesignerForm.GetScriptsFolder

                    If mySourceFolder <> "" Then
                        Dim myFileNames() As String = IO.Directory.GetFiles(mySourceFolder)
                        Dim myCount As Integer = 0

                        Label2.Visible = True
                        ProgressBar1.Visible = True
                        ProgressBar1.Maximum = myFileNames.Count
                        ProgressBar1.Value = 0
                        'For Each myScriptName As String In defaultScriptsTableContents
                        'Dim myFileName As String = DesignerForm.FindScriptFile(mySourceFolder, myScriptName)

                        'If myFileName <> "" Then
                        'DesignerForm.CopyFunctionToDatabase(myFileName, False, False, False)
                        'End If
                        'Next

                        For Each myFileName As String In myFileNames
                            Label1.Text = "Copying " & myFileName & " to Scripts database table"
                            DesignerForm.CopyFunctionToDatabase(myFileName, False, False, False)
                            ProgressBar1.Value = myCount
                            Me.Refresh()
                            System.Threading.Thread.Sleep(100)
                            myCount += 1
                        Next

                        MsgBox("Done ..")
                        ProgressBar1.Visible = False
                        Label2.Visible = False
                    End If
                End If
        End Select

        Select Case MsgBox("Do you want to install the stored procedures ?", MsgBoxStyle.YesNo)
            Case MsgBoxResult.Yes
                Dim myStoredProcedures As New List(Of String)
                Dim allPresent As Boolean = True

                With myStoredProcedures
                    .Add("SP_ADD_CALL_TO_QUEUE")
                    .Add("SP_CHANGE_QUEUE_STATE")
                    .Add("SP_CHECK_AGENTS")
                    .Add("SP_CHECK_AGENTS_EX")
                    .Add("SP_GET_POSITION_IN_QUEUE")
                    .Add("SP_GET_POSITION_IN_QUEUE_EX")
                    .Add("SP_GET_TOP_OF_QUEUE_CALLID")
                    .Add("SP_REMOVE_CALL_FROM_QUEUE")
                    .Add("SP_ScanEventTable")
                    .Add("SP_UPDATE_STATUS_TABLE")
                End With

                For Each myStoredProcedure As String In myStoredProcedures
                    If Not DoesDatabaseStoredProcedureExist(settingsConfigDictionary, myStoredProcedure) Then
                        allPresent = False
                        Exit For
                    End If
                Next

                If allPresent Then
                    MsgBox("All stored procedures are already installed")
                Else
                    For Each myStoredProcedure As String In myStoredProcedures
                        If Not DoesDatabaseStoredProcedureExist(settingsConfigDictionary, myStoredProcedure) Then
                            Dim myCancelled As Boolean = False

                            InstallFunctionUtilities.InstallMsg("Installing stored procedure: " & myStoredProcedure, myStoredProcedure, settingsConfigDictionary, True, "Stored Procedures", myCancelled)
                        End If
                    Next

                    MsgBox("All stored procedures installed")
                End If
        End Select
    End Sub

    Private Function CreateTable(ByRef p_tableName As String, ByRef p_columnDefs As String, Optional ByVal p_showResult As Boolean = True) As Boolean
        Dim result As Boolean = False

        ' Check if table already exists
        If DatabaseAccess.DoesDatabaseTableExist(settingsConfigDictionary, p_tableName) Then
            MsgBox("Table: " & p_tableName & " already exists ..")
        Else
            Dim mySql As String = "create table " & p_tableName & " (" & p_columnDefs & ")"

            If ExecuteNonQuery(CreateConnectionString(settingsConfigDictionary), mySql) Then
                If p_showResult Then MsgBox(p_tableName & " table created OK")

                result = True
            Else
                MsgBox("Error: Could not create " & p_tableName & " table")
            End If
        End If

        Return result
    End Function

    Private Sub DatabaseSettingsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DatabaseSettingsToolStripMenuItem.Click
        EditDatabaseSettings()
    End Sub

    Private Sub EditDatabaseSettings()
        With DatabaseSettingsForm
            .dataSourceTextBox.Text = settingsConfigDictionary.GetItem("dataSource")
            .databaseTextBox.Text = settingsConfigDictionary.GetItem("databaseName")
            .userIdTextBox.Text = settingsConfigDictionary.GetItem("databaseUserId")
            .passwordTextBox.Text = settingsConfigDictionary.GetItem("databasePassword")
            .swyxDatabaseNameTextBox.Text = settingsConfigDictionary.GetItem("swyxDatabaseName")

            If .ShowDialog = Windows.Forms.DialogResult.OK Then
                settingsConfigDictionary.SetItem("dataSource", .dataSourceTextBox.Text)
                settingsConfigDictionary.SetItem("databaseName", .databaseTextBox.Text)
                settingsConfigDictionary.SetItem("databaseUserId", .userIdTextBox.Text)
                settingsConfigDictionary.SetItem("databasePassword", .passwordTextBox.Text)
                settingsConfigDictionary.SetItem("swyxDatabaseName", .swyxDatabaseNameTextBox.Text)

                GenericXMLConfigSaver(CONFIG_FILENAME, settingsConfigDictionary)
            End If
        End With
    End Sub

    Private Sub SettingsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SettingsToolStripMenuItem.Click
        SettingsForm.ShowDialog()
    End Sub

    Private Sub AnalyseCDRToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AnalyseCDRToolStripMenuItem.Click
        CDRSearchForm.Show()
    End Sub

    Private Sub DigToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DigToolStripMenuItem.Click
        Dim mySql As String = "select * from ippbxcdr as b left join ippbxcdr as z on b.transferredToCallId = z.CallId"
        Dim myTable As New DataTable
        Dim myCallIds As New List(Of String)

        mySql &= " left join ServiceBuilderEventTable as Y on b.CallId = Y.CallId AND Node = 14 AND Output IS NOT NULL"
        mySql &= " where ((B.DeliveredTime IS NOT NULL AND B.ConnectTime IS NULL AND Z.ConnectTime IS NULL AND ((B.CalledName <> '_RU_Main_number') "
        mySql &= "OR (B.CalledName = '_RU_Main_number' AND Output IS NOT NULL))) OR (Z.ConnectTime IS NOT NULL AND Z.DestinationName = '_IN_QUEUE')) "
        mySql &= "AND datepart(dw, B.StartTime) BETWEEN 2 AND 6 AND datepart(hour, B.StartTime) BETWEEN 8 AND 17"
        mySql &= " and b.starttime > '2016-07-01' and b.starttime < '2016-07-02'"
        mySql &= " and b.calledName = '_ru_main_number'"
        mySql &= " order by b.callid"

        If FillTableFromCommand(CreateConnectionString(Form1.settingsConfigDictionary), mySql, myTable) Then
            For i = 0 To myTable.Rows.Count - 1
                With myTable.Rows(i)
                    DigForm.TextBox1.AppendText(.Item(0) & vbCrLf)
                End With
            Next

            DigForm.ShowDialog()
        End If
    End Sub

    Private Sub JavascriptBuilderToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles JavascriptBuilderToolStripMenuItem.Click
        JavascriptBuilderForm.ShowDialog()
    End Sub
End Class
