using DevExpress.Mvvm.POCO;
using Scaffolding.ManyToMany.Common.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scaffolding.ManyToMany.Common.ViewModel {
    partial class SingleObjectViewModel<TEntity, TPrimaryKey, TUnitOfWork>
        where TEntity : class
        where TUnitOfWork : IUnitOfWork {

        protected AddRemoveDetailEntitiesViewModel<TEntity, TPrimaryKey, TDetailEntity, TDetailPrimaryKey, TUnitOfWork>
            CreateAddRemoveDetailEntitiesViewModel<TDetailEntity, TDetailPrimaryKey>(Func<TUnitOfWork, IRepository<TDetailEntity, TDetailPrimaryKey>> getDetailsRepositoryFunc, Func<TEntity, ICollection<TDetailEntity>> getDetailsFunc)
            where TDetailEntity : class {
            return AddRemoveDetailEntitiesViewModel<TEntity, TPrimaryKey, TDetailEntity, TDetailPrimaryKey, TUnitOfWork>.Create(UnitOfWorkFactory, this.getRepositoryFunc, getDetailsRepositoryFunc, getDetailsFunc, PrimaryKey);
        }

    }
}
