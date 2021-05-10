using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A886852.Actividad03
{
    public static class LibroDiario
    {
        public static void GrabarAsiento(List<Asiento> listaAsientos)
        {
            string Path = System.IO.Path.Combine(System.IO.Directory.GetCurrentDirectory(), "Files", "Diario.txt");

            foreach (Asiento a in listaAsientos)
            {
                foreach (LineaAsiento lineaAsiento in a.ListaLineas)
                {
                   
                        string strDebe = String.Format("{0:#,####0.00}", lineaAsiento.Debe);
                        string strHaber = String.Format("{0:#,####0.00}", lineaAsiento.Haber);
                        File.AppendAllText(Path, $"{a.NumAsiento}|{a.Fecha:dd/MM/yyyy}|{lineaAsiento.CodCuenta}|{strDebe}|{strHaber}" + System.Environment.NewLine);
                }
            }
        }

        public static int GetUltAsiento()
        {
            string Path = System.IO.Path.Combine(System.IO.Directory.GetCurrentDirectory(), "Files", "Diario.txt");
            var lista_Num_Asientos = new List<int>();
            if (File.Exists(Path))
            {
                foreach (string asiento in File.ReadAllLines(Path))
                {
                    int NumAsiento = Convert.ToInt32(asiento.Split('|')[0]);
                    lista_Num_Asientos.Add(NumAsiento);
                }

                return lista_Num_Asientos[lista_Num_Asientos.Count - 1];
            }
            else
            {
                return 0;
            }
        }
    }
}
