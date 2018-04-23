Imports System
Imports System.Linq
Imports System.Data.Entity
Imports System.Collections.Generic
Imports System.Collections.ObjectModel

Namespace Scaffolding.ManyToMany.Common.DataModel.EntityFramework
    ''' <summary>
    ''' A DbReadOnlyRepository is a IReadOnlyRepository interface implementation representing the collection of all entities in the unit of work, or that can be queried from the database, of a given type. 
    ''' DbReadOnlyRepository objects are created from a DbUnitOfWork using the GetReadOnlyRepository method. 
    ''' DbReadOnlyRepository provides only read-only operations against entities of a given type.
    ''' </summary>
    ''' <typeparam name="TEntity">Repository entity type.</typeparam>
    ''' <typeparam name="TDbContext">DbContext type.</typeparam>
    Public Class DbReadOnlyRepository(Of TEntity As Class, TDbContext As DbContext)
        Implements IReadOnlyRepository(Of TEntity)

        Private ReadOnly dbSetAccessor As Func(Of TDbContext, DbSet(Of TEntity))

        Private ReadOnly unitOfWork_Renamed As DbUnitOfWork(Of TDbContext)

        Private dbSet_Renamed As DbSet(Of TEntity)

        ''' <summary>
        ''' Initializes a new instance of DbReadOnlyRepository class.
        ''' </summary>
        ''' <param name="unitOfWork">Owner unit of work that provides context for repository entities.</param>
        ''' <param name="dbSetAccessor">Function that returns DbSet entities from Entity Framework DbContext.</param>
        Public Sub New(ByVal unitOfWork As DbUnitOfWork(Of TDbContext), ByVal dbSetAccessor As Func(Of TDbContext, DbSet(Of TEntity)))
            Me.dbSetAccessor = dbSetAccessor
            Me.unitOfWork_Renamed = unitOfWork
        End Sub

        Protected ReadOnly Property DbSet() As DbSet(Of TEntity)
            Get
                If dbSet_Renamed Is Nothing Then
                    dbSet_Renamed = dbSetAccessor(unitOfWork_Renamed.Context)
                End If
                Return dbSet_Renamed
            End Get
        End Property

        Protected ReadOnly Property Context() As TDbContext
            Get
                Return unitOfWork_Renamed.Context
            End Get
        End Property

        Protected Overridable Function GetEntities() As IQueryable(Of TEntity)
            Return DbSet
        End Function

        #Region "IReadOnlyRepository"
        Private Function IReadOnlyRepositoryGeneric_GetEntities() As IQueryable(Of TEntity) Implements IReadOnlyRepository(Of TEntity).GetEntities
            Return GetEntities()
        End Function

        Private ReadOnly Property IReadOnlyRepositoryGeneric_UnitOfWork() As IUnitOfWork Implements IReadOnlyRepository(Of TEntity).UnitOfWork
            Get
                Return unitOfWork_Renamed
            End Get
        End Property

        Private ReadOnly Property IReadOnlyRepositoryGeneric_Local() As ObservableCollection(Of TEntity) Implements IReadOnlyRepository(Of TEntity).Local
            Get
                Return DbSet.Local
            End Get
        End Property
        #End Region
    End Class
End Namespace
