Module Paste
    Public Sub PasteHandler(Optional ByRef p_absoluteLocation As AbsoluteLocationClass = Nothing)
        For i = 0 To myCopyList.Count - 1
            Dim myAbsoluteLocation As New AbsoluteLocationClass(10, 10)

            If p_absoluteLocation IsNot Nothing Then
                If i = 0 Then
                    myAbsoluteLocation.CopyFrom(p_absoluteLocation)
                Else
                    Dim z As New DeltaLocationClass(sibbList(myCopyList(0)).absoluteLocation, sibbList(myCopyList(i)).absoluteLocation)

                    myAbsoluteLocation.CopyFrom(p_absoluteLocation)
                    myAbsoluteLocation.Add(z.GetX, z.GetY)
                End If
            End If

            DesignerForm.CommonNodeAdd(sibbList(myCopyList(i)).Clone(), myAbsoluteLocation)
        Next

        myCopyList.Clear()
    End Sub
End Module
