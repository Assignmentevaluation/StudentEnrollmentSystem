using Microsoft.EntityFrameworkCore;
using StudentEnrollment.Core.DTOs;
using StudentEnrollment.Core.Entities;
using StudentEnrollment.Core.Interfaces.Repositories;
using StudentEnrollment.Core.Interfaces.Services;
using StudentEnrollment.Infrastructure.Data;
using StudentEnrollment.Infrastructure.Repositories;
using StudentEnrollment.Infrastructure.Services;
using System.Numerics;

var builder = WebApplication.CreateBuilder(args);

// DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")));

// Repositories 
builder.Services.AddScoped<IStudentRepository, StudentRepository>();
builder.Services.AddScoped<ICourseRepository, CourseRepository>();
builder.Services.AddScoped<IEnrollmentRepository, EnrollmentRepository>();

// Services 
builder.Services.AddScoped<IStudentService, StudentService>();
builder.Services.AddScoped<ICourseService, CourseService>();
builder.Services.AddScoped<IEnrollmentService, EnrollmentService>();

// CORS
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});



// API 
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
app.UseCors();
app.UseStaticFiles();
app.UseAuthorization();
app.MapControllers();

app.Run();


