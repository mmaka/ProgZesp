using Rozmieszczenie.Widoki;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Rozmieszczenie
{
    public class Matryca
    {
        //rozmiar matrycy
        public int rozmiar_x;
        public int rozmiar_y;
        
        //zajetosc z perspektywy x - tj. indeksy tablicy to x, a wartości to maksymalny y (dla danego x)
        public int[] zajetosc_x;
        
        public Matryca() { }
        public Matryca(int x, int y)
        {
            rozmiar_x = x;
            rozmiar_y = y;
            zajetosc_x = new int[x+1];
       
            for (int i = 0; i < zajetosc_x.Length; i++)
                zajetosc_x[i] = rozmiar_y;
            
        }
        public Matryca(nowa_matryca NP)
        {

            int tmp_szerokosc = Convert.ToInt32(NP.textBox_matryca_szerokość.Text);
            int tmp_wysokosc = Convert.ToInt32(NP.textBox_matryca_wysokość.Text);
            if (tmp_szerokosc != 0 && tmp_wysokosc != 0)
            {
                rozmiar_x = tmp_szerokosc;
                rozmiar_y = tmp_wysokosc;
            }
            NP.Close();
        }

        //NIE UŻYWAMY TEJ METODY: reinicjacja tablicy - po wygenerowaniu rozmieszczenia, reinicjujemy tablicę danej matrycy przed kolejnym użyciem (potrzebne do generowania wielu rozmieszczen
        public void reinicjuj_tab_zajetosci() 
        {
            for (int i = 0; i < zajetosc_x.Length; i++)
                zajetosc_x[i] = rozmiar_y;
            
        }
    }
}
