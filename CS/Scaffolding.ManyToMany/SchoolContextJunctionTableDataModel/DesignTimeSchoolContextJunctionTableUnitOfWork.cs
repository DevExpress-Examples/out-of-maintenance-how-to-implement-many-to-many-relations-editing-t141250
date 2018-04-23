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
    /// A SchoolContextJunctionTableDesignTimeUnitOfWork instance that represents the design-time implementation of the ISchoolContextJunctionTableUnitOfWork interface.
    /// </summary>
    public class SchoolContextJunctionTableDesignTimeUnitOfWork : DesignTimeUnitOfWork, ISchoolContextJunctionTableUnitOfWork {

        /// <summary>
        /// Initializes a new instance of the SchoolContextJunctionTableDesignTimeUnitOfWork class.
        /// </summary>
        public SchoolContextJunctionTableDesignTimeUnitOfWork() {
        }

        IRepository<Course, int> ISchoolContextJunctionTableUnitOfWork.Courses {
            get { return GetRepository((Course x) => x.CourseId); }
        }

        IRepository<CourseStudentJunction, Tuple<int, int>> ISchoolContextJunctionTableUnitOfWork.CourseStudentJunctions {
            get { return GetRepository((CourseStudentJunction x) => Tuple.Create(x.CourseId, x.StudentId)); }
        }

        IRepository<Student, int> ISchoolContextJunctionTableUnitOfWork.Students {
            get { return GetRepository((Student x) => x.StudentId); }
        }
    }
}
