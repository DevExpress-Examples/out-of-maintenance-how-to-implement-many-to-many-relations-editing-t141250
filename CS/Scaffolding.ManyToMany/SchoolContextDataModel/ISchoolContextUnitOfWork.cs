using DevExpress.Mvvm.DataModel;
using Scaffolding.ManyToMany.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Scaffolding.ManyToMany.SchoolContextDataModel {

    /// <summary>
    /// ISchoolContextUnitOfWork extends the IUnitOfWork interface with repositories representing specific entities.
    /// </summary>
    public interface ISchoolContextUnitOfWork : IUnitOfWork {

        /// <summary>
        /// The Course entities repository.
        /// </summary>
        IRepository<Course, int> Courses { get; }

        /// <summary>
        /// The Student entities repository.
        /// </summary>
        IRepository<Student, int> Students { get; }
    }
}
