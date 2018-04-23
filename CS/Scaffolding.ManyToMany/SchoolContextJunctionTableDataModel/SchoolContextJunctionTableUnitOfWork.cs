using System;
using System.Linq;
using System.Data;
using System.Linq.Expressions;
using System.Collections.Generic;
using Scaffolding.ManyToManyJunctionTable.Common.Utils;
using Scaffolding.ManyToManyJunctionTable.Common.DataModel;
using Scaffolding.ManyToManyJunctionTable.Common.DataModel.DesignTime;
using Scaffolding.ManyToManyJunctionTable.Common.DataModel.EntityFramework;
using Scaffolding.ManyToManyJunctionTable.Model;

namespace Scaffolding.ManyToManyJunctionTable.SchoolContextJunctionTableDataModel {
    /// <summary>
    /// A SchoolContextJunctionTableUnitOfWork instance that represents the run-time implementation of the ISchoolContextJunctionTableUnitOfWork interface.
    /// </summary>
    public class SchoolContextJunctionTableUnitOfWork : DbUnitOfWork<SchoolContextJunctionTable>, ISchoolContextJunctionTableUnitOfWork {

        public SchoolContextJunctionTableUnitOfWork(Func<SchoolContextJunctionTable> contextFactory)
            : base(contextFactory) {
        }

        IRepository<Course, int> ISchoolContextJunctionTableUnitOfWork.Courses {
            get { return GetRepository(x => x.Set<Course>(), (Course x) => x.CourseId); }
        }

        IRepository<CourseStudentJunction, Tuple<int, int>> ISchoolContextJunctionTableUnitOfWork.CourseStudentJunctions {
            get { return GetRepository(x => x.Set<CourseStudentJunction>(), (CourseStudentJunction x) => Tuple.Create(x.CourseId, x.StudentId)); }
        }

        IRepository<Student, int> ISchoolContextJunctionTableUnitOfWork.Students {
            get { return GetRepository(x => x.Set<Student>(), (Student x) => x.StudentId); }
        }
    }
}
