Imports DevExpress.Mvvm.POCO
Imports Scaffolding.ManyToMany.Common.DataModel
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Threading.Tasks

Namespace Scaffolding.ManyToMany.Common.ViewModel
    Partial Public Class SingleObjectViewModel(Of TEntity As Class, TPrimaryKey, TUnitOfWork As IUnitOfWork)

        Protected Function CreateAddRemoveDetailEntitiesViewModel(Of TDetailEntity As Class, TDetailPrimaryKey)(ByVal getDetailsRepositoryFunc As Func(Of TUnitOfWork, IRepository(Of TDetailEntity, TDetailPrimaryKey)), ByVal getDetailsFunc As Func(Of TEntity, ICollection(Of TDetailEntity))) As AddRemoveDetailEntitiesViewModel(Of TEntity, TPrimaryKey, TDetailEntity, TDetailPrimaryKey, TUnitOfWork)
            Return AddRemoveDetailEntitiesViewModel(Of TEntity, TPrimaryKey, TDetailEntity, TDetailPrimaryKey, TUnitOfWork).Create(UnitOfWorkFactory, Me.getRepositoryFunc, getDetailsRepositoryFunc, getDetailsFunc, Me.GetPrimaryKey(Entity))
        End Function

    End Class
End Namespace
