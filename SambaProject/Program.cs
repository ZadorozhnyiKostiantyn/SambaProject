using Microsoft.EntityFrameworkCore;
using SambaProject.Data;
using SambaProject.Service;
using Syncfusion.EJ2.Inputs;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services.AddSession();
    builder.Services.AddControllersWithViews();

    builder.Services
        .AddData(builder.Configuration)
        .AddService(builder.Configuration);
}

var app = builder.Build();
{
    app.UseSession();
    app.Use(async (context, next) =>
    {
        var token = context.Session.GetString("Token");
        if (!string.IsNullOrEmpty(token))
        {
            context.Request.Headers.Add("Authorization", "Bearer " + token);
        }
        await next();
    });

    app.UseHttpsRedirection();
    app.UseStaticFiles();

    app.UseRouting();

    app.UseAuthentication();
    app.UseAuthorization();

    app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Authentication}/{action=Login}/{id?}");

    app.Run();
}
