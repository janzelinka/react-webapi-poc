using System;
using api.Models.Events;
using app.Models;
using Microsoft.EntityFrameworkCore;

public class ApiDataContext : DbContext
{
    public DbSet<User>? Users { get; set; }
    public DbSet<City>? Cities { get; set; }
    public DbSet<Address>? Addresses { get; set; }
    public DbSet<Event>? Events { get; set; }
    public DbSet<Order>? Orders { get; set; }
    public DbSet<Region>? Regions { get; set; }
    public DbSet<Country>? Countries { get; set; }
    public DbSet<Category>? Categories { get; set; }
    public DbSet<Product>? Products { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite(
            @"Data Source=database.db");
    }

}