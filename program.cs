using System;

namespace P1{
    class Program{
        static void main(String [] asrgs){
            Fila f1 = new Fila(5,1);
            f1.agregarNodo(Estado.Sana, 1,1);
            f1.agregarNodo(Estado.Sana, 1,2);
            f1.agregarNodo(Estado.Contagiada, 1,1);
            f1.agregarNodo(Estado.Contagiada, 1,3);
            f1.agregarNodo((Estado)0,1,4);
            f1.agregarNodo((Estado)0,1,5);

            f1.mostrarFila();
        }
    }
}