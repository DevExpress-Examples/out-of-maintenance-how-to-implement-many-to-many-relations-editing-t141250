Imports DevExpress.Mvvm.DataModel
Imports Scaffolding.ManyToMany.Model
Imports System
Imports System.Collections.Generic
Imports System.Linq

Namespace Scaffolding.ManyToMany.SchoolContextDataModel

	''' <summary>
	''' ISchoolContextUnitOfWork extends the IUnitOfWork interface with repositories representing specific entities.
	''' </summary>
	Public Interface ISchoolContextUnitOfWork
		Inherits IUnitOfWork

		''' <summary>
		''' The Course entities repository.
		''' </summary>
		ReadOnly Property Courses() As IRepository(Of Course, Integer)

		''' <summary>
		''' The Student entities repository.
		''' </summary>
		ReadOnly Property Students() As IRepository(Of Student, Integer)
	End Interface
End Namespace
