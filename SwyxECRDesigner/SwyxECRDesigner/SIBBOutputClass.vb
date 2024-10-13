Public Class SIBBOutputClass
    Public nextNodeIndex As Integer
    Public name As String
    Public selected As Boolean
    Public visible As Boolean
    Public debugSelected As Boolean
    Public tabValue As Integer
    Public order As Integer
    Public routeColour As Drawing.Color
    ' Public points As New List(Of AbsoluteLocationClass)
    Public lineSegmentIndexes As New List(Of Integer)

    Public Sub New(Optional ByVal p_name As String = "Disconnected")
        nextNodeIndex = -1
        name = p_name
        selected = False
        visible = True
        debugSelected = False
        tabValue = 0
        order = -1
        routeColour = Nothing
    End Sub

    Public Sub Remove()
        nextNodeIndex = -1
        selected = False
        debugSelected = False
    End Sub
End Class
