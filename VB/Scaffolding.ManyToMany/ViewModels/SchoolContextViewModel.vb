Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.ComponentModel
Imports DevExpress.Mvvm
Imports DevExpress.Mvvm.DataAnnotations
Imports DevExpress.Mvvm.POCO
Imports DevExpress.Mvvm.ViewModel
Imports Scaffolding.ManyToMany.Localization
Imports Scaffolding.ManyToMany.SchoolContextDataModel

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

		Shared Sub New()
			MetadataLocator.Default = MetadataLocator.Create().AddMetadata(Of SchoolContextMetadataProvider)()
		End Sub
		''' <summary>
		''' Initializes a new instance of the SchoolContextViewModel class.
		''' This constructor is declared protected to avoid undesired instantiation of the SchoolContextViewModel type without the POCO proxy factory.
		''' </summary>
		Protected Sub New()
			MyBase.New(UnitOfWorkSource.GetUnitOfWorkFactory())
		End Sub

		Protected Overrides Function CreateModules() As SchoolContextModuleDescription()
			Return New SchoolContextModuleDescription() {
				New SchoolContextModuleDescription(SchoolContextResources.CoursePlural, "CourseCollectionView", TablesGroup, GetPeekCollectionViewModelFactory(Function(x) x.Courses)),
				New SchoolContextModuleDescription(SchoolContextResources.StudentPlural, "StudentCollectionView", TablesGroup, GetPeekCollectionViewModelFactory(Function(x) x.Students))
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