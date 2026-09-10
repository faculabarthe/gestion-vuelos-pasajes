using Dominio;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    public class LoginController : Controller
    {
        private Sistema s = Sistema.Instancia;
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]

        public IActionResult Login()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Login(string email, string contrasenia)
        {
            try
            {
                if(string.IsNullOrEmpty(email) && string.IsNullOrEmpty(contrasenia))
                {
                    ViewBag.Mensaje = "Ingrese datos";
                }else
                {
                    Usuario usuario = s.BusarUsuario(email);
                    if(usuario != null)
                    {
                        if (usuario.Contrasenia == contrasenia)
                        {
                            HttpContext.Session.SetString("email", usuario.Email);
                            HttpContext.Session.SetString("contrasenia", usuario.Contrasenia);

                            if (usuario is Administrador)
                            {
                                HttpContext.Session.SetString("rol", "Administrador");
                            }
                            else
                            if (usuario is Ocacional)
                            {
                                HttpContext.Session.SetString("rol", "Ocacional");
                            }
                            else if (usuario is Premium)
                            {
                                HttpContext.Session.SetString("rol", "Premium");
                            }
                            return Redirect("/Usuario/Index");
                        }
                        else
                        {
                            ViewBag.Mensaje = "Usuario o contrasenia incorrecta";
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                ViewBag.Mensaje = ex.Message;
            }

            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}
