using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Avion:IValidable
    {
        //properties
        private string _fabricante;
        private string _modelo;
        private int _cantidadAsientos;
        private int _alcance;
        private decimal _costoKm;

        //get y set 
        public string Fabricante
        {
            get { return _fabricante; }
            set { _fabricante = value; }
        }
        //get y set
        public string Modelo
        {
            get { return _modelo; }
            set { _modelo = value; }
        }
        //get y set
        public int CantidadAsientos
        {
            get { return _cantidadAsientos; }
            set { _cantidadAsientos = value; }
        }
        //get y set
        public int Alcance
        {
            get { return _alcance; }
            set { _alcance = value; }
        }
        //get y set
        public decimal CostoKm
        {
            get { return _costoKm; }
            set { _costoKm = value; }
        }

        //constructor de avion
          public Avion(string fabricante, string modelo, int cantidadAsientos, int alcance, decimal costoKm)
          {
            this._fabricante = fabricante;
            this._modelo = modelo;
            this._cantidadAsientos = cantidadAsientos;
            this._alcance = alcance;
            this._costoKm = costoKm;
          }

        //metodo validar que llama a los metodos que validan las properties
        public void Validar()
        {
            ValidarFabricante();
            ValidarModelo();
            ValidarCantidadAsientos();
            ValidarAlcance();
            ValidarCostoKM();
        }
        //metodo que valida fabricante
        private void ValidarFabricante()
        {
            if (string.IsNullOrEmpty(_fabricante))
            {
                throw new Exception("Debes ingresar un fabricante");
            }
        }
        //metodo que valida modelo
        private void ValidarModelo()
        {
            if (string.IsNullOrEmpty(_modelo))
            {
                throw new Exception("Debes ingresar un modelo");
            }
        }
        //metodo validar cantidad de asientos
        private void ValidarCantidadAsientos()
        {
            if (_cantidadAsientos < 0)
            {
                throw new Exception("La cantidad de asientos debe ser mayor a 0");
            }
        }
        //metodo validar alcance
        private void ValidarAlcance()
        {
            if (_alcance < 0)
            {
                throw new Exception("El alcance debe ser mayor a 0");
            }
        }
        //metodo validar costo km
        private void ValidarCostoKM()
        {
            if (_costoKm < 0)
            {
                throw new Exception("El costo por KM debe ser mayor a 0");
            }
        }

        //equal por el contains en sistema
        public override bool Equals(object? obj)
        {
            Avion av = obj as Avion;
            return av != null && _fabricante == av.Fabricante || _modelo==av.Modelo;
            
        }  


    }
}
