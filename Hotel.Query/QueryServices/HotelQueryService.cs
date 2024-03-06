using Hotel.Query.Data;
using Hotel.Query.Dtos;
using Infrastructure.Data.Entities;
using Microsoft.EntityFrameworkCore;

public class HotelQueryService : IHotelQueryService
{
    private readonly HotelDbContext db;

    public HotelQueryService(HotelDbContext db)
    {
        this.db = db;
    }

    public async Task<IResult> GetAllHotels()
    {
        HotelResponseDto[] value = await db.Hotels.Include(r=>r.Address).Select(x => HotelResponseDto.CreateFromQueryResult(x)).ToArrayAsync();
        return TypedResults.Ok(value);
    }

    public async Task<IResult> GetHotel(int id)
    {
        return await db.Hotels.FindAsync(id)
            is IHotelEntity hotel
                ? TypedResults.Ok(HotelResponseDto.CreateFromQueryResult(hotel))
                : TypedResults.NotFound();
    }
}
