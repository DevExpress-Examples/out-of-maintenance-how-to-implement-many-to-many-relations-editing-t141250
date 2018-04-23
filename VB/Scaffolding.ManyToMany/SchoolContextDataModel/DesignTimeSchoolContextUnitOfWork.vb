Imports DevExpress.Mvvm.DataModel
Imports DevExpress.Mvvm.DataModel.DesignTime
Imports Scaffolding.ManyToMany.Model
Imports System
Imports System.Collections.Generic
Imports System.Linq

Namespace Scaffolding.ManyToMany.SchoolContextDataModel

	''' <summary>
	''' A SchoolContextDesignTimeUnitOfWork instance that represents the design-time implementation of the ISchoolContextUnitOfWork interface.
	''' </summary>
	Public Class SchoolContextDesignTimeUnitOfWork
		Inherits DesignTimeUnitOfWork
		Implements ISchoolContextUnitOfWork

		''' <summary>
		''' Initializes a new instance of the SchoolContextDesignTimeUnitOfWork class.
		''' </summary>
		Public Sub New()
		End Sub

		Private ReadOnly Property ISchoolContextUnitOfWork_Courses() As IRepository(Of Course, Integer) Implements ISchoolContextUnitOfWork.Courses
			Get
				Return GetRepository(Function(x As Course) x.CourseId)
			End Get
		End Property

		Private ReadOnly Property ISchoolContextUnitOfWork_Students() As IRepository(Of Student, Integer) Implements ISchoolContextUnitOfWork.Students
			Get
				Return GetRepository(Function(x As Student) x.StudentId)
			End Get
		End Property
	End Class
End Namespace
