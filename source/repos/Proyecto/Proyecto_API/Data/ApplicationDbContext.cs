using Microsoft.EntityFrameworkCore;
using Proyecto_API.Models;

namespace Proyecto_API.Data
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
        {
            
        }
        public DbSet<Usuario>Usuarios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Usuario>().HasData(
                new Usuario()
                {
                    Id = 1,
                    Nombre = "Prueba",
                    Apellido = "Prueba1",
                    Correo = "cortesantonio822@gmail.com",
                    Numero = "4494597406",
                    ImagenUrl = "",
                    FechaCreacion = DateTime.Now,
                },
                new Usuario()
                {
                    Id = 2,
                    Nombre = "Prueba2",
                    Apellido = "Prueba2",
                    Correo = "cortesantonio820@gmail.com",
                    Numero = "4494597405",
                    ImagenUrl = "",
                    FechaCreacion = DateTime.Now,
                });
        }
    }
}
