Imports System.Collections.Generic
Imports System.Reflection

<System.ComponentModel.ImmutableObject(True)>
Friend Class FastPropertyAccessor
    Implements IFastPropertyAccessor

    Private ReadOnly _properties As Lazy(Of PropertyInfo())
    Private ReadOnly _getterMap As IDictionary(Of PropertyInfo, PropertyGetterDelegate) = New Dictionary(Of PropertyInfo, PropertyGetterDelegate)
    Private ReadOnly _setterMap As IDictionary(Of PropertyInfo, PropertySetterDelegate) = New Dictionary(Of PropertyInfo, PropertySetterDelegate)

    Friend Sub New(type As Type)
        _properties = New Lazy(Of PropertyInfo())(Function() type.GetProperties())
    End Sub

    Friend ReadOnly Property Properties As PropertyInfo() _
        Implements IFastPropertyAccessor.Properties
        Get
            Return _properties.Value
        End Get
    End Property

    Friend ReadOnly Property GetterMap As IDictionary(Of PropertyInfo, PropertyGetterDelegate) _
        Implements IFastPropertyAccessor.GetterMap
        Get
            If _getterMap.Count = 0 Then
                SetGetters()
            End If
            Return _getterMap
        End Get
    End Property

    Friend ReadOnly Property SetterMap As IDictionary(Of PropertyInfo, PropertySetterDelegate) _
        Implements IFastPropertyAccessor.SetterMap
        Get
            If _setterMap.Count = 0 Then
                SetSetters()
            End If
            Return _setterMap
        End Get
    End Property

    Private Sub SetGetters()
        For i = 0 To Properties.GetUpperBound(0)
            If _getterMap.ContainsKey(Properties(i)) Then
                Continue For
            End If
            _getterMap.Add(Properties(i), EmitInvoker.Getter(Properties(i)))
        Next
    End Sub

    Private Sub SetSetters()
        For i = 0 To Properties.GetUpperBound(0)
            If _setterMap.ContainsKey(Properties(i)) Then
                Continue For
            End If
            _setterMap.Add(Properties(i), EmitInvoker.Setter(Properties(i)))
        Next
    End Sub

End Class
