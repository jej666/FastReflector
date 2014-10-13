Imports System.Collections.Generic
Imports System.Reflection

Public Interface IFastPropertyAccessor

    ''' <summary>
    ''' Gets the properties.
    ''' </summary>
    ''' <value>
    ''' The properties.
    ''' </value>
    ReadOnly Property Properties As PropertyInfo()
    
    ''' <summary>
    ''' Gets the getter map.
    ''' </summary>
    ''' <value>
    ''' The getter map.
    ''' </value>
    ReadOnly Property GetterMap As IDictionary(Of PropertyInfo, PropertyGetterDelegate)
    
    ''' <summary>
    ''' Gets the setter map.
    ''' </summary>
    ''' <value>
    ''' The setter map.
    ''' </value>
    ReadOnly Property SetterMap As IDictionary(Of PropertyInfo, PropertySetterDelegate)

End Interface
