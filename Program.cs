using System;
using System.Xml.Linq;
using System.Collections.Generic;

namespace EjemploP1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== PROYECTO 1 IPC2 ===");
            Console.Write("Ingrese ruta del XML de entrada: ");
            string ruta = Console.ReadLine();
            XDocument doc = XDocument.Load(ruta);
            XElement resultados = new XElement("resultados");

            foreach (var paciente in doc.Descendants("paciente"))
            {
                string nombre = paciente.Element("datospersonales").Element("nombre").Value;
                int edad = int.Parse(paciente.Element("datospersonales").Element("edad").Value);
                int periodos = int.Parse(paciente.Element("periodos").Value);
                int m = int.Parse(paciente.Element("m").Value);

                Rejilla rejillaObj = new Rejilla(m);

                
                var rejillaXml = paciente.Element("rejilla");
                foreach (var celda in rejillaXml.Elements("celda"))
                {
                    int fila = int.Parse(celda.Attribute("f").Value);
                    int columna = int.Parse(celda.Attribute("c").Value);
                    rejillaObj.AgregarCelda(fila, columna, Estado.Contagiada);
                }

                List<string> historial = new List<string>();
                historial.Add(rejillaObj.EstadoActual());

                for (int p = 1; p <= periodos; p++)
                {
                    rejillaObj.SimularPeriodo();
                    historial.Add(rejillaObj.EstadoActual());
                }

                int N, N1;
                string resultado = rejillaObj.DetectarPatron(historial, out N, out N1);

                XElement pacienteXml = new XElement("paciente",
                    new XElement("nombre", nombre),
                    new XElement("edad", edad),
                    new XElement("resultado", resultado),
                    new XElement("n", N),
                    new XElement("n1", N1)
                );

                resultados.Add(pacienteXml);
            }


            XDocument docSalida = new XDocument(resultados);
            docSalida.Save("resultados.xml");

            Console.WriteLine("Archivo resultados.xml generado correctamente.");
        }
    }
}