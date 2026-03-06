namespace EjemploP1
{
    class Rejilla
    {
        private Fila[] filas;
        private int m;          

        public Rejilla(int m)
        {
            this.m = m;
            filas = new Fila[m];
            for (int i = 0; i < m; i++)
            {
                filas[i] = new Fila(m, i); 
            }
        }

        public void AgregarCelda(int fila, int columna, Estado estado)
        {
            filas[fila].agregarNodo(estado, fila, columna);
        }

        public Nodo BuscarCelda(int fila, int columna)
        {
            return filas[fila].Buscar(columna); 
        }

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
        }

        public int ContarVecinosContagiados(int fila, int columna)
        {
            int contador = 0;
            int[] dx = { -1, -1, -1, 0, 0, 1, 1, 1 };
            int[] dy = { -1, 0, 1, -1, 1, -1, 0, 1 };

            for (int i = 0; i < 8; i++)
            {
                int nuevaFila = fila + dx[i];
                int nuevaColumna = columna + dy[i];

                if (nuevaFila >= 0 && nuevaFila < m && nuevaColumna >= 0 && nuevaColumna < m)
                {
                    Nodo vecino = BuscarCelda(nuevaFila, nuevaColumna);
                    if (vecino != null && vecino.estado == Estado.Contagiada)
                    {
                        contador++;
                    }
                }
            }

            return contador;
        }
        public void SimularPeriodo()
        {
            Estado[,] nuevosEstados = new Estado[m, m];

            for (int fila = 0; fila < m; fila++)
            {
                Nodo actual = filas[fila].primero;
                while (actual != null)
                {
                    int vecinos = ContarVecinosContagiados(fila, actual.columna);

                    if (actual.estado == Estado.Contagiada)
                    {
                        if (vecinos == 2 || vecinos == 3)
                            nuevosEstados[fila, actual.columna] = Estado.Contagiada;
                        else
                            nuevosEstados[fila, actual.columna] = Estado.Sana;
                    }
                    else 
                    {
                        if (vecinos == 3)
                            nuevosEstados[fila, actual.columna] = Estado.Contagiada;
                        else
                            nuevosEstados[fila, actual.columna] = Estado.Sana;
                    }

                    actual = actual.siguiente;
                }
            }

            for (int fila = 0; fila < m; fila++)
            {
                Nodo actual = filas[fila].primero;
                while (actual != null)
                {
                    actual.estado = nuevosEstados[fila, actual.columna];
                    actual = actual.siguiente;
                }
            }
        }
        public string EstadoActual()
        {
            string estado = "";
            for (int fila = 0; fila < m; fila++)
            {
                Nodo actual = filas[fila].primero;
                while (actual != null)
                {
                    estado += (actual.estado == Estado.Contagiada ? "1" : "0");
                    actual = actual.siguiente;
                }
                estado += "\n";
            }
            return estado;
        }
        public string DetectarPatron(List<string> historial)
        {
            string actual = EstadoActual();


            if (actual == historial[0])
            {
                int N = historial.Count;
                return $"Patrón inicial repetido en período {N}";
            }

            for (int i = 1; i < historial.Count; i++)
            {
                if (actual == historial[i])
                {
                    int N = historial.Count;
                    int N1 = historial.Count - i;
                    return $"Nuevo patrón repetido en período {N}, cada {N1} períodos";
                }
            }

            return "Sin repetición detectada";
        }
        public string DetectarPatron(List<string> historial, out int N, out int N1)
        {
            string actual = EstadoActual(); 
            N = 0;
            N1 = 0;

            if (actual == historial[0])
            {
                N = historial.Count;
                if (N == 1)
                    return "Mortal";
                else
                    return "Grave";
            }

            for (int i = 1; i < historial.Count; i++)
            {
                if (actual == historial[i])
                {
                    N = historial.Count;
                    N1 = historial.Count - i;
                    if (N1 == 1)
                        return "Mortal";
                    else
                        return "Grave";
                }
            }

            return "Leve";
        }
    }
}