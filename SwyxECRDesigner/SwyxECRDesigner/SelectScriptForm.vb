Public Class SelectScriptForm

    Private Sub CheckBox1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox1.CheckedChanged
        ComboBox1.Enabled = Not CheckBox1.Checked
    End Sub
End Class