using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    // Define your DbSet properties for tables
    public DbSet<MyModel> MyModels { get; set; }
}

// Define the model class
public class MyModel
{
    public int Id { get; set; }
    public string Name { get; set; }
}
