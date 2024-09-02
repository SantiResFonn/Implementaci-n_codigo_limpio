using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1
{
    public class Pedido
    {
        public int Id { get; set; } = 1;

        public float Propina { get; set; }

        public List<string> Productos_pedido { get; set; }

        public int Numero_mesa { get; set; }

   
        public Pedido(List<string> productos, int numero_mesa) {
            Id++;
            Productos_pedido = productos;
            Numero_mesa = numero_mesa;
            Propina = 0;
        }
    }
}
