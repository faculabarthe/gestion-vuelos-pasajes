using Dominio;
namespace Obligatorio
{
    internal class Program
    {
        static Sistema sistema = new Sistema();
        static void Main(string[] args)

        {
            //llamo a la precarga de datos y el menu
            sistema.PrecargaDatos();
            int opcion;
            bool flag = false;
            while (!flag)
            {

                OpcionesMenu();

                opcion = SolicitarNumero();
                try
                {
                    switch (opcion)
                    {
                        case 0:
                            flag = true;
                            break;
                        case 1:
                            MostrarDatosUsuarios();
                            break;
                        case 2:
                            MostrarVuelosSegunCodigo();
                            break;
                        case 3:
                            AgregarClienteOcacional();
                            break;
                        case 4:
                            MostrarPasajesSegunFechas();
                            break;

                        default:
                            Console.WriteLine("La opcion seleccionada no existe\n");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                if (opcion != 0)
                {
                    Console.WriteLine("\nPresione cualquier tecla para volver al menu");
                    Console.ReadKey();
                }

                Console.Clear();
            }

        }

        //opciones para el menu
        public static void OpcionesMenu()
        {
            Console.WriteLine("Menu\n");
            Console.WriteLine("1 - Listado de todos los clientes");
            Console.WriteLine("2 - Listar todos los vuelos que incluyen un aeropuerto en específico ");
            Console.WriteLine("3 - Alta de cliente ocasional");
            Console.WriteLine("4 - Listar los pasajes entre dos fechas");
            Console.WriteLine("Ingrese 0 para salir\n");

        }

        //solicitar numero para el menu
        private static int SolicitarNumero()
        {
            int numero = 0;
            bool seleccionoNumero = false;
            Console.WriteLine("Ingrese un valor\n");
            while (!seleccionoNumero)
            {

                try
                {

                    numero = int.Parse(Console.ReadLine());
                    seleccionoNumero = true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Ingrese un numero");
                }
            }


            return numero;
        }


        //metodo para agregar cliente ocacional
        private static void AgregarClienteOcacional()
        {

            bool elejible = sistema.ObtenerValorAleatorio();
            Console.WriteLine("Ingrese documento");
            string documento = Console.ReadLine();
            Console.WriteLine("Ingrese nombre");
            string nombre = Console.ReadLine();
            Console.WriteLine("Ingrese nacionalidad");
            string nacionalidad = Console.ReadLine();
            Console.WriteLine("Ingrese mail");
            string email = Console.ReadLine();
            Console.WriteLine("Ingrese contraseña");
            string constraseña = Console.ReadLine();
            if (!string.IsNullOrEmpty(documento) || !string.IsNullOrEmpty(nombre) || !string.IsNullOrEmpty(nacionalidad) || !string.IsNullOrEmpty(email) || !string.IsNullOrEmpty(constraseña))
            {
                Ocacional ocacional = new Ocacional(elejible, documento, nombre, nacionalidad, email, constraseña);
                sistema.AgregarCliente(ocacional);
                Console.WriteLine("Se agrego correctamente");
            }else
            {
                Console.WriteLine("Error en los datos");
            }

            
        }

        //metodo para  listar todos los clientes
        private static void MostrarDatosUsuarios()
        {
            List<Cliente> listaClientes = sistema.MostrarClientes();
            if (listaClientes.Count == 0)
            {
                Console.WriteLine("No hay clientes");   
            }
            foreach (Cliente cli in listaClientes)
            {
                if(cli is Ocacional ocacional)
                {
                    Ocacional oacional = (Ocacional) cli;
                    Console.WriteLine(cli);
                }
                else
                {
                    Premium premium = (Premium) cli;
                    Console.WriteLine(cli);
                }
            }
        }

        //metodo para que cuando ingreses un codigo te muestre los vuelos 
        private static void MostrarVuelosSegunCodigo()
        {
            Console.WriteLine("Escriba codigo IATA");
            string codigoIATA = Console.ReadLine();
            if(codigoIATA.Length==3)
            {
                List<Vuelo> vuelosSegunCodigo = sistema.VuelosSegunAeropuerto(codigoIATA);
                if (vuelosSegunCodigo.Count == 0)
                {
                    Console.WriteLine("No se encontro aeropuerto con ese codigo");
                }
                    foreach (Vuelo vuelo in vuelosSegunCodigo)
                    {
                        Console.WriteLine(vuelo);
                    }
            }
            else
            {
                Console.WriteLine("El codigo es incorrecto");
            }
            

        }

        //motodo para mostrar los pasajes entre las dos fechas ingresadas
        private static void MostrarPasajesSegunFechas()
        {
            Console.WriteLine("Escriba la primera fecha (formato: yyyy/MM/dd)");
            DateTime.TryParse(Console.ReadLine(), out DateTime fecha1);           
            Console.WriteLine("Escriba la segunda fecha (formato: yyyy/MM/dd)");
            DateTime.TryParse(Console.ReadLine(), out DateTime fecha2);
            if(fecha1 != new DateTime() || fecha1 != new DateTime() || fecha1 <fecha2)
            {
                List<Pasaje> PasajesSegunFechas = sistema.MostrarPasajesEntreFecha(fecha1, fecha2);
                if (PasajesSegunFechas.Count != 0)
                {
                    foreach (Pasaje pasaje in PasajesSegunFechas)
                    {
                        Console.WriteLine(pasaje);
                    }
                }
            }
            else
            {
                Console.WriteLine("Error en las fechas");
            }
            

        }
    }
}
