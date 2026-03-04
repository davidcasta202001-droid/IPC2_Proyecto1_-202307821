namespace EjemploP1
{
    class Rejilla
    {
        private Fila[] filas;   // arreglo de filas
        private int m;          // tamaño de la rejilla (M x M)

        public Rejilla(int m)
        {
            this.m = m;
            filas = new Fila[m];
            for (int i = 0; i < m; i++)
            {
                filas[i] = new Fila(m, i); // inicializa cada fila
            }
        }

        // Agregar una celda en la rejilla
        public void AgregarCelda(int fila, int columna, Estado estado)
        {
            filas[fila].agregarNodo(estado, fila, columna);
        }

        // Mostrar toda la rejilla
        public void MostrarRejilla()
        {
            for (int i = 0; i < m; i++)
            {
                Console.Write($"Fila {i}: ");
                filas[i].mostrarFila();
                Console.WriteLine();
            }
        }

        // Buscar una celda en la rejilla
        public Nodo BuscarCelda(int fila, int columna)
        {
            return filas[fila].Buscar(columna); // necesitarás implementar Buscar en Fila
        }

        // Contar células sanas y contagiadas
        public void ContarEstados()
        {
            int sanas = 0;
            int contagiadas = 0;

            for (int i = 0; i < m; i++)
            {
                Nodo actual = filas[i].primero;
                while (actual != null)
                {
                    if (actual.estado == Estado.Sana)
                        sanas++;
                    else
                        contagiadas++;
                    actual = actual.siguiente;
                }
            }

            Console.WriteLine($"Sanas: {sanas}, Contagiadas: {contagiadas}");
        }
    }
}