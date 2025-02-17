using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Vehicle_Assembly.Models;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<VehicleModel> vehicle { get; set; }
    public DbSet<WorkerModel> worker { get; set; }
    public DbSet<AssembleModel> assembles { get; set; }

}


