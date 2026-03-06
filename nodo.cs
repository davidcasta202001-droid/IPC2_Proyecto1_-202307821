namespace EjemploP1
{
    class Nodo : Celula
    {
        public Nodo siguiente;
        public Nodo anterior;

        public int fila { get; set; }
        public int columna { get; set; }

        public Nodo(Estado estado, int fila, int columna) : base(estado)
        {
            this.fila = fila;
            this.columna = columna;
        }

        public override void cambiarEstado()
        {
            if (estado == Estado.Sana)
            {
                estado = Estado.Contagiada;
            }
            else
            {
                estado = Estado.Sana;
            }
        }
    }
}