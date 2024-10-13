<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class TODDOWSIBBForm
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(TODDOWSIBBForm))
        Me.myTree = New System.Windows.Forms.TreeView
        Me.SuspendLayout()
        '
        'myTree
        '
        Me.myTree.Font = New System.Drawing.Font("Lucida Console", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.myTree.ForeColor = System.Drawing.Color.Red
        Me.myTree.Location = New System.Drawing.Point(12, 23)
        Me.myTree.Name = "myTree"
        Me.myTree.Size = New System.Drawing.Size(467, 429)
        Me.myTree.TabIndex = 0
        '
        'TODDOWSIBBForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(491, 464)
        Me.Controls.Add(Me.myTree)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "TODDOWSIBBForm"
        Me.Text = "TODDOW Properties"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents myTree As System.Windows.Forms.TreeView
End Class
