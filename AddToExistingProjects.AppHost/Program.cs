var builder = DistributedApplication.CreateBuilder(args);

var password = builder.AddParameter("password", "123");

var sql = builder.AddSqlServer("sqlServer", password, 1433)
    .WithLifetime(ContainerLifetime.Persistent)
    .WithDataVolume("SqlServerVolume");

var db = sql.AddDatabase("People");


var backend = builder.AddProject<Projects.Project_Api>("backend")
    .WithReference(db)
    .WaitFor(db);

var frontend = builder.AddProject<Projects.Project_MvcClient>("frontend")
    .WithReference(backend);

builder.Build().Run();
