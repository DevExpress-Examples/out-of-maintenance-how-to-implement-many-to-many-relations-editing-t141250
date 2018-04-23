using System;
using System.Linq;
using System.Linq.Expressions;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using DevExpress.Mvvm;
using DevExpress.Mvvm.POCO;
using Scaffolding.ManyToMany.Common.Utils;
using Scaffolding.ManyToMany.SchoolContextDataModel;
using Scaffolding.ManyToMany.Common.DataModel;
using Scaffolding.ManyToMany.Model;
using Scaffolding.ManyToMany.Common.ViewModel;

namespace Scaffolding.ManyToMany.ViewModels {
    /// <summary>
    /// Represents the single Student object view model.
    /// </summary>
    public partial class StudentViewModel : SingleObjectViewModel<Student, int, ISchoolContextUnitOfWork> {

        /// <summary>
        /// Creates a new instance of StudentViewModel as a POCO view model.
        /// </summary>
        /// <param name="unitOfWorkFactory">A factory used to create a unit of work instance.</param>
        public static StudentViewModel Create(IUnitOfWorkFactory<ISchoolContextUnitOfWork> unitOfWorkFactory = null) {
            return ViewModelSource.Create(() => new StudentViewModel(unitOfWorkFactory));
        }

        /// <summary>
        /// Initializes a new instance of the StudentViewModel class.
        /// This constructor is declared protected to avoid undesired instantiation of the StudentViewModel type without the POCO proxy factory.
        /// </summary>
        /// <param name="unitOfWorkFactory">A factory used to create a unit of work instance.</param>
        protected StudentViewModel(IUnitOfWorkFactory<ISchoolContextUnitOfWork> unitOfWorkFactory = null)
            : base(unitOfWorkFactory ?? UnitOfWorkSource.GetUnitOfWorkFactory(), x => x.Students, x => x.LastName) {
        }
    }
}
