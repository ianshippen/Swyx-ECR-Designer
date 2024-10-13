<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form1))
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip
        Me.FileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.AddComponenetsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.RenderCodeToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ViewCodeToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.DesignerToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.JavascriptBuilderToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.EditToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.DatabaseSettingsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.SettingsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.AnalyseCDRToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator
        Me.BuildTablesToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.DigToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.HelpToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.AboutToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.Label2 = New System.Windows.Forms.Label
        Me.ProgressBar1 = New System.Windows.Forms.ProgressBar
        Me.Label1 = New System.Windows.Forms.Label
        Me.MenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.FileToolStripMenuItem, Me.EditToolStripMenuItem, Me.ToolsToolStripMenuItem, Me.HelpToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(542, 24)
        Me.MenuStrip1.TabIndex = 0
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'FileToolStripMenuItem
        '
        Me.FileToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.AddComponenetsToolStripMenuItem, Me.RenderCodeToolStripMenuItem, Me.ViewCodeToolStripMenuItem, Me.DesignerToolStripMenuItem, Me.JavascriptBuilderToolStripMenuItem})
        Me.FileToolStripMenuItem.Name = "FileToolStripMenuItem"
        Me.FileToolStripMenuItem.Size = New System.Drawing.Size(35, 20)
        Me.FileToolStripMenuItem.Text = "File"
        '
        'AddComponenetsToolStripMenuItem
        '
        Me.AddComponenetsToolStripMenuItem.Name = "AddComponenetsToolStripMenuItem"
        Me.AddComponenetsToolStripMenuItem.Size = New System.Drawing.Size(182, 22)
        Me.AddComponenetsToolStripMenuItem.Text = "Add Components ..."
        '
        'RenderCodeToolStripMenuItem
        '
        Me.RenderCodeToolStripMenuItem.Name = "RenderCodeToolStripMenuItem"
        Me.RenderCodeToolStripMenuItem.Size = New System.Drawing.Size(182, 22)
        Me.RenderCodeToolStripMenuItem.Text = "Render Code"
        '
        'ViewCodeToolStripMenuItem
        '
        Me.ViewCodeToolStripMenuItem.Name = "ViewCodeToolStripMenuItem"
        Me.ViewCodeToolStripMenuItem.Size = New System.Drawing.Size(182, 22)
        Me.ViewCodeToolStripMenuItem.Text = "View Code"
        '
        'DesignerToolStripMenuItem
        '
        Me.DesignerToolStripMenuItem.Name = "DesignerToolStripMenuItem"
        Me.DesignerToolStripMenuItem.Size = New System.Drawing.Size(182, 22)
        Me.DesignerToolStripMenuItem.Text = "Designer"
        '
        'JavascriptBuilderToolStripMenuItem
        '
        Me.JavascriptBuilderToolStripMenuItem.Name = "JavascriptBuilderToolStripMenuItem"
        Me.JavascriptBuilderToolStripMenuItem.Size = New System.Drawing.Size(182, 22)
        Me.JavascriptBuilderToolStripMenuItem.Text = "Javascript Builder"
        '
        'EditToolStripMenuItem
        '
        Me.EditToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.DatabaseSettingsToolStripMenuItem, Me.SettingsToolStripMenuItem})
        Me.EditToolStripMenuItem.Name = "EditToolStripMenuItem"
        Me.EditToolStripMenuItem.Size = New System.Drawing.Size(37, 20)
        Me.EditToolStripMenuItem.Text = "Edit"
        '
        'DatabaseSettingsToolStripMenuItem
        '
        Me.DatabaseSettingsToolStripMenuItem.Name = "DatabaseSettingsToolStripMenuItem"
        Me.DatabaseSettingsToolStripMenuItem.Size = New System.Drawing.Size(173, 22)
        Me.DatabaseSettingsToolStripMenuItem.Text = "Database Settings"
        '
        'SettingsToolStripMenuItem
        '
        Me.SettingsToolStripMenuItem.Name = "SettingsToolStripMenuItem"
        Me.SettingsToolStripMenuItem.Size = New System.Drawing.Size(173, 22)
        Me.SettingsToolStripMenuItem.Text = "Settings Test"
        '
        'ToolsToolStripMenuItem
        '
        Me.ToolsToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.AnalyseCDRToolStripMenuItem, Me.ToolStripSeparator1, Me.BuildTablesToolStripMenuItem, Me.DigToolStripMenuItem})
        Me.ToolsToolStripMenuItem.Name = "ToolsToolStripMenuItem"
        Me.ToolsToolStripMenuItem.Size = New System.Drawing.Size(44, 20)
        Me.ToolsToolStripMenuItem.Text = "Tools"
        '
        'AnalyseCDRToolStripMenuItem
        '
        Me.AnalyseCDRToolStripMenuItem.Name = "AnalyseCDRToolStripMenuItem"
        Me.AnalyseCDRToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
        Me.AnalyseCDRToolStripMenuItem.Text = "CDR Search"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(149, 6)
        '
        'BuildTablesToolStripMenuItem
        '
        Me.BuildTablesToolStripMenuItem.Name = "BuildTablesToolStripMenuItem"
        Me.BuildTablesToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
        Me.BuildTablesToolStripMenuItem.Text = "Build Tables"
        '
        'DigToolStripMenuItem
        '
        Me.DigToolStripMenuItem.Name = "DigToolStripMenuItem"
        Me.DigToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
        Me.DigToolStripMenuItem.Text = "Dig"
        '
        'HelpToolStripMenuItem
        '
        Me.HelpToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.AboutToolStripMenuItem})
        Me.HelpToolStripMenuItem.Name = "HelpToolStripMenuItem"
        Me.HelpToolStripMenuItem.Size = New System.Drawing.Size(40, 20)
        Me.HelpToolStripMenuItem.Text = "Help"
        '
        'AboutToolStripMenuItem
        '
        Me.AboutToolStripMenuItem.Name = "AboutToolStripMenuItem"
        Me.AboutToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
        Me.AboutToolStripMenuItem.Text = "About"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(46, 135)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(48, 13)
        Me.Label2.TabIndex = 9
        Me.Label2.Text = "Progress"
        '
        'ProgressBar1
        '
        Me.ProgressBar1.Location = New System.Drawing.Point(49, 160)
        Me.ProgressBar1.Name = "ProgressBar1"
        Me.ProgressBar1.Size = New System.Drawing.Size(463, 23)
        Me.ProgressBar1.TabIndex = 8
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(46, 84)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(45, 13)
        Me.Label1.TabIndex = 7
        Me.Label1.Text = "Label1"
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(542, 266)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.ProgressBar1)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.MenuStrip1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.MaximizeBox = False
        Me.Name = "Form1"
        Me.Text = "Swyx ECR Designer"
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents MenuStrip1 As System.Windows.Forms.MenuStrip
    Friend WithEvents FileToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents AddComponenetsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents RenderCodeToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ViewCodeToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents DesignerToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents HelpToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents AboutToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents BuildTablesToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents EditToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents DatabaseSettingsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SettingsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents AnalyseCDRToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents DigToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents ProgressBar1 As System.Windows.Forms.ProgressBar
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents JavascriptBuilderToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem

End Class
