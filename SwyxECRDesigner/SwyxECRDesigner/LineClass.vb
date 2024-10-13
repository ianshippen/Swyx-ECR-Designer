Public Class LineClass
    Private nodeIndex As Integer
    Public startPos, endPos As New AbsoluteLocationClass
    Private nextNodeIndex As Integer

    Public Sub New(ByVal p_nodeIndex As Integer, ByRef p_startPos As AbsoluteLocationClass, ByRef p_endPos As AbsoluteLocationClass, ByVal p_nextNodeIndex As Integer)
        nodeIndex = p_nodeIndex
        startPos.CopyFrom(p_startPos)
        endPos.CopyFrom(p_endPos)
        nextNodeIndex = p_nextNodeIndex
    End Sub

    Public Function IsXInRange(ByVal p_x As Integer) As Boolean
        Dim result As Boolean = False

        If startPos.GetX <= endPos.GetX Then
            If p_x > startPos.GetX And p_x < endPos.GetX Then result = True
        Else
            If p_x > endPos.GetX And p_x < startPos.GetX Then result = True
        End If

        Return result
    End Function

    Public Function IsYInRange(ByVal p_y As Integer) As Boolean
        Dim result As Boolean = False

        If startPos.GetY <= endPos.GetY Then
            If p_y > startPos.GetY And p_y < endPos.GetY Then result = True
        Else
            If p_y > endPos.GetY And p_y < startPos.GetY Then result = True
        End If

        Return result
    End Function

    Public Function IsVertical() As Boolean
        Dim result As Boolean = False

        If startPos.GetX = endPos.GetX Then result = True

        Return result
    End Function

    Public Function GetNodeIndex() As Integer
        Return nodeIndex
    End Function

    Public Function GetNextNodeIndex() As Integer
        Return nextNodeIndex
    End Function
End Class
