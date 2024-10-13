Public Class DeltaLocationClass
    Private x, y As Integer

    Public Sub New(ByVal p_oldX As Integer, ByVal p_newX As Integer, ByVal p_oldY As Integer, ByVal p_newY As Integer)
        x = p_newX - p_oldX
        y = p_newY - p_oldY
    End Sub

    Public Sub New(ByRef p_startPos As ScreenLocationClass, ByRef p_currentPos As ScreenLocationClass)
        x = p_currentPos.GetX - p_startPos.GetX
        y = p_currentPos.GetY - p_startPos.GetY
    End Sub

    Public Sub New(ByRef p_startPos As AbsoluteLocationClass, ByRef p_currentPos As AbsoluteLocationClass)
        x = p_currentPos.GetX - p_startPos.GetX
        y = p_currentPos.GetY - p_startPos.GetY
    End Sub

    Public Function GetX() As Integer
        Return x
    End Function

    Public Function GetY() As Integer
        Return y
    End Function

    Public Function Length() As Integer
        Return Math.Sqrt((x * x) + (y * y))
    End Function
End Class
