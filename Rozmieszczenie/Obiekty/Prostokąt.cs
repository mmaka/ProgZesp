using System;
using Rozmieszczenie.Widoki;
using Rozmieszczenie.Logika;

namespace Rozmieszczenie
{
    public class Prostokat : IFigura
    {
        public static int licznik=0;
        int szerokosc;
        int wysokosc;
        string nazwa;
        int id;

        public int ID { get { return id; }  }
        public int SETID { set { id = value; } }
        public string Nazwa {   get { return nazwa; }  }
        public int W { get { return szerokosc; } set { szerokosc = value; } } 
        public int H { get { return wysokosc; } set { wysokosc = value; } }        
        public int Pole { get  { return szerokosc * wysokosc;} }



        public Prostokat()
        {
            szerokosc = 0;
            wysokosc = 0;
        }

        public Prostokat(int s, int w, int id = 0, string nazwa = "")
        {
            szerokosc = s;
            wysokosc = w;
            this.id = id;
            this.nazwa = nazwa;
        }
        public Prostokat(nowa_prostokat NP)
        {

            int tmp_szerokosc = Convert.ToInt32(NP.textBox_szerokosc_prostokat.Text);
            int tmp_wysokosc = Convert.ToInt32(NP.textBox_wysokosc_prostokat.Text);
            licznik = Jądro.lista_obiektow.Count;
            if (tmp_szerokosc != 0 && tmp_wysokosc != 0)
            {

                szerokosc = tmp_szerokosc;
                wysokosc = tmp_wysokosc;
                licznik++;
                id = licznik;
                nazwa = NP.textBox_nazwa_prostokat.Text;
            }
            NP.Close();
            
        }

        //
        



        //na pewno potrzebna będzie kontrola wprowadzanych danych
        public void wprowadz_wymiary()
        {
           // Console.WriteLine("Podaj w milimetrach szerokosc prostokąta: ");
            int tmp_szerokosc = Convert.ToInt32(Console.ReadLine());
           // Console.WriteLine("Podaj w milimetrach wysokosc prostokąta: ");
            int tmp_wysokosc = Convert.ToInt32(Console.ReadLine());

            if (tmp_szerokosc != 0 && tmp_wysokosc != 0)
            {
                szerokosc = tmp_szerokosc;
                wysokosc = tmp_wysokosc;
            }
        }

        public string przedstaw_sie()
        {
           // Console.WriteLine("Prostokat o szerokosci {0} i wysokosci {1}", szerokosc, wysokosc);
           return "\tID: " + ID+"\tNazwa: " +nazwa+ "\tSzerokość: " + szerokosc + "\tWysokość: " + wysokosc;
        }
        //przekazujemy punkt i otrzymujemy minimalną wartość x danego prostokąta
        public int min_x(Punkt p)
        {
            return p.x;
        }
        //przekazujemy punkt i otrzymujemy minimalną wartość y danego prostokąta
        public int min_y(Punkt p)
        {
            return p.y;
        }
        //przekazujemy punkt i otrzymujemy maksymalną wartość x danego prostokąta
        public int max_x(Punkt p)
        {
            return p.x + szerokosc;
        }
        //przekazujemy punkt i otrzymujemy maksymalną wartość y danego prostokąta
        public int max_y(Punkt p)
        {
            return p.y + wysokosc;
        }
        //ustalamy pozycje: potrzebna jest matryca (przede wszystkim tablica zajetosci) i punkt (domyślny punkt zaczepienia, tj. 0,0)
        public bool ustal_pozycje(Punkt p, Matryca m,int odstep)
        {
            bool start = false;
            if (polozenie_poczatkowe_prostokat(p, m.zajetosc_x,odstep)) //sprawdzamy czy w ogóle można umieścić prostokąt na matrycy
            {
                start = true;
                idz_max_w_gore(p, m.zajetosc_x,odstep);
                idz_max_w_prawo(p, m.zajetosc_x,odstep);
                idz_max_w_gore(p, m.zajetosc_x,odstep);
                aktualizacja_tablic_zajetosci(p, m.zajetosc_x); //po wykonaniu powyższych trzech kroków, znajdujemy punkt zaczepienia prostokąta na matrycy i aktualizujemy tablicę zajętości
            }

            return start;
        }
        //zwracamy maksymalną wartość x jaką może zajmować prostokąt
        private int zwroc_max_x_prostokat(Punkt p, int[] tab_zaj_x,int odstep)
        {
            int y_max = max_y(p);
            int poczatkowy_max_x = max_x(p);
            int poczatek_x = poczatkowy_max_x;
            int rozmiar_tab = tab_zaj_x.Length;

            if (poczatek_x + odstep < tab_zaj_x.Length - 1) poczatek_x += odstep;

            for (int i = poczatek_x; i < rozmiar_tab; i++)
            {
                poczatek_x = i;
                if ((tab_zaj_x[i] < y_max)) break;
            }

            return poczatek_x;           
        }
        //zwracamy minimalną wartość x jaką może zająć prostokąt - nie używamy tej metody
        private int zwroc_min_x_prostokat(Punkt p, int[] tab_zaj_y)
        {
            int poczatek_y = min_y(p);
            int koniec_y = max_y(p);
            int max = 0;

            for (int i = poczatek_y; i <= koniec_y; i++)
                if (tab_zaj_y[i] > max) max = tab_zaj_y[i];

            return max;
        }
        //zwracamy maksymalną wartość y jaką może zająć prostokąt
        private int zwroc_max_y_prostokat(Punkt p, int[] tab_zaj_x,int odstep)
        {
            int poczatek_x = min_x(p);
            int koniec_x = max_x(p);
            int min = tab_zaj_x[poczatek_x];

            if (koniec_x != tab_zaj_x.Length-1)
                    koniec_x = koniec_x + odstep;

            for (int i = poczatek_x; i < koniec_x; i++) //tutaj i<=koniec_x powoduje, że nie zawsze krawędzie będą mogły na siebie zachodzić - jeżeli mamy dwa prostokaty na szczycie matrycy i jeden z nich jest wyższy (tj. ma min_y mniejszy), to gdy mniejszy dosuniemy do większego to zostanie on obniżony do min_y drugiego, bo wg tablicy zajętości ta powierzchnia jest zajęta
                if (tab_zaj_x[i] < min) min = tab_zaj_x[i];

            if (min == tab_zaj_x[tab_zaj_x.Length - 1]) return min;
            else return min - odstep;
        }
        //sprawdzamy czy prostokąt można umieścić w domyślnym punkcie zaczepienia matrycy (0,0)
        private bool polozenie_poczatkowe_prostokat(Punkt p, int[] tab_zaj_x,int odstep)
        {
            int max_wart_x = zwroc_max_x_prostokat(p, tab_zaj_x,odstep);
            int max_wart_y = zwroc_max_y_prostokat(p, tab_zaj_x,odstep);
            return p.y + max_y(p) <= max_wart_y && p.x + max_x(p) <= max_wart_x;
        }
        //przesuwamy położenie prostokąta maksymalnie w prawo
        private void idz_max_w_prawo(Punkt p, int[] tab_zaj_x,int odstep)
        {
            int max_wart_x = zwroc_max_x_prostokat(p, tab_zaj_x,odstep);

            if (max_wart_x != tab_zaj_x.Length - 1) max_wart_x -= odstep;

            p.x = max_wart_x - max_x(p) + min_x(p);
        }
        //przesuwamy położenie prostokąta maksymalnie w góre
        private void idz_max_w_gore(Punkt p, int[] tab_zaj_x,int odstep)
        {
            int max_wart_y = zwroc_max_y_prostokat(p, tab_zaj_x,odstep);
            p.y = max_wart_y - max_y(p) + min_y(p);
        }
        //dla danego położenia sprawdzamy czy można iść w góre - tej metody nie używyamy (w bieżącej wersji zamiast sprawdzać mozna po prostu pójść w góre)
        private bool probkuj_gore(Punkt p, int[] tab_zaj_x,int odstep)
        {
            int max_wart_y = zwroc_max_y_prostokat(p, tab_zaj_x,odstep);
            return max_wart_y > p.y + max_y(p) - min_y(p);
        }

        //po zajęciu powierzchni matrycy przez prostokąt musimy odnotować to w tablicy zajętości
        private void aktualizacja_tablic_zajetosci(Punkt p, int[] zajetosc_x)
        {
            int poczatek_x = min_x(p);
            int koniec_x = max_x(p);

            for (int i = poczatek_x; i < koniec_x; i++)
                zajetosc_x[i] = p.y;

        }
    }
}
