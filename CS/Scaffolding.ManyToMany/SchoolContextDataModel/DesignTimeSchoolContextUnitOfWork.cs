using DevExpress.Mvvm.DataModel;
using DevExpress.Mvvm.DataModel.DesignTime;
using Scaffolding.ManyToMany.Model;
using System;
using System.Collections.Generic;
using System.Linq;

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

        IRepository<Course, int> ISchoolContextUnitOfWork.Courses
        {
            get { return GetRepository((Course x) => x.CourseId); }
        }

        IRepository<Student, int> ISchoolContextUnitOfWork.Students
        {
            get { return GetRepository((Student x) => x.StudentId); }
        }
    }
}
