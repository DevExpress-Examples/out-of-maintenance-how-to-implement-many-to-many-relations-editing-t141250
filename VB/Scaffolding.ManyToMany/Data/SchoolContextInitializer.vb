Imports System
Imports System.Collections.Generic
Imports System.Data.Entity
Imports System.Linq
Imports System.Text
Imports System.Threading.Tasks

Namespace Scaffolding.ManyToMany.Model
	Public Class SchoolContextInitializer
		Inherits DropCreateDatabaseAlways(Of SchoolContext)

		Protected Overrides Sub Seed(ByVal context As SchoolContext)
			MyBase.Seed(context)
			Dim course1 = New Course() With {.Title = "Course 1"}
			Dim course2 = New Course() With {.Title = "Course 2"}
			Dim course3 = New Course() With {.Title = "Course 3"}
			Dim course4 = New Course() With {.Title = "Course 4"}
			Dim course5 = New Course() With {.Title = "Course 5"}
			Dim course6 = New Course() With {.Title = "Course 6"}

			Dim student1 = New Student() With {.FirstName = "Carolyn", .LastName = "Baker"}
			Dim student2 = New Student() With {.FirstName = "Amber", .LastName = "Seaman"}
			Dim student3 = New Student() With {.FirstName = "Annie", .LastName = "Vicars"}


			student1.CoursesAttending.Add(course1)
			student1.CoursesAttending.Add(course2)
			student1.CoursesAttending.Add(course3)

			student2.CoursesAttending.Add(course2)

			context.Courses.AddRange( { course1, course2, course3, course4, course5, course6 })
			context.Students.AddRange( { student1, student2, student3 })

			context.SaveChanges()
		End Sub
	End Class
End Namespace
