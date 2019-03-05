Imports System.Windows
Imports System.Windows.Interop

Public Class BandControl

    Sub New()

        ' 此调用是设计器所必需的。
        InitializeComponent()

        ' 在 InitializeComponent() 调用之后添加任何初始化。

    End Sub

    Public ReadOnly Property Handle As IntPtr
        Get
            Return DirectCast(PresentationSource.FromVisual(Me), HwndSource).Handle
        End Get
    End Property
End Class
