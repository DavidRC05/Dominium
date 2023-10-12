using DominiumLocal.Models;
using System.Collections.Generic;
using System.Data.Entity;

public class conexiondb : DbContext
{
    public conexiondb() : base("DefaultConnection")
    {
    }

    public DbSet<usuario> Usuarios { get; set; } // DbSet para la tabla de usuarios

    // Otras propiedades DbSet para las demás tablas de la base de datos

    // Otras configuraciones específicas de Entity Framework, si es necesario
}
