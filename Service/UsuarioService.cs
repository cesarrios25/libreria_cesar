using LibreriaCesar.Models;
using LibreriaCesar.Response;
using LibreriaCesar.Data;

namespace LibreriaCesar.Service;

public class UsuarioService
{
    private readonly LibreriaAppDbContext _context;

    public UsuarioService(LibreriaAppDbContext context)
    {
        _context = context;
    }
    
    // OBTENER TODOS LOS USUARIOS
    public ResponseService<IEnumerable<Usuario>> ObtenerUsuarios()
    {
        var usuarios = _context.Usuarios.ToList();

        if (usuarios.Count <= 0)
        {
            return new ResponseService<IEnumerable<Usuario>>
            {
                Data = usuarios,
                Success = false,
                Message = "No hay usuarios registrados"
            };
        }
        return new ResponseService<IEnumerable<Usuario>>
        {
            Data = usuarios,
            Success = true,
            Message = "Usuarios obtenidos correctamente"
        };
    }
    
    // OBTENER USUARIO POR ID.
    public ResponseService<Usuario> ObtenerPorId(int id)
    {
        var usuario = _context.Usuarios.FirstOrDefault(u => u.IdUsuario == id);

        if (usuario == null)
        {
            return new ResponseService<Usuario>
            {
                Data = null,
                Success = false,
                Message = "Usuario no encontrado"
            };
        }
        return new ResponseService<Usuario>
        {
            Data = usuario,
            Success = true,
            Message = "Usuario encontrado"
        };
    }
    
    // CREAR USUARIO.
    public ResponseService<Usuario> CrearUsuario(Usuario usuario)
    {
        usuario.FechaRegistro = DateTime.Now;
        _context.Usuarios.Add(usuario);
        _context.SaveChanges();

        return new ResponseService<Usuario>
        {
            Data = usuario,
            Success = true,
            Message = "Usuario creado correctamente"
        };
    }
    
    // EDITAR USUARIO.
    public ResponseService<Usuario> EditarUsuario(Usuario usuario)
    {
        var usuarioExistente = _context.Usuarios.FirstOrDefault(u => u.IdUsuario == usuario.IdUsuario);

        if (usuarioExistente == null)
        {
            return new ResponseService<Usuario>
            {
                Data = null,
                Success = false,
                Message = "Usuario no encontrado"
            };
        }

        usuarioExistente.Nombre = usuario.Nombre;
        usuarioExistente.Apellido = usuario.Apellido;
        usuarioExistente.Email = usuario.Email;
        usuarioExistente.Telefono = usuario.Telefono;

        _context.SaveChanges();

        return new ResponseService<Usuario>
        {
            Data = usuarioExistente,
            Success = true,
            Message = "Usuario editado correctamente"
        };
    }
    
    // ELIMINAR USUARIO.
    public ResponseService<bool> Eliminar(int id)
    {
        var usuario = _context.Usuarios.FirstOrDefault(u => u.IdUsuario == id);

        if (usuario == null)
        {
            return new ResponseService<bool>
            {
                Data = false,
                Success = false,
                Message = "Usuario no encontrado"
            };
        }

        _context.Usuarios.Remove(usuario);
        _context.SaveChanges();

        return new ResponseService<bool>
        {
            Data = true,
            Success = true,
            Message = "Usuario eliminado correctamente"
        };
    }
}