Function LongestWaitingAgentDistributor(ByRef p_target, ByVal p_agentTimeout, ByVal p_groupTimeout)
  Dim i, myUserInfoList, useAlertSound

' *** Point A ***
  useExit = 0

  If p_alertSound <> "" Then useAlertSound = True

  Set myUserInfoList = New UserInfoListClass

' *** Point B ***
  ' Get group members
  GetGroupMembers p_target, myUserInfoList

' *** Point C ***
  ' Check if any member is available
  For i = 0 To myUserInfoList.GetCount() - 1
    Dim myState : myState = myUserInfoList.GetState(i)

    If myState = 2 Then
      useExit = 1 ' Block C exit
      Exit For
    End If
  Next

  If useExit = 1 Then
' *** Point E ***
    Dim gettingAgents

    gettingAgents = True

    While gettingAgents
      ' Generate a list of available agents
      Dim myList

      gettingAgents = False
      useExit = 0

      myList = ""

      myList = LWAD_GenerateList(myUserInfoList)

      If Len(myList) = 0 Then
        ' No agents available
        useExit = 1 ' Block E exit
      Else
        LWADReadTableIntoArray "exec sp_check_agents " & WrapInSingleQuotes(myList), "name", myArray

        If UBound(myArray) = -1 Then
          ' Nothing returned. Database error ?
	  ' Convert the original list into an array
	  Dim tempArray, numberOfAgents

	  tempArray = Array

	  While InStr(myList, ",") > 0
	    ReDim Preserve tempArray(UBound(tempArray) + 1)
	    tempArray(UBound(tempArray)) = Left(myList, InStr(myList, ",") - 1)
	    myList = Right(myList, Len(myList) - InStr(myList, ","))
	  WEnd
			
	  ReDim Preserve tempArray(UBound(tempArray) + 1)
	  tempArray(UBound(tempArray)) = myList

          Randomize
		
          numberOfAgents = UBound(tempArray) + 1

	  For i = 0 To numberOfAgents - 1
	    myIndex = Int(Rnd * (UBound(tempArray) + 1))
	    ReDim Preserve myArray(UBound(myArray) + 1)
	    myArray(UBound(myArray)) = tempArray(myIndex)

	    For j = (myIndex + 1) To UBound(tempArray)
	      tempArray(j - 1) = tempArray(j)
	    Next

	    ReDim Preserve tempArray(UBound(tempArray) - 1) 
	  Next
        End If

        myIndex = -1
      End If
      If useExit = 0 Then
' *** Point F ***
        Dim tryingAgent

        tryingAgent = True
      	
        While tryingAgent
          Dim rcDummy

	  useExit = 0
          tryingAgent = False

          ' Try agent
          If DateDiff("s", startTime, Now) > groupTimeoutAsInt Then
	    UseExit = 2
          Else
	    myIndex = myIndex + 1

	    If myIndex <= UBound(myArray) Then
	      myName = myArray(myIndex)
	      lastTarget = myName
	      ' PBXScript.OutputTrace("Trying agent " & myName)
	    Else
	      UseExit = 1
	    End If
          End If

          Select Case useExit
            Case 0
              ' Proceed. Try to connect to agent
' *** Point G ***
              useExit = gseConnectToEx6(myName, p_agentTimeout, "", False, rcDummy, True, useAlertSound, p_alertSound, False, "", False)

              Select Case useExit
                Case gseStateConnected
	          useExit = 0

	        Case gseStateTimeout
	          useExit = 1

	        Case gseStateNoAnswer
	          useExit = 1

	        Case gseStateNotDelivered
	          useExit = 1

                Case gseStateDisconnected
                  useExit = 2
              End Select

              Select Case useExit
	        Case 0
                 ' Connected, return Connected
                 returnCode = 0

                Case 1
                  ' Try agent again
                  tryingAgent = True
  
                Case 2
                   ' Disconnected
                   returnCode = 9
	      End Select
        
	    Case 1
              ' Loop finished, check for group timeout
' *** Point H ***
              If DateDiff("s", startTime, Now) > groupTimeoutAsInt Then
                ' Return Group Timeout
                returnCode = 1
              Else
	        PBXScript.Sleep 1000
                gettingAgents = True
              End If

            Case 2
' *** Point J ***
              ' Return Group Timeout
	      returnCode = 1

          End Select
        Wend
      Else
' *** Point I ***
        ' Return Busy
        returnCode = 2
      End If 
    Wend ' While gettingAgents
  Else
' *** Point D ***
    ' Return Not Delivered. useExit = 0
      returnCode = 3
  End If

  LongestWaitingAgentDistributor = returnCode
End Function

Function LWAD_GenerateList(ByRef p_userInfoList)
  Dim i, myList

  myList = ""

  For i = 0 To p_userInfoList.GetCount() - 1
    If p_userInfoList.GetState(i) = 2 Then
      If Len(myList) > 0 Then myList = myList & ","

      myList = mylist & SingleQuoteCheck(p_userInfoList.GetName(i))
    End If
  Next

  LWAD_GenerateList = myList
End Function

Sub LWADReadTableIntoArray()
  Dim db, rs, myCount

  On Error Resume Next
  myCount = 0
  Set db = CreateObject("ADODB.Connection")
 
  If Err = 0 Then
     db.Open GenerateDatabaseConnectionString()
    
    If Err = 0 Then
      Set rs = db.Execute(p_statement)

      If Err = 0 Then
        If Not rs.EOF Then
          While Not rs.EOF
            ReDim Preserve p_array(myCount)
            p_array(myCount) = rs(p_fieldName)
            myCount = myCount + 1
            rs.MoveNext
          Wend
        End If
      End If
    End If

    db.Close
  End If

  Set db = Nothing
End Sub