using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scaffolding.ManyToMany.Model {
    public class SchoolContextInitializer : DropCreateDatabaseAlways<SchoolContext> {
        protected override void Seed(SchoolContext context) {
            base.Seed(context);
            var course1 = new Course() { Title = "Course 1" };
            var course2 = new Course() { Title = "Course 2" };
            var course3 = new Course() { Title = "Course 3" };
            var course4 = new Course() { Title = "Course 4" };
            var course5 = new Course() { Title = "Course 5" };
            var course6 = new Course() { Title = "Course 6" };

            var student1 = new Student() { FirstName = "Carolyn", LastName = "Baker" };
            var student2 = new Student() { FirstName = "Amber", LastName = "Seaman" };
            var student3 = new Student() { FirstName = "Annie", LastName = "Vicars" };


            student1.CoursesAttending.Add(course1);
            student1.CoursesAttending.Add(course2);
            student1.CoursesAttending.Add(course3);

            student2.CoursesAttending.Add(course2);

            context.Courses.AddRange(new[] { course1, course2, course3, course4, course5, course6 });
            context.Students.AddRange(new[] { student1, student2, student3 });

            context.SaveChanges();
        }
    }
}
