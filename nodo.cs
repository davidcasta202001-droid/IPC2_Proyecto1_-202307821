using System.Dynamic;
using System.Globalization;

namespace P1
{
    class Nodo : Celula
    {
        public Nodo siguiente;
        public Nodo anterior;
        public int fila{get;set;}
        public int columna {get;set;}

        public Nodo (Estado Estado, int fila, int columna): base(estado)
        {
            this.fila = fila;
            this.columna = columna;
        }
        public abstract void cambiarEstado()
        {
            if (estado == estado.sana)
            {
                estado = Estado.contagiada;
            }
            else
            {
                estado = Estado.Sana;
            }
        }
    }
}