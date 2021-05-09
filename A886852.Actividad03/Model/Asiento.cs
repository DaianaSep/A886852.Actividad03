using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A886852.Actividad03
{
    public class Asiento
    {
        public int NumAsiento { get; set; }
        public DateTime Fecha { get; set; }
        public List<LineaAsiento> ListaLineas { get; set; }

        public Asiento() { }

        public Asiento(int numAsiento, DateTime fecha)
        {
            this.NumAsiento = numAsiento;
            this.Fecha = fecha;
            this.ListaLineas = new List<LineaAsiento>();
        }
    }
}
