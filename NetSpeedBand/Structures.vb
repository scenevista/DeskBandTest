Imports System.IO
Imports System.Runtime.InteropServices

<StructLayout(LayoutKind.Sequential)>
Public Structure DESKBANDINFO
    Dim dwMask As UInteger
    Dim ptMinSize As POINTL
    Dim ptMaxSize As POINTL
    Dim ptIntegral As POINTL
    Dim ptActual As POINTL
    Dim wszTitle As String
    Dim dwModeFlags As Integer
    Dim crBkgnd As Integer
End Structure

<StructLayout(LayoutKind.Sequential)>
Public Structure POINTL
    Dim x As Integer
    Dim y As Integer
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
