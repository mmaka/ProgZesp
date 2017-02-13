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
        public int NajPowPro;//największa wolna prostokątna powierzchnia na wszystkich matrycach
        public int NajPowProNr;//największa powierzchnia prostokątna na wskazanej matrycy - domyślnie ostatnia użyta
        public int SumNajPowPro;//suma największych prostokątnych powierzchni na kolejnych matrycach
        public int WolPowNrMat;//wolna powierzchnia na wskazanej matrycy - domyślnie ostatniej użytej
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
            NajPowProNr = 0;

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
        public string wypiszNRMatrycy(int nrMatrcy)
        {

            string s = "";
            for (int i = 0; i < lokalizacja_figur.Count(); i++)
            {
                if (lokalizacja_figur[i].nr_matrycy == nrMatrcy)
                    s += ("\n" + lokalizacja_figur[i].nr_matrycy);

            }
            return s;
        }
        public string wypiszID(int nrMatrcy)
        {

            string s = "";
            for (int i = 0; i < lokalizacja_figur.Count(); i++)
            {
                if (lokalizacja_figur[i].nr_matrycy == nrMatrcy)
                    s += ("\n" + lokalizacja_figur[i].figura.ID);

            }
            return s;
        }
        public string wypiszWymiary(int nrMatrcy)
        {

            string s = "";
            for (int i = 0; i < lokalizacja_figur.Count(); i++)
            {
                if (lokalizacja_figur[i].nr_matrycy == nrMatrcy)
                    s += ("\n" + lokalizacja_figur[i].figura.W + " " + lokalizacja_figur[i].figura.H);

            }
            return s;
        }
        public string wypiszPunktZaczepienia(int nrMatrcy)
        {

            string s = "";
            for (int i = 0; i < lokalizacja_figur.Count(); i++)
            {
                if (lokalizacja_figur[i].nr_matrycy == nrMatrcy)
                    s += ("\n" + lokalizacja_figur[i].p.wypisz());

            }
            return s;
        }
        public int wolna_powierzchnia_matrycy(int nr_matrycy)
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

            return suma;
        }

        public void wolna_powierzchnia_ostatnia_matryca()
        {
            WolPowNrMat = wolna_powierzchnia_matrycy(Liczba_wykorzystanych_matryc - 1);
        }
  
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

        public void najwieksza_prostokatna_powierzchnia()
        {
            int tmp_najwieksza = 0;

            foreach (Matryca m in lista_matryc)
            {
                int tmp = max_prostokat(m.zajetosc_x, 0);
                if (tmp_najwieksza < tmp) tmp_najwieksza = tmp;
            }

            NajPowPro = tmp_najwieksza;
        }

        public void najwieksza_prostokatna_powierzchnia_nr_matrycy(int nr_matrycy)
        {
            NajPowProNr = max_prostokat(lista_matryc[nr_matrycy].zajetosc_x, 0);
        }

        public void zaktualizuj_liste_indeksow()
        {
            int[] nowe_indeksy = new int[indeksy.Length];
            int k = 0;

            for(int j = 0; j < Liczba_wykorzystanych_matryc; j++)
            {
                for (int i = 0; i < indeksy.Length; i++)
                {
                    if(lokalizacja_figur[i].nr_matrycy == j)
                    {
                        nowe_indeksy[k] = indeksy[i];
                        k++;
                    }
                }
            }

            indeksy = nowe_indeksy;
            
        }

        public void suma_NajPowPro()
        {
            int suma = 0;

            for(int i =0; i<lista_matryc.Count;i++)
            {
                int tmp = max_prostokat(lista_matryc[i].zajetosc_x, 0);
                suma += tmp;
            }

            SumNajPowPro = suma;
        }
      }
    }
