<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class WalkthroughForm
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(WalkthroughForm))
        Me.walkthroughGlobalsDataGridView = New System.Windows.Forms.DataGridView
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.myName = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.myDefaultValue = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.myType = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.walkthroughDDIDataGridView = New System.Windows.Forms.DataGridView
        Me.myVariableName = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.myKeyValue = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn3 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.myValue = New System.Windows.Forms.DataGridViewTextBoxColumn
        CType(Me.walkthroughGlobalsDataGridView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        CType(Me.walkthroughDDIDataGridView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'walkthroughGlobalsDataGridView
        '
        Me.walkthroughGlobalsDataGridView.AllowUserToAddRows = False
        Me.walkthroughGlobalsDataGridView.AllowUserToDeleteRows = False
        Me.walkthroughGlobalsDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.walkthroughGlobalsDataGridView.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.myName, Me.myDefaultValue, Me.myType})
        Me.walkthroughGlobalsDataGridView.Location = New System.Drawing.Point(15, 19)
        Me.walkthroughGlobalsDataGridView.Name = "walkthroughGlobalsDataGridView"
        Me.walkthroughGlobalsDataGridView.ReadOnly = True
        Me.walkthroughGlobalsDataGridView.RowHeadersVisible = False
        Me.walkthroughGlobalsDataGridView.Size = New System.Drawing.Size(503, 512)
        Me.walkthroughGlobalsDataGridView.TabIndex = 0
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.walkthroughGlobalsDataGridView)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 12)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(535, 550)
        Me.GroupBox1.TabIndex = 1
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = " Global Variables "
        '
        'myName
        '
        Me.myName.Frozen = True
        Me.myName.HeaderText = "Name"
        Me.myName.Name = "myName"
        Me.myName.ReadOnly = True
        Me.myName.Width = 150
        '
        'myDefaultValue
        '
        Me.myDefaultValue.Frozen = True
        Me.myDefaultValue.HeaderText = "Default Value"
        Me.myDefaultValue.Name = "myDefaultValue"
        Me.myDefaultValue.ReadOnly = True
        Me.myDefaultValue.Width = 200
        '
        'myType
        '
        Me.myType.Frozen = True
        Me.myType.HeaderText = "Type"
        Me.myType.Name = "myType"
        Me.myType.ReadOnly = True
        Me.myType.Width = 150
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.walkthroughDDIDataGridView)
        Me.GroupBox2.Location = New System.Drawing.Point(615, 12)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(642, 550)
        Me.GroupBox2.TabIndex = 2
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "DDI Variables "
        '
        'walkthroughDDIDataGridView
        '
        Me.walkthroughDDIDataGridView.AllowUserToAddRows = False
        Me.walkthroughDDIDataGridView.AllowUserToDeleteRows = False
        Me.walkthroughDDIDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.walkthroughDDIDataGridView.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.myVariableName, Me.myKeyValue, Me.DataGridViewTextBoxColumn3, Me.myValue})
        Me.walkthroughDDIDataGridView.Location = New System.Drawing.Point(15, 19)
        Me.walkthroughDDIDataGridView.Name = "walkthroughDDIDataGridView"
        Me.walkthroughDDIDataGridView.ReadOnly = True
        Me.walkthroughDDIDataGridView.RowHeadersVisible = False
        Me.walkthroughDDIDataGridView.Size = New System.Drawing.Size(604, 512)
        Me.walkthroughDDIDataGridView.TabIndex = 0
        '
        'myVariableName
        '
        Me.myVariableName.Frozen = True
        Me.myVariableName.HeaderText = "Variable Name"
        Me.myVariableName.Name = "myVariableName"
        Me.myVariableName.ReadOnly = True
        Me.myVariableName.Width = 150
        '
        'myKeyValue
        '
        Me.myKeyValue.Frozen = True
        Me.myKeyValue.HeaderText = "Key Value"
        Me.myKeyValue.Name = "myKeyValue"
        Me.myKeyValue.ReadOnly = True
        Me.myKeyValue.Width = 200
        '
        'DataGridViewTextBoxColumn3
        '
        Me.DataGridViewTextBoxColumn3.Frozen = True
        Me.DataGridViewTextBoxColumn3.HeaderText = "Type"
        Me.DataGridViewTextBoxColumn3.Name = "DataGridViewTextBoxColumn3"
        Me.DataGridViewTextBoxColumn3.ReadOnly = True
        Me.DataGridViewTextBoxColumn3.Width = 150
        '
        'myValue
        '
        Me.myValue.Frozen = True
        Me.myValue.HeaderText = "Value"
        Me.myValue.Name = "myValue"
        Me.myValue.ReadOnly = True
        '
        'WalkthroughForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1269, 616)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "WalkthroughForm"
        Me.Text = "Walk Through"
        CType(Me.walkthroughGlobalsDataGridView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        CType(Me.walkthroughDDIDataGridView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents walkthroughGlobalsDataGridView As System.Windows.Forms.DataGridView
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents myName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents myDefaultValue As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents myType As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents walkthroughDDIDataGridView As System.Windows.Forms.DataGridView
    Friend WithEvents myVariableName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents myKeyValue As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn3 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents myValue As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
