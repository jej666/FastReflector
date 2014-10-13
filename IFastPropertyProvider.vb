Imports System.Collections.Generic
Imports System.Reflection

Public Interface IFastPropertyProvider

    ''' <summary>
    ''' Stores the specified type.
    ''' </summary>
    ''' <param name="type">The type.</param>
    Sub Store(type As Type)

    ''' <summary>
    ''' Gets the property information.
    ''' </summary>
    ''' <param name="type">The type.</param>
    ''' <returns></returns>
    Function GetPropertyInfo(type As Type) As PropertyInfo()
    
    ''' <summary>
    ''' Gets the property getter.
    ''' </summary>
    ''' <param name="type">The type.</param>
    ''' <returns></returns>
    Function GetPropertyGetter(type As Type) As IDictionary(Of PropertyInfo, PropertyGetterDelegate)
    
    ''' <summary>
    ''' Gets the property setter.
    ''' </summary>
    ''' <param name="type">The type.</param>
    ''' <returns></returns>
    Function GetPropertySetter(type As Type) As IDictionary(Of PropertyInfo, PropertySetterDelegate)

    ''' <summary>
    ''' Gets the item.
    ''' </summary>
    ''' <value>
    ''' The item.
    ''' </value>
    Default ReadOnly Property Item(type As Type) As IFastPropertyAccessor
    
    ''' <summary>
    ''' Gets the type cached.
    ''' </summary>
    ''' <value>
    ''' The type cached.
    ''' </value>
    ReadOnly Property TypeCached As IEnumerable(Of Type)

End Interface
