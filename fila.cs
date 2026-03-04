namespace EjemploP1
{
    class Fila
    {
        public Nodo primero;
        public Nodo ultimo;
        public int numfila;
        public int m;

        public Fila(int m, int numfila)
        {
            this.m = m;
            this.numfila = numfila;
            this.primero = null;
            this.ultimo = null;
        }
        public Nodo Buscar(int columna)
        {
            Nodo actual = primero;
            while (actual != null)
            {
                if (actual.columa == columna) 
                {
                    return actual; 
                }
                actual = actual.siguiente; 
            }
            return null; 
        }
        public void agregarNodo(Estado estado, int fila, int columna)
        {
            Nodo nuevo = new Nodo(estado, fila, columna);
            if (primero == null)
            {
                primero = nuevo;
                ultimo = nuevo;
            }
            else
            {
                ultimo.siguiente = nuevo;
                nuevo.anterior = ultimo;
                ultimo = nuevo;
            }
        }

        public void mostrarFila()
        {
            Nodo actual = primero;
            while (actual != null)
            {
                Console.WriteLine($"|{actual.estado}|");
                actual = actual.siguiente;
            }
        }
        
    }
}