// Em MyProject.Infrastructure
using Hotel.Api.Data;
using Hotel.Api.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static void AddInfrastructure(this IServiceCollection services)
    {
        services.AddDbContext<HotelContext>(opt => opt.UseInMemoryDatabase("InMemoryDatabase"));
        services.AddScoped<IHotelRepository, HotelRepository>();
    }
}
