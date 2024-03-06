using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Extensions;

public static class WebApplicationBuilderDataBaseExtensions
{
    public static WebApplicationBuilder AddDataBase<ContextType>(this WebApplicationBuilder builder,
                                                                 bool useSQLiteConnection = false,
                                                                 bool useInMemoryDatabase = false)
        where ContextType : DbContext
    {
        if (useInMemoryDatabase) return builder.AddInMemoryDatabase<ContextType>();

        return builder;
    }

    private static WebApplicationBuilder AddInMemoryDatabase<ContextType>(this WebApplicationBuilder builder)
        where ContextType : DbContext
    {
        builder.Services.AddDbContext<ContextType>(opt => opt.UseInMemoryDatabase("InMemoryDatabase"));
        return builder;
    }

    // private static WebApplicationBuilder AddSqliteDatabase<ContextType>(this WebApplicationBuilder builder)
    //  where ContextType : DbContext
    // {

    //     var dbName = "SQLiteDB.db";
    //     var folder = Environment.SpecialFolder.LocalApplicationData;
    //     var path = Environment.GetFolderPath(folder);
    //     var DbPath = System.IO.Path.Join(path, dbName);
    //     builder.Services.AddDbContext<ContextType>(options => options.UseSqlite($"Data Source={DbPath}"));
    //     return builder;
    // }

    // private static WebApplicationBuilder AddEntityFrameworkDatabase<ContextType>(this WebApplicationBuilder builder)
    //     where ContextType : DbContext
    // {
    //     builder.Services.AddDbContext<ContextType>(options =>
    //     {
    //         string connectioString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new ArgumentException("DefaultConnection");
    //         options.UseSqlServer(connectioString);
    //     });
    //     return builder;
    // }


}
