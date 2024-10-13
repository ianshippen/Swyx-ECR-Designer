Public Class HolidayClass
    Inherits SIBBClass

    Public Sub New(ByRef p_bitmap As Bitmap)
        MyBase.New(SIBBTypeClass.SIBBTYPE.HOLIDAY, p_bitmap)
    End Sub

    Public Overrides Sub PackData()
    End Sub

    Public Overrides Sub UnpackData()
    End Sub

    Public Overrides Sub SetupForm(ByRef p_tabPage As System.Windows.Forms.TabPage)
        MyBase.SetupForm(p_tabPage)
    End Sub

    Public Overrides Sub TakedownForm(ByRef p_tabPage As System.Windows.Forms.TabPage)
        MyBase.TakedownForm(p_tabPage)
    End Sub

    Public Overrides Function Run(ByRef p_data As String) As Integer
        Dim result As Integer = -1

        HolidayForm.DateTimePicker1.Value = Now

        If HolidayForm.ShowDialog = DialogResult.OK Then
            result = HolidayForm.result

            If result = 3 Then
                ' Use Database
                Dim myTable As New DataTable
                Dim mySql As New SQLStatementClass

                mySql.SetPrimaryTable("BankHolidayTable")
                mySql.AddSelectString("IsNull(count(*), 0)", "value")
                'mySql.AddCondition("date = convert(varchar(10), getdate(), 103)")
                mySql.AddCondition("date = " & WrapInSingleQuotes(HolidayForm.DateTimePicker1.Value.ToString.Substring(0, 10)))

                result = -1

                If FillTableFromCommand(CreateConnectionString(Form1.settingsConfigDictionary), mySql.GetSQLStatement, myTable) Then
                    If myTable.Rows.Count > 0 Then
                        If Not myTable.Rows(0).Item(0) Is DBNull.Value Then
                            result = CInt(myTable.Rows(0).Item(0))

                            If result > 1 Then result = 1
                        End If
                    End If
                End If
            End If
        End If

        Return result
    End Function
End Class
