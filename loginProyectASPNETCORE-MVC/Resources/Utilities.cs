using Microsoft.EntityFrameworkCore.Query.Internal;
using System.Security.Cryptography;
using System.Text;


namespace loginProyectASPNETCORE_MVC.Resources
{
    /*Aquí se crearán los procedimientos requeridos para encriptar la contraseña del usuario
     */
    public class Utilities
    {
        public static string EncryptPassword(string password)
        {
            /*StringBuilder es una clase que permite la concatenación eficiente de cadenas en C#.
             Almacena caracteres en un búfer interno y proporciona métodos para agregar, eliminar e insertar caracteres.
            ¿Por qué usarla?

                Eficiencia: Es más eficiente que la concatenación de cadenas "+" para operaciones repetitivas.
                Menos uso de memoria: Evita la creación de objetos String intermedios.
                Mutable: Permite modificar el contenido de la cadena sin crear nuevos objetos.*/

            StringBuilder sb = new StringBuilder();

            /*SHA-256 es un algoritmo que transforma cualquier cantidad de datos en una cadena fija de 256 bits (32 bytes). Se utiliza para:

                Verificar la integridad de los datos: detectar si se han modificado sin autorización.
                Comparar dos conjuntos de datos: determinar si son iguales o diferentes.
                Crear firmas digitales: garantizar la autenticidad del origen de un mensaje.
                ¿Cómo funciona?

                Toma los datos de entrada y los procesa a través de una serie de pasos matemáticos complejos, generando un hash único e irreversible. */
            using (SHA256 hash = SHA256Managed.Create())
            {
                Encoding en = Encoding.UTF8;
                byte[] result = hash.ComputeHash(en.GetBytes(password));
                foreach (byte b in result)
                {
                    //Nota, investigar el uso del "x2" en ToString.
                    sb.Append(b.ToString("x2"));
                }
            }
            return sb.ToString();
        }
    }
}
