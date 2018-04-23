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
Imports DevExpress.Mvvm

Namespace Scaffolding.ManyToMany.SchoolContextDataModel
    ''' <summary>
    ''' Provides methods to obtain the relevant IUnitOfWorkFactory.
    ''' </summary>
    Public NotInheritable Class UnitOfWorkSource

        Private Sub New()
        End Sub


        #Region "inner classes"
        Private Class DbUnitOfWorkFactory
            Implements IUnitOfWorkFactory(Of ISchoolContextUnitOfWork)

            Public Shared ReadOnly Instance As IUnitOfWorkFactory(Of ISchoolContextUnitOfWork) = New DbUnitOfWorkFactory()
            Private Sub New()
            End Sub
            Private Function IUnitOfWorkFactoryGeneric_CreateUnitOfWork() As ISchoolContextUnitOfWork Implements IUnitOfWorkFactory(Of ISchoolContextUnitOfWork).CreateUnitOfWork
                Return New SchoolContextUnitOfWork(Function() New SchoolContext())
            End Function
        End Class

        Private Class DesignUnitOfWorkFactory
            Implements IUnitOfWorkFactory(Of ISchoolContextUnitOfWork)

            Public Shared ReadOnly Instance As IUnitOfWorkFactory(Of ISchoolContextUnitOfWork) = New DesignUnitOfWorkFactory()
            Private Sub New()
            End Sub
            Private Function IUnitOfWorkFactoryGeneric_CreateUnitOfWork() As ISchoolContextUnitOfWork Implements IUnitOfWorkFactory(Of ISchoolContextUnitOfWork).CreateUnitOfWork
                Return New SchoolContextDesignTimeUnitOfWork()
            End Function
        End Class
        #End Region

        ''' <summary>
        ''' Returns the IUnitOfWorkFactory implementation based on the current mode (run-time or design-time).
        ''' </summary>
        Public Shared Function GetUnitOfWorkFactory() As IUnitOfWorkFactory(Of ISchoolContextUnitOfWork)
            Return GetUnitOfWorkFactory(ViewModelBase.IsInDesignMode)
        End Function

        ''' <summary>
        ''' Returns the IUnitOfWorkFactory implementation based on the given mode (run-time or design-time).
        ''' </summary>
        ''' <param name="isInDesignTime">Used to determine which implementation of IUnitOfWorkFactory should be returned.</param>
        Public Shared Function GetUnitOfWorkFactory(ByVal isInDesignTime As Boolean) As IUnitOfWorkFactory(Of ISchoolContextUnitOfWork)
            Return If(isInDesignTime, DesignUnitOfWorkFactory.Instance, DbUnitOfWorkFactory.Instance)
        End Function
    End Class
End Namespace