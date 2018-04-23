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
