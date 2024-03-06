namespace Hotel.Api.Domain;

public interface IHotelRepository
{
    Task AddAsync(CreateHotelDomain createHotelDomain);
    Task SaveChangesAsync();
    Task DeleteAsync(Guid id);
}
