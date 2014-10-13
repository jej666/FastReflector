Imports System.Collections.Generic
Imports InfoSante.Elisagenda.Infrastructure.Common

Public Interface IFastActivator

    ''' <summary>
    ''' Gets the instance delegate.
    ''' </summary>
    ''' <value>
    ''' The instance delegate.
    ''' </value>
    ReadOnly Property InstanceDelegate As ActivatorDelegate
    ''' <summary>
    ''' Gets the create instance delegate.
    ''' </summary>
    ''' <param name="type">The type.</param>
    ''' <returns></returns>
    Function GetCreateInstanceDelegate(type As Type) As ActivatorDelegate
    ''' <summary>
    ''' Creates the instance.
    ''' </summary>
    ''' <typeparam name="T"></typeparam>
    ''' <returns></returns>
    Function CreateInstance(Of T)() As T

End Interface
