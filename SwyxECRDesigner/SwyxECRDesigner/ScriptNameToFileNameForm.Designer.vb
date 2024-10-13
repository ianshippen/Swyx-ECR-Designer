<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ScriptNameToFileNameForm
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ScriptNameToFileNameForm))
        Me.scriptNameComboBox = New System.Windows.Forms.ComboBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.filenameTextBox = New System.Windows.Forms.TextBox
        Me.okButton = New System.Windows.Forms.Button
        Me.loadButton = New System.Windows.Forms.Button
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'scriptNameComboBox
        '
        Me.scriptNameComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.scriptNameComboBox.FormattingEnabled = True
        Me.scriptNameComboBox.Location = New System.Drawing.Point(28, 33)
        Me.scriptNameComboBox.Name = "scriptNameComboBox"
        Me.scriptNameComboBox.Size = New System.Drawing.Size(232, 21)
        Me.scriptNameComboBox.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(25, 17)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(65, 13)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Script Name"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.filenameTextBox)
        Me.GroupBox1.Location = New System.Drawing.Point(28, 76)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(733, 70)
        Me.GroupBox1.TabIndex = 2
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = " Source File Name "
        '
        'filenameTextBox
        '
        Me.filenameTextBox.Location = New System.Drawing.Point(17, 28)
        Me.filenameTextBox.Name = "filenameTextBox"
        Me.filenameTextBox.Size = New System.Drawing.Size(698, 20)
        Me.filenameTextBox.TabIndex = 0
        '
        'okButton
        '
        Me.okButton.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.okButton.Location = New System.Drawing.Point(686, 166)
        Me.okButton.Name = "okButton"
        Me.okButton.Size = New System.Drawing.Size(75, 23)
        Me.okButton.TabIndex = 3
        Me.okButton.Text = "OK"
        Me.okButton.UseVisualStyleBackColor = True
        '
        'loadButton
        '
        Me.loadButton.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.loadButton.Location = New System.Drawing.Point(585, 166)
        Me.loadButton.Name = "loadButton"
        Me.loadButton.Size = New System.Drawing.Size(75, 23)
        Me.loadButton.TabIndex = 4
        Me.loadButton.Text = "Load"
        Me.loadButton.UseVisualStyleBackColor = True
        '
        'ScriptNameToFileNameForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(777, 205)
        Me.Controls.Add(Me.loadButton)
        Me.Controls.Add(Me.okButton)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.scriptNameComboBox)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "ScriptNameToFileNameForm"
        Me.Text = "Script Name To File Name"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents scriptNameComboBox As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents filenameTextBox As System.Windows.Forms.TextBox
    Friend WithEvents okButton As System.Windows.Forms.Button
    Friend WithEvents loadButton As System.Windows.Forms.Button
End Class
