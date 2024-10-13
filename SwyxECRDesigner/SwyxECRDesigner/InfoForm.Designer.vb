<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class InfoForm
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(InfoForm))
        Me.infoLabel = New System.Windows.Forms.Label
        Me.showIndexCheckBox = New System.Windows.Forms.CheckBox
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.searchNodesButton = New System.Windows.Forms.Button
        Me.searchTextBox = New System.Windows.Forms.TextBox
        Me.fileLabel = New System.Windows.Forms.Label
        Me.pathLabel = New System.Windows.Forms.Label
        Me.fileTextBox = New System.Windows.Forms.TextBox
        Me.pathTextBox = New System.Windows.Forms.TextBox
        Me.usedByTextBox = New System.Windows.Forms.TextBox
        Me.GroupBox3 = New System.Windows.Forms.GroupBox
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.SuspendLayout()
        '
        'infoLabel
        '
        Me.infoLabel.AutoSize = True
        Me.infoLabel.Location = New System.Drawing.Point(322, 98)
        Me.infoLabel.Name = "infoLabel"
        Me.infoLabel.Size = New System.Drawing.Size(24, 13)
        Me.infoLabel.TabIndex = 0
        Me.infoLabel.Text = "text"
        '
        'showIndexCheckBox
        '
        Me.showIndexCheckBox.AutoSize = True
        Me.showIndexCheckBox.Location = New System.Drawing.Point(325, 17)
        Me.showIndexCheckBox.Name = "showIndexCheckBox"
        Me.showIndexCheckBox.Size = New System.Drawing.Size(116, 17)
        Me.showIndexCheckBox.TabIndex = 1
        Me.showIndexCheckBox.Text = "Show node indices"
        Me.showIndexCheckBox.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.searchNodesButton)
        Me.GroupBox1.Controls.Add(Me.searchTextBox)
        Me.GroupBox1.Location = New System.Drawing.Point(325, 125)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(265, 94)
        Me.GroupBox1.TabIndex = 2
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = " Search Nodes "
        '
        'searchNodesButton
        '
        Me.searchNodesButton.Location = New System.Drawing.Point(194, 54)
        Me.searchNodesButton.Name = "searchNodesButton"
        Me.searchNodesButton.Size = New System.Drawing.Size(65, 23)
        Me.searchNodesButton.TabIndex = 1
        Me.searchNodesButton.Text = "Search ..."
        Me.searchNodesButton.UseVisualStyleBackColor = True
        '
        'searchTextBox
        '
        Me.searchTextBox.Location = New System.Drawing.Point(13, 19)
        Me.searchTextBox.Name = "searchTextBox"
        Me.searchTextBox.Size = New System.Drawing.Size(246, 20)
        Me.searchTextBox.TabIndex = 0
        '
        'fileLabel
        '
        Me.fileLabel.AutoSize = True
        Me.fileLabel.Location = New System.Drawing.Point(18, 18)
        Me.fileLabel.Name = "fileLabel"
        Me.fileLabel.Size = New System.Drawing.Size(23, 13)
        Me.fileLabel.TabIndex = 4
        Me.fileLabel.Text = "File"
        '
        'pathLabel
        '
        Me.pathLabel.AutoSize = True
        Me.pathLabel.Location = New System.Drawing.Point(12, 51)
        Me.pathLabel.Name = "pathLabel"
        Me.pathLabel.Size = New System.Drawing.Size(29, 13)
        Me.pathLabel.TabIndex = 5
        Me.pathLabel.Text = "Path"
        '
        'fileTextBox
        '
        Me.fileTextBox.Location = New System.Drawing.Point(41, 15)
        Me.fileTextBox.Name = "fileTextBox"
        Me.fileTextBox.ReadOnly = True
        Me.fileTextBox.Size = New System.Drawing.Size(239, 20)
        Me.fileTextBox.TabIndex = 6
        '
        'pathTextBox
        '
        Me.pathTextBox.Location = New System.Drawing.Point(41, 48)
        Me.pathTextBox.Name = "pathTextBox"
        Me.pathTextBox.ReadOnly = True
        Me.pathTextBox.Size = New System.Drawing.Size(543, 20)
        Me.pathTextBox.TabIndex = 7
        '
        'usedByTextBox
        '
        Me.usedByTextBox.Location = New System.Drawing.Point(16, 21)
        Me.usedByTextBox.Multiline = True
        Me.usedByTextBox.Name = "usedByTextBox"
        Me.usedByTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.usedByTextBox.Size = New System.Drawing.Size(233, 201)
        Me.usedByTextBox.TabIndex = 8
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.usedByTextBox)
        Me.GroupBox3.Location = New System.Drawing.Point(15, 98)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(265, 238)
        Me.GroupBox3.TabIndex = 8
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = " Used By "
        '
        'InfoForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(607, 348)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.pathTextBox)
        Me.Controls.Add(Me.fileTextBox)
        Me.Controls.Add(Me.pathLabel)
        Me.Controls.Add(Me.fileLabel)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.showIndexCheckBox)
        Me.Controls.Add(Me.infoLabel)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "InfoForm"
        Me.Text = "Info"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents infoLabel As System.Windows.Forms.Label
    Friend WithEvents showIndexCheckBox As System.Windows.Forms.CheckBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents searchTextBox As System.Windows.Forms.TextBox
    Friend WithEvents searchNodesButton As System.Windows.Forms.Button
    Friend WithEvents fileLabel As System.Windows.Forms.Label
    Friend WithEvents pathLabel As System.Windows.Forms.Label
    Friend WithEvents fileTextBox As System.Windows.Forms.TextBox
    Friend WithEvents pathTextBox As System.Windows.Forms.TextBox
    Friend WithEvents usedByTextBox As System.Windows.Forms.TextBox
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
End Class
