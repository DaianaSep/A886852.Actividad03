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
            decimal debe = 0;
            decimal haber = 0;
            int CodCuentaOk = 0;
            decimal ImporteOk = 0;
            DateTime fechaOk = DateTime.Now;
            bool flag;
            string nuevaLinea = "1";

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
                Console.WriteLine("<------------------------ Generación/Actualización de Libro Diario ----------------------------->");
                Console.WriteLine();

                Console.WriteLine("Seleccione una de las siguientes opciones del menú y presione [ENTER] para continuar:\n" +
               "1 - Crear Asiento \n" +
               "9 - Finalizar");
                nuevaLinea = Console.ReadLine();

                switch (nuevaLinea)
                {
                    case "1":
                        nuevaLinea = "1";
                        break;
                    case "9":
                        break;
                    default:
                        do
                        {
                            Console.WriteLine("No ha ingresado una opción válida.");
                            Console.WriteLine("Seleccione una de las siguientes opciones del menú y presione [ENTER] para continuar:\n" +
                               "1 - Crear Asiento \n" +
                               "9 - Finalizar");
                            nuevaLinea = Console.ReadLine();
                        } while (nuevaLinea != "1" && nuevaLinea != "9");
                        break;
                }

                while (nuevaLinea == "1")
                {
                    debe = 0;
                    haber = 0;
                    Console.WriteLine();
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
                    nuevaLinea = "1";
                    while (nuevaLinea == "1")
                    {
                        var linea = new LineaAsiento();
                        crearAsiento.ListaLineas.Add(linea);
                        flag = false;

                        do
                        {
                            do
                            {
                                Console.Write(" - Código de la cuenta: ");
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
                                Console.WriteLine($" --- La cuenta ingresada es: {Existe.Nombre} - Tipo: {Existe.Tipo} --- ");
                                linea.CodCuenta = CodCuentaOk;
                            }

                        } while (!flag);

                        do
                        {
                            flag = false;
                            Console.WriteLine();
                            Console.Write("Ingrese a continuación el importe de la línea.\nIMPORTANTE! Si corresponde al HABER debe ingresarse con signo negativo\n" +
                                " - Importe: ");
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

                        Console.WriteLine();
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
                            Console.WriteLine($"Ups! No se cumple la Partida Doble ({debe} != {haber}). Ingrese una nueva línea para equilibrar las partidas.");
                            Console.WriteLine();
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
                        nuevaLinea = Console.ReadLine();
                    }
                }

                if (nuevaLinea == "0")
                {
                    LibroDiario.GrabarAsiento(listaAsientos);
                    string Path = System.IO.Path.Combine(System.IO.Directory.GetCurrentDirectory(), "Files", "Diario.txt");
                    Console.WriteLine();
                    Console.WriteLine($"Éxito! Se ha generado/actualizado el Libro diario en la ruta: {Path}");

                }

                Console.ReadKey();
            }
        }
    }
}


