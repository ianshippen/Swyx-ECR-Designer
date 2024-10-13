<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class VBEditorForm
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(VBEditorForm))
        Me.RichTextBox1 = New System.Windows.Forms.RichTextBox
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip
        Me.FileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.FunctionsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.AddVarToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.DecVarToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.GetVarToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.IncVarToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.IsVarToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.IsVarIntToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.IsVarTrueToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.SetVarToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.CAllMachineToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.InitialisationToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator
        Me.StartToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.InHoursToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.OutOfHoursToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.DeliveredToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ConnectedToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.DisconnectToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ListBox1 = New System.Windows.Forms.ListBox
        Me.okButton = New System.Windows.Forms.Button
        Me.cancelButton = New System.Windows.Forms.Button
        Me.NewToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.VariablesToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.MenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'RichTextBox1
        '
        Me.RichTextBox1.AcceptsTab = True
        Me.RichTextBox1.AutoWordSelection = True
        Me.RichTextBox1.Font = New System.Drawing.Font("Lucida Console", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RichTextBox1.ForeColor = System.Drawing.Color.MediumSeaGreen
        Me.RichTextBox1.Location = New System.Drawing.Point(24, 40)
        Me.RichTextBox1.Name = "RichTextBox1"
        Me.RichTextBox1.Size = New System.Drawing.Size(447, 486)
        Me.RichTextBox1.TabIndex = 0
        Me.RichTextBox1.Text = ""
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.FileToolStripMenuItem, Me.FunctionsToolStripMenuItem, Me.VariablesToolStripMenuItem, Me.CAllMachineToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(497, 24)
        Me.MenuStrip1.TabIndex = 1
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'FileToolStripMenuItem
        '
        Me.FileToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.NewToolStripMenuItem})
        Me.FileToolStripMenuItem.Name = "FileToolStripMenuItem"
        Me.FileToolStripMenuItem.Size = New System.Drawing.Size(35, 20)
        Me.FileToolStripMenuItem.Text = "File"
        '
        'FunctionsToolStripMenuItem
        '
        Me.FunctionsToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.AddVarToolStripMenuItem, Me.DecVarToolStripMenuItem, Me.GetVarToolStripMenuItem, Me.IncVarToolStripMenuItem, Me.IsVarToolStripMenuItem, Me.IsVarIntToolStripMenuItem, Me.IsVarTrueToolStripMenuItem, Me.SetVarToolStripMenuItem})
        Me.FunctionsToolStripMenuItem.Name = "FunctionsToolStripMenuItem"
        Me.FunctionsToolStripMenuItem.Size = New System.Drawing.Size(65, 20)
        Me.FunctionsToolStripMenuItem.Text = "Functions"
        '
        'AddVarToolStripMenuItem
        '
        Me.AddVarToolStripMenuItem.Name = "AddVarToolStripMenuItem"
        Me.AddVarToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
        Me.AddVarToolStripMenuItem.Text = "AddVar"
        '
        'DecVarToolStripMenuItem
        '
        Me.DecVarToolStripMenuItem.Name = "DecVarToolStripMenuItem"
        Me.DecVarToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
        Me.DecVarToolStripMenuItem.Text = "DecVar"
        '
        'GetVarToolStripMenuItem
        '
        Me.GetVarToolStripMenuItem.Name = "GetVarToolStripMenuItem"
        Me.GetVarToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
        Me.GetVarToolStripMenuItem.Text = "GetVar"
        '
        'IncVarToolStripMenuItem
        '
        Me.IncVarToolStripMenuItem.Name = "IncVarToolStripMenuItem"
        Me.IncVarToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
        Me.IncVarToolStripMenuItem.Text = "IncVar"
        '
        'IsVarToolStripMenuItem
        '
        Me.IsVarToolStripMenuItem.Name = "IsVarToolStripMenuItem"
        Me.IsVarToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
        Me.IsVarToolStripMenuItem.Text = "IsVar"
        '
        'IsVarIntToolStripMenuItem
        '
        Me.IsVarIntToolStripMenuItem.Name = "IsVarIntToolStripMenuItem"
        Me.IsVarIntToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
        Me.IsVarIntToolStripMenuItem.Text = "IsVarInt"
        '
        'IsVarTrueToolStripMenuItem
        '
        Me.IsVarTrueToolStripMenuItem.Name = "IsVarTrueToolStripMenuItem"
        Me.IsVarTrueToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
        Me.IsVarTrueToolStripMenuItem.Text = "IsVarTrue"
        '
        'SetVarToolStripMenuItem
        '
        Me.SetVarToolStripMenuItem.Name = "SetVarToolStripMenuItem"
        Me.SetVarToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
        Me.SetVarToolStripMenuItem.Text = "SetVar"
        '
        'CAllMachineToolStripMenuItem
        '
        Me.CAllMachineToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.InitialisationToolStripMenuItem, Me.ToolStripSeparator1, Me.StartToolStripMenuItem, Me.InHoursToolStripMenuItem, Me.OutOfHoursToolStripMenuItem, Me.DeliveredToolStripMenuItem, Me.ConnectedToolStripMenuItem, Me.DisconnectToolStripMenuItem})
        Me.CAllMachineToolStripMenuItem.Name = "CAllMachineToolStripMenuItem"
        Me.CAllMachineToolStripMenuItem.Size = New System.Drawing.Size(78, 20)
        Me.CAllMachineToolStripMenuItem.Text = "Call Machine"
        '
        'InitialisationToolStripMenuItem
        '
        Me.InitialisationToolStripMenuItem.Name = "InitialisationToolStripMenuItem"
        Me.InitialisationToolStripMenuItem.Size = New System.Drawing.Size(153, 22)
        Me.InitialisationToolStripMenuItem.Text = "Initialisation .."
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(150, 6)
        '
        'StartToolStripMenuItem
        '
        Me.StartToolStripMenuItem.Name = "StartToolStripMenuItem"
        Me.StartToolStripMenuItem.Size = New System.Drawing.Size(153, 22)
        Me.StartToolStripMenuItem.Text = "Start"
        '
        'InHoursToolStripMenuItem
        '
        Me.InHoursToolStripMenuItem.Name = "InHoursToolStripMenuItem"
        Me.InHoursToolStripMenuItem.Size = New System.Drawing.Size(153, 22)
        Me.InHoursToolStripMenuItem.Text = "In Hours"
        '
        'OutOfHoursToolStripMenuItem
        '
        Me.OutOfHoursToolStripMenuItem.Name = "OutOfHoursToolStripMenuItem"
        Me.OutOfHoursToolStripMenuItem.Size = New System.Drawing.Size(153, 22)
        Me.OutOfHoursToolStripMenuItem.Text = "Out Of Hours"
        '
        'DeliveredToolStripMenuItem
        '
        Me.DeliveredToolStripMenuItem.Name = "DeliveredToolStripMenuItem"
        Me.DeliveredToolStripMenuItem.Size = New System.Drawing.Size(153, 22)
        Me.DeliveredToolStripMenuItem.Text = "Delivered"
        '
        'ConnectedToolStripMenuItem
        '
        Me.ConnectedToolStripMenuItem.Name = "ConnectedToolStripMenuItem"
        Me.ConnectedToolStripMenuItem.Size = New System.Drawing.Size(153, 22)
        Me.ConnectedToolStripMenuItem.Text = "Connected"
        '
        'DisconnectToolStripMenuItem
        '
        Me.DisconnectToolStripMenuItem.Name = "DisconnectToolStripMenuItem"
        Me.DisconnectToolStripMenuItem.Size = New System.Drawing.Size(153, 22)
        Me.DisconnectToolStripMenuItem.Text = "Disconnect"
        '
        'ListBox1
        '
        Me.ListBox1.FormattingEnabled = True
        Me.ListBox1.Location = New System.Drawing.Point(34, 65)
        Me.ListBox1.Name = "ListBox1"
        Me.ListBox1.Size = New System.Drawing.Size(224, 134)
        Me.ListBox1.Sorted = True
        Me.ListBox1.TabIndex = 2
        Me.ListBox1.Visible = False
        '
        'okButton
        '
        Me.okButton.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.okButton.Location = New System.Drawing.Point(396, 543)
        Me.okButton.Name = "okButton"
        Me.okButton.Size = New System.Drawing.Size(75, 23)
        Me.okButton.TabIndex = 4
        Me.okButton.Text = "OK"
        Me.okButton.UseVisualStyleBackColor = True
        '
        'cancelButton
        '
        Me.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.cancelButton.Location = New System.Drawing.Point(294, 543)
        Me.cancelButton.Name = "cancelButton"
        Me.cancelButton.Size = New System.Drawing.Size(75, 23)
        Me.cancelButton.TabIndex = 5
        Me.cancelButton.Text = "Cancel"
        Me.cancelButton.UseVisualStyleBackColor = True
        '
        'NewToolStripMenuItem
        '
        Me.NewToolStripMenuItem.Name = "NewToolStripMenuItem"
        Me.NewToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
        Me.NewToolStripMenuItem.Text = "New"
        '
        'VariablesToolStripMenuItem
        '
        Me.VariablesToolStripMenuItem.Name = "VariablesToolStripMenuItem"
        Me.VariablesToolStripMenuItem.Size = New System.Drawing.Size(62, 20)
        Me.VariablesToolStripMenuItem.Text = "Variables"
        '
        'VBEditorForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(497, 578)
        Me.Controls.Add(Me.ListBox1)
        Me.Controls.Add(Me.cancelButton)
        Me.Controls.Add(Me.okButton)
        Me.Controls.Add(Me.RichTextBox1)
        Me.Controls.Add(Me.MenuStrip1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Name = "VBEditorForm"
        Me.Text = "VB Script Editor"
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents RichTextBox1 As System.Windows.Forms.RichTextBox
    Friend WithEvents MenuStrip1 As System.Windows.Forms.MenuStrip
    Friend WithEvents FileToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents CAllMachineToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents StartToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents InHoursToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents OutOfHoursToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents DeliveredToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ConnectedToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents DisconnectToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents InitialisationToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ListBox1 As System.Windows.Forms.ListBox
    Friend WithEvents okButton As System.Windows.Forms.Button
    Friend WithEvents cancelButton As System.Windows.Forms.Button
    Friend WithEvents FunctionsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents AddVarToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents DecVarToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents GetVarToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents IncVarToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents IsVarToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents IsVarIntToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents IsVarTrueToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SetVarToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents NewToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents VariablesToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
End Class
