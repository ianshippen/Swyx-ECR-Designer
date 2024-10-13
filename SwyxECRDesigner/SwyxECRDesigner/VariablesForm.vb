Public Class VariablesForm
    Public Sub ClearVariables()
        ddiKeysDataGridView.Tag = ""
        serviceWideVariablesDataGridView.Rows.Clear()
        swyxVariableNameComboBox.Text = ""
        DesignerForm.ClearDDIVariables()

        ddiKeysDataGridView.Rows.Clear()
        ddiVariablesDataGridView.Rows.Clear()
        ddiVariablesLabel.Text = "DDI Variables"
    End Sub

    Public Sub SortServiceWideVariablesByName()
        serviceWideVariablesDataGridView.Sort(serviceWideVariablesDataGridView.Columns(0), System.ComponentModel.ListSortDirection.Ascending)
    End Sub

    Private Sub VariablesForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SortServiceWideVariablesByName()

        deleteDDIKeyButton.Enabled = False
        editDDIVarButton.Enabled = False
        deleteDDIVarButton.Enabled = False
        ddiVariablesLabel.Text = "DDI Variables"

        With DesignerForm.ddiVariableTypes
            swyxVariableNameComboBox.Items.Clear()

            For i = 0 To .Count - 1
                swyxVariableNameComboBox.Items.Add(.Item(i).GetDescription)
            Next
        End With

        If swyxVariableNameComboBox.Items.Count > 0 Then swyxVariableNameComboBox.Text = swyxVariableNameComboBox.Items(0)

        ddiKeysDataGridView.Tag = ""
        SwyxVariableNameIndexChangedHandler()
    End Sub

    Public Function GetDDIKeyName() As String
        Return swyxVariableNameComboBox.Text
    End Function

    Public Function GetNumberOfDDIKeys() As Integer
        Return ddiKeysDataGridView.Rows.Count
    End Function

    Public Function GetDDIKey(ByVal p_index As Integer) As String
        Dim myKey As String = ""

        myKey = ddiKeysDataGridView.Rows(p_index).Cells(1).Value

        If myKey Is Nothing Then myKey = ""

        Return myKey
    End Function

    Public Function GetDDIDescription(ByVal p_index As Integer) As String
        Dim x As String = ""

        x = ddiKeysDataGridView.Rows(p_index).Cells(0).Value

        If x Is Nothing Then x = ""

        Return x
    End Function

    Public Function GetDDIVariableName(ByVal p_index As Integer) As String
        Dim result As String = ddiVariablesDataGridView.Rows(p_index).Cells(0).Value

        If result Is Nothing Then result = ""

        Return result
    End Function

    Public Function GetDDIVariableValue(ByVal p_index As Integer) As String
        Dim result As String = ddiVariablesDataGridView.Rows(p_index).Cells(1).Value

        If result Is Nothing Then result = ""

        Return result
    End Function

    Private Sub ddiKeysDataGridView_CellLeave(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles ddiKeysDataGridView.CellLeave
        ' Did we leave the key value column ?
        If e.ColumnIndex = 1 Then
            ' Yes. Commit the change
            ddiKeysDataGridView.EndEdit()

            ' Bind to the Key Value cell
            With ddiKeysDataGridView.Rows(e.RowIndex).Cells(e.ColumnIndex)
                Dim myKeyValue As String = ""

                If .Value IsNot Nothing Then myKeyValue = .Value

                If myKeyValue <> "" Then
                    Dim mySwyxVariableDescription As String = swyxVariableNameComboBox.SelectedItem
                    Dim mySwyxVariableKey As String = DesignerForm.MapDescriptionToKey(mySwyxVariableDescription)
                    Dim myDescription As String = ddiKeysDataGridView.Rows(e.RowIndex).Cells(0).Value

                    ' Is this a new key value for this Swyx Variable type ?
                    If Not DesignerForm.DoesDDIKeyExist(mySwyxVariableKey, myKeyValue) Then
                        ' Add the new key value
                        DesignerForm.AddNewDDIKey(mySwyxVariableKey, myKeyValue, myDescription)

                        ' Clear the variables datagrid
                        ddiVariablesDataGridView.Rows.Clear()
                    End If
                End If
            End With
        End If
    End Sub

    Private Sub ddiKeysDataGridView_RowEnter(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles ddiKeysDataGridView.RowEnter
        ' Update the variables for this row
        Dim myDDIKey As String = ddiKeysDataGridView.Rows(e.RowIndex).Cells(1).Value
        Dim myVariableDescription As String = ddiKeysDataGridView.Rows(e.RowIndex).Cells(0).Value
        Dim myDescription As String = swyxVariableNameComboBox.SelectedItem

        If myDDIKey Is Nothing Then myDDIKey = ""

        ddiVariablesDataGridView.Rows.Clear()

        If myDDIKey <> "" Then
            Dim myDDIVariablesNames As List(Of String) = DesignerForm.GetDDIVariableNameList(DesignerForm.MapDescriptionToKey(myDescription), myDDIKey)

            myDDIVariablesNames.Sort()
            ddiVariablesDataGridView.Rows.Clear()

            For Each myDDIVariableName As String In myDDIVariablesNames
                ddiVariablesDataGridView.Rows.Add(myDDIVariableName, DesignerForm.GetDDIVariableValue(DesignerForm.MapDescriptionToKey(myDescription), myDDIKey, myDDIVariableName))
            Next

            ddiVariablesLabel.Text = "DDI Variables for " & myDDIKey & "  [ " & myVariableDescription & " ]"
            deleteDDIKeyButton.Enabled = True
        Else
            ddiVariablesLabel.Text = "DDI Variables"
            deleteDDIKeyButton.Enabled = False
        End If

        ddiKeysDataGridView.Tag = myDDIKey
    End Sub

    Private Sub addDDIVarButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles addDDIVarButton.Click
        EditVarForm.varNameComboBox.Items.Clear()
        EditVarForm.varValueTextBox.Clear()

        If ddiKeysDataGridView.Tag = "" Then
            MsgBox("Please select a DDI in the DDI Keys list", MsgBoxStyle.Information, "No DDI Selected ..")
        Else
            Dim myDDIKey As String = ddiKeysDataGridView.Tag
            Dim myServiceWideVariableName As String = ""

            EditVarForm.varNameTextBox.Visible = False
            EditVarForm.varNameComboBox.Visible = True
            EditVarForm.GroupBox1.Text = " Value "

            ' Get all the available service wide variables
            ' For i = 0 To DesignerForm.GetNumberOfServiceWideVariables() - 1
            '   myServiceWideVariableName = DesignerForm.GetServiceWideVariableName(i)
            For i = 0 To serviceWideVariablesDataGridView.Rows.Count - 1
                myServiceWideVariableName = serviceWideVariablesDataGridView.Rows(i).Cells(0).Value
                If myServiceWideVariableName <> "" Then
                    Dim myDescription As String = swyxVariableNameComboBox.SelectedItem

                    If Not DesignerForm.DoesDDIVariableExist(DesignerForm.MapDescriptionToKey(myDescription), myDDIKey, myServiceWideVariableName) Then
                        ' Add it to list of thoses available if it has not been used before
                        EditVarForm.varNameComboBox.Items.Add(myServiceWideVariableName)
                    End If
                End If
            Next

            If EditVarForm.varNameComboBox.Items.Count > 0 Then EditVarForm.varNameComboBox.SelectedIndex = 0

            EditVarForm.Text = "Add " & ddiVariablesLabel.Text
            EditVarForm.varTypeComboBox.Enabled = False

            If EditVarForm.ShowDialog = Windows.Forms.DialogResult.OK Then
                myServiceWideVariableName = EditVarForm.varNameComboBox.Text

                If myServiceWideVariableName = "" Then
                    MsgBox("No variable selected")
                Else
                    Dim myDescription As String = swyxVariableNameComboBox.SelectedItem

                    If Not DesignerForm.AddDDIVariable(DesignerForm.MapDescriptionToKey(myDescription), myDDIKey, myServiceWideVariableName, EditVarForm.varValueTextBox.Text) Then MsgBox("This DDI already has this variable")

                    RefreshDDIDataGridView()
                End If
            End If

            EditVarForm.varNameTextBox.Visible = True
            EditVarForm.varNameComboBox.Visible = False
            EditVarForm.GroupBox1.Text = " Default Value "
            EditVarForm.varTypeComboBox.Enabled = True
        End If
    End Sub

    Private Sub editDDIVarButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles editDDIVarButton.Click
        Dim myRow As Integer = -1

        ' Get the row of the DDI variable value to edit
        If ddiVariablesDataGridView.SelectedCells.Count > 0 Then
            If ddiVariablesDataGridView.SelectedCells(0).RowIndex >= 0 Then myRow = ddiVariablesDataGridView.SelectedCells(0).RowIndex
        End If

        If myRow >= 0 Then
            With EditVarForm
                .varNameComboBox.Items.Clear()
                .varNameTextBox.Visible = True
                .varNameComboBox.Visible = False
                .GroupBox1.Text = " Value "
                .varValueTextBox.Enabled = True

                .varNameTextBox.Text = ddiVariablesDataGridView.Rows(myRow).Cells(0).Value
                .varValueTextBox.Text = ddiVariablesDataGridView.Rows(myRow).Cells(1).Value

                .varTypeComboBox.Enabled = True
                .SetVarTypeComboBox(.GetServiceWideVariableTypeFromDatagridView(.varNameTextBox.Text))
                .varTypeComboBox.Enabled = False

                If .ShowDialog = Windows.Forms.DialogResult.OK Then
                    Dim myDDIVariableName As String = .varNameTextBox.Text
                    Dim myDescription As String = swyxVariableNameComboBox.SelectedItem
                    Dim myDDIKey As String = ddiKeysDataGridView.Tag

                    DesignerForm.SetDDIVariable(DesignerForm.MapDescriptionToKey(myDescription), myDDIKey, myDDIVariableName, EditVarForm.varValueTextBox.Text)
                    RefreshDDIDataGridView()
                End If
            End With

            EditVarForm.varNameTextBox.Visible = True
            EditVarForm.varNameComboBox.Visible = False
            EditVarForm.GroupBox1.Text = " Default Value "
            EditVarForm.varTypeComboBox.Enabled = True
        End If

        Return

        ' Check that the user has selected a DDI
        If ddiKeysDataGridView.Tag = "" Then
            MsgBox("No DDI selected")
        Else
            Dim myDescription As String = swyxVariableNameComboBox.SelectedItem
            Dim myDDIKey As String = ddiKeysDataGridView.Tag

            EditVarForm.varNameTextBox.Visible = False
            EditVarForm.varNameComboBox.Visible = True
            EditVarForm.GroupBox1.Text = " Value "

            ' Get all the variables that this DDI refines
            Dim myDDIVariables As List(Of String) = DesignerForm.GetDDIVariableNameList(DesignerForm.MapDescriptionToKey(myDescription), myDDIKey)

            For Each myDDIVariableName As String In myDDIVariables
                If myDDIVariableName <> "" Then EditVarForm.varNameComboBox.Items.Add(myDDIVariableName)
            Next

            EditVarForm.Text = "Edit " & ddiVariablesLabel.Text
            EditVarForm.varTypeComboBox.Enabled = False
            EditVarForm.varNameComboBox.SelectedItem = ddiVariablesDataGridView.Rows(myRow).Cells(0).Value
            EditVarForm.varValueTextBox.Text = ddiVariablesDataGridView.Rows(myRow).Cells(1).Value

            If EditVarForm.ShowDialog = Windows.Forms.DialogResult.OK Then
                Dim myDDIVariableName As String = EditVarForm.varNameComboBox.Text

                If myDDIVariableName = "" Then
                    MsgBox("No variable selected")
                Else
                    DesignerForm.SetDDIVariable(DesignerForm.MapDescriptionToKey(myDescription), myDDIKey, myDDIVariableName, EditVarForm.varValueTextBox.Text)
                    RefreshDDIDataGridView()
                End If
            End If

            EditVarForm.varNameTextBox.Visible = True
            EditVarForm.varNameComboBox.Visible = False
            EditVarForm.GroupBox1.Text = " Default Value "
            EditVarForm.varTypeComboBox.Enabled = True
        End If
    End Sub

    Public Sub RefreshDDIDataGridView()
        Dim myDescription As String = swyxVariableNameComboBox.SelectedItem

        With ddiVariablesDataGridView
            .Rows.Clear()

            If ddiKeysDataGridView.Tag <> "" Then
                Dim myDDIKey As String = ddiKeysDataGridView.Tag
                Dim ddiVariableNameList As List(Of String) = DesignerForm.GetDDIVariableNameList(DesignerForm.MapDescriptionToKey(myDescription), myDDIKey)

                For Each ddiVariableName As String In ddiVariableNameList
                    ddiVariablesDataGridView.Rows.Add(ddiVariableName, DesignerForm.GetDDIVariableValue(DesignerForm.MapDescriptionToKey(myDescription), myDDIKey, ddiVariableName))
                Next
            End If
        End With
    End Sub

    Private Sub addVarButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles addVarButton.Click
        With EditVarForm
            .Text = "Add Service Wide Variable"
            .varNameTextBox.Clear()
            .varValueTextBox.Clear()
            .SetVarTypeComboBox("")

            If .ShowDialog = Windows.Forms.DialogResult.OK Then
                Dim defaultValue As String = ""
                Dim myType As String = .varTypeComboBox.Text

                If .varValueTextBox.Text = "" Then
                    Select Case myType
                        Case "Boolean"
                            defaultValue = "False"

                        Case "Integer"
                            defaultValue = "0"
                    End Select
                Else
                    defaultValue = .varValueTextBox.Text
                End If

                serviceWideVariablesDataGridView.Rows.Add(.varNameTextBox.Text, defaultValue, myType)
                SortServiceWideVariablesByName()
            End If
        End With
    End Sub

    Private Sub editVarButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles editVarButton.Click
        If serviceWideVariablesDataGridView.SelectedCells.Count > 0 Then
            If serviceWideVariablesDataGridView.CurrentCell.RowIndex >= 0 Then
                EditRow()
            End If
        End If
    End Sub

    Private Sub deleteVarButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles deleteVarButton.Click
        If serviceWideVariablesDataGridView.SelectedCells.Count > 0 Then
            If serviceWideVariablesDataGridView.CurrentCell.RowIndex >= 0 Then
                Dim myName As String = serviceWideVariablesDataGridView.Rows(serviceWideVariablesDataGridView.CurrentCell.RowIndex).Cells(0).Value

                If myName <> "" Then
                    If DesignerForm.GetServiceWideVariableDeletable(myName) Then
                        ' Check if this variable is being used a node
                        Dim usedByNodeIndex = -1

                        For i = 0 To sibbList.Count - 1
                            With sibbList(i)
                                Dim myData = .GetData.ToLower
                                Dim myNameAsLower = myName.ToLower

                                If myData.Contains(myNameAsLower) Then
                                    usedByNodeIndex = i
                                    Exit For
                                End If
                            End With
                        Next

                        If usedByNodeIndex >= 0 Then
                            If MsgBox("Service Wide variable " & WrapInQuotes(myName) & " is used by Node " & usedByNodeIndex & " " & WrapInQuotes(sibbList(usedByNodeIndex).GetNodeTitle) & vbCrLf & "Are you sure you want to remove it ?", MsgBoxStyle.YesNoCancel) = MsgBoxResult.Yes Then
                                usedByNodeIndex = -1
                            End If
                        End If

                        If usedByNodeIndex = -1 Then
                            If MsgBox("Are you sure you want to delete Service Wide variable " & WrapInQuotes(myName) & " ?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                                serviceWideVariablesDataGridView.Rows.RemoveAt(serviceWideVariablesDataGridView.CurrentCell.RowIndex)
                            End If
                        End If
                    Else
                        MsgBox("Service Wide variable " & WrapInQuotes(myName) & " is not deletable")
                    End If
                End If
            End If
        End If
    End Sub


    Private Sub DataGridView1_CellContentDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles serviceWideVariablesDataGridView.CellContentDoubleClick
        With serviceWideVariablesDataGridView
            If .SelectedCells.Count > 0 Then EditRow()
        End With
    End Sub

    Private Sub EditRow()
        With serviceWideVariablesDataGridView.Rows(serviceWideVariablesDataGridView.CurrentCell.RowIndex)
            Dim myName As String = .Cells(0).Value
            Dim myValue As String = .Cells(1).Value
            Dim myType As String = .Cells(2).Value

            With EditVarForm
                If myType = "" Or myType.ToLower = "null" Then
                    .varTypeComboBox.Enabled = True
                    .varTypeComboBox.SelectedItem = Nothing
                Else
                    .SetVarTypeComboBox(myType)
                    .varTypeComboBox.Enabled = False
                End If

                .varNameTextBox.Text = myName
                .varValueTextBox.Text = myValue

                .Text = "Edit Service Wide Variable"
                .varNameTextBox.Enabled = False
            End With

            If EditVarForm.ShowDialog = Windows.Forms.DialogResult.OK Then
                .Cells(1).Value = EditVarForm.varValueTextBox.Text
                .Cells(2).Value = EditVarForm.varTypeComboBox.Text
            End If

            With EditVarForm
                .varTypeComboBox.Enabled = True
                .varNameTextBox.Enabled = True
                .varValueTextBox.Enabled = True
            End With
        End With
    End Sub

    Private Sub deleteDDIKeyButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles deleteDDIKeyButton.Click
        Dim myDescription As String = swyxVariableNameComboBox.SelectedItem

        With ddiKeysDataGridView
            If .SelectedCells.Count > 0 Then
                If .CurrentCell.RowIndex >= 0 Then
                    Dim myDDIKey As String = GetDDIKey(.CurrentCell.RowIndex)

                    If myDDIKey <> "" Then DesignerForm.RemoveDDIKey(DesignerForm.MapDescriptionToKey(myDescription), myDDIKey)

                    .Tag = ""
                    .Rows.RemoveAt(.CurrentCell.RowIndex)
                End If
            End If
        End With
    End Sub

    Private Sub deleteDDIVarButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles deleteDDIVarButton.Click
        Dim myDescription As String = swyxVariableNameComboBox.SelectedItem

        With ddiVariablesDataGridView
            If .SelectedCells.Count > 0 Then
                If .CurrentCell.RowIndex >= 0 Then
                    If ddiKeysDataGridView.Tag <> "" Then
                        Dim myDDIKey As String = ddiKeysDataGridView.Tag
                        Dim myVariableName As String = .Rows(.CurrentCell.RowIndex).Cells(0).Value

                        DesignerForm.RemoveDDIVariable(DesignerForm.MapDescriptionToKey(myDescription), myDDIKey, myVariableName)
                    End If

                    .Rows.RemoveAt(.CurrentCell.RowIndex)
                End If
            End If
        End With
    End Sub

    Private Sub okButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles okButton.Click
        serviceWideVariablesDataGridView.CommitEdit(DataGridViewDataErrorContexts.CurrentCellChange)
        DialogResult = Windows.Forms.DialogResult.OK
    End Sub

    Private Sub swyxVariableNameComboBox_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles swyxVariableNameComboBox.SelectedIndexChanged
        SwyxVariableNameIndexChangedHandler()
    End Sub

    Private Sub SwyxVariableNameIndexChangedHandler()
        Dim myDDITypeKey As String = GetSwyxVariableNameKeyFromComboBox()

        ' Clear the variables in case we do not get anything
        ddiVariablesDataGridView.Rows.Clear()

        ' Clear the data grid view
        With ddiKeysDataGridView
            .Rows.Clear()

            ' Get the list of DDI keys for this entry and add them to the data grid view
            For Each myKey As String In DesignerForm.GetListOfDDIKeysForThisEntry(myDDITypeKey)
                .Rows.Add(DesignerForm.GetDDIKeyDescription(myDDITypeKey, myKey), myKey)
            Next
        End With
    End Sub

    Public Function GetSwyxVariableNameKeyFromComboBox() As String
        Dim myDescription As String = swyxVariableNameComboBox.SelectedItem

        Return DesignerForm.MapDescriptionToKey(myDescription)
    End Function

    Private Sub ddiVariablesDataGridView_RowEnter(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles ddiVariablesDataGridView.RowEnter
        Dim myVariableName As String = ddiVariablesDataGridView.Rows(e.RowIndex).Cells(0).Value

        If myVariableName Is Nothing Then myVariableName = ""

        If myVariableName <> "" Then
            editDDIVarButton.Enabled = True
            deleteDDIVarButton.Enabled = True
        End If
    End Sub
End Class