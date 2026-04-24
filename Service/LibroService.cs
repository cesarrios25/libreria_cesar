using LibreriaCesar.Data;
using LibreriaCesar.Models;
using LibreriaCesar.Response;

namespace LibreriaCesar.Service;

public class LibroService
{
    private readonly LibreriaAppDbContext _context;

    public LibroService(LibreriaAppDbContext context)
    {
        _context = context;
    }

    public ResponseService<IEnumerable<Libro>> ObtenerLibros()
    {
        var libros = _context.Libros.ToList();

        if (libros.Count <= 0)
        {
            return new ResponseService<IEnumerable<Libro>>
            {
                Data = libros,
                Success = false,
                Message = "No hay libros registrados"
            };
        }
        return new ResponseService<IEnumerable<Libro>>
        {
            Data = libros,
            Success = true,
            Message = "Libros obtenidos correctamente"
        };
    }

    public ResponseService<Libro> ObtenerPorId(int id)
    {
        var libro = _context.Libros.FirstOrDefault(l => l.IdLibro == id);

        if (libro == null)
        {
            return new ResponseService<Libro>
            {
                Data = null,
                Success = false,
                Message = "Libro no encontrado"
            };
        }
        return new ResponseService<Libro>
        {
            Data = libro,
            Success = true,
            Message = "Libro encontrado"
        };
    }

    public ResponseService<Libro> CrearLibro(Libro libro)
    {
        _context.Libros.Add(libro);
        _context.SaveChanges();

        return new ResponseService<Libro>
        {
            Data = libro,
            Success = true,
            Message = "Libro creado correctamente"
        };
    }

    public ResponseService<Libro> EditarLibro(Libro libro)
    {
        var libroExistente = _context.Libros.FirstOrDefault(l => l.IdLibro == libro.IdLibro);

        if (libroExistente == null)
        {
            return new ResponseService<Libro>
            {
                Data = null,
                Success = false,
                Message = "Libro no encontrado"
            };
        }

        libroExistente.Titulo = libro.Titulo;
        libroExistente.Autor = libro.Autor;
        libroExistente.ISBN = libro.ISBN;
        libroExistente.Genero = libro.Genero;
        libroExistente.AnoPublicacion = libro.AnoPublicacion;
        libroExistente.StockTotal = libro.StockTotal;
        libroExistente.StockDisponible = libro.StockDisponible;

        _context.SaveChanges();

        return new ResponseService<Libro>
        {
            Data = libroExistente,
            Success = true,
            Message = "Libro editado correctamente"
        };
    }

    public ResponseService<bool> EliminarLibro(int id)
    {
        var libro = _context.Libros.FirstOrDefault(l => l.IdLibro == id);

        if (libro == null)
        {
            return new ResponseService<bool>
            {
                Data = false,
                Success = false,
                Message = "Libro no encontrado"
            };
        }

        _context.Libros.Remove(libro);
        _context.SaveChanges();

        return new ResponseService<bool>
        {
            Data = true,
            Success = true,
            Message = "Libro eliminado correctamente"
        };
    }
}