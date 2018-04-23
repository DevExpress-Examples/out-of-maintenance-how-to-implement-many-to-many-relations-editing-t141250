Imports System
Imports System.Linq
Imports System.Linq.Expressions
Imports System.Collections.Generic
Imports System.Collections.ObjectModel
Imports DevExpress.Mvvm
Imports DevExpress.Mvvm.POCO
Imports Scaffolding.ManyToMany.Common.Utils
Imports Scaffolding.ManyToMany.SchoolContextDataModel
Imports Scaffolding.ManyToMany.Common.DataModel
Imports Scaffolding.ManyToMany.Model
Imports Scaffolding.ManyToMany.Common.ViewModel

Namespace Scaffolding.ManyToMany.ViewModels
    ''' <summary>
    ''' Represents the single Course object view model.
    ''' </summary>
    Partial Public Class CourseViewModel
        Inherits SingleObjectViewModel(Of Course, Integer, ISchoolContextUnitOfWork)

        ''' <summary>
        ''' Creates a new instance of CourseViewModel as a POCO view model.
        ''' </summary>
        ''' <param name="unitOfWorkFactory">A factory used to create a unit of work instance.</param>
        Public Shared Function Create(Optional ByVal unitOfWorkFactory As IUnitOfWorkFactory(Of ISchoolContextUnitOfWork) = Nothing) As CourseViewModel
            Return ViewModelSource.Create(Function() New CourseViewModel(unitOfWorkFactory))
        End Function

        ''' <summary>
        ''' Initializes a new instance of the CourseViewModel class.
        ''' This constructor is declared protected to avoid undesired instantiation of the CourseViewModel type without the POCO proxy factory.
        ''' </summary>
        ''' <param name="unitOfWorkFactory">A factory used to create a unit of work instance.</param>
        Protected Sub New(Optional ByVal unitOfWorkFactory As IUnitOfWorkFactory(Of ISchoolContextUnitOfWork) = Nothing)
            MyBase.New(If(unitOfWorkFactory, UnitOfWorkSource.GetUnitOfWorkFactory()), Function(x) x.Courses, Function(x) x.Title)
        End Sub
    End Class
End Namespace
