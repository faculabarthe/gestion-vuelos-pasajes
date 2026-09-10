using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public abstract class Usuario : IValidable
    {
        //properties
        private string _email;
        private string _contrasenia;

        //get y set
        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }
        //get y set
        public string Contrasenia
        {
            get { return _contrasenia; }
            set { _contrasenia = value; }
        }

        //constructor
        public Usuario(string email, string contrasenia)
        {
            this._email = email;
            this._contrasenia = contrasenia;
        }
        public Usuario() { }

        //metodo que valida el email
        private void ValidarEmail()
        {
            if (string.IsNullOrEmpty(_email))
            {
                throw new Exception("No ingresaste email");
            }
        }

        //valida la contrasenia
        private void ValidarContrasenia()
        {
            if (string.IsNullOrEmpty(_contrasenia))
            {
                throw new Exception("Debes ingresar una contrasenia");
            }
        }

       
        //metodo validar
        public virtual void Validar()
        {
            ValidarEmail();
            ValidarContrasenia();
        }

        // metodo equals  por el contains en sistema
        public override bool Equals(object? obj)
        {
            Usuario us= obj as Usuario;
            return us != null && _email == us.Email;
        }
        //metodo to string que retorna lo que queremos
        public override string ToString()
        {
            return $"Email:{_email} \n";
        }

    }
    }
