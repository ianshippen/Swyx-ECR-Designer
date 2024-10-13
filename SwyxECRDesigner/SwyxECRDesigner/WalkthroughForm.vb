Public Class WalkthroughForm
    Private Sub WalkthroughForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        With walkthroughGlobalsDataGridView
            For i = 0 To DesignerForm.GetNumberOfServiceWideVariables - 1
                .Rows.Add(DesignerForm.GetServiceWideVariableName(i), DesignerForm.GetServiceWideVariableValue(i), DesignerForm.GetServiceWideVariableType(i))
            Next

            .Sort(.Columns(0), System.ComponentModel.ListSortDirection.Ascending)
        End With

        With walkthroughDDIDataGridView
            For i = 0 To DesignerForm.ddiVariableTypes.Count - 1
                Dim myDDITypeDescription As String = DesignerForm.ddiVariableTypes(i).GetDescription
                Dim myDDIType As String = DesignerForm.ddiVariableTypes(i).GetKey

                ' Is this DDI key type used at all for this service ?
                Dim myDDIKeyTypeList As List(Of String) = DesignerForm.GetListOfDDIKeyEntries()

                For Each keyType As String In myDDIKeyTypeList
                    Dim myMatchList As List(Of String) = DesignerForm.GetListOfDDIKeysForThisEntry(keyType)

                    For Each myMatch As String In myMatchList
                        Dim myVariablesList As List(Of String) = DesignerForm.GetListOfDDIVariables(myDDIType, myMatch)

                        For Each myVariable As String In myVariablesList
                            .Rows.Add(myDDITypeDescription, myMatch, myVariable, DesignerForm.GetDDIVariableValue(myDDIType, myMatch, myVariable))
                        Next
                    Next
                Next
            Next
        End With
    End Sub
End Class