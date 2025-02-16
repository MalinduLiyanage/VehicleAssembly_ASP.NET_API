using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Vehicle_Assembly.Models;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<Vehicle> vehicle { get; set; }
    public DbSet<Worker> worker { get; set; }
    public DbSet<Assemble> assembles { get; set; }

}


