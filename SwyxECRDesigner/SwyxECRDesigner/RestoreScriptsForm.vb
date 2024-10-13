Public Class RestoreScriptsForm
    Private Sub clearSelectionsButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles clearSelectionsButton.Click
        functionNameListBox.SelectedItem = Nothing
        sibbNeedsHistoryListBox.SelectedItem = Nothing
    End Sub

    Private Sub functionNameListBox_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles functionNameListBox.SelectedIndexChanged
        UpdateSIBBNeedsList()
    End Sub

    Public Sub UpdateSIBBNeedsList()
        Dim mySql As New SQLStatementClass
        Dim myTable As New DataTable

        mySql.SetPrimaryTable("SIBBNeedsTable_history")
        mySql.AddSelectString("distinct timestamp", "")
        mySql.AddOrderByString("timestamp desc")

        If functionNameListBox.SelectedItem IsNot Nothing Then
            If CheckBox1.Checked Then
                Dim myTimeStampString As String = functionNameListBox.SelectedItem
                myTimeStampString = myTimeStampString.Substring(0, myTimeStampString.IndexOf(" "))

                mySql.AddCondition("timestamp >= " & WrapInSingleQuotes(myTimeStampString))
            End If
        End If

        sibbNeedsHistoryListBox.Items.Clear()

        If FillTableFromCommand(CreateConnectionString(Form1.settingsConfigDictionary), mySql.GetSQLStatement, myTable) Then
            If myTable.Columns.Count = 1 Then
                For i = 0 To myTable.Rows.Count - 1
                    With myTable.Rows(i)
                        If .Item("timestamp") IsNot DBNull.Value Then
                            Dim myDateTime As DateTime = .Item("timestamp")
                            Dim myTimeStamp As String = myDateTime.ToString("yyyy-MM-dd HH:mm:ss.fff")

                            sibbNeedsHistoryListBox.Items.Add(myTimeStamp)
                        End If
                    End With
                Next
            End If
        End If
    End Sub

    Private Sub CheckBox1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox1.CheckedChanged
        UpdateSIBBNeedsList()
    End Sub

    Private Sub okButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles okButton.Click
        Dim doIt As Boolean = False

        If functionNameListBox.SelectedItem Is Nothing Then
            If sibbNeedsHistoryListBox.SelectedItem Is Nothing Then
                MsgBox("Nothing selected - no action taken", MsgBoxStyle.Information)
            Else
                Select Case MsgBox("No SIBB Needs rollback selected" & vbCrLf & "Do you want to proceed with just the function rollback ?", MsgBoxStyle.YesNoCancel)
                    Case MsgBoxResult.Yes
                        doIt = True
                End Select
            End If
        Else
            If sibbNeedsHistoryListBox.SelectedItem Is Nothing Then
                Select Case MsgBox("No function rollback selected" & vbCrLf & "Do you want to proceed with just the SIBB Needs rollback ?", MsgBoxStyle.YesNoCancel)
                    Case MsgBoxResult.Yes
                        doIt = True
                End Select
            Else
                doIt = True
            End If
        End If

        If doIt Then
            If functionNameListBox.SelectedItem IsNot Nothing Then
                Dim myFunctionName As String = functionNameListBox.SelectedItem
                Dim myTimestamp As String = myFunctionName.Substring(0, myFunctionName.LastIndexOf(" ")).TrimEnd
                myFunctionName = myFunctionName.Substring(myFunctionName.LastIndexOf(" ") + 1)

                ' Backup current version from live database
                If DesignerForm.BackupFunction(myFunctionName) Then

                    ' Remove current function from live database
                    If DesignerForm.DeleteFunctionFromLiveDB(myFunctionName) Then

                        ' Restore this version into live database
                        If DesignerForm.RestoreFunction(myFunctionName, myTimestamp) Then
                            MsgBox("Function " & WrapInQuotes(myFunctionName) & " has been rolled back to " & WrapInQuotes(myTimestamp))
                        Else
                            MsgBox("Error in rolling back function - this is now missing from the live database" & WrapInQuotes(myFunctionName), MsgBoxStyle.Critical)
                        End If
                    Else
                        MsgBox("Error in deleting current live version of function " & WrapInQuotes(myFunctionName) & " for rollback", MsgBoxStyle.Critical)
                    End If
                Else
                    MsgBox("Error in backing up current live version of function " & WrapInQuotes(myFunctionName) & " for rollback", MsgBoxStyle.Critical)
                End If
            End If

            If sibbNeedsHistoryListBox.SelectedItem IsNot Nothing Then
                Dim myTimestamp As String = sibbNeedsHistoryListBox.SelectedItem

                ' Backup current SIBBNeedsTable from live database
                If DesignerForm.BackupSIBBNeeds() Then
                    ' Clear current SIBBNeedsTable in live database
                    If DesignerForm.ClearSIBBNeeds() Then
                        ' Restore from history
                        If DesignerForm.RestoreSIBBNeeds(myTimestamp) Then
                            MsgBox("SIBB Needs table has been rolled back to " & WrapInQuotes(myTimestamp))
                        Else
                            MsgBox("Error in rolling back SIBB Needs table - this is now missing from the live database", MsgBoxStyle.Critical)
                        End If
                    Else
                        MsgBox("Error in clearing current live SIBB Needs table for rollback", MsgBoxStyle.Critical)
                    End If
                Else
                    MsgBox("Error in backing up current live SIBB Needs table for rollback", MsgBoxStyle.Critical)
                End If
            End If
        End If
    End Sub
End Class