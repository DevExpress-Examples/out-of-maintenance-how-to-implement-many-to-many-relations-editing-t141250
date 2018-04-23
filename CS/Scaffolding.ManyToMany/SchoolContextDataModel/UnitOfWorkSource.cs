using System;
using System.Linq;
using System.Data;
using System.Data.Entity;
using System.Linq.Expressions;
using System.Collections.Generic;
using Scaffolding.ManyToMany.Common.Utils;
using Scaffolding.ManyToMany.Common.DataModel;
using Scaffolding.ManyToMany.Common.DataModel.EntityFramework;
using Scaffolding.ManyToMany.Model;
using DevExpress.Mvvm;

namespace Scaffolding.ManyToMany.SchoolContextDataModel {
    /// <summary>
    /// Provides methods to obtain the relevant IUnitOfWorkFactory.
    /// </summary>
    public static class UnitOfWorkSource {

        #region inner classes
        class DbUnitOfWorkFactory : IUnitOfWorkFactory<ISchoolContextUnitOfWork> {
            public static readonly IUnitOfWorkFactory<ISchoolContextUnitOfWork> Instance = new DbUnitOfWorkFactory();
            DbUnitOfWorkFactory() { }
            ISchoolContextUnitOfWork IUnitOfWorkFactory<ISchoolContextUnitOfWork>.CreateUnitOfWork() {
                return new SchoolContextUnitOfWork(() => new SchoolContext());
            }
        }

        class DesignUnitOfWorkFactory : IUnitOfWorkFactory<ISchoolContextUnitOfWork> {
            public static readonly IUnitOfWorkFactory<ISchoolContextUnitOfWork> Instance = new DesignUnitOfWorkFactory();
            DesignUnitOfWorkFactory() { }
            ISchoolContextUnitOfWork IUnitOfWorkFactory<ISchoolContextUnitOfWork>.CreateUnitOfWork() {
                return new SchoolContextDesignTimeUnitOfWork();
            }
        }
        #endregion

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
            return isInDesignTime ? DesignUnitOfWorkFactory.Instance : DbUnitOfWorkFactory.Instance;
        }
    }
}