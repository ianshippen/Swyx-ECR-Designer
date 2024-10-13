Public Class ComponentClass
    Enum IDType
        NULL
        AGENT_AVAILABLE
        AGENT_DATA
        ARE_WE_USING_OVERFLOW
        CF_LOOKUP
        CONSTANTS
        DB_RETURN_RESULTS
        DELTA_TABLE_VALUE
        BASE
        BOOTSTRAP
        CALL_MACHINE_CLASS
        CALL_MACHINE_TARGET_CLASS
        CONVERT_DATE_TO_ISO
        DB_EXECUTE
        DB_READ_SCALAR
        DEBUG_LOG_ERROR
        DELTA_STATS_FIELD
        END_CALL
        EXPAND_GROUP_RANGES
        GENERATE_DATABASE_CONNECTION_STRING
        GET_COMPOSITE_GROUP_MEMBERS
        GET_GROUP_MEMBERS
        GET_USER_BY_ADDRESS_WRAPPER
        GROUP_AVAILABLE
        HOLD
        INFRONT_PEGGING
        INFRONT_VARIABLES
        INT_TO_FILENAME
        IS_AGENT_FREE
        IS_DIGIT
        IS_TODAY_BANK_HOLIDAY
        LEAST_USED_AGENT_DISTRIBUTOR
        LOG_AGENT_STATUS
        LOG_TRACE_DB
        MANUAL_STATUS
        PAUSE
        QUEUE_ROUTINES
        SCRIPTS
        SET_VALUE_FOR_CALL_ID
        SIBB_ADD_CALL_TO_QUEUE
        SIBB_CHANGE_QUEUE_STATE
        SIBB_GET_POSITION_IN_QUEUE
        SIBB_REMOVE_CALL_FROM_QUEUE
        SIBB_QUEUE_PAUSE
        SIBB_TOP_OF_QUEUE
        SIBB_CLASS
        SIBB_CONNECT
        SIBB_END_CALL
        SIBB_GET_DTMF_STRING
        SIBB_GROUP_AVAILABLE
        SIBB_HOLD
        SIBB_HOLIDAY
        SIBB_LOG_POINT
        SIBB_LONGEST_WAIT
        SIBB_PA
        SIBB_PAUSE
        SIBB_SLEEP
        SINGLE_QUOTE_CHECK
        SLEEP
        TARGET_LOG_ERROR
        TEST_CASES
        TIMESTAMP
        TIMESTAMP_EX
        UPDATE_DELIVERED_TIME_TABLE
        USERINFOLISTCLASS
        WILDCARDSWYXGROUP
        WRAP_IN_DOUBLE_QUOTES
        WRAP_IN_SINGLE_QUOTES
    End Enum

    Public name As String = ""
    Public id As IDType
    Public needs As New List(Of IDType)
    Public filename As String = ""
    Public notes As String = ""

    Public Sub New(Optional ByVal p_noBase As Boolean = False)
        id = IDType.NULL

        If Not p_noBase Then needs.Add(ComponentClass.IDType.BASE)
    End Sub
End Class
