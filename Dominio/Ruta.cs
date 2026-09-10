using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Ruta:IValidable
    { 
        //properties
        private static int s_ultimoId;
        private int _id;
        private int _distancia;
        private Aeropuerto _aeropuertoLlegada;
        private Aeropuerto _aeropuertoSalida;

        //get y set
        public int Id
        {
            get { return _id; }
            set { _id = value; } 
        }
        //get y set
        public int Distancia
        {
            get { return _distancia; }
            set { _distancia = value; }
        }
        //get y set
        public Aeropuerto AeropuertoLlegada
        {
            get { return _aeropuertoLlegada; }
            set { _aeropuertoLlegada = value; }
        }
        //get y set
        public Aeropuerto AeropuertoSalida
        {
            get { return _aeropuertoSalida; }
            set { _aeropuertoSalida = value; }
        }

        //constructor
        public Ruta(int distancia, Aeropuerto aeropuertoLlegada, Aeropuerto aeropuertoSalida)
        {
            _id = s_ultimoId++;
            this._distancia = distancia;
            this._aeropuertoLlegada = aeropuertoLlegada;
            this._aeropuertoSalida = aeropuertoSalida;
        }

        //metodo que pregunta si lo contiene
        public bool ContieneCodigoIata(string codigo)
        {
            bool retorno = false;
            if (_aeropuertoLlegada.CodigoIATA == codigo || _aeropuertoSalida.CodigoIATA == codigo)
            {
                retorno = true;
            }

            return retorno;
        }

        //metodo validar 
        public void Validar()
        {
            ValidarDistancia();
        }

        //metodo validar distancia
        private void ValidarDistancia()
        {
            if (_distancia < 0)
            {
                throw new Exception("La distancia debe ser mayor a 0"); 
            }
        }
        //equals por el constains en sistema
        public override bool Equals(object? obj)
        {
            Ruta ru = obj as Ruta;
            return ru != null && _id == ru.Id;
        }

        //este metodo me devuelve el codigo iata se salida y el de llegada
       public string DevolverAeropuertos()
        {
            return $"{_aeropuertoSalida.CodigoIATA}-{_aeropuertoLlegada.CodigoIATA}" ;
        }

        //para el calculo 
        public decimal CostoOperacionesAeropuertoLLegada()
        {
            decimal costo = 0;
            costo = _aeropuertoLlegada.CostoOperaciones;
            return costo;
        }
        //para el calculo 
        public decimal CostoOperacionesAeropuertoSalida()
        {
            decimal costo = 0;
            costo = _aeropuertoSalida.CostoOperaciones;
            return costo;
        }

        //para el calculo 
        public decimal CostoTasaAeropuertoLLegada()
        {
            decimal costo = 0;
            costo = _aeropuertoLlegada.CostoTasa;
            return costo;
        }
        //para el calculo 
        public decimal CostoTasaAeropuertosSalida()
        {
            decimal costo = 0;
            costo = _aeropuertoSalida.CostoTasa;
            return costo;
        }
    }
}

