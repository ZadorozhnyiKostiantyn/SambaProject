using SambaProject.Data;

Syncfusion.Licensing
    .SyncfusionLicenseProvider
    .RegisterLicense("Mgo+DSMBPh8sVXJ0S0V+XE9AcVRDX3xKf0x/TGpQb19xflBPallYVBYiSV9jS3xSd0dkWH1bcXZRRGFeUw==");

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
        pattern: "{controller=FileManager}/{action=Index}/{id?}");

    app.DbInitialize();

    app.Run();
}


