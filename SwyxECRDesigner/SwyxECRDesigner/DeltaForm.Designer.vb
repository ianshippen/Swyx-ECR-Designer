<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class DeltaForm
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(DeltaForm))
        Me.fileContentsLabel = New System.Windows.Forms.Label
        Me.databaseContentsLabel = New System.Windows.Forms.Label
        Me.okButton = New System.Windows.Forms.Button
        Me.cancelButton = New System.Windows.Forms.Button
        Me.fileTextBox = New System.Windows.Forms.RichTextBox
        Me.databaseTextBox = New System.Windows.Forms.RichTextBox
        Me.SuspendLayout()
        '
        'fileContentsLabel
        '
        Me.fileContentsLabel.AutoSize = True
        Me.fileContentsLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fileContentsLabel.Location = New System.Drawing.Point(55, 28)
        Me.fileContentsLabel.Name = "fileContentsLabel"
        Me.fileContentsLabel.Size = New System.Drawing.Size(116, 20)
        Me.fileContentsLabel.TabIndex = 4
        Me.fileContentsLabel.Text = "File Contents"
        '
        'databaseContentsLabel
        '
        Me.databaseContentsLabel.AutoSize = True
        Me.databaseContentsLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.databaseContentsLabel.Location = New System.Drawing.Point(772, 28)
        Me.databaseContentsLabel.Name = "databaseContentsLabel"
        Me.databaseContentsLabel.Size = New System.Drawing.Size(165, 20)
        Me.databaseContentsLabel.TabIndex = 5
        Me.databaseContentsLabel.Text = "Database Contents"
        '
        'okButton
        '
        Me.okButton.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.okButton.Location = New System.Drawing.Point(1377, 693)
        Me.okButton.Name = "okButton"
        Me.okButton.Size = New System.Drawing.Size(75, 23)
        Me.okButton.TabIndex = 0
        Me.okButton.Text = "OK"
        Me.okButton.UseVisualStyleBackColor = True
        '
        'cancelButton
        '
        Me.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.cancelButton.Location = New System.Drawing.Point(1280, 693)
        Me.cancelButton.Name = "cancelButton"
        Me.cancelButton.Size = New System.Drawing.Size(75, 23)
        Me.cancelButton.TabIndex = 1
        Me.cancelButton.Text = "Cancel"
        Me.cancelButton.UseVisualStyleBackColor = True
        '
        'fileTextBox
        '
        Me.fileTextBox.Font = New System.Drawing.Font("Lucida Console", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fileTextBox.Location = New System.Drawing.Point(58, 48)
        Me.fileTextBox.Name = "fileTextBox"
        Me.fileTextBox.Size = New System.Drawing.Size(677, 621)
        Me.fileTextBox.TabIndex = 6
        Me.fileTextBox.Text = ""
        Me.fileTextBox.WordWrap = False
        '
        'databaseTextBox
        '
        Me.databaseTextBox.Font = New System.Drawing.Font("Lucida Console", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.databaseTextBox.Location = New System.Drawing.Point(775, 48)
        Me.databaseTextBox.Name = "databaseTextBox"
        Me.databaseTextBox.Size = New System.Drawing.Size(677, 621)
        Me.databaseTextBox.TabIndex = 7
        Me.databaseTextBox.Text = ""
        Me.databaseTextBox.WordWrap = False
        '
        'DeltaForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1479, 746)
        Me.Controls.Add(Me.databaseTextBox)
        Me.Controls.Add(Me.fileTextBox)
        Me.Controls.Add(Me.cancelButton)
        Me.Controls.Add(Me.okButton)
        Me.Controls.Add(Me.databaseContentsLabel)
        Me.Controls.Add(Me.fileContentsLabel)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "DeltaForm"
        Me.Text = "Delta"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents fileContentsLabel As System.Windows.Forms.Label
    Friend WithEvents databaseContentsLabel As System.Windows.Forms.Label
    Friend WithEvents okButton As System.Windows.Forms.Button
    Friend WithEvents cancelButton As System.Windows.Forms.Button
    Friend WithEvents fileTextBox As System.Windows.Forms.RichTextBox
    Friend WithEvents databaseTextBox As System.Windows.Forms.RichTextBox
End Class
