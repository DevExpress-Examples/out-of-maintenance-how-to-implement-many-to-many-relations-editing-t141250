Imports DevExpress.Mvvm
Imports DevExpress.Mvvm.POCO
Imports Scaffolding.ManyToMany.Common.DataModel
Imports System
Imports System.Collections.Generic
Imports System.Collections.ObjectModel
Imports System.ComponentModel.DataAnnotations
Imports System.Linq
Imports System.Text
Imports System.Threading.Tasks
Imports System.Windows

Namespace Scaffolding.ManyToMany.Common.ViewModel
    Public Class AddRemoveDetailEntitiesViewModel(Of TEntity As Class, TPrimaryKey, TDetailEntity As Class, TDetailPrimaryKey, TUnitOfWork As IUnitOfWork)
        Inherits SingleObjectViewModelBase(Of TEntity, TPrimaryKey, TUnitOfWork)

        Public Shared Function Create(ByVal unitOfWorkFactory As IUnitOfWorkFactory(Of TUnitOfWork), ByVal getRepositoryFunc As Func(Of TUnitOfWork, IRepository(Of TEntity, TPrimaryKey)), ByVal getDetailsRepositoryFunc As Func(Of TUnitOfWork, IRepository(Of TDetailEntity, TDetailPrimaryKey)), ByVal getDetailsFunc As Func(Of TEntity, ICollection(Of TDetailEntity)), ByVal id As TPrimaryKey) As AddRemoveDetailEntitiesViewModel(Of TEntity, TPrimaryKey, TDetailEntity, TDetailPrimaryKey, TUnitOfWork)
            Return ViewModelSource.Create(Function() New AddRemoveDetailEntitiesViewModel(Of TEntity, TPrimaryKey, TDetailEntity, TDetailPrimaryKey, TUnitOfWork)(unitOfWorkFactory, getRepositoryFunc, getDetailsRepositoryFunc, getDetailsFunc, id))
        End Function

        Private ReadOnly id As TPrimaryKey
        Private ReadOnly getDetailsRepositoryFunc As Func(Of TUnitOfWork, IRepository(Of TDetailEntity, TDetailPrimaryKey))
        Private ReadOnly getDetailsFunc As Func(Of TEntity, ICollection(Of TDetailEntity))

        <Required> _
        Protected Overridable ReadOnly Property DialogService() As IDialogService
            Get
                Return Nothing
            End Get
        End Property

        Private ReadOnly Property DetailsRepository() As IRepository(Of TDetailEntity, TDetailPrimaryKey)
            Get
                Return getDetailsRepositoryFunc(UnitOfWork)
            End Get
        End Property

        Public ReadOnly Property DetailEntities() As ICollection(Of TDetailEntity)
            Get
                Return If(Entity IsNot Nothing, getDetailsFunc(Entity), Enumerable.Empty(Of TDetailEntity)().ToArray())
            End Get
        End Property
        Private privateSelectedEntities As ObservableCollection(Of TDetailEntity)
        Public Property SelectedEntities() As ObservableCollection(Of TDetailEntity)
            Get
                Return privateSelectedEntities
            End Get
            Private Set(ByVal value As ObservableCollection(Of TDetailEntity))
                privateSelectedEntities = value
            End Set
        End Property

        Protected Sub New(ByVal unitOfWorkFactory As IUnitOfWorkFactory(Of TUnitOfWork), ByVal getRepositoryFunc As Func(Of TUnitOfWork, IRepository(Of TEntity, TPrimaryKey)), ByVal getDetailsRepositoryFunc As Func(Of TUnitOfWork, IRepository(Of TDetailEntity, TDetailPrimaryKey)), ByVal getDetailsFunc As Func(Of TEntity, ICollection(Of TDetailEntity)), ByVal id As TPrimaryKey)
            MyBase.New(unitOfWorkFactory, getRepositoryFunc, Nothing)
            Me.id = id
            Me.getDetailsRepositoryFunc = getDetailsRepositoryFunc
            Me.getDetailsFunc = getDetailsFunc
            SelectedEntities = New ObservableCollection(Of TDetailEntity)()
            ReloadDetails()
        End Sub

        Protected Overrides Sub OnInitializeInRuntime()
            MyBase.OnInitializeInRuntime()
            Messenger.Default.Register(Of EntityMessage(Of TEntity))(Me, AddressOf OnMessage)
        End Sub

        Public Sub AddDetailEntities()
            Dim availalbleEntities = DetailsRepository.GetEntities().ToList().Except(DetailEntities).ToArray()
            Dim selectEntitiesViewModel = New SelectDetailEntitiesViewModel(Of TDetailEntity)(availalbleEntities)
            If DialogService.ShowDialog(MessageBoxButton.OKCancel, "Select objects to add", selectEntitiesViewModel) = MessageBoxResult.OK AndAlso selectEntitiesViewModel.SelectedEntities.Any() Then

                For Each entity_Renamed In selectEntitiesViewModel.SelectedEntities
                    DetailEntities.Add(entity_Renamed)
                Next entity_Renamed
                SaveChangesAndNotify(selectEntitiesViewModel.SelectedEntities)
            End If
        End Sub

        Public Function CanAddDetailEntities() As Boolean
            Return Entity IsNot Nothing
        End Function

        Public Sub RemoveDetailEntities()
            If Not SelectedEntities.Any() Then
                Return
            End If

            For Each entity_Renamed In SelectedEntities
                DetailEntities.Remove(entity_Renamed)
            Next entity_Renamed
            SaveChangesAndNotify(SelectedEntities)
            SelectedEntities.Clear()
        End Sub

        Public Function CanRemoveDetailEntities() As Boolean
            Return Entity IsNot Nothing
        End Function

        Private Sub SaveChangesAndNotify(ByVal detailEntities As IEnumerable(Of TDetailEntity))
            Try
                UnitOfWork.SaveChanges()

                For Each entity_Renamed In detailEntities
                    Messenger.Default.Send(New EntityMessage(Of TDetailEntity)(entity_Renamed, EntityMessageType.Changed))
                Next entity_Renamed
                ReloadDetails()
            Catch e As DbException
                MessageBoxService.Show(e.ErrorMessage, e.ErrorCaption, MessageBoxButton.OK, MessageBoxImage.Error)
            End Try
        End Sub

        Private Sub OnMessage(ByVal message As EntityMessage(Of TEntity))
            If Entity IsNot Nothing AndAlso Object.Equals(GetPrimaryKey(Entity), GetPrimaryKey(message.Entity)) AndAlso message.MessageType = EntityMessageType.Changed Then
                ReloadDetails()
            End If
        End Sub

        Private Sub ReloadDetails()
            UnitOfWork = UnitOfWorkFactory.CreateUnitOfWork()
            OnParameterChanged(id)
            Me.RaisePropertyChanged(Function(x) x.DetailEntities)
        End Sub
    End Class
    Public Class SelectDetailEntitiesViewModel(Of TEntity As Class)
        Public Sub New(ByVal availableCourses() As TEntity)
            AvailableEntities = availableCourses
            SelectedEntities = New List(Of TEntity)()
        End Sub
        Private privateAvailableEntities As TEntity()
        Public Property AvailableEntities() As TEntity()
            Get
                Return privateAvailableEntities
            End Get
            Private Set(ByVal value As TEntity())
                privateAvailableEntities = value
            End Set
        End Property
        Private privateSelectedEntities As List(Of TEntity)
        Public Property SelectedEntities() As List(Of TEntity)
            Get
                Return privateSelectedEntities
            End Get
            Private Set(ByVal value As List(Of TEntity))
                privateSelectedEntities = value
            End Set
        End Property
    End Class
End Namespace
