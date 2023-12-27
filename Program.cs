using project_task_netangular.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();

//Conexion postgres
builder.Services.AddNpgsql<DbTareasContext>(builder.Configuration.GetConnectionString("ConnectionPostgres"));
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

// Agrega el siguiente código en la configuración del servicio en tu Startup.cs

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowOrigin",
        builder =>
        {
            builder.WithOrigins("https://localhost:44401")
                   .AllowAnyHeader()
                   .AllowAnyMethod();
        });
});

// Luego, en el middleware, antes de app.UseEndpoints(), agrega:



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseCors("AllowOrigin");
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html");

app.Run();
