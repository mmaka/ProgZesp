using System;
using System.Collections.Generic;
using System.Text;


namespace Rozmieszczenie
{
    public class Punkt
    {
        public int x;
        public int y;

        public Punkt(int i = 0, int j = 0) { x = i; y = j; }
        public string wypisz()
        {
           // Console.Write("{0},{1} | ", x, y);
           return (string)(x+","+ y);
        }
    }
}
