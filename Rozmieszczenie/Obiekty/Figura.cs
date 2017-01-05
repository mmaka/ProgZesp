using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rozmieszczenie
{
    public interface IFigura
    {
        void wprowadz_wymiary();
        bool ustal_pozycje(Punkt p,Matryca m,int odstep);
        string przedstaw_sie();
      //  void rysuj();
    }
}
