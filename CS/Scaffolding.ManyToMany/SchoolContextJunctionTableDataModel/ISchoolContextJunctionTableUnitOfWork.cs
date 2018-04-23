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
    /// ISchoolContextJunctionTableUnitOfWork extends the IUnitOfWork interface with repositories representing specific entities.
    /// </summary>
    public interface ISchoolContextJunctionTableUnitOfWork : IUnitOfWork {
        
        /// <summary>
        /// The Course entities repository.
        /// </summary>
		IRepository<Course, int> Courses { get; }
        
        /// <summary>
        /// The CourseStudentJunction entities repository.
        /// </summary>
		IRepository<CourseStudentJunction, Tuple<int, int>> CourseStudentJunctions { get; }
        
        /// <summary>
        /// The Student entities repository.
        /// </summary>
		IRepository<Student, int> Students { get; }
    }
}
