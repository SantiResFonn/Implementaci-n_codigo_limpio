using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1
{

    class Program
    {

        public Restaurante Cargar_Archivo()
        {
            using (FileStream fs = new FileStream("Restaurante.dat", FileMode.Open))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                Restaurante restaurante = (Restaurante)formatter.Deserialize(fs);
                return restaurante;
            }
        }

        public void Iniciar()
        {
            Restaurante miRestaurante = new Restaurante();

            Gestor gestor = new Gestor
            {
                Restaurante = this.Cargar_Archivo()
            };

            gestor.Identificar_usuario();
        }
    }


}
