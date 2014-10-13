Imports System.Collections.Generic
Imports System.Reflection

<System.ComponentModel.ImmutableObject(True)>
Public Class FastPropertyProvider
    Implements IFastPropertyProvider

    Private Shared ReadOnly _cache As New Dictionary(Of Type, IFastPropertyAccessor)

    Public ReadOnly Property TypeCached As IEnumerable(Of Type) _
        Implements IFastPropertyProvider.TypeCached
        Get
            Return _cache.Keys
        End Get
    End Property

    Default Public ReadOnly Property Item(type As Type) As IFastPropertyAccessor _
        Implements IFastPropertyProvider.Item
        Get
            Return _cache(type)
        End Get
    End Property

    Public Sub Store(type As Type) _
        Implements IFastPropertyProvider.Store
        StoreIfNecessary(type)
    End Sub

    Private Shared Sub StoreIfNecessary(type As Type)
        If Not _cache.ContainsKey(type) Then
            _cache.Add(type, type.FastReflect())
        End If
    End Sub

    Public Function GetPropertyInfo(type As Type) As PropertyInfo() _
        Implements IFastPropertyProvider.GetPropertyInfo
        StoreIfNecessary(type)
        Return _cache(type).Properties
    End Function

    Public Function GetPropertyGetter(type As Type) As IDictionary(Of PropertyInfo, PropertyGetterDelegate) _
        Implements IFastPropertyProvider.GetPropertyGetter
        StoreIfNecessary(type)
        Return _cache(type).GetterMap
    End Function

    Public Function GetPropertySetter(type As Type) As IDictionary(Of PropertyInfo, PropertySetterDelegate) _
        Implements IFastPropertyProvider.GetPropertySetter
        StoreIfNecessary(type)
        Return _cache(type).SetterMap
    End Function

End Class
