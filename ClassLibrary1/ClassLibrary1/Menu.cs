using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1
{
    public class Menu
    {
        public List<Producto> Productos { get; set; }
        public void Agregar_producto(string nombre_producto, float precio, List<string> materiales)
        {
            Producto producto_nuevo = new Producto(nombre_producto, precio, materiales);
            Productos.Add(producto_nuevo);

        }
        public void Eliminar_producto(string nombre)
        {
            foreach (Producto producto in Productos)
            {
                if (producto.Nombre == nombre)
                {
                    Productos.Remove(producto);
                }
            }
        }

    }
}
