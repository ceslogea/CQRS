using Hotel.Api.Domain.Data;
using Microsoft.EntityFrameworkCore;

namespace Hotel.Api.Application.Interfaces;

public interface IApplicationDbContext
{
    DbSet<HotelEntity> Hotels { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
