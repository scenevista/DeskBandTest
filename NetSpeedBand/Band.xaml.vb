Imports System.ComponentModel
Imports System.Runtime.CompilerServices
Imports System.Windows.Interop

Public Class Band
    Sub New()

        ' 此调用是设计器所必需的。
        InitializeComponent()

        ' 在 InitializeComponent() 调用之后添加任何初始化。
        interopHelper = New WindowInteropHelper(Me)
    End Sub

    Private interopHelper As WindowInteropHelper

    Public ReadOnly Property Handle As IntPtr
        Get
            Return interopHelper.Handle
        End Get
    End Property
End Class

Class BandModel
    Implements INotifyPropertyChanged

    Private _UpSpeed As Long
    Private _DnSpeed As Long


    Public Sub SetUpSpeed(val As Long)
        _UpSpeed = val
        OnPropertyChanged("UpSpeed")
    End Sub

    Public Sub SetDnSpeed(val As Long)
        _DnSpeed = val
        OnPropertyChanged("DnSpeed")
    End Sub

    ReadOnly Property UpSpeed As String
        Get
            Return GetSpeedString(_UpSpeed)
        End Get
    End Property

    ReadOnly Property DnSpeed As String
        Get
            Return GetSpeedString(_DnSpeed)
        End Get
    End Property

    Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged
    Protected Friend Sub OnPropertyChanged(Optional ByVal callMemberName As String = "")
        RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(callMemberName))
    End Sub
End Class