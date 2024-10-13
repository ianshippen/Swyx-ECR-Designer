Public Class ScriptForm
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim x As New SQLStatementClass
        Dim scriptName As String = "Bootstrap"
        Dim myCode As New List(Of String)
        Dim myTable As New DataTable
        Dim pullInSIBBSFor As String = "Test Script"
        Dim myUsesArrayList As New List(Of String)
        Dim baseUsesArrayList As New List(Of String)

        x.SetPrimaryTable("Scripts")
        x.AddSelectString("code", "")
        x.AddCondition("FunctionName = " & WrapInSingleQuotes(scriptName))
        x.AddOrderByString("lineNumber")

        TextBox1.Clear()

        If FillTableFromCommand(CreateConnectionString(Form1.settingsConfigDictionary), x.GetSQLStatement, myTable) Then
            For i = 0 To myTable.Rows.Count - 1
                With myTable.Rows(i)
                    If Not .Item("Code") Is DBNull.Value Then myCode.Add(.Item("Code"))
                End With
            Next

            TextBox1.Text &= "Found " & myCode.Count & " lines of code for " & WrapInQuotes(scriptName) & vbCrLf
        End If

        If pullInSIBBSFor <> "" Then
            Dim myList As New List(Of String)

            x.Clear()
            myTable.Rows.Clear()
            myTable.Columns.Clear()

            x.SetPrimaryTable(DesignerForm.SERVICEBUILDER_TABLE_NAME)
            x.AddSelectString("DISTINCT nodeType", "")
            x.AddCondition("scriptName = " & WrapInSingleQuotes(pullInSIBBSFor))

            If FillTableFromCommand(CreateConnectionString(Form1.settingsConfigDictionary), x.GetSQLStatement, myTable) Then
                Dim mySql As String = ""

                For i = 0 To myTable.Rows.Count - 1
                    With myTable.Rows(i)
                        If Not .Item("nodeType") Is DBNull.Value Then myList.Add(.Item("nodeType"))
                    End With
                Next

                TextBox1.Text &= "Requires " & myList.Count & " SIBBs for " & WrapInQuotes(pullInSIBBSFor) & vbCrLf

                For i = 0 To myList.Count - 1
                    TextBox1.Text &= "Requires SIBB: " & WrapInQuotes(myList(i)) & vbCrLf
                Next

                For i = 0 To myList.Count - 1
                    If mySql <> "" Then mySql &= ","

                    mySql &= WrapInSingleQuotes(myList(i))
                Next

                If mySql <> "" Then
                    mySql = "select distinct Needs from " & DesignerForm.SIBB_NEEDS_TABLE_NAME & " where SIBB in (" & mySql & ")"

                    myTable.Rows.Clear()
                    myTable.Columns.Clear()

                    If FillTableFromCommand(CreateConnectionString(Form1.settingsConfigDictionary), mySql, myTable) Then
                        For i = 0 To myTable.Rows.Count - 1
                            With myTable.Rows(i)
                                If Not .Item("Needs") Is DBNull.Value Then
                                    Dim myObject As String = .Item("Needs")


                                    If myObject <> "" Then
                                        If Not myUsesArrayList.Contains(myObject) Then myUsesArrayList.Add(myObject)
                                    End If
                                End If
                            End With
                        Next
                    End If
                End If

                For i = 0 To myUsesArrayList.Count - 1
                    TextBox1.Text &= "SIBBs need " & WrapInQuotes(myUsesArrayList(i)) & vbCrLf

                    If baseUsesArrayList.Contains(myUsesArrayList(i)) Then
                        TextBox1.Text &= "Already have " & WrapInQuotes(myUsesArrayList(i)) & " via Base" & vbCrLf
                    Else
                        myList.Add(myUsesArrayList(i))
                    End If
                Next

                For i = 0 To myList.Count - 1
                    Dim count As Integer = 0

                    TextBox1.Text &= "Looking for code for " & WrapInQuotes(myList(i)) & vbCrLf

                    x.Clear()

                    x.SetPrimaryTable("Scripts")
                    x.AddSelectString("code", "")
                    x.AddCondition("FunctionName = " & WrapInSingleQuotes(myList(i)))
                    x.AddOrderByString("lineNumber")
                    myTable.Rows.Clear()
                    myTable.Columns.Clear()

                    If FillTableFromCommand(CreateConnectionString(Form1.settingsConfigDictionary), x.GetSQLStatement, myTable) Then
                        For j = 0 To myTable.Rows.Count - 1
                            With myTable.Rows(j)
                                If Not .Item("Code") Is DBNull.Value Then
                                    myCode.Add(.Item("Code"))
                                    count += 1
                                End If
                            End With
                        Next

                        If count = 0 Then TextBox1.Text &= "Error: Could not find code in Scripts table for " & WrapInQuotes(myList(i)) & vbCrLf

                        TextBox1.Text &= "Found " & count & " lines of code for " & WrapInQuotes(myList(i)) & vbCrLf
                    End If
                Next
            End If
        End If

        Dim a As New SaveFileDialog

        If a.ShowDialog = Windows.Forms.DialogResult.OK Then
            Dim b As New IO.StreamWriter(a.FileName)

            For i = 0 To myCode.Count - 1
                b.WriteLine(myCode(i))
            Next

            b.Close()
        End If
    End Sub
End Class