using Etica.Alpayana.Application.Interfaces;
using Etica.Alpayana.Application.Services;
using Etica.Alpayana.Domain.Interfaces;
using Etica.Alpayana.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();

builder.Services.AddTransient<IDenunciaService, DenunciaService>();
builder.Services.AddTransient<IDenunciaRepository, DenunciaRepository>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

var app = builder.Build();



if (!app.Environment.IsDevelopment())
{
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Etica}/{action=Index}");
app.UseCors("AllowAll");
app.Run();
