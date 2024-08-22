using System;
using app.Models;
using Microsoft.EntityFrameworkCore;

public class ApiDataContext : DbContext
{
    public DbSet<User>? Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite(
            @"Data Source=databse.dat");
    }


}