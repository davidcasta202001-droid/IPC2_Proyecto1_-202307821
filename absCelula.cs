namespace EjemploP1
{
    abstract class Celula
    {
        public Estado estado;

        public Celula(Estado estado)
        {
            this.estado = estado;
        }

        public abstract void cambiarEstado();
    }
}