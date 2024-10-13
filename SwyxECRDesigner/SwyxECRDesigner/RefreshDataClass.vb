Imports System.Math

Public Class RefreshDataClass
    Private m_data() As Integer
    Private m_width, m_height As Integer
    Private m_valid As Boolean
    Private m_position As New AbsoluteLocationClass

    Public Sub New()
        m_valid = False
        m_width = 0
        m_height = 0
    End Sub

    Public Sub Init(ByRef p_position As AbsoluteLocationClass, ByVal p_width As Integer, ByVal p_height As Integer)
        m_width = p_width
        m_height = p_height
        ReDim m_data((2 * Abs(m_width)) + (2 * (Abs(m_height) - 2)) - 1)
        m_position.CopyFrom(p_position)
    End Sub

    Public Sub SetTopPixel(ByVal p_index As Integer, ByVal p_colour As Integer)
        m_data(p_index) = p_colour
    End Sub

    Public Sub SetBottomPixel(ByVal p_index As Integer, ByVal p_colour As Integer)
        m_data(Abs(m_width) + p_index) = p_colour
    End Sub

    Public Sub SetLeftPixel(ByVal p_index As Integer, ByVal p_colour As Integer)
        m_data((Abs(m_width) * 2) + p_index) = p_colour
    End Sub

    Public Sub SetRightPixel(ByVal p_index As Integer, ByVal p_colour As Integer)
        m_data((Abs(m_width) * 2) + (Abs(m_height) - 2) + p_index) = p_colour
    End Sub

    Public Sub MakeValid()
        m_valid = True
    End Sub

    Public Sub MakeInvalid()
        m_valid = False
    End Sub

    Public Function IsValid() As Boolean
        Return m_valid
    End Function

    Public Function GetWidth() As Integer
        Return m_width
    End Function

    Public Function GetHeight() As Integer
        Return m_height
    End Function

    Public Function GetPosition() As AbsoluteLocationClass
        Dim z As New AbsoluteLocationClass

        z.CopyFrom(m_position)

        Return z
    End Function

    Public Function GetTopPixel(ByVal p_index As Integer) As Integer
        Return m_data(p_index)
    End Function

    Public Function GetBottomPixel(ByVal p_index As Integer) As Integer
        Return m_data(Abs(m_width) + p_index)
    End Function

    Public Function GetLeftPixel(ByVal p_index As Integer) As Integer
        Return m_data((2 * Abs(m_width)) + p_index)
    End Function

    Public Function GetRightPixel(ByVal p_index As Integer) As Integer
        Return m_data((2 * Abs(m_width)) + (Abs(m_height) - 2) + p_index)
    End Function
End Class
