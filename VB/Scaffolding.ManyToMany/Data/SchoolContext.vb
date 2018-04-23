Imports System
Imports System.Collections.Generic
Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema
Imports System.Data.Entity
Imports System.Linq
Imports System.Text
Imports System.Threading.Tasks

Namespace Scaffolding.ManyToMany.Model
	Public Class Student
		Public Property StudentId() As Integer
		Public Property FirstName() As String
		Public Property LastName() As String

		Public Overridable Property CoursesAttending() As ICollection(Of Course)

		Public Sub New()
			CoursesAttending = New HashSet(Of Course)()
		End Sub
	End Class

	Public Class Course
		Public Property CourseId() As Integer
		Public Property Title() As String

		Public Overridable Property Students() As ICollection(Of Student)

		Public Sub New()
			Students = New HashSet(Of Student)()
		End Sub
	End Class

	Public Class SchoolContext
		Inherits DbContext

		Public Property Courses() As DbSet(Of Course)
		Public Property Students() As DbSet(Of Student)
		Public Sub New()
		End Sub
	End Class
End Namespace
