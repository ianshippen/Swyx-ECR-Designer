Public Class LocationBaseClass
    Protected x, y As Integer

    Public Sub New()
    End Sub

    Public Sub New(ByVal p_x As Integer, ByVal p_y As Integer)
        x = p_x
        y = p_y
    End Sub

    Public Sub [Set](ByVal p_x As Integer, ByVal p_y As Integer)
        x = p_x
        y = p_y
    End Sub

    Public Sub [Set](ByRef p As LocationBaseClass)
        x = p.x
        y = p.y
    End Sub

    Public Function GetX() As Integer
        Return x
    End Function

    Public Function GetY() As Integer
        Return y
    End Function

    Public Sub SetX(ByRef p As LocationBaseClass)
        x = p.x
    End Sub

    Public Sub SetX(ByVal p As Integer)
        x = p
    End Sub

    Public Sub SetY(ByVal p As Integer)
        y = p
    End Sub

    Public Sub CopyFrom(ByRef p As LocationBaseClass)
        x = p.x
        y = p.y
    End Sub

    Public Shared Operator +(ByVal z1 As LocationBaseClass, ByVal z2 As LocationBaseClass) As LocationBaseClass
        Return New LocationBaseClass(z1.x + z2.x, z1.y + z2.y)
    End Operator

    Public Shared Operator +(ByVal z1 As LocationBaseClass, ByVal z2 As DeltaLocationClass) As LocationBaseClass
        Return New LocationBaseClass(z1.x + z2.GetX, z1.y + z2.GetY)
    End Operator

    Public Sub Add(ByVal p_x As Integer, ByVal p_y As Integer)
        x += p_x
        y += p_y
    End Sub

    Public Sub Add(ByRef p As DeltaLocationClass)
        x += p.getx
        y += p.gety
    End Sub
End Class
