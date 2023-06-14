using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UsuariosApp.Infra.Data.Contexts
{
    public class DataContextMigration : IDesignTimeDbContextFactory<DataContext>
    {
        public DataContext CreateDbContext(string[] args)
        {
            //localizar e lendo o arquivo appsettings.json
            var configurationBuilder = new ConfigurationBuilder();
            var path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
            configurationBuilder.AddJsonFile(path, false);

            //capturar a connectionstring localizada dentro do arquivo
            var root = configurationBuilder.Build();
            var connectionString = root.GetConnectionString("UsuariosApp");

            //injeção de dependência para o appsettings.json
            var builder = new DbContextOptionsBuilder<DataContext>();
            builder.UseSqlServer(connectionString); 

            return new DataContext(builder.Options);
        }
    }
}
