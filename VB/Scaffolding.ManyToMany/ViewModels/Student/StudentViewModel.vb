Imports System
Imports System.Linq
Imports System.Linq.Expressions
Imports System.Collections.Generic
Imports System.Collections.ObjectModel
Imports DevExpress.Mvvm
Imports DevExpress.Mvvm.POCO
Imports DevExpress.Mvvm.DataModel
Imports DevExpress.Mvvm.ViewModel
Imports Scaffolding.ManyToMany.SchoolContextDataModel
Imports Scaffolding.ManyToMany.Common
Imports Scaffolding.ManyToMany.Model

Namespace Scaffolding.ManyToMany.ViewModels

	''' <summary>
	''' Represents the single Student object view model.
	''' </summary>
	Partial Public Class StudentViewModel
		Inherits SingleObjectViewModel(Of Student, Integer, ISchoolContextUnitOfWork)

		''' <summary>
		''' Creates a new instance of StudentViewModel as a POCO view model.
		''' </summary>
		''' <param name="unitOfWorkFactory">A factory used to create a unit of work instance.</param>
		Public Shared Function Create(Optional ByVal unitOfWorkFactory As IUnitOfWorkFactory(Of ISchoolContextUnitOfWork) = Nothing) As StudentViewModel
			Return ViewModelSource.Create(Function() New StudentViewModel(unitOfWorkFactory))
		End Function

		''' <summary>
		''' Initializes a new instance of the StudentViewModel class.
		''' This constructor is declared protected to avoid undesired instantiation of the StudentViewModel type without the POCO proxy factory.
		''' </summary>
		''' <param name="unitOfWorkFactory">A factory used to create a unit of work instance.</param>
		Protected Sub New(Optional ByVal unitOfWorkFactory As IUnitOfWorkFactory(Of ISchoolContextUnitOfWork) = Nothing)
			MyBase.New(If(unitOfWorkFactory, UnitOfWorkSource.GetUnitOfWorkFactory()), Function(x) x.Students, Function(x) x.LastName)
		End Sub


		Protected Overrides Sub RefreshLookUpCollections(ByVal raisePropertyChanged As Boolean)
			MyBase.RefreshLookUpCollections(raisePropertyChanged)
			CoursesAttendingDetailEntities = CreateAddRemoveDetailEntitiesViewModel(Function(x) x.Courses, Function(x) x.CoursesAttending)
		End Sub

		Private privateCoursesAttendingDetailEntities As AddRemoveDetailEntitiesViewModel(Of Student, Int32, Course, Int32, ISchoolContextUnitOfWork)
		Public Overridable Property CoursesAttendingDetailEntities() As AddRemoveDetailEntitiesViewModel(Of Student, Int32, Course, Int32, ISchoolContextUnitOfWork)
			Get
				Return privateCoursesAttendingDetailEntities
			End Get
			Protected Set(ByVal value As AddRemoveDetailEntitiesViewModel(Of Student, Int32, Course, Int32, ISchoolContextUnitOfWork))
				privateCoursesAttendingDetailEntities = value
			End Set
		End Property
	End Class
End Namespace
