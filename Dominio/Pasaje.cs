using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Pasaje : IValidable, IComparable<Pasaje>
    {
        //properties
        private static int s_ultimoId = 1;
        private int _id;
        private Vuelo _vuelo;
        private DateTime _fecha;
        private Cliente _pasajeroComprador;
        private Equipaje _equipaje;
        private decimal _precio;

        //get y set
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }
        //get y set
        public Vuelo Vuelo
        {
            get { return _vuelo; }
            set { _vuelo = value; }
        }
        //get y set
        public DateTime Fecha
        {
            get { return _fecha; }
            set { _fecha = value; }
        }
        // get y set
        public Cliente PasajeroComprador
        {
            get { return _pasajeroComprador; }
            set { _pasajeroComprador = value; }
        }
        //get y set
        public Equipaje Equipaje
        {
            get { return _equipaje; }
            set { _equipaje = value; }
        }
        //get y set
        public decimal Precio
        {
            get { return _precio; }
            set { _precio = value; }
        }
        //constructor de pasaje
        public Pasaje(Vuelo vuelo, DateTime fecha, Cliente cliente, Equipaje equipaje, decimal precio)
        {
            _id = s_ultimoId++;
            this._vuelo = vuelo;
            this._fecha = fecha;
            this._pasajeroComprador = cliente;
            this._equipaje = equipaje;
            this._precio = precio;
        }
        //metodo validar que llama al metodo privado de validar precio
        public void Validar()
        {
            ValidarPrecio();
            ValidarFecha();
        }
        //metodo validar de el precio
        public void ValidarPrecio()
        {
            if (_precio < 0)
            {
                throw new Exception("El precio debe ser mayor a 0");
            }
        }

        //metodo de validar fecha para que la fecha del pasaje coincida con la frecuencia del vuelo
        private void ValidarFecha()
        {

            bool coincide = false;
            int numeroDia = (int)_fecha.DayOfWeek;
            foreach (Frecuencia f in _vuelo.Frecuencias)
            {
                if ((int)f == numeroDia)
                {
                    coincide = true;
                }
            }

            if (coincide == false)
            {
                throw new Exception("La fecha del pasaje no coincide con la frecuencia del vuelo");
            }
        }

        //equals ya que usamos el contains en sistema
        public override bool Equals(object? obj)
        {
            Pasaje pa = obj as Pasaje;
            return pa != null && _id == pa.Id;
        }
        //metodo to string para que muestre el mensajje con lo que qurremos
        public override string ToString()
        {
            return  $"Id pasaje:{_id} \n Nombre pasajero:{_pasajeroComprador.Nombre} \n Precio pasje:{_precio} \n Fecha:{_fecha} \n Numero de vuelo:{_vuelo.NumeroVuelo} \n";
        }

        //calculo precio pasaje
        public decimal CalcularPrecioPasaje()
        {
            decimal precioPasaje = 0;

            precioPasaje = (_vuelo.CostoPorAcientoDeLosVuelos() * 1.25m);

            if (_equipaje == Equipaje.CABINA && _pasajeroComprador is Ocacional)
            {
                precioPasaje = precioPasaje * 1.10m;
            }else 
            if (_equipaje == Equipaje.BODEGA && _pasajeroComprador is Ocacional)
            {
                precioPasaje = precioPasaje * 1.20m;
            } else
            if (_equipaje == Equipaje.BODEGA && _pasajeroComprador is Premium)
            {
                precioPasaje = precioPasaje * 1.05m;
            }

            precioPasaje = precioPasaje+ _vuelo.CostoTasaAeropuertoLLegada() + _vuelo.CostoTasaAeropuertoSalida();

            decimal precioPasajeFinal = Math.Round(precioPasaje, 2);
            return precioPasajeFinal;

        }

        //compare to 
        public int CompareTo(Pasaje? other)
        {
            return _fecha.CompareTo(other.Fecha);
        }

        //devolver nombre
        public string DevolverNombre()
        {
            return _pasajeroComprador.Nombre;
        }
        //devolver nuero vuelo
        public string DevolverNumeroVuelo()
        {
            return _vuelo.NumeroVuelo;
        }

        //devolver email
        public string DevolverEmail()
        {
            return _pasajeroComprador.Email;
        }

    }
}
