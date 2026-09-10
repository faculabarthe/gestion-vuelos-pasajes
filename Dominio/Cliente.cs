using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public abstract class Cliente : Usuario
    {
        //propierties de la clase cliente
        private string _documento;
        private string _nombre;
        private string _nacionalidad;

        //get y set de documento
        public string Documento
        {
            get { return _documento; }
            set { _documento = value; }
        }
        //get y set de nombre
        public string Nombre
        {
            get { return _nombre; }
            set { _nombre = value; }
        }
        //get y set de nacionalidad
        public string Nacionalidad
        {
            get { return _nacionalidad; }
            set { _nacionalidad = value; }
        }

        //constructor de cliente
        public Cliente(string documento, string nombre, string nacionalidad, string email, string contrasenia) : base(email, contrasenia)
        {
            this._documento = documento;
            this._nombre = nombre;
            this._nacionalidad = nacionalidad;
        }
        public Cliente() { }

        //metodo validar que llama a los otros metodos que validan las properties`
        public override void Validar()
        {
            base.Validar();
            ValidarDocumento();
            ValidarNombre();
            ValidarNacionalidad();

        }
        //metodo validar documento
        private void ValidarDocumento()
        {
            if (string.IsNullOrEmpty(_documento))
            {
                throw new Exception("Debes ingresar documento");
            }
        }
        //metodo validar nombre 
        private void ValidarNombre()
        {
            if (string.IsNullOrEmpty(_nombre))
            {
                throw new Exception("Debes ingresar nombre");
            }
        }
        //metodo validar nacionalidad
        private void ValidarNacionalidad()
        {
            if (string.IsNullOrEmpty(_nacionalidad))
            {
                throw new Exception("Debes ingresar nacionalidad");
            }
        }
        //metodo ToString para que retorne nombre,documento y nacionalidad
        public override string ToString()
        {
            return $"Nombre:{_nombre} \n" + base.ToString() + $"Documento:{_documento} \n" + $"Nacionalidad:{_nacionalidad} \n";
        }

        //equals porque use el contains en sistema
        public override bool Equals(object? obj)
        {
            Cliente cli = obj as Cliente;
            return cli != null && _documento == cli.Documento;
        }
       
       


    }
}
