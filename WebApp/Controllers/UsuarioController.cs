using Dominio;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    public class UsuarioController : Controller
    {

        Sistema s = Sistema.Instancia;

        public IActionResult Index()
        {
            return View();
        }

        //Ver perfil: El pasajero podrá ver sus datos, los pasajeros premium ven además sus puntos
            [HttpGet]
        
        public IActionResult VerDatos()
        {

            string email = HttpContext.Session.GetString("email");
            Cliente cliente = s.BuscarClientePorEmail(email);
            
            return View(cliente);

        }




        [HttpGet]
        public IActionResult RegistrarClienteOcacional()
        {
            return View(new Ocacional());
        }

        [HttpPost]
        public IActionResult RegistrarClienteOcacional(Ocacional oc)
        {
            try
            {
               s.AgregarCliente(oc);
               ViewBag.Mensaje = "SE AGREGO CORRECTAMENTE";

            }
            catch (Exception ex)
            {
                ViewBag.Mensaje = ex.Message;
            }

            return View(oc);
        }

        [HttpGet]
       public IActionResult VerPasajeComprador()
        {
            string email = HttpContext.Session.GetString("email");
            Cliente cliente = s.BuscarClientePorEmail(email);
            List<Pasaje> lista  = s.ListaPasajeUsuario(email);
            ViewBag.lista = lista;
            return View();
        }

        [HttpGet]
        public IActionResult VerYEditarTodosLosUsuarios()
        {
            List<Usuario> lista = s.Usuarios;
            ViewBag.lista = lista;
            return View();
        }

        [HttpGet]
        public IActionResult CambiosUsuarioPremium(string email)
        {
            ViewBag.email = email;
            return View();
        }

        [HttpGet]
        public IActionResult CambiosUsuarioOcacional(string email)
        {
            ViewBag.email = email;
            return View();
        }

        [HttpPost]
        public IActionResult CambiosUsuarioPremium(int puntos, string email)
        {
            try
            {
               Premium p= s.CambiarPuntosPremium(email,puntos);
                
               ViewBag.Mensaje = "CAMBIO REALIZADO CON EXITO";
            }
            catch(Exception ex)
            {
                ViewBag.Mensaje=ex.Message;
            }



            return View();
        }

        [HttpPost]
        public IActionResult CambiosUsuarioOcacional(bool elejible, string email)
        {
            try
            {
                Ocacional o = s.CambiarElejibleOcacional(email, elejible);

                ViewBag.Mensaje = "CAMBIO REALIZADO CON EXITO";
            }
            catch (Exception ex)
            {
                ViewBag.Mensaje = ex.Message;
            }

            return View();
        }

    }
}
