using System;
using System.Xml.Linq;
using System.Collections.Generic;

namespace EjemploP1
{
    class Program2
    {
        static void Main(string[] args)
        {
            XDocument doc = XDocument.Load(@"C:\Users\Proyectos\Desktop\Proyecto\Facil.xml");
            XElement resultados = new XElement("resultados");
            while (true)
            {
                Console.WriteLine("Ingrese el nombre del paciente a analizar:");
                string nombrePaciente = Console.ReadLine();
                var paciente = doc.Descendants("paciente")
                      .FirstOrDefault(p => p.Element("datospersonales")
                                            .Element("nombre").Value == nombrePaciente);
                if (paciente == null)
                {
                    Console.WriteLine("Error: ese nombre no se encuentra en el archivo");
                }
                else
                {
                    string nombre = paciente.Element("datospersonales").Element("nombre").Value;
                    int edad = int.Parse(paciente.Element("datospersonales").Element("edad").Value);
                    int periodos = int.Parse(paciente.Element("periodos").Value);
                    int m = int.Parse(paciente.Element("m").Value);
                    var rejillaXml = paciente.Element("rejilla");
                    Rejilla rejilla = new Rejilla(m);
                    for (int i = 0; i < m; i++)
                    {
                        for (int j = 0; j < m; j++)
                        {
                            rejilla.AgregarCelda(i, j, Estado.Sana);
                        }
                    }
                    foreach (var celda in rejillaXml.Elements("celda"))
                    {
                        int fila = int.Parse(celda.Attribute("f").Value) - 1;
                        int columna = int.Parse(celda.Attribute("c").Value) - 1;
                        rejilla.cambiarEstado(fila, columna, Estado.Contagiada);
                    }
                    List<string> historial = new List<string>();
                    historial.Add(rejilla.EstadoActual());
                    for (int p = 1; p <= periodos; p++)
                            {
                                rejilla.SimularPeriodo();
                                historial.Add(rejilla.EstadoActual()); // Periodo p
                            }
                    int N, N1;
                    string resultado = rejilla.DetectarPatron(historial, out N, out N1);
                    Console.WriteLine("INGRESE UNA OPCION ");
                    Console.WriteLine("1.PERIODOS DE ANALISIS DE PACIENTE ");
                    Console.WriteLine("2.EJECUCION DE TODOS LOS PERIODOS ");
                    Console.WriteLine("3.GENERAR XML  ");
                    Console.WriteLine("4. LIMPIAR MEMORIA");
                    Console.WriteLine("5.Salir ");
                    Console.WriteLine("6.Agregar celda ");
                    int opcion = int.Parse(Console.ReadLine());
                    switch (opcion)
                    {
                        case 1:
                            Console.WriteLine("Estado inicial");
                            Console.WriteLine(historial[0]);
                            for (int p = 1; p <= periodos; p++)
                            {
                                rejilla.SimularPeriodo();
                                Console.WriteLine($"Periodo {p}:");
                                Console.WriteLine(rejilla.EstadoActual());
                            }
                            break;
                        case 2:
                            if (resultado == "Grave" || resultado == "Mortal")
                            {
                                Console.WriteLine($"Resultado detectado: {resultado} en periodo {N}, cada {N1} periodos.");
                                break;
                            }
                            else
                            {
                                Console.WriteLine($"Resultado detectado: Leve en periodo {N}, cada {N1} periodos.");
                            }
                            break;
                        case 3:
                            XElement pacienteXml = new XElement("paciente",
                            new XElement("nombre", nombre),
                            new XElement("edad", edad),
                            new XElement("resultado", resultado),
                            new XElement("n", N),
                            new XElement("n1", N1)
                        );

                            resultados.Add(pacienteXml);
                            XDocument docSalida = new XDocument(resultados);
                            docSalida.Save("resultados.xml");

                            Console.WriteLine("Archivo resultados.xml generado correctamente.");
                            break;
                        case 4:
                            rejilla.LimpiarDatos();
                            break;
                        case 6:
                            Console.WriteLine("Ingrese fila. ");
                            int fila = int.Parse(Console.ReadLine());
                            Console.WriteLine("Ingrese COLUMNA: ");
                            int columna = int.Parse(Console.ReadLine());
                            rejilla.cambiarEstado(fila, columna, Estado.Sana);

                            Console.WriteLine("Ingrese valor de resistencia N: ");
                            int n = int.Parse(Console.ReadLine());
                            Console.WriteLine(rejilla.EstadoActual());
                            for (int p = 1; p <= periodos; p++)
                            {
                                int k=n-p;
                                Console.WriteLine("Contador de N");
                                Console.WriteLine(k);
                                if (k == 0)
                                {
                                    break;
                                }
                            }
                            
                        break;
                    }
                }
            }
        }
    }
}