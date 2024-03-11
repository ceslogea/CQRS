namespace Hotel.Api.Infrastructure.Data;

using System.Reflection;
using Hotel.Api.Application.Interfaces;
using Hotel.Api.Domain.Data;
using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext : DbContext , IApplicationDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) { }

    public DbSet<HotelEntity> Hotels => Set<HotelEntity>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
