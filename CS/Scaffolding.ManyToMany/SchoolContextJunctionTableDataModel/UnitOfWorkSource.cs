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
using DevExpress.Mvvm;
using System.Collections;
using System.ComponentModel;
using DevExpress.Data.Linq;
using DevExpress.Data.Linq.Helpers;
using DevExpress.Data.Async.Helpers;

namespace Scaffolding.ManyToManyJunctionTable.SchoolContextJunctionTableDataModel {
    /// <summary>
    /// Provides methods to obtain the relevant IUnitOfWorkFactory.
    /// </summary>
    public static class UnitOfWorkSource {
		/// <summary>
        /// Returns the IUnitOfWorkFactory implementation based on the current mode (run-time or design-time).
        /// </summary>
        public static IUnitOfWorkFactory<ISchoolContextJunctionTableUnitOfWork> GetUnitOfWorkFactory() {
            return GetUnitOfWorkFactory(ViewModelBase.IsInDesignMode);
        }

		/// <summary>
        /// Returns the IUnitOfWorkFactory implementation based on the given mode (run-time or design-time).
        /// </summary>
        /// <param name="isInDesignTime">Used to determine which implementation of IUnitOfWorkFactory should be returned.</param>
        public static IUnitOfWorkFactory<ISchoolContextJunctionTableUnitOfWork> GetUnitOfWorkFactory(bool isInDesignTime) {
			if(isInDesignTime)
                return new DesignTimeUnitOfWorkFactory<ISchoolContextJunctionTableUnitOfWork>(() => new SchoolContextJunctionTableDesignTimeUnitOfWork());
            return new DbUnitOfWorkFactory<ISchoolContextJunctionTableUnitOfWork>(() => new SchoolContextJunctionTableUnitOfWork(() => new SchoolContextJunctionTable()));
        }
    }
}