using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A886852.Actividad03
{
    public static class Validaciones
    { 
        public static bool ValidarEntero(string numero, ref int Salida)
        {
            bool Flag = false;

            if (!int.TryParse(numero, out Salida))
            {
                Console.WriteLine("   El valor debe ser numérico.");
            }
            else
            {
                Flag = true;
            }
            return Flag;
        }

        public static bool ValidarTexto(string valor, string campo)
        {
            bool Flag = false;

            if (string.IsNullOrEmpty(valor))
            {
                Console.WriteLine("   El campo {0} no debe estar vacío.", campo);
            }
            else
            {
                Flag = true;
            }
            return Flag;
        }

        public static bool ValidarFecha(string fecha, ref DateTime Salida)
        {
            bool Flag = false;

            if (!DateTime.TryParse(fecha, out Salida))
            {
                Console.WriteLine("   No ha ingresado una fecha válida");
            }
            else if(Salida>DateTime.Now)
            {
                Console.WriteLine("   La fecha debe ser menor a la fecha actual");
            }
            else
            {
                Flag = true;
            }
            return Flag;
        }

        public static bool ValidarMonto(string numero, ref decimal Salida)
        {
            bool Flag = false;

            if (!decimal.TryParse(numero, out Salida))
            {
                Console.WriteLine("   El valor debe ser numérico.");
            }
            else
            {
                Flag = true;
            }
            return Flag;
        }
    }
}
