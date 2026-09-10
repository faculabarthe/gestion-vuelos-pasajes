using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Administrador:Usuario
    {
        //properties
        private string _apodo;


        //get y set
        public string Apodo
        {
            get { return _apodo; }
            set { _apodo = value; }
        }

        //constructor
        public Administrador(string apodo,string email, string contrasenia):base( email , contrasenia)
        {
            this._apodo = apodo;
        }

        //metodo validar que llama a los otros validar
        public override void Validar()
        {
            base.Validar();
            ValidarApodo();
        }

        //metodo validar apodo
        private void ValidarApodo()
        {
            if (string.IsNullOrEmpty(_apodo))
            {
                throw new Exception("Debe ingresar apodo");
            }
        }

        //equals por el conteins utilizado en sistema 
        public override bool Equals(object? obj)
        {
            return base.Equals(obj);
        }

    }
}
