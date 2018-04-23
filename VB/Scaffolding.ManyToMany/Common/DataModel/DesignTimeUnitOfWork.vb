Imports System
Imports System.Linq
Imports System.Collections.Generic
Imports System.Linq.Expressions

Namespace Scaffolding.ManyToMany.Common.DataModel
    ''' <summary>
    ''' A DbUnitOfWork class instance represents the implementation of the Unit Of Work pattern for design-time mode. 
    ''' </summary>
    Public Class DesignTimeUnitOfWork
        Inherits UnitOfWorkBase
        Implements IUnitOfWork

        Public Shared ReadOnly Instance As IUnitOfWork = New DesignTimeUnitOfWork()

        Private Sub IUnitOfWork_SaveChanges() Implements IUnitOfWork.SaveChanges
        End Sub

        Private Function IUnitOfWork_GetState(ByVal entity As Object) As EntityState Implements IUnitOfWork.GetState
            Return EntityState.Detached
        End Function

        Private Sub IUnitOfWork_Update(ByVal entity As Object) Implements IUnitOfWork.Update
        End Sub

        Private Sub IUnitOfWork_Detach(ByVal entity As Object) Implements IUnitOfWork.Detach
        End Sub

        Private Function IUnitOfWork_HasChanges() As Boolean Implements IUnitOfWork.HasChanges
            Return False
        End Function

        Protected Function GetRepository(Of TEntity As Class, TPrimaryKey)(ByVal getPrimaryKeyExpression As Expression(Of Func(Of TEntity, TPrimaryKey)), Optional ByVal setPrimaryKeyAction As Action(Of TEntity, TPrimaryKey) = Nothing) As IRepository(Of TEntity, TPrimaryKey)
            Return GetRepositoryCore(Of IRepository(Of TEntity, TPrimaryKey), TEntity)(Function() New DesignTimeRepository(Of TEntity, TPrimaryKey)(getPrimaryKeyExpression, setPrimaryKeyAction))
        End Function

        Protected Function GetReadOnlyRepository(Of TEntity As Class)() As IReadOnlyRepository(Of TEntity)
            Return GetRepositoryCore(Of IReadOnlyRepository(Of TEntity), TEntity)(Function() New DesignTimeReadOnlyRepository(Of TEntity)())
        End Function
    End Class
End Namespace