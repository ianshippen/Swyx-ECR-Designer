<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class CDRSearchForm
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(CDRSearchForm))
        Me.DataGridView1 = New System.Windows.Forms.DataGridView
        Me.CallId = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.OriginationNumber = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.OriginationName = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.CalledNumber = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.CalledName = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DestinationNumber = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DestinationName = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.StartTime = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.ScriptConnectTime = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DeliveredTime = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.ConnectTime = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.EndTime = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.currency = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.costs = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.state = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.publicAccessPrefix = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.lcrProvider = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.projectNumber = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.aoc = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.originationDevice = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.destinationDevice = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.transferredByNumber = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.transferredByName = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.transferredCallId1 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.transferredCallId2 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.transferredToCallId = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.transferTime = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.disconnectReason = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.Label8 = New System.Windows.Forms.Label
        Me.disconnectReasonComboBox = New System.Windows.Forms.ComboBox
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.clearButton = New System.Windows.Forms.Button
        Me.customConditionsTextBox = New System.Windows.Forms.TextBox
        Me.searchButton = New System.Windows.Forms.Button
        Me.Label7 = New System.Windows.Forms.Label
        Me.destNameTextBox = New System.Windows.Forms.TextBox
        Me.Label6 = New System.Windows.Forms.Label
        Me.destNumberTextBox = New System.Windows.Forms.TextBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.calledNameTextBox = New System.Windows.Forms.TextBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.calledNumberTextBox = New System.Windows.Forms.TextBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.origNameTextBox = New System.Windows.Forms.TextBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.origNumberTextBox = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.callIdTextBox = New System.Windows.Forms.TextBox
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.SuspendLayout()
        '
        'DataGridView1
        '
        Me.DataGridView1.AllowUserToAddRows = False
        Me.DataGridView1.AllowUserToDeleteRows = False
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.CallId, Me.OriginationNumber, Me.OriginationName, Me.CalledNumber, Me.CalledName, Me.DestinationNumber, Me.DestinationName, Me.StartTime, Me.ScriptConnectTime, Me.DeliveredTime, Me.ConnectTime, Me.EndTime, Me.currency, Me.costs, Me.state, Me.publicAccessPrefix, Me.lcrProvider, Me.projectNumber, Me.aoc, Me.originationDevice, Me.destinationDevice, Me.transferredByNumber, Me.transferredByName, Me.transferredCallId1, Me.transferredCallId2, Me.transferredToCallId, Me.transferTime, Me.disconnectReason})
        Me.DataGridView1.Location = New System.Drawing.Point(12, 275)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.ReadOnly = True
        Me.DataGridView1.RowHeadersWidth = 55
        Me.DataGridView1.Size = New System.Drawing.Size(1224, 324)
        Me.DataGridView1.TabIndex = 0
        '
        'CallId
        '
        Me.CallId.HeaderText = "Call Id"
        Me.CallId.Name = "CallId"
        Me.CallId.ReadOnly = True
        '
        'OriginationNumber
        '
        Me.OriginationNumber.HeaderText = "Origination Number"
        Me.OriginationNumber.Name = "OriginationNumber"
        Me.OriginationNumber.ReadOnly = True
        '
        'OriginationName
        '
        Me.OriginationName.HeaderText = "Origination Name"
        Me.OriginationName.Name = "OriginationName"
        Me.OriginationName.ReadOnly = True
        '
        'CalledNumber
        '
        Me.CalledNumber.HeaderText = "Called Number"
        Me.CalledNumber.Name = "CalledNumber"
        Me.CalledNumber.ReadOnly = True
        '
        'CalledName
        '
        Me.CalledName.HeaderText = "Called Name"
        Me.CalledName.Name = "CalledName"
        Me.CalledName.ReadOnly = True
        '
        'DestinationNumber
        '
        Me.DestinationNumber.HeaderText = "Destination Number"
        Me.DestinationNumber.Name = "DestinationNumber"
        Me.DestinationNumber.ReadOnly = True
        '
        'DestinationName
        '
        Me.DestinationName.HeaderText = "Destination Name"
        Me.DestinationName.Name = "DestinationName"
        Me.DestinationName.ReadOnly = True
        '
        'StartTime
        '
        Me.StartTime.HeaderText = "Start Time"
        Me.StartTime.Name = "StartTime"
        Me.StartTime.ReadOnly = True
        '
        'ScriptConnectTime
        '
        Me.ScriptConnectTime.HeaderText = "Script Connect Time"
        Me.ScriptConnectTime.Name = "ScriptConnectTime"
        Me.ScriptConnectTime.ReadOnly = True
        '
        'DeliveredTime
        '
        Me.DeliveredTime.HeaderText = "Delivered Time"
        Me.DeliveredTime.Name = "DeliveredTime"
        Me.DeliveredTime.ReadOnly = True
        '
        'ConnectTime
        '
        Me.ConnectTime.HeaderText = "Connect Time"
        Me.ConnectTime.Name = "ConnectTime"
        Me.ConnectTime.ReadOnly = True
        '
        'EndTime
        '
        Me.EndTime.HeaderText = "End Time"
        Me.EndTime.Name = "EndTime"
        Me.EndTime.ReadOnly = True
        '
        'currency
        '
        Me.currency.HeaderText = "Currency"
        Me.currency.Name = "currency"
        Me.currency.ReadOnly = True
        '
        'costs
        '
        Me.costs.HeaderText = "Costs"
        Me.costs.Name = "costs"
        Me.costs.ReadOnly = True
        '
        'state
        '
        Me.state.HeaderText = "State"
        Me.state.Name = "state"
        Me.state.ReadOnly = True
        '
        'publicAccessPrefix
        '
        Me.publicAccessPrefix.HeaderText = "Public Access Prefix"
        Me.publicAccessPrefix.Name = "publicAccessPrefix"
        Me.publicAccessPrefix.ReadOnly = True
        '
        'lcrProvider
        '
        Me.lcrProvider.HeaderText = "LCR Provider"
        Me.lcrProvider.Name = "lcrProvider"
        Me.lcrProvider.ReadOnly = True
        '
        'projectNumber
        '
        Me.projectNumber.HeaderText = "Project Number"
        Me.projectNumber.Name = "projectNumber"
        Me.projectNumber.ReadOnly = True
        '
        'aoc
        '
        Me.aoc.HeaderText = "AOC"
        Me.aoc.Name = "aoc"
        Me.aoc.ReadOnly = True
        '
        'originationDevice
        '
        Me.originationDevice.HeaderText = "Origination Device"
        Me.originationDevice.Name = "originationDevice"
        Me.originationDevice.ReadOnly = True
        '
        'destinationDevice
        '
        Me.destinationDevice.HeaderText = "Destination Device"
        Me.destinationDevice.Name = "destinationDevice"
        Me.destinationDevice.ReadOnly = True
        '
        'transferredByNumber
        '
        Me.transferredByNumber.HeaderText = "Transferred By Number"
        Me.transferredByNumber.Name = "transferredByNumber"
        Me.transferredByNumber.ReadOnly = True
        '
        'transferredByName
        '
        Me.transferredByName.HeaderText = "Transferred By Name"
        Me.transferredByName.Name = "transferredByName"
        Me.transferredByName.ReadOnly = True
        '
        'transferredCallId1
        '
        Me.transferredCallId1.HeaderText = "Transferred Call Id 1"
        Me.transferredCallId1.Name = "transferredCallId1"
        Me.transferredCallId1.ReadOnly = True
        '
        'transferredCallId2
        '
        Me.transferredCallId2.HeaderText = "Transferred Call Id 2"
        Me.transferredCallId2.Name = "transferredCallId2"
        Me.transferredCallId2.ReadOnly = True
        '
        'transferredToCallId
        '
        Me.transferredToCallId.HeaderText = "Transferred To Call Id"
        Me.transferredToCallId.Name = "transferredToCallId"
        Me.transferredToCallId.ReadOnly = True
        '
        'transferTime
        '
        Me.transferTime.HeaderText = "Transfer Time"
        Me.transferTime.Name = "transferTime"
        Me.transferTime.ReadOnly = True
        '
        'disconnectReason
        '
        Me.disconnectReason.HeaderText = "Disconnect Reason"
        Me.disconnectReason.Name = "disconnectReason"
        Me.disconnectReason.ReadOnly = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Label8)
        Me.GroupBox1.Controls.Add(Me.disconnectReasonComboBox)
        Me.GroupBox1.Controls.Add(Me.GroupBox2)
        Me.GroupBox1.Controls.Add(Me.searchButton)
        Me.GroupBox1.Controls.Add(Me.Label7)
        Me.GroupBox1.Controls.Add(Me.destNameTextBox)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.destNumberTextBox)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.calledNameTextBox)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.calledNumberTextBox)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.origNameTextBox)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.origNumberTextBox)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.callIdTextBox)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 12)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(1224, 243)
        Me.GroupBox1.TabIndex = 1
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Search Criteria"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(442, 22)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(101, 13)
        Me.Label8.TabIndex = 16
        Me.Label8.Text = "Disconnect Reason"
        '
        'disconnectReasonComboBox
        '
        Me.disconnectReasonComboBox.FormattingEnabled = True
        Me.disconnectReasonComboBox.Items.AddRange(New Object() {"Busy", "CallDisconnected", "CallIgnored", "CallRoutingFailed", "CallTransferred", "DivertToCallerImpossible", "Failure", "IncompatibleDestination", "NetworkCongestion", "NoAnswer", "NoChannelAvailable", "NumberChanged", "OriginatorDisconnected", "PermissionDenied", "ProceedWithDestinationScript", "Reject", "ScriptActionTimeout", "SecurityNegotiationFailed", "SubstituteNumberDenied", "UnknownNumber", "Unreachable"})
        Me.disconnectReasonComboBox.Location = New System.Drawing.Point(288, 19)
        Me.disconnectReasonComboBox.Name = "disconnectReasonComboBox"
        Me.disconnectReasonComboBox.Size = New System.Drawing.Size(146, 21)
        Me.disconnectReasonComboBox.Sorted = True
        Me.disconnectReasonComboBox.TabIndex = 15
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.clearButton)
        Me.GroupBox2.Controls.Add(Me.customConditionsTextBox)
        Me.GroupBox2.Location = New System.Drawing.Point(890, 19)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(317, 190)
        Me.GroupBox2.TabIndex = 14
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = " Custom Conditions "
        '
        'clearButton
        '
        Me.clearButton.Location = New System.Drawing.Point(236, 19)
        Me.clearButton.Name = "clearButton"
        Me.clearButton.Size = New System.Drawing.Size(75, 23)
        Me.clearButton.TabIndex = 15
        Me.clearButton.Text = "Clear"
        Me.clearButton.UseVisualStyleBackColor = True
        '
        'customConditionsTextBox
        '
        Me.customConditionsTextBox.Location = New System.Drawing.Point(15, 19)
        Me.customConditionsTextBox.Multiline = True
        Me.customConditionsTextBox.Name = "customConditionsTextBox"
        Me.customConditionsTextBox.Size = New System.Drawing.Size(206, 157)
        Me.customConditionsTextBox.TabIndex = 0
        '
        'searchButton
        '
        Me.searchButton.Location = New System.Drawing.Point(1132, 215)
        Me.searchButton.Name = "searchButton"
        Me.searchButton.Size = New System.Drawing.Size(75, 23)
        Me.searchButton.TabIndex = 2
        Me.searchButton.Text = "Search .."
        Me.searchButton.UseVisualStyleBackColor = True
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(169, 214)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(60, 13)
        Me.Label7.TabIndex = 13
        Me.Label7.Text = "Dest Name"
        '
        'destNameTextBox
        '
        Me.destNameTextBox.Location = New System.Drawing.Point(16, 211)
        Me.destNameTextBox.Name = "destNameTextBox"
        Me.destNameTextBox.Size = New System.Drawing.Size(147, 20)
        Me.destNameTextBox.TabIndex = 12
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(169, 182)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(69, 13)
        Me.Label6.TabIndex = 11
        Me.Label6.Text = "Dest Number"
        '
        'destNumberTextBox
        '
        Me.destNumberTextBox.Location = New System.Drawing.Point(16, 179)
        Me.destNumberTextBox.Name = "destNumberTextBox"
        Me.destNumberTextBox.Size = New System.Drawing.Size(147, 20)
        Me.destNumberTextBox.TabIndex = 10
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(169, 150)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(67, 13)
        Me.Label5.TabIndex = 9
        Me.Label5.Text = "Called Name"
        '
        'calledNameTextBox
        '
        Me.calledNameTextBox.Location = New System.Drawing.Point(16, 147)
        Me.calledNameTextBox.Name = "calledNameTextBox"
        Me.calledNameTextBox.Size = New System.Drawing.Size(147, 20)
        Me.calledNameTextBox.TabIndex = 8
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(169, 118)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(76, 13)
        Me.Label4.TabIndex = 7
        Me.Label4.Text = "Called Number"
        '
        'calledNumberTextBox
        '
        Me.calledNumberTextBox.Location = New System.Drawing.Point(16, 115)
        Me.calledNumberTextBox.Name = "calledNumberTextBox"
        Me.calledNumberTextBox.Size = New System.Drawing.Size(147, 20)
        Me.calledNumberTextBox.TabIndex = 6
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(169, 86)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(57, 13)
        Me.Label3.TabIndex = 5
        Me.Label3.Text = "Orig Name"
        '
        'origNameTextBox
        '
        Me.origNameTextBox.Location = New System.Drawing.Point(16, 83)
        Me.origNameTextBox.Name = "origNameTextBox"
        Me.origNameTextBox.Size = New System.Drawing.Size(147, 20)
        Me.origNameTextBox.TabIndex = 4
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(169, 54)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(66, 13)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "Orig Number"
        '
        'origNumberTextBox
        '
        Me.origNumberTextBox.Location = New System.Drawing.Point(16, 51)
        Me.origNumberTextBox.Name = "origNumberTextBox"
        Me.origNumberTextBox.Size = New System.Drawing.Size(147, 20)
        Me.origNumberTextBox.TabIndex = 2
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(169, 22)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(38, 13)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Call ID"
        '
        'callIdTextBox
        '
        Me.callIdTextBox.Location = New System.Drawing.Point(16, 19)
        Me.callIdTextBox.Name = "callIdTextBox"
        Me.callIdTextBox.Size = New System.Drawing.Size(147, 20)
        Me.callIdTextBox.TabIndex = 0
        '
        'CDRSearchForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1248, 624)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.DataGridView1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "CDRSearchForm"
        Me.Text = "CDR Search Results"
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents CallId As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents OriginationNumber As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents OriginationName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents CalledNumber As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents CalledName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DestinationNumber As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DestinationName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents StartTime As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ScriptConnectTime As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DeliveredTime As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ConnectTime As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents EndTime As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents currency As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents costs As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents state As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents publicAccessPrefix As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents lcrProvider As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents projectNumber As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents aoc As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents originationDevice As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents destinationDevice As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents transferredByNumber As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents transferredByName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents transferredCallId1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents transferredCallId2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents transferredToCallId As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents transferTime As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents disconnectReason As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents destNameTextBox As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents destNumberTextBox As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents calledNameTextBox As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents calledNumberTextBox As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents origNameTextBox As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents origNumberTextBox As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents callIdTextBox As System.Windows.Forms.TextBox
    Friend WithEvents searchButton As System.Windows.Forms.Button
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents clearButton As System.Windows.Forms.Button
    Friend WithEvents customConditionsTextBox As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents disconnectReasonComboBox As System.Windows.Forms.ComboBox
End Class
