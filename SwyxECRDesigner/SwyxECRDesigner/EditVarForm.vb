Public Class EditVarForm

    Private Sub varNameTextBox_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles varNameTextBox.TextChanged
        With varNameTextBox
            If .Text <> "" Then
                If Not .Text.StartsWith("$") Then
                    .Text = "$" & .Text
                    .Select(.Text.Length, .Text.Length)
                End If
            End If
        End With
    End Sub

    Private Sub varValueTextBox_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles varValueTextBox.TextChanged
        With varValueTextBox
            If .Tag Is Nothing Then .Tag = ""

            Select Case varTypeComboBox.Text
                Case DesignerForm.GetVariableTypeNameFromType(DesignerForm.VariableTypes.ALPHA_STRING)

                Case DesignerForm.GetVariableTypeNameFromType(DesignerForm.VariableTypes.BOOLEAN)
                    If .Text.Length >= .Tag.length Then
                        If .Text.ToLower.StartsWith("t") Then .Text = "True"
                        If .Text.ToLower.StartsWith("f") Then .Text = "False"
                    Else
                        .Text = ""
                    End If

                    .Select(.Text.Length, 0)

                Case DesignerForm.GetVariableTypeNameFromType(DesignerForm.VariableTypes.DATE)
                    Select Case .Text.Length
                        Case 2, 5
                            ' Check if we are we have moved from a 1 to 2 digit or a 4 to 5 digit scenario
                            If .Text.Length > .Tag.length Then
                                ' Append a / after DD or DD/MM
                                .Text &= "/"
                                varValueTextBox.Select(.Text.Length, .Text.Length)
                            Else
                                ' This is a 3 to 2 or a 6 to 5 digit scenario so trim the last digit
                                .Text = .Text.Substring(0, .Text.Length - 1)
                                .Select(.Text.Length, 0)
                            End If

                        Case 11
                            .Text = .Text.Substring(0, 8)
                    End Select

                Case DesignerForm.GetVariableTypeNameFromType(DesignerForm.VariableTypes.DATE_TIME_ISO)
                    Select Case .Text.Length
                        Case 4, 7
                            If .Text.Length > .Tag.length Then
                                .Text &= "-"
                                varValueTextBox.Select(.Text.Length, .Text.Length)
                            Else
                                .Text = .Text.Substring(0, .Text.Length - 1)
                                .Select(.Text.Length, 0)
                            End If

                        Case 10
                            If .Text.Length > .Tag.length Then
                                .Text &= " "
                                varValueTextBox.Select(.Text.Length, .Text.Length)
                            Else
                                .Text = .Text.Substring(0, .Text.Length - 1)
                                .Select(.Text.Length, 0)
                            End If

                        Case 13, 16
                            If .Text.Length > .Tag.length Then
                                .Text &= ":"
                                varValueTextBox.Select(.Text.Length, .Text.Length)
                            Else
                                .Text = .Text.Substring(0, .Text.Length - 1)
                                .Select(.Text.Length, 0)
                            End If
                    End Select

                Case DesignerForm.GetVariableTypeNameFromType(DesignerForm.VariableTypes.FILENAME)

                Case DesignerForm.GetVariableTypeNameFromType(DesignerForm.VariableTypes.INTEGER)
                    Dim myIndex As Integer = -1

                    For i = 0 To .Text.Length - 1
                        If Not (Asc(.Text(i)) >= Asc("0") And Asc(.Text(i)) <= Asc("9")) Then
                            myIndex = i
                            Exit For
                        End If
                    Next

                    If myIndex >= 0 Then
                        .Text = .Text.Remove(myIndex, 1)
                    End If

                Case DesignerForm.GetVariableTypeNameFromType(DesignerForm.VariableTypes.NUMBER_STRING)
                    Dim myIndex As Integer = -1

                    For i = 0 To .Text.Length - 1
                        If Not ((Asc(.Text(i)) >= Asc("0") And Asc(.Text(i)) <= Asc("9")) Or .Text(i) = "+") Then
                            myIndex = i
                            Exit For
                        End If
                    Next

                    If myIndex >= 0 Then
                        .Text = .Text.Remove(myIndex, 1)
                    End If

                Case DesignerForm.GetVariableTypeNameFromType(DesignerForm.VariableTypes.TIME)
                    Select Case .Text.Length
                        Case 2, 5
                            If .Text.Length > .Tag.length Then
                                ' Add the colon if required
                                .AppendText(":")
                            Else
                                .Text = .Text.Substring(0, .Text.Length - 1)
                                .Select(.Text.Length, 0)
                            End If

                        Case 9
                            ' Limit length to 8 characters
                            .Text = .Text.Substring(0, 8)
                    End Select
            End Select

            'varValueTextBox.Select(.Text.Length, .Text.Length)
            .Tag = .Text
        End With
    End Sub

    Private Sub EditVarForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        '       If varTypeComboBox.Enabled Then
        'If varTypeComboBox.SelectedIndex = -1 Then
        'MsgBox("Please select a Type for this variable")
        '  End If
        '   End If
        Tag = varValueTextBox.Text
        varValueTextBox.Enabled = True

        If varTypeComboBox.Enabled Then
            If varTypeComboBox.SelectedIndex = -1 Then
                varValueTextBox.Enabled = False
            End If
        End If
    End Sub

    Private Sub varTypeComboBox_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles varTypeComboBox.SelectedIndexChanged
        varValueTextBox.Enabled = True
        Return
        varTypeComboBox.TabIndex = 1
        varNameComboBox.TabIndex = 2
        varValueTextBox.TabIndex = 3
        okButton.TabIndex = 4
        cancelButton.TabIndex = 5
    End Sub

    Public Function GetServiceWideVariableTypeFromDatagridView(ByRef p_name As String) As String
        Dim result As String = "Null"

        For i = 0 To VariablesForm.serviceWideVariablesDataGridView.Rows.Count - 1
            With VariablesForm.serviceWideVariablesDataGridView.Rows(i)
                If .Cells(0).Value = p_name Then
                    result = .Cells(2).Value
                    Exit For
                End If
            End With
        Next

        Return result
    End Function

    Private Sub varNameComboBox_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles varNameComboBox.SelectedIndexChanged
        varTypeComboBox.Enabled = True
        'SetVarTypeComboBox(DesignerForm.GetServiceWideVariableType(varNameComboBox.SelectedItem))
        SetVarTypeComboBox(GetServiceWideVariableTypeFromDatagridView(varNameComboBox.SelectedItem))
        varTypeComboBox.Enabled = False

        ' If the DDI variable already exists, get its current value
        If VariablesForm.ddiKeysDataGridView.Tag <> "" Then
            Dim myKey As String = VariablesForm.ddiKeysDataGridView.Tag
            Dim myDescription As String = VariablesForm.swyxVariableNameComboBox.SelectedItem

            If DesignerForm.DoesDDIVariableExist(DesignerForm.MapDescriptionToKey(myDescription), myKey, varNameComboBox.SelectedItem) Then varValueTextBox.Text = DesignerForm.GetDDIVariableValue(DesignerForm.MapDescriptionToKey(myDescription), myKey, varNameComboBox.SelectedItem)
        End If
    End Sub

    Private Sub varValueTextBox_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles varValueTextBox.KeyPress
        Dim myType As DesignerForm.VariableTypes = DesignerForm.VariableTypes.NULL

        ' Intercept all key presses to the Value text box here so we can screen them
        If varTypeComboBox.SelectedItem IsNot Nothing Then myType = DesignerForm.GetVariableTypeFromName(varTypeComboBox.SelectedItem)

        Select Case myType
            Case DesignerForm.VariableTypes.NULL
                MsgBox("Please select a Type for this Variable", MsgBoxStyle.OkOnly, "Edit Variable Check")

            Case DesignerForm.VariableTypes.ALPHA_STRING

            Case DesignerForm.VariableTypes.BOOLEAN

            Case DesignerForm.VariableTypes.DATE
                ' Only allow 0-9 if length is currently 0, 1, 3, 4, 6, 7, 8 or 9
                ' Also check valid digit values depending upon position
                ' Pos 0: Allow 0, 1, 2, 3
                ' Pos 1: Allow 0 and  1 up to "31"
                ' Pos 3: Allow 0 to 1
                ' Pos 4: Allow 0 to 9 up to "12"
                ' Pos 6 onwards: Allow 0 to 9

                ' Only allow / if length is currently 2 or 5
                ' Allow backspace for any length
                Dim myLength As Integer = varValueTextBox.Text.Length
                Dim myCode As Integer = Asc(e.KeyChar)

                e.Handled = True

                If myCode = Keys.Back Then
                    ' Pass backspace right through
                    e.Handled = False
                Else
                    If myLength < 10 Then
                        If myLength = 2 Or myLength = 5 Then
                            If myCode = Asc("/") Then e.Handled = False
                        Else
                            Dim lowerLimit As Integer = Asc("0")
                            Dim upperLimit As Integer = Asc("9")

                            Select Case myLength
                                Case 0
                                    upperLimit = Asc("3")

                                Case 1
                                    GetDayUnitsRange(varValueTextBox.Text(myLength - 1), lowerLimit, upperLimit)

                                Case 3
                                    upperLimit = Asc("1")

                                Case 4
                                    GetMonthUnitsRange(varValueTextBox.Text(myLength - 1), lowerLimit, upperLimit)
                            End Select

                            If myCode >= lowerLimit And myCode <= upperLimit Then e.Handled = False
                        End If
                    End If
                End If

            Case DesignerForm.VariableTypes.DATE_TIME_ISO
                Dim myLength As Integer = varValueTextBox.Text.Length
                Dim myCode As Integer = Asc(e.KeyChar)
                e.Handled = True

                If myCode = Keys.Back Then
                    e.Handled = False
                Else
                    If myLength < 19 Then
                        If myLength = 4 Or myLength = 7 Then
                            If myCode = Asc("-") Then e.Handled = False
                        Else
                            Dim lowerLimit As Integer = Asc("0")
                            Dim upperLimit As Integer = Asc("9")

                            Select Case myLength
                                Case 5
                                    upperLimit = Asc("1")

                                Case 6
                                    GetMonthUnitsRange(varValueTextBox.Text(myLength - 1), lowerLimit, upperLimit)

                                Case 8
                                    upperLimit = Asc("3")

                                Case 9
                                    GetDayUnitsRange(varValueTextBox.Text(myLength - 1), lowerLimit, upperLimit)

                                Case 11
                                    upperLimit = Asc("2")

                                Case 12
                                    If varValueTextBox.Text(11) = "2" Then upperLimit = Asc("3")

                                Case 13, 16
                                    If myCode = Asc(":") Then e.Handled = False

                                Case 14, 17
                                    upperLimit = Asc("5")
                            End Select

                            If myCode >= lowerLimit And myCode <= upperLimit Then e.Handled = False
                        End If
                    End If
                End If

            Case DesignerForm.VariableTypes.FILENAME

            Case DesignerForm.VariableTypes.INTEGER

            Case DesignerForm.VariableTypes.NUMBER_STRING

            Case DesignerForm.VariableTypes.TIME
                ' Only allow 0-9 if length is currently 0, 1, 3, 4, 6 or 7
                ' Also check valid digit values depending upon position
                ' Pos 0: Allow 0, 1, 2
                ' Pos 1: Allow 0 and  1 up to "23"
                ' Pos 3: Allow 0 to 5
                ' Pos 4: Allow 0 to 9
                ' Pos 6: Allow 0 to 5
                ' Pos 7: Allow 0 to 9

                ' Only allow : if length is currently 2 or 5
                ' Allow backspace for any length
                Dim myLength As Integer = varValueTextBox.Text.Length
                Dim myCode As Integer = Asc(e.KeyChar)
                e.Handled = True

                If myCode = Keys.Back Then
                    e.Handled = False
                Else
                    If myLength < 8 Then
                        If myLength = 2 Or myLength = 5 Then
                            If myCode = Asc(":") Then e.Handled = False
                        Else
                            Dim upperLimit As Integer = Asc("9")

                            Select Case myLength
                                Case 0
                                    upperLimit = Asc("2")

                                Case 1
                                    If varValueTextBox.Text = "2" Then upperLimit = Asc("3")

                                Case 3, 6
                                    upperLimit = Asc("5")
                            End Select

                            If myCode >= Asc("0") And myCode <= upperLimit Then e.Handled = False
                        End If
                    End If
                End If
        End Select
    End Sub

    Private Sub GetMonthUnitsRange(ByRef p_monthTens As String, ByRef p_lowerLimit As Integer, ByRef p_upperLimit As Integer)
        Select Case p_monthTens
            Case "0"
                p_lowerLimit = Asc("1")

            Case "1"
                p_upperLimit = Asc("2")
        End Select
    End Sub

    Private Sub GetDayUnitsRange(ByRef p_daysTens As String, ByRef p_lowerLimit As Integer, ByRef p_upperLimit As Integer)
        Select Case p_daysTens
            Case "0"
                p_lowerLimit = Asc("1")

            Case "3"
                p_upperLimit = Asc("1")
        End Select
    End Sub

    Private Sub okButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles okButton.Click
        ' Parse the data
        Dim parsedOK As Boolean = False
        Dim myType As String = ""
        Dim myValue As String = varValueTextBox.Text
        Dim myName As String = varNameComboBox.Text

        If myName = "" Then myName = varNameTextBox.Text

        Dim allowEmptyString As Boolean = DesignerForm.GetServiceWideVariableAllowEmptyString(myName)

        If varTypeComboBox.SelectedItem IsNot Nothing Then myType = varTypeComboBox.SelectedItem

        Select Case DesignerForm.GetVariableTypeFromName(myType)
            Case DesignerForm.VariableTypes.NULL
                MsgBox("Please select a Type for this Variable", MsgBoxStyle.OkOnly, "Edit Variable Check")

            Case DesignerForm.VariableTypes.ALPHA_STRING
                parsedOK = True

            Case DesignerForm.VariableTypes.BOOLEAN
                Select Case myValue.ToLower
                    Case "false", "true"
                        parsedOK = True

                    Case Else
                        MsgBox("Value cannot be empty - must be set to True or False")
                End Select

            Case DesignerForm.VariableTypes.DATE
                If myValue.Length = 10 Then
                    parsedOK = True
                End If

                If Not parsedOK Then MsgBox("Error: Date value must be in DD/MM/YYYY format", MsgBoxStyle.OkOnly, "Edit Variable check")

            Case DesignerForm.VariableTypes.DATE_TIME_ISO
                If myValue = "" Then
                    If allowEmptyString Then parsedOK = True
                Else
                    If myValue.Length = 19 Then parsedOK = True
                End If

            Case DesignerForm.VariableTypes.FILENAME
                parsedOK = True

            Case DesignerForm.VariableTypes.INTEGER
                parsedOK = True

            Case DesignerForm.VariableTypes.NUMBER_STRING
                parsedOK = True

            Case DesignerForm.VariableTypes.TIME
                If myValue.Length = 8 Then
                    parsedOK = True
                End If

                If Not parsedOK Then MsgBox("Error: Time value must be in HH:MM:SS format", MsgBoxStyle.OkOnly, "Edit Variable check")
        End Select

        If parsedOK Then DialogResult = Windows.Forms.DialogResult.OK
    End Sub

    Public Sub SetVarTypeComboBox(ByRef p_value As String)
        With varTypeComboBox
            If p_value = "" Then
                .SelectedIndex = -1
            Else
                .SelectedItem = p_value
            End If
        End With
    End Sub

    Private Sub clearButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles clearButton.Click
        varValueTextBox.Clear()
    End Sub
End Class