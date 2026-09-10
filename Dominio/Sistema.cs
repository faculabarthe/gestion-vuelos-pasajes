using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.Unicode;
using System.Threading;
using System.Threading.Tasks;

namespace Dominio
{
    public class Sistema
    {
        ///listas 
        private List<Usuario> _usuarios;
        private List<Aeropuerto> _aeropuertos;
        private List<Avion> _aviones;
        private List<Pasaje> _pasajes;
        private List<Ruta> _rutas;
        private List<Vuelo> _vuelos;
        private static Sistema s_instancia; // 1er paso :patron singleton

        //get de la lista de Pasajes
        public List<Pasaje> Pasajes { get { return new List<Pasaje>(_pasajes); } }
        //get de la lista de usuarios
        public List<Usuario> Usuarios { get { return new List<Usuario>(_usuarios); } }

        public List<Vuelo> Vuelos { get { return new List<Vuelo>(_vuelos); } }

        // 2do paso :patron singleton
        public static Sistema Instancia
        {
            get
            {
                if (s_instancia == null) s_instancia = new Sistema();
                return s_instancia;
            }

        }

        //constructor de sistema
        public Sistema()
        {
            _usuarios = new List<Usuario>();
            _aviones = new List<Avion>();
            _aeropuertos = new List<Aeropuerto>();
            _pasajes = new List<Pasaje>();
            _rutas = new List<Ruta>();
            _vuelos = new List<Vuelo>();
            PrecargaDatos();
        }



        

        //metodo para agregar el usuario
        public void AgregarUsuario(Usuario usuario)
        {
            if(usuario is Administrador administrador)
            {
                administrador.Validar();
            }
            else
            {
                Cliente cliente = (Cliente) usuario;
                cliente.Validar();
            }

            if (_usuarios.Contains(usuario)){
                throw new Exception("Ya existe un usuario con ese nombre");
            }
            _usuarios.Add(usuario);
        }
        //metodo para agregar el cliente
        public void AgregarCliente(Cliente cliente)
        {
            if (cliente is Premium premium)
            {
                premium.Validar();
            }
            else
            {
                Ocacional ocacional = (Ocacional)cliente;
                ocacional.Validar();
            }

            if (_usuarios.Contains(cliente))
            {
                throw new Exception("Ya existe un client con ese nombre");
            }
            _usuarios.Add(cliente);
        }

        //metodo para agregar el avion
        public void AgregarAvion(Avion avion)
        {
            if (_aviones.Contains(avion))
            {
                throw new Exception("Ya existe un avion con esos datos");
            }
            _aviones.Add(avion);
        }
        //metodo para agregar el aeropuerto
        public void AgregarAeropuerto(Aeropuerto aeropuerto)
        {
            if (_aeropuertos.Contains(aeropuerto))
            {
                throw new Exception("Ya existe un aeropuerto con esos datos");
            }
            _aeropuertos.Add(aeropuerto);
        }
        //metodo para agregar la ruta
        public void AgregarRuta(Ruta ruta)
        {
            if (_rutas.Contains(ruta))
            {
                throw new Exception("Ya existe una ruta con esos datos");
            }
            _rutas.Add(ruta);
        }

        //metodo para agregar vuelo
        public void AgregarVuelo(Vuelo vuelo)
        {
            if (_vuelos.Contains(vuelo))
            {
                throw new Exception("Ya existe un vuelo con esos datos");
            }
            _vuelos.Add(vuelo); 
        }
        //metodo para agregar pasaje
        public void AgregarPasaje(Pasaje pasaje)
        {
            if (_pasajes.Contains(pasaje))
            {
                throw new Exception("Ya existe un pasaje con esos datos");
            }
            _pasajes.Add(pasaje);
        }

        //metodo para listar los vulos segun el codigo iata del aeropuerto
        public List<Vuelo> VuelosSegunAeropuerto(string codigoIATA)
        {
            List <Vuelo> listaVuelos = new List <Vuelo>();

            foreach(Vuelo vuelo in _vuelos)
            {
                if(vuelo.DevolverCodigoIATA(codigoIATA))
                {
                    listaVuelos.Add(vuelo);
                }
            }

            return listaVuelos;
        }
        
        //metodo para mostrar todos los clientes 
        public List<Cliente> MostrarClientes()
        {
            List<Cliente> mostrarClientes = new List<Cliente>();

            foreach (Usuario usuario in _usuarios)
            {
                if (usuario is Cliente)
                {
                    Cliente c = (Cliente)usuario;  //casteo para que el usuario sea cliente
                    mostrarClientes.Add(c);
                }
            }
            return mostrarClientes;
        }
       
        //metodo para mostrar los pasajes entre dos fechas
        public List<Pasaje> MostrarPasajesEntreFecha(DateTime fecha1, DateTime fecha2)
        {
            List<Pasaje> listaPasajes = new List<Pasaje>();

            foreach (Pasaje pasaje in _pasajes)
            {
                if (fecha1 <= pasaje.Fecha && fecha2 >= pasaje.Fecha)
                {
                    listaPasajes.Add(pasaje);
                }
            }

            return listaPasajes;

        }

        //metodo buscar usuario por email
        public Usuario BusarUsuario(string email)
        {
            foreach(Usuario u in _usuarios)
            {
                if (u.Email == email)
                {
                    return u;
                }
            }
            return null;
        }

        //metodo del cambiar puntos
        public Premium CambiarPuntosPremium(string email,int puntos)
        {
            if (puntos < 0)
            {
                throw new Exception("no se pueden asignar puntos negativos");
            }
                
            foreach (Usuario u in _usuarios)
            {
                    if(u is Premium p)
                    {
                        if (p.Email==email)
                        {
                            p.CambiarPuntos(puntos);
                            return p;
                        }
                    }
                
            }
            return null;
        }

        //metodo de cambiar elejible
        public Ocacional CambiarElejibleOcacional(string email, bool elejible)
        {
            foreach (Usuario u in _usuarios)
            {

                if (u is Ocacional o)
                {
                    if (o.Email == email)
                    {
                        o.CambiarElejible(elejible);
                        return o;
                    }
                }

            }
            return null;
        }
        
        //Buscar cliente por email 
        public Cliente BuscarClientePorEmail(string email) 
        {
            foreach (Usuario u in _usuarios)
            {
                if (u.Email == email && u is Cliente)
                {
                    return (Cliente)u;
                }
            }
            return null;
        }


        public List<Vuelo> VuelosSegunFecha(DateTime fecha)
        {
            List<Vuelo> listaVuelos = new List<Vuelo>();

            int numeroDia = (int)fecha.DayOfWeek;

            foreach (Vuelo vuelo in _vuelos)
            {
                foreach(int dia in vuelo.Frecuencias)
                {
                    if (numeroDia == dia)
                    {
                        listaVuelos.Add(vuelo);
                    }
                }
            }

            return listaVuelos;
        }
        //vuelo segun id 
        public Vuelo VueloSegunID(string idVuelo)
        {
            foreach(Vuelo v in _vuelos)
            {
                if (v.NumeroVuelo == idVuelo)
                {
                    return v;
                }
            }

            return null;
        }
        





        //metodo de precargar que llama a todas las precargas 
        public void PrecargaDatos()
        {
            PrecargarAdministrador();
            PrecargarClientePremium();
            PrecargarClienteOcacional();
            PrecargarAvion();
            PrecargarAeropuertos();
            PrecargarRutas();
            PrecargarVuelos();
            PrecargarPasajes();
        }

        //precarga aministradores
        private void PrecargarAdministrador()
        {
            Administrador a1 = new Administrador("crack", "crack10@gmail.com", "crack10");
            Administrador a2 = new Administrador("maquina", "maquina10@gmail.com", "maquina10");
            AgregarUsuario(a1);
            AgregarUsuario(a2);
        }

        //precarga clientes premium
        private void PrecargarClientePremium()
        {
            Premium p1 = new Premium(100, "11111111", "Marcos", "Uruguaya", "marcos@gmail.com", "marcos123");
            Premium p2 = new Premium(250, "22222222", "Lucía", "Argentina", "lucia@gmail.com", "lucia456");
            Premium p3 = new Premium(180, "33333333", "Carlos", "Chilena", "carlos@gmail.com", "carlos789");
            Premium p4 = new Premium(300, "44444444", "Ana", "Brasileña", "ana@gmail.com", "ana321");
            Premium p5 = new Premium(90, "55555555", "Jorge", "Paraguaya", "jorge@gmail.com", "jorge654");

            AgregarCliente(p1);
            AgregarCliente(p2);
            AgregarCliente(p3);
            AgregarCliente(p4);
            AgregarCliente(p5);
        }

        //precarga clientes ocacionales
        private void PrecargarClienteOcacional()
        {
            Ocacional o1 = new Ocacional("12121212", "Esteban", "Uruguaya", "esteban@gmail.com", "esteban123");
            Ocacional o2 = new Ocacional("66666666", "Florencia", "Argentina", "flor@gmail.com", "flor123");
            Ocacional o3 = new Ocacional("77777777", "Diego", "Peruana", "diego@gmail.com", "diego456");
            Ocacional o4 = new Ocacional("88888888", "Camila", "Colombiana", "camila@gmail.com", "camila789");
            Ocacional o5 = new Ocacional("99999999", "Martin", "Uruguaya", "martin@gmail.com", "martin321");

            AgregarCliente(o1);
            AgregarCliente(o2);
            AgregarCliente(o3);
            AgregarCliente(o4);
            AgregarCliente(o5);
        }

        //precarga avion
        private void PrecargarAvion()
        {
            Avion avion1 = new Avion("Elon Musk", "B104", 200, 20000, 100);
            Avion avion2 = new Avion("Boeing", "737 MAX", 180, 21000, 120);
            Avion avion3 = new Avion("Airbus", "A320", 150, 22000, 110);
            Avion avion4 = new Avion("Embraer", "E195", 120, 23000, 90);
            
            AgregarAvion(avion1);
            AgregarAvion(avion2);
            AgregarAvion(avion3);
            AgregarAvion(avion4);
        }

        //precarga aeropuerto 
        private void PrecargarAeropuertos()
        {
            Aeropuerto a1 = new Aeropuerto("MVD", "Montevideo", 5000, 150);
            Aeropuerto a2 = new Aeropuerto("EZE", "Buenos Aires", 7000, 200);
            Aeropuerto a3 = new Aeropuerto("GRU", "Sao Paulo", 8000, 220);
            Aeropuerto a4 = new Aeropuerto("SCL", "Santiago", 6500, 180);
            Aeropuerto a5 = new Aeropuerto("LIM", "Lima", 6000, 170);
            Aeropuerto a6 = new Aeropuerto("BOG", "Bogotá", 7500, 210);
            Aeropuerto a7 = new Aeropuerto("MEX", "Ciudad de México", 9000, 250);
            Aeropuerto a8 = new Aeropuerto("JFK", "Nueva York", 12000, 300);
            Aeropuerto a9 = new Aeropuerto("LAX", "Los Ángeles", 11000, 280);
            Aeropuerto a10 = new Aeropuerto("MAD", "Madrid", 9500, 270);
            Aeropuerto a11 = new Aeropuerto("CDG", "París", 10000, 290);
            Aeropuerto a12 = new Aeropuerto("LHR", "Londres", 10500, 310);
            Aeropuerto a13 = new Aeropuerto("FRA", "Frankfurt", 9800, 275);
            Aeropuerto a14 = new Aeropuerto("AMS", "Ámsterdam", 9700, 265);
            Aeropuerto a15 = new Aeropuerto("BCN", "Barcelona", 9400, 260);
            Aeropuerto a16 = new Aeropuerto("PTY", "Panamá", 7700, 190);
            Aeropuerto a17 = new Aeropuerto("MIA", "Miami", 11500, 285);
            Aeropuerto a18 = new Aeropuerto("YYZ", "Toronto", 8900, 240);
            Aeropuerto a19 = new Aeropuerto("FCO", "Roma", 9300, 255);
            Aeropuerto a20 = new Aeropuerto("SYD", "Sídney", 13000, 320);

            AgregarAeropuerto(a1);
            AgregarAeropuerto(a2);
            AgregarAeropuerto(a3);
            AgregarAeropuerto(a4);
            AgregarAeropuerto(a5);
            AgregarAeropuerto(a6);
            AgregarAeropuerto(a7);
            AgregarAeropuerto(a8);
            AgregarAeropuerto(a9);
            AgregarAeropuerto(a10);
            AgregarAeropuerto(a11);
            AgregarAeropuerto(a12);
            AgregarAeropuerto(a13);
            AgregarAeropuerto(a14);
            AgregarAeropuerto(a15);
            AgregarAeropuerto(a16);
            AgregarAeropuerto(a17);
            AgregarAeropuerto(a18);
            AgregarAeropuerto(a19);
            AgregarAeropuerto(a20);
        }

        //precargas de rutas
        private void PrecargarRutas()
        {
            Ruta ruta1 = new Ruta(200, _aeropuertos[1], _aeropuertos[0]); AgregarRuta(ruta1);
            Ruta ruta2 = new Ruta(1500, _aeropuertos[2], _aeropuertos[0]); AgregarRuta(ruta2);
            Ruta ruta3 = new Ruta(1700, _aeropuertos[3], _aeropuertos[0]); AgregarRuta(ruta3);
            Ruta ruta4 = new Ruta(2700, _aeropuertos[4], _aeropuertos[0]); AgregarRuta(ruta4);
            Ruta ruta5 = new Ruta(4800, _aeropuertos[5], _aeropuertos[1]); AgregarRuta(ruta5);
            Ruta ruta6 = new Ruta(7400, _aeropuertos[6], _aeropuertos[1]); AgregarRuta(ruta6);
            Ruta ruta7 = new Ruta(8300, _aeropuertos[7], _aeropuertos[2]); AgregarRuta(ruta7);
            Ruta ruta8 = new Ruta(10000, _aeropuertos[8], _aeropuertos[2]); AgregarRuta(ruta8);
            Ruta ruta9 = new Ruta(9500, _aeropuertos[9], _aeropuertos[3]); AgregarRuta(ruta9);
            Ruta ruta10 = new Ruta(9800, _aeropuertos[10], _aeropuertos[4]); AgregarRuta(ruta10);
            Ruta ruta11 = new Ruta(10200, _aeropuertos[11], _aeropuertos[5]); AgregarRuta(ruta11);
            Ruta ruta12 = new Ruta(10400, _aeropuertos[12], _aeropuertos[6]); AgregarRuta(ruta12);
            Ruta ruta13 = new Ruta(10600, _aeropuertos[13], _aeropuertos[7]); AgregarRuta(ruta13);
            Ruta ruta14 = new Ruta(10700, _aeropuertos[14], _aeropuertos[8]); AgregarRuta(ruta14);
            Ruta ruta15 = new Ruta(4900, _aeropuertos[15], _aeropuertos[6]); AgregarRuta(ruta15);
            Ruta ruta16 = new Ruta(7800, _aeropuertos[16], _aeropuertos[15]); AgregarRuta(ruta16);
            Ruta ruta17 = new Ruta(7100, _aeropuertos[17], _aeropuertos[16]); AgregarRuta(ruta17);
            Ruta ruta18 = new Ruta(9200, _aeropuertos[18], _aeropuertos[9]); AgregarRuta(ruta18);
            Ruta ruta19 = new Ruta(15000, _aeropuertos[19], _aeropuertos[10]); AgregarRuta(ruta19);
            Ruta ruta20 = new Ruta(19300, _aeropuertos[19], _aeropuertos[0]); AgregarRuta(ruta20);
            Ruta ruta21 = new Ruta(8900, _aeropuertos[0], _aeropuertos[5]); AgregarRuta(ruta21);
            Ruta ruta22 = new Ruta(11200, _aeropuertos[1], _aeropuertos[7]); AgregarRuta(ruta22);
            Ruta ruta23 = new Ruta(12500, _aeropuertos[2], _aeropuertos[8]); AgregarRuta(ruta23);
            Ruta ruta24 = new Ruta(7200, _aeropuertos[3], _aeropuertos[15]); AgregarRuta(ruta24);
            Ruta ruta25 = new Ruta(13700, _aeropuertos[4], _aeropuertos[19]); AgregarRuta(ruta25);
            Ruta ruta26 = new Ruta(10300, _aeropuertos[6], _aeropuertos[14]); AgregarRuta(ruta26);
            Ruta ruta27 = new Ruta(8700, _aeropuertos[9], _aeropuertos[16]); AgregarRuta(ruta27);
            Ruta ruta28 = new Ruta(6200, _aeropuertos[10], _aeropuertos[17]); AgregarRuta(ruta28);
            Ruta ruta29 = new Ruta(8200, _aeropuertos[12], _aeropuertos[18]); AgregarRuta(ruta29);
            Ruta ruta30 = new Ruta(10500, _aeropuertos[13], _aeropuertos[11]); AgregarRuta(ruta30);
        }

        //precargar vuelos
        private void PrecargarVuelos()
        {
            Vuelo vuelo1 = new Vuelo("VV001", _rutas[0], _aviones[0], new List<Frecuencia> { Frecuencia.Lunes, Frecuencia.Miercoles, Frecuencia.Viernes });
            AgregarVuelo(vuelo1);

            Vuelo vuelo2 = new Vuelo("VV002", _rutas[1], _aviones[1], new List<Frecuencia> { Frecuencia.Martes, Frecuencia.Jueves });
            AgregarVuelo(vuelo2);

            Vuelo vuelo3 = new Vuelo("V003", _rutas[2], _aviones[2], new List<Frecuencia> { Frecuencia.Miercoles, Frecuencia.Viernes });
            AgregarVuelo(vuelo3);

            Vuelo vuelo4 = new Vuelo("VV004", _rutas[3], _aviones[3], new List<Frecuencia> { Frecuencia.Lunes, Frecuencia.Jueves });
            AgregarVuelo(vuelo4);

            Vuelo vuelo5 = new Vuelo("VV005", _rutas[4], _aviones[0], new List<Frecuencia> { Frecuencia.Lunes, Frecuencia.Miercoles, Frecuencia.Viernes });
            AgregarVuelo(vuelo5);

            Vuelo vuelo6 = new Vuelo("VV006", _rutas[5], _aviones[1], new List<Frecuencia> { Frecuencia.Martes, Frecuencia.Jueves });
            AgregarVuelo(vuelo6);

            Vuelo vuelo7 = new Vuelo("VV007", _rutas[6], _aviones[2], new List<Frecuencia> { Frecuencia.Miercoles, Frecuencia.Viernes });
            AgregarVuelo(vuelo7);

            Vuelo vuelo8 = new Vuelo("VV008", _rutas[7], _aviones[3], new List<Frecuencia> { Frecuencia.Lunes, Frecuencia.Jueves });
            AgregarVuelo(vuelo8);

            Vuelo vuelo9 = new Vuelo("VV009", _rutas[8], _aviones[0], new List<Frecuencia> { Frecuencia.Lunes, Frecuencia.Miercoles, Frecuencia.Viernes });
            AgregarVuelo(vuelo9);

            Vuelo vuelo10 = new Vuelo("VV010", _rutas[9], _aviones[1], new List<Frecuencia> { Frecuencia.Martes, Frecuencia.Jueves });
            AgregarVuelo(vuelo10);

            Vuelo vuelo11 = new Vuelo("VV011", _rutas[10], _aviones[2], new List<Frecuencia> { Frecuencia.Miercoles, Frecuencia.Viernes });
            AgregarVuelo(vuelo11);

            Vuelo vuelo12 = new Vuelo("VV012", _rutas[11], _aviones[3], new List<Frecuencia> { Frecuencia.Lunes, Frecuencia.Jueves });
            AgregarVuelo(vuelo12);

            Vuelo vuelo13 = new Vuelo("VV013", _rutas[12], _aviones[0], new List<Frecuencia> { Frecuencia.Lunes, Frecuencia.Miercoles, Frecuencia.Viernes });
            AgregarVuelo(vuelo13);

            Vuelo vuelo14 = new Vuelo("VV014", _rutas[13], _aviones[1], new List<Frecuencia> { Frecuencia.Martes, Frecuencia.Jueves });
            AgregarVuelo(vuelo14);

            Vuelo vuelo15 = new Vuelo("VV015", _rutas[14], _aviones[2], new List<Frecuencia> { Frecuencia.Miercoles, Frecuencia.Viernes });
            AgregarVuelo(vuelo15);

            Vuelo vuelo16 = new Vuelo("VV016", _rutas[15], _aviones[3], new List<Frecuencia> { Frecuencia.Lunes, Frecuencia.Jueves });
            AgregarVuelo(vuelo16);

            Vuelo vuelo17 = new Vuelo("VV017", _rutas[16], _aviones[0], new List<Frecuencia> { Frecuencia.Lunes, Frecuencia.Miercoles, Frecuencia.Viernes });
            AgregarVuelo(vuelo17);

            Vuelo vuelo18 = new Vuelo("VV018", _rutas[17], _aviones[1], new List<Frecuencia> { Frecuencia.Martes, Frecuencia.Jueves });
            AgregarVuelo(vuelo18);

            Vuelo vuelo19 = new Vuelo("VV019", _rutas[18], _aviones[2], new List<Frecuencia> { Frecuencia.Miercoles, Frecuencia.Viernes });
            AgregarVuelo(vuelo19);

            Vuelo vuelo20 = new Vuelo("VV020", _rutas[19], _aviones[3], new List<Frecuencia> { Frecuencia.Lunes, Frecuencia.Jueves });
            AgregarVuelo(vuelo20);

            Vuelo vuelo21 = new Vuelo("VV021", _rutas[20], _aviones[0], new List<Frecuencia> { Frecuencia.Lunes, Frecuencia.Miercoles, Frecuencia.Viernes });
            AgregarVuelo(vuelo21);

            Vuelo vuelo22 = new Vuelo("VV022", _rutas[21], _aviones[1], new List<Frecuencia> { Frecuencia.Martes, Frecuencia.Jueves });
            AgregarVuelo(vuelo22);

            Vuelo vuelo23 = new Vuelo("VV023", _rutas[22], _aviones[2], new List<Frecuencia> { Frecuencia.Miercoles, Frecuencia.Viernes });
            AgregarVuelo(vuelo23);

            Vuelo vuelo24 = new Vuelo("VV024", _rutas[23], _aviones[3], new List<Frecuencia> { Frecuencia.Lunes, Frecuencia.Jueves });
            AgregarVuelo(vuelo24);

            Vuelo vuelo25 = new Vuelo("VV025", _rutas[24], _aviones[0], new List<Frecuencia> { Frecuencia.Lunes, Frecuencia.Miercoles, Frecuencia.Viernes });
            AgregarVuelo(vuelo25);

            Vuelo vuelo26 = new Vuelo("VV026", _rutas[25], _aviones[1], new List<Frecuencia> { Frecuencia.Martes, Frecuencia.Jueves });
            AgregarVuelo(vuelo26);

            Vuelo vuelo27 = new Vuelo("VV027", _rutas[26], _aviones[2], new List<Frecuencia> { Frecuencia.Miercoles, Frecuencia.Viernes });
            AgregarVuelo(vuelo27);

            Vuelo vuelo28 = new Vuelo("VV028", _rutas[27], _aviones[3], new List<Frecuencia> { Frecuencia.Lunes, Frecuencia.Jueves });
            AgregarVuelo(vuelo28);

            Vuelo vuelo29 = new Vuelo("VV029", _rutas[28], _aviones[0], new List<Frecuencia> { Frecuencia.Martes, Frecuencia.Jueves });
            AgregarVuelo(vuelo29);

            Vuelo vuelo30 = new Vuelo("VV030", _rutas[29], _aviones[1], new List<Frecuencia> { Frecuencia.Miercoles, Frecuencia.Viernes });
            AgregarVuelo(vuelo30);
        }
        private void PrecargarPasajes()
        {
            // Pasaje 1
            Pasaje pasaje1 = new Pasaje(_vuelos[1],new DateTime(2025, 5, 1),(Cliente)_usuarios[2],Equipaje.CABINA,2500m);
            AgregarPasaje(pasaje1);

            // Pasaje 2
            Pasaje pasaje2 = new Pasaje(
                _vuelos[0],
                new DateTime(2025, 6, 6),
                (Cliente)_usuarios[3],  
                Equipaje.CABINA,
                3000m
            );

            // Pasaje 3
            Pasaje pasaje3 = new Pasaje(
                _vuelos[2],
                new DateTime(2025, 7, 4),
                (Cliente)_usuarios[4],  
                Equipaje.CABINA,
                2800m
            );

            // Pasaje 4
            Pasaje pasaje4 = new Pasaje(
                _vuelos[3],
                new DateTime(2025, 8, 7),
                (Cliente)_usuarios[5],  
                Equipaje.CABINA,
                3300m
            );

            // Pasaje 5
            Pasaje pasaje5 = new Pasaje(
                _vuelos[4],
                new DateTime(2025, 9, 5),
                (Cliente)_usuarios[6],  
                Equipaje.CABINA,
                2700m
            );

            // Pasaje 6
            Pasaje pasaje6 = new Pasaje(
                _vuelos[0],
                new DateTime(2025, 5, 9),
                (Cliente)_usuarios[7],  
                Equipaje.CABINA,
                1500m
            );

            // Pasaje 7
            Pasaje pasaje7 = new Pasaje(
                _vuelos[1],
                new DateTime(2025, 6, 5),
                (Cliente)_usuarios[8],  
                Equipaje.CABINA,
                1200m
            );

            // Pasaje 8
            Pasaje pasaje8 = new Pasaje(
                _vuelos[2],
                new DateTime(2025, 7, 11),
                (Cliente)_usuarios[9],  
                Equipaje.CABINA,
                1800m
            );

            // Pasaje 9
            Pasaje pasaje9 = new Pasaje(
                _vuelos[3],
                new DateTime(2025, 8, 14),
                (Cliente)_usuarios[10], 
                Equipaje.CABINA,
                1600m
            );

            // Pasaje 10
            Pasaje pasaje10 = new Pasaje(
                _vuelos[4],
                new DateTime(2025, 9, 26),
                (Cliente)_usuarios[11], 
                Equipaje.CABINA,
                1400m
            );

            // Pasaje 11
            Pasaje pasaje11 = new Pasaje(
                _vuelos[1],
                new DateTime(2025, 5, 15),
                (Cliente)_usuarios[3], 
                Equipaje.CABINA,
                2600m
            );

            // Pasaje 12
            Pasaje pasaje12 = new Pasaje(
                _vuelos[0],
                new DateTime(2025, 6, 20),
                (Cliente)_usuarios[4], 
                Equipaje.CABINA,
                3200m
            );

            // Pasaje 13
            Pasaje pasaje13 = new Pasaje(
                _vuelos[2],
                new DateTime(2025, 10, 3),
                (Cliente)_usuarios[5],  
                Equipaje.CABINA,
                3100m
            );

            // Pasaje 14
            Pasaje pasaje14 = new Pasaje(
                _vuelos[3],
                new DateTime(2025, 8, 21),
                (Cliente)_usuarios[6], 
                Equipaje.CABINA,
                3400m
            );

            // Pasaje 15
            Pasaje pasaje15 = new Pasaje(
                _vuelos[4],
                new DateTime(2025, 9, 19),
                (Cliente)_usuarios[6],  
                Equipaje.CABINA,
                3000m
            );

            // Pasaje 16
            Pasaje pasaje16 = new Pasaje(
                _vuelos[0],
                new DateTime(2025, 5, 23),
                (Cliente)_usuarios[7],  
                Equipaje.CABINA,
                1400m
            );

            // Pasaje 17
            Pasaje pasaje17 = new Pasaje(
                _vuelos[1],
                new DateTime(2025, 10, 2),
                (Cliente)_usuarios[8], 
                Equipaje.CABINA,
                1700m
            );

            // Pasaje 18
            Pasaje pasaje18 = new Pasaje(
                _vuelos[2],
                new DateTime(2025, 11, 7),
                (Cliente)_usuarios[9],  
                Equipaje.CABINA,
                1900m
            );

            // Pasaje 19
            Pasaje pasaje19 = new Pasaje(
                _vuelos[3],
                new DateTime(2025, 8, 14),
                (Cliente)_usuarios[10],  
                Equipaje.CABINA,
                1600m
            );

            // Pasaje 20
            Pasaje pasaje20 = new Pasaje(
                _vuelos[4],
                new DateTime(2025, 9, 12),
                (Cliente)_usuarios[11],  
                Equipaje.CABINA,
                1500m
            );

            // Pasaje 21
            Pasaje pasaje21 = new Pasaje(
                _vuelos[0],
                new DateTime(2025, 8, 1),
                (Cliente)_usuarios[4],  
                Equipaje.CABINA,
                3300m
            );

            // Pasaje 22
            Pasaje pasaje22 = new Pasaje(
                _vuelos[1],
                new DateTime(2025, 7, 3),
                (Cliente)_usuarios[3],  
                Equipaje.CABINA,
                3200m
            );

            // Pasaje 23
            Pasaje pasaje23 = new Pasaje(
                _vuelos[2],
                new DateTime(2025, 8, 8),
                (Cliente)_usuarios[4],  
                Equipaje.CABINA,
                3000m
            );

            // Pasaje 24
            Pasaje pasaje24 = new Pasaje(
                _vuelos[3],
                new DateTime(2025, 8, 28),
                (Cliente)_usuarios[5],  
                Equipaje.CABINA,
                3500m
            );

            // Pasaje 25
            Pasaje pasaje25 = new Pasaje(
                _vuelos[4],
                new DateTime(2025, 8, 29),
                (Cliente)_usuarios[6],  
                Equipaje.CABINA,
                3000m
            );

            // Agregar los pasajes a la lista o lo que necesites hacer con ellos
            
            AgregarPasaje(pasaje2);
            AgregarPasaje(pasaje3);
            AgregarPasaje(pasaje4);
            AgregarPasaje(pasaje5);
            AgregarPasaje(pasaje6);
            AgregarPasaje(pasaje7);
            AgregarPasaje(pasaje8);
            AgregarPasaje(pasaje9);
            AgregarPasaje(pasaje10);
            AgregarPasaje(pasaje11);
            AgregarPasaje(pasaje12);
            AgregarPasaje(pasaje13);
            AgregarPasaje(pasaje14);
            AgregarPasaje(pasaje15);
            AgregarPasaje(pasaje16);
            AgregarPasaje(pasaje17);
            AgregarPasaje(pasaje18);
            AgregarPasaje(pasaje19);
            AgregarPasaje(pasaje20);
            AgregarPasaje(pasaje21);
            AgregarPasaje(pasaje22);
            AgregarPasaje(pasaje23);
            AgregarPasaje(pasaje24);
            AgregarPasaje(pasaje25);
        }

        //metodo que ordena un pasaje por fecha
        public List<Pasaje> OrdenadosPorFecha()
        {
            List<Pasaje> lista = Pasajes;
            lista.Sort();
            return lista;
        }

        //lista que ordena pasajes por precio
        public List<Pasaje> ListaPasajeUsuario(string email)
        {
            List<Pasaje> lista = new List<Pasaje>();
            foreach(Pasaje p in _pasajes)
            {
                if (p.DevolverEmail() == email)
                {
                    lista.Add(p);
                }
            }
            lista.Sort(new listaPasajeOrdenadaPorPrecio());
            return lista;
        }


    }
}
