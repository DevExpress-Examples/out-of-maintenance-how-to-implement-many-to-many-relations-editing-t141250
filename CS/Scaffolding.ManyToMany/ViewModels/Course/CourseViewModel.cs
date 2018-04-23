using System;
using System.Linq;
using System.Linq.Expressions;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using DevExpress.Mvvm;
using DevExpress.Mvvm.POCO;
using DevExpress.Mvvm.DataModel;
using DevExpress.Mvvm.ViewModel;
using Scaffolding.ManyToMany.SchoolContextDataModel;
using Scaffolding.ManyToMany.Common;
using Scaffolding.ManyToMany.Model;

namespace Scaffolding.ManyToMany.ViewModels {

    /// <summary>
    /// Represents the single Course object view model.
    /// </summary>
    public partial class CourseViewModel : SingleObjectViewModel<Course, int, ISchoolContextUnitOfWork> {

        /// <summary>
        /// Creates a new instance of CourseViewModel as a POCO view model.
        /// </summary>
        /// <param name="unitOfWorkFactory">A factory used to create a unit of work instance.</param>
        public static CourseViewModel Create(IUnitOfWorkFactory<ISchoolContextUnitOfWork> unitOfWorkFactory = null) {
            return ViewModelSource.Create(() => new CourseViewModel(unitOfWorkFactory));
        }

        /// <summary>
        /// Initializes a new instance of the CourseViewModel class.
        /// This constructor is declared protected to avoid undesired instantiation of the CourseViewModel type without the POCO proxy factory.
        /// </summary>
        /// <param name="unitOfWorkFactory">A factory used to create a unit of work instance.</param>
        protected CourseViewModel(IUnitOfWorkFactory<ISchoolContextUnitOfWork> unitOfWorkFactory = null)
            : base(unitOfWorkFactory ?? UnitOfWorkSource.GetUnitOfWorkFactory(), x => x.Courses, x => x.Title) {
        }


        protected override void RefreshLookUpCollections(bool raisePropertyChanged) {
            base.RefreshLookUpCollections(raisePropertyChanged);
            StudentsDetailEntities = CreateAddRemoveDetailEntitiesViewModel(x => x.Students, x => x.Students);
        }

        public virtual AddRemoveDetailEntitiesViewModel<Course, Int32, Student, Int32, ISchoolContextUnitOfWork> StudentsDetailEntities { get; protected set; }
    }
}
