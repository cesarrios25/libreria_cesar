using System.ComponentModel.DataAnnotations;

namespace LibreriaCesar.Models;

public class Libro
{
    [Key]
    public int IdLibro { get; set; }
    public string Titulo { get; set; }
    public string Autor { get; set; }
    public string ISBN { get; set; }
    public string Genero { get; set; }
    public int AnoPublicacion { get; set; }
    public int StockTotal { get; set; }
    public int StockDisponible { get; set; }

    public ICollection<Prestamo> Prestamos { get; set; }
}