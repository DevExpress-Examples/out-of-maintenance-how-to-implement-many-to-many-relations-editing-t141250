using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;
using DevExpress.Mvvm;
using DevExpress.Mvvm.POCO;
using Scaffolding.ManyToMany.Common.DataModel;
using Scaffolding.ManyToMany.Common.ViewModel;
using Scaffolding.ManyToMany.SchoolContextDataModel;
using Scaffolding.ManyToMany.Model;

namespace Scaffolding.ManyToMany.ViewModels {
    /// <summary>
    /// Represents the root POCO view model for the SchoolContext data model.
    /// </summary>
    public partial class SchoolContextViewModel : DocumentsViewModel<SchoolContextModuleDescription, ISchoolContextUnitOfWork> {

        const string TablesGroup = "Tables";

        const string ViewsGroup = "Views";

        /// <summary>
        /// Creates a new instance of SchoolContextViewModel as a POCO view model.
        /// </summary>
        public static SchoolContextViewModel Create() {
            return ViewModelSource.Create(() => new SchoolContextViewModel());
        }

        /// <summary>
        /// Initializes a new instance of the SchoolContextViewModel class.
        /// This constructor is declared protected to avoid undesired instantiation of the SchoolContextViewModel type without the POCO proxy factory.
        /// </summary>
        protected SchoolContextViewModel()
            : base(UnitOfWorkSource.GetUnitOfWorkFactory()) {
        }

        protected override SchoolContextModuleDescription[] CreateModules() {
            return new SchoolContextModuleDescription[] {
                new SchoolContextModuleDescription("Courses", "CourseCollectionView", TablesGroup, GetPeekCollectionViewModelFactory(x => x.Courses)),
                new SchoolContextModuleDescription("Students", "StudentCollectionView", TablesGroup, GetPeekCollectionViewModelFactory(x => x.Students)),
            };
        }



    }

    public partial class SchoolContextModuleDescription : ModuleDescription<SchoolContextModuleDescription> {
        public SchoolContextModuleDescription(string title, string documentType, string group, Func<SchoolContextModuleDescription, object> peekCollectionViewModelFactory = null)
            : base(title, documentType, group, peekCollectionViewModelFactory) {
        }
    }
}