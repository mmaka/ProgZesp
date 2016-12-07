using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace figury
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

    public class Prostokat
    {

        public uint f_id;
        public int szerokosc;
        public int wysokosc;
        public Punkt punkt_zaczepienia;
        public Prostokat()
        {
            szerokosc = 0;
            wysokosc = 0;
            punkt_zaczepienia = new Punkt(0,0);
        }

        public Prostokat(int s,int w)
        {
            szerokosc = s;
            wysokosc = w;
            punkt_zaczepienia = new Punkt(0, 0);
        }
        
        public void wypisz_punkty()
        {
            Console.WriteLine("Prostokat zaczepiony w punkcie {0},{1} (lewy dolny wierzcholek) o szerokosci {2} i wysokosci {3}", punkt_zaczepienia.x, punkt_zaczepienia.y, szerokosc, wysokosc);
        }

        public int min_x()
        {
            return punkt_zaczepienia.x;
        }

        public int min_y()
        {
            return punkt_zaczepienia.y;
        }

        public int max_x()
        {
            return punkt_zaczepienia.x+szerokosc;
        }

        public int max_y()
        {
            return punkt_zaczepienia.y+wysokosc;
        }

        public void aktualizacja_zajetosci_prostokat(int[][] zajetosc_x,int[][] zajetosc_y)
        {
            int poczatek_x = min_x(); ;
            int poczatek_y = min_y();
            int koniec_x = max_x();
            int koniec_y = max_y();

            for (int i = poczatek_x; i < koniec_x; i++)
            {
                zajetosc_x[i][0] = punkt_zaczepienia.y;
            }

            for (int i = poczatek_y; i < koniec_y; i++)
            {
                zajetosc_y[i][0] = punkt_zaczepienia.x;
            }
        }

    }

    public class Matryca
    {

        private int rozmiar_x;
        private int rozmiar_y;
        List<Prostokat> lista_dodanych_figur;
        public int[][] zajetosc_x;
        public int[][] zajetosc_y;

        public Matryca() { }

        public Matryca(int x, int y)
        {
            rozmiar_x = x;
            rozmiar_y = y;
            lista_dodanych_figur = new List<Prostokat>();
            zajetosc_x = new int[x][];
            zajetosc_y = new int[y][];

            for (int i = 0; i < x; i++)
                zajetosc_x[i] = new int[1];

            for (int i = 0; i < y; i++)
                zajetosc_y[i] = new int[1];

            for (int i = 0; i < x; i++)
                zajetosc_x[i][0] = rozmiar_y;

            for (int i = 0; i < y; i++)
                zajetosc_y[i][0] = rozmiar_x;
        }

        public int zwroc_max_x_prostokat(Prostokat p)
        {

            int poczatek_y = p.min_y();
            int koniec_y = p.max_y();
            int min = zajetosc_y[poczatek_y][0];

            for (int i = poczatek_y; i < koniec_y; i++)
            {

                if (zajetosc_y[i][0] < min) min = zajetosc_y[i][0];
            }
            
            return min;
        }

        public int zwroc_min_x_prostokat(Prostokat p)
        {
            int poczatek_y = p.min_y();
            int koniec_y = p.max_y();

            int max = 0;

            for (int i = poczatek_y; i < koniec_y; i++)
            {

                if (zajetosc_y[i][0] > max) max = zajetosc_y[i][0];
            }
            
            return max;
        }

        public int zwroc_max_y_prostokat(Prostokat p)
        {
            int poczatek_x = p.min_x();
            int koniec_x = p.max_x();
            int min = zajetosc_x[poczatek_x][0];

            for (int i = poczatek_x; i < koniec_x; i++)
            {
                if (zajetosc_x[i][0] < min) min = zajetosc_x[i][0];
            }

            return min;
        }
        
        public bool polozenie_poczatkowe_prostokat(Prostokat p)
        {
            float max_x = zwroc_max_x_prostokat(p);
            float max_y = zwroc_max_y_prostokat(p);

            //tutaj raczej nie trzeba sprawdzać dwóch warunków - przeszukiwanie x i y jest symetryczne
            if (p.punkt_zaczepienia.y + p.max_y() <= max_y && p.punkt_zaczepienia.x + p.max_x() <= max_x)
            {
                return true;
            }
            else return false;

        }

        public void idz_max_w_prawo(Prostokat p)
        {

            int max_x = zwroc_max_x_prostokat(p);
            p.punkt_zaczepienia.x = max_x - p.max_x() + p.min_x();
        }

        public void idz_max_w_gore(Prostokat p)
        {
            int max_y = zwroc_max_y_prostokat(p);
            p.punkt_zaczepienia.y = max_y - p.max_y() + p.min_y();
        }

        public bool probkuj_gore(Prostokat p)
        {
            int max_y = zwroc_max_y_prostokat(p);

            if (max_y > p.punkt_zaczepienia.y + p.max_y() - p.min_y()) return true;
            else return false;

        }

        public void idz_max_w_lewo(Prostokat p)
        {
            int max_x = zwroc_min_x_prostokat(p);
            //p.punkt_zaczepienia = new Punkt(max_x, p.punkt_zaczepienia.y);
            p.punkt_zaczepienia.x = max_x;
        }
        
    }
}

