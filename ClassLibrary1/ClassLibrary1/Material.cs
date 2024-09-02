using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1
{
    public class Material
    {
        public string Nombre { get; set; }
        public int Cantidad { get; set; }
        public Material(string nombre , int cantidad) {
            Nombre = nombre;
            Cantidad = cantidad;
        }
    }
}
