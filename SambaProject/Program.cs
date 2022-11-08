using Microsoft.EntityFrameworkCore;
using SambaProject.Data;
using SambaProject.Service;
using Syncfusion.EJ2.Inputs;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services.AddControllersWithViews();

    builder.Services
        .AddData(builder.Configuration.GetConnectionString("MySqlDatabase"))
        .AddService();
}

var app = builder.Build();
{
    app.UseHttpsRedirection();
    app.UseStaticFiles();

    app.UseRouting();

    app.UseAuthorization();

    app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Authentication}/{action=Login}/{id?}");

    app.Run();
}

