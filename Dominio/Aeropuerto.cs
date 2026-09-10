using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Aeropuerto:IValidable
    {
        //properties
        private string _codigoIATA;
        private string _ciudad;
        private decimal _costoOperaciones;
        private decimal _costoTasa;

        //get y set 
        public string CodigoIATA
        {
            get { return _codigoIATA; }
            set { _codigoIATA = value; }
        }
        //get y set 
        public string Ciudad
        {
            get { return _ciudad; }
            set { _ciudad = value; }
        }
        //get y set 
        public decimal CostoOperaciones
        {
            get { return _costoOperaciones; }
            set { _costoOperaciones = value; }
        }
        //get y set 
        public decimal CostoTasa
        {
            get { return _costoTasa; }
            set { _costoTasa = value; }
        }

        //constructor de aeropuerto
        public Aeropuerto(string codigoIata,string ciudad,decimal costoOperaciones,decimal costoTasa)
        {
            this._codigoIATA = codigoIata.ToUpper();
            this._ciudad = ciudad;
            this._costoOperaciones= costoOperaciones;
            this._costoTasa= costoTasa;
        }

        //metodo que devuelve el codigo iata que es utilizado para devolver los aeropuertos con ese codigo
        public string DevolverCodigoIATA()
        {
            return _codigoIATA;
        }

        //metodo validar , que llama a los otros metodos
        public void Validar()
        {
            ValidarCodigo();
            ValidarCiudad();
            ValidarCostoOperaciones();
            ValidarCostoTasa();
        }

        //metodo validar codigo
        private void ValidarCodigo()
        {
            if (string.IsNullOrEmpty(_codigoIATA) && _codigoIATA.Length == 3)
            {
                throw new Exception("Debes ingresar un codigo IATA con 3 letras");
            }
        }
        //metodo validar ciudad
        private void ValidarCiudad()
        {
            if (string.IsNullOrEmpty(_ciudad))
            {
                throw new Exception("Debes ingresar una ciudad");
            }
        }
        //metodo validar costo operaciones
        private void ValidarCostoOperaciones()
        {
            if (_costoOperaciones < 0)
            {
                throw new Exception("El costo tiene que ser mayor a 0");
            }
        }
        //metodo validar costo tasa
        private void ValidarCostoTasa()
        {
            if (_costoTasa < 0)
            {
                throw new Exception("El costo tiene que ser mayor a 0");
            }
        }
        //metodo equals por el contains utilizado en sistema
        public override bool Equals(object? obj)
        {
            Aeropuerto a = obj as Aeropuerto;
            return a != null && _codigoIATA == a._codigoIATA;
        }

        

    }
}
