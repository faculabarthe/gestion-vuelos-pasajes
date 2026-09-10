using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class listaPasajeOrdenadaPorPrecio : IComparer<Pasaje>
    {
        public int Compare(Pasaje? x, Pasaje? y)
        {
           return x.CalcularPrecioPasaje().CompareTo(y.CalcularPrecioPasaje());
        }
    }
}
