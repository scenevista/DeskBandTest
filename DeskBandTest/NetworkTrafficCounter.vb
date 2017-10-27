Imports System.Net.NetworkInformation
Imports System.Threading

Public Class NetworkTrafficCounter

    Public Sub New()
        timer = New Timer(New TimerCallback(AddressOf TimerCallback))
        receiveCounters = New List(Of PerformanceCounter)
        sendCounters = New List(Of PerformanceCounter)
        InitializeBaseCounters()
        RefreshCounters()
        TickSpan = 500
    End Sub

    Public Event Tick(ByVal upSpeed As Long, ByVal downSpeed As Long)
    Public Event NetworkInterfaceChanged()

    Protected timer As Timer
    Protected receiveCounters As List(Of PerformanceCounter)
    Protected sendCounters As List(Of PerformanceCounter)
    Protected baseSendBytes As Long
    Protected baseReceiveBytes As Long
    Private _tickSpan As Integer
    Private Property TickSpan As Integer
        Get
            Return _tickSpan
        End Get
        Set(value As Integer)
            _tickSpan = value
            timer.Change(0, _tickSpan)
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
End Class
