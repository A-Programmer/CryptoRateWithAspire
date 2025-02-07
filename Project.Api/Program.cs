using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project.Api.Contracts;
using Project.Api.Data;
using Project.Api.Services;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.Services.AddControllers();

builder.Services.AddHttpContextAccessor();

builder.Services.AddTransient<IGenericHttpService, GenericHttpService>();

builder.Services.AddHttpClient("CoinMarketCap", client =>
{
    client.BaseAddress = new Uri(builder.Configuration["Clients:CoinMarketCap:BaseHttpsUrl"]);
    client.DefaultRequestHeaders.Add("X-CMC_PRO_API_KEY", "134248cb-4248-471e-82f7-91c765cc3d9c");
});

builder.Services.AddApiVersioning(options =>
{
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.ReportApiVersions = true;
});

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "People API",
        Version = "v1",
    });
});

builder.AddSqlServerDbContext<ApplicationDbContext>(connectionName: "sqlServer");

var app = builder.Build();

var scope = app.Services.CreateScope();
var serviceProvder = scope.ServiceProvider.GetRequiredService<IServiceScopeFactory>();
var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

await dbContext.Database.MigrateAsync();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseRouting();

app.MapControllers();

app.MapDefaultEndpoints();

app.Run();
