using LibreriaCesar.Models;
using LibreriaCesar.Service;
using Microsoft.AspNetCore.Mvc;

namespace LibreriaCesar.Controllers;

public class UsuarioController : Controller
{
    private readonly UsuarioService _usuarioService;

    public UsuarioController(UsuarioService usuarioService)
    {
        _usuarioService = usuarioService;
    }

    // OBTENER USUARIOS.
    public IActionResult Index()
    {
        var resultado = _usuarioService.ObtenerUsuarios();
        return View(resultado.Data);
    }

    // CREAR USUARIO -> GET
    public IActionResult CrearUsuario()
    {
        return View();
    }

    // CREAR USUARIO -> POST
    [HttpPost]
    public IActionResult CrearUsuario(Usuario usuario)
    {
        ModelState.Remove("FechaRegistro");
        ModelState.Remove("Prestamos");

        if (!ModelState.IsValid)
            return View(usuario);

        var resultado = _usuarioService.CrearUsuario(usuario);

        if (!resultado.Success)
        {
            ModelState.AddModelError("", resultado.Message);
            return View(usuario);
        }

        return RedirectToAction("Index");
    }

    // EDITAR USUARIO -> GET
    public IActionResult EditarUsuario(int id)
    {
        var resultado = _usuarioService.ObtenerPorId(id);

        if (!resultado.Success)
            return RedirectToAction("Index");

        return View(resultado.Data);
    }

    // EDITAR USUARIO -> POST
    [HttpPost]
    public IActionResult EditarUsuario(Usuario usuario)
    {
        ModelState.Remove("Prestamos");

        if (!ModelState.IsValid)
            return View(usuario);

        var resultado = _usuarioService.EditarUsuario(usuario);

        if (!resultado.Success)
        {
            ModelState.AddModelError("", resultado.Message);
            return View(usuario);
        }

        return RedirectToAction("Index");
    }

    // ELIMINAR USUARIO -> GET
    public IActionResult EliminarUsuario(int id)
    {
        var resultado = _usuarioService.ObtenerPorId(id);

        if (!resultado.Success)
            return RedirectToAction("Index");

        return View(resultado.Data);
    }

    // ELIMINAR USUARIO -> POST
    [HttpPost, ActionName("EliminarUsuario")]
    public IActionResult EliminarUsuarioConfirmado(int id)
    {
        _usuarioService.Eliminar(id);
        return RedirectToAction("Index");
    }
}