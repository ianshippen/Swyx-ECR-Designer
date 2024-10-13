Module Delete
    Public Sub DeleteHandler(Optional ByVal p_nodeIndex As Integer = -1)
        Dim myIndexesToDelete As New List(Of Integer)

        If p_nodeIndex >= 0 Then
            If sibbList(p_nodeIndex).CanDelete Then myIndexesToDelete.Add(p_nodeIndex)
        Else
            Select Case GetGUIState()
                Case GUIStateType.HEADING_SELECTED, GUIStateType.HEADINGS_SELECTED
                    ' Make a list of all the selected nodes indexes we are allowed to delete
                    For i = 0 To sibbList.Count - 1
                        If sibbList(i).IsHilighted And sibbList(i).CanDelete Then myIndexesToDelete.Add(i)
                    Next

                    myIndexesToDelete.Sort()

                    SetGUIState(GUIStateType.NULL)

                Case GUIStateType.OUTPUT_SELECTED
                    For i = 0 To sibbList.Count - 1
                        With sibbList(i)
                            For j = 0 To .GetOutputCount - 1
                                If .OutputIsSelected(j) Then
                                    ' Remove this connection
                                    .RemoveOutput(j)
                                End If
                            Next
                        End With
                    Next

                    SetGUIState(GUIStateType.NULL)
            End Select
        End If

        For i = myIndexesToDelete.Count - 1 To 0 Step -1
            ' Remove any links to this node, and decrement any links that point to beyond this node
            For j = 0 To sibbList.Count - 1
                With sibbList(j)
                    For k = 0 To .GetOutputCount - 1
                        If .GetOutputNextNode(k) = myIndexesToDelete(i) Then .SetOutputNextNodeToNull(k)

                        If .GetOutputNextNode(k) > myIndexesToDelete(i) Then .DecOutputNextNode(k)
                    Next
                End With
            Next

            sibbList.RemoveAt(myIndexesToDelete(i))
        Next

        RepaintAll()
    End Sub
End Module
