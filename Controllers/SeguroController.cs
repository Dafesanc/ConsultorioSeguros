using ConsultorioSeguros.Data;
using ConsultorioSeguros.Models;
using Microsoft.AspNetCore.Mvc;

namespace ConsultorioSeguros.Controllers
{
    public class SeguroController : Controller
    {
        private SeguroRepository _repository = new SeguroRepository();

        [HttpGet]
        public IActionResult SeguroView()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("Usuario")))
            {
                return RedirectToAction("Login", "Acceso");
            }
            var seguros = _repository.GetAll();
            return View(seguros);
        }
        [HttpGet]
        public  IActionResult Crear()
        {
            var cultureInfo = new System.Globalization.CultureInfo("en-US");
            System.Threading.Thread.CurrentThread.CurrentCulture = cultureInfo;
            System.Threading.Thread.CurrentThread.CurrentUICulture = cultureInfo;

            return View();
        }

        [HttpPost]
        public IActionResult Crear(Seguros seguro)
        {
            // Cambia la cultura para aceptar . como separador decimal
            var cultureInfo = new System.Globalization.CultureInfo("en-US");
            System.Threading.Thread.CurrentThread.CurrentCulture = cultureInfo;
            System.Threading.Thread.CurrentThread.CurrentUICulture = cultureInfo;
            if (decimal.TryParse(seguro.SumaAsegurada.ToString(), out var sumaAsegurada))
            {
                seguro.SumaAsegurada = sumaAsegurada;
            }
            else
            {
                ModelState.AddModelError("SumaAsegurada", "El formato de Suma Asegurada es inválido.");
            }

            if (decimal.TryParse(seguro.Prima.ToString(), out var prima))
            {
                seguro.Prima = prima;
            }
            else
            {
                ModelState.AddModelError("Prima", "El formato de Prima es inválido.");
            }

            if (ModelState.IsValid)
            {
                _repository.Agregar(seguro);
                return RedirectToAction("SeguroView");
            }

            if (ModelState.IsValid)
            {
                _repository.Agregar(seguro);
                return RedirectToAction("SeguroView");
            }
            return View(seguro);
        }

        [HttpGet]

        public IActionResult Editar(int id)
        {
            Seguros seguro = _repository.GetById(id);
            if (seguro == null)
            {
                return NotFound();
            }
            return View(seguro);
        }
        [HttpPost]
        public IActionResult Editar(Seguros seguro)
        {
            if (ModelState.IsValid)
            {
                _repository.Actualizar(seguro);
                return RedirectToAction("SeguroView");
            }
            return View(seguro);
        }

        [HttpGet]

        public IActionResult Eliminar(int id)
        {
            Seguros seguro = _repository.GetById(id);
            if (seguro == null)
            {
                return NotFound();
            }
            _repository.Eliminar(id);
            return RedirectToAction("SeguroView");

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
                _repository.ProcesarExcel(filePath);
            }

            return RedirectToAction("SeguroView");
        }

    }
}
