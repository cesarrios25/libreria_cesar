using System.ComponentModel.DataAnnotations;

namespace LibreriaCesar.Models;

public class Usuario
{
    [Key]
    public int IdUsuario { get; set; }
    public string Nombre { get; set; }
    public string Apellido { get; set; }
    public string Email { get; set; }
    public string Telefono { get; set; }
    public DateTime FechaRegistro { get; set; } // = DateTime.UtcNow;
    
    public ICollection<Prestamo> Prestamos { get; set; }
}