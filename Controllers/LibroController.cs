using LibreriaCesar.Models;
using LibreriaCesar.Service;
using Microsoft.AspNetCore.Mvc;

namespace LibreriaCesar.Controllers;

public class LibroController : Controller
{
    private readonly LibroService _libroService;

    public LibroController(LibroService libroService)
    {
        _libroService = libroService;
    }

    public IActionResult Index()
    {
        var resultado = _libroService.ObtenerLibros();
        return View(resultado.Data);
    }

    public IActionResult CrearLibro()
    {
        return View();
    }

    [HttpPost]
    public IActionResult CrearLibro(Libro libro)
    {
        ModelState.Remove("Prestamos");

        if (!ModelState.IsValid)
            return View(libro);

        var resultado = _libroService.CrearLibro(libro);

        if (!resultado.Success)
        {
            ModelState.AddModelError("", resultado.Message);
            return View(libro);
        }

        return RedirectToAction("Index");
    }

    public IActionResult EditarLibro(int id)
    {
        var resultado = _libroService.ObtenerPorId(id);

        if (!resultado.Success)
            return RedirectToAction("Index");

        return View(resultado.Data);
    }

    [HttpPost]
    public IActionResult EditarLibro(Libro libro)
    {
        ModelState.Remove("Prestamos");

        if (!ModelState.IsValid)
            return View(libro);

        var resultado = _libroService.EditarLibro(libro);

        if (!resultado.Success)
        {
            ModelState.AddModelError("", resultado.Message);
            return View(libro);
        }

        return RedirectToAction("Index");
    }

    public IActionResult EliminarLibro(int id)
    {
        var resultado = _libroService.ObtenerPorId(id);

        if (!resultado.Success)
            return RedirectToAction("Index");

        return View(resultado.Data);
    }

    [HttpPost, ActionName("EliminarLibro")]
    public IActionResult EliminarLibroConfirmado(int id)
    {
        _libroService.EliminarLibro(id);
        return RedirectToAction("Index");
    }
}