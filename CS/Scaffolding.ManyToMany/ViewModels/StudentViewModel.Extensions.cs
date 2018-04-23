using Scaffolding.ManyToMany.Common.ViewModel;
using Scaffolding.ManyToMany.Model;
using Scaffolding.ManyToMany.SchoolContextDataModel;
using System;
using System.Linq;

namespace Scaffolding.ManyToMany.ViewModels {
    partial class StudentViewModel {
        protected override void RefreshLookUpCollections(int key) {
            base.RefreshLookUpCollections(key);
            StudentCourses = CreateAddRemoveDetailEntitiesViewModel(x => x.Courses, x => x.CoursesAttending);
        }
        public virtual AddRemoveDetailEntitiesViewModel<Student, int, Course, int, ISchoolContextUnitOfWork> StudentCourses { get; protected set; }
    }
}
