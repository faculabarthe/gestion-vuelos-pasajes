using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Vuelo:IValidable
    {
        //properties
        private string _numeroVuelo;
        private Ruta _ruta;
        private Avion _avion;
        private List<Frecuencia> _frecuencias=new List<Frecuencia>();

        //get y set
        public string NumeroVuelo
        {
            get { return _numeroVuelo; }
            set { _numeroVuelo = value; }
        }
        //get y set
        public Ruta Ruta
        {
            get { return _ruta; }
            set { _ruta = value; }
        }
        //get y set
        public Avion Avion
        {
            get { return _avion; }
            set { _avion = value; }
        }
        //get y set
        public List<Frecuencia> Frecuencias
        {
            get { return new List<Frecuencia>(_frecuencias); }
            
        }

        //constructor
        public Vuelo(string numeroVuelo, Ruta ruta, Avion avion, List<Frecuencia> frecuencias)
        {
            this._numeroVuelo = numeroVuelo;
            this._ruta = ruta;
            this._avion = avion;
            this._frecuencias = frecuencias;
        }
        //metodo par devolver el codigo iata es para el metodo de listar vuelos segun aeropuerto  
        public bool DevolverCodigoIATA(string codigo)
        {
            return _ruta.ContieneCodigoIata(codigo);
        }

        //metodo validar  que adentro llama a los otros validar
        public void Validar()
        {
            ValidarNumeroVuelo();
            AlcanceAvion();
        }

        //metodo
        //que el avion tenga el aclcance para cubrir la distancia de la ruta 
        private bool AlcanceAvion()
        {
            if (_ruta.Distancia > _avion.Alcance)
            {
                throw new Exception("El avion no tiene el alcance necesario");
            }
            return true;
        }

       


        //validar numero de vuelo
        private void ValidarNumeroVuelo()
        {
            if (string.IsNullOrEmpty(_numeroVuelo))
            {
                throw new Exception("El numero de vuelo no puede esar vacio");
            }


            int contadorPalabras = 0;
            int contadorNumeros = 0;

            int[] numeros = { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

            char[] alfabeto = "abcdefghijklmnopqrstuvwxyz".ToCharArray();

            foreach (char letra in alfabeto)
            {
                foreach (char c in _numeroVuelo)
                {
                    if (c == letra)
                    {
                        contadorPalabras++;
                    }
                }
            }

            foreach (int numero in numeros)
            {
                foreach (int n in _numeroVuelo)
                {
                    if (n == numero)
                    {
                        contadorNumeros++;
                    }
                }
            }

            if (contadorPalabras != 2)
            {
                throw new Exception("El numero de vuelo debe tener al menos 2 letras");
            }

            if (contadorNumeros < 0 && contadorNumeros < 4)
            {
                throw new Exception("El numero de vuelo debe tener entre 1 y 4 numeros");
            }

        }

        //equals por el contains llamado en 
        public override bool Equals(object? obj)
        {
            Vuelo vu = obj as Vuelo;
            return vu != null && _numeroVuelo == vu.NumeroVuelo;
        }

       //to string
        public override string ToString()
        {
            string frecuenciasTexto = string.Join(", ", _frecuencias);

            return $"Numero vuelo: {_numeroVuelo}\nFrecuencia: {frecuenciasTexto}\nAvion: {_avion.Modelo}\nRuta: {_ruta.DevolverAeropuertos()} \n";
        }

        //Costo por aciento
        public decimal CostoPorAcientoDeLosVuelos()
        {
            decimal costo = 0;
            costo = (_avion.CostoKm * _ruta.Distancia + _ruta.CostoOperacionesAeropuertoLLegada() + _ruta.CostoOperacionesAeropuertoSalida()) / _avion.CantidadAsientos;
            return costo;

        }

        ////para el calculo 
        public decimal CostoTasaAeropuertoLLegada()
        {
            decimal costo = 0;

            costo = _ruta.CostoTasaAeropuertoLLegada();

            return costo;
        }
        //para el calculo 
        public decimal CostoTasaAeropuertoSalida()
        {
            decimal costo = 0;

            costo = _ruta.CostoTasaAeropuertosSalida();

            return costo;
        }
        //devolver ruta
        public string DevolverRuta()
        {
            return _ruta.DevolverAeropuertos();
        }
        //devolver avion
        public string DevolverAvion()
        {
            return _avion.Modelo;
        }

    }
}
