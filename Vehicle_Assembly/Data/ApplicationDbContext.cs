using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Vehicle_Assembly.Attributes;
using Vehicle_Assembly.Models;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(){ }
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        try
        {
            Database.Migrate();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Database migration failed: {ex.Message}");
        }
    }

    public DbSet<VehicleModel> vehicle { get; set; }
    public DbSet<WorkerModel> worker { get; set; }
    public DbSet<AssembleModel> assembles { get; set; }
    public DbSet<AdminModel> admins { get; set; }
    public DbSet<LoginInfoModel> loginInfos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured) 
        {
            optionsBuilder.UseMySql(GlobalAttributes.mySQLConfig.connectionString, ServerVersion.AutoDetect(GlobalAttributes.mySQLConfig.connectionString));
        }
    }

}


