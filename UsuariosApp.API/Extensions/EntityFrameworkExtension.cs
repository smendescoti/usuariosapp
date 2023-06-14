using Microsoft.EntityFrameworkCore;
using UsuariosApp.Infra.Data.Contexts;

namespace UsuariosApp.API.Extensions
{
    public static class EntityFrameworkExtension
    {
        public static IServiceCollection AddEntityFramework
            (this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("UsuariosApp");
            services.AddDbContext<DataContext>(options => 
            {
                options.UseSqlServer(connectionString);
            });

            return services;
        }
    }
}
