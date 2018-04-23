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


