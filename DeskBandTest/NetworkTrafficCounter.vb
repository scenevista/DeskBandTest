Imports System.Net.NetworkInformation
Imports System.Threading

Friend NotInheritable Class NetworkTrafficCounter
    Implements IDisposable

    Public Sub New()
        timer = New Timer(New TimerCallback(AddressOf TimerCallback))
        TickSpan = 1000
        receiveCounters = New List(Of PerformanceCounter)
        sendCounters = New List(Of PerformanceCounter)
        InitializeBaseCounters()
        RefreshCounters()
    End Sub

    Public Event Tick(ByVal upSpeed As Long, ByVal downSpeed As Long)
    Public Event NetworkInterfaceChanged()

    Private timer As Timer
    Private receiveCounters As List(Of PerformanceCounter)
    Private sendCounters As List(Of PerformanceCounter)
    Private baseSendBytes As Long
    Private baseReceiveBytes As Long
    Private _tickSpan As Integer
    Private Property TickSpan As Integer
        Get
            Return _tickSpan
        End Get
        Set(value As Integer)
            _tickSpan = value
            timer.Change(value, value)
        End Set
    End Property

    Public ReadOnly Property SentBytes As Long
        Get
            Dim interfaces() As NetworkInterface = NetworkInterface.GetAllNetworkInterfaces()
            Dim snd As Long = 0
            For Each inf As NetworkInterface In interfaces
                snd += inf.GetIPStatistics.BytesSent
            Next
            Return snd - baseSendBytes
        End Get
    End Property

    Public ReadOnly Property ReceivedBytes As Long
        Get
            Dim interfaces() As NetworkInterface = NetworkInterface.GetAllNetworkInterfaces()
            Dim rec As Long = 0
            For Each inf As NetworkInterface In interfaces
                rec += inf.GetIPStatistics.BytesReceived
            Next
            Return rec - baseReceiveBytes
        End Get
    End Property

    Private Sub TimerCallback(state As Object)
        Dim snd As Long = 0, rec As Long = 0
        For Each counter As PerformanceCounter In sendCounters
            snd += counter.NextValue()
        Next
        For Each counter As PerformanceCounter In receiveCounters
            rec += counter.NextValue()
        Next
        RaiseEvent Tick(snd, rec)
    End Sub

    Private Sub RefreshCounters()
        '清理所有计数器
        For Each counter As PerformanceCounter In sendCounters
            counter.Close()
        Next
        sendCounters.Clear()
        For Each counter As PerformanceCounter In receiveCounters
            counter.Close()
        Next
        receiveCounters.Clear()

        '创建新的计数器
        Dim cat As New PerformanceCounterCategory("Network Interface")
        Dim insts() = cat.GetInstanceNames
        For Each instance As String In insts
            sendCounters.Add(New PerformanceCounter("Network Interface", "Bytes Sent/sec", instance))
            receiveCounters.Add(New PerformanceCounter("Network Interface", "Bytes Received/sec", instance))
        Next

    End Sub

    Public Sub InitializeBaseCounters()
        Dim interfaces() As NetworkInterface = NetworkInterface.GetAllNetworkInterfaces()
        Dim rec As Long = 0, snd As Long = 0
        For Each inf As NetworkInterface In interfaces
            rec += inf.GetIPStatistics.BytesReceived
            snd += inf.GetIPStatistics.BytesSent
        Next
        baseReceiveBytes = rec
        baseSendBytes = snd
    End Sub

#Region "IDisposable Support"
    Private disposedValue As Boolean ' 要检测冗余调用

    ' IDisposable
    Protected Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                ' TODO: 释放托管状态(托管对象)。

                '停止计时器
                TickSpan = -1
                timer.Dispose()

                '清理所有计数器
                For Each counter As PerformanceCounter In sendCounters
                    counter.Close()
                Next
                sendCounters.Clear()

                For Each counter As PerformanceCounter In receiveCounters
                    counter.Close()
                Next
                receiveCounters.Clear()
            End If

            ' TODO: 释放未托管资源(未托管对象)并在以下内容中替代 Finalize()。
            ' TODO: 将大型字段设置为 null。
        End If
        disposedValue = True
    End Sub

    ' TODO: 仅当以上 Dispose(disposing As Boolean)拥有用于释放未托管资源的代码时才替代 Finalize()。
    'Protected Overrides Sub Finalize()
    '    ' 请勿更改此代码。将清理代码放入以上 Dispose(disposing As Boolean)中。
    '    Dispose(False)
    '    MyBase.Finalize()
    'End Sub

    ' Visual Basic 添加此代码以正确实现可释放模式。
    Public Sub Dispose() Implements IDisposable.Dispose
        ' 请勿更改此代码。将清理代码放入以上 Dispose(disposing As Boolean)中。
        Dispose(True)
        ' TODO: 如果在以上内容中替代了 Finalize()，则取消注释以下行。
        ' GC.SuppressFinalize(Me)
    End Sub
#End Region
End Class
