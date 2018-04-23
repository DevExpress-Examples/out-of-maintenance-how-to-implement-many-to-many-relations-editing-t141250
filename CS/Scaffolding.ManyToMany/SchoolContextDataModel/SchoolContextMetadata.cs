using DevExpress.Mvvm.DataAnnotations;
using Scaffolding.ManyToMany.Localization;
using Scaffolding.ManyToMany.Model;

namespace Scaffolding.ManyToMany.SchoolContextDataModel {

    public class SchoolContextMetadataProvider {
        public static void BuildMetadata(MetadataBuilder<Course> builder) {
            builder.DisplayName(SchoolContextResources.Course);
            builder.Property(x => x.CourseId).DisplayName(SchoolContextResources.Course_CourseId);
            builder.Property(x => x.Title).DisplayName(SchoolContextResources.Course_Title);
        }
        public static void BuildMetadata(MetadataBuilder<Student> builder) {
            builder.DisplayName(SchoolContextResources.Student);
            builder.Property(x => x.StudentId).DisplayName(SchoolContextResources.Student_StudentId);
            builder.Property(x => x.FirstName).DisplayName(SchoolContextResources.Student_FirstName);
            builder.Property(x => x.LastName).DisplayName(SchoolContextResources.Student_LastName);
        }
    }
}