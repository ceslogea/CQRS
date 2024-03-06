using Microsoft.EntityFrameworkCore;
using Infrastructure.Data.Entities;

namespace Hotel.Query.Data;

public class HotelDbContext : DbContext
{
    public HotelDbContext(DbContextOptions<HotelDbContext> options)
       : base(options) { }

    public DbSet<HotelEntity> Hotels => Set<HotelEntity>();
}
