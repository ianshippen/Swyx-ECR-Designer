Public Class LineSegmentClass
    Public myStart As New AbsoluteLocationClass
    Public myEnd As New AbsoluteLocationClass
    Public myDestNodeIndex As Integer = -1
    Public myNextSegmentIndex As Integer = -1
    Public myUsedByList As New List(Of UsedByClass)

    Public Sub New(ByVal p_sibbIndex As Integer, ByVal p_outputIndex As Integer, ByVal p_destNodeIndex As Integer)
        Dim myUsedBy As New UsedByClass(p_sibbIndex, p_outputIndex)

        myUsedByList.Add(myUsedBy)
        myDestNodeIndex = p_destNodeIndex
    End Sub

    Public Function IsHorizontal() As Boolean
        Dim result As Boolean = False

        If myStart.GetY = myEnd.GetY Then result = True

        Return result
    End Function

    Public Function IsVertical() As Boolean
        Return Not IsHorizontal()
    End Function

    Public Function GetAngle() As Integer
        Dim result As Integer = 0

        If IsHorizontal() Then
            If myEnd.GetX < myStart.GetX Then result = 180
        Else
            result = 90

            If myEnd.GetY < myStart.GetY Then result = 270
        End If

        Return result
    End Function
End Class
