using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using AplikasiInventarisToko.Modules;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();

ModulBarang.Initialize();
ModulTransaksi.Initialize();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseCors("AllowAll");
app.UseRouting();
app.UseAuthorization();
app.MapControllers();

app.Run("http://localhost:5001");using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using AplikasiInventarisToko.Modules;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();

ModulBarang.Initialize();
ModulTransaksi.Initialize();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseCors("AllowAll");
app.UseRouting();
app.UseAuthorization();
app.MapControllers();

app.Run("http://localhost:9000");