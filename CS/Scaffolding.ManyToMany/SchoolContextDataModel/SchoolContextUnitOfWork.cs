using System;
using System.Linq;
using System.Data;
using System.Data.Entity;
using System.Linq.Expressions;
using System.Collections.Generic;
using Scaffolding.ManyToMany.Common.Utils;
using Scaffolding.ManyToMany.Common.DataModel;
using Scaffolding.ManyToMany.Common.DataModel.EntityFramework;
using Scaffolding.ManyToMany.Model;

namespace Scaffolding.ManyToMany.SchoolContextDataModel {
    /// <summary>
    /// A SchoolContextUnitOfWork instance that represents the run-time implementation of the ISchoolContextUnitOfWork interface.
    /// </summary>
    public class SchoolContextUnitOfWork : DbUnitOfWork<SchoolContext>, ISchoolContextUnitOfWork {

        /// <summary>
        /// Initializes a new instance of the SchoolContextUnitOfWork class.
        /// </summary>
        /// <param name="context">An underlying DbContext.</param>
        public SchoolContextUnitOfWork(SchoolContext context)
            : base(context) {
        }

        IRepository<Course, int> ISchoolContextUnitOfWork.Courses {
            get { return GetRepository(x => x.Set<Course>(), x => x.CourseId); }
        }

        IRepository<Student, int> ISchoolContextUnitOfWork.Students {
            get { return GetRepository(x => x.Set<Student>(), x => x.StudentId); }
        }
    }
}
