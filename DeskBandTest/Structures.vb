Imports System.IO
Imports System.Runtime.InteropServices

<StructLayout(LayoutKind.Sequential, CharSet:=CharSet.Unicode)>
Public Structure DESKBANDINFO
    Dim dwMask As DBIM
    Dim ptMinSize As POINTL
    Dim ptMaxSize As POINTL
    Dim ptIntegral As POINTL
    Dim ptActual As POINTL
    <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=255)>
    Dim wszTitle As String
    Dim dwModeFlags As DBIMF
    Dim crBkgnd As Integer
End Structure

<StructLayout(LayoutKind.Sequential)>
Public Structure POINTL
    Dim X As Integer
    Dim Y As Integer
End Structure

<StructLayout(LayoutKind.Sequential)>
Public Structure RECT
    Dim Left As Integer
    Dim Top As Integer
    Dim Right As Integer
    Dim Bottom As Integer
End Structure

<StructLayout(LayoutKind.Sequential)>
Public Structure Settings
    Dim totalUploadedBytes As Long
    Dim totalDownloadedBytes As Long
End Structure

