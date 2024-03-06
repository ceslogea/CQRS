namespace Hotel.Api.Data;

using System.Threading.Tasks;
using Hotel.Api.Domain;
using Infrastructure.Data.Entities;

public class HotelRepository : IHotelRepository
{
    private readonly HotelContext context;

    public HotelRepository(HotelContext hotelContext)
    {
        context = hotelContext;
    }

    public async Task AddAsync(CreateHotelDomain createHotelDomain)
    {

        var entry = createHotelDomain.CreateEntity();
        await context.AddAsync(entry);
    }

    public async Task DeleteAsync(Guid id)
    {
        if (await context.Todos.FindAsync(id) is HotelEntity todo)
        {
            context.Todos.Remove(todo);
            await context.SaveChangesAsync();
        }
    }

    public async Task SaveChangesAsync()
    {
        await context.SaveChangesAsync();
    }
}