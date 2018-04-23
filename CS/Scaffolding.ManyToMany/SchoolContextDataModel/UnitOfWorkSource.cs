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
using DevExpress.Mvvm;
using System.Collections;
using System.ComponentModel;
using DevExpress.Data.Linq;
using DevExpress.Data.Linq.Helpers;
using DevExpress.Data.Async.Helpers;

namespace Scaffolding.ManyToMany.SchoolContextDataModel {
    /// <summary>
    /// Provides methods to obtain the relevant IUnitOfWorkFactory.
    /// </summary>
    public static class UnitOfWorkSource {
        /// <summary>
        /// Returns the IUnitOfWorkFactory implementation based on the current mode (run-time or design-time).
        /// </summary>
        public static IUnitOfWorkFactory<ISchoolContextUnitOfWork> GetUnitOfWorkFactory() {
            return GetUnitOfWorkFactory(ViewModelBase.IsInDesignMode);
        }

        /// <summary>
        /// Returns the IUnitOfWorkFactory implementation based on the given mode (run-time or design-time).
        /// </summary>
        /// <param name="isInDesignTime">Used to determine which implementation of IUnitOfWorkFactory should be returned.</param>
        public static IUnitOfWorkFactory<ISchoolContextUnitOfWork> GetUnitOfWorkFactory(bool isInDesignTime) {
            if(isInDesignTime)
                return new DesignTimeUnitOfWorkFactory<ISchoolContextUnitOfWork>(() => new SchoolContextDesignTimeUnitOfWork());
            return new DbUnitOfWorkFactory<ISchoolContextUnitOfWork>(() => new SchoolContextUnitOfWork(() => new SchoolContext()));
        }
    }
}