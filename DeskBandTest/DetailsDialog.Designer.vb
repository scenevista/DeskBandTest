<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class DetailsDialog
    Inherits System.Windows.Forms.Form

    'Form 重写 Dispose，以清理组件列表。
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Windows 窗体设计器所必需的
    Private components As System.ComponentModel.IContainer

    '注意: 以下过程是 Windows 窗体设计器所必需的
    '可以使用 Windows 窗体设计器修改它。  
    '不要使用代码编辑器修改它。
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.LabelTotalUpload = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.LabelTotalDownload = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.LabelSessionUpload = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.LabelSessionDownload = New System.Windows.Forms.Label()
        Me.btn_close = New System.Windows.Forms.Button()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 2
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.Label1, 0, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.LabelTotalUpload, 1, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.Label3, 0, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.LabelTotalDownload, 1, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.Label5, 0, 3)
        Me.TableLayoutPanel1.Controls.Add(Me.LabelSessionUpload, 1, 3)
        Me.TableLayoutPanel1.Controls.Add(Me.Label7, 0, 4)
        Me.TableLayoutPanel1.Controls.Add(Me.LabelSessionDownload, 1, 4)
        Me.TableLayoutPanel1.Controls.Add(Me.btn_close, 1, 5)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 6
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 80.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 100.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(618, 424)
        Me.TableLayoutPanel1.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(114, 101)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(80, 18)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "累计上传"
        '
        'LabelTotalUpload
        '
        Me.LabelTotalUpload.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.LabelTotalUpload.AutoSize = True
        Me.LabelTotalUpload.Location = New System.Drawing.Point(312, 101)
        Me.LabelTotalUpload.Name = "LabelTotalUpload"
        Me.LabelTotalUpload.Size = New System.Drawing.Size(152, 18)
        Me.LabelTotalUpload.TabIndex = 1
        Me.LabelTotalUpload.Text = "LabelTotalUpload"
        '
        'Label3
        '
        Me.Label3.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(114, 162)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(80, 18)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "累计下载"
        '
        'LabelTotalDownload
        '
        Me.LabelTotalDownload.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.LabelTotalDownload.AutoSize = True
        Me.LabelTotalDownload.Location = New System.Drawing.Point(312, 162)
        Me.LabelTotalDownload.Name = "LabelTotalDownload"
        Me.LabelTotalDownload.Size = New System.Drawing.Size(170, 18)
        Me.LabelTotalDownload.TabIndex = 3
        Me.LabelTotalDownload.Text = "LabelTotalDownload"
        '
        'Label5
        '
        Me.Label5.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(114, 223)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(80, 18)
        Me.Label5.TabIndex = 4
        Me.Label5.Text = "本次上传"
        '
        'LabelSessionUpload
        '
        Me.LabelSessionUpload.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.LabelSessionUpload.AutoSize = True
        Me.LabelSessionUpload.Location = New System.Drawing.Point(312, 223)
        Me.LabelSessionUpload.Name = "LabelSessionUpload"
        Me.LabelSessionUpload.Size = New System.Drawing.Size(170, 18)
        Me.LabelSessionUpload.TabIndex = 5
        Me.LabelSessionUpload.Text = "LabelSessionUpload"
        '
        'Label7
        '
        Me.Label7.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(114, 284)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(80, 18)
        Me.Label7.TabIndex = 6
        Me.Label7.Text = "本次下载"
        '
        'LabelSessionDownload
        '
        Me.LabelSessionDownload.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.LabelSessionDownload.AutoSize = True
        Me.LabelSessionDownload.Location = New System.Drawing.Point(312, 284)
        Me.LabelSessionDownload.Name = "LabelSessionDownload"
        Me.LabelSessionDownload.Size = New System.Drawing.Size(188, 18)
        Me.LabelSessionDownload.TabIndex = 7
        Me.LabelSessionDownload.Text = "LabelSessionDownload"
        '
        'btn_close
        '
        Me.btn_close.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.btn_close.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btn_close.Location = New System.Drawing.Point(403, 350)
        Me.btn_close.Name = "btn_close"
        Me.btn_close.Size = New System.Drawing.Size(120, 48)
        Me.btn_close.TabIndex = 8
        Me.btn_close.Text = "确定"
        Me.btn_close.UseVisualStyleBackColor = True
        '
        'DetailsDialog
        '
        Me.AcceptButton = Me.btn_close
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 18.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(618, 424)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.MaximumSize = New System.Drawing.Size(640, 480)
        Me.MinimumSize = New System.Drawing.Size(640, 480)
        Me.Name = "DetailsDialog"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "详细信息"
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents TableLayoutPanel1 As Windows.Forms.TableLayoutPanel
    Friend WithEvents Label1 As Windows.Forms.Label
    Friend WithEvents LabelTotalUpload As Windows.Forms.Label
    Friend WithEvents Label3 As Windows.Forms.Label
    Friend WithEvents LabelTotalDownload As Windows.Forms.Label
    Friend WithEvents Label5 As Windows.Forms.Label
    Friend WithEvents LabelSessionUpload As Windows.Forms.Label
    Friend WithEvents Label7 As Windows.Forms.Label
    Friend WithEvents LabelSessionDownload As Windows.Forms.Label
    Friend WithEvents btn_close As Windows.Forms.Button
End Class
