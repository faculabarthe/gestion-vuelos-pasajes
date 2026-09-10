using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Ocacional:Cliente
    {
        //properties
        private bool _elejible;

        //get y set
        public bool Elejible
        {
            get { return _elejible; }
            set {  _elejible = value; }
           
        }

        //constructor de cliente ocacional
        public Ocacional(string documento, string nombre, string nacionalidad, string email, string contrasenia)
            : base(documento, nombre, nacionalidad, email, contrasenia)
        {
            this._elejible = ObtenerValorAleatorio();
        }
        public Ocacional() { }

        //creo el numero aleatorio
        public bool ObtenerValorAleatorio()
        {
            Random random = new Random();
            return random.Next(0, 2) == 0; // numero aleatorio entre 0 y 1, y devuelve true si es 0, false si es 1
        }

        //metodo validar que llama al del padre
        public override void Validar()
        {
            base.Validar();
            
        }
        //metodo para que eselejible se vea con si o no 
        public string EsElejible()
        {
            string mensaje;

            if(_elejible == false)
            {
                mensaje = "NO";
            }
            else
            {
                mensaje = "SI";
            }

            return mensaje;
        }

        //metodo to string 
        public override string ToString()
        {
            return base.ToString()  + $"Es elejible:{EsElejible()} \n";
        }
        //equals por el contains de sistema
        public override bool Equals(object? obj)
        {
            return base.Equals(obj);
        }

        //para el metodo de cambiar elejible
        public void CambiarElejible(bool elejible)
        {
            _elejible = elejible;
        }


    }
}
