Module ContextMenu
    Enum ContextTypes
        CUT
        COPY
        PASTE
        DELETE
    End Enum

    Public myCopyList As New List(Of Integer)

    Public Sub MyContextHandler(ByVal p_operation As ContextTypes, ByVal p_nodeIndex As Integer, ByVal p_outputIndex As Integer)
        If p_outputIndex = -1 Then
            Select Case p_operation
                Case ContextTypes.COPY
                    myCopyList.Clear()

                    myCopyList.Add(p_nodeIndex)

                    ' If this node is selected, are there any other nodes selected ?
                    If p_nodeIndex >= 0 Then
                        If sibbList(p_nodeIndex).IsHilighted Then
                            For i = 0 To sibbList.Count - 1
                                If i <> p_nodeIndex Then
                                    With sibbList(i)
                                        If .IsHilighted And .CanDelete Then
                                            myCopyList.Add(i)
                                        End If
                                    End With
                                End If
                            Next
                        End If
                    End If

                Case ContextTypes.DELETE
                    DeleteHandler(p_nodeIndex)

                Case ContextTypes.PASTE
                    Dim x As New ScreenLocationClass(lastMousePosition.GetX, lastMousePosition.GetY)

                    PasteHandler(ScreenToAbsolutePosition(x))
            End Select
        Else
            Select Case p_operation
                Case ContextTypes.DELETE
                    With sibbList(p_nodeIndex)
                        .outputs(p_outputIndex).nextNodeIndex = -1
                    End With

                    SetGUIState(GUIStateType.NULL)
                    RepaintAll()
            End Select
        End If
    End Sub
End Module
