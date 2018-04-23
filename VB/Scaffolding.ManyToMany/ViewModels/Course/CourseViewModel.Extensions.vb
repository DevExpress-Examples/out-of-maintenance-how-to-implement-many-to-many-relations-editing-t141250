Imports Scaffolding.ManyToMany.Common.ViewModel
Imports Scaffolding.ManyToMany.Model
Imports Scaffolding.ManyToMany.SchoolContextDataModel
Imports System
Imports System.Linq


Namespace Scaffolding.ManyToMany.ViewModels
    Partial Public Class CourseViewModel
        Protected Overrides Sub RefreshLookUpCollections(ByVal raisePropertyChanged As Boolean)
            MyBase.RefreshLookUpCollections(raisePropertyChanged)
            CourseStudents = CreateAddRemoveDetailEntitiesViewModel(Function(x) x.Students, Function(x) x.Students)
        End Sub
        Private privateCourseStudents As AddRemoveDetailEntitiesViewModel(Of Course, Integer, Student, Integer, ISchoolContextUnitOfWork)
        Public Overridable Property CourseStudents() As AddRemoveDetailEntitiesViewModel(Of Course, Integer, Student, Integer, ISchoolContextUnitOfWork)
            Get
                Return privateCourseStudents
            End Get
            Protected Set(ByVal value As AddRemoveDetailEntitiesViewModel(Of Course, Integer, Student, Integer, ISchoolContextUnitOfWork))
                privateCourseStudents = value
            End Set
        End Property
    End Class
End Namespace