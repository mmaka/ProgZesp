using Rozmieszczenie;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Rozmieszczenie
{
    //przechowujemy tu informacje dotyczace rozmieszczenia figury, tj. na ktorej matrycy (przy rozmieszczeniach zajmujacych wiele matryc) i w jakim punkcie jest zaczepiony
    public class MatrycaFiguraPunkt
    {
        public int nr_matrycy;
        public IFigura figura;
        public Punkt p;
        public MatrycaFiguraPunkt(int nr,IFigura fig,Punkt punkt)
        {
            nr_matrycy = nr;
            figura = fig;
            p = punkt;   
        }
    }

    public class Rozmieszczenia
    {
        public MatrycaFiguraPunkt[] lokalizacja_figur; //dla danego rozmieszczenia potrzebujemy tablicę z lokalizacjami prostokątów
        public List<Matryca> lista_matryc;
        public int[] indeksy;
        public int NajPowPro;
        public int WolPowNrMat;
        public Rozmieszczenia(int liczba_figur,Matryca m,int[] tab_indeksow)
        {
            lista_matryc = new List<Matryca>();
            lista_matryc.Add(m);
            lokalizacja_figur = new MatrycaFiguraPunkt[liczba_figur];
            indeksy = new int[liczba_figur];
            NajPowPro = 0;
            WolPowNrMat = 0;

            for (int i = 0; i < liczba_figur; i++)
            {
                lokalizacja_figur[i] = new MatrycaFiguraPunkt(1, null, new Punkt());
                indeksy[i] = tab_indeksow[i];
            }
        }
        //wypisywanie na potrzeby testów
        public string wypisz()
        {
            string s="";
            for (int i = 0; i < lokalizacja_figur.Count(); i++)
            {
                s=(lokalizacja_figur[i].nr_matrycy+ lokalizacja_figur[i].p.wypisz()+lokalizacja_figur[i].figura.przedstaw_sie());
                
            }
            return s;
        }

        public int Liczba_wykorzystanych_matryc
        {
            get { return lista_matryc.Count; }
        }

        public void wolna_powierzchnia_matrycy(int nr_matrycy)
        {
            int j = 0,suma = 0;
            int tmp = lista_matryc[nr_matrycy].zajetosc_x[0];

            for(int i = 0; i < lista_matryc[nr_matrycy].zajetosc_x.Count(); i++)
            {
                if (tmp == lista_matryc[nr_matrycy].zajetosc_x[i])
                {
                    j++;
                }
                else
                {
                    suma += j * tmp;
                    tmp = lista_matryc[nr_matrycy].zajetosc_x[i];
                    j = 0;
                }
            }

            WolPowNrMat=suma;
        }

        public void najwieksza_prostokatna_powierzchnia()
        {
             int tmp_najwieksza = 0;

            foreach(Matryca m in lista_matryc)
            {
                int tmp_x = 0, tmp_y = 0, tmp_y_min = m.zajetosc_x[0], tmp_pole = 0;

                for (int i = 0; i < m.zajetosc_x.Length; i++)
                {
                    if(tmp_y != m.zajetosc_x[i])
                    {
                        tmp_x = i - tmp_x;
                        tmp_pole = tmp_y * tmp_x;
                        if (tmp_pole > tmp_najwieksza) tmp_najwieksza = tmp_pole;
                        tmp_y = m.zajetosc_x[i];
                        if(tmp_y_min > tmp_y) tmp_y_min = tmp_y;
                    }
                   
                }
                tmp_pole = tmp_y_min * m.zajetosc_x.Length;
                if (tmp_pole > tmp_najwieksza) tmp_najwieksza = tmp_pole;
            }

            NajPowPro=tmp_najwieksza;
        }

    }
}
