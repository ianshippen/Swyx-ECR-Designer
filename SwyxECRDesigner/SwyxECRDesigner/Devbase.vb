Module Devbase
    Const DATA_SOURCE As String = ".\SQLEXPRESS"
    Const STANDBY_DATA_SOURCE As String = ""
    Const STANDBY_MACHINE_NAME As String = ""
    Const DATABASE As String = "OpenQueue"
    Const USERID As String = "reachall"
    Const PASSWORD As String = "reachall"

    Public debugFlag As Boolean = False
    Public myCodeArray(1) As String
    Public myCodeFileName As String = ""

    Public Function DBReturnResults(ByRef p_statement, ByRef p_rs, ByRef p_db)
        Dim rc As Boolean = False
        Dim foundError As Boolean = False

        On Error Resume Next

        If p_db Is Nothing Then
            p_db = CreateObject("ADODB.Connection")

            If Err.Number <> 0 Then
                LogError("Error in DBReturnResults::CreateObject(" & WrapInQuotes("ADODB.Connection") & ")")
                LogError("Error code = " & CStr(Hex(Err.Number)))
                LogError("Error description = " & Err.Description)
                foundError = True
            End If

            If foundError = False Then
                p_db.Open(GenerateDatabaseConnectionString())

                If Err.Number <> 0 Then
                    LogError("Error in DBReturnResults::Open(" & WrapInQuotes(GenerateDatabaseConnectionString()) & ")")
                    LogError("Error code = " & CStr(Hex(Err.Number)))
                    LogError("Error description = " & Err.Description)
                    foundError = True
                End If
            End If
        End If

        If foundError = False Then
            p_rs = p_db.Execute(p_statement)

            If Err.Number = 0 Then
                rc = True
            Else
                LogError("Error in DBReturnResults::db.Execute(" & WrapInQuotes(p_statement) & ")")
                LogError("Error code = " & CStr(Hex(Err.Number)))
                LogError("Error description = " & Err.Description)
                foundError = True
            End If
        End If

        DBReturnResults = rc
    End Function

    Function GenerateDatabaseConnectionString()
        GenerateDatabaseConnectionString = "Provider=SQLOLEDB;Data Source=" & GetDataSource() & ";Initial Catalog=" & DATABASE & ";User Id=" & USERID & ";Password=" & PASSWORD
    End Function

    Function GetDataSource()
        Dim x : x = DATA_SOURCE

        If STANDBY_MACHINE_NAME <> "" Then
            Dim y : y = CreateObject("WScript.Network")

            If LCase(STANDBY_MACHINE_NAME) = LCase(y.ComputerName) Then x = STANDBY_DATA_SOURCE

            y = Nothing
        End If

        GetDataSource = x
    End Function

    Class SIBBListClass
        Private myCollection
        Private myDebugFlag

        Public Sub New()
            myCollection = CreateObject("System.Collections.ArrayList")
        End Sub

        Protected Overrides Sub Finalize()
            myCollection = Nothing
        End Sub

        Public Sub Add(ByVal p_nodeNumber, ByVal p_nodeType, ByVal p_data, ByVal p_outputs, ByVal p_title, ByVal p_internalReference)
            Dim x As SIBBClass

            If myDebugFlag Then LogError("SIBBListClass::Add(" & p_nodeNumber & ", " & WrapInQuotes(p_nodeType) & ", " & WrapInQuotes(p_data) & ", " & WrapInQuotes(p_outputs) & ", " & WrapInQuotes(p_title) & ", " & WrapInQuotes(p_internalReference))

            x = New SIBBClass
            x.nodeNumber = p_nodeNumber
            x.nodeType = p_nodeType
            x.data = p_data
            x.outputs = p_outputs
            x.title = p_title
            x.internalReference = p_internalReference
            myCollection.Add(x)
        End Sub

        Public Sub GetEntry(ByVal p_index, ByRef p_nodeNumber, ByRef p_nodeType, ByRef p_data, ByRef p_outputs, ByRef p_title, ByRef p_internalReference)
            p_nodeNumber = myCollection.Item(p_index).nodeNumber
            p_nodeType = myCollection.Item(p_index).nodeType
            p_data = myCollection.Item(p_index).data
            p_outputs = myCollection.Item(p_index).outputs
            p_title = myCollection.Item(p_index).title
            p_internalReference = myCollection.Item(p_index).internalReference
        End Sub

        Public Function GetCount()
            GetCount = myCollection.Count()
        End Function

        Public Sub Debug(ByVal p_debugFlag)
            myDebugFlag = p_debugFlag
        End Sub
    End Class

    Class SIBBClass
        Public nodeNumber As Integer
        Public nodeType As String
        Public data As String
        Public outputs As String
        Public title As String
        Public internalReference As String

        Public Sub New()
            nodeNumber = -1
            nodeType = ""
            data = ""
            outputs = ""
            title = ""
            internalReference = ""
        End Sub
    End Class

    Public Function Left(ByVal p_string As String, ByVal p_length As Integer) As String
        Return p_string.Substring(0, p_length)
    End Function

    Public Function Right(ByVal p_string As String, ByVal p_length As Integer) As String
        Return p_string.Substring(p_string.Length - p_length)
    End Function

    Sub DBExecute(ByRef p_statement)
        Dim db

        On Error Resume Next
        db = CreateObject("ADODB.Connection")

        If Err.Number = 0 Then
            db.Open(GenerateDatabaseConnectionString())

            If Err.Number = 0 Then
                db.Execute(p_statement)

                If Err.Number <> 0 Then
                    LogError("DBExecute(" & p_statement & ") could not execute statement")
                    LogError("DBExecute: " & Err.Source)
                    LogError("DBExecute: " & Err.Description)
                End If

                db.Close()
            Else
                LogError("DBExecute(" & p_statement & ") could not open database with connection string = " & WrapInQuotes(GenerateDatabaseConnectionString()))
                LogError("DBExecute: " & Err.Source)
                LogError("DBExecute: " & Err.Description)
            End If

            db = Nothing
        Else
            LogError("DBExecute(" & p_statement & ") could not create ADODB.Connection")
            LogError("DBExecute: " & Err.Source)
            LogError("DBExecute: " & Err.Description)
        End If
    End Sub

    Function IsNull(ByVal x) As Boolean
        Return False
    End Function

    Function ConvertDateToISO(ByVal p_date)
        Dim result, temp

        result = Year(p_date) & "-"
        temp = Month(p_date)

        If temp < 10 Then result = result & "0"

        result = result & temp & "-"
        'temp = Day(p_date)
        temp = 1

        If temp < 10 Then result = result & "0"

        result = result & temp & " "

        temp = Hour(p_date)

        If temp < 10 Then result = result & "0"

        result = result & temp & ":"
        temp = Minute(p_date)

        If temp < 10 Then result = result & "0"

        result = result & temp & ":"
        temp = Second(p_date)

        If temp < 10 Then result = result & "0"

        result = result & temp

        ConvertDateToISO = result
    End Function

    Sub RunScript(ByRef p_scriptName As String, ByRef p_immediateCommand As String, ByRef p_pullInSIBBsFor As String, ByRef p_startRuleFileName As String, ByRef p_insertScriptCodeFileName As String)
        ' p_scriptName is usualy "Bootstrap"
        If debugFlag Then LogError("Scripts.txt::ServiceBuilder::RunScript(" & WrapInQuotes(p_scriptName) & ", " & WrapInQuotes(p_immediateCommand) & ")")

        Dim myList, myUsesArrayList
        Dim myTable As New DataTable
        Dim count As Integer = 0
        Dim baseUsesArrayList = CreateObject("System.Collections.ArrayList")
        Dim myCode As String = ""

        myCodeFileName = ""
        myCode = "' Pulling in static code for Start Block" & vbCrLf

        ' Get the baseUsesArray.Add lines from DevBase 3.0
        ' Dim myBaseUsesLines As List(Of String) = PullInFileToList("DevBase 3.0.txt")
        Dim myBaseUsesLines As List(Of String) = PullInFileToList(p_startRuleFileName)

        For Each myLine As String In myBaseUsesLines
            If myLine.StartsWith("baseUsesArrayList.Add ") Then
                Dim myItem As String = myLine.Substring(myLine.IndexOf(" ") + 1).Trim(Chr(34))

                baseUsesArrayList.add(myItem)
            End If
        Next

        ' Add p_scriptName to stop it getting pulled in again
        baseUsesArrayList.add(p_scriptName)

        ' Pull in any static code for a Start Block
        Dim mySql As String = "SELECT data FROM ServiceBuilderTable WHERE scriptName = " & WrapInSingleQuotes(p_pullInSIBBsFor) & " AND nodeType = 'SIBB_Start'"

        If FillTableFromCommand(GenerateDatabaseConnectionString, mySql, myTable) Then
            If myTable.Rows.Count > 0 Then
                With myTable.Rows(0)
                    If Not .Item("data") Is DBNull.Value Then myCode = .Item("data")
                End With
            End If
        End If

        If Len(myCode) > 0 Then myCode = myCode & vbCrLf

        myCode = myCode & p_immediateCommand

        ' This is usually for p_scriptName = "Bootstrap"
        myCode &= vbCrLf & "' Pulling in code from Scripts table for " & WrapInQuotes(p_scriptName) & vbCrLf

        myTable = New DataTable
        mySql = "SELECT code FROM scripts WHERE FunctionName = " & WrapInSingleQuotes(p_scriptName) & " ORDER BY lineNumber"
        myUsesArrayList = CreateObject("System.Collections.ArrayList")

        If FillTableFromCommand(GenerateDatabaseConnectionString, mySql, myTable) Then
            For i = 0 To myTable.Rows.Count - 1
                With myTable.Rows(i)
                    If Not .Item("Code") Is DBNull.Value Then
                        If Len(myCode) > 0 Then myCode = myCode & vbCrLf

                        myCode = myCode & .Item("Code")
                        count = count + 1
                    End If
                End With
            Next

            If debugFlag Then LogError("Scripts.txt::ServiceBuilder::RunScript() found " & count & " lines of code for " & WrapInQuotes(p_scriptName))

            myTable = New DataTable

            If p_pullInSIBBsFor <> "" Then
                mySql = "SELECT DISTINCT nodeType FROM ServiceBuilderTable WHERE scriptName = " & WrapInSingleQuotes(p_pullInSIBBsFor)
                myList = CreateObject("System.Collections.ArrayList")

                If FillTableFromCommand(GenerateDatabaseConnectionString, mySql, myTable) Then
                    For i = 0 To myTable.Rows.Count - 1
                        With myTable.Rows(i)
                            If Not .Item("nodeType") Is DBNull.Value Then
                                Dim myNodeName As String = .Item("nodeType")

                                myList.Add(myNodeName)
                            End If
                        End With
                    Next

                    If debugFlag Then
                        LogError("Scripts.txt::ServiceBuilder::RunScript() requires " & myList.Count() & " SIBBs for " & WrapInQuotes(p_pullInSIBBsFor))

                        For i = 0 To myList.Count() - 1
                            LogError("Scripts.txt::ServiceBuilder::RunScript() requires SIBB: " & WrapInQuotes(myList.Item(i)))
                        Next
                    End If

                    myTable = New DataTable

                    ' Pull in the dependencies for each SIBB function used
                    mySql = ""

                    ' Generate a list of the SIBBs used to match against in the Needs database table
                    For i = 0 To myList.Count() - 1
                        If mySql <> "" Then mySql = mySql & ","

                        mySql = mySql & WrapInSingleQuotes(myList.Item(i))
                    Next

                    If mySql <> "" Then
                        mySql = "SELECT DISTINCT Needs FROM SIBBNeedsTable WHERE SIBB IN (" & mySql & ")"

                        If FillTableFromCommand(GenerateDatabaseConnectionString, mySql, myTable) Then
                            For i = 0 To myTable.Rows.Count - 1
                                With myTable.Rows(i)
                                    If Not .Item("Needs") Is DBNull.Value Then
                                        Dim myObject As String = .Item("Needs")

                                        If myObject <> "" Then
                                            ' Add to myUsesArrayList if not already added
                                            If Not myUsesArrayList.Contains(myObject) Then myUsesArrayList.Add(myObject)
                                        End If
                                    End If
                                End With
                            Next
                        End If
                    End If

                    ' Loop through each of our needs
                    For i = 0 To myUsesArrayList.Count() - 1
                        If debugFlag Then LogError("Scripts.txt::ServiceBuilder::RunScript() SIBBs need " & WrapInQuotes(myUsesArrayList.Item(i)))

                        ' Check if it has already been pulled in via the base ..
                        If baseUsesArrayList.Contains(myUsesArrayList.Item(i)) Then
                            If debugFlag Then LogError("Scripts.txt::ServiceBuilder::RunScript() already have " & WrapInQuotes(myUsesArrayList.Item(i)) & " via Base")
                        Else
                            ' If it has not already been pulled in via the base then add to myList
                            myList.Add(myUsesArrayList.Item(i))
                        End If
                    Next

                    myTable = New DataTable

                    ' Pull in the source code for each SIBB function and needs used - why is it pulling in "Bootstrap" when we have it already ?
                    For i = 0 To myList.Count() - 1
                        If debugFlag Then LogError("Scripts.txt::ServiceBuilder::RunScript() looking for code for " & WrapInQuotes(myList.Item(i)))

                        myCode &= vbCrLf & vbCrLf & "' Pulling in source code for " & WrapInQuotes(myList.item(i)) & vbCrLf

                        mySql = "SELECT code FROM scripts WHERE FunctionName = " & WrapInSingleQuotes(myList.Item(i)) & " ORDER BY lineNumber"
                        count = 0

                        If FillTableFromCommand(GenerateDatabaseConnectionString, mySql, myTable) Then
                            For j = 0 To myTable.Rows.Count - 1
                                With myTable.Rows(j)
                                    If Not .Item("Code") Is DBNull.Value Then
                                        If Len(myCode) > 0 Then myCode = myCode & vbCrLf

                                        myCode = myCode & .Item("Code")
                                        count = count + 1
                                    End If
                                End With
                            Next

                            myTable = New DataTable

                            If count = 0 Then LogError("Scripts.txt::ServiceBuilder::Runscript() Error: Could not find code in Scripts table for " & WrapInQuotes(myList.Item(i)))

                            If debugFlag Then LogError("Scripts.txt::ServiceBuilder::Runscript() found " & count & " lines of code for " & WrapInQuotes(myList.Item(i)))
                        Else
                            LogError("Scripts.txt::ServiceBuilder::Runscript() Error: Looking for code for " & WrapInQuotes(myList.Item(i)))
                        End If
                    Next
                End If

                myList = Nothing
            End If

            If debugFlag Then LogError("Scripts.txt::ServiceBuilder::RunScript() about to execute")

            ' Prepend with the contents of "InsertScriptCode.txt" if specified - This is what is in the VBScript block in the Swyx ECR that calls the ServiceBuilder script
            If p_insertScriptCodeFileName <> "" Then myCode = PullInFile(p_insertScriptCodeFileName) & vbCrLf & myCode

            ' Prepend with the contents of "Devbase 3.0.txt" - this is what goes in the Swyx Start rule Block
            ' myCode = PullInFile("DevBase 3.0.txt") & vbCrLf & myCode
            myCode = PullInFile(p_startRuleFileName) & vbCrLf & myCode

            ' Prepend with the contents of "PBXScriptDummyClass.txt")
            myCode = PullInFile("PBXScriptDummyClass.txt") & vbCrLf & myCode

            ' Prepend with the contents of "SwyxExtras.txt")
            myCode = PullInFile("SwyxExtras.txt") & vbCrLf & myCode

            myCode &= vbCrLf & PullInFile("ExitCode.txt")

            myCodeArray(0) = myCode

            Dim askUser As Boolean = False
            Dim runIt As Boolean = True

            If MsgBox("Do you want to save the generated code to file ?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                Dim myDialog As New SaveFileDialog

                If myDialog.ShowDialog = DialogResult.OK Then
                    myCodeFileName = myDialog.FileName
                    SaveMyCode(0, myCodeFileName)
                End If

                askUser = True
            End If

            If askUser Then
                runIt = False

                If MsgBox("Do you want to run the simulation ?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then runIt = True
            End If

            If runIt Then
                myCodeArray(1) = myCode
            End If
        Else
            LogError("Scripts.txt::ServiceBuilder::RunScript() Error: DBReturnResults() failed")
        End If

        myUsesArrayList = Nothing
        baseUsesArrayList = Nothing

        'MsgBox("Simulation finished ..")
        mySimulatorFormRef.UpdateMessageDisplay("Simulation finished ..")
    End Sub

    Public Function ExecuteGlobal(ByRef p_code As String) As String
        Dim x As New MSScriptControl.ScriptControl
        Dim errorFound As Boolean = False
        Dim errorString As String = ""

        x.Language = "VBScript"

        Try
            x.ExecuteStatement(p_code)
        Catch ex As Exception
            errorFound = True

            errorString = "Error in running simulator: " & ex.Message
        End Try

        Return errorString
    End Function

    Public Function PullInFile(ByRef p_filename As String) As String
        Dim fullFilename As String = "C:\Documents and Settings\Ian\My Documents\Visual Studio 2008\Projects\SwyxECRDesigner\SwyxECRDesigner\bin\Debug\SimulatorFiles\" & p_filename
        Dim myReader As New IO.StreamReader(fullFilename)
        Dim reading As Boolean = True
        Dim myData As String = vbCrLf & "' Pulling in file: " & WrapInQuotes(fullFilename) & vbCrLf

        While reading
            Dim myLine As String = myReader.ReadLine

            If myLine Is Nothing Then
                reading = False
            Else
                myData &= myLine & vbCrLf
            End If
        End While

        myReader.Close()

        Return myData
    End Function

    Private Function PullInFileToList(ByRef p_filename As String) As List(Of String)
        Dim fullFilename As String = "C:\Documents and Settings\Ian\My Documents\Visual Studio 2008\Projects\SwyxECRDesigner\SwyxECRDesigner\bin\Debug\SimulatorFiles\" & p_filename
        Dim myReader As New IO.StreamReader(fullFilename)
        Dim reading As Boolean = True
        Dim myData As New List(Of String)

        While reading
            Dim myLine As String = myReader.ReadLine

            If myLine Is Nothing Then
                reading = False
            Else
                myData.Add(myLine)
            End If
        End While

        myReader.Close()

        Return myData
    End Function

    Public Sub UpdateCallingNumber(ByVal p_index As Integer, ByRef p_callingNumber As String)
        UpdateStringField(p_index, p_callingNumber, "IpPbx.myCallingNumber")
    End Sub

    Public Sub UpdateCalledNumber(ByVal p_index As Integer, ByRef p_calledNumber As String)
        UpdateStringField(p_index, p_calledNumber, "CalledNumber")
    End Sub

    Public Sub UpdateOrigIPAddress(ByVal p_index As Integer, ByRef p_ipAddress As System.Net.IPAddress)
        UpdateStringField(p_index, p_ipAddress.ToString, "OrigIPAddress")
    End Sub

    Public Sub UpdateOrigIPPort(ByVal p_index As Integer, ByVal p_port As Integer)
        UpdateStringField(p_index, p_port, "GetOrigPort")
    End Sub

    Private Sub UpdateStringField(ByVal p_index As Integer, ByRef p_fieldValue As String, ByRef p_fieldName As String)
        ' Get index of start of "<field name> = "
        Dim myIndex As Integer = myCodeArray(p_index).IndexOf(p_fieldName & " = ")

        If myIndex >= 0 Then
            ' Set myIndex to character after the opening quotation mark
            myIndex += (p_fieldName & " = ").Length + 1 ' Include opening quotation mark

            ' Set remainderText to be everything after the opening quotation mark
            Dim remainderText As String = myCodeArray(p_index).Substring(myIndex)

            ' Chop anything out that lies between the quotation marks
            remainderText = remainderText.Substring(remainderText.IndexOf(Chr(34)))
            myCodeArray(p_index) = myCodeArray(p_index).Substring(0, myIndex) & p_fieldValue & remainderText
        End If
    End Sub

    Public Sub UpdateCallId(ByVal p_index As Integer, ByVal p_callId As Integer)
        Dim myIndex As Integer = myCodeArray(p_index).IndexOf("CallId = ")

        If myIndex >= 0 Then
            myIndex += "CallId = ".Length + 1 ' Include opening bracket

            Dim remainderText As String = myCodeArray(p_index).Substring(myIndex)

            remainderText = remainderText.Substring(remainderText.IndexOf(")"))
            myCodeArray(p_index) = myCodeArray(p_index).Substring(0, myIndex) & p_callId & remainderText
        End If
    End Sub

    Public Sub SaveMyCode(ByVal p_index As Integer, ByRef p_filename As String)
        Dim myWriter As New IO.StreamWriter(p_filename)

        myWriter.Write(myCodeArray(p_index))
        myWriter.Close()
    End Sub
End Module
