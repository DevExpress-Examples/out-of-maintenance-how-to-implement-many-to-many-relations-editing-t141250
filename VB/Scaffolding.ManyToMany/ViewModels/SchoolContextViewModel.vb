Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.ComponentModel
Imports DevExpress.Mvvm
Imports DevExpress.Mvvm.POCO
Imports Scaffolding.ManyToMany.Common.DataModel
Imports Scaffolding.ManyToMany.Common.ViewModel
Imports Scaffolding.ManyToMany.SchoolContextDataModel
Imports Scaffolding.ManyToMany.Model

Namespace Scaffolding.ManyToMany.ViewModels
    ''' <summary>
    ''' Represents the root POCO view model for the SchoolContext data model.
    ''' </summary>
    Partial Public Class SchoolContextViewModel
        Inherits DocumentsViewModel(Of SchoolContextModuleDescription, ISchoolContextUnitOfWork)

        Private Const TablesGroup As String = "Tables"

        Private Const ViewsGroup As String = "Views"

        ''' <summary>
        ''' Creates a new instance of SchoolContextViewModel as a POCO view model.
        ''' </summary>
        Public Shared Function Create() As SchoolContextViewModel
            Return ViewModelSource.Create(Function() New SchoolContextViewModel())
        End Function

        ''' <summary>
        ''' Initializes a new instance of the SchoolContextViewModel class.
        ''' This constructor is declared protected to avoid undesired instantiation of the SchoolContextViewModel type without the POCO proxy factory.
        ''' </summary>
        Protected Sub New()
            MyBase.New(UnitOfWorkSource.GetUnitOfWorkFactory())
        End Sub

        Protected Overrides Function CreateModules() As SchoolContextModuleDescription()
            Return New SchoolContextModuleDescription() { _
                New SchoolContextModuleDescription("Courses", "CourseCollectionView", TablesGroup, GetPeekCollectionViewModelFactory(Function(x) x.Courses)), _
                New SchoolContextModuleDescription("Students", "StudentCollectionView", TablesGroup, GetPeekCollectionViewModelFactory(Function(x) x.Students)) _
            }
        End Function


    End Class

    Partial Public Class SchoolContextModuleDescription
        Inherits ModuleDescription(Of SchoolContextModuleDescription)

        Public Sub New(ByVal title As String, ByVal documentType As String, ByVal group As String, Optional ByVal peekCollectionViewModelFactory As Func(Of SchoolContextModuleDescription, Object) = Nothing)
            MyBase.New(title, documentType, group, peekCollectionViewModelFactory)
        End Sub
    End Class
End Namespace