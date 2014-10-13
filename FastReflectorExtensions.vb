Imports System.Runtime.CompilerServices
Imports InfoSante.Elisagenda.Domain.Facade

<HideModuleName(), System.ComponentModel.ImmutableObject(True)>
Public Module FastReflectorExtensions

    ''' <summary>
    ''' Fasts the reflect.
    ''' </summary>
    ''' <param name="type">The type.</param>
    ''' <returns></returns>
    <Extension()>
    Public Function FastReflect(type As Type) As IFastPropertyAccessor
        Return New FastPropertyAccessor(type)
    End Function

    ''' <summary>
    ''' Fasts the activator.
    ''' </summary>
    ''' <param name="type">The type.</param>
    ''' <returns></returns>
    <Extension()>
    Public Function FastActivator(type As Type) As IFastActivator
        Return New FastActivator(type)
    End Function

End Module
