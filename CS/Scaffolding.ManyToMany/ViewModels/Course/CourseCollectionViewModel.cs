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
    /// Represents the Courses collection view model.
    /// </summary>
    public partial class CourseCollectionViewModel : CollectionViewModel<Course, int, ISchoolContextUnitOfWork> {

        /// <summary>
        /// Creates a new instance of CourseCollectionViewModel as a POCO view model.
        /// </summary>
        /// <param name="unitOfWorkFactory">A factory used to create a unit of work instance.</param>
        public static CourseCollectionViewModel Create(IUnitOfWorkFactory<ISchoolContextUnitOfWork> unitOfWorkFactory = null) {
            return ViewModelSource.Create(() => new CourseCollectionViewModel(unitOfWorkFactory));
        }

        /// <summary>
        /// Initializes a new instance of the CourseCollectionViewModel class.
        /// This constructor is declared protected to avoid undesired instantiation of the CourseCollectionViewModel type without the POCO proxy factory.
        /// </summary>
        /// <param name="unitOfWorkFactory">A factory used to create a unit of work instance.</param>
        protected CourseCollectionViewModel(IUnitOfWorkFactory<ISchoolContextUnitOfWork> unitOfWorkFactory = null)
            : base(unitOfWorkFactory ?? UnitOfWorkSource.GetUnitOfWorkFactory(), x => x.Courses) {
        }
    }
}