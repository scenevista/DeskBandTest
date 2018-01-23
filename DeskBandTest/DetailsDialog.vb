Public Class DetailsDialog

    Public Sub New(info As NetworkBandwidthDetial)

        ' 此调用是设计器所必需的。
        InitializeComponent()

        ' 在 InitializeComponent() 调用之后添加任何初始化。
        LabelSessionDownload.Text = GetDataSizeString(info.SessionDownloadBytes)
        LabelSessionUpload.Text = GetDataSizeString(info.SessionUploadBytes)
        LabelTotalDownload.Text = GetDataSizeString(info.TotalDownloadBytes)
        LabelTotalUpload.Text = GetDataSizeString(info.TotalUploadBytes)
    End Sub

    Private Sub btn_close_Click(sender As Object, e As EventArgs) Handles btn_close.Click
        Close()
    End Sub

    Private Function GetDataSizeString(speed As Long) As String
        Select Case speed
            Case Is < 1024
                Return speed.ToString("0 B")
            Case Is < 1048576
                Return (speed / 1024).ToString("0.0 KB")
            Case Is < 1073741824
                Return (speed / 1048576).ToString("0.0 MB")
            Case Is < 1099511627776
                Return (speed / 1073741824).ToString("0.0 GB")
            Case Else
                Return (speed / 1099511627776).ToString("0.0 TB")
        End Select
    End Function
End Class