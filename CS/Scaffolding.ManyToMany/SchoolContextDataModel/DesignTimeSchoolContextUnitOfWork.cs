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
    /// A SchoolContextDesignTimeUnitOfWork instance that represents the design-time implementation of the ISchoolContextUnitOfWork interface.
    /// </summary>
    public class SchoolContextDesignTimeUnitOfWork : DesignTimeUnitOfWork, ISchoolContextUnitOfWork {

        /// <summary>
        /// Initializes a new instance of the SchoolContextDesignTimeUnitOfWork class.
        /// </summary>
        public SchoolContextDesignTimeUnitOfWork() {
        }

        IRepository<Course, int> ISchoolContextUnitOfWork.Courses {
            get { return GetRepository((Course x) => x.CourseId); }
        }

        IRepository<Student, int> ISchoolContextUnitOfWork.Students {
            get { return GetRepository((Student x) => x.StudentId); }
        }
    }
}
