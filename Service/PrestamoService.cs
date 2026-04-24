using LibreriaCesar.Data;
using LibreriaCesar.Models;
using LibreriaCesar.Response;
using Microsoft.EntityFrameworkCore;

namespace LibreriaCesar.Service;

public class PrestamoService
{
    private readonly LibreriaAppDbContext _context;

    public PrestamoService(LibreriaAppDbContext context)
    {
        _context = context;
    }

    public ResponseService<IEnumerable<Prestamo>> ObtenerPrestamos()
    {
        var prestamos = _context.Prestamos
            .Include(p => p.Usuario)
            .Include(p => p.Libro)
            .ToList();

        if (prestamos.Count <= 0)
        {
            return new ResponseService<IEnumerable<Prestamo>>
            {
                Data = prestamos,
                Success = false,
                Message = "No hay préstamos registrados"
            };
        }
        return new ResponseService<IEnumerable<Prestamo>>
        {
            Data = prestamos,
            Success = true,
            Message = "Préstamos obtenidos correctamente"
        };
    }

    public ResponseService<Prestamo> ObtenerPorId(int id)
    {
        var prestamo = _context.Prestamos
            .Include(p => p.Usuario)
            .Include(p => p.Libro)
            .FirstOrDefault(p => p.IdPrestamo == id);

        if (prestamo == null)
        {
            return new ResponseService<Prestamo>
            {
                Data = null,
                Success = false,
                Message = "Préstamo no encontrado"
            };
        }
        return new ResponseService<Prestamo>
        {
            Data = prestamo,
            Success = true,
            Message = "Préstamo encontrado"
        };
    }

    public ResponseService<Prestamo> CrearPrestamo(int usuarioId, int libroId)
    {
        var libro = _context.Libros.FirstOrDefault(l => l.IdLibro == libroId);

        if (libro == null)
        {
            return new ResponseService<Prestamo>
            {
                Data = null,
                Success = false,
                Message = "Libro no encontrado"
            };
        }

        if (libro.StockDisponible <= 0)
        {
            return new ResponseService<Prestamo>
            {
                Data = null,
                Success = false,
                Message = "No hay stock disponible para este libro"
            };
        }

        var prestamo = new Prestamo
        {
            UsuarioId = usuarioId,
            LibroId = libroId,
            FechaPrestamo = DateTime.Now,
            FechaDevolucion = null
        };

        libro.StockDisponible -= 1;

        _context.Prestamos.Add(prestamo);
        _context.SaveChanges();

        return new ResponseService<Prestamo>
        {
            Data = prestamo,
            Success = true,
            Message = "Préstamo registrado correctamente"
        };
    }

    public ResponseService<Prestamo> DevolverPrestamo(int id)
    {
        var prestamo = _context.Prestamos
            .Include(p => p.Libro)
            .FirstOrDefault(p => p.IdPrestamo == id);

        if (prestamo == null)
        {
            return new ResponseService<Prestamo>
            {
                Data = null,
                Success = false,
                Message = "Préstamo no encontrado"
            };
        }

        if (prestamo.FechaDevolucion != null)
        {
            return new ResponseService<Prestamo>
            {
                Data = null,
                Success = false,
                Message = "Este préstamo ya fue devuelto"
            };
        }

        prestamo.FechaDevolucion = DateTime.Now;
        prestamo.Libro.StockDisponible += 1;

        _context.SaveChanges();

        return new ResponseService<Prestamo>
        {
            Data = prestamo,
            Success = true,
            Message = "Libro devuelto correctamente"
        };
    }

    public ResponseService<bool> EliminarPrestamo(int id)
    {
        var prestamo = _context.Prestamos.FirstOrDefault(p => p.IdPrestamo == id);

        if (prestamo == null)
        {
            return new ResponseService<bool>
            {
                Data = false,
                Success = false,
                Message = "Préstamo no encontrado"
            };
        }

        _context.Prestamos.Remove(prestamo);
        _context.SaveChanges();

        return new ResponseService<bool>
        {
            Data = true,
            Success = true,
            Message = "Préstamo eliminado correctamente"
        };
    }
}