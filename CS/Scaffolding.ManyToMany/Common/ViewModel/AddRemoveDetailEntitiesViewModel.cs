using DevExpress.Mvvm;
using DevExpress.Mvvm.POCO;
using Scaffolding.ManyToMany.Common.DataModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Scaffolding.ManyToMany.Common.ViewModel {
    public class AddRemoveDetailEntitiesViewModel<TEntity, TPrimaryKey, TDetailEntity, TDetailPrimaryKey, TUnitOfWork> : SingleObjectViewModelBase<TEntity, TPrimaryKey, TUnitOfWork>
        where TEntity : class
        where TDetailEntity : class
        where TUnitOfWork : IUnitOfWork {

        public static AddRemoveDetailEntitiesViewModel<TEntity, TPrimaryKey, TDetailEntity, TDetailPrimaryKey, TUnitOfWork> Create(IUnitOfWorkFactory<TUnitOfWork> unitOfWorkFactory, Func<TUnitOfWork, IRepository<TEntity, TPrimaryKey>> getRepositoryFunc, Func<TUnitOfWork, IRepository<TDetailEntity, TDetailPrimaryKey>> getDetailsRepositoryFunc, Func<TEntity, ICollection<TDetailEntity>> getDetailsFunc, TPrimaryKey id) {
            return ViewModelSource.Create(() => new AddRemoveDetailEntitiesViewModel<TEntity, TPrimaryKey, TDetailEntity, TDetailPrimaryKey, TUnitOfWork>(unitOfWorkFactory, getRepositoryFunc, getDetailsRepositoryFunc, getDetailsFunc, id));
        }

        readonly TPrimaryKey id;
        readonly Func<TUnitOfWork, IRepository<TDetailEntity, TDetailPrimaryKey>> getDetailsRepositoryFunc;
        readonly Func<TEntity, ICollection<TDetailEntity>> getDetailsFunc;

        protected IDialogService DialogService { get { return this.GetRequiredService<IDialogService>(); } }

        IRepository<TDetailEntity, TDetailPrimaryKey> DetailsRepository { get { return getDetailsRepositoryFunc(UnitOfWork); } }

        public ICollection<TDetailEntity> DetailEntities { get { return Entity != null ? getDetailsFunc(Entity) : Enumerable.Empty<TDetailEntity>().ToArray(); } }
        public ObservableCollection<TDetailEntity> SelectedEntities { get; private set; }

        protected AddRemoveDetailEntitiesViewModel(IUnitOfWorkFactory<TUnitOfWork> unitOfWorkFactory, Func<TUnitOfWork, IRepository<TEntity, TPrimaryKey>> getRepositoryFunc, Func<TUnitOfWork, IRepository<TDetailEntity, TDetailPrimaryKey>> getDetailsRepositoryFunc, Func<TEntity, ICollection<TDetailEntity>> getDetailsFunc, TPrimaryKey id)
            : base(unitOfWorkFactory, getRepositoryFunc, null) {
            this.id = id;
            this.getDetailsRepositoryFunc = getDetailsRepositoryFunc;
            this.getDetailsFunc = getDetailsFunc;
            SelectedEntities = new ObservableCollection<TDetailEntity>();
            ReloadDetails();
        }

        protected override void OnInitializeInRuntime() {
            base.OnInitializeInRuntime();
            Messenger.Default.Register<EntityMessage<TEntity, TPrimaryKey>>(this, OnMessage);
        }

        public void AddDetailEntities() {
            var availalbleEntities = DetailsRepository.ToList().Except(DetailEntities).ToArray();
            var selectEntitiesViewModel = new SelectDetailEntitiesViewModel<TDetailEntity>(availalbleEntities);
            if(DialogService.ShowDialog(MessageButton.OKCancel, "Select objects to add", selectEntitiesViewModel) == MessageResult.OK && selectEntitiesViewModel.SelectedEntities.Any()) {
                foreach(var entity in selectEntitiesViewModel.SelectedEntities) {
                    DetailEntities.Add(entity);
                }
                SaveChangesAndNotify(selectEntitiesViewModel.SelectedEntities);
            }
        }

        public bool CanAddDetailEntities() {
            return Entity != null;
        }

        public void RemoveDetailEntities() {
            if(!SelectedEntities.Any())
                return;
            foreach(var entity in SelectedEntities) {
                DetailEntities.Remove(entity);
            }
            SaveChangesAndNotify(SelectedEntities);
            SelectedEntities.Clear();
        }

        public bool CanRemoveDetailEntities() {
            return Entity != null;
        }

        void SaveChangesAndNotify(IEnumerable<TDetailEntity> detailEntities) {
            try {
                UnitOfWork.SaveChanges();
                foreach(var entity in detailEntities) {
                    Messenger.Default.Send(new EntityMessage<TDetailEntity, TDetailPrimaryKey>(DetailsRepository.GetPrimaryKey(entity), EntityMessageType.Changed));
                }
                ReloadDetails();
            } catch (DbException e) {
                MessageBoxService.ShowMessage(e.ErrorMessage, e.ErrorCaption, MessageButton.OK, MessageIcon.Error);
            }
        }

        void OnMessage(EntityMessage<TEntity, TPrimaryKey> message) {
            if(message.MessageType == EntityMessageType.Changed && Entity != null && object.Equals(PrimaryKey, message.PrimaryKey)) {
                ReloadDetails();
            }
        }

        void ReloadDetails() {
            UnitOfWork = UnitOfWorkFactory.CreateUnitOfWork();
            OnParameterChanged(id);
            this.RaisePropertyChanged(x => x.DetailEntities);
        }
    }
    public class SelectDetailEntitiesViewModel<TEntity> where TEntity : class {
        public SelectDetailEntitiesViewModel(TEntity[] availableCourses) {
            AvailableEntities = availableCourses;
            SelectedEntities = new List<TEntity>();
        }
        public TEntity[] AvailableEntities { get; private set; }
        public List<TEntity> SelectedEntities { get; private set; }
    }
}
