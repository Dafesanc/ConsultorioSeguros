using ConsultorioSeguros.Data;
using ConsultorioSeguros.Models;
using Microsoft.AspNetCore.Mvc;

namespace ConsultorioSeguros.Controllers
{
    public class ClienteController : Controller
    {
        private ClientesRepository _repositorio = new ClientesRepository();

        [HttpGet]
        public IActionResult ClienteView()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("Usuario")))
            {
                return RedirectToAction("Login", "Acceso");
            }
            var seguros = _repositorio.GetAll();
            return View(seguros);
        }

        [HttpGet]
        public IActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Crear(Clientes cliente)
        {
            if (ModelState.IsValid)
            {
                _repositorio.AgregarCliente(cliente);
                return RedirectToAction("ClienteView");
            }
            return View(cliente);
        }

        [HttpGet]

        public IActionResult Editar(int id)
        {
            Clientes cliente = _repositorio.GetById(id);
            if (cliente == null)
            {
                return NotFound();
            }
            return View(cliente);
        }

        [HttpPost]
        public IActionResult Editar(Clientes cliente)
        {
            if (ModelState.IsValid)
            {
                _repositorio.Actualizar(cliente);
                return RedirectToAction("ClienteView");
            }
            return View(cliente);
        }
        

        public IActionResult Eliminar(int id)
        {
            Clientes
cliente = _repositorio.GetById(id);
            if (cliente == null)
            {
                return NotFound();
            }
            _repositorio.Eliminar(id);
            return RedirectToAction("ClienteView");

        }

        [HttpGet]
        public IActionResult CargarExcel()
        {
            return View();
        }



        [HttpPost]
        public IActionResult CargarExcel(IFormFile archivoExcel)
        {
            if (archivoExcel != null && archivoExcel.Length > 0)
            {
                // Lógica para procesar el archivo Excel y cargar los datos en la base de datos.
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "uploads", archivoExcel.FileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    archivoExcel.CopyTo(stream);
                }
                _repositorio.ProcesarExcel(filePath);
            }

            return RedirectToAction("ClienteView");
        }
    }
}
