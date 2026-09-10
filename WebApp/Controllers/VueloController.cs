using Dominio;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    public class VueloController : Controller
    {
        Sistema s = Sistema.Instancia;
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult BuscarVueloPorRuta()
        {

            return View();
        }

        [HttpPost]
        public IActionResult BuscarVueloPorRuta(string codigoIATA)
        {
            try 
            {
                if(string.IsNullOrEmpty(codigoIATA))
                {
                    ViewBag.Mensaje = "Codigo IATA vacio";
                    return View();
                }

                List<Vuelo> lista = s.VuelosSegunAeropuerto(codigoIATA.ToUpper());

                if (lista == null || lista.Count == 0)
                {
                    ViewBag.Mensaje = "No se encontraron vuelos para el código ingresado.";
                }
                else
                {
                    ViewBag.listaVuelo = lista;
                }
            } catch (Exception ex)
            {
                ViewBag.Mensaje = ex.Message;
                return View();
            }

            return View();
        }



        public IActionResult VerYComprarVuelo()
        {
            List<Vuelo> lista = s.Vuelos;
            ViewBag.listaVuelo = lista;

            return View();
        }

        [HttpGet]
        public IActionResult VerDetallesVuelo(string idVuelo)
        {
            Vuelo vuelo = s.VueloSegunID(idVuelo);
            ViewBag.vuelo = vuelo;
            return View();
        }








    }
}
