using System.Collections.Generic;
using System.Linq;

namespace Rozmieszczenie
{
    //przechowujemy tu informacje dotyczace rozmieszczenia figury, tj. na ktorej matrycy (przy rozmieszczeniach zajmujacych wiele matryc) i w jakim punkcie jest zaczepiony
    public class MatrycaFiguraPunkt
    {
        public int nr_matrycy;
        public Prostokat figura;
        public Punkt p;
        public MatrycaFiguraPunkt(int nr,Prostokat fig,Punkt punkt)
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
        public int odstep;
        public int NajPowPro;
        public int NajPowPro2;
        public int SumNajPowPro;
        public int WolPowNrMat;
        public float miara;//dopisane
        public bool czyZmienaneRecznie = false;
        public int Liczba_wykorzystanych_matryc
        {
            get { return lista_matryc.Count; }
        }
        //KONSTRUKTOR
        public Rozmieszczenia(int liczba_figur,Matryca m,int odleglosc,int[] tab_indeksow=null)
        {
            lista_matryc = new List<Matryca>();
            lista_matryc.Add(m);
            lokalizacja_figur = new MatrycaFiguraPunkt[liczba_figur];
            indeksy = new int[liczba_figur];
            odstep = odleglosc;
            NajPowPro = 0;
            WolPowNrMat = 0;
            miara = 0;

            if(tab_indeksow!=null)
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
                s+=("\nNr. matrycy: "+lokalizacja_figur[i].nr_matrycy+ "\tPunkt: "+lokalizacja_figur[i].p.wypisz()+
                    lokalizacja_figur[i].figura.przedstaw_sie());
                
            }
            return s;
        }

        public void wolna_powierzchnia_matrycy(int nr_matrycy)
        {
            int j = 0, suma = 0;
            int tmp = lista_matryc[nr_matrycy].zajetosc_x[0];

            for (int i = 0; i < lista_matryc[nr_matrycy].zajetosc_x.Count(); i++)
            {
                if (tmp != lista_matryc[nr_matrycy].zajetosc_x[i] || i == lista_matryc[nr_matrycy].zajetosc_x.Length - 1)
                {
                    suma += j * tmp;
                    tmp = lista_matryc[nr_matrycy].zajetosc_x[i];
                    j = 0;
                }
                j++;
            }

            WolPowNrMat = suma;
        }

        //ta metoda niech jeszcze zostanie - miała długą historię ;) była błędna, ale udało mi się ją poprawić - teraz można z niej korzystać do celów kontrolnych: tzn. mamy dwie metody, które liczą to samo - łatwiej jest kontrolować poprawne wyniki
        /*     public void najwieksza_prostokatna_powierzchnia_old()
             {
                 int tmp_najwieksza = 0;

                 foreach (Matryca m in lista_matryc)
                 {
                     int tmp_x = 0, tmp_y = 0, tmp_y_min = m.zajetosc_x[0], tmp_pole = 0;

                     for (int i = 0; i < m.zajetosc_x.Length; i++)
                     {
                         if (tmp_y != m.zajetosc_x[i] || i == m.zajetosc_x.Length-1)
                         {
                             tmp_x = i - tmp_x;
                             tmp_pole = tmp_y * tmp_x;
                             if (tmp_pole > tmp_najwieksza) tmp_najwieksza = tmp_pole;
                             tmp_y = m.zajetosc_x[i];
                             if (tmp_y_min > tmp_y) tmp_y_min = tmp_y;
                         }

                     }
                     tmp_pole = tmp_y_min * (m.zajetosc_x.Length - 1);
                     if (tmp_pole > tmp_najwieksza) tmp_najwieksza = tmp_pole;
                 }

                 NajPowPro = tmp_najwieksza;
             }
             */

        
        public int max_prostokat(int[] tab_zaj, int poczatek)
        {
            int tmp_suma = 0, biezacy_y = tab_zaj[poczatek], max_powierzchnia = 0, suma_rek = 0;
            bool reaguj_na_wyzsze = true;

            for (int i = poczatek; i < tab_zaj.Length; i++)
            {
                if (biezacy_y > tab_zaj[i] || i == tab_zaj.Length - 1)
                {
                    tmp_suma = biezacy_y * (i - poczatek);
                    biezacy_y = tab_zaj[i];
                    if (tmp_suma > max_powierzchnia) max_powierzchnia = tmp_suma;
                }
                else if (biezacy_y < tab_zaj[i] && reaguj_na_wyzsze == true)
                {
                    reaguj_na_wyzsze = false;
                    suma_rek = max_prostokat(tab_zaj, i);
                }
            }

            if (suma_rek > max_powierzchnia) max_powierzchnia = suma_rek;


            return max_powierzchnia;
        }

        //tutaj bez sensu jest sprawdzać wszystkie matryce z danego rozmieszczenia... kolejne, mniej użyte będą miały większą powierzchnię
        public void najwieksza_prostokatna_powierzchnia()
        {
            int tmp_najwieksza = 0;

            foreach (Matryca m in lista_matryc)
            {
                int tmp = max_prostokat(m.zajetosc_x, 0);
                if (tmp_najwieksza < tmp) tmp_najwieksza = tmp;
            }

            NajPowPro2 = tmp_najwieksza;
        }

        public void suma_NajPowPro()
        {
            int suma = 0;
            float tmp_miara = 0;
            int j = 0;

            for(int i =0; i<lista_matryc.Count;i++)
            {
                int tmp = max_prostokat(lista_matryc[i].zajetosc_x, 0);
                int ile_figur = 0;
                for(; j < lokalizacja_figur.Length; j++)
                {
                    if (lokalizacja_figur[j].nr_matrycy == i) ile_figur++;
                    else break;
                }
                tmp_miara += (tmp * ((float)ile_figur / lokalizacja_figur.Length));
                suma += tmp;
            }

            SumNajPowPro = suma;
            miara = tmp_miara;
        }
       
        public List<int> generowanie_rozmieszczenia(int liczba_figur, List<Prostokat> lista_figur, int[] indeksowanie, int nr_matrycy, int nr_pierwszego_prostokata)
        {
            List<int> pozostale_prostokaty = new List<int>();
            int nr_wstawianego = nr_pierwszego_prostokata;

            for (int i = 0; i < liczba_figur; i++)
            {
                Punkt p = new Punkt();
                if (lista_figur[indeksowanie[i]].ustal_pozycje(p, lista_matryc[nr_matrycy], odstep))
                {
                    lokalizacja_figur[nr_wstawianego] = new MatrycaFiguraPunkt(nr_matrycy, lista_figur[indeksowanie[i]], p);
                    indeksy[nr_wstawianego] = indeksowanie[i]; //rozmieszczenie musi pamietać kolejność ustawiania prostokątów - tu ją zapamiętujemy
                    nr_wstawianego++;
                }
                else
                {
                    pozostale_prostokaty.Add(indeksowanie[i]);
                }

            }
            return pozostale_prostokaty;
        }
        }
    }
