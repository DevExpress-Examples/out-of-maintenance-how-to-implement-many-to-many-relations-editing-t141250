Imports DevExpress.Mvvm.DataAnnotations
Imports Scaffolding.ManyToMany.Localization
Imports Scaffolding.ManyToMany.Model

Namespace Scaffolding.ManyToMany.SchoolContextDataModel

	Public Class SchoolContextMetadataProvider
		Public Shared Sub BuildMetadata(ByVal builder As MetadataBuilder(Of Course))
			builder.DisplayName(SchoolContextResources.Course)
			builder.Property(Function(x) x.CourseId).DisplayName(SchoolContextResources.Course_CourseId)
			builder.Property(Function(x) x.Title).DisplayName(SchoolContextResources.Course_Title)
		End Sub
		Public Shared Sub BuildMetadata(ByVal builder As MetadataBuilder(Of Student))
			builder.DisplayName(SchoolContextResources.Student)
			builder.Property(Function(x) x.StudentId).DisplayName(SchoolContextResources.Student_StudentId)
			builder.Property(Function(x) x.FirstName).DisplayName(SchoolContextResources.Student_FirstName)
			builder.Property(Function(x) x.LastName).DisplayName(SchoolContextResources.Student_LastName)
		End Sub
	End Class
End Namespace