using System.ComponentModel.DataAnnotations;

namespace LibreriaCesar.Models;

public class Prestamo
{
    [Key]
    public int IdPrestamo { get; set; }
    public DateTime FechaPrestamo { get; set; }
    public DateTime? FechaDevolucion { get; set; } 

    // FORANEAS
    public int UsuarioId { get; set; }
    public int LibroId { get; set; }
    
    public Usuario Usuario { get; set; }
    public Libro Libro { get; set; }
}