using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dodawanie_figur1.Obiekty
{
    public class Punkt
    {
        public int x;
        public int y;

        public Punkt(int i = 0, int j = 0) { x = i; y = j; }
        public void wypisz()
        {
            Console.WriteLine("{0},{1}", x, y);
        }
    }
}
