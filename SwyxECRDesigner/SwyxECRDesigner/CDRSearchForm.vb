Public Class CDRSearchForm
    Private Sub searchButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles searchButton.Click
        Dim myTable As New DataTable
        Dim mySql As New SQLStatementClass

        DataGridView1.Rows.Clear()
        mySql.AddSelectString("TOP 100 *", "")

        If callIdTextBox.Text <> "" Then mySql.AddCondition("CallId LIKE " & WrapInSingleQuotes(callIdTextBox.Text))
        If origNumberTextBox.Text <> "" Then mySql.AddCondition("OriginationNumber LIKE " & WrapInSingleQuotes(origNumberTextBox.Text))
        If origNameTextBox.Text <> "" Then mySql.AddCondition("OriginationName LIKE " & WrapInSingleQuotes(origNameTextBox.Text))
        If calledNumberTextBox.Text <> "" Then mySql.AddCondition("CalledNumber LIKE " & WrapInSingleQuotes(calledNumberTextBox.Text))
        If calledNameTextBox.Text <> "" Then mySql.AddCondition("CalledName LIKE " & WrapInSingleQuotes(calledNameTextBox.Text))
        If destNumberTextBox.Text <> "" Then mySql.AddCondition("DestinationNumber LIKE " & WrapInSingleQuotes(destNumberTextBox.Text))
        If destNameTextBox.Text <> "" Then mySql.AddCondition("DestinationName LIKE " & WrapInSingleQuotes(destNameTextBox.Text))

        If disconnectReasonComboBox.Text <> "" Then mySql.AddCondition("DisconnectReason LIKE " & WrapInSingleQuotes(disconnectReasonComboBox.Text))

        If customConditionsTextBox.Text <> "" Then mySql.AddCondition(customConditionsTextBox.Text)

        mySql.SetPrimaryTable("IpPbxCDR")
        mySql.AddOrderByString("StartTime DESC")

        If FillTableFromCommand(CreateConnectionString(Form1.settingsConfigDictionary), mySql.GetSQLStatement, myTable) Then
            For i = 0 To myTable.Rows.Count - 1
                With myTable.Rows(i)
                    Dim myFields As New List(Of String)

                    For j = 0 To myTable.Columns.Count - 1
                        If .Item(j) Is DBNull.Value Then
                            myFields.Add("NULL")
                        Else
                            myFields.Add(.Item(j))
                        End If
                    Next

                    With DataGridView1
                        Dim myIndex As Integer = .Rows.Add

                        For j = 0 To .ColumnCount - 1
                            With .Rows(myIndex).Cells(j)
                                .Value = myFields(j)
                            End With
                        Next

                        .Rows(myIndex).HeaderCell.Value = (i + 1).ToString
                    End With
                End With
            Next
        End If
    End Sub

    Private Sub clearButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles clearButton.Click
        customConditionsTextBox.Clear()
    End Sub
End Class