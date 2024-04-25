using halalpizzabackend.Data;
using halalpizzabackend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.Features;
using Serilog;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting.Internal;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var configuration = builder.Configuration;
var connectionString = configuration.GetConnectionString("DefaultConnection");

// Replace with your server version and type.
var serverVersion = new MySqlServerVersion(new Version(8, 0, 34));

// Add DbContext
builder.Services.AddDbContext<ApplicationDbContext>(
    dbContextOptions => dbContextOptions
        .UseMySql(connectionString, serverVersion)
        .LogTo(Serilog.Log.Information)
        .EnableSensitiveDataLogging()
        .EnableDetailedErrors()
);

// Add session services
builder.Services.AddScoped<IUserDetailsService, UserDetailsService>();
builder.Services.AddScoped<ISliderSettingsService, SliderSettingsService>();
builder.Services.AddScoped<IProductsService, ProductsService>();
builder.Services.AddScoped<ICategoriesService, CategoriesService>();
builder.Services.AddScoped<IAddonsService, AddonsService>();


builder.Services.Configure<FormOptions>(options =>
{
    options.MultipartBodyLengthLimit = long.MaxValue; // or a specific limit that fits your requirements
}); ;
// Configure Serilog for logging
builder.Host.UseSerilog((hostingContext, loggerConfiguration) =>
{
    loggerConfiguration
        .WriteTo.Console()
        .WriteTo.File("log.txt", rollingInterval: RollingInterval.Day);
});

var app = builder.Build();

// Enable CORS with AllowOrigin set to all
app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
            Path.Combine(app.Environment.ContentRootPath, "images")),
    RequestPath = "/images"
});
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();





app.MapControllers();

app.Run();
