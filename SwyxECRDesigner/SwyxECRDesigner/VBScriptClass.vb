Public Class VBScriptClass
    Inherits SIBBClass

    Public linesOfCode As New List(Of String)

    Public Sub New(ByRef p_bitmap As Bitmap)
        MyBase.New(SIBBTypeClass.SIBBTYPE.VBSCRIPT, p_bitmap)
    End Sub

    Public Overrides Sub PackData()
        PackData("")

        For i = 0 To linesOfCode.Count - 1
            PackedDataAddVbCrLf(linesOfCode(i))
        Next
    End Sub

    Public Overrides Sub UnpackData()
        Dim myList As New List(Of String)

        UnpackDataVbCrLf(myList)
        linesOfCode.Clear()

        For i = 0 To myList.Count - 1
            linesOfCode.Add(myList(i))
        Next
    End Sub

    Public Overrides Sub SetupForm(ByRef p_tabPage As System.Windows.Forms.TabPage)
        MyBase.SetupForm(p_tabPage)

        Dim x As New TextBox

        x.Multiline = True
        x.Size = New Point(p_tabPage.Size.Width - 10, p_tabPage.Height - 10)
        x.ScrollBars = ScrollBars.Both
        x.WordWrap = False
        x.Font = vbscriptFont
        x.ForeColor = vbscriptFontColour
        x.AcceptsTab = True

        For i = 0 To linesOfCode.Count - 1
            If i > 0 Then x.AppendText(vbCrLf)

            'If linesOfCode(i).Contains("'") Then
            'DesignerForm.AddLineSpecialForVBSComment(linesOfCode(i), Color.Red, x.BackColor, x)
            'Else
            x.AppendText(linesOfCode(i))
            'End If
        Next

        AddHandler x.DoubleClick, AddressOf MyBaseForm.textbox_DoubleClick
        p_tabPage.Controls.Add(x)
    End Sub

    Public Overrides Sub TakedownForm(ByRef p_p_tabPage As System.Windows.Forms.TabPage)
        Dim myTextBox As TextBox = p_p_tabPage.Controls(0)
        Dim myText As String = myTextBox.Text
        Dim myArray As String() = Nothing

        'If myText.Contains(vbCr) Then
        myArray = System.Text.RegularExpressions.Regex.Split(myText, vbCrLf)
        'Else
        'myArray = System.Text.RegularExpressions.Regex.Split(myText, vbLf)
        'End If

        linesOfCode.Clear()

        For i = 0 To myArray.Length - 1
            linesOfCode.Add(myArray(i))
        Next

        MyBase.TakedownForm(p_p_tabPage)
    End Sub
End Class
