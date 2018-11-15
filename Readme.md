<!-- default file list -->
*Files to look at*:

* [AddRemoveDetailEntitiesViewModel.cs](./CS/Scaffolding.ManyToMany/Common/ViewModel/AddRemoveDetailEntitiesViewModel.cs) (VB: [AddRemoveDetailEntitiesViewModel.vb](./VB/Scaffolding.ManyToMany/Common/ViewModel/AddRemoveDetailEntitiesViewModel.vb))
* [SingleObjectViewModel.Extensions.cs](./CS/Scaffolding.ManyToMany/Common/ViewModel/SingleObjectViewModel.Extensions.cs) (VB: [SingleObjectViewModel.Extensions.vb](./VB/Scaffolding.ManyToMany/Common/ViewModel/SingleObjectViewModel.Extensions.vb))
* [CourseCollectionViewModel.cs](./CS/Scaffolding.ManyToMany/ViewModels/CourseCollectionViewModel.cs) (VB: [CourseCollectionViewModel.vb](./VB/Scaffolding.ManyToMany/ViewModels/CourseCollectionViewModel.vb))
* [CourseViewModel.Extensions.cs](./CS/Scaffolding.ManyToMany/ViewModels/CourseViewModel.Extensions.cs) (VB: [CourseViewModel.Extensions.vb](./VB/Scaffolding.ManyToMany/ViewModels/CourseViewModel.Extensions.vb))
* [StudentCollectionViewModel.cs](./CS/Scaffolding.ManyToMany/ViewModels/StudentCollectionViewModel.cs) (VB: [StudentCollectionViewModel.vb](./VB/Scaffolding.ManyToMany/ViewModels/StudentCollectionViewModel.vb))
* [StudentViewModel.Extensions.cs](./CS/Scaffolding.ManyToMany/ViewModels/StudentViewModel.Extensions.cs) (VB: [StudentViewModel.Extensions.vb](./VB/Scaffolding.ManyToMany/ViewModels/StudentViewModel.Extensions.vb))
* [CourseCollectionView.xaml](./CS/Scaffolding.ManyToMany/Views/CourseCollectionView.xaml) (VB: [CourseCollectionView.xaml](./VB/Scaffolding.ManyToMany/Views/CourseCollectionView.xaml))
* [DetailEntitiesView.xaml](./CS/Scaffolding.ManyToMany/Views/DetailEntitiesView.xaml) (VB: [DetailEntitiesView.xaml](./VB/Scaffolding.ManyToMany/Views/DetailEntitiesView.xaml))
* [StudentCollectionView.xaml](./CS/Scaffolding.ManyToMany/Views/StudentCollectionView.xaml) (VB: [StudentCollectionView.xaml](./VB/Scaffolding.ManyToMany/Views/StudentCollectionView.xaml))
<!-- default file list end -->
# How To: Implement Many-to-many Relations Editing


<p><strong>Starting with 15.2, Scaffolding Wizard supports generating many-to-many relations out of the box, so you don't need to implement it manually for a new application.</strong><br><br>This example shows how to implement many-to-many relations editing in an application generated with the DevExpress Scaffolding Wizard.</p>
<br>
<p>The resulting application supports adding multiple Course objects to the Student.CoursesAttending detail collection and vice versa.<br><br></p>
<p>You can encounter the following exception:</p>
<p><em>A first chance exception of type 'System.Data.SqlClient.SqlException' occurred in System.Data.dll</em></p>
<p><em>Additional information: A network-related or instance-specific error occurred while establishing a connection to SQL Server. The server was not found or was not accessible. Verify that the instance name is correct and that SQL Server is configured to allow remote connections. (provider: SQL Network Interfaces, error: 52 - Unable to locate a Local Database Runtime installation. Verify that SQL Server Express is properly installed and that the Local Database Runtime feature is enabled.)</em></p>
<p>This error is caused because the EntityFramework uses the missing LocalDB component.</p>
<p>To fix the issue, locate the <strong>App.config</strong> file within your application and open it. Change the defaultConnectionFactory in the following manner:<br><br></p>


```xaml
    <defaultConnectionFactory type="System.Data.Entity.Infrastructure.SqlConnectionFactory, EntityFramework" /> 
```



<br/>


