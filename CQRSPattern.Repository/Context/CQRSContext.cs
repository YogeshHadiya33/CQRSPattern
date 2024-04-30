using CQRSPattern.Entity.Employee.Database;
using CQRSPattern.Entity.User.Database;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CQRSPattern.Repository.Context;

public class CQRSContext : IdentityDbContext<User>
{
    public CQRSContext(DbContextOptions<CQRSContext> options) : base(options)
    {
    }

    public DbSet<Employee> Employees { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}