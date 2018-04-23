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
