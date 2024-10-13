<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class RestoreScriptsForm
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(RestoreScriptsForm))
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.functionNameListBox = New System.Windows.Forms.ListBox
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.sibbNeedsHistoryListBox = New System.Windows.Forms.ListBox
        Me.clearSelectionsButton = New System.Windows.Forms.Button
        Me.CheckBox1 = New System.Windows.Forms.CheckBox
        Me.okButton = New System.Windows.Forms.Button
        Me.cancelButton = New System.Windows.Forms.Button
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.functionNameListBox)
        Me.GroupBox1.Location = New System.Drawing.Point(22, 24)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(498, 232)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = " Function Name "
        '
        'functionNameListBox
        '
        Me.functionNameListBox.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.functionNameListBox.ForeColor = System.Drawing.Color.Blue
        Me.functionNameListBox.FormattingEnabled = True
        Me.functionNameListBox.HorizontalScrollbar = True
        Me.functionNameListBox.ItemHeight = 16
        Me.functionNameListBox.Location = New System.Drawing.Point(18, 28)
        Me.functionNameListBox.Name = "functionNameListBox"
        Me.functionNameListBox.Size = New System.Drawing.Size(461, 180)
        Me.functionNameListBox.TabIndex = 0
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.CheckBox1)
        Me.GroupBox2.Controls.Add(Me.sibbNeedsHistoryListBox)
        Me.GroupBox2.Location = New System.Drawing.Point(548, 24)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(284, 232)
        Me.GroupBox2.TabIndex = 1
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = " SIBB Needs History "
        '
        'sibbNeedsHistoryListBox
        '
        Me.sibbNeedsHistoryListBox.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.sibbNeedsHistoryListBox.ForeColor = System.Drawing.Color.Red
        Me.sibbNeedsHistoryListBox.FormattingEnabled = True
        Me.sibbNeedsHistoryListBox.ItemHeight = 16
        Me.sibbNeedsHistoryListBox.Location = New System.Drawing.Point(18, 28)
        Me.sibbNeedsHistoryListBox.Name = "sibbNeedsHistoryListBox"
        Me.sibbNeedsHistoryListBox.Size = New System.Drawing.Size(252, 164)
        Me.sibbNeedsHistoryListBox.TabIndex = 0
        '
        'clearSelectionsButton
        '
        Me.clearSelectionsButton.Location = New System.Drawing.Point(22, 279)
        Me.clearSelectionsButton.Name = "clearSelectionsButton"
        Me.clearSelectionsButton.Size = New System.Drawing.Size(99, 23)
        Me.clearSelectionsButton.TabIndex = 2
        Me.clearSelectionsButton.Text = "Clear Selections"
        Me.clearSelectionsButton.UseVisualStyleBackColor = True
        '
        'CheckBox1
        '
        Me.CheckBox1.AutoSize = True
        Me.CheckBox1.Location = New System.Drawing.Point(18, 200)
        Me.CheckBox1.Name = "CheckBox1"
        Me.CheckBox1.Size = New System.Drawing.Size(259, 17)
        Me.CheckBox1.TabIndex = 1
        Me.CheckBox1.Text = "Only show history since selected function change"
        Me.CheckBox1.UseVisualStyleBackColor = True
        '
        'okButton
        '
        Me.okButton.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.okButton.Location = New System.Drawing.Point(757, 279)
        Me.okButton.Name = "okButton"
        Me.okButton.Size = New System.Drawing.Size(75, 23)
        Me.okButton.TabIndex = 3
        Me.okButton.Text = "Restore"
        Me.okButton.UseVisualStyleBackColor = True
        '
        'cancelButton
        '
        Me.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.cancelButton.Location = New System.Drawing.Point(651, 279)
        Me.cancelButton.Name = "cancelButton"
        Me.cancelButton.Size = New System.Drawing.Size(75, 23)
        Me.cancelButton.TabIndex = 4
        Me.cancelButton.Text = "Cancel"
        Me.cancelButton.UseVisualStyleBackColor = True
        '
        'RestoreScriptsForm
        '
        Me.AcceptButton = Me.cancelButton
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.cancelButton = Me.cancelButton
        Me.ClientSize = New System.Drawing.Size(844, 322)
        Me.Controls.Add(Me.cancelButton)
        Me.Controls.Add(Me.okButton)
        Me.Controls.Add(Me.clearSelectionsButton)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "RestoreScriptsForm"
        Me.Text = "Restore Scripts"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents functionNameListBox As System.Windows.Forms.ListBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents sibbNeedsHistoryListBox As System.Windows.Forms.ListBox
    Friend WithEvents clearSelectionsButton As System.Windows.Forms.Button
    Friend WithEvents CheckBox1 As System.Windows.Forms.CheckBox
    Friend WithEvents okButton As System.Windows.Forms.Button
    Friend WithEvents cancelButton As System.Windows.Forms.Button
End Class
