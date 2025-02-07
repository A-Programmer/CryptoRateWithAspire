using Microsoft.Net.Http.Headers;
using Project.MvcClient.Contracts;
using Project.MvcClient.Services;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.Services.AddControllersWithViews();

builder.Services.AddHttpContextAccessor();

builder.Services.AddScoped<IGenericHttpService, GenericHttpService>();

builder.Services.AddHttpClient("Api", client =>
{

    client.BaseAddress = new Uri(builder.Configuration["Clients:Api:BaseHttpsUrl"]);
    client.DefaultRequestHeaders.Clear();
    client.DefaultRequestHeaders.Add(HeaderNames.Accept, "application/json");

});

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=People}/{action=Index}/{id?}")
    .WithStaticAssets();

app.MapDefaultEndpoints();

app.Run();
