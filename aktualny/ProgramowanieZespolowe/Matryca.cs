using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace dodawanie_figur1
{
    class Matryca
    {

        public List<Prostokat> lista; //lista figur
        public List<Prostokat> wstawione_obiekty = new List<Prostokat>();
        int vx, vy; 
        private int rozmiar_x;
        private int rozmiar_y;
        public int[][] zajetosc_x;
        public int[][] zajetosc_y;

        public int VX { get { return vx; } }
        public int VY { get { return vy; } }

        public Matryca(int w =0, int h = 0)
        {
            lista = new List<Prostokat>();
            vx = w; vy = h;

            int x = (int)w; int y = (int)h;

            rozmiar_x = x;
            rozmiar_y = y;           
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

        public bool usun(int id)
        {
            for (int i = 0; i < lista.Count(); i++) 
                if (lista[i].ID == id)
                {
                    Prostokat O =  lista[i];
                    lista.Remove(O);
                    O = null;
                    GC.Collect();

                }
            for (int i = 0; i < wstawione_obiekty.Count(); i++)
                if (wstawione_obiekty[i].ID == id)
                {
                    Prostokat O = wstawione_obiekty[i];
                    wstawione_obiekty.Remove(O);
                    O = null;
                    GC.Collect();

                }
            return true;
        }

       public Prostokat szukaj_obiektuXY(int x, int y)
        {
            Prostokat szukany = null;

           for (int i = 0; i < lista.Count(); i++)
                if (lista[i].X >= x)
                    if (lista[i].X + lista[i].W <= x)
                        if (lista[i].Y >= y)
                            if (lista[i].Y + lista[i].H >= y)
                            { szukany = lista[i]; break; }

            if (szukany == null) 
                for (int i = 0; i < wstawione_obiekty.Count(); i++)
                    if (wstawione_obiekty[i].X <= x)
                        if ((wstawione_obiekty[i].X + wstawione_obiekty[i].W) >= x)
                          if (wstawione_obiekty[i].Y <= y)
                                if (wstawione_obiekty[i].Y + wstawione_obiekty[i].H >= y)
                                 {
                                    szukany = wstawione_obiekty[i];
                                    break;
                                }
                        
                    
            return szukany;

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



        public void rysuj(object sender, PaintEventArgs e)
        {         
                   
            foreach (Prostokat O in lista)
            {
                O.rysuj(e);
            }
        }



    }






}

