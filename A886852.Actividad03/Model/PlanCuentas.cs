using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A886852.Actividad03
{
    public static class PlanCuentas
    {
        public static List<Cuenta> CargarCuentas()
        {
            var listaCuentas = new List<Cuenta>();
            string Path = System.IO.Path.Combine(System.IO.Directory.GetCurrentDirectory(),"Files","Plan_de_cuentas.txt");
            FileInfo FI = new FileInfo(Path);

            if (FI.Exists)
            {
                using (StreamReader SR = FI.OpenText())
                {
                    while (!SR.EndOfStream)
                    {
                        string Linea = SR.ReadLine();
                        string[] Vector = Linea.Split('|');

                        int codigo = Convert.ToInt32(Vector[0]);
                        string nombre = Vector[1];
                        string tipo = Vector[2];
                        listaCuentas.Add(new Cuenta(codigo, nombre, tipo));
                    }
                }
            }
            return listaCuentas;
        }

        public static Cuenta BuscarCuenta(int codigo)
        {
            List<Cuenta> ListCuentas = CargarCuentas();
            Cuenta cuenta = ListCuentas.Find(c => c.Codigo == codigo);
            return cuenta;
        }
    }
}