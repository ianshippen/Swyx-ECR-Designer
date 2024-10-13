Public Class OpeningTimesClass
    Public openingMinute As New OpeningTimeClass
    Public closingMinute As New OpeningTimeClass

    Public Sub New()
        openingMinute.Set(0, 0)
        closingMinute.Set(24, 0)
    End Sub

    Public Overrides Function ToString() As String
        Return openingMinute.ToString & " - " & closingMinute.ToString
    End Function

    Public Sub [Set](ByRef p_text As String)
        ' Remove any spaces
        Dim myText As String = p_text.Replace(" ", "")

        If myText.Length = 11 Then
            openingMinute.Set(myText.Substring(0, 5))
            closingMinute.Set(myText.Substring(6, 5))
        End If
    End Sub
End Class
