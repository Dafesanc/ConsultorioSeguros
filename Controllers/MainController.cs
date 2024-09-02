using ConsultorioSeguros.Data;
using ConsultorioSeguros.Models;
using Microsoft.AspNetCore.Mvc;

namespace ConsultorioSeguros.Controllers
{
    public class MainController : Controller
    {
        private RegistroVentaRepository _ventaRepository = new RegistroVentaRepository();
        private readonly ClientesRepository _clientesRepository = new ClientesRepository();
        private readonly SeguroRepository _seguroRepository = new SeguroRepository();
        private AseguradoSeguroRepository _aseguradoSeguroRepository = new AseguradoSeguroRepository();


        [HttpGet]
        public IActionResult Main()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("Usuario")))
            {
                return RedirectToAction("Login", "Acceso");
            }
            var aseguradoSeguroData = _ventaRepository.GetAseguradoSeguroData();
            return View(aseguradoSeguroData);
        }

        [HttpGet]
        public IActionResult Crear()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("Usuario")))
            {
                return RedirectToAction("Login", "Acceso");
            }
            ViewBag.Clientes = _clientesRepository.GetAll();
            ViewBag.Seguros = _seguroRepository.GetAll();
            return View();

        }

        [HttpPost]
        public IActionResult Crear(AseguradoSeguro aseguradoSeguro)
        {
            if (ModelState.IsValid)
            {
                _aseguradoSeguroRepository.InsertarAseguradoSeguro(aseguradoSeguro);
                TempData["SuccessMessage"] = "Registro exitoso";
                return RedirectToAction("Main"); // Redirige a una vista donde se muestre la lista de asegurados
            }
            foreach (var modelStateKey in ModelState.Keys)
            {
                var value = ModelState[modelStateKey];
                foreach (var error in value.Errors)
                {
                    Console.WriteLine($"Key: {modelStateKey}, Error: {error.ErrorMessage}");
                }
            }
            ViewBag.Clientes = _clientesRepository.GetAll();
            TempData["ErrorMessage"] = "Hubo un error en el formulario. Revisa los datos.";
            return View(aseguradoSeguro);
        }
    }
}
