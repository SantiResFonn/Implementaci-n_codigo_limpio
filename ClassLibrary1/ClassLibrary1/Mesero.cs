

using System.Collections.Generic;
using System.Linq;

namespace ClassLibrary1
{
    public class Mesero
    {
        public string Nombre { get; set; }
        public float Propinas { get; set; }
        public List<int> Pedidos_tomados { get; set; }
        public Mesero(string nombre) {
            Nombre = nombre;
            Propinas = 0;
            Pedidos_tomados = new List<int>();
        }

        public Pedido Crear_pedido(List<string> productos_pedido, int numero_mesa)
        {
            Pedido nuevo_pedido = new Pedido(productos_pedido, numero_mesa);
            Pedidos_tomados.Add(nuevo_pedido.Id);
            return nuevo_pedido;
        }

    }
}
