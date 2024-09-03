using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1
{
    public class Restaurante
    {
        public List<Mesero> Meseros { get; set; }
        public Menu Menu_restaurante { get; set; }

        public string Administrador = "12345";
        public Inventario Inventario { get; set; }
        public List<Pedido> Pedidos { get; set; }
        public List<Mesa> Mesas { get; set; }

        public void Crear_pedido(string nombre_mesero, List<string> productos, int numero_mesa)
        {
           foreach (Mesero mesero in Meseros)
            {
                if (mesero.Nombre == nombre_mesero)
                {
                    Pedido pedido = mesero.Crear_pedido(productos, numero_mesa);
                    foreach(Mesa mesa in Mesas)
                    {
                        if (mesa.numero_mesa == numero_mesa)
                        {
                            mesa.disponibilidad = false;
                        }
                    }
                    Pedidos.Add(pedido);
                }
            }

        }
        public float Liquidar_pedido(int id_pedido)
        {
            float valor_total = 0;
            foreach (Pedido pedido in Pedidos)
            {
                if( pedido.Id == id_pedido)
                {
                    foreach (string producto in pedido.Productos_pedido)
                    {
                        foreach (Producto producto_menu in Menu_restaurante.Productos)
                        {
                            if (producto_menu.Nombre == producto)
                            {
                                valor_total += producto_menu.Precio;
                            }
                        }
                    }
                    foreach (Mesa mesa in Mesas)
                    {
                        if(mesa.numero_mesa == pedido.Numero_mesa)
                        {
                            mesa.disponibilidad = true;
                        }
                    }
                }

            }
            foreach(Mesero mesero in Meseros)
            {
                foreach(int pedido in mesero.Pedidos_tomados)
                {
                    if(pedido == id_pedido)
                    {
                        mesero.Propinas = mesero.Propinas + (valor_total / 10);
                    }
                }
            }
            return valor_total;
        }

        public void Agregar_producto_pedido(int id_pedido, string nombre_producto)
        {
            foreach (Pedido pedido in Pedidos)
            {
                if (pedido.Id == id_pedido)
                {
                    pedido.Productos_pedido.Add(nombre_producto);
                }

            }
        }
        public void Eliminar_producto_pedido(int id_pedido, string nombre_producto)
        {
            foreach (Pedido pedido in Pedidos)
            {
                if (pedido.Id == id_pedido)
                {
                    foreach (string producto in pedido.Productos_pedido)
                    {
                        if(producto == nombre_producto)
                        {
                            pedido.Productos_pedido.Remove(nombre_producto);
                        }
                    }
                }

            }

        }
        public string Buscar_mesero(string nombre_mesero)
        {
            Mesero mesero_encontrado = null;
            foreach (Mesero mesero in Meseros)
            {
                if(mesero.Nombre == nombre_mesero)
                {
                   mesero_encontrado = mesero;
                }
            }
            return mesero_encontrado.ToString();
        }
        public string Visualizar_mesas()
        {
            return Mesas.ToString();
        }
        public void Agregar_mesa(int capacidad)
        {
            Mesa nueva_mesa = new Mesa(capacidad);
            Mesas.Add(nueva_mesa);

        }
    }
}

