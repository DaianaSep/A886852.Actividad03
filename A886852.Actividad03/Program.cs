using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A886852.Actividad03
{
    class Program
    {
        static void Main(string[] args)
        {
            bool Salida = false;
            decimal debe;
            decimal haber;
            DateTime fechaOk = DateTime.Now;
            bool flag;
            string nuevaLinea = "1";
            int CodCuentaOk = 0;
            decimal ImporteOk = 0;


            var listaAsientos = new List<Asiento>();
            int UltNumAsiento = LibroDiario.GetUltAsiento();

            List<Cuenta> cuentas = PlanCuentas.CargarCuentas();
            if (cuentas.Count == 0)
            {
                string PathCuenta = System.IO.Path.Combine(System.IO.Directory.GetCurrentDirectory(), "Files", "Plan_de_cuentas.txt");
                Console.WriteLine($"Error. No se pudo encontrar el archivo Plan_de_cuentas.txt en la ruta {PathCuenta}. Asegúrese de que el archivo se encuentre en la ruta indicada.\nPrograma finalizado.");
            }
            else
            {
                Console.WriteLine("<------------------------ Generación de Libro Diario ----------------------------->");

                Console.WriteLine();
                Console.WriteLine("Seleccione una de las siguientes opciones del menú:\n" +
                    "1 - Crear Asiento \n" +
                    "9 - Finalizar");
                nuevaLinea = Console.ReadLine();
                while (nuevaLinea != "1" && nuevaLinea != "9")
                {
                    Console.WriteLine("No ha ingresado una opción válida.");
                    nuevaLinea = "1";
                }
               

                while (nuevaLinea == "1")
                {
                    debe = 0;
                    haber = 0;
                    Console.WriteLine($"Ingrese a continuación los datos del asiento contable N° {UltNumAsiento + 1}:");
                    Console.WriteLine();
                    do
                    {
                        Console.Write(" - Fecha del asiento con formato dd/mm/yyyy: ");
                        string Fecha = Console.ReadLine();
                        flag = Validaciones.ValidarFecha(Fecha, ref fechaOk);
                    } while (!flag);

                    var crearAsiento = new Asiento(UltNumAsiento + 1, fechaOk);
                    listaAsientos.Add(crearAsiento);

                    while (nuevaLinea == "1")
                    {
                        var linea = new LineaAsiento();
                        crearAsiento.ListaLineas.Add(linea);
                        flag = false;

                        do
                        {
                            do
                            {
                                Console.Write(" - Código de cuenta: ");
                                string CodCuenta = Console.ReadLine();
                                flag = Validaciones.ValidarEntero(CodCuenta, ref CodCuentaOk);
                            } while (!flag);

                            var Existe = PlanCuentas.BuscarCuenta(CodCuentaOk);
                            if (Existe == null)
                            {
                                Console.WriteLine("No existe la cuenta con el código ingresado");
                                flag = false;
                            }
                            else
                            {
                                Console.WriteLine($"La cuenta ingresada es: {Existe.Nombre} - Tipo: {Existe.Tipo}");
                            }

                        } while (!flag);

                        do
                        {
                            flag = false;
                            Console.WriteLine();
                            Console.Write("Ingrese a continuación el importe de la línea. IMPORTANTE! Si corresponde al HABER debe ingresarse con el signo de resta '-' al inicio) \n " +
                                "- Importe: ");
                            string Importe = Console.ReadLine();
                            flag = Validaciones.ValidarMonto(Importe, ref ImporteOk);
                        } while (!flag);

                        if (ImporteOk > 0)
                        {
                            linea.Debe += ImporteOk;
                        }
                        else
                        {
                            linea.Haber += Math.Abs(ImporteOk);
                        }


                        Console.WriteLine($"¿Desea agregar otra línea en el asiento N°{UltNumAsiento + 1}?\n" +
                            $"1 - SI\n" +
                            $"0 - NO");
                        nuevaLinea = Console.ReadLine();

                        while (nuevaLinea != "1" && nuevaLinea != "0")
                        {
                            Console.WriteLine("No ha ingresado una opción válida.");
                            nuevaLinea = Console.ReadLine();
                        }
                        debe += linea.Debe;
                        haber += linea.Haber;

                        if (nuevaLinea == "0" && debe != haber)
                        {
                            Console.WriteLine($"Ups! No se cumple la Partida doble ({debe} != {haber}). Ingrese nuevamente las líneas del asiento");
                            debe = 0;
                            haber = 0;
                            nuevaLinea = "1";
                        }
                    }

                    UltNumAsiento++;
                    Console.WriteLine();
                    Console.WriteLine("Seleccione una de las siguientes opciones del menú: \n " +
                       "1 - Crear Asiento \n " +
                       "0 - Guardar Asientos y Finalizar");
                    nuevaLinea = Console.ReadLine();

                    while (nuevaLinea != "1" && nuevaLinea != "0")
                    {
                        Console.WriteLine("No ha ingresado una opción válida.");
                        nuevaLinea = Convert.ToString(Console.ReadKey(true));
                    }
                }
                LibroDiario.GrabarAsiento(listaAsientos);
                

                if (nuevaLinea == "9")
                {
                    string PathDiario = System.IO.Path.Combine(System.IO.Directory.GetCurrentDirectory(), "Files", "Diario.txt");
                    File.Delete(PathDiario);
                }
            }
        }
    }
}


