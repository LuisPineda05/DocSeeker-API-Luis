using DocSeeker.API.DocSeeker.Domain.Repositories;
using DocSeeker.API.DocSeeker.Domain.Services;
using DocSeeker.API.DocSeeker.Persistent.Repositories;
using DocSeeker.API.DocSeeker.Services;
using DocSeeker.API.Docseeker.Persistent.Repositories;
using DocSeeker.API.Docseeker.Services;
using DocSeeker.API.Security.Authorization.Handlers.Implementations;
using DocSeeker.API.Security.Authorization.Handlers.Interfaces;
using DocSeeker.API.Security.Authorization.Middleware;
using DocSeeker.API.Security.Authorization.Settings;
using DocSeeker.API.Security.Domain.Repositories;
using DocSeeker.API.Security.Domain.Services;
using DocSeeker.API.Security.Persistence.Repositories;
using DocSeeker.API.Security.Services;
using DocSeeker.API.Shared.Domain.Repositories;
using DocSeeker.API.Shared.Persistence.Contexts;
using DocSeeker.API.Shared.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add CORS

builder.Services.AddCors();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    // Add API Documentation Information

    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "MIRAI DocSeeker API",
        Description = "MIRAI DocSeeker RESTful API",
        TermsOfService = new Uri("https://mirai-docseeker.com/tos"),
        Contact = new OpenApiContact
        {
            Name = "MIRAI.studio",
            Url = new Uri("https://mirai.studio")
        },
        License = new OpenApiLicense
        {
            Name = "MIRAI DocSeeker Resources License",
            Url = new Uri("https://mirai-docseeker.com/license")
        }
    });
    options.EnableAnnotations();
});
// Add Database Connection

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<AppDbContext>(
    options => options.UseMySQL(connectionString)
        .LogTo(Console.WriteLine, LogLevel.Information)
        .EnableSensitiveDataLogging()
        .EnableDetailedErrors());

// Add lowercase routes

builder.Services.AddRouting(options => options.LowercaseUrls = true);

// Dependency Injection Configuration

// Shared Injection Configuration

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// AppSettings Configuration
builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));
// Learning Injection Configuration

builder.Services.AddScoped<IPatientRepository, PatientRepository>();
builder.Services.AddScoped<IPatientService, PatientService>();
builder.Services.AddScoped<IDoctorRepository, DoctorRepository>();
builder.Services.AddScoped<IDoctorService, DoctorService>();
builder.Services.AddScoped<INewRepository, NewRepository>();
builder.Services.AddScoped<INewService, NewService>();
builder.Services.AddScoped<IDateRepository, DateRepository>();
builder.Services.AddScoped<IDateService, DateService>();
builder.Services.AddScoped<IHourAvailableRepository, HourAvailableRepository>();
builder.Services.AddScoped<IHourAvailableService, HourAvailableService>();

// Security Injection Configuration

builder.Services.AddScoped<IJwtHandler, JwtHandler>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();

// AutoMapper Configuration

builder.Services.AddAutoMapper(
    typeof(DocSeeker.API.DocSeeker.Mapping.ModelToResourceProfile),
    typeof(DocSeeker.API.Security.Mapping.ModelToResourceProfile),
    typeof(DocSeeker.API.DocSeeker.Mapping.ResourceToModelProfile),
    typeof(DocSeeker.API.Security.Mapping.ResourceToModelProfile));


var app = builder.Build();

// Validation for ensuring Database Objects are created

using (var scope = app.Services.CreateScope())
using (var context = scope.ServiceProvider.GetService<AppDbContext>())
{
    context.Database.EnsureCreated();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("v1/swagger.json", "v1");
        options.RoutePrefix = "swagger";
    });

    // Configure CORS 

    app.UseCors(x => x
        .AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader());

    // Configure Error Handler Middleware

    app.UseMiddleware<ErrorHandlerMiddleware>();

    app.UseMiddleware<JwtMiddleware>();

    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}

public partial class Program {}