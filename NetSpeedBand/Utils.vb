Module Utils
    Public Function GetDataSizeString(value As Long) As String
        Select Case value
            Case Is < 1024
                Return value.ToString("0 B")
            Case Is < 1048576
                Return (value / 1024).ToString("0.0 KB")
            Case Is < 1073741824
                Return (value / 1048576).ToString("0.0 MB")
            Case Is < 1099511627776
                Return (value / 1073741824).ToString("0.0 GB")
            Case Else
                Return (value / 1099511627776).ToString("0.0 TB")
        End Select
    End Function

    Public Function GetSpeedString(value As Long) As String
        Return GetDataSizeString(value) & "/s"
    End Function
End Module
