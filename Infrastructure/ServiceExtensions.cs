
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using DataAccess.Data;
using DataAccess.Interfaces;
using DataAccess.Repositories;

namespace DataAccess;

public static class ServiceExtensions
{
    public static void AddDbContext(this IServiceCollection services, string connectionString, MySqlServerVersion serverVersion)
    {
        services.AddDbContext<AppDbContext>(x =>
            x.UseMySql(connectionString, serverVersion));
    }

    public static void AddRepository(this IServiceCollection services)
    {
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
    }
}
