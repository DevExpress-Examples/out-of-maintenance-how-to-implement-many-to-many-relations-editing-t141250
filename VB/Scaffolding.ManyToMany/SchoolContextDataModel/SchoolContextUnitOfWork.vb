Imports DevExpress.Mvvm.DataModel
Imports DevExpress.Mvvm.DataModel.EF6
Imports Scaffolding.ManyToMany.Model
Imports System
Imports System.Collections.Generic
Imports System.Linq

Namespace Scaffolding.ManyToMany.SchoolContextDataModel

	''' <summary>
	''' A SchoolContextUnitOfWork instance that represents the run-time implementation of the ISchoolContextUnitOfWork interface.
	''' </summary>
	Public Class SchoolContextUnitOfWork
		Inherits DbUnitOfWork(Of SchoolContext)
		Implements ISchoolContextUnitOfWork

		Public Sub New(ByVal contextFactory As Func(Of SchoolContext))
			MyBase.New(contextFactory)
		End Sub

		Private ReadOnly Property ISchoolContextUnitOfWork_Courses() As IRepository(Of Course, Integer) Implements ISchoolContextUnitOfWork.Courses
			Get
				Return GetRepository(Function(x) x.Set(Of Course)(), Function(x As Course) x.CourseId)
			End Get
		End Property

		Private ReadOnly Property ISchoolContextUnitOfWork_Students() As IRepository(Of Student, Integer) Implements ISchoolContextUnitOfWork.Students
			Get
				Return GetRepository(Function(x) x.Set(Of Student)(), Function(x As Student) x.StudentId)
			End Get
		End Property
	End Class
End Namespace
