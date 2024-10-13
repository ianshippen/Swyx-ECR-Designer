<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ViewCodeForm
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ViewCodeForm))
        Me.viewCodeTextBox = New System.Windows.Forms.TextBox
        Me.SuspendLayout()
        '
        'viewCodeTextBox
        '
        Me.viewCodeTextBox.Font = New System.Drawing.Font("Lucida Console", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.viewCodeTextBox.ForeColor = System.Drawing.Color.DodgerBlue
        Me.viewCodeTextBox.Location = New System.Drawing.Point(31, 39)
        Me.viewCodeTextBox.Multiline = True
        Me.viewCodeTextBox.Name = "viewCodeTextBox"
        Me.viewCodeTextBox.ReadOnly = True
        Me.viewCodeTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.viewCodeTextBox.Size = New System.Drawing.Size(756, 453)
        Me.viewCodeTextBox.TabIndex = 0
        '
        'ViewCodeForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(821, 559)
        Me.Controls.Add(Me.viewCodeTextBox)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "ViewCodeForm"
        Me.Text = "View Code"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents viewCodeTextBox As System.Windows.Forms.TextBox
End Class
