using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scaffolding.ManyToMany.Model {
    public class Student {
        public int StudentId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public virtual ICollection<Course> CoursesAttending { get; set; }

        public Student() {
            CoursesAttending = new HashSet<Course>();
        }
    }

    public class Course {
        public int CourseId { get; set; }
        public string Title { get; set; }

        public virtual ICollection<Student> Students { get; set; }

        public Course() {
            Students = new HashSet<Student>();
        }
    }

    public class SchoolContext : DbContext {
        public DbSet<Course> Courses { get; set; }
        public DbSet<Student> Students { get; set; }
        public SchoolContext() {
        }
    }
}
