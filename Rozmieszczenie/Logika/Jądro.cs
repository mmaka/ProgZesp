using System;
using Rozmieszczenie.Widoki;
using System.Collections.Generic;

namespace Rozmieszczenie.Logika
{
    public class Jądro
    {
        nowa_prostokat np;
        nowa_matryca nm;
        widok_matryca wm;
        MainWindow MW;
        Matryca Matka;
        informacyjne InfoOkno;
        public Rysowanie R;
  
        public List<Prostokat> lista_obiektow;
        public List<Rozmieszczenia> lista_rozmieszczen = new List<Rozmieszczenia>();
        //Konstruktor
        public Jądro(MainWindow _mw)
        {
            MW = _mw;           
            lista_obiektow = new List<Prostokat>();
           
        }

        //Określenie matrycy
        public void nowy_matryca()
        {
            nm = new nowa_matryca(this);
            nm.Show();

        }      

        public void dodaj_matryce()
        {
            Matka = new Matryca(nm);
            wm = new widok_matryca(this, Matka.rozmiar_x, Matka.rozmiar_y);                  
            wm.Show();           
        }


        //Dodawanie prostokątów
        public void nowy_prostokat()
        {
            np = new nowa_prostokat(this);
            np.Show();            
        }
        public void dodaj_prostokat()
        {
            int tmp_ile = Convert.ToInt32(np.textBox_ilosc_prostokat.Text);
            for (int i = 0; i < tmp_ile; i++)
            {
                lista_obiektow.Add(new Prostokat(np));
                MW.dataGrid.DataContext = lista_obiektow;
                MW.dataGrid.Items.Refresh();
            }           
        }



        //miksowanko
        public void miksuj()
        {
            InfoOkno = new informacyjne();


            int m_x = Matka.rozmiar_x;
            int m_y = Matka.rozmiar_y;
            int liczba_indeksowan = 45;
            List<int[]> lista_indeksow = new List<int[]>();

            wygeneruj_indeksy(lista_indeksow, lista_obiektow.Count, liczba_indeksowan, indeksowania_poczatkowe(lista_obiektow));
            generuj_rozmieszczenia(lista_obiektow, lista_indeksow, lista_rozmieszczen, lista_obiektow.Count, m_x, m_y);

            //    foreach (Rozmieszczenie roz in lista_rozmieszczen)
            //      roz.wypisz();
            int w = 0;
            do
            {
               // selekcja(lista_rozmieszczen, 15);
                List<int[]> lista_ind = new List<int[]>();
                List<Rozmieszczenia> lista_roz2 = new List<Rozmieszczenia>();
                //miksowanie_indeksow(lista_rozmieszczen, lista_ind, liczba_indeksowan);
                miksowanie_indeksow2(lista_rozmieszczen, lista_ind, liczba_indeksowan);
                generuj_rozmieszczenia(lista_obiektow, lista_ind, lista_roz2, lista_obiektow.Count, m_x, m_y);
                w++;

            } while (w < liczba_indeksowan);

            Rozmieszczenia NAJLEPSZE=lista_rozmieszczen[0];
            for (int i = 0; i < lista_rozmieszczen.Count; i++)
            {
                if (NAJLEPSZE.NajPowPro2 < lista_rozmieszczen[i].NajPowPro2)
                    NAJLEPSZE = lista_rozmieszczen[i];
            }

            {             
                InfoOkno.textBox.Text = ("\nRozmieszczenie, szczegóły: \n" +
                    "Liczba wykorzystanych matryc: "+ NAJLEPSZE.Liczba_wykorzystanych_matryc+
                    "\nNajwiększa wolna prostokatna powierzchnia: " + NAJLEPSZE.NajPowPro2 );
                InfoOkno.textBox.Text += ("\n********************************************************************\n ");
                InfoOkno.textBox.Text += NAJLEPSZE.wypisz() + "\n";
                InfoOkno.Show();
                R = new Rysowanie(wm);
                R.Rysuj(wm, NAJLEPSZE);
            }
        }



        //Metody Michała służące całej logice rozmieszczania i wszystkiego innego narazie tutaj ale nie na zawsze

        /*      public static void wygeneruj_indeksy(List<int[]> li, int liczba_figur, int liczba_indeksowan)
              {
                  Random rand = new Random();
                  int j, k;
                  int i = 1;

                  li.Add(new int[liczba_figur]);
                  for (int x = 0; x < liczba_figur; x++)
                      li[0][x] = x;


                  while (i < liczba_indeksowan)
                  {
                      li.Add(new int[liczba_figur]);
                      do
                      {
                          j = rand.Next() % liczba_figur;
                          k = rand.Next() % liczba_figur;

                      } while (j == k);

                      li[i][j] = li[0][k];
                      li[i][k] = li[0][j];

                      for (int w = 0; w < liczba_figur; w++)
                      {
                          if (w != j && w != k)
                              li[i][w] = li[0][w];
                      }

                      i++;
                  }

              }*/

        public static void wygeneruj_indeksy(List<int[]> li, int liczba_figur, int liczba_indeksowan, List<int[]> proponowane)
        {
            Random rand = new Random();
            int j, k;
            int i = proponowane.Count + 1; //indeks 0 jest zajęty przez indeksowanie zgodne z wprowadzoną kolejnością a następne proponowane.Count to indeksowania mające szanse być dobrymi, czyli łacznie losowe generowanie zaczynamy od indeksu proponowane.Count+1

            li.Add(new int[liczba_figur]);
            for (int x = 0; x < liczba_figur; x++)
                li[0][x] = x;

            for (int x = 0; x < proponowane.Count; x++)
            {
                li.Add(proponowane[x]);
            }

            while (i < liczba_indeksowan)
            {
                li.Add(new int[liczba_figur]);
                do
                {
                    j = rand.Next() % liczba_figur;
                    k = rand.Next() % liczba_figur;

                } while (j == k);

                li[i][j] = li[0][k];
                li[i][k] = li[0][j];

                for (int w = 0; w < liczba_figur; w++)
                {
                    if (w != j && w != k)
                        li[i][w] = li[0][w];
                }

                i++;
            }

        }

        //tutaj możemy ustalać indeksowania, które mają szanse być dobrymi
        //zwracana jest lista, więc można dodawać jakiś kod wyliczający indeksowanie, a potem zwyczajnie dodać go do listy
        //metoda generująca indeksowania kontroluje liczbę dodawanych ustalonych indeksowań, więc o nic więcej nie trzeba się martwić
        public static List<int[]> indeksowania_poczatkowe(List<Prostokat> lp)
        {
            List<int[]> tmp_tab = new List<int[]>();

            int[] tab = new int[lp.Count];
            int tmp;

            for (int i = 0; i < lp.Count; i++)
                tab[i] = i;

            for (int i = 0; i < lp.Count; i++)
            {
                for (int j = lp.Count - 1; j > i; j--)
                {
                    if (lp[j].Pole > lp[j - 1].Pole)
                    {
                        tmp = tab[j];
                        tab[j] = tab[j - 1];
                        tab[j - 1] = tmp;
                    }
                }
            }
            
            tmp_tab.Add(tab);
            return tmp_tab;
        }


        public static void miksowanie_indeksow2(List<Rozmieszczenia> lista_roz, List<int[]> lista_ind, int liczba_nowych_indeksowan)
        {
            int liczba_rozmieszczen = lista_roz.Count;
            Random rand = new Random();
            int[] prob_choice = new int[lista_roz.Count];
            int j, k, suma = 0;

            for (int i = 0; i < lista_roz.Count; i++)
            {
                suma += lista_roz[i].NajPowPro2;
                prob_choice[i] = suma;
            }

            for (int i = 0; i < liczba_nowych_indeksowan; i++)
            {
                lista_ind.Add(new int[liczba_rozmieszczen]);

                do
                {
                    j = (rand.Next() % suma) + 1;
                    k = (rand.Next() % suma) + 1;

                } while (j == k);

                int l = 0;
                while (prob_choice[l] < j)
                    l++;

                int m = 0;
                while (prob_choice[m] < k)
                    m++;

                lista_ind[i] = miksuj_dwa_roz(lista_roz[l], lista_roz[m]);

            }
        }
        
        public static void selekcja(List<Rozmieszczenia> lista, int liczba_wybieranych)
        {
            Rozmieszczenia tmp;

            if (liczba_wybieranych < lista.Count)
            {

                for (int j = 0; j < liczba_wybieranych; j++)
                {
                    for (int i = lista.Count - 1; i > j; i--)
                    {
                        if (lista[i].NajPowPro > lista[i - 1].NajPowPro)
                        {
                            tmp = lista[i];
                            lista[i] = lista[i - 1];
                            lista[i - 1] = tmp;
                        }
                    }
                }

                for (int i = lista.Count - 1; i >= liczba_wybieranych; i--)
                    lista.RemoveAt(i);

            }
        }

        public static void miksowanie_indeksow(List<Rozmieszczenia> lista_roz, List<int[]> lista_ind, int liczba_nowych_indeksowan)
        {
            int liczba_rozmieszczen = lista_roz.Count;
            Random rand = new Random();
            int j, k;

            for (int i = 0; i < liczba_nowych_indeksowan; i++)
            {
                lista_ind.Add(new int[liczba_rozmieszczen]);

                do
                {
                    j = rand.Next() % liczba_rozmieszczen;
                    k = rand.Next() % liczba_rozmieszczen;

                } while (j == k);

                lista_ind[i] = miksuj_dwa_roz(lista_roz[k], lista_roz[j]);

            }
        }

        public static int[] miksuj_dwa_roz(Rozmieszczenia r1, Rozmieszczenia r2)
        {
            int liczba_figur = r1.indeksy.Length;
            int[] nowe_indeksowanie = new int[liczba_figur];

            for (int i = 0; i < liczba_figur / 2; i++)
                nowe_indeksowanie[i] = r1.indeksy[i];

            int j = 0;

            for (int i = liczba_figur / 2; i < liczba_figur; i++)
            {
                for (; j < liczba_figur; j++)
                {
                    bool dodaj = true;

                    for (int k = 0; k < liczba_figur / 2; k++)
                    {

                        if (r2.indeksy[j] == nowe_indeksowanie[k])
                        {
                            dodaj = false;
                            break;
                        }
                    }

                    if (dodaj == true) break;

                }

                nowe_indeksowanie[i] = r2.indeksy[j];
                j++;
            }

            mutacja_indeksowania(nowe_indeksowanie);

            return nowe_indeksowanie;
        }

        public static void mutacja_indeksowania(int[] indeksowanie)
        {
            int liczba_figur = indeksowanie.Length;
            int x, y;
            Random rand = new Random();

            do
            {
                x = rand.Next() % liczba_figur;
                y = rand.Next() % liczba_figur;

            } while (x == y);

            int tmp = indeksowanie[x];
            indeksowanie[x] = indeksowanie[y];
            indeksowanie[y] = tmp;
        }

        public static void generuj_rozmieszczenia(List<Prostokat> lista_figur, List<int[]> lista_indeksow, List<Rozmieszczenia> lista_rozmieszczen, int liczba_figur, int m_rozmiar_x, int m_rozmiar_y)
        {
            for (int k = 0; k < lista_indeksow.Count; k++)
            {
                Rozmieszczenia roz = new Rozmieszczenia(liczba_figur, new Matryca(m_rozmiar_x, m_rozmiar_y), lista_indeksow[k]);
                lista_rozmieszczen.Add(roz);

                for (int i = 0; i < liczba_figur; i++)
                {
                    int j = 0;
                    roz.lokalizacja_figur[i] = new MatrycaFiguraPunkt(j, lista_figur[lista_indeksow[k][i]], new Punkt());

                    while (!lista_figur[lista_indeksow[k][i]].ustal_pozycje(roz.lokalizacja_figur[i].p, roz.lista_matryc[j]))
                    {
                        if (j == roz.lista_matryc.Count - 1) roz.lista_matryc.Add(new Matryca(m_rozmiar_x, m_rozmiar_y));

                        j++;
                    }

                    roz.lokalizacja_figur[i].nr_matrycy = j;
                }

                roz.wolna_powierzchnia_matrycy(roz.Liczba_wykorzystanych_matryc - 1);
                roz.najwieksza_prostokatna_powierzchnia();
            }
        }
    


    //Inne
    //zamkniecie programu
    internal void zamknij()
        {
            
            wm.Close();
            
        }





    }
}
