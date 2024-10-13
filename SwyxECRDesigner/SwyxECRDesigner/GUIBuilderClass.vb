Public Class GUIBuilderClass
    Public absolutelocation As New AbsoluteLocationClass
    Private parent As TabPage

    Public Sub New(ByRef p_tabPage As TabPage)
        parent = p_tabPage
        absolutelocation.Set(40, 40)
    End Sub

    Public Sub AddTextBoxLabelPair(ByRef p_text As String, ByRef p_label As String, Optional ByVal p_dropDownOnly As Boolean = False, Optional ByRef p_dropDownList As List(Of String) = Nothing)
        Dim x As New ComboBox
        Dim y As New Label

        x.Location = New Point(absolutelocation.GetX, absolutelocation.GetY)
        x.Size = New Point(180, 20)
        x.Text = p_text
        x.Tag = "Validate"

        If p_dropDownList IsNot Nothing Then
            For Each item As String In p_dropDownList
                x.Items.Add(item)
            Next
        End If

        ' Add all the declared variables
        With VariablesForm
            .SortServiceWideVariablesByName()

            For i = 0 To DesignerForm.GetNumberOfServiceWideVariables - 1
                Dim myName As String = DesignerForm.GetServiceWideVariableName(i)

                If myName <> "" Then x.Items.Add(myName)
            Next
        End With

        If p_dropDownOnly Then
            x.DropDownStyle = ComboBoxStyle.DropDownList
            x.SelectedItem = p_text
        End If

        y.Location = New Point(absolutelocation.GetX + 190, absolutelocation.GetY + 3)
        y.Size = New Point(180, y.Size.Height)
        y.Text = p_label
        y.Tag = ""

        parent.Controls.Add(x)
        parent.Controls.Add(y)

        absolutelocation.Add(0, 40)
    End Sub

    Public Sub AddCheckBoxLabelPair(ByRef p_value As Boolean, ByRef p_label As String)
        Dim x As New CheckBox

        x.Location = New Point(absolutelocation.GetX, absolutelocation.GetY)
        x.Size = New Point(180, 20)
        x.Text = p_label
        x.Checked = p_value

        parent.Controls.Add(x)

        absolutelocation.Add(0, 40)
    End Sub

    Public Sub AddEditTextBoxLabelPair(ByRef p_value As String, ByRef p_label As String)
        Dim x As New TextBox
        Dim y As New Label

        x.Multiline = True
        x.WordWrap = False
        x.ScrollBars = ScrollBars.Both
        x.Location = New Point(absolutelocation.GetX, absolutelocation.GetY)
        x.Size = New Point(350, 300)
        x.Text = p_value

        parent.Controls.Add(x)

        y.Location = New Point(absolutelocation.GetX, absolutelocation.GetY - 16)
        y.Text = p_label

        parent.Controls.Add(y)
        absolutelocation.Add(0, 210)
    End Sub
End Class
