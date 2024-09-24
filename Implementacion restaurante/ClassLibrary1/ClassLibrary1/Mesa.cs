using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1
{
    public class Mesa
    {
        public int numero_mesa = 0;
        public int Capacidad;
        public bool disponibilidad = true;
        
        public Mesa(int capacidad) {
            numero_mesa += 1;
            Capacidad = capacidad;
        }
    }
}
