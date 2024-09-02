using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1
{
    public class Inventario
    {
        public List<Material> Materiales { get; set; }
        public void Agregar_material(string nombre_material, int cantidad)
        {
            Material material_nuevo = new Material(nombre_material, cantidad);
            Materiales.Add(material_nuevo);

        }
        public void Eliminar_material(string nombre_material)
        {
            foreach (Material material in Materiales)
            {
                if (material.Nombre == nombre_material)
                {
                    Materiales.Remove(material);
                }
            }
        }
        public void Eliminar_cantidad_material(string nombre_material, int cantidad)
        {
            foreach (Material material in Materiales)
            {
                if (material.Nombre == nombre_material)
                {
                    material.Cantidad = material.Cantidad - cantidad;
                }
            }
        }
        public void Agregar_cantidad_material(string nombre_material, int cantidad)
        {
            foreach (Material material in Materiales)
            {
                if (material.Nombre == nombre_material)
                {
                    material.Cantidad = material.Cantidad + cantidad;
                }
            }
        }
    }
}
