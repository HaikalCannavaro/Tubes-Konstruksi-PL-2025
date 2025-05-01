// Program.cs
using AplikasiInventarisToko.Managers;
using AplikasiInventarisToko.Models;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// Configure Swagger
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Aplikasi Inventaris Toko API",
        Version = "v1",
        Description = "API untuk manajemen inventaris toko",
        Contact = new OpenApiContact
        {
            Name = "Admin",
            Email = "admin@inventoris.com"
        }
    });
});

// Register services
builder.Services.AddSingleton<BarangManager<Barang>>();
builder.Services.AddSingleton<TransaksiManager>();

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Aplikasi Inventaris Toko API v1");
        c.RoutePrefix = string.Empty; // To serve the Swagger UI at the app's root
    });
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();