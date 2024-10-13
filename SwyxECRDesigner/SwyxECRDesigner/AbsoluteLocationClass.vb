Public Class AbsoluteLocationClass
    Inherits LocationBaseClass

    Private m_isNull As Boolean
    Private m_isDetour As Boolean
    Private m_nodeDest As Integer

    Public Sub New()
        m_isNull = True
        m_isDetour = False
        m_nodeDest = -1
    End Sub

    Public Sub New(ByVal p_x As Integer, ByVal p_y As Integer)
        MyBase.New(p_x, p_y)
        m_isNull = False
    End Sub

    Public Sub New(ByRef p As AbsoluteLocationClass)
        MyBase.New(p.x, p.y)
        m_isNull = p.m_isNull
        m_isDetour = p.m_isDetour
        m_nodeDest = p.m_nodeDest
    End Sub

    Public Sub New(ByRef p As AbsoluteLocationClass, ByVal p_xDelta As Integer, ByVal p_yDelta As Integer)
        MyBase.New(p.x + p_xDelta, p.y + p_yDelta)
        m_isNull = p.m_isNull
        m_isDetour = p.m_isDetour
        m_nodeDest = p.m_nodeDest
    End Sub

    Public Function IsNull() As Boolean
        Return m_isNull
    End Function

    Public Sub SetToNull()
        m_isNull = True
    End Sub

    Public Overloads Shared Operator +(ByVal p1 As AbsoluteLocationClass, ByVal p2 As DeltaLocationClass) As AbsoluteLocationClass
        Return New AbsoluteLocationClass(p1.x + p2.GetX, p1.y + p2.GetY)
    End Operator

    Public Overloads Shared Operator +(ByVal p1 As AbsoluteLocationClass, ByVal p2 As AbsoluteLocationClass) As AbsoluteLocationClass
        Return New AbsoluteLocationClass(p1.x + p2.GetX, p1.y + p2.GetY)
    End Operator

    Public Function IsEqualTo(ByRef p As AbsoluteLocationClass) As Boolean
        Dim result As Boolean = False

        If p.x = x And p.y = y Then result = True

        Return result
    End Function

    Public Sub SetDetour()
        m_isDetour = True
    End Sub

    Public Function IsDetour() As Boolean
        Return m_isDetour
    End Function

    Public Sub SetNodeDest(ByVal p As Integer)
        m_nodeDest = p
    End Sub

    Public Function GetNodeDest() As Integer
        Return m_nodeDest
    End Function

    Public Sub Move(ByVal p_angle As Integer)
        Dim dx As Integer = 0
        Dim dy As Integer = 0

        Select Case p_angle
            Case 0
                dx = 1

            Case 90
                dy = 1

            Case 180
                dx = -1

            Case 270
                dy = -1
        End Select

        x += dx
        y += dy
    End Sub

    Public Function Print() As String
        Return "(" & GetX() & ", " & GetY() & ")"
    End Function
End Class
