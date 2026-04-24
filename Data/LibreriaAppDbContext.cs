using LibreriaCesar.Models;
using Microsoft.EntityFrameworkCore;

namespace LibreriaCesar.Data;

public class LibreriaAppDbContext : DbContext
{
    public LibreriaAppDbContext(DbContextOptions<LibreriaAppDbContext> options) : base(options){}
    
    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<Libro> Libros { get; set; }
    public DbSet<Prestamo> Prestamos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<Prestamo>()
            .HasOne(p=>p.Usuario)
            .WithMany(u=>u.Prestamos)
            .HasForeignKey(p=>p.UsuarioId)
            .OnDelete(DeleteBehavior.Restrict);
        
        modelBuilder.Entity<Prestamo>()
            .HasOne(p=>p.Libro)
            .WithMany(l=>l.Prestamos)
            .HasForeignKey(p=>p.LibroId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}