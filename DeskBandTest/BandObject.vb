Imports System.Drawing
Imports System.Runtime.InteropServices
Imports System.Windows.Forms
Imports DeskBandTest

<ComVisible(True), Guid(BandObject.ClassId)>
Public Class BandObject
    Inherits UserControl
    Implements IOleWindow, IDockingWindow, IDeskBand, IObjectWithSite, IPersist, IPersistStream, IDeskBand2

    Friend Shared ReadOnly Title As String = "网速监控" '显示在选项里面的名称
    Friend Shared ReadOnly Help As String = "测试工具条"

    Private BandObjectSite As IntPtr = IntPtr.Zero
    Private parentWindowHandle As IntPtr = IntPtr.Zero
    Private mRenderComposited As Boolean = True
    Private mSettings As New Settings
    Private mSettingsChanged As Boolean = False
    Private mCounter As NetworkTrafficCounter

#Region "Properties"
    Public ReadOnly Property TotalBytesDown As Long
        Get
            Return mSettings.totalDownloadedBytes + mCounter.ReceivedBytes
        End Get
    End Property
    Public ReadOnly Property TotalBytesUp As Long
        Get
            Return mSettings.totalUploadedBytes + mCounter.SentBytes
        End Get
    End Property
#End Region
#Region "Native Methods"
    Private Declare Auto Function SetParent Lib "user32.dll" (ByVal hWndChild As IntPtr, ByVal hWndNewParent As IntPtr) As <MarshalAs(UnmanagedType.Bool)> Boolean
    Private Declare Function MonitorFromWindow Lib "user32.dll" (hwnd As IntPtr, dwFlags As HMONITOR_OPTION) As IntPtr
    Private Declare Function GetDesktopWindow Lib "user32.dll" () As IntPtr
    Private Declare Function GetScaleFactorForMonitor Lib "Shcore.dll" (hMonitor As IntPtr, ByRef pScale As Integer) As HResult
#End Region
#Region "Const"
    Private ReadOnly CATID_DESKBAND As Guid = New Guid("00021492-0000-0000-C000-000000000046")

#End Region
#Region "COM GUIDs"
    ' 这些 GUID 提供此类的 COM 标识 
    ' 及其 COM 接口。若更改它们，则现有的
    ' 客户端将不再能访问此类。
    Public Const ClassId As String = "29662d0a-3784-4737-8d05-6de97d9d0a4b"
    Public Const EventsId As String = "272c1cfa-5290-427f-98cb-33756d471171"
    Friend WithEvents LCapDn As Label
    Private components As System.ComponentModel.IContainer
    Friend WithEvents LUpSpeed As Label
    Friend WithEvents LDnSpeed As Label
    Friend WithEvents LCapUp As Label
    Friend WithEvents TableLayoutPanel1 As TableLayoutPanel
    Friend WithEvents ToolTip1 As ToolTip
    Public Const InterfaceId As String = "2bfa76a6-67b3-4798-8b58-73034782da1d"
#End Region

    ' 可创建的 COM 类必须具有一个不带参数的 Public Sub New() 
    ' 否则， 将不会在 
    ' COM 注册表中注册此类，且无法通过
    ' CreateObject 创建此类。
    Public Sub New()
        MyBase.New()
        InitializeComponent()
        Hide()
        mCounter = New NetworkTrafficCounter()

        '根据DPI缩放系数，适当调整界面大小
        Dim scale As Integer = 0
        GetScaleFactorForMonitor(MonitorFromWindow(Handle, 0), scale)
        Width = CInt((scale / 100) * Width)
        Height = CInt((scale / 100) * Height)
        Dim nf As New Font(New FontFamily("Arial Narrow"), CSng(0.1F * (scale / 100)), GraphicsUnit.Inch)
        LUpSpeed.Font = nf
        LDnSpeed.Font = nf
        LCapUp.Font = nf
        LCapDn.Font = nf

        AddHandler mCounter.Tick, AddressOf CounterTick
        AddHandler Microsoft.Win32.SystemEvents.SessionEnding, AddressOf ShotdownHandler
    End Sub

    Private Sub ShotdownHandler(sender As Object, e As Microsoft.Win32.SessionEndingEventArgs)
        e.Cancel = False
        SaveSettings()
        RemoveHandler Microsoft.Win32.SystemEvents.SessionEnding, AddressOf ShotdownHandler
    End Sub

    Private Sub CounterTick(upSpeed As Long, downSpeed As Long)
        LUpSpeed.Text = GetSpeedString(upSpeed)
        LDnSpeed.Text = GetSpeedString(downSpeed)

        ToolTip1.SetToolTip(LUpSpeed, GetTooltipString)
        ToolTip1.SetToolTip(LDnSpeed, GetTooltipString)
        ToolTip1.SetToolTip(TableLayoutPanel1, GetTooltipString)
    End Sub

    Private Function GetSpeedString(speed As Long)
        Return (GetDataSizeString(speed) & "/s").PadLeft(12)
    End Function

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

    Private Sub LoadSettings()
        mSettings.totalUploadedBytes = My.Settings.UpBytes
        mSettings.totalDownloadedBytes = My.Settings.DnBytes
    End Sub

    Private Sub SaveSettings()
        My.Settings.UpBytes = TotalBytesUp
        My.Settings.DnBytes = TotalBytesDown
        My.Settings.Save()
    End Sub

    Private Function GetTooltipString() As String
        'Return $"Network Traffic    Uploaded:{...}     Downloaded:{...}"
        Return $"流量统计   上传：{GetDataSizeString(TotalBytesUp).PadLeft(9)}    下载：{GetDataSizeString(TotalBytesDown).PadLeft(9)}"
    End Function
#Region "IDeskBand2"

    Public Function GetWindow(ByRef phwnd As IntPtr) As HResult Implements IOleWindow.GetWindow, IDockingWindow.GetWindow, IDeskBand.GetWindow, IDeskBand2.GetWindow
        phwnd = Handle
        Return HResult.S_OK
    End Function

    Public Function ContextSensitiveHelp(<[In]> fEnterMode As Boolean) As HResult Implements IOleWindow.ContextSensitiveHelp, IDockingWindow.ContextSensitiveHelp, IDeskBand.ContextSensitiveHelp, IDeskBand2.ContextSensitiveHelp
        Return HResult.E_NOTIMPL
    End Function

    Public Function ShowDW(<[In]> fShow As Boolean) As HResult Implements IDockingWindow.ShowDW, IDeskBand.ShowDW, IDeskBand2.ShowDW
        If fShow Then
            Show()
        Else
            Hide()
        End If
        Return HResult.S_OK
    End Function

    Public Function CloseDW(<[In]> dwReserved As UInteger) As HResult Implements IDockingWindow.CloseDW, IDeskBand.CloseDW, IDeskBand2.CloseDW
        Hide()
        mSettingsChanged = True
        mSettings.totalDownloadedBytes += mCounter.ReceivedBytes
        mSettings.totalUploadedBytes += mCounter.SentBytes
        Return HResult.S_OK
    End Function

    Public Function ResizeBorderDW(ByRef prcBorder As RECT, <[In]> <MarshalAs(UnmanagedType.IUnknown)> punkToolbarSite As Object, fReserved As Boolean) As HResult Implements IDockingWindow.ResizeBorderDW, IDeskBand.ResizeBorderDW, IDeskBand2.ResizeBorderDW
        Return HResult.E_NOTIMPL
    End Function

    Public Function GetBandInfo(dwBandID As UInteger, dwViewMode As UInteger, ByRef pdbi As DESKBANDINFO) As HResult Implements IDeskBand.GetBandInfo, IDeskBand2.GetBandInfo
        pdbi.wszTitle = Title

        pdbi.ptActual.x = Size.Width
        pdbi.ptActual.y = Size.Height

        pdbi.ptMaxSize.x = Size.Width * 2
        pdbi.ptMaxSize.y = Size.Height * 2

        pdbi.ptMinSize.x = Size.Width
        pdbi.ptMinSize.y = Size.Height

        pdbi.ptIntegral.x = 1
        pdbi.ptIntegral.y = 1

        'pdbi.crBkgnd = Drawing.Color.FromArgb(0, 0, 0, 0).ToArgb

        pdbi.dwModeFlags = pdbi.dwModeFlags Or DBIM.TITLE Or DBIM.ACTUAL Or DBIM.MAXSIZE Or DBIM.MINSIZE 'Or DBIM.BKCOLOR

        Return HResult.S_OK
    End Function

    Public Function CanRenderComposited(<MarshalAs(UnmanagedType.Bool)> <Out> ByRef pfCanRenderComposited As Boolean) As HResult Implements IDeskBand2.CanRenderComposited
        pfCanRenderComposited = True
        Return HResult.S_OK
    End Function

    Public Function SetCompositionState(<[In]> <MarshalAs(UnmanagedType.Bool)> fCompositionEnabled As Boolean) As HResult Implements IDeskBand2.SetCompositionState
        mRenderComposited = fCompositionEnabled
        Return HResult.S_OK
    End Function

    Public Function GetCompositionState(<MarshalAs(UnmanagedType.Bool)> <Out> ByRef pfCompositionEnabled As Boolean) As HResult Implements IDeskBand2.GetCompositionState
        pfCompositionEnabled = mRenderComposited
        Return HResult.S_OK
    End Function

#End Region
#Region "IObjectWithSite"

    Public Function SetSite(<[In]> <MarshalAs(UnmanagedType.Interface)> pUnkSite As IOleWindow) As HResult Implements IObjectWithSite.SetSite
        If BandObjectSite <> IntPtr.Zero Then
            Try
                Marshal.Release(BandObjectSite)
            Catch ex As COMException

            Finally
                BandObjectSite = IntPtr.Zero
            End Try
        End If

        If pUnkSite Is Nothing Then
            SaveSettings()
            parentWindowHandle = IntPtr.Zero
            mCounter.Dispose()
            Dispose(True)
            GC.Collect()
        Else
            Try
                pUnkSite.GetWindow(parentWindowHandle)
                SetParent(Handle, parentWindowHandle)
                BandObjectSite = Marshal.GetIUnknownForObject(pUnkSite)
                LoadSettings()
            Catch ex As Exception

            End Try
        End If

        Return HResult.S_OK
    End Function

    Public Function GetSite(ByRef riid As Guid, <MarshalAs(UnmanagedType.IUnknown)> ByRef ppvSite As IntPtr) As HResult Implements IObjectWithSite.GetSite
        Return Marshal.QueryInterface(BandObjectSite, riid, ppvSite)
    End Function
#End Region
#Region "IPersistStream"
    Public Function GetClassID(ByRef pClassID As Guid) As HResult Implements IPersist.GetClassID, IPersistStream.GetClassID
        pClassID = New Guid(ClassId)
        Return HResult.S_OK
    End Function

    Public Function IsDirty() As Integer Implements IPersistStream.IsDirty
        Return 1 'S_FALSE
    End Function

    Public Function IPersistStream_Load(<[In]> <MarshalAs(UnmanagedType.Interface)> pStm As IStream) As HResult Implements IPersistStream.Load
        Return HResult.S_OK
    End Function

    Public Function Save(<[In]> <MarshalAs(UnmanagedType.Interface)> pStm As IStream, <[In]> fClearDirty As Boolean) As HResult Implements IPersistStream.Save
        Return HResult.S_OK
    End Function

    Public Function GetSizeMax(<Out> ByRef pcbSize As ULong) As HResult Implements IPersistStream.GetSizeMax
        Return HResult.S_OK
    End Function

#End Region
#Region "DLL注册卸载函数"
    <ComRegisterFunction> <Security.SuppressUnmanagedCodeSecurity> Public Shared Sub ClassRegister(t As Type)

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

    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.LCapDn = New System.Windows.Forms.Label()
        Me.LUpSpeed = New System.Windows.Forms.Label()
        Me.LDnSpeed = New System.Windows.Forms.Label()
        Me.LCapUp = New System.Windows.Forms.Label()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.TableLayoutPanel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'LCapDn
        '
        Me.LCapDn.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.LCapDn.AutoSize = True
        Me.LCapDn.BackColor = System.Drawing.Color.Transparent
        Me.LCapDn.Font = New System.Drawing.Font("Arial Narrow", 0.11!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Inch)
        Me.LCapDn.ForeColor = System.Drawing.Color.AliceBlue
        Me.LCapDn.Location = New System.Drawing.Point(0, 20)
        Me.LCapDn.Margin = New System.Windows.Forms.Padding(0)
        Me.LCapDn.Name = "LCapDn"
        Me.LCapDn.Size = New System.Drawing.Size(25, 20)
        Me.LCapDn.TabIndex = 1
        Me.LCapDn.Text = "Dn"
        Me.LCapDn.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'LUpSpeed
        '
        Me.LUpSpeed.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.LUpSpeed.AutoSize = True
        Me.LUpSpeed.Font = New System.Drawing.Font("Arial Narrow", 0.11!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Inch)
        Me.LUpSpeed.ForeColor = System.Drawing.SystemColors.HighlightText
        Me.LUpSpeed.Location = New System.Drawing.Point(60, 0)
        Me.LUpSpeed.Margin = New System.Windows.Forms.Padding(0)
        Me.LUpSpeed.Name = "LUpSpeed"
        Me.LUpSpeed.Size = New System.Drawing.Size(40, 20)
        Me.LUpSpeed.TabIndex = 2
        Me.LUpSpeed.Text = "0 B/s"
        Me.LUpSpeed.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.LUpSpeed.UseMnemonic = False
        '
        'LDnSpeed
        '
        Me.LDnSpeed.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.LDnSpeed.AutoSize = True
        Me.LDnSpeed.Font = New System.Drawing.Font("Arial Narrow", 0.11!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Inch)
        Me.LDnSpeed.ForeColor = System.Drawing.SystemColors.HighlightText
        Me.LDnSpeed.Location = New System.Drawing.Point(60, 20)
        Me.LDnSpeed.Margin = New System.Windows.Forms.Padding(0)
        Me.LDnSpeed.Name = "LDnSpeed"
        Me.LDnSpeed.Size = New System.Drawing.Size(40, 20)
        Me.LDnSpeed.TabIndex = 3
        Me.LDnSpeed.Text = "0 B/s"
        Me.LDnSpeed.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.LDnSpeed.UseMnemonic = False
        '
        'LCapUp
        '
        Me.LCapUp.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.LCapUp.AutoSize = True
        Me.LCapUp.BackColor = System.Drawing.Color.Transparent
        Me.LCapUp.Font = New System.Drawing.Font("Arial Narrow", 0.11!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Inch)
        Me.LCapUp.ForeColor = System.Drawing.Color.AliceBlue
        Me.LCapUp.Location = New System.Drawing.Point(0, 0)
        Me.LCapUp.Margin = New System.Windows.Forms.Padding(0)
        Me.LCapUp.Name = "LCapUp"
        Me.LCapUp.Size = New System.Drawing.Size(26, 20)
        Me.LCapUp.TabIndex = 0
        Me.LCapUp.Text = "Up"
        Me.LCapUp.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.TableLayoutPanel1.ColumnCount = 2
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.TableLayoutPanel1.Controls.Add(Me.LUpSpeed, 1, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.LDnSpeed, 1, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.LCapDn, 0, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.LCapUp, 0, 0)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel1.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel1.MinimumSize = New System.Drawing.Size(100, 40)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 2
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(100, 40)
        Me.TableLayoutPanel1.TabIndex = 4
        Me.ToolTip1.SetToolTip(Me.TableLayoutPanel1, "这是内容")
        '
        'ToolTip1
        '
        Me.ToolTip1.AutoPopDelay = 5000
        Me.ToolTip1.InitialDelay = 500
        Me.ToolTip1.IsBalloon = True
        Me.ToolTip1.ReshowDelay = 0
        Me.ToolTip1.ShowAlways = True
        Me.ToolTip1.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info
        Me.ToolTip1.ToolTipTitle = "网速监控"
        Me.ToolTip1.UseAnimation = False
        Me.ToolTip1.UseFading = False
        '
        'BandObject
        '
        Me.BackColor = System.Drawing.Color.Black
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.DoubleBuffered = True
        Me.Margin = New System.Windows.Forms.Padding(0)
        Me.MinimumSize = New System.Drawing.Size(100, 40)
        Me.Name = "BandObject"
        Me.Size = New System.Drawing.Size(100, 40)
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub


End Class


