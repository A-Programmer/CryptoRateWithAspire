var builder = DistributedApplication.CreateBuilder(args);

var password = builder.AddParameter("password", "0DvR3Sanew1V(xw!KBgJ*J");

var sql = builder.AddSqlServer("sql", password, 63847)
    .WithLifetime(ContainerLifetime.Persistent)
    .WithDataVolume("SqlServerVolume");

var db = sql.AddDatabase("People");


var backend = builder.AddProject<Projects.Project_Api>("backend")
    .WithReference(db)
    .WaitFor(db);

var frontend = builder.AddProject<Projects.Project_MvcClient>("frontend")
    .WithReference(backend);

builder.Build().Run();
