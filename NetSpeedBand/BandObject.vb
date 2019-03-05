Imports System.Runtime.InteropServices
Imports NetSpeedBand

<ComVisible(True), Guid("0E75A60B-965E-4FFA-8CC1-94D7DCBE1126")>
Public Class BandObject
    Implements IOleWindow, IDockingWindow, IDeskBand, IObjectWithSite, IPersist, IPersistStream, IDeskBand2

#Region "私有变量"
    Private bandWindow As Band
    Private m_composition As Boolean
    Private parentWindowHandle As IntPtr
    Private psite As IntPtr

    Shared ReadOnly Property Title As String = "小工具"
    Shared ReadOnly Property Help As String = "网速监控小工具"
#End Region

#Region "API函数调用"
    Private Declare Auto Function SetParent Lib "user32.dll" (ByVal hWndChild As IntPtr, ByVal hWndNewParent As IntPtr) As <MarshalAs(UnmanagedType.Bool)> Boolean
    Private Declare Function MonitorFromWindow Lib "user32.dll" (hwnd As IntPtr, dwFlags As HMONITOR_OPTION) As IntPtr
    Private Declare Function GetDesktopWindow Lib "user32.dll" () As IntPtr
#End Region

#Region "Dll注册卸载函数"
    <ComRegisterFunction> Public Shared Sub ClassRegister(t As Type)

        Dim guid As String = t.GUID.ToString("B")
        Dim rkClass = My.Computer.Registry.ClassesRoot.OpenSubKey("CLSID", True).CreateSubKey(guid, True)
        Dim rkCat = rkClass.CreateSubKey("Implemented Categories", True)
        rkClass.Flush()
        rkClass.SetValue(Nothing, Title)
        rkClass.SetValue("MenuText", Title)
        rkClass.SetValue("HelpText", Help)

        rkCat.CreateSubKey("{00021492-0000-0000-C000-000000000046}")
        rkClass.Flush()
        rkCat.Flush()
    End Sub

    <ComUnregisterFunction> Public Shared Sub ClassUnRegister(t As Type)
        Dim guid As String = t.GUID.ToString("B")

        My.Computer.Registry.ClassesRoot.DeleteSubKeyTree("CLSID\" & guid)
    End Sub
#End Region


    Sub New()
        m_composition = True
        parentWindowHandle = IntPtr.Zero
        psite = IntPtr.Zero
        bandWindow = New Band
        bandWindow.Show()
        bandWindow.Hide()
    End Sub

    Public Function SetSite(<[In]> <MarshalAs(UnmanagedType.Interface)> pUnkSite As IOleWindow) As HResult Implements IObjectWithSite.SetSite
        If psite <> IntPtr.Zero Then
            Try
                Marshal.Release(psite)
            Catch ex As COMException

            Finally
                psite = IntPtr.Zero
            End Try
        End If

        If pUnkSite Is Nothing Then
            parentWindowHandle = IntPtr.Zero
            bandWindow = Nothing
            GC.Collect()
        Else
            Try
                pUnkSite.GetWindow(parentWindowHandle)
                SetParent(bandWindow.Handle, parentWindowHandle)
                psite = Marshal.GetIUnknownForObject(pUnkSite)
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        End If

        Return HResult.S_OK
    End Function

    Public Function GetSite(ByRef riid As Guid, <Out> ByRef ppvSite As IntPtr) As HResult Implements IObjectWithSite.GetSite
        Return Marshal.QueryInterface(psite, riid, ppvSite)
    End Function

    Public Function GetClassID(ByRef pClassID As Guid) As HResult Implements IPersist.GetClassID, IPersistStream.GetClassID
        pClassID = Me.GetType.GUID
        Return HResult.S_OK
    End Function

    Public Function IsDirty() As Integer Implements IPersistStream.IsDirty
        Return 0
    End Function

    Public Function Load(<[In]> <MarshalAs(UnmanagedType.Interface)> pStm As IStream) As HResult Implements IPersistStream.Load
        Return HResult.S_OK
    End Function

    Public Function Save(<[In]> <MarshalAs(UnmanagedType.Interface)> pStm As IStream, <[In]> fClearDirty As Boolean) As HResult Implements IPersistStream.Save
        Return HResult.S_OK
    End Function

    Public Function GetSizeMax(<Out> ByRef pcbSize As ULong) As HResult Implements IPersistStream.GetSizeMax
        pcbSize = 0
        Return HResult.S_OK
    End Function

    Private Function GetWindow(ByRef phwnd As IntPtr) As HResult Implements IOleWindow.GetWindow, IDockingWindow.GetWindow, IDeskBand.GetWindow, IDeskBand2.GetWindow
        phwnd = bandWindow.Handle
        Return HResult.S_OK
    End Function

    Private Function ContextSensitiveHelp(<[In]> <MarshalAs(UnmanagedType.Bool)> fEnterMode As Boolean) As HResult Implements IOleWindow.ContextSensitiveHelp, IDockingWindow.ContextSensitiveHelp, IDeskBand.ContextSensitiveHelp, IDeskBand2.ContextSensitiveHelp
        Return HResult.E_NOTIMPL
    End Function

    Private Function ShowDW(<[In]> <MarshalAs(UnmanagedType.Bool)> fShow As Boolean) As HResult Implements IDockingWindow.ShowDW, IDeskBand.ShowDW, IDeskBand2.ShowDW
        If fShow Then
            bandWindow.Show()
        Else
            bandWindow.Hide()
        End If
        Return HResult.S_OK
    End Function

    Private Function CloseDW(<[In]> dwReserved As UInteger) As HResult Implements IDockingWindow.CloseDW, IDeskBand.CloseDW, IDeskBand2.CloseDW
        'bandWindow.Hide()
        bandWindow.Close()
        Return HResult.S_OK
    End Function

    Private Function ResizeBorderDW(<MarshalAs(UnmanagedType.LPStruct)> <Out> ByRef prcBorder As RECT, <[In]> <MarshalAs(UnmanagedType.IUnknown)> punkToolbarSite As Object, <MarshalAs(UnmanagedType.Bool)> fReserved As Boolean) As HResult Implements IDockingWindow.ResizeBorderDW, IDeskBand.ResizeBorderDW, IDeskBand2.ResizeBorderDW
        Return HResult.E_NOTIMPL
    End Function

    Private Function GetBandInfo(dwBandID As UInteger, dwViewMode As UInteger, ByRef pdbi As DESKBANDINFO) As HResult Implements IDeskBand.GetBandInfo, IDeskBand2.GetBandInfo
        pdbi.wszTitle = Title

        pdbi.ptActual.x = 88
        pdbi.ptActual.y = 40

        pdbi.ptMaxSize.x = 200
        pdbi.ptMaxSize.y = 40

        pdbi.ptMinSize.x = 84
        pdbi.ptMinSize.y = 40

        pdbi.ptIntegral.x = 1
        pdbi.ptIntegral.y = 1

        pdbi.dwModeFlags = pdbi.dwModeFlags Or DBIM.TITLE Or DBIM.ACTUAL Or DBIM.MAXSIZE Or DBIM.MINSIZE

        Return HResult.S_OK
    End Function

    Public Function CanRenderComposited(<MarshalAs(UnmanagedType.Bool)> <Out> ByRef pfCanRenderComposited As Boolean) As HResult Implements IDeskBand2.CanRenderComposited
        pfCanRenderComposited = True
        Return HResult.S_OK
    End Function

    Public Function SetCompositionState(<[In]> <MarshalAs(UnmanagedType.Bool)> fCompositionEnabled As Boolean) As HResult Implements IDeskBand2.SetCompositionState
        m_composition = fCompositionEnabled
        If m_composition Then
            bandWindow.Background = Windows.Media.Brushes.Black
        Else
            bandWindow.Background = Windows.Media.Brushes.DarkSlateGray
        End If
        Return HResult.S_OK
    End Function

    Public Function GetCompositionState(<MarshalAs(UnmanagedType.Bool)> <Out> ByRef pfCompositionEnabled As Boolean) As HResult Implements IDeskBand2.GetCompositionState
        pfCompositionEnabled = m_composition
        Return HResult.S_OK
    End Function
End Class
