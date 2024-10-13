Public Class OpeningTimeClass
    Private minuteOfDay As Integer

    Public Sub New()
        minuteOfDay = 0
    End Sub

    Public Sub [Set](ByVal p_minuteOfDay As Integer)
        minuteOfDay = p_minuteOfDay
    End Sub

    Public Sub [Set](ByVal p_hour As Integer, ByVal p_minute As Integer)
        minuteOfDay = (60 * p_hour) + p_minute
    End Sub

    Public Sub [Set](ByRef p_text As String)
        If p_text.Length = 5 Then
            [Set](CInt(p_text.Substring(0, 2)), CInt(p_text.Substring(3, 2)))
        End If
    End Sub

    Public Overrides Function ToString() As String
        Dim result As String = ""
        Dim myHours As Integer = minuteOfDay \ 60
        Dim myMinutes As Integer = minuteOfDay Mod 60

        If myHours < 10 Then result &= "0"

        result &= myHours & ":"

        If myMinutes < 10 Then result &= "0"

        result &= myMinutes

        Return result
    End Function
End Class
