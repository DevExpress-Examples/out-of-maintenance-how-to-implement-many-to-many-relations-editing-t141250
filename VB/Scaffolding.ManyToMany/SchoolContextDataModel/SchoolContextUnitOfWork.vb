Imports System
Imports System.Linq
Imports System.Data
Imports System.Data.Entity
Imports System.Linq.Expressions
Imports System.Collections.Generic
Imports Scaffolding.ManyToMany.Common.Utils
Imports Scaffolding.ManyToMany.Common.DataModel
Imports Scaffolding.ManyToMany.Common.DataModel.EntityFramework
Imports Scaffolding.ManyToMany.Model

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
                Return GetRepository(Function(x) x.Set(Of Course)(), Function(x) x.CourseId)
            End Get
        End Property

        Private ReadOnly Property ISchoolContextUnitOfWork_Students() As IRepository(Of Student, Integer) Implements ISchoolContextUnitOfWork.Students
            Get
                Return GetRepository(Function(x) x.Set(Of Student)(), Function(x) x.StudentId)
            End Get
        End Property
    End Class
End Namespace
