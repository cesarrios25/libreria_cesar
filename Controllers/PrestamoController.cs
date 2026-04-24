using LibreriaCesar.Models;
using LibreriaCesar.Service;
using Microsoft.AspNetCore.Mvc;

namespace LibreriaCesar.Controllers;

public class PrestamoController : Controller
{
    private readonly PrestamoService _prestamoService;
    private readonly UsuarioService _usuarioService;
    private readonly LibroService _libroService;

    public PrestamoController(PrestamoService prestamoService, UsuarioService usuarioService, LibroService libroService)
    {
        _prestamoService = prestamoService;
        _usuarioService = usuarioService;
        _libroService = libroService;
    }

    public IActionResult Index()
    {
        var resultado = _prestamoService.ObtenerPrestamos();
        return View(resultado.Data);
    }

    public IActionResult CrearPrestamo()
    {
        ViewBag.Usuarios = _usuarioService.ObtenerUsuarios().Data;
        ViewBag.Libros = _libroService.ObtenerLibros().Data;
        return View();
    }

    [HttpPost]
    public IActionResult CrearPrestamo(int usuarioId, int libroId)
    {
        var resultado = _prestamoService.CrearPrestamo(usuarioId, libroId);

        if (!resultado.Success)
        {
            ViewBag.Usuarios = _usuarioService.ObtenerUsuarios().Data;
            ViewBag.Libros = _libroService.ObtenerLibros().Data;
            ModelState.AddModelError("", resultado.Message);
            return View();
        }

        return RedirectToAction("Index");
    }

    public IActionResult DevolverPrestamo(int id)
    {
        var resultado = _prestamoService.ObtenerPorId(id);

        if (!resultado.Success)
            return RedirectToAction("Index");

        return View(resultado.Data);
    }

    [HttpPost, ActionName("DevolverPrestamo")]
    public IActionResult DevolverPrestamoConfirmado(int id)
    {
        _prestamoService.DevolverPrestamo(id);
        return RedirectToAction("Index");
    }

    public IActionResult EliminarPrestamo(int id)
    {
        var resultado = _prestamoService.ObtenerPorId(id);

        if (!resultado.Success)
            return RedirectToAction("Index");

        return View(resultado.Data);
    }

    [HttpPost, ActionName("EliminarPrestamo")]
    public IActionResult EliminarPrestamoConfirmado(int id)
    {
        _prestamoService.EliminarPrestamo(id);
        return RedirectToAction("Index");
    }
}