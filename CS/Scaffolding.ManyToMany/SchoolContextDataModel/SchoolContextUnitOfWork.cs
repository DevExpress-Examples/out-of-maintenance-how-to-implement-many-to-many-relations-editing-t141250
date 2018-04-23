using System;
using System.Linq;
using System.Data;
using System.Linq.Expressions;
using System.Collections.Generic;
using Scaffolding.ManyToMany.Common.Utils;
using Scaffolding.ManyToMany.Common.DataModel;
using Scaffolding.ManyToMany.Common.DataModel.DesignTime;
using Scaffolding.ManyToMany.Common.DataModel.EntityFramework;
using Scaffolding.ManyToMany.Model;

namespace Scaffolding.ManyToMany.SchoolContextDataModel {
    /// <summary>
    /// A SchoolContextUnitOfWork instance that represents the run-time implementation of the ISchoolContextUnitOfWork interface.
    /// </summary>
    public class SchoolContextUnitOfWork : DbUnitOfWork<SchoolContext>, ISchoolContextUnitOfWork {

        public SchoolContextUnitOfWork(Func<SchoolContext> contextFactory)
            : base(contextFactory) {
        }

        IRepository<Course, int> ISchoolContextUnitOfWork.Courses {
            get { return GetRepository(x => x.Set<Course>(), (Course x) => x.CourseId); }
        }

        IRepository<Student, int> ISchoolContextUnitOfWork.Students {
            get { return GetRepository(x => x.Set<Student>(), (Student x) => x.StudentId); }
        }
    }
}
