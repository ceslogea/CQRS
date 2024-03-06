namespace Hotel.Api.Data;
using Infrastructure.Data.Entities;
using Microsoft.EntityFrameworkCore;

public class HotelContext : DbContext
{
    public HotelContext(DbContextOptions<HotelContext> options)
        : base(options) { }

    public DbSet<HotelEntity> Todos => Set<HotelEntity>();
}
