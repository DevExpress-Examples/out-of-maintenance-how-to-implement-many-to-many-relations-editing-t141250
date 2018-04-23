Imports System
Imports System.Linq
Imports DevExpress.Mvvm.POCO
Imports Scaffolding.ManyToMany.Common.Utils
Imports Scaffolding.ManyToMany.SchoolContextDataModel
Imports Scaffolding.ManyToMany.Common.DataModel
Imports Scaffolding.ManyToMany.Model
Imports Scaffolding.ManyToMany.Common.ViewModel

Namespace Scaffolding.ManyToMany.ViewModels
    ''' <summary>
    ''' Represents the Students collection view model.
    ''' </summary>
    Partial Public Class StudentCollectionViewModel
        Inherits CollectionViewModel(Of Student, Integer, ISchoolContextUnitOfWork)

        ''' <summary>
        ''' Creates a new instance of StudentCollectionViewModel as a POCO view model.
        ''' </summary>
        ''' <param name="unitOfWorkFactory">A factory used to create a unit of work instance.</param>
        Public Shared Function Create(Optional ByVal unitOfWorkFactory As IUnitOfWorkFactory(Of ISchoolContextUnitOfWork) = Nothing) As StudentCollectionViewModel
            Return ViewModelSource.Create(Function() New StudentCollectionViewModel(unitOfWorkFactory))
        End Function

        ''' <summary>
        ''' Initializes a new instance of the StudentCollectionViewModel class.
        ''' This constructor is declared protected to avoid undesired instantiation of the StudentCollectionViewModel type without the POCO proxy factory.
        ''' </summary>
        ''' <param name="unitOfWorkFactory">A factory used to create a unit of work instance.</param>
        Protected Sub New(Optional ByVal unitOfWorkFactory As IUnitOfWorkFactory(Of ISchoolContextUnitOfWork) = Nothing)
            MyBase.New(If(unitOfWorkFactory, UnitOfWorkSource.GetUnitOfWorkFactory()), Function(x) x.Students)
        End Sub
    End Class
End Namespace