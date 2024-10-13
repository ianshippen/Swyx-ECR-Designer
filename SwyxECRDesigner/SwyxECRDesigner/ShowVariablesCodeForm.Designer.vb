<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ShowVariablesCodeForm
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ShowVariablesCodeForm))
        Me.showVariablesCodeRichTextBox = New System.Windows.Forms.RichTextBox
        Me.okButton = New System.Windows.Forms.Button
        Me.cancelButton = New System.Windows.Forms.Button
        Me.SuspendLayout()
        '
        'showVariablesCodeRichTextBox
        '
        Me.showVariablesCodeRichTextBox.Font = New System.Drawing.Font("Lucida Console", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.showVariablesCodeRichTextBox.Location = New System.Drawing.Point(12, 12)
        Me.showVariablesCodeRichTextBox.Name = "showVariablesCodeRichTextBox"
        Me.showVariablesCodeRichTextBox.Size = New System.Drawing.Size(1058, 495)
        Me.showVariablesCodeRichTextBox.TabIndex = 0
        Me.showVariablesCodeRichTextBox.Text = ""
        '
        'okButton
        '
        Me.okButton.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.okButton.Location = New System.Drawing.Point(995, 525)
        Me.okButton.Name = "okButton"
        Me.okButton.Size = New System.Drawing.Size(75, 23)
        Me.okButton.TabIndex = 1
        Me.okButton.Text = "OK"
        Me.okButton.UseVisualStyleBackColor = True
        '
        'cancelButton
        '
        Me.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.cancelButton.Location = New System.Drawing.Point(903, 525)
        Me.cancelButton.Name = "cancelButton"
        Me.cancelButton.Size = New System.Drawing.Size(75, 23)
        Me.cancelButton.TabIndex = 2
        Me.cancelButton.Text = "Cancel"
        Me.cancelButton.UseVisualStyleBackColor = True
        '
        'ShowVariablesCodeForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1082, 560)
        Me.Controls.Add(Me.cancelButton)
        Me.Controls.Add(Me.okButton)
        Me.Controls.Add(Me.showVariablesCodeRichTextBox)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "ShowVariablesCodeForm"
        Me.Text = "Show Start Block Code"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents showVariablesCodeRichTextBox As System.Windows.Forms.RichTextBox
    Friend WithEvents okButton As System.Windows.Forms.Button
    Friend Shadows WithEvents cancelButton As System.Windows.Forms.Button
End Class
