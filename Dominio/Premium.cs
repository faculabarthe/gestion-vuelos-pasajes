using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Premium:Cliente
    {
        //properties
        private int _puntos;
        //get y set
        public int Puntos
        {
            get { return _puntos; }
            set { _puntos = value; }
        }
        //constructor premium
        public Premium(int puntos,string documento,string nombre,string nacionalidad,string email, string contrasenia)
            : base(documento, nombre , nacionalidad, email ,  contrasenia)
        {
            this._puntos = puntos;
        }
        //metodo validar que llama al del padre y a otro metodo que valida los puntos
        public override void Validar()
        {
            base.Validar();
            ValidarPuntos();
        }
        //metodo que valida los puntos
        private void ValidarPuntos()
        {
            if (_puntos < 0)
            {
                throw new Exception("Los puntos deben ser mayor a 0");
            }
        }
        //metodo to string para que se vea los puntos
        public override string ToString()
        {
            return base.ToString() + $"Puntos:{_puntos} \n";
        }

        //metodo equals que es por el contains en sistema 
        public override bool Equals(object? obj)
        {
        return base.Equals(obj);
        }

        //para poder cambiar los puntos
        public void CambiarPuntos(int puntos)
        {
                _puntos = puntos;
           
        }




    }
}
