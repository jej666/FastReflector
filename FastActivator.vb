Imports System.Collections.Generic
Imports InfoSante.Elisagenda.Infrastructure.Common

Friend Class FastActivator
    Implements IFastActivator

    Private _delegateInstance As ActivatorDelegate
    Private Shared ReadOnly _cache As New Dictionary(Of Type, ActivatorDelegate)

    Friend Sub New(type As Type)
        StoreIfNecessary(type)
    End Sub

    Private Sub StoreIfNecessary(type As Type)
        If _cache.ContainsKey(type) Then
            Return
        End If
        _delegateInstance = EmitInvoker.CreateInstance(type)
        _cache.Add(type, _delegateInstance)
    End Sub

    Friend ReadOnly Property InstanceDelegate As ActivatorDelegate Implements IFastActivator.InstanceDelegate
        Get
            Return _delegateInstance
        End Get
    End Property

    Friend Function GetCreateInstanceDelegate(type As Type) As ActivatorDelegate Implements IFastActivator.GetCreateInstanceDelegate
        StoreIfNecessary(type)
        Return _cache(type)
    End Function

    Friend Function CreateInstance(Of T)() As T Implements IFastActivator.CreateInstance
        StoreIfNecessary(GetType(T))
        Return DirectCast(_cache(GetType(T)).Invoke, T)
    End Function

End Class
