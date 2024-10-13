<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class VariablesForm
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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(VariablesForm))
        Me.serviceWideVariablesDataGridView = New System.Windows.Forms.DataGridView
        Me.myName = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.defaultValue = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Type = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.deletable = New System.Windows.Forms.DataGridViewCheckBoxColumn
        Me.allowEmptyString = New System.Windows.Forms.DataGridViewCheckBoxColumn
        Me.sanityCheckForNonEmptyValue = New System.Windows.Forms.DataGridViewCheckBoxColumn
        Me.TabControl1 = New System.Windows.Forms.TabControl
        Me.TabPage1 = New System.Windows.Forms.TabPage
        Me.deleteVarButton = New System.Windows.Forms.Button
        Me.editVarButton = New System.Windows.Forms.Button
        Me.addVarButton = New System.Windows.Forms.Button
        Me.TabPage2 = New System.Windows.Forms.TabPage
        Me.editDDIVarButton = New System.Windows.Forms.Button
        Me.deleteDDIVarButton = New System.Windows.Forms.Button
        Me.deleteDDIKeyButton = New System.Windows.Forms.Button
        Me.addDDIVarButton = New System.Windows.Forms.Button
        Me.ddiVariablesLabel = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.ddiVariablesDataGridView = New System.Windows.Forms.DataGridView
        Me.varName = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.varValue = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.ddiKeysDataGridView = New System.Windows.Forms.DataGridView
        Me.myDescription = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.myKey = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Label1 = New System.Windows.Forms.Label
        Me.swyxVariableNameComboBox = New System.Windows.Forms.ComboBox
        Me.okButton = New System.Windows.Forms.Button
        Me.cancelButton = New System.Windows.Forms.Button
        CType(Me.serviceWideVariablesDataGridView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        CType(Me.ddiVariablesDataGridView, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ddiKeysDataGridView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'serviceWideVariablesDataGridView
        '
        Me.serviceWideVariablesDataGridView.AllowUserToAddRows = False
        Me.serviceWideVariablesDataGridView.AllowUserToDeleteRows = False
        Me.serviceWideVariablesDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.serviceWideVariablesDataGridView.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.myName, Me.defaultValue, Me.Type, Me.deletable, Me.allowEmptyString, Me.sanityCheckForNonEmptyValue})
        Me.serviceWideVariablesDataGridView.Location = New System.Drawing.Point(25, 60)
        Me.serviceWideVariablesDataGridView.Name = "serviceWideVariablesDataGridView"
        Me.serviceWideVariablesDataGridView.RowHeadersVisible = False
        Me.serviceWideVariablesDataGridView.Size = New System.Drawing.Size(803, 299)
        Me.serviceWideVariablesDataGridView.TabIndex = 0
        '
        'myName
        '
        Me.myName.HeaderText = "Name"
        Me.myName.Name = "myName"
        Me.myName.Width = 200
        '
        'defaultValue
        '
        Me.defaultValue.HeaderText = "Value"
        Me.defaultValue.Name = "defaultValue"
        Me.defaultValue.Width = 200
        '
        'Type
        '
        Me.Type.HeaderText = "Type"
        Me.Type.Name = "Type"
        '
        'deletable
        '
        Me.deletable.HeaderText = "Deletable"
        Me.deletable.Name = "deletable"
        Me.deletable.ReadOnly = True
        '
        'allowEmptyString
        '
        Me.allowEmptyString.HeaderText = "Allow Empty String"
        Me.allowEmptyString.Name = "allowEmptyString"
        Me.allowEmptyString.ReadOnly = True
        '
        'sanityCheckForNonEmptyValue
        '
        Me.sanityCheckForNonEmptyValue.HeaderText = "Sanity Check For Non Empty Value"
        Me.sanityCheckForNonEmptyValue.Name = "sanityCheckForNonEmptyValue"
        Me.sanityCheckForNonEmptyValue.ReadOnly = True
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Location = New System.Drawing.Point(29, 29)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(859, 430)
        Me.TabControl1.TabIndex = 1
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.deleteVarButton)
        Me.TabPage1.Controls.Add(Me.editVarButton)
        Me.TabPage1.Controls.Add(Me.addVarButton)
        Me.TabPage1.Controls.Add(Me.serviceWideVariablesDataGridView)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(851, 404)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Service Wide"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'deleteVarButton
        '
        Me.deleteVarButton.Location = New System.Drawing.Point(753, 16)
        Me.deleteVarButton.Name = "deleteVarButton"
        Me.deleteVarButton.Size = New System.Drawing.Size(75, 23)
        Me.deleteVarButton.TabIndex = 3
        Me.deleteVarButton.Text = "Delete"
        Me.deleteVarButton.UseVisualStyleBackColor = True
        '
        'editVarButton
        '
        Me.editVarButton.Location = New System.Drawing.Point(149, 16)
        Me.editVarButton.Name = "editVarButton"
        Me.editVarButton.Size = New System.Drawing.Size(75, 23)
        Me.editVarButton.TabIndex = 2
        Me.editVarButton.Text = "Edit .."
        Me.editVarButton.UseVisualStyleBackColor = True
        '
        'addVarButton
        '
        Me.addVarButton.Location = New System.Drawing.Point(25, 16)
        Me.addVarButton.Name = "addVarButton"
        Me.addVarButton.Size = New System.Drawing.Size(75, 23)
        Me.addVarButton.TabIndex = 1
        Me.addVarButton.Text = "Add .."
        Me.addVarButton.UseVisualStyleBackColor = True
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.editDDIVarButton)
        Me.TabPage2.Controls.Add(Me.deleteDDIVarButton)
        Me.TabPage2.Controls.Add(Me.deleteDDIKeyButton)
        Me.TabPage2.Controls.Add(Me.addDDIVarButton)
        Me.TabPage2.Controls.Add(Me.ddiVariablesLabel)
        Me.TabPage2.Controls.Add(Me.Label2)
        Me.TabPage2.Controls.Add(Me.ddiVariablesDataGridView)
        Me.TabPage2.Controls.Add(Me.ddiKeysDataGridView)
        Me.TabPage2.Controls.Add(Me.Label1)
        Me.TabPage2.Controls.Add(Me.swyxVariableNameComboBox)
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(851, 404)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Per DDI"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'editDDIVarButton
        '
        Me.editDDIVarButton.Location = New System.Drawing.Point(472, 375)
        Me.editDDIVarButton.Name = "editDDIVarButton"
        Me.editDDIVarButton.Size = New System.Drawing.Size(75, 23)
        Me.editDDIVarButton.TabIndex = 11
        Me.editDDIVarButton.Text = "Edit"
        Me.editDDIVarButton.UseVisualStyleBackColor = True
        '
        'deleteDDIVarButton
        '
        Me.deleteDDIVarButton.Location = New System.Drawing.Point(564, 375)
        Me.deleteDDIVarButton.Name = "deleteDDIVarButton"
        Me.deleteDDIVarButton.Size = New System.Drawing.Size(75, 23)
        Me.deleteDDIVarButton.TabIndex = 10
        Me.deleteDDIVarButton.Text = "Delete"
        Me.deleteDDIVarButton.UseVisualStyleBackColor = True
        '
        'deleteDDIKeyButton
        '
        Me.deleteDDIKeyButton.Location = New System.Drawing.Point(243, 375)
        Me.deleteDDIKeyButton.Name = "deleteDDIKeyButton"
        Me.deleteDDIKeyButton.Size = New System.Drawing.Size(75, 23)
        Me.deleteDDIKeyButton.TabIndex = 9
        Me.deleteDDIKeyButton.Text = "Delete"
        Me.deleteDDIKeyButton.UseVisualStyleBackColor = True
        '
        'addDDIVarButton
        '
        Me.addDDIVarButton.Location = New System.Drawing.Point(379, 375)
        Me.addDDIVarButton.Name = "addDDIVarButton"
        Me.addDDIVarButton.Size = New System.Drawing.Size(75, 23)
        Me.addDDIVarButton.TabIndex = 8
        Me.addDDIVarButton.Text = "Add"
        Me.addDDIVarButton.UseVisualStyleBackColor = True
        '
        'ddiVariablesLabel
        '
        Me.ddiVariablesLabel.AutoSize = True
        Me.ddiVariablesLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ddiVariablesLabel.Location = New System.Drawing.Point(376, 68)
        Me.ddiVariablesLabel.Name = "ddiVariablesLabel"
        Me.ddiVariablesLabel.Size = New System.Drawing.Size(85, 13)
        Me.ddiVariablesLabel.TabIndex = 5
        Me.ddiVariablesLabel.Text = "DDI Variables"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(23, 68)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(60, 13)
        Me.Label2.TabIndex = 4
        Me.Label2.Text = "DDI Keys"
        '
        'ddiVariablesDataGridView
        '
        Me.ddiVariablesDataGridView.AllowUserToAddRows = False
        Me.ddiVariablesDataGridView.AllowUserToDeleteRows = False
        Me.ddiVariablesDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.ddiVariablesDataGridView.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.varName, Me.varValue})
        Me.ddiVariablesDataGridView.Location = New System.Drawing.Point(379, 84)
        Me.ddiVariablesDataGridView.Name = "ddiVariablesDataGridView"
        Me.ddiVariablesDataGridView.RowHeadersVisible = False
        Me.ddiVariablesDataGridView.Size = New System.Drawing.Size(452, 276)
        Me.ddiVariablesDataGridView.TabIndex = 3
        '
        'varName
        '
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.Color.Blue
        Me.varName.DefaultCellStyle = DataGridViewCellStyle1
        Me.varName.HeaderText = "Variable Name"
        Me.varName.Name = "varName"
        Me.varName.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.varName.Width = 145
        '
        'varValue
        '
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.Color.Red
        Me.varValue.DefaultCellStyle = DataGridViewCellStyle2
        Me.varValue.HeaderText = "Variable Value"
        Me.varValue.Name = "varValue"
        Me.varValue.Width = 304
        '
        'ddiKeysDataGridView
        '
        Me.ddiKeysDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.ddiKeysDataGridView.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.myDescription, Me.myKey})
        Me.ddiKeysDataGridView.Location = New System.Drawing.Point(20, 84)
        Me.ddiKeysDataGridView.Name = "ddiKeysDataGridView"
        Me.ddiKeysDataGridView.RowHeadersVisible = False
        Me.ddiKeysDataGridView.Size = New System.Drawing.Size(298, 276)
        Me.ddiKeysDataGridView.TabIndex = 2
        '
        'myDescription
        '
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle3.ForeColor = System.Drawing.Color.Blue
        Me.myDescription.DefaultCellStyle = DataGridViewCellStyle3
        Me.myDescription.HeaderText = "Description"
        Me.myDescription.Name = "myDescription"
        Me.myDescription.Width = 147
        '
        'myKey
        '
        DataGridViewCellStyle4.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle4.ForeColor = System.Drawing.Color.Red
        Me.myKey.DefaultCellStyle = DataGridViewCellStyle4
        Me.myKey.HeaderText = "Key Value"
        Me.myKey.Name = "myKey"
        Me.myKey.Width = 147
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(17, 11)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(122, 13)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Swyx Variable Name"
        '
        'swyxVariableNameComboBox
        '
        Me.swyxVariableNameComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.swyxVariableNameComboBox.FormattingEnabled = True
        Me.swyxVariableNameComboBox.Location = New System.Drawing.Point(20, 27)
        Me.swyxVariableNameComboBox.Name = "swyxVariableNameComboBox"
        Me.swyxVariableNameComboBox.Size = New System.Drawing.Size(298, 21)
        Me.swyxVariableNameComboBox.Sorted = True
        Me.swyxVariableNameComboBox.TabIndex = 0
        '
        'okButton
        '
        Me.okButton.Location = New System.Drawing.Point(809, 479)
        Me.okButton.Name = "okButton"
        Me.okButton.Size = New System.Drawing.Size(75, 23)
        Me.okButton.TabIndex = 2
        Me.okButton.Text = "OK"
        Me.okButton.UseVisualStyleBackColor = True
        '
        'cancelButton
        '
        Me.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.cancelButton.Location = New System.Drawing.Point(711, 479)
        Me.cancelButton.Name = "cancelButton"
        Me.cancelButton.Size = New System.Drawing.Size(75, 23)
        Me.cancelButton.TabIndex = 3
        Me.cancelButton.Text = "Cancel"
        Me.cancelButton.UseVisualStyleBackColor = True
        '
        'VariablesForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(900, 514)
        Me.ControlBox = False
        Me.Controls.Add(Me.cancelButton)
        Me.Controls.Add(Me.okButton)
        Me.Controls.Add(Me.TabControl1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "VariablesForm"
        Me.Text = "Variables"
        CType(Me.serviceWideVariablesDataGridView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage2.ResumeLayout(False)
        Me.TabPage2.PerformLayout()
        CType(Me.ddiVariablesDataGridView, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ddiKeysDataGridView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents serviceWideVariablesDataGridView As System.Windows.Forms.DataGridView
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents swyxVariableNameComboBox As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents ddiKeysDataGridView As System.Windows.Forms.DataGridView
    Friend WithEvents ddiVariablesDataGridView As System.Windows.Forms.DataGridView
    Friend WithEvents ddiVariablesLabel As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents addDDIVarButton As System.Windows.Forms.Button
    Friend WithEvents addVarButton As System.Windows.Forms.Button
    Friend WithEvents editVarButton As System.Windows.Forms.Button
    Friend WithEvents deleteVarButton As System.Windows.Forms.Button
    Friend WithEvents deleteDDIKeyButton As System.Windows.Forms.Button
    Friend WithEvents deleteDDIVarButton As System.Windows.Forms.Button
    Friend WithEvents editDDIVarButton As System.Windows.Forms.Button
    Friend WithEvents okButton As System.Windows.Forms.Button
    Friend Shadows WithEvents cancelButton As System.Windows.Forms.Button
    Friend WithEvents myDescription As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents myKey As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents myName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents defaultValue As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Type As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents deletable As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents allowEmptyString As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents sanityCheckForNonEmptyValue As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents varName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents varValue As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
