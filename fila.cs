namespace P1
{
    class Fila
    {
        public Nodo primero;
        public Nodo ultimo;

        public int numFila;
        public int m; 
        public Fila(int m, int numFila)
        {
            this.m = m;
            this.numFila = numFila;
            this.primero = null;
            this.ultimo = null;
        }
        public void agregarNodo(Estado estado, int fila, int columna)
        {
         Nodo nuevo = new Nodo(estado, fila, columna);
            if(primero == null)
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
    }
    
}