Imports System.Data.OleDb

Module DoesDatabaseTableExist
    Public Function DoesDatabaseTableExist(ByRef p_tableName) As Boolean
        Dim myConnectionString As String = CreateConnectionString(Form1.settingsConfigDictionary)
        Dim dbConnection As New OleDbConnection(myConnectionString)
        Dim objCommand As New OleDbCommand
        Dim errorString As String = ""
        Dim noTable As Boolean = False
        Dim result As Boolean = False

        ' Try to connect to the specified database
        Try
            dbConnection.Open()
        Catch ex As OleDbException
            errorString = "Cannot connect to database with connection string: " & myConnectionString
            errorString &= vbCrLf & ex.Message
        End Try

        If errorString = "" Then
            ' We have connected to the database
            Try
                objCommand.Connection = dbConnection
            Catch ex As Exception
                errorString = "Error in creating OleDbDataAdapter"
                errorString &= vbCrLf & ex.Message
            End Try

            If errorString = "" Then
                Dim myObject As Object = Nothing

                objCommand.CommandText = "select count (*) from " & p_tableName

                Try
                    myObject = objCommand.ExecuteScalar()
                Catch ex As Exception
                    errorString = "Command " & objCommand.CommandText & " failed"
                    errorString &= vbCrLf & ex.Message
                    noTable = True
                End Try

                If errorString = "" Then
                    If myObject IsNot DBNull.Value Then
                        Dim x As String = " counted"
                        Dim myResult As Integer = 0

                        result = True
                        myResult = myObject

                        If Not myResult = 1 Then x = "s" & x

                        'MsgBox("Database connection and table OK - " & myResult & " record" & x)
                    Else
                        errorString = "Returned object is NULL instead of an INTEGER"
                    End If
                End If
            Else
            End If
        Else
        End If

        Return result
    End Function
End Module
