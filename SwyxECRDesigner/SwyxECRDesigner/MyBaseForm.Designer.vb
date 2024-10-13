<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MyBaseForm
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(MyBaseForm))
        Me.TabControl1 = New System.Windows.Forms.TabControl
        Me.TabPage1 = New System.Windows.Forms.TabPage
        Me.GroupBox5 = New System.Windows.Forms.GroupBox
        Me.hideInCallTraceCheckBox = New System.Windows.Forms.CheckBox
        Me.GroupBox4 = New System.Windows.Forms.GroupBox
        Me.internalReferenceTextBox = New System.Windows.Forms.TextBox
        Me.GroupBox3 = New System.Windows.Forms.GroupBox
        Me.nodeNumberLabel = New System.Windows.Forms.Label
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.nodeTypeLabel = New System.Windows.Forms.Label
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.titleTextBox = New System.Windows.Forms.TextBox
        Me.TabPage2 = New System.Windows.Forms.TabPage
        Me.TabPage3 = New System.Windows.Forms.TabPage
        Me.nodePropertiesDataGridView = New System.Windows.Forms.DataGridView
        Me.Visible = New System.Windows.Forms.DataGridViewCheckBoxColumn
        Me.FixedName = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.LinkName = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.LinkedTo = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.order = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.TabPage4 = New System.Windows.Forms.TabPage
        Me.notesRichTextBox = New System.Windows.Forms.RichTextBox
        Me.okButton = New System.Windows.Forms.Button
        Me.cancelButton = New System.Windows.Forms.Button
        Me.fromNodesLabel = New System.Windows.Forms.Label
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.GroupBox5.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.TabPage3.SuspendLayout()
        CType(Me.nodePropertiesDataGridView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage4.SuspendLayout()
        Me.SuspendLayout()
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Controls.Add(Me.TabPage3)
        Me.TabControl1.Controls.Add(Me.TabPage4)
        Me.TabControl1.Location = New System.Drawing.Point(20, 25)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(423, 407)
        Me.TabControl1.TabIndex = 0
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.GroupBox5)
        Me.TabPage1.Controls.Add(Me.GroupBox4)
        Me.TabPage1.Controls.Add(Me.GroupBox3)
        Me.TabPage1.Controls.Add(Me.GroupBox2)
        Me.TabPage1.Controls.Add(Me.GroupBox1)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(415, 381)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "General"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'GroupBox5
        '
        Me.GroupBox5.Controls.Add(Me.hideInCallTraceCheckBox)
        Me.GroupBox5.Location = New System.Drawing.Point(18, 221)
        Me.GroupBox5.Name = "GroupBox5"
        Me.GroupBox5.Size = New System.Drawing.Size(380, 45)
        Me.GroupBox5.TabIndex = 4
        Me.GroupBox5.TabStop = False
        Me.GroupBox5.Text = " Options "
        '
        'hideInCallTraceCheckBox
        '
        Me.hideInCallTraceCheckBox.AutoSize = True
        Me.hideInCallTraceCheckBox.Location = New System.Drawing.Point(16, 17)
        Me.hideInCallTraceCheckBox.Name = "hideInCallTraceCheckBox"
        Me.hideInCallTraceCheckBox.Size = New System.Drawing.Size(111, 17)
        Me.hideInCallTraceCheckBox.TabIndex = 0
        Me.hideInCallTraceCheckBox.Text = "Hide In Call Trace"
        Me.hideInCallTraceCheckBox.UseVisualStyleBackColor = True
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.internalReferenceTextBox)
        Me.GroupBox4.Location = New System.Drawing.Point(18, 128)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(380, 75)
        Me.GroupBox4.TabIndex = 2
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = " Internal Reference Name "
        '
        'internalReferenceTextBox
        '
        Me.internalReferenceTextBox.Location = New System.Drawing.Point(16, 30)
        Me.internalReferenceTextBox.Name = "internalReferenceTextBox"
        Me.internalReferenceTextBox.Size = New System.Drawing.Size(343, 20)
        Me.internalReferenceTextBox.TabIndex = 0
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.nodeNumberLabel)
        Me.GroupBox3.Location = New System.Drawing.Point(308, 282)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(90, 75)
        Me.GroupBox3.TabIndex = 3
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = " Node Number "
        '
        'nodeNumberLabel
        '
        Me.nodeNumberLabel.AutoSize = True
        Me.nodeNumberLabel.Location = New System.Drawing.Point(27, 31)
        Me.nodeNumberLabel.Name = "nodeNumberLabel"
        Me.nodeNumberLabel.Size = New System.Drawing.Size(39, 13)
        Me.nodeNumberLabel.TabIndex = 0
        Me.nodeNumberLabel.Text = "Label2"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.nodeTypeLabel)
        Me.GroupBox2.Location = New System.Drawing.Point(18, 282)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(267, 75)
        Me.GroupBox2.TabIndex = 2
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = " Type "
        '
        'nodeTypeLabel
        '
        Me.nodeTypeLabel.AutoSize = True
        Me.nodeTypeLabel.Location = New System.Drawing.Point(27, 31)
        Me.nodeTypeLabel.Name = "nodeTypeLabel"
        Me.nodeTypeLabel.Size = New System.Drawing.Size(39, 13)
        Me.nodeTypeLabel.TabIndex = 0
        Me.nodeTypeLabel.Text = "Label1"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.titleTextBox)
        Me.GroupBox1.Location = New System.Drawing.Point(18, 22)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(380, 75)
        Me.GroupBox1.TabIndex = 1
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = " Title "
        '
        'titleTextBox
        '
        Me.titleTextBox.Location = New System.Drawing.Point(16, 30)
        Me.titleTextBox.Name = "titleTextBox"
        Me.titleTextBox.Size = New System.Drawing.Size(343, 20)
        Me.titleTextBox.TabIndex = 0
        '
        'TabPage2
        '
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(415, 381)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Parameters"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'TabPage3
        '
        Me.TabPage3.Controls.Add(Me.fromNodesLabel)
        Me.TabPage3.Controls.Add(Me.nodePropertiesDataGridView)
        Me.TabPage3.Location = New System.Drawing.Point(4, 22)
        Me.TabPage3.Name = "TabPage3"
        Me.TabPage3.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage3.Size = New System.Drawing.Size(415, 381)
        Me.TabPage3.TabIndex = 2
        Me.TabPage3.Text = "Links"
        Me.TabPage3.UseVisualStyleBackColor = True
        '
        'nodePropertiesDataGridView
        '
        Me.nodePropertiesDataGridView.AllowUserToAddRows = False
        Me.nodePropertiesDataGridView.AllowUserToDeleteRows = False
        Me.nodePropertiesDataGridView.AllowUserToResizeColumns = False
        Me.nodePropertiesDataGridView.AllowUserToResizeRows = False
        Me.nodePropertiesDataGridView.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells
        Me.nodePropertiesDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.nodePropertiesDataGridView.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Visible, Me.FixedName, Me.LinkName, Me.LinkedTo, Me.order})
        Me.nodePropertiesDataGridView.Location = New System.Drawing.Point(3, 29)
        Me.nodePropertiesDataGridView.Name = "nodePropertiesDataGridView"
        Me.nodePropertiesDataGridView.RowHeadersVisible = False
        Me.nodePropertiesDataGridView.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.nodePropertiesDataGridView.Size = New System.Drawing.Size(406, 136)
        Me.nodePropertiesDataGridView.TabIndex = 0
        '
        'Visible
        '
        Me.Visible.HeaderText = "Visible"
        Me.Visible.Name = "Visible"
        Me.Visible.Width = 60
        '
        'FixedName
        '
        Me.FixedName.HeaderText = "Fixed Name"
        Me.FixedName.Name = "FixedName"
        Me.FixedName.ReadOnly = True
        '
        'LinkName
        '
        Me.LinkName.HeaderText = "User Link Tag"
        Me.LinkName.Name = "LinkName"
        '
        'LinkedTo
        '
        Me.LinkedTo.HeaderText = "Linked To"
        Me.LinkedTo.Name = "LinkedTo"
        Me.LinkedTo.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.LinkedTo.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'order
        '
        Me.order.HeaderText = "Order"
        Me.order.Name = "order"
        Me.order.Width = 50
        '
        'TabPage4
        '
        Me.TabPage4.Controls.Add(Me.notesRichTextBox)
        Me.TabPage4.Location = New System.Drawing.Point(4, 22)
        Me.TabPage4.Name = "TabPage4"
        Me.TabPage4.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage4.Size = New System.Drawing.Size(415, 381)
        Me.TabPage4.TabIndex = 3
        Me.TabPage4.Text = "Notes"
        Me.TabPage4.UseVisualStyleBackColor = True
        '
        'notesRichTextBox
        '
        Me.notesRichTextBox.Font = New System.Drawing.Font("Lucida Console", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.notesRichTextBox.Location = New System.Drawing.Point(18, 24)
        Me.notesRichTextBox.Name = "notesRichTextBox"
        Me.notesRichTextBox.Size = New System.Drawing.Size(379, 332)
        Me.notesRichTextBox.TabIndex = 0
        Me.notesRichTextBox.Text = ""
        '
        'okButton
        '
        Me.okButton.Location = New System.Drawing.Point(368, 467)
        Me.okButton.Name = "okButton"
        Me.okButton.Size = New System.Drawing.Size(75, 23)
        Me.okButton.TabIndex = 1
        Me.okButton.Text = "OK"
        Me.okButton.UseVisualStyleBackColor = True
        '
        'cancelButton
        '
        Me.cancelButton.Location = New System.Drawing.Point(252, 467)
        Me.cancelButton.Name = "cancelButton"
        Me.cancelButton.Size = New System.Drawing.Size(75, 23)
        Me.cancelButton.TabIndex = 2
        Me.cancelButton.Text = "Cancel"
        Me.cancelButton.UseVisualStyleBackColor = True
        '
        'fromNodesLabel
        '
        Me.fromNodesLabel.AutoSize = True
        Me.fromNodesLabel.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fromNodesLabel.ForeColor = System.Drawing.Color.Red
        Me.fromNodesLabel.Location = New System.Drawing.Point(6, 351)
        Me.fromNodesLabel.Name = "fromNodesLabel"
        Me.fromNodesLabel.Size = New System.Drawing.Size(46, 16)
        Me.fromNodesLabel.TabIndex = 1
        Me.fromNodesLabel.Text = "Label1"
        '
        'MyBaseForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(460, 520)
        Me.ControlBox = False
        Me.Controls.Add(Me.cancelButton)
        Me.Controls.Add(Me.okButton)
        Me.Controls.Add(Me.TabControl1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "MyBaseForm"
        Me.Text = "Node Properties"
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.GroupBox5.ResumeLayout(False)
        Me.GroupBox5.PerformLayout()
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox4.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.TabPage3.ResumeLayout(False)
        Me.TabPage3.PerformLayout()
        CType(Me.nodePropertiesDataGridView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage4.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage3 As System.Windows.Forms.TabPage
    Friend WithEvents okButton As System.Windows.Forms.Button
    Friend Shadows WithEvents cancelButton As System.Windows.Forms.Button
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents titleTextBox As System.Windows.Forms.TextBox
    Friend WithEvents nodePropertiesDataGridView As System.Windows.Forms.DataGridView
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents nodeNumberLabel As System.Windows.Forms.Label
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents nodeTypeLabel As System.Windows.Forms.Label
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents internalReferenceTextBox As System.Windows.Forms.TextBox
    Friend WithEvents GroupBox5 As System.Windows.Forms.GroupBox
    Friend WithEvents hideInCallTraceCheckBox As System.Windows.Forms.CheckBox
    Friend WithEvents TabPage4 As System.Windows.Forms.TabPage
    Friend WithEvents notesRichTextBox As System.Windows.Forms.RichTextBox
    Friend WithEvents Visible As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents FixedName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents LinkName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents LinkedTo As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents order As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents fromNodesLabel As System.Windows.Forms.Label
End Class
