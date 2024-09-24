using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace ClassLibrary1
{
    public class Gestor
    {
        public Restaurante Restaurante { get; set; }

        public void GuardarArchivo()
        {
            try
            {
                using (FileStream archivo = new FileStream("Restaurante.dat", FileMode.OpenOrCreate))
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    formatter.Serialize(archivo, Restaurante);
                }
            }
            catch (IOException ex)
            {
                Console.WriteLine($"Error al guardar el archivo: {ex.Message}");
            }
        }

        public Restaurante CargarArchivo()
        {
            try
            {
                using (FileStream archivo = new FileStream("Restaurante.dat", FileMode.Open))
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    Restaurante = (Restaurante)formatter.Deserialize(archivo);
                    return Restaurante;
                }
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("Archivo no encontrado. Se utilizará una instancia nueva.");
                return new Restaurante();
            }
            catch (IOException ex)
            {
                Console.WriteLine($"Error al cargar el archivo: {ex.Message}");
                return null;
            }
        }

        private string LeerEntrada(string mensaje)
        {
            Console.WriteLine(mensaje);
            return Console.ReadLine();
        }

        private int LeerEntero(string mensaje)
        {
            while (true)
            {
                try
                {
                    Console.WriteLine(mensaje);
                    return int.Parse(Console.ReadLine());
                }
                catch (FormatException)
                {
                    Console.WriteLine("Entrada inválida, por favor ingrese un número.");
                }
            }
        }

        private void MostrarOpcionesMenu(string[] opciones)
        {
            for (int i = 0; i < opciones.Length; i++)
            {
                Console.WriteLine($"{i + 1}. {opciones[i]}");
            }
        }

        public void GestionAdministrador()
        {
            string[] opciones = {
                "Buscar mesero",
                "Visualizar mesas",
                "Agregar mesa nueva",
                "Agregar producto al menú",
                "Eliminar producto del menú",
                "Agregar nuevo material al inventario",
                "Eliminar material del inventario",
                "Agregar cantidad a un material del inventario",
                "Eliminar cantidad de un material del inventario"
            };

            MostrarOpcionesMenu(opciones);

            int opcion = LeerEntero("Seleccione una opción:");
            switch (opcion)
            {
                case 1:
                    BuscarMesero();
                    break;
                case 2:
                    VisualizarMesas();
                    break;
                case 3:
                    AgregarMesa();
                    break;
                case 4:
                    AgregarProducto();
                    break;
                case 5:
                    EliminarProducto();
                    break;
                case 6:
                    AgregarMaterial();
                    break;
                case 7:
                    EliminarMaterial();
                    break;
                case 8:
                    AgregarCantidadMaterial();
                    break;
                case 9:
                    EliminarCantidadMaterial();
                    break;
                default:
                    Console.WriteLine("Opción inválida.");
                    break;
            }
        }

        private void BuscarMesero()
        {
            string nombre_mesero = LeerEntrada("Escriba el nombre del mesero que quiere encontrar:");
            string encontrado = Restaurante.Buscar_mesero(nombre_mesero);
            Console.WriteLine($"Mesero encontrado: {encontrado}");
            GuardarArchivo();
        }

        private void VisualizarMesas()
        {
            string mesas = Restaurante.Visualizar_mesas();
            Console.WriteLine($"Mesas del restaurante: {mesas}");
        }

        private void AgregarMesa()
        {
            int capacidad = LeerEntero("Introduzca la capacidad de la nueva mesa:");
            Restaurante.Agregar_mesa(capacidad);
            Console.WriteLine("Mesa agregada con éxito.");
            GuardarArchivo();
        }

        private void AgregarProducto()
        {
            string nombre = LeerEntrada("Introduzca el nombre del nuevo producto:");
            float precio = float.Parse(LeerEntrada("Introduzca el valor del nuevo producto:"));
            string materiales = LeerEntrada("Introduzca la lista de materiales del producto (separados por coma):");
            List<string> listaMateriales = new List<string>(materiales.Split(','));

            Restaurante.Menu_restaurante.Agregar_producto(nombre, precio, listaMateriales);
            Console.WriteLine("Producto agregado con éxito.");
            GuardarArchivo();
        }

        private void EliminarProducto()
        {
            string nombre = LeerEntrada("Introduzca el nombre del producto a eliminar:");
            Restaurante.Menu_restaurante.Eliminar_producto(nombre);
            Console.WriteLine("Producto eliminado con éxito.");
            GuardarArchivo();
        }

        private void AgregarMaterial()
        {
            string nombre = LeerEntrada("Introduzca el nombre del nuevo material:");
            int cantidad = LeerEntero("Introduzca la cantidad a agregar:");
            Restaurante.Inventario.Agregar_material(nombre, cantidad);
            Console.WriteLine("Material agregado con éxito.");
            GuardarArchivo();
        }

        private void EliminarMaterial()
        {
            string nombre = LeerEntrada("Introduzca el nombre del material a eliminar:");
            Restaurante.Inventario.Eliminar_material(nombre);
            Console.WriteLine("Material eliminado con éxito.");
            GuardarArchivo();
        }

        private void AgregarCantidadMaterial()
        {
            string nombre = LeerEntrada("Introduzca el nombre del material:");
            int cantidad = LeerEntero("Introduzca la cantidad a agregar:");
            Restaurante.Inventario.Agregar_cantidad_material(nombre, cantidad);
            Console.WriteLine("Cantidad agregada con éxito.");
            GuardarArchivo();
        }

        private void EliminarCantidadMaterial()
        {
            string nombre = LeerEntrada("Introduzca el nombre del material:");
            int cantidad = LeerEntero("Introduzca la cantidad a eliminar:");
            Restaurante.Inventario.Eliminar_cantidad_material(nombre, cantidad);
            Console.WriteLine("Cantidad eliminada con éxito.");
            GuardarArchivo();
        }

        public void GestionMesero(string nombre_mesero)
        {
            string[] opciones = { "Crear pedido", "Liquidar pedido", "Agregar producto a pedido", "Eliminar producto de pedido" };
            MostrarOpcionesMenu(opciones);

            int opcion = LeerEntero("Seleccione una opción:");
            switch (opcion)
            {
                case 1:
                    CrearPedido(nombre_mesero);
                    break;
                case 2:
                    LiquidarPedido();
                    break;
                case 3:
                    AgregarProductoPedido();
                    break;
                case 4:
                    EliminarProductoPedido();
                    break;
                default:
                    Console.WriteLine("Opción inválida.");
                    break;
            }
        }

        private void CrearPedido(string nombre_mesero)
        {
            int numero_mesa = LeerEntero("Introduzca el número de mesa del pedido:");
            string productos = LeerEntrada("Introduzca la lista de productos (separados por coma):");
            List<string> lista_productos = new List<string>(productos.Split(','));

            Restaurante.Crear_pedido(nombre_mesero, lista_productos, numero_mesa);
            Console.WriteLine("Pedido creado con éxito.");
            GuardarArchivo();
        }

        private void LiquidarPedido()
        {
            int numero_pedido = LeerEntero("Introduzca el número del pedido a liquidar:");
            float valor_pedido = Restaurante.Liquidar_pedido(numero_pedido);
            Console.WriteLine($"El valor total del pedido es: {valor_pedido}");
            GuardarArchivo();
        }

        private void AgregarProductoPedido()
        {
            int numero_pedido = LeerEntero("Introduzca el número del pedido al que desea agregar un producto:");
            string producto = LeerEntrada("Introduzca el nombre del producto:");
            Restaurante.Agregar_producto_pedido(numero_pedido, producto);
            Console.WriteLine("Producto agregado con éxito.");
            GuardarArchivo();
        }

        private void EliminarProductoPedido()
        {
            int numero_pedido = LeerEntero("Introduzca el número del pedido al que desea eliminar un producto:");
            string producto = LeerEntrada("Introduzca el nombre del producto:");
            Restaurante.Eliminar_producto_pedido(numero_pedido, producto);
            Console.WriteLine("Producto eliminado con éxito.");
            GuardarArchivo();
        }

        public void IdentificarMesero()
        {
            string nombre_mesero = LeerEntrada("Por favor, introduzca su nombre:");
            Mesero mesero = Restaurante.Meseros.Find(m => m.Nombre == nombre_mesero);

            if (mesero != null)
            {
                Console.WriteLine($"Bienvenido, {nombre_mesero}");
                GestionMesero(nombre_mesero);
            }
            else
            {
                Console.WriteLine("El nombre no corresponde a ningún trabajador.");
                IdentificarMesero();
            }
        }

        public void IdentificarAdministrador()
        {
            string contraseña = LeerEntrada("Introduzca la contraseña de administrador:");
            if (contraseña == Restaurante.Administrador)
            {
                Console.WriteLine("Acceso concedido.");
                GestionAdministrador();
            }
            else
            {
                Console.WriteLine("Contraseña incorrecta.");
                IdentificarAdministrador();
            }
        }

        public void IdentificarUsuario()
        {
            string[] opciones = { "Administrador", "Mesero" };
            MostrarOpcionesMenu(opciones);

            int tipo_usuario = LeerEntero("Seleccione su tipo de usuario:");
            switch (tipo_usuario)
            {
                case 1:
                    IdentificarAdministrador();
                    break;
                case 2:
                    IdentificarMesero();
                    break;
                default:
                    Console.WriteLine("Opción inválida.");
                    break;
            }
        }
    }
}
