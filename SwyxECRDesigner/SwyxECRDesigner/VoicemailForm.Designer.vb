<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class VoicemailForm
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(VoicemailForm))
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.GroupBox5 = New System.Windows.Forms.GroupBox
        Me.Label6 = New System.Windows.Forms.Label
        Me.emailTextBox = New System.Windows.Forms.TextBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.maxMessageLengthTextBox = New System.Windows.Forms.TextBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.variableNameTextBox = New System.Windows.Forms.TextBox
        Me.saveFilenameCheckBox = New System.Windows.Forms.CheckBox
        Me.GroupBox4 = New System.Windows.Forms.GroupBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.announcementTextBox = New System.Windows.Forms.TextBox
        Me.playAnnouncementCheckBox = New System.Windows.Forms.CheckBox
        Me.GroupBox3 = New System.Windows.Forms.GroupBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.dtmfTextBox = New System.Windows.Forms.TextBox
        Me.dtmfCheckBox = New System.Windows.Forms.CheckBox
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.welcomeAnnouncementTextBox = New System.Windows.Forms.TextBox
        Me.playWelcomeAnnouncementCheckBox = New System.Windows.Forms.CheckBox
        Me.okButton = New System.Windows.Forms.Button
        Me.RadioButton1 = New System.Windows.Forms.RadioButton
        Me.RadioButton2 = New System.Windows.Forms.RadioButton
        Me.TabControl1 = New System.Windows.Forms.TabControl
        Me.TabPage1 = New System.Windows.Forms.TabPage
        Me.GroupBox6 = New System.Windows.Forms.GroupBox
        Me.hideInCallTraceCheckBox = New System.Windows.Forms.CheckBox
        Me.GroupBox7 = New System.Windows.Forms.GroupBox
        Me.internalReferenceTextBox = New System.Windows.Forms.TextBox
        Me.GroupBox8 = New System.Windows.Forms.GroupBox
        Me.nodeNumberLabel = New System.Windows.Forms.Label
        Me.GroupBox9 = New System.Windows.Forms.GroupBox
        Me.nodeTypeLabel = New System.Windows.Forms.Label
        Me.GroupBox10 = New System.Windows.Forms.GroupBox
        Me.titleTextBox = New System.Windows.Forms.TextBox
        Me.TabPage2 = New System.Windows.Forms.TabPage
        Me.TabPage3 = New System.Windows.Forms.TabPage
        Me.nodePropertiesDataGridView = New System.Windows.Forms.DataGridView
        Me.TabPage4 = New System.Windows.Forms.TabPage
        Me.notesRichTextBox = New System.Windows.Forms.RichTextBox
        Me.cancelButton = New System.Windows.Forms.Button
        Me.Visible = New System.Windows.Forms.DataGridViewCheckBoxColumn
        Me.FixedName = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.LinkName = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.LinkedTo = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.order = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox5.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.GroupBox6.SuspendLayout()
        Me.GroupBox7.SuspendLayout()
        Me.GroupBox8.SuspendLayout()
        Me.GroupBox9.SuspendLayout()
        Me.GroupBox10.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        Me.TabPage3.SuspendLayout()
        CType(Me.nodePropertiesDataGridView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage4.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.GroupBox5)
        Me.GroupBox1.Controls.Add(Me.GroupBox4)
        Me.GroupBox1.Controls.Add(Me.GroupBox3)
        Me.GroupBox1.Controls.Add(Me.GroupBox2)
        Me.GroupBox1.Location = New System.Drawing.Point(28, 53)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(476, 622)
        Me.GroupBox1.TabIndex = 2
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = " Special Voicemail Properties "
        '
        'GroupBox5
        '
        Me.GroupBox5.Controls.Add(Me.Label6)
        Me.GroupBox5.Controls.Add(Me.emailTextBox)
        Me.GroupBox5.Controls.Add(Me.Label5)
        Me.GroupBox5.Controls.Add(Me.maxMessageLengthTextBox)
        Me.GroupBox5.Controls.Add(Me.Label4)
        Me.GroupBox5.Controls.Add(Me.variableNameTextBox)
        Me.GroupBox5.Controls.Add(Me.saveFilenameCheckBox)
        Me.GroupBox5.Location = New System.Drawing.Point(25, 418)
        Me.GroupBox5.Name = "GroupBox5"
        Me.GroupBox5.Size = New System.Drawing.Size(423, 182)
        Me.GroupBox5.TabIndex = 4
        Me.GroupBox5.TabStop = False
        Me.GroupBox5.Text = " Message "
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(273, 103)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(73, 13)
        Me.Label6.TabIndex = 6
        Me.Label6.Text = "Email Address"
        '
        'emailTextBox
        '
        Me.emailTextBox.Location = New System.Drawing.Point(24, 100)
        Me.emailTextBox.Name = "emailTextBox"
        Me.emailTextBox.Size = New System.Drawing.Size(243, 20)
        Me.emailTextBox.TabIndex = 5
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(111, 142)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(133, 13)
        Me.Label5.TabIndex = 4
        Me.Label5.Text = "Maximum Message Length"
        '
        'maxMessageLengthTextBox
        '
        Me.maxMessageLengthTextBox.Location = New System.Drawing.Point(24, 139)
        Me.maxMessageLengthTextBox.Name = "maxMessageLengthTextBox"
        Me.maxMessageLengthTextBox.Size = New System.Drawing.Size(72, 20)
        Me.maxMessageLengthTextBox.TabIndex = 3
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(273, 65)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(76, 13)
        Me.Label4.TabIndex = 2
        Me.Label4.Text = "Variable Name"
        '
        'variableNameTextBox
        '
        Me.variableNameTextBox.Location = New System.Drawing.Point(24, 62)
        Me.variableNameTextBox.Name = "variableNameTextBox"
        Me.variableNameTextBox.Size = New System.Drawing.Size(243, 20)
        Me.variableNameTextBox.TabIndex = 1
        '
        'saveFilenameCheckBox
        '
        Me.saveFilenameCheckBox.AutoSize = True
        Me.saveFilenameCheckBox.Location = New System.Drawing.Point(24, 30)
        Me.saveFilenameCheckBox.Name = "saveFilenameCheckBox"
        Me.saveFilenameCheckBox.Size = New System.Drawing.Size(149, 17)
        Me.saveFilenameCheckBox.TabIndex = 0
        Me.saveFilenameCheckBox.Text = "Save Filename In Variable"
        Me.saveFilenameCheckBox.UseVisualStyleBackColor = True
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.Label3)
        Me.GroupBox4.Controls.Add(Me.announcementTextBox)
        Me.GroupBox4.Controls.Add(Me.playAnnouncementCheckBox)
        Me.GroupBox4.Location = New System.Drawing.Point(25, 293)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(423, 99)
        Me.GroupBox4.TabIndex = 3
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = " Announcement "
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(273, 65)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(79, 13)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "Announcement"
        '
        'announcementTextBox
        '
        Me.announcementTextBox.Location = New System.Drawing.Point(24, 62)
        Me.announcementTextBox.Name = "announcementTextBox"
        Me.announcementTextBox.Size = New System.Drawing.Size(243, 20)
        Me.announcementTextBox.TabIndex = 1
        '
        'playAnnouncementCheckBox
        '
        Me.playAnnouncementCheckBox.AutoSize = True
        Me.playAnnouncementCheckBox.Location = New System.Drawing.Point(24, 30)
        Me.playAnnouncementCheckBox.Name = "playAnnouncementCheckBox"
        Me.playAnnouncementCheckBox.Size = New System.Drawing.Size(115, 17)
        Me.playAnnouncementCheckBox.TabIndex = 0
        Me.playAnnouncementCheckBox.Text = "Play Annoucement"
        Me.playAnnouncementCheckBox.UseVisualStyleBackColor = True
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.Label2)
        Me.GroupBox3.Controls.Add(Me.dtmfTextBox)
        Me.GroupBox3.Controls.Add(Me.dtmfCheckBox)
        Me.GroupBox3.Location = New System.Drawing.Point(25, 168)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(423, 99)
        Me.GroupBox3.TabIndex = 3
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = " DTMF Caller ID"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(273, 65)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(122, 13)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "Caller ID Announcement"
        '
        'dtmfTextBox
        '
        Me.dtmfTextBox.Location = New System.Drawing.Point(24, 62)
        Me.dtmfTextBox.Name = "dtmfTextBox"
        Me.dtmfTextBox.Size = New System.Drawing.Size(243, 20)
        Me.dtmfTextBox.TabIndex = 1
        '
        'dtmfCheckBox
        '
        Me.dtmfCheckBox.AutoSize = True
        Me.dtmfCheckBox.Location = New System.Drawing.Point(24, 30)
        Me.dtmfCheckBox.Name = "dtmfCheckBox"
        Me.dtmfCheckBox.Size = New System.Drawing.Size(145, 17)
        Me.dtmfCheckBox.TabIndex = 0
        Me.dtmfCheckBox.Text = "Get Caller ID From DTMF"
        Me.dtmfCheckBox.UseVisualStyleBackColor = True
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.Label1)
        Me.GroupBox2.Controls.Add(Me.welcomeAnnouncementTextBox)
        Me.GroupBox2.Controls.Add(Me.playWelcomeAnnouncementCheckBox)
        Me.GroupBox2.Location = New System.Drawing.Point(25, 37)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(423, 99)
        Me.GroupBox2.TabIndex = 0
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = " Welcome Announcement "
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(273, 65)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(127, 13)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Welcome Announcement"
        '
        'welcomeAnnouncementTextBox
        '
        Me.welcomeAnnouncementTextBox.Location = New System.Drawing.Point(24, 62)
        Me.welcomeAnnouncementTextBox.Name = "welcomeAnnouncementTextBox"
        Me.welcomeAnnouncementTextBox.Size = New System.Drawing.Size(243, 20)
        Me.welcomeAnnouncementTextBox.TabIndex = 1
        '
        'playWelcomeAnnouncementCheckBox
        '
        Me.playWelcomeAnnouncementCheckBox.AutoSize = True
        Me.playWelcomeAnnouncementCheckBox.Location = New System.Drawing.Point(24, 30)
        Me.playWelcomeAnnouncementCheckBox.Name = "playWelcomeAnnouncementCheckBox"
        Me.playWelcomeAnnouncementCheckBox.Size = New System.Drawing.Size(163, 17)
        Me.playWelcomeAnnouncementCheckBox.TabIndex = 0
        Me.playWelcomeAnnouncementCheckBox.Text = "Play Welcome Annoucement"
        Me.playWelcomeAnnouncementCheckBox.UseVisualStyleBackColor = True
        '
        'okButton
        '
        Me.okButton.Location = New System.Drawing.Point(474, 749)
        Me.okButton.Name = "okButton"
        Me.okButton.Size = New System.Drawing.Size(75, 23)
        Me.okButton.TabIndex = 3
        Me.okButton.Text = "OK"
        Me.okButton.UseVisualStyleBackColor = True
        '
        'RadioButton1
        '
        Me.RadioButton1.AutoSize = True
        Me.RadioButton1.Location = New System.Drawing.Point(28, 17)
        Me.RadioButton1.Name = "RadioButton1"
        Me.RadioButton1.Size = New System.Drawing.Size(138, 17)
        Me.RadioButton1.TabIndex = 16
        Me.RadioButton1.TabStop = True
        Me.RadioButton1.Text = "Use Standard Voicemail"
        Me.RadioButton1.UseVisualStyleBackColor = True
        '
        'RadioButton2
        '
        Me.RadioButton2.AutoSize = True
        Me.RadioButton2.Location = New System.Drawing.Point(214, 17)
        Me.RadioButton2.Name = "RadioButton2"
        Me.RadioButton2.Size = New System.Drawing.Size(130, 17)
        Me.RadioButton2.TabIndex = 17
        Me.RadioButton2.TabStop = True
        Me.RadioButton2.Text = "Use Special Voicemail"
        Me.RadioButton2.UseVisualStyleBackColor = True
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
        Me.TabControl1.Size = New System.Drawing.Size(533, 703)
        Me.TabControl1.TabIndex = 18
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.GroupBox6)
        Me.TabPage1.Controls.Add(Me.GroupBox7)
        Me.TabPage1.Controls.Add(Me.GroupBox8)
        Me.TabPage1.Controls.Add(Me.GroupBox9)
        Me.TabPage1.Controls.Add(Me.GroupBox10)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(525, 677)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "General"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'GroupBox6
        '
        Me.GroupBox6.Controls.Add(Me.hideInCallTraceCheckBox)
        Me.GroupBox6.Location = New System.Drawing.Point(69, 222)
        Me.GroupBox6.Name = "GroupBox6"
        Me.GroupBox6.Size = New System.Drawing.Size(380, 45)
        Me.GroupBox6.TabIndex = 4
        Me.GroupBox6.TabStop = False
        Me.GroupBox6.Text = " Options "
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
        'GroupBox7
        '
        Me.GroupBox7.Controls.Add(Me.internalReferenceTextBox)
        Me.GroupBox7.Location = New System.Drawing.Point(69, 129)
        Me.GroupBox7.Name = "GroupBox7"
        Me.GroupBox7.Size = New System.Drawing.Size(380, 75)
        Me.GroupBox7.TabIndex = 2
        Me.GroupBox7.TabStop = False
        Me.GroupBox7.Text = " Internal Reference Name "
        '
        'internalReferenceTextBox
        '
        Me.internalReferenceTextBox.Location = New System.Drawing.Point(16, 30)
        Me.internalReferenceTextBox.Name = "internalReferenceTextBox"
        Me.internalReferenceTextBox.Size = New System.Drawing.Size(343, 20)
        Me.internalReferenceTextBox.TabIndex = 0
        '
        'GroupBox8
        '
        Me.GroupBox8.Controls.Add(Me.nodeNumberLabel)
        Me.GroupBox8.Location = New System.Drawing.Point(359, 283)
        Me.GroupBox8.Name = "GroupBox8"
        Me.GroupBox8.Size = New System.Drawing.Size(90, 75)
        Me.GroupBox8.TabIndex = 3
        Me.GroupBox8.TabStop = False
        Me.GroupBox8.Text = " Node Number "
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
        'GroupBox9
        '
        Me.GroupBox9.Controls.Add(Me.nodeTypeLabel)
        Me.GroupBox9.Location = New System.Drawing.Point(69, 283)
        Me.GroupBox9.Name = "GroupBox9"
        Me.GroupBox9.Size = New System.Drawing.Size(267, 75)
        Me.GroupBox9.TabIndex = 2
        Me.GroupBox9.TabStop = False
        Me.GroupBox9.Text = " Type "
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
        'GroupBox10
        '
        Me.GroupBox10.Controls.Add(Me.titleTextBox)
        Me.GroupBox10.Location = New System.Drawing.Point(69, 23)
        Me.GroupBox10.Name = "GroupBox10"
        Me.GroupBox10.Size = New System.Drawing.Size(380, 75)
        Me.GroupBox10.TabIndex = 1
        Me.GroupBox10.TabStop = False
        Me.GroupBox10.Text = " Title "
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
        Me.TabPage2.Controls.Add(Me.RadioButton1)
        Me.TabPage2.Controls.Add(Me.RadioButton2)
        Me.TabPage2.Controls.Add(Me.GroupBox1)
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(525, 677)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Parameters"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'TabPage3
        '
        Me.TabPage3.Controls.Add(Me.nodePropertiesDataGridView)
        Me.TabPage3.Location = New System.Drawing.Point(4, 22)
        Me.TabPage3.Name = "TabPage3"
        Me.TabPage3.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage3.Size = New System.Drawing.Size(525, 677)
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
        Me.nodePropertiesDataGridView.Location = New System.Drawing.Point(54, 35)
        Me.nodePropertiesDataGridView.Name = "nodePropertiesDataGridView"
        Me.nodePropertiesDataGridView.RowHeadersVisible = False
        Me.nodePropertiesDataGridView.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.nodePropertiesDataGridView.Size = New System.Drawing.Size(406, 136)
        Me.nodePropertiesDataGridView.TabIndex = 0
        '
        'TabPage4
        '
        Me.TabPage4.Controls.Add(Me.notesRichTextBox)
        Me.TabPage4.Location = New System.Drawing.Point(4, 22)
        Me.TabPage4.Name = "TabPage4"
        Me.TabPage4.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage4.Size = New System.Drawing.Size(525, 677)
        Me.TabPage4.TabIndex = 3
        Me.TabPage4.Text = "Notes"
        Me.TabPage4.UseVisualStyleBackColor = True
        '
        'notesRichTextBox
        '
        Me.notesRichTextBox.Font = New System.Drawing.Font("Lucida Console", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.notesRichTextBox.Location = New System.Drawing.Point(18, 24)
        Me.notesRichTextBox.Name = "notesRichTextBox"
        Me.notesRichTextBox.Size = New System.Drawing.Size(488, 635)
        Me.notesRichTextBox.TabIndex = 0
        Me.notesRichTextBox.Text = ""
        '
        'cancelButton
        '
        Me.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.cancelButton.Location = New System.Drawing.Point(358, 749)
        Me.cancelButton.Name = "cancelButton"
        Me.cancelButton.Size = New System.Drawing.Size(75, 23)
        Me.cancelButton.TabIndex = 19
        Me.cancelButton.Text = "Cancel"
        Me.cancelButton.UseVisualStyleBackColor = True
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
        'VoicemailForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(573, 787)
        Me.Controls.Add(Me.cancelButton)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.okButton)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "VoicemailForm"
        Me.Text = "Voicemail Properties"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox5.ResumeLayout(False)
        Me.GroupBox5.PerformLayout()
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox4.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.GroupBox6.ResumeLayout(False)
        Me.GroupBox6.PerformLayout()
        Me.GroupBox7.ResumeLayout(False)
        Me.GroupBox7.PerformLayout()
        Me.GroupBox8.ResumeLayout(False)
        Me.GroupBox8.PerformLayout()
        Me.GroupBox9.ResumeLayout(False)
        Me.GroupBox9.PerformLayout()
        Me.GroupBox10.ResumeLayout(False)
        Me.GroupBox10.PerformLayout()
        Me.TabPage2.ResumeLayout(False)
        Me.TabPage2.PerformLayout()
        Me.TabPage3.ResumeLayout(False)
        CType(Me.nodePropertiesDataGridView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage4.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents welcomeAnnouncementTextBox As System.Windows.Forms.TextBox
    Friend WithEvents playWelcomeAnnouncementCheckBox As System.Windows.Forms.CheckBox
    Friend WithEvents GroupBox5 As System.Windows.Forms.GroupBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents variableNameTextBox As System.Windows.Forms.TextBox
    Friend WithEvents saveFilenameCheckBox As System.Windows.Forms.CheckBox
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents announcementTextBox As System.Windows.Forms.TextBox
    Friend WithEvents playAnnouncementCheckBox As System.Windows.Forms.CheckBox
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents dtmfTextBox As System.Windows.Forms.TextBox
    Friend WithEvents dtmfCheckBox As System.Windows.Forms.CheckBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents emailTextBox As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents maxMessageLengthTextBox As System.Windows.Forms.TextBox
    Friend WithEvents okButton As System.Windows.Forms.Button
    Friend WithEvents RadioButton1 As System.Windows.Forms.RadioButton
    Friend WithEvents RadioButton2 As System.Windows.Forms.RadioButton
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents GroupBox6 As System.Windows.Forms.GroupBox
    Friend WithEvents hideInCallTraceCheckBox As System.Windows.Forms.CheckBox
    Friend WithEvents GroupBox7 As System.Windows.Forms.GroupBox
    Friend WithEvents internalReferenceTextBox As System.Windows.Forms.TextBox
    Friend WithEvents GroupBox8 As System.Windows.Forms.GroupBox
    Friend WithEvents nodeNumberLabel As System.Windows.Forms.Label
    Friend WithEvents GroupBox9 As System.Windows.Forms.GroupBox
    Friend WithEvents nodeTypeLabel As System.Windows.Forms.Label
    Friend WithEvents GroupBox10 As System.Windows.Forms.GroupBox
    Friend WithEvents titleTextBox As System.Windows.Forms.TextBox
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage3 As System.Windows.Forms.TabPage
    Friend WithEvents nodePropertiesDataGridView As System.Windows.Forms.DataGridView
    Friend WithEvents TabPage4 As System.Windows.Forms.TabPage
    Friend WithEvents notesRichTextBox As System.Windows.Forms.RichTextBox
    Friend WithEvents cancelButton As System.Windows.Forms.Button
    Friend WithEvents Visible As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents FixedName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents LinkName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents LinkedTo As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents order As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
