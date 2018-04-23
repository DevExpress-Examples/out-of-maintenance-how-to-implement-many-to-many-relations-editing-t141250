using DevExpress.Mvvm.DataModel;
using DevExpress.Mvvm.DataModel.EF6;
using Scaffolding.ManyToMany.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Scaffolding.ManyToMany.SchoolContextDataModel {

    /// <summary>
    /// A SchoolContextUnitOfWork instance that represents the run-time implementation of the ISchoolContextUnitOfWork interface.
    /// </summary>
    public class SchoolContextUnitOfWork : DbUnitOfWork<SchoolContext>, ISchoolContextUnitOfWork {

        public SchoolContextUnitOfWork(Func<SchoolContext> contextFactory)
            : base(contextFactory) {
        }

        IRepository<Course, int> ISchoolContextUnitOfWork.Courses
        {
            get { return GetRepository(x => x.Set<Course>(), (Course x) => x.CourseId); }
        }

        IRepository<Student, int> ISchoolContextUnitOfWork.Students
        {
            get { return GetRepository(x => x.Set<Student>(), (Student x) => x.StudentId); }
        }
    }
}
