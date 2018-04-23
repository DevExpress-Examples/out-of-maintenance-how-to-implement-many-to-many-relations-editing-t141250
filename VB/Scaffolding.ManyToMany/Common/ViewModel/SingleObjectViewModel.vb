﻿Imports System
Imports System.ComponentModel
Imports System.ComponentModel.DataAnnotations
Imports System.Collections.Generic
Imports System.Linq
Imports System.Linq.Expressions
Imports DevExpress.Mvvm
Imports DevExpress.Mvvm.POCO
Imports DevExpress.Mvvm.DataAnnotations
Imports Scaffolding.ManyToMany.Common.Utils
Imports Scaffolding.ManyToMany.Common.DataModel
Imports MessageBoxButton = System.Windows.MessageBoxButton
Imports MessageBoxImage = System.Windows.MessageBoxImage
Imports MessageBoxResult = System.Windows.MessageBoxResult

Namespace Scaffolding.ManyToMany.Common.ViewModel
    ''' <summary>
    ''' The base class for POCO view models exposing a single entity of a given type and CRUD operations against this entity. 
    ''' This is a partial class that provides the extension point to add custom properties, commands and override methods without modifying the auto-generated code.
    ''' </summary>
    ''' <typeparam name="TEntity">An entity type.</typeparam>
    ''' <typeparam name="TPrimaryKey">A primary key value type.</typeparam>
    ''' <typeparam name="TUnitOfWork">A unit of work type.</typeparam>
    Public MustInherit Partial Class SingleObjectViewModel(Of TEntity As Class, TPrimaryKey, TUnitOfWork As IUnitOfWork)
        Inherits SingleObjectViewModelBase(Of TEntity, TPrimaryKey, TUnitOfWork)

        ''' <summary>
        ''' Initializes a new instance of the SingleObjectViewModel class.
        ''' </summary>
        ''' <param name="unitOfWorkFactory">A factory used to create the unit of work instance.</param>
        ''' <param name="getRepositoryFunc">A function that returns the repository representing entities of a given type.</param>
        ''' <param name="getEntityDisplayNameFunc">An optional parameter that provides a function to obtain the display text for a given entity. If ommited, the primary key value is used as a display text.</param>
        Protected Sub New(ByVal unitOfWorkFactory As IUnitOfWorkFactory(Of TUnitOfWork), ByVal getRepositoryFunc As Func(Of TUnitOfWork, IRepository(Of TEntity, TPrimaryKey)), Optional ByVal getEntityDisplayNameFunc As Func(Of TEntity, Object) = Nothing)
            MyBase.New(unitOfWorkFactory, getRepositoryFunc, getEntityDisplayNameFunc)
        End Sub
    End Class

    ''' <summary>
    ''' The base class for POCO view models exposing a single entity of a given type and CRUD operations against this entity. 
    ''' It is not recommended to inherit directly from this class. Use the SingleObjectViewModel class instead.
    ''' </summary>
    ''' <typeparam name="TEntity">An entity type.</typeparam>
    ''' <typeparam name="TPrimaryKey">A primary key value type.</typeparam>
    ''' <typeparam name="TUnitOfWork">A unit of work type.</typeparam>
    <POCOViewModel> _
    Public MustInherit Class SingleObjectViewModelBase(Of TEntity As Class, TPrimaryKey, TUnitOfWork As IUnitOfWork)
        Implements ISingleObjectViewModel(Of TEntity, TPrimaryKey), ISupportParameter, IDocumentContent


        Private title_Renamed As Object
        Protected ReadOnly getRepositoryFunc As Func(Of TUnitOfWork, IRepository(Of TEntity, TPrimaryKey))
        Protected ReadOnly getEntityDisplayNameFunc As Func(Of TEntity, Object)

        ''' <summary>
        ''' Initializes a new instance of the SingleObjectViewModelBase class.
        ''' </summary>
        ''' <param name="unitOfWorkFactory">A factory used to create the unit of work instance.</param>
        ''' <param name="getRepositoryFunc">A function that returns repository representing entities of a given type.</param>
        ''' <param name="getEntityDisplayNameFunc">An optional parameter that provides a function to obtain the display text for a given entity. If ommited, the primary key value is used as a display text.</param>
        Protected Sub New(ByVal unitOfWorkFactory As IUnitOfWorkFactory(Of TUnitOfWork), ByVal getRepositoryFunc As Func(Of TUnitOfWork, IRepository(Of TEntity, TPrimaryKey)), ByVal getEntityDisplayNameFunc As Func(Of TEntity, Object))
            Me.UnitOfWorkFactory = unitOfWorkFactory
            Me.getRepositoryFunc = getRepositoryFunc
            Me.getEntityDisplayNameFunc = getEntityDisplayNameFunc
            UnitOfWork = Me.UnitOfWorkFactory.CreateUnitOfWork()
            If Me.IsInDesignMode() Then
                Me.Entity = Me.Repository.Local.FirstOrDefault()
            Else
                OnInitializeInRuntime()
            End If
        End Sub

        Private privateUnitOfWorkFactory As IUnitOfWorkFactory(Of TUnitOfWork)
        Protected Property UnitOfWorkFactory() As IUnitOfWorkFactory(Of TUnitOfWork)
            Get
                Return privateUnitOfWorkFactory
            End Get
            Private Set(ByVal value As IUnitOfWorkFactory(Of TUnitOfWork))
                privateUnitOfWorkFactory = value
            End Set
        End Property

        Protected Property UnitOfWork() As TUnitOfWork

        ''' <summary>
        ''' The display text for a given entity used as a title in the corresponding view.
        ''' </summary>
        ''' <returns></returns>
        Public ReadOnly Property Title() As Object
            Get
                Return title_Renamed
            End Get
        End Property

        ''' <summary>
        ''' An entity represented by this view model.
        ''' Since SingleObjectViewModelBase is a POCO view model, this property will raise INotifyPropertyChanged.PropertyEvent when modified so it can be used as a binding source in views.
        ''' </summary>
        ''' <returns></returns>
        Public Overridable Property Entity() As TEntity

        ''' <summary>
        ''' Updates the Title property value and raises CanExecute changed for relevant commands.
        ''' Since SingleObjectViewModelBase is a POCO view model, an instance of this class will also expose the UpdateCommand property that can be used as a binding source in views.
        ''' </summary>
        <Display(AutoGenerateField := False)> _
        Public Sub Update()
            UpdateTitle()
            UpdateCommands()
        End Sub

        ''' <summary>
        ''' Saves changes in the underlying unit of work.
        ''' Since SingleObjectViewModelBase is a POCO view model, an instance of this class will also expose the SaveCommand property that can be used as a binding source in views.
        ''' </summary>
        <Command> _
        Public Overridable Function Save() As Boolean
            Try
                Dim isNewEntity As Boolean = IsNew()
                If Not isNewEntity Then
                    UnitOfWork.Update(Entity)
                End If
                UnitOfWork.SaveChanges()
                If isNewEntity Then
                    Messenger.Default.Send(New EntityMessage(Of TEntity)(Entity, EntityMessageType.Added))
                Else
                    Messenger.Default.Send(New EntityMessage(Of TEntity)(Entity, EntityMessageType.Changed))
                End If
                Reload()
                OnAfterEntitySaved(Entity)
                Return True
            Catch e As DbException
                MessageBoxService.Show(e.ErrorMessage, e.ErrorCaption, MessageBoxButton.OK, MessageBoxImage.Error)
                Return False
            End Try
        End Function

        ''' <summary>
        ''' Determines whether entity local changes can be saved.
        ''' Since SingleObjectViewModelBase is a POCO view model, this method will be used as a CanExecute callback for SaveCommand.
        ''' </summary>
        Public Overridable Function CanSave() As Boolean
            Return Entity IsNot Nothing AndAlso Not HasValidationErrors()
        End Function

        ''' <summary>
        ''' Deletes the entity, save changes and closes the corresponding view if confirmed by a user.
        ''' Since SingleObjectViewModelBase is a POCO view model, an instance of this class will also expose the DeleteCommand property that can be used as a binding source in views.
        ''' </summary>
        Public Overridable Sub Delete()
            If MessageBoxService.Show(String.Format(CommonResources.Confirmation_Delete, GetType(TEntity).Name), CommonResources.Confirmation_Caption, MessageBoxButton.YesNo) <> MessageBoxResult.Yes Then
                Return
            End If
            Try
                OnBeforeEntityDeleted(Entity)
                Repository.Remove(Entity)
                UnitOfWork.SaveChanges()
                Dim toMessage As TEntity = Entity
                Entity = Nothing
                Messenger.Default.Send(New EntityMessage(Of TEntity)(toMessage, EntityMessageType.Deleted))
                Close()
            Catch e As DbException
                MessageBoxService.Show(e.ErrorMessage, e.ErrorCaption, MessageBoxButton.OK, MessageBoxImage.Error)
            End Try
        End Sub

        ''' <summary>
        ''' Determines whether the entity can be deleted.
        ''' Since SingleObjectViewModelBase is a POCO view model, this method will be used as a CanExecute callback for DeleteCommand.
        ''' </summary>
        Public Overridable Function CanDelete() As Boolean
            Return Entity IsNot Nothing AndAlso Not IsNew()
        End Function

        ''' <summary>
        ''' Closes the corresponding view.
        ''' </summary>
        Public Sub Close()
            If Not TryClose() Then
                Return
            End If
            If DocumentOwner IsNot Nothing Then
                DocumentOwner.Close(Me)
            End If
        End Sub

        Protected Overridable Sub OnInitializeInRuntime()
            Messenger.Default.Register(Of EntityMessage(Of TEntity))(Me, Sub(x) OnEntityMessage(x))
        End Sub

        Protected Overridable Sub OnEntityMessage(ByVal x As EntityMessage(Of TEntity))
            If Entity Is Nothing Then
                Return
            End If
            If x.MessageType = EntityMessageType.Deleted AndAlso Object.Equals(GetPrimaryKey(x.Entity), GetPrimaryKey(Entity)) Then
                Close()
            End If
        End Sub

        Protected Overridable Sub OnEntityChanged()
            Update()
            If Me.Entity IsNot Nothing AndAlso Repository.HasPrimaryKey(Entity) Then
                RefreshLookUpCollections(GetPrimaryKey(Me.Entity))
            End If
        End Sub

        Protected Overridable Sub RefreshLookUpCollections(ByVal key As TPrimaryKey)
        End Sub

        Protected ReadOnly Property Repository() As IRepository(Of TEntity, TPrimaryKey)
            Get
                Return getRepositoryFunc(UnitOfWork)
            End Get
        End Property

        <Required> _
        Protected Overridable ReadOnly Property MessageBoxService() As IMessageBoxService
            Get
                Return Nothing
            End Get
        End Property

        Protected Overridable Sub OnParameterChanged(ByVal parameter As Object)
            If Entity IsNot Nothing AndAlso IsNew() Then
                Remove(Entity)
            End If
            Dim entityInitializer As IEntityInitializer(Of TEntity, TUnitOfWork) = TryCast(parameter, IEntityInitializer(Of TEntity, TUnitOfWork))
            If entityInitializer IsNot Nothing Then
                Entity = CreateEntity()
                InitializeEntity(entityInitializer)
            ElseIf TypeOf parameter Is TPrimaryKey Then
                Me.parameter_Renamed = parameter
                Entity = Repository.Find(DirectCast(parameter, TPrimaryKey))
            ElseIf parameter Is Nothing Then
                Entity = CreateEntity()
            Else
                Entity = Nothing
            End If
        End Sub

        Protected Overridable Function CreateEntity() As TEntity
            Return Repository.Create()
        End Function

        Private Sub UpdateTitle()
            If Entity Is Nothing Then
                title_Renamed = Nothing
            ElseIf IsNew() Then
                title_Renamed = GetTitleForNewEntity()
            Else
                title_Renamed = GetTitle(GetState() = EntityState.Modified)
            End If
            Me.RaisePropertyChanged(Function(x) x.Title)
        End Sub

        Protected Overridable Sub UpdateCommands()
            Me.RaiseCanExecuteChanged(Sub(x) x.Save())
            Me.RaiseCanExecuteChanged(Sub(x) x.Delete())
        End Sub

        Protected Overridable Sub OnAfterEntitySaved(ByVal entity As TEntity)
        End Sub

        Protected Sub Reload()
            OnParameterChanged(GetPrimaryKey(Entity))
            OnEntityChanged()
            Me.RaisePropertyChanged(Function(x) x.Entity)
        End Sub

        Protected Overridable Sub OnBeforeEntityDeleted(ByVal entity As TEntity)
        End Sub

        Private privateDocumentOwner As IDocumentOwner
        Protected Property DocumentOwner() As IDocumentOwner
            Get
                Return privateDocumentOwner
            End Get
            Private Set(ByVal value As IDocumentOwner)
                privateDocumentOwner = value
            End Set
        End Property

        Protected Overridable Sub OnDestroy()
            Messenger.Default.Unregister(Me)
        End Sub

        Protected Overridable Function TryClose() As Boolean
            If HasValidationErrors() Then
                Dim warningResult As MessageBoxResult = MessageBoxService.Show(CommonResources.Warning_SomeFieldsContainInvalidData, CommonResources.Warning_Caption, MessageBoxButton.OKCancel)
                Return warningResult = MessageBoxResult.OK
            End If
            If Not NeedSave() Then
                Return True
            End If
            Dim result As MessageBoxResult = MessageBoxService.Show(CommonResources.Confirmation_Save, CommonResources.Confirmation_Caption, MessageBoxButton.YesNoCancel)
            If result = MessageBoxResult.Yes Then
                Return Save()
            End If
            Return result <> MessageBoxResult.Cancel
        End Function

        Protected Function IsNew() As Boolean
            Return GetState() = EntityState.Added
        End Function

        Protected Overridable Function NeedSave() As Boolean
            If Entity Is Nothing Then
                Return False
            End If
            Dim state As EntityState = GetState()
            Return state = EntityState.Modified OrElse state = EntityState.Added
        End Function

        Protected Overridable Function HasValidationErrors() As Boolean
            Return False
        End Function

        Private Function GetTitle(ByVal entityModified As Boolean) As String
            If entityModified Then
                Return GetTitle() & CommonResources.Entity_Changed
            Else
                Return GetTitle()
            End If
        End Function

        Protected Overridable Function GetTitleForNewEntity() As String
            Return GetType(TEntity).Name & CommonResources.Entity_New
        End Function

        Protected Overridable Function GetTitle() As String
            Return GetType(TEntity).Name & " - " & Convert.ToString(If(getEntityDisplayNameFunc IsNot Nothing, getEntityDisplayNameFunc(Entity), GetPrimaryKey(Entity)))
        End Function

        Private Sub InitializeEntity(ByVal entityInitializer As IEntityInitializer(Of TEntity, TUnitOfWork))
            entityInitializer.InitializeEntity(UnitOfWork, Entity)
            Me.RaisePropertyChanged(Function(x) x.Entity)
        End Sub

        Protected Function GetState() As EntityState
            Try
                Return UnitOfWork.GetState(Entity)
            Catch e1 As InvalidOperationException
                If TypeOf Parameter Is TPrimaryKey Then
                    Repository.SetPrimaryKey(Entity, DirectCast(Parameter, TPrimaryKey))
                End If
                Return UnitOfWork.GetState(Entity)
            End Try
        End Function

        Private Sub Remove(ByVal entity As TEntity)
            If Repository.HasPrimaryKey(entity) Then
                entity = Repository.Find(GetPrimaryKey(entity))
            End If
            Repository.Remove(Me.Entity)
        End Sub

        Protected Overridable Function GetPrimaryKey(ByVal entity As TEntity) As TPrimaryKey
            Return Me.Repository.GetPrimaryKey(entity)
        End Function

        Protected Sub DestroyDocument(ByVal document As IDocument)
            If document IsNot Nothing Then
                document.Close()
            End If
        End Sub

        Protected Overridable Function CreateLookUpCollectionViewModel(Of TDetailEntity As Class, TDetailPrimaryKey, TForeignKey)(ByVal getRepositoryFunc As Func(Of TUnitOfWork, IRepository(Of TDetailEntity, TDetailPrimaryKey)), ByVal foreignKeyExpression As Expression(Of Func(Of TDetailEntity, TForeignKey)), ByVal setMasterEntityAction As Action(Of TDetailEntity, TEntity), ByVal masterEntityKey As TForeignKey) As CollectionViewModel(Of TDetailEntity, TDetailPrimaryKey, TUnitOfWork)
            Dim lookUpViewModel = ViewModelSource.Create(Function() New CollectionViewModel(Of TDetailEntity, TDetailPrimaryKey, TUnitOfWork)(UnitOfWorkFactory, getRepositoryFunc, Function() CreateNavigationPropertyInitializer(setMasterEntityAction, masterEntityKey)))
            lookUpViewModel.SetParentViewModel(Me)
            lookUpViewModel.FilterExpression = ExpressionHelper.GetValueEqualsExpression(foreignKeyExpression, masterEntityKey)
            Return lookUpViewModel
        End Function

        Protected Overridable Function CreateReadOnlyLookUpCollectionViewModel(Of TDetailEntity As Class, TForeignKey)(ByVal getRepositoryFunc As Func(Of TUnitOfWork, IReadOnlyRepository(Of TDetailEntity)), ByVal foreignKeyExpression As Expression(Of Func(Of TDetailEntity, TForeignKey)), ByVal masterEntityKey As TForeignKey) As ReadOnlyCollectionViewModel(Of TDetailEntity, TUnitOfWork)
            Dim lookUpViewModel = ViewModelSource.Create(Function() New ReadOnlyCollectionViewModel(Of TDetailEntity, TUnitOfWork)(UnitOfWorkFactory, getRepositoryFunc))
            lookUpViewModel.SetParentViewModel(Me)
            lookUpViewModel.FilterExpression = ExpressionHelper.GetValueEqualsExpression(foreignKeyExpression, masterEntityKey)
            Return lookUpViewModel
        End Function

        Private Function CreateNavigationPropertyInitializer(Of TDetailEntity As Class, TForeignKey)(ByVal setMasterEntityAction As Action(Of TDetailEntity, TEntity), ByVal masterEntityKey As TForeignKey) As IEntityInitializer(Of TDetailEntity, TUnitOfWork)
            Return New EntityNavigationPropertyInitializer(Of TDetailEntity, TEntity, TPrimaryKey, TUnitOfWork)(DirectCast(DirectCast(masterEntityKey, Object), TPrimaryKey), setMasterEntityAction, Me.getRepositoryFunc)
        End Function

        Protected Overridable Function GetLookUpEntities(Of TLookUpEntity As Class)(ByVal getRepositoryFunc As Func(Of TUnitOfWork, IReadOnlyRepository(Of TLookUpEntity))) As IList(Of TLookUpEntity)

            Dim repository_Renamed = getRepositoryFunc(UnitOfWork)
            repository_Renamed.GetEntities().Load()
            Return repository_Renamed.Local
        End Function

        #Region "ISupportParameter"
        Private Shared ReadOnly NotSetParameter As New Object()

        Private parameter_Renamed As Object = NotSetParameter

        Private Property Parameter() As Object
            Get
                Return If(Object.Equals(parameter_Renamed, NotSetParameter), Nothing, parameter_Renamed)
            End Get
            Set(ByVal value As Object)
                If Object.Equals(parameter_Renamed, value) Then
                    Return
                End If
                OnParameterChanged(value)
            End Set
        End Property

        Private Property ISupportParameter_Parameter() As Object Implements ISupportParameter.Parameter
            Get
                Return Parameter
            End Get
            Set(ByVal value As Object)
                Parameter = value
            End Set
        End Property
        #End Region

        #Region "IDocumentContent"
        Private ReadOnly Property IDocumentContent_Title() As Object Implements IDocumentContent.Title
            Get
                Return Title
            End Get
        End Property

        Private Sub IDocumentContent_OnClose(ByVal e As CancelEventArgs) Implements IDocumentContent.OnClose
            e.Cancel = Not TryClose()
        End Sub

        Private Sub IDocumentContent_OnDestroy() Implements IDocumentContent.OnDestroy
            OnDestroy()
        End Sub

        Private Property IDocumentContent_DocumentOwner() As IDocumentOwner Implements IDocumentContent.DocumentOwner
            Get
                Return DocumentOwner
            End Get
            Set(ByVal value As IDocumentOwner)
                DocumentOwner = value
            End Set
        End Property
        #End Region

        #Region "ISingleObjectViewModel"
        Private ReadOnly Property ISingleObjectViewModelGeneric_Entity() As TEntity Implements ISingleObjectViewModel(Of TEntity, TPrimaryKey).Entity
            Get
                Return Entity
            End Get
        End Property

        Private ReadOnly Property ISingleObjectViewModelGeneric_PrimaryKey() As TPrimaryKey Implements ISingleObjectViewModel(Of TEntity, TPrimaryKey).PrimaryKey
            Get
                Return GetPrimaryKey(Entity)
            End Get
        End Property
        #End Region
    End Class
End Namespace