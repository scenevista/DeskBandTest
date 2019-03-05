Namespace Plugin.Core

    Public MustInherit Class PluginBase

        Public MustOverride ReadOnly Property Id As Guid
        Public MustOverride ReadOnly Property Name As String
        Public MustOverride ReadOnly Property Version As Version

        Public MustOverride ReadOnly Property Activities As Activity()

    End Class

    Public Class Activity


    End Class

    Public Class EngineKernel

    End Class
End Namespace

