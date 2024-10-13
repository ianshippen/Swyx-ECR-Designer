<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ExportForm
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ExportForm))
        Me.scriptNameComboBox = New System.Windows.Forms.ComboBox
        Me.okButton = New System.Windows.Forms.Button
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.infoTextBox = New System.Windows.Forms.TextBox
        Me.cancelButton = New System.Windows.Forms.Button
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'scriptNameComboBox
        '
        Me.scriptNameComboBox.FormattingEnabled = True
        Me.scriptNameComboBox.Location = New System.Drawing.Point(6, 19)
        Me.scriptNameComboBox.Name = "scriptNameComboBox"
        Me.scriptNameComboBox.Size = New System.Drawing.Size(222, 21)
        Me.scriptNameComboBox.TabIndex = 0
        '
        'okButton
        '
        Me.okButton.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.okButton.Location = New System.Drawing.Point(174, 217)
        Me.okButton.Name = "okButton"
        Me.okButton.Size = New System.Drawing.Size(75, 23)
        Me.okButton.TabIndex = 2
        Me.okButton.Text = "OK"
        Me.okButton.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.infoTextBox)
        Me.GroupBox1.Controls.Add(Me.scriptNameComboBox)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 22)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(237, 179)
        Me.GroupBox1.TabIndex = 3
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = " Export to database as .. "
        '
        'infoTextBox
        '
        Me.infoTextBox.Location = New System.Drawing.Point(6, 56)
        Me.infoTextBox.Multiline = True
        Me.infoTextBox.Name = "infoTextBox"
        Me.infoTextBox.ReadOnly = True
        Me.infoTextBox.Size = New System.Drawing.Size(222, 117)
        Me.infoTextBox.TabIndex = 1
        '
        'cancelButton
        '
        Me.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.cancelButton.Location = New System.Drawing.Point(83, 217)
        Me.cancelButton.Name = "cancelButton"
        Me.cancelButton.Size = New System.Drawing.Size(75, 23)
        Me.cancelButton.TabIndex = 4
        Me.cancelButton.Text = "Cancel"
        Me.cancelButton.UseVisualStyleBackColor = True
        '
        'ExportForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(271, 266)
        Me.Controls.Add(Me.cancelButton)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.okButton)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "ExportForm"
        Me.Text = "Export Properties"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents scriptNameComboBox As System.Windows.Forms.ComboBox
    Friend WithEvents okButton As System.Windows.Forms.Button
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents infoTextBox As System.Windows.Forms.TextBox
    Friend WithEvents cancelButton As System.Windows.Forms.Button
End Class
