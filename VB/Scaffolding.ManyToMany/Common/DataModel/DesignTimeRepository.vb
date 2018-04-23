Imports System
Imports System.Linq
Imports System.Linq.Expressions
Imports System.Collections.Generic
Imports DevExpress.Mvvm
Imports Scaffolding.ManyToMany.Common.Utils

Namespace Scaffolding.ManyToMany.Common.DataModel
    ''' <summary>
    ''' DesignTimeRepository is an IRepository interface implementation representing the collection of entities of a given type for design-time mode. 
    ''' DesignTimeRepository objects are created from a DesignTimeUnitOfWork class instance using the GetRepository method. 
    ''' Write operations against entities of a given type are not supported in this implementation and throw InvalidOperationException.
    ''' </summary>
    ''' <typeparam name="TEntity">A repository entity type.</typeparam>
    ''' <typeparam name="TPrimaryKey">An entity primary key type.</typeparam>
    Public Class DesignTimeRepository(Of TEntity As Class, TPrimaryKey)
        Inherits DesignTimeReadOnlyRepository(Of TEntity)
        Implements IRepository(Of TEntity, TPrimaryKey)


        Private ReadOnly getPrimaryKeyExpression_Renamed As Expression(Of Func(Of TEntity, TPrimaryKey))
        Private ReadOnly entityTraits As EntityTraits(Of TEntity, TPrimaryKey)

        ''' <summary>
        ''' Initializes a new instance of the DesignTimeRepository class.
        ''' </summary>
        ''' <param name="getPrimaryKeyExpression">A lambda-expression that returns the entity primary key.</param>
        ''' <param name="setPrimaryKeyAction">An action that provides a way to set an entity primary key in case the primary key is a nullable type, otherwise this parameter can be ommited.</param>
        Public Sub New(ByVal getPrimaryKeyExpression As Expression(Of Func(Of TEntity, TPrimaryKey)), Optional ByVal setPrimaryKeyAction As Action(Of TEntity, TPrimaryKey) = Nothing)
            Me.getPrimaryKeyExpression_Renamed = getPrimaryKeyExpression
            Me.entityTraits = ExpressionHelper.GetEntityTraits(Me, getPrimaryKeyExpression, setPrimaryKeyAction)
        End Sub

        Protected Overridable Function CreateCore() As TEntity
            Return DesignTimeHelper.CreateDesignTimeObject(Of TEntity)()
        End Function

        Protected Overridable Function FindCore(ByVal key As TPrimaryKey) As TEntity
            Throw New InvalidOperationException()
        End Function

        Protected Overridable Sub RemoveCore(ByVal entity As TEntity)
            Throw New InvalidOperationException()
        End Sub

        Protected Overridable Function ReloadCore(ByVal entity As TEntity) As TEntity
            Throw New InvalidOperationException()
        End Function

        Protected Overridable Function GetPrimaryKeyCore(ByVal entity As TEntity) As TPrimaryKey
            Return entityTraits.GetPrimaryKey(entity)
        End Function

        Protected Overridable Sub SetPrimaryKeyCore(ByVal entity As TEntity, ByVal key As TPrimaryKey)
            Dim setPrimaryKeyaction = entityTraits.SetPrimaryKey
            setPrimaryKeyaction(entity, key)
        End Sub

        #Region "IRepository"
        Private Function IRepositoryGeneric_Find(ByVal key As TPrimaryKey) As TEntity Implements IRepository(Of TEntity, TPrimaryKey).Find
            Return FindCore(key)
        End Function

        Private Sub IRepositoryGeneric_Remove(ByVal entity As TEntity) Implements IRepository(Of TEntity, TPrimaryKey).Remove
            RemoveCore(entity)
        End Sub

        Private Function IRepositoryGeneric_Create() As TEntity Implements IRepository(Of TEntity, TPrimaryKey).Create
            Return CreateCore()
        End Function

        Private Function IRepositoryGeneric_Reload(ByVal entity As TEntity) As TEntity Implements IRepository(Of TEntity, TPrimaryKey).Reload
            Return ReloadCore(entity)
        End Function
        Private ReadOnly Property IRepositoryGeneric_GetPrimaryKeyExpression() As Expression(Of Func(Of TEntity, TPrimaryKey)) Implements IRepository(Of TEntity, TPrimaryKey).GetPrimaryKeyExpression
            Get
                Return getPrimaryKeyExpression_Renamed
            End Get
        End Property

        Private Function IRepositoryGeneric_GetPrimaryKey(ByVal entity As TEntity) As TPrimaryKey Implements IRepository(Of TEntity, TPrimaryKey).GetPrimaryKey
            Return GetPrimaryKeyCore(entity)
        End Function

        Private Function IRepositoryGeneric_HasPrimaryKey(ByVal entity As TEntity) As Boolean Implements IRepository(Of TEntity, TPrimaryKey).HasPrimaryKey
            Return entityTraits.HasPrimaryKey(entity)
        End Function

        Private Sub IRepositoryGeneric_SetPrimaryKey(ByVal entity As TEntity, ByVal key As TPrimaryKey) Implements IRepository(Of TEntity, TPrimaryKey).SetPrimaryKey
            SetPrimaryKeyCore(entity, key)
        End Sub
        #End Region
    End Class
End Namespace