Imports System.Reflection
Imports System.Reflection.Emit

<System.ComponentModel.ImmutableObject(True)>
Friend NotInheritable Class EmitInvoker

    Private Sub New()
    End Sub

    Friend Shared Function Setter(propertyInfo As PropertyInfo) As PropertySetterDelegate
        Dim setMethod = propertyInfo.GetSetMethod()
        If setMethod Is Nothing Then
            Return Nothing
        End If
        Dim arguments As Type() = New Type(1) {}
        arguments(0) = InlineAssignHelper(arguments(1), GetType(Object))
        Dim dm As New DynamicMethod(String.Concat("_Set", propertyInfo.Name, "_"),
                            GetType(Void), arguments, propertyInfo.DeclaringType)
        Dim generator = dm.GetILGenerator()
        generator.Emit(OpCodes.Ldarg_0)
        generator.Emit(OpCodes.Castclass, propertyInfo.DeclaringType)
        generator.Emit(OpCodes.Ldarg_1)
        generator.Emit(If(propertyInfo.PropertyType.IsClass,
                          OpCodes.Castclass, OpCodes.Unbox_Any),
                          propertyInfo.PropertyType)
        generator.EmitCall(OpCodes.Callvirt, setMethod, Nothing)
        generator.Emit(OpCodes.Ret)
        Return DirectCast(dm.CreateDelegate(GetType(PropertySetterDelegate)), PropertySetterDelegate)
    End Function

    Friend Shared Function Getter(propertyInfo As PropertyInfo) As PropertyGetterDelegate
        Dim getMethod = propertyInfo.GetGetMethod()
        If getMethod Is Nothing Then
            Return Nothing
        End If
        Dim arguments As Type() = New Type(0) {}
        arguments(0) = GetType(Object)
        Dim dm As New DynamicMethod(String.Concat("_Get", propertyInfo.Name, "_"),
                                    GetType(Object), arguments, propertyInfo.DeclaringType)
        Dim generator = dm.GetILGenerator()
        generator.DeclareLocal(GetType(Object))
        generator.Emit(OpCodes.Ldarg_0)
        generator.Emit(OpCodes.Castclass, propertyInfo.DeclaringType)
        generator.EmitCall(OpCodes.Callvirt, getMethod, Nothing)
        If Not propertyInfo.PropertyType.IsClass Then
            generator.Emit(OpCodes.Box, propertyInfo.PropertyType)
        End If
        generator.Emit(OpCodes.Ret)
        Return DirectCast(dm.CreateDelegate(GetType(PropertyGetterDelegate)), PropertyGetterDelegate)
    End Function

    Private Shared Function InlineAssignHelper(Of T)(ByRef target As T, value As T) As T
        target = value
        Return value
    End Function

    Friend Shared Function CreateInstance(objectType As Type) As ActivatorDelegate
        Dim dm As New DynamicMethod("Create_" + objectType.Name,
                                    objectType, Type.EmptyTypes)
        Dim ctor = objectType.GetConstructor(Type.EmptyTypes)
        Dim generator = dm.GetILGenerator()
        generator.Emit(OpCodes.Newobj, ctor)
        generator.Emit(OpCodes.Ret)
        Return DirectCast(dm.CreateDelegate(GetType(ActivatorDelegate)), ActivatorDelegate)
    End Function

End Class
