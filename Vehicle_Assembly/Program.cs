using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Vehicle_Assembly.Services;
using Vehicle_Assembly.Services.VehicleService;
using Vehicle_Assembly.Services.WorkerService;
using Vehicle_Assembly.Services.AssmblyService;
using Vehicle_Assembly.Services.AssembleService;
using Vehicle_Assembly.Services.AdminService;
using Vehicle_Assembly.Utilities.EmailService;
using Vehicle_Assembly.Utilities.ValidationService.AssembleRequest;
using Vehicle_Assembly.Utilities.AccountUtility.AdminAccount;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Use Pomelo MySQL 
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

builder.Services.AddScoped<IVehicleService, VehicleService>();
builder.Services.AddScoped<IWorkerService, WorkerService>();
builder.Services.AddScoped<IAssembleService, AssembleService>();
builder.Services.AddScoped<IAdminService, AdminService>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<IAssembleRequestValidationService, AssembleRequestValidationService>();
builder.Services.AddScoped<IAdminAccountUtility, AdminAccountUtility>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder => builder
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAll"); 

app.UseStaticFiles();

//app.MapFallbackToFile("index.html");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
