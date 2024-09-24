using System;
using System.Collections.Generic;
using System.Text;

namespace ClassLibrary1
{
    public class Restaurante
    {
        public List<Mesero> Meseros { get; set; }
        public Menu MenuRestaurante { get; set; }
        public string Administrador { get; set; } = "12345";
        public Inventario Inventario { get; set; }
        public List<Pedido> Pedidos { get; set; }
        public List<Mesa> Mesas { get; set; }

        public void CrearPedido(string nombreMesero, List<string> productos, int numeroMesa)
        {
            Mesero mesero = BuscarMeseroPorNombre(nombreMesero);
            if (mesero != null)
            {
                Pedido pedido = mesero.Crear_pedido(productos, numeroMesa);
                Mesa mesa = BuscarMesaPorNumero(numeroMesa);
                if (mesa != null)
                {
                    mesa.disponibilidad = false;
                }
                Pedidos.Add(pedido);
            }
            else
            {
                Console.WriteLine($"Mesero {nombreMesero} no encontrado.");
            }
        }

        public float LiquidarPedido(int idPedido)
        {
            Pedido pedido = BuscarPedidoPorId(idPedido);
            if (pedido == null)
            {
                Console.WriteLine($"Pedido {idPedido} no encontrado.");
                return 0;
            }

            float valorTotal = CalcularValorTotalPedido(pedido);

            Mesa mesa = BuscarMesaPorNumero(pedido.Numero_mesa);
            if (mesa != null)
            {
                mesa.disponibilidad = true;
            }

            Mesero mesero = BuscarMeseroPorPedido(idPedido);
            if (mesero != null)
            {
                mesero.Propinas += valorTotal / 10; // 10% de propina
            }

            return valorTotal;
        }

        public void AgregarProductoAPedido(int idPedido, string nombreProducto)
        {
            Pedido pedido = BuscarPedidoPorId(idPedido);
            if (pedido != null)
            {
                pedido.Productos_pedido.Add(nombreProducto);
            }
            else
            {
                Console.WriteLine($"Pedido {idPedido} no encontrado.");
            }
        }

        public void EliminarProductoDePedido(int idPedido, string nombreProducto)
        {
            Pedido pedido = BuscarPedidoPorId(idPedido);
            if (pedido != null)
            {
                if (pedido.Productos_pedido.Contains(nombreProducto))
                {
                    pedido.Productos_pedido.Remove(nombreProducto);
                }
                else
                {
                    Console.WriteLine($"Producto {nombreProducto} no encontrado en el pedido.");
                }
            }
            else
            {
                Console.WriteLine($"Pedido {idPedido} no encontrado.");
            }
        }

        public Mesero BuscarMeseroPorNombre(string nombreMesero)
        {
            return Meseros?.Find(m => m.Nombre == nombreMesero);
        }

        public Pedido BuscarPedidoPorId(int idPedido)
        {
            return Pedidos?.Find(p => p.Id == idPedido);
        }

        public Mesero BuscarMeseroPorPedido(int idPedido)
        {
            foreach (Mesero mesero in Meseros)
            {
                if (mesero.Pedidos_tomados.Contains(idPedido))
                {
                    return mesero;
                }
            }
            return null;
        }

        public Mesa BuscarMesaPorNumero(int numeroMesa)
        {
            return Mesas?.Find(m => m.numero_mesa == numeroMesa);
        }

        public float CalcularValorTotalPedido(Pedido pedido)
        {
            float valorTotal = 0;
            foreach (string productoNombre in pedido.Productos_pedido)
            {
                Producto producto = MenuRestaurante.Productos.Find(p => p.Nombre == productoNombre);
                if (producto != null)
                {
                    valorTotal += producto.Precio;
                }
            }
            return valorTotal;
        }

        public void AgregarMesa(int capacidad)
        {
            Mesa nuevaMesa = new Mesa(capacidad);
            Mesas.Add(nuevaMesa);
        }

        public string VisualizarMesas()
        {
            StringBuilder sb = new StringBuilder();
            foreach (Mesa mesa in Mesas)
            {
                sb.AppendLine($"Mesa {mesa.numero_mesa} - Capacidad: {mesa.Capacidad}, Disponible: {mesa.disponibilidad}");
            }
            return sb.ToString();
        }
    }
}
