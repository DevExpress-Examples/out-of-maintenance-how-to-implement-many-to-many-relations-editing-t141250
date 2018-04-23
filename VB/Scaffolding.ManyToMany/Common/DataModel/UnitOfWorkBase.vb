Imports System
Imports System.Linq
Imports System.Collections.Generic
Imports System.Linq.Expressions

Namespace Scaffolding.ManyToMany.Common.DataModel
    ''' <summary>
    ''' The base class for unit of works that provides the storage for repositories. 
    ''' </summary>
    Public Class UnitOfWorkBase

        Private ReadOnly repositories As New Dictionary(Of Type, Object)()

        Protected Function GetRepositoryCore(Of TRepository As IReadOnlyRepository(Of TEntity), TEntity As Class)(ByVal createRepositoryFunc As Func(Of TRepository)) As TRepository
            Dim result As Object = Nothing
            If Not repositories.TryGetValue(GetType(TEntity), result) Then
                result = createRepositoryFunc()
                repositories(GetType(TEntity)) = result
            End If
            Return DirectCast(result, TRepository)
        End Function
    End Class
End Namespace