Imports System.Runtime.InteropServices

Friend Module Utils

    Sub ConvertStructureToBlob(Of T As Structure)(ByVal val As T, ByRef data As Byte())
        Try
            Dim size As Integer = Marshal.SizeOf(GetType(T))
            If data Is Nothing Or data.Length < size Then
                data = New Byte() {}
                ReDim data(size)
            End If

            Dim ptrtmp = Marshal.AllocHGlobal(size)
            Marshal.StructureToPtr(val, ptrtmp, False)
            Marshal.Copy(ptrtmp, data, 0, size)
            Marshal.FreeHGlobal(ptrtmp)
        Catch ex As Exception

        End Try
    End Sub

    Sub ConvertBlobToStructure(Of T As Structure)(ByVal data As Byte(), ByRef val As T)
        Try
            Dim size As Integer = Marshal.SizeOf(GetType(T))
            If data Is Nothing Or data.Length < size Then
                data = New Byte() {}
                ReDim data(size)
            End If

            Dim ptrtmp = Marshal.AllocHGlobal(size)
            Marshal.Copy(data, 0, ptrtmp, size)
            Marshal.PtrToStructure(ptrtmp, val)
            Marshal.FreeHGlobal(ptrtmp)
        Catch ex As Exception

        End Try
    End Sub
End Module
