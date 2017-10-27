Imports System.Runtime.InteropServices
Imports System.Runtime.InteropServices.ComTypes
Imports System.Security

<ComImport>
<InterfaceType(ComInterfaceType.InterfaceIsIUnknown)>
<Guid("FC4801A3-2BA9-11CF-A229-00AA003D7352")>
Public Interface IObjectWithSite
    <PreserveSig> Function SetSite(<[In]> <MarshalAs(UnmanagedType.Interface)> pUnkSite As IOleWindow) As HResult
    <PreserveSig> Function GetSite(ByRef riid As Guid, <Out> ByRef ppvSite As IntPtr) As HResult
End Interface

<ComImport>
<InterfaceType(ComInterfaceType.InterfaceIsIUnknown)>
<Guid("00000114-0000-0000-C000-000000000046")>
Public Interface IOleWindow
    <PreserveSig> Function GetWindow(<Out> ByRef phwnd As IntPtr) As HResult
    <PreserveSig> Function ContextSensitiveHelp(<[In]> fEnterMode As Boolean) As HResult
End Interface

<ComImport>
<InterfaceType(ComInterfaceType.InterfaceIsIUnknown)>
<Guid("012dd920-7b26-11d0-8ca9-00a0c92dbfe8")>
Public Interface IDockingWindow
    <PreserveSig> Function GetWindow(ByRef phwnd As IntPtr) As HResult
    <PreserveSig> Function ContextSensitiveHelp(<[In]> fEnterMode As Boolean) As HResult
    <PreserveSig> Function ShowDW(<[In]> fShow As Boolean) As HResult
    <PreserveSig> Function CloseDW(<[In]> dwReserved As UInt32) As HResult
    <PreserveSig> Function ResizeBorderDW(<Out, MarshalAs(UnmanagedType.LPStruct)> ByRef prcBorder As RECT,
                                          <[In], MarshalAs(UnmanagedType.IUnknown)> punkToolbarSite As Object,
                                          <MarshalAs(UnmanagedType.Bool)> fReserved As Boolean) As HResult
End Interface

<ComImport>
<InterfaceType(ComInterfaceType.InterfaceIsIUnknown)>
<Guid("EB0FE172-1A3A-11D0-89B3-00A0C90A90AC")>
Public Interface IDeskBand
    <PreserveSig> Function GetWindow(ByRef phwnd As IntPtr) As HResult
    <PreserveSig> Function ContextSensitiveHelp(<[In], MarshalAs(UnmanagedType.Bool)> fEnterMode As Boolean) As HResult
    <PreserveSig> Function ShowDW(<[In], MarshalAs(UnmanagedType.Bool)> fShow As Boolean) As HResult
    <PreserveSig> Function CloseDW(<[In]> dwReserved As UInt32) As HResult
    <PreserveSig> Function ResizeBorderDW(<Out, MarshalAs(UnmanagedType.LPStruct)> ByRef prcBorder As RECT,
                                          <[In], MarshalAs(UnmanagedType.IUnknown)> punkToolbarSite As Object,
                                          <MarshalAs(UnmanagedType.Bool)> fReserved As Boolean) As HResult
    <PreserveSig> Function GetBandInfo(dwBandID As UInt32, dwViewMode As UInt32, ByRef pdbi As DESKBANDINFO) As HResult
End Interface

<ComImport>
<InterfaceType(ComInterfaceType.InterfaceIsIUnknown)>
<Guid("79D16DE4-ABEE-4021-8D9D-9169B261D657")>
Public Interface IDeskBand2
    <PreserveSig> Function GetWindow(ByRef phwnd As IntPtr) As HResult
    <PreserveSig> Function ContextSensitiveHelp(<[In], MarshalAs(UnmanagedType.Bool)> fEnterMode As Boolean) As HResult
    <PreserveSig> Function ShowDW(<[In], MarshalAs(UnmanagedType.Bool)> fShow As Boolean) As HResult
    <PreserveSig> Function CloseDW(<[In]> dwReserved As UInt32) As HResult
    <PreserveSig> Function ResizeBorderDW(<Out, MarshalAs(UnmanagedType.LPStruct)> ByRef prcBorder As RECT,
                                          <[In], MarshalAs(UnmanagedType.IUnknown)> punkToolbarSite As Object,
                                          <MarshalAs(UnmanagedType.Bool)> fReserved As Boolean) As HResult
    <PreserveSig> Function GetBandInfo(dwBandID As UInt32, dwViewMode As UInt32, ByRef pdbi As DESKBANDINFO) As HResult
    <PreserveSig> Function CanRenderComposited(<Out, MarshalAs(UnmanagedType.Bool)> ByRef pfCanRenderComposited As Boolean) As HResult
    <PreserveSig> Function SetCompositionState(<[In], MarshalAs(UnmanagedType.Bool)> ByVal fCompositionEnabled As Boolean) As HResult
    <PreserveSig> Function GetCompositionState(<Out, MarshalAs(UnmanagedType.Bool)> ByRef pfCompositionEnabled As Boolean) As HResult
End Interface

<ComImport>
<InterfaceType(ComInterfaceType.InterfaceIsIUnknown)>
<Guid("68284faa-6a48-11d0-8c78-00c04fd918b4")>
Public Interface IInputObject
    <PreserveSig> Function UIActivateIO(<MarshalAs(UnmanagedType.Bool)> fActivate As Boolean, ByRef msg As MSG) As HResult
    <PreserveSig> Function HasFocusIO() As Int32
    <PreserveSig> Function TranslateAcceleratorIO(ByRef msg As MSG) As HResult
End Interface

<ComImport>
<InterfaceType(ComInterfaceType.InterfaceIsIUnknown)>
<Guid("f1db8392-7331-11d0-8c99-00a0c92dbfe8")>
Public Interface IInputObjectSite
    <PreserveSig> Function OnFocusChangeIS(<[In]> punkObj As Object, fSetFocus As Int32) As HResult
End Interface

<ComImport>
<InterfaceType(ComInterfaceType.InterfaceIsIUnknown)>
<Guid("0000010c-0000-0000-C000-000000000046")>
Public Interface IPersist
    <PreserveSig> Function GetClassID(ByRef pClassID As Guid) As HResult
End Interface

<ComImport>
<InterfaceType(ComInterfaceType.InterfaceIsIUnknown)>
<Guid("00000109-0000-0000-C000-000000000046")>
Public Interface IPersistStream

    <PreserveSig> Function GetClassID(ByRef pClassID As Guid) As HResult
    Function IsDirty() As Integer
    <PreserveSig> Function Load(<[In], MarshalAs(UnmanagedType.[Interface])> pStm As IStream) As HResult
    <PreserveSig> Function Save(<[In], MarshalAs(UnmanagedType.[Interface])> pStm As IStream, <[In]> fClearDirty As Boolean) As HResult
    <PreserveSig> Function GetSizeMax(<Out> ByRef pcbSize As UInt64) As HResult
End Interface

<SuppressUnmanagedCodeSecurity, ComImport, Guid("7FD52380-4E07-101B-AE2D-08002B2EC713"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)>
Public Interface IPersistStreamInit
    <PreserveSig> Function GetClassID(ByRef pClassID As Guid) As HResult
    Function IsDirty() As Integer
    <PreserveSig> Function Load(<[In], MarshalAs(UnmanagedType.[Interface])> pStm As IStream) As HResult
    <PreserveSig> Function Save(<[In], MarshalAs(UnmanagedType.[Interface])> pStm As IStream, <[In]> fClearDirty As Boolean) As HResult
    <PreserveSig> Function GetSizeMax(<Out> ByRef pcbSize As UInt64) As HResult
    <PreserveSig> Function InitNew() As HResult
End Interface

''' <summary>
''' 提供具有 ISequentialStream 功能的 IStream 接口的托管定义。
''' </summary>
<ComImport, Guid("0000000c-0000-0000-C000-000000000046"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)>
Public Interface IStream

    ''' <summary>
    ''' 读取指定的数目的字节从流对象读入内存起价当前搜索指针。
    ''' </summary>
    ''' <param name="pv">此方法返回时，包含从流中读取的数据。 此参数未经初始化即被传递。</param>
    ''' <param name="cb">要从流对象读取的字节数。</param>
    ''' <param name="pcbRead">一个指向 ULONG 从流对象读取该变量接收的实际字节数。</param>
    <PreserveSig> Function Read(pv() As Byte, cb As UInteger, ByRef pcbRead As UInteger) As HResult

    ''' <summary>
    ''' 将指定数量的字节写入以当前开始的流对象搜索指针。
    ''' </summary>
    ''' <param name="pv">要写入此流的缓冲区。</param>
    ''' <param name="cb">要写入流的字节数。</param>
    ''' <param name="pcbWritten">在成功返回时，包含实际写入的流对象的字节数。 如果调用方将此指针设置为 System.IntPtr.Zero, ，此方法不提供的实际写入的字节数。</param>
    <PreserveSig> Function Write(pv() As Byte, cb As UInteger, ByRef pcbWritten As UInteger) As HResult

    ''' <summary>
    ''' 将搜索指针更改到流的新位置相对于开头、 流的结尾或当前搜索指针。
    ''' </summary>
    ''' <param name="dlibMove">偏移量将添加到 dwOrigin。</param>
    ''' <param name="dwOrigin">查找的起始地址。 原始位置可以是文件的开头、 当前查找指针或该文件的末尾。</param>
    ''' <param name="plibNewPosition">在成功返回时，包含从流的开始位置的搜索指针的偏移量。</param>
    <PreserveSig> Function Seek(dlibMove As Long, dwOrigin As Integer, ByRef plibNewPosition As UInteger) As HResult

    ''' <summary>
    ''' 流对象的大小更改。
    ''' </summary>
    ''' <param name="libNewSize">将流的字节数作为新大小。</param>
    <PreserveSig> Function SetSize(libNewSize As ULong) As HResult

    ''' <summary>
    ''' 副本指定的数目的字节从当前搜索指针中的流与当前搜索另一个流中的指针。
    ''' </summary>
    ''' <param name="pstm"> 对目标流的引用。</param>
    ''' <param name="cb">要从源流复制的字节数。</param>
    ''' <param name="pcbRead">在成功返回时，包含实际从源中读取的字节数。</param>
    ''' <param name="pcbWritten">在成功返回时，包含实际写入该目标的字节数。</param>
    <PreserveSig> Function CopyTo(pstm As IStream, cb As ULong, ByRef pcbRead As ULong, ByRef pcbWritten As ULong) As HResult

    ''' <summary>
    ''' 确保在事务处理模式中打开的流对象所做任何更改都会反映在父存储中。
    ''' </summary>
    ''' <param name="grfCommitFlags">一个值，控制流对象所做的更改的提交方式。</param>
    <PreserveSig> Function Commit(grfCommitFlags As Integer) As HResult

    ''' <summary>
    ''' 放弃自上一个对事务流做的所有更改 System.Runtime.InteropServices.ComTypes.IStream.Commit(System.Int32)调用。
    ''' </summary>
    <PreserveSig> Function Revert() As HResult

    ''' <summary>
    ''' 将访问限制为指定的流中的字节范围。
    ''' </summary>
    ''' <param name="libOffset">范围的起始位置的字节偏移量。</param>
    ''' <param name="cb">范围，以字节为单位，以限制的长度。</param>
    ''' <param name="dwLockType">对访问该范围的请求的限制。</param>
    <PreserveSig> Function LockRegion(libOffset As ULong, cb As ULong, dwLockType As Integer) As HResult

    ''' <summary>
    ''' 删除某个范围的字节与以往限制的访问限制 System.Runtime.InteropServices.ComTypes.IStream.LockRegion(System.Int64,System.Int64,System.Int32)方法。
    ''' </summary>
    ''' <param name="libOffset">范围的起始位置的字节偏移量。</param>
    ''' <param name="cb">要限制的范围的长度，以字节为单位。</param>
    ''' <param name="dwLockType">以前对范围施加的访问限制。</param>
    <PreserveSig> Function UnlockRegion(libOffset As ULong, cb As ULong, dwLockType As Integer) As HResult

    ''' <summary>
    ''' 检索 System.Runtime.InteropServices.STATSTG 此流的结构。
    ''' </summary>
    ''' <param name="pstatstg">此方法返回时，包含 STATSTG 描述此流对象的结构。 此参数未经初始化即被传递</param>
    ''' <param name="grfStatFlag">中的成员 STATSTG 此方法不返回，从而节省一些内存分配操作的结构。</param>
    <PreserveSig> Function Stat(ByRef pstatstg As ComTypes.STATSTG, grfStatFlag As Integer) As HResult

    ''' <summary>
    ''' 创建新的流对象具有其自己查找引用与原始流相同的字节的指针
    ''' </summary>
    ''' <param name="ppstm">此方法返回时，包含新的流对象。 此参数未经初始化即被传递。</param>
    <PreserveSig> Function Clone(ByRef ppstm As IStream) As HResult
End Interface


Friend Module ShellGUIDs
    Public ReadOnly IID_IOleWindow As New Guid("00000114-0000-0000-C000-000000000046")
    Public ReadOnly IID_IWebBrowserApp As New Guid("0002DF05-0000-0000-C000-000000000046")
    Public ReadOnly IID_IUnknown As New Guid("00000000-0000-0000-C000-000000000046")
    Public ReadOnly CATID_DeskBand As New Guid("00021492-0000-0000-C000-000000000046")
    Public ReadOnly CATID_InfoBand As New Guid("00021493-0000-0000-C000-000000000046")
    Public ReadOnly CATID_CommBand As New Guid("00021494-0000-0000-C000-000000000046")
End Module