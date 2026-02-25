namespace P1
{
    abstract class Celula
    {
        public Estado Estado;
        public Celula(Estado Estado){
            this.Estado = Estado;

        }
        public abstract void cambiarEstado();
    }
}