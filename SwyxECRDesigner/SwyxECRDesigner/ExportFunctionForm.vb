Public Class ExportFunctionForm

    Private Sub ListBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ListBox1.SelectedIndexChanged
        TextBox1.Clear()

        With ListBox1
            If .SelectedItem IsNot Nothing Then
                Dim x As String = .SelectedItem

                If x <> "" Then
                    Dim mySql As String = "select Code from Scripts where functionName = " & WrapInSingleQuotes(x) & " order by lineNumber"
                    Dim myTable As New DataTable

                    If FillTableFromCommand(CreateConnectionString(Form1.settingsConfigDictionary), mySql, myTable) Then
                        If myTable.Columns.Count > 0 Then
                            For i = 0 To myTable.Rows.Count - 1
                                With myTable.Rows(i)
                                    If .Item(0) IsNot DBNull.Value Then TextBox1.AppendText(.Item(0) & vbCrLf)
                                End With
                            Next
                        End If
                    End If
                End If
            End If
        End With
    End Sub
End Class