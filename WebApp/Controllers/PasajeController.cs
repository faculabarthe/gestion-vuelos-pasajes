using Dominio;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    public class PasajeController : Controller
    {
        private Sistema s = Sistema.Instancia;
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult VerPasajes()
        {
            List<Pasaje> listaPasajes = s.OrdenadosPorFecha();

            if (listaPasajes == null || listaPasajes.Count == 0)
            {
                ViewBag.Mensaje = "no hay pasajes comprados.";
            }
            else
            {
                ViewBag.pasajes = listaPasajes;
            }

            return View();
        }


        [HttpGet]
        public IActionResult ComprarPasaje(string idVuelo) //parametro que recibimos cuando hacen click en comprar vuelo (?idVuelo = NumeroVuelo)
        {

            ViewBag.IdVuelo = idVuelo; // lo mandamos aca y despues se lo mandamos al post como valor oculto (hidden)

            return View();
        }

        [HttpPost]

        public IActionResult ComprarPasaje(string idVuelo, DateTime fecha, Equipaje equipaje)
        {
            try
            {
                string email = HttpContext.Session.GetString("email");
                Cliente cliente = s.BuscarClientePorEmail(email);

                Vuelo vuelo = s.VueloSegunID(idVuelo);
                DateTime ahora = DateTime.Today;

                if(cliente == null || vuelo == null || fecha<ahora)
                {
                    ViewBag.Mensaje = "Error cliente, vuelo o fecha no encontrados";
                    return View();
                }

                Pasaje pasajeTemp = new Pasaje (vuelo, fecha, cliente, equipaje, 0); //pasaje temporal para despues calcular el precio

                decimal precio = pasajeTemp.CalcularPrecioPasaje();

                Pasaje pasaje = new Pasaje (vuelo, fecha, cliente, equipaje, precio);
                pasaje.Validar();

                s.AgregarPasaje(pasaje);

                ViewBag.Mensaje = "Pasaje comprado con exito";
                
            }
            catch (Exception ex)
            {
                ViewBag.Mensaje = ex.Message;
                return View();
            }
            return View();
        }


    }
}
