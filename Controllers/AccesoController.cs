using Microsoft.AspNetCore.Mvc;
using ConsultorioSeguros.Data_Access;
using ConsultorioSeguros.Models;
using Microsoft.EntityFrameworkCore;
using ConsultorioSeguros.ViewModels;


namespace ConsultorioSeguros.Controllers
{
    public class AccesoController : Controller
    {
        private readonly AppDbContext _appDbContext;
        public AccesoController(AppDbContext appDbContext) { 
            _appDbContext = appDbContext;
        
        }

        public IActionResult Registrarse()
        {
            // Tu lógica de login...
            ViewData["ShowMenu"] = false;
            return View();
        }

        [HttpPost]
        public async Task <IActionResult> Registrarse(UsuarioVM usuario)
        {
            if(usuario.Password != usuario.ConfirmPassword)
            {
                ViewData["Mensje"] = "Las contraseñas que ingresaste no coinciden";
                return View();

            }

            Usuario user = new Usuario()
            {
                NombreCompleto = usuario.NombreCompleto,
                User = usuario.User,
                Correo = usuario.Correo,
                Password = usuario.Password,
            };
            await _appDbContext.usuarios.AddAsync(user);
            await _appDbContext.SaveChangesAsync();

            if (user.IdUsuario> 0)
            {
                return RedirectToAction("Login", "Acceso");

            }
            ViewData["Mensje"] = "No se pudo registrar el usuario";


            return View();

        }


        public IActionResult Login()
        {
            // Tu lógica de login...
            ViewData["ShowMenu"] = false;
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Login(Login usuario)
        {
            Usuario? usuario_econtrado = await _appDbContext.usuarios.Where(u =>
            u.Correo == usuario.Correo && u.Password == usuario.Password).FirstOrDefaultAsync();

            if (usuario_econtrado == null)
            {
                ViewData["Mensaje"] = "No existe o no se encontro coincidencias de este usuario";
                return View();

            }
            HttpContext.Session.SetString("Usuario", usuario_econtrado.Correo);
            return RedirectToAction("Main", "Main");

        }

        [HttpGet]
        public IActionResult RecuperarContraseña()
        {
            // Tu lógica de login...
            ViewData["ShowMenu"] = false;
            return View();
        }

    }
}
