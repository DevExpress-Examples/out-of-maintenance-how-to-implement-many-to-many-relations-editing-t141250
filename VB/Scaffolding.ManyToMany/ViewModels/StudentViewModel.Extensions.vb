Imports Scaffolding.ManyToMany.Common.ViewModel
Imports Scaffolding.ManyToMany.Model
Imports Scaffolding.ManyToMany.SchoolContextDataModel
Imports System
Imports System.Linq

Namespace Scaffolding.ManyToMany.ViewModels
    Partial Public Class StudentViewModel
        Protected Overrides Sub RefreshLookUpCollections(ByVal key As Integer)
            MyBase.RefreshLookUpCollections(key)
            StudentCourses = CreateAddRemoveDetailEntitiesViewModel(Function(x) x.Courses, Function(x) x.CoursesAttending)
        End Sub
        Private privateStudentCourses As AddRemoveDetailEntitiesViewModel(Of Student, Integer, Course, Integer, ISchoolContextUnitOfWork)
        Public Overridable Property StudentCourses() As AddRemoveDetailEntitiesViewModel(Of Student, Integer, Course, Integer, ISchoolContextUnitOfWork)
            Get
                Return privateStudentCourses
            End Get
            Protected Set(ByVal value As AddRemoveDetailEntitiesViewModel(Of Student, Integer, Course, Integer, ISchoolContextUnitOfWork))
                privateStudentCourses = value
            End Set
        End Property
    End Class
End Namespace
