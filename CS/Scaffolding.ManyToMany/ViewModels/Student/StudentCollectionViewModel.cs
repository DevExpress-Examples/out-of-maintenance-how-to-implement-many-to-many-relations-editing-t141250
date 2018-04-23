using System;
using System.Linq;
using DevExpress.Mvvm.POCO;
using Scaffolding.ManyToMany.Common.Utils;
using Scaffolding.ManyToMany.SchoolContextDataModel;
using Scaffolding.ManyToMany.Common.DataModel;
using Scaffolding.ManyToMany.Model;
using Scaffolding.ManyToMany.Common.ViewModel;

namespace Scaffolding.ManyToMany.ViewModels {
    /// <summary>
    /// Represents the Students collection view model.
    /// </summary>
    public partial class StudentCollectionViewModel : CollectionViewModel<Student, int, ISchoolContextUnitOfWork> {

        /// <summary>
        /// Creates a new instance of StudentCollectionViewModel as a POCO view model.
        /// </summary>
        /// <param name="unitOfWorkFactory">A factory used to create a unit of work instance.</param>
        public static StudentCollectionViewModel Create(IUnitOfWorkFactory<ISchoolContextUnitOfWork> unitOfWorkFactory = null) {
            return ViewModelSource.Create(() => new StudentCollectionViewModel(unitOfWorkFactory));
        }

        /// <summary>
        /// Initializes a new instance of the StudentCollectionViewModel class.
        /// This constructor is declared protected to avoid undesired instantiation of the StudentCollectionViewModel type without the POCO proxy factory.
        /// </summary>
        /// <param name="unitOfWorkFactory">A factory used to create a unit of work instance.</param>
        protected StudentCollectionViewModel(IUnitOfWorkFactory<ISchoolContextUnitOfWork> unitOfWorkFactory = null)
            : base(unitOfWorkFactory ?? UnitOfWorkSource.GetUnitOfWorkFactory(), x => x.Students) {
        }
    }
}