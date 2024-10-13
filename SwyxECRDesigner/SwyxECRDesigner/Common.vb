Module Common
    Public Function GenerateDerivedClass(ByRef p_nodeType As String, ByRef p_masterBitmap As Bitmap) As SIBBClass
        Dim myRef As SIBBClass = Nothing

        Select Case p_nodeType
            Case "SIBB_TODDOW"
                myRef = New TODDOWSIBBClass(p_masterBitmap)

            Case "SIBB_PlayAnnouncement"
                myRef = New PASIBBClass(p_masterBitmap)

            Case "SIBB_Connect"
                myRef = New ConnectSIBBClass(p_masterBitmap)

            Case "SIBB_GetDTMFString"
                myRef = New GetDTMFStringClass(p_masterBitmap)

            Case "SIBB_Voicemail"
                myRef = New VoicemailSIBBClass(p_masterBitmap)

            Case "SIBB_LongestWaiting"
                myRef = New LongestWaitingSIBBClass(p_masterBitmap)

            Case "SIBB_GroupAvailable"
                myRef = New GroupAvailableSIBBClass(p_masterBitmap)

            Case "SIBB_Hold"
                myRef = New HoldSIBBClass(p_masterBitmap)

            Case "SIBB_Pause"
                myRef = New PauseSIBBClass(p_masterBitmap)

            Case "SIBB_Sleep"
                myRef = New SleepSIBBClass(p_masterBitmap)

            Case "SIBB_EndCall"
                myRef = New EndCallClass(p_masterBitmap)

            Case "SIBB_DayOfWeek"
                myRef = New DayOfWeekClass(p_masterBitmap)

            Case "SIBB_TimeOfDay"
                myRef = New TimeOfDayClass(p_masterBitmap)

            Case "SIBB_Start"
                myRef = New StartClass(p_masterBitmap)

            Case "SIBB_Done"
                myRef = New DoneClass(p_masterBitmap)

            Case "SIBB_Skip"
                myRef = New SkipClass(p_masterBitmap)

            Case "SIBB_OnDisconnect"
                myRef = New OnDisconnectClass(p_masterBitmap)

            Case "SIBB_VBScript"
                myRef = New VbscriptClass(p_masterBitmap)

            Case "SIBB_GetDTMFDigit"
                myRef = New GetDTMFDigitClass(p_masterBitmap)

            Case "SIBB_Holiday"
                myRef = New HolidayClass(p_masterBitmap)

            Case "SIBB_AddCallToQueue"
                myRef = New AddCallToQueueSIBBClass(p_masterBitmap)

            Case "SIBB_TopOfQueue"
                myRef = New TopOfQueueSIBBClass(p_masterBitmap)

            Case "SIBB_ChangeQueueState"
                myRef = New ChangeQueueStateSIBBClass(p_masterBitmap)

            Case "SIBB_RemoveCallFromQueue"
                myRef = New RemoveCallFromQueueSIBBClass(p_masterBitmap)

            Case "SIBB_GetPositionInQueue"
                myRef = New GetPositionInQueueSIBBClass(p_masterBitmap)

            Case "SIBB_QueuePause"
                myRef = New QueuePauseSIBBClass(p_masterBitmap)

            Case "SIBB_GetCurrentTime"
                myRef = New GetCurrentTimeSIBBClass(p_masterBitmap)

            Case "SIBB_HasTimeElapsed"
                myRef = New HasTimeElapsedSIBBClass(p_masterBitmap)

            Case "SIBB_GetQueueLength"
                myRef = New GetQueueLength(p_masterBitmap)

            Case "SIBB_FirstTime"
                myRef = New FirstTimeSIBBClass(p_masterBitmap)

            Case "SIBB_LogPoint"
                myRef = New LogPointSIBBClass(p_masterBitmap)

            Case "SIBB_LogAgentStatus"
                myRef = New LogAgentStatusSIBBClass(p_masterBitmap)

            Case "SIBB_ActivateCall"
                myRef = New ActivateCallSIBBClass(p_masterBitmap)

            Case "SIBB_SendEmail"
                myRef = New SendEmailSIBBClass(p_masterBitmap)

            Case Else
                MsgBox("Unknown SIBB type: " & p_nodeType)
        End Select

        Return myRef
    End Function

    Public Function GreatestOf(ByVal ParamArray p() As Integer) As Integer
        Dim result As Integer = p(0)

        For i = 1 To p.Length - 1
            If p(i) > result Then result = p(i)
        Next

        Return result
    End Function

    Public Function LowestOf(ByVal ParamArray p() As Integer) As Integer
        Dim result As Integer = p(0)

        For i = 1 To p.Length - 1
            If p(i) < result Then result = p(i)
        Next

        Return result
    End Function


End Module
