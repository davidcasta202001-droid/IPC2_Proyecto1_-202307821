using System.Runtime.InteropServices;

namespace EjemploP1
{
    class NodoFila
    {
        public Fila fila;
        public NodoFila siguiente;
        public NodoFila(Fila fila)
        {
            this.fila = fila;
            this.siguiente = null;
        }
    }
    class Rejilla
    {
        //private Fila[] filas;

        private int m;
        private NodoFila primero;
        public Rejilla(int m)
        {
            this.m = m;
            NodoFila actual = null;
            for (int i = 0; i < m; i++)
            {
                NodoFila nuevo = new NodoFila(new Fila(m, i));
                if (primero == null)
                {
                    primero = nuevo;
                }
                else
                {
                    actual.siguiente = nuevo;
                }
                actual = nuevo;

            }
            /*filas = new Fila[m];
            for (int i = 0; i < m; i++)
            {
                filas[i] = new Fila(m, i); 
            }*/
        }

        public void AgregarCelda(int fila, int columna, Estado estado)
        {

            NodoFila actual = primero;
            int indice = 0;
            while (actual != null && indice < fila)
            {
                actual = actual.siguiente;
                indice++;
            }
            if (actual != null)
            {
                actual.fila.agregarNodo(estado, fila, columna);
            }
            else
            {
                Console.WriteLine($"Fila {fila} fuera de rango en la lista enlazada");
            }
            //filas[fila].agregarNodo(estado, fila, columna);
        }

        public Nodo BuscarCelda(int fila, int columna)
        {
            NodoFila actual = primero;
            int indice = 0;
            while (actual != null && indice < fila)
            {
                actual = actual.siguiente;
                indice++;
            }
            if (actual != null)
            {
                return actual.fila.Buscar(columna);
            }
            return null;
        }

        public void ContarEstados()
        {
            int sanas = 0;
            int contagiadas = 0;
            NodoFila actualFila = primero;
            while (actualFila != null)
            {
                Nodo actualCelda = actualFila.fila.primero;
                while (actualCelda != null)
                {
                    if (actualCelda.estado == Estado.Sana)
                    {
                        sanas++;
                    }
                    else
                    {
                        contagiadas++;
                    }
                    actualCelda = actualCelda.siguiente;
                }
                actualFila = actualFila.siguiente;
            }
            Console.WriteLine(sanas);
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


        public void cambiarEstado(int fila, int columna, Estado estado)
        {
            NodoFila actualFila = primero;
            for (int i = 0; i < fila && actualFila != null; i++)
            {
                actualFila = actualFila.siguiente;
            }
            if (actualFila != null)
            {
                Nodo actualCelda = actualFila.fila.primero;
                for (int j = 0; j < columna && actualCelda != null; j++)
                {
                    actualCelda = actualCelda.siguiente;
                }
                if (actualCelda != null)
                {
                    actualCelda.estado = estado;
                }
            }

        }
        public void SimularPeriodo()
        {
            Estado[,] nuevosEstados = new Estado[m, m];
            NodoFila actualFIla = primero;
            int filaIndice = 0;
            while (actualFIla != null)
            {
                Nodo actualCelda = actualFIla.fila.primero;
                while (actualCelda != null)
                {
                    int vecinos = ContarVecinosContagiados(filaIndice, actualCelda.columna);
                    if (actualCelda.estado == Estado.Contagiada)
                    {
                        if (vecinos == 2 || vecinos == 3)
                        {
                            nuevosEstados[filaIndice, actualCelda.columna] = Estado.Contagiada;
                        }
                        else
                        {
                            nuevosEstados[filaIndice, actualCelda.columna] = Estado.Sana;
                        }
                    }
                    else
                    {
                        if (vecinos == 3)
                        {
                            nuevosEstados[filaIndice, actualCelda.columna] = Estado.Contagiada;
                        }
                        else
                        {
                            nuevosEstados[filaIndice, actualCelda.columna] = Estado.Sana;
                        }
                    }
                    actualCelda = actualCelda.siguiente;
                }
                actualFIla = actualFIla.siguiente;
                filaIndice++;
            }
            actualFIla = primero;
            filaIndice = 0;
            while (actualFIla != null)
            {
                Nodo actualCelda = actualFIla.fila.primero;
                while (actualCelda != null)
                {
                    actualCelda.estado = nuevosEstados[filaIndice, actualCelda.columna];
                    actualCelda = actualCelda.siguiente;
                }
                actualFIla = actualFIla.siguiente;
                filaIndice++;
            }

        }
        public string EstadoActual()
        {
            string estado = "";
            NodoFila actualFila = primero;
            while (actualFila != null)
            {
                Nodo actualCelda = actualFila.fila.primero;
                while (actualCelda != null)
                {
                    if (actualCelda.estado == Estado.Contagiada)
                    {
                        estado += "1";
                    }
                    else
                    {
                        estado += "0";
                    }
                    actualCelda = actualCelda.siguiente;
                }
                estado += "\n";
                actualFila = actualFila.siguiente;
            }
            return estado;
        }
        public string DetectarPatron(List<string> historial, out int N, out int N1)
        {
            string actual = EstadoActual();
            N = 0;
            N1 = 0;
            string patronInicial = historial[0];
            string ultimo = historial[historial.Count-1];
            bool casoSano = !ultimo.Contains("1");
            if (casoSano)
            {
                return "Leve";
            }
            for (int i = 1; i < historial.Count; i++)
            {
                if (historial[i] == patronInicial)
                {
                    N = i;
                    break;
                }
            }
            if (N == 1)
            {
                return "Mortal";
            }
            else if (N > 1)
            {
                return "Grave";
            }
            if (N == 0)
            {
                for(int j = 1; j < historial.Count; j++)
                {
                    for(int i = j + 1; i < historial.Count; i++)
                    {
                        if (historial[i] == historial[j] && historial[j] != patronInicial)
                        {
                            N1 =i-j; 
                        }

                    }
                }
                if (N1 == 1)
                {
                    return "Mortal";
                }
                else if (N1 > 1)
                {
                    return "Grave";
                }
            }
            return "Leve";
        }

        public void LimpiarDatos()
        {
            this.primero = null;
        }
        public void Resistencia(int fila, int columna)
        {
            NodoFila actualFIla = primero;
            for(int i=0; i < fila && actualFIla !=null; i++)
            {
                actualFIla = actualFIla.siguiente;
            }
            if (actualFIla != null)
            {
                Nodo actualCelda = actualFIla.fila.primero;
                for (int j = 0; j < columna && actualCelda != null; j++)
                {
                    actualCelda = actualCelda.siguiente;
                }
                if (actualCelda != null)
                {
                    actualCelda.estado = Estado.Sana;
                }
            }
                /*
                NodoFila actualFila = primero;
            for (int i = 0; i < fila && actualFila != null; i++)
            {
                actualFila = actualFila.siguiente;
            }
            if (actualFila != null)
            {
                Nodo actualCelda = actualFila.fila.primero;
                for (int j = 0; j < columna && actualCelda != null; j++)
                {
                    actualCelda = actualCelda.siguiente;
                }
                if (actualCelda != null)
                {
                    actualCelda.estado = estado;
                }
            }*/
        }
    }
}

