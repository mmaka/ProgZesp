using System;
using Rozmieszczenie.Widoki;
using System.Collections.Generic;
using System.Windows;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Threading;
using System.Threading;

namespace Rozmieszczenie.Logika
{
    public class Jądro
    {
        nowa_prostokat np;
        nowa_matryca nm;
        MainWindow MW;
        Zła_MatrycaFigura zMF;

        public pasek Pasek;
        public int odstep; //tutaj ręcznie możemy ustawić rozmiar odstępu
        public widok_matryca wm;
        public Matryca Matka;
        public informacyjne InfoOkno;
        public Rysowanie R;
        public static Rozmieszczenia NAJLEPSZE = null;
        public Przeciaganie prz;
        public static List<Prostokat> lista_obiektow;
        public static int licznik = 0;
        public List<Rozmieszczenia> lista_rozmieszczen;
        volatile public static bool status = true;
        //Konstruktor
        public Jądro(MainWindow _mw)
        {
            MW = _mw;
            lista_obiektow = new List<Prostokat>();

        }

        //Określenie matrycy
        public void nowy_matryca(int i = 0)
        {
            nm = new nowa_matryca(this, i);
            nm.ShowDialog();

        }

        public void dodaj_matryce()
        {
            Matka = new Matryca(nm);
            wm = new widok_matryca(this, Matka.rozmiar_x, Matka.rozmiar_y);
            if (lista_obiektow.Count > 0)
                Sprawdź1();
            MW.label.Content = "Rozmiar matrycy: " + Matka.rozmiar_x + " x " + Matka.rozmiar_y + "\t Rzaz: " + odstep;

        }
        public void zmień_matryce(int x, int y)
        {
            Matka.EdytujMatrycę(x, y);
            wm.Close();
            wm = new widok_matryca(this, Matka.rozmiar_x, Matka.rozmiar_y);

            MW.label.Content = "Rozmiar matrycy: " + Matka.rozmiar_x + " x " + Matka.rozmiar_y + "\t Rzaz: " + odstep;
        }
        public void Sprawdź1()
        {
            MW.button_rozmiesc.IsEnabled = false;
            MW.RozmieśćMenu.IsEnabled = false;
            List<Prostokat> listaObiektówKtóreMieszcząSieNaMatrycach = new List<Prostokat>();
            foreach (var item in lista_obiektow)                             //sprawdzanie czy obiekt zmieści sie na matrycy, jesli tak dodajemy do listy
            {


                if (item.W <= Matka.rozmiar_x && item.H <= Matka.rozmiar_y)
                    listaObiektówKtóreMieszcząSieNaMatrycach.Add(item);

            }

            if (listaObiektówKtóreMieszcząSieNaMatrycach.Count != lista_obiektow.Count)  //sprawdzamy czy jakaś figura się nie mieści 
            {
                List<int> listaIDdoUsunięcia = new List<int>();
                var zapytanie = lista_obiektow.Except<Prostokat>(listaObiektówKtóreMieszcząSieNaMatrycach);
                foreach (var item in zapytanie) //szukanie i dodawanie id figur które sie nie mieszcza
                {
                    listaIDdoUsunięcia.Add(item.ID);
                }
                if (zapytanie.Count() != 0)// jesli lista nie jest pusta to znaczy ze sa takie fiugury i trzeba albo je usunac albo zmienic rozmiar matrycy
                {
                    foreach (var item in listaIDdoUsunięcia)
                    {
                        zMF = new Zła_MatrycaFigura(item, this, "Figura " + item + " o wymiarach " + lista_obiektow[item - 1].W + "x" + lista_obiektow[item - 1].H + " nie zmieści się \n na matrycy (" + Matka.rozmiar_x + "x" + Matka.rozmiar_y + ")." + " Co chcesz zrobić?", MW);
                        zMF.ShowDialog();


                    }

                }
            }
            if (sprawdź2() == 1)
            {
                MW.button_rozmiesc.IsEnabled = true;
                MW.RozmieśćMenu.IsEnabled = true;
                MW.InfoButton.Visibility = Visibility.Hidden;
            }
        }
        int sprawdź2()
        {

            List<Prostokat> listaObiektówKtóreMieszcząSieNaMatrycach = new List<Prostokat>();
            foreach (var item in lista_obiektow)
            {


                if (item.W <= Matka.rozmiar_x && item.H <= Matka.rozmiar_y)
                    listaObiektówKtóreMieszcząSieNaMatrycach.Add(item);

            }
            if (listaObiektówKtóreMieszcząSieNaMatrycach.Count != lista_obiektow.Count)
            {
                MW.InfoButton.Visibility = Visibility.Visible;
                return 0;
            }
            return 1;
        }
        internal void rozmiesc()
        {
            miksuj();
        }

        internal void test()
        {
            MW.textBox_komunikat.Text = "" + lista_obiektow.Count();
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
            if (Matka != null)
                Sprawdź1();
        }
        public void usuń_prostokąt(Prostokat prostokat)
        {
            lista_obiektow.Remove(prostokat);
            MW.dataGrid.DataContext = lista_obiektow;
            MW.dataGrid.Items.Refresh();

        }
        public void usuń_prostokąt(int id)
        {
            var obiektDoUsunięcia = (from figura in lista_obiektow
                                     where figura.ID == id
                                     select figura).First<Prostokat>();


            lista_obiektow.Remove(obiektDoUsunięcia);
            foreach (var item in lista_obiektow)
            {
                if (item.ID > id)
                    item.ID = item.ID - 1;
            }
            Prostokat.licznik = lista_obiektow.Count;
            MW.dataGrid.DataContext = lista_obiektow;
            MW.dataGrid.Items.Refresh();
            if (Matka != null)
                if (sprawdź2() == 1)
                {
                    MW.button_rozmiesc.IsEnabled = true;
                    MW.RozmieśćMenu.IsEnabled = true;
                    MW.InfoButton.Visibility = Visibility.Hidden;
                }

        }
        public void usuń_wszystkie()
        {
            lista_obiektow.Clear();
            Prostokat.licznik = 0;
            MW.dataGrid.DataContext = lista_obiektow;
            MW.dataGrid.Items.Refresh();

        }
        public void modyfikuj_prostokąt()
        {
            MW.dataGrid.DataContext = lista_obiektow;
            MW.dataGrid.Items.Refresh();

        }

        void UpdatePasek()
        {   
            Pasek.status.Dispatcher.Invoke(() => Pasek.status.Value++, DispatcherPriority.Background);
        }

        //miksowanko
        public void miksuj()
        {                
           int S = Convert.ToInt32(MW.textBox_Czas.Text);
           int sekundCzas = Math.Max(S, 10);
           bool zaleznosc_od_t = true;
           ParameterizedThreadStart pts = new ParameterizedThreadStart(go);
           Thread thr = new Thread(pts);

           if (lista_obiektow.Count < 4)
                zaleznosc_od_t = false;

           int m_x = Matka.rozmiar_x;
           int m_y = Matka.rozmiar_y;

           Pasek = new Widoki.pasek(sekundCzas);
           Pasek.Show();
           Pasek.status.Value = 0;

           List<int> tab_arg = new List<int>();
           tab_arg.Add(m_x);
           tab_arg.Add(m_y);
           tab_arg.Add(odstep);
       //    tab_arg.Add(sekundCzas);
           thr.Start(tab_arg);

           var startTime = DateTime.UtcNow;
           do
           {
              UpdatePasek();
              Task.Delay(400);
                  
           } while ((DateTime.UtcNow - startTime < TimeSpan.FromSeconds(sekundCzas) && zaleznosc_od_t) || NAJLEPSZE == null);

           status = false; //tutaj kończymy działanie rozmieszczania i blokujemy możliwość dostępu do blokady
           startTime = DateTime.UtcNow; 
           while (DateTime.UtcNow - startTime < TimeSpan.FromSeconds(1)){ }; //odczekujemy sekunde

           try
           {
              thr.Abort(); //kto nie zdążył się zakończyć jest niszczony - nie należy tak zabijać wątków, ale wydaje mi się, że
                                 //z punktu widzenia naszych obliczeń nie ma niebezpieczeństw (pracujemy na paru listach i tablicach), oprócz
                                 //blokady, w ramach której ustalamy najlepsze rozmieszczenie - tego wątku nie chciałbym ubić bo manipuluje zmienną
                                 //która zawiera wynik obliczeń. dlatego po upływie czasu ustalanie najlepszego rozmieszczenia jest blokowane
                                //i odczekujemy sekunde, aż te wątki, które już czekają na porównanie swojego wyniku z aktualnym najlepszym
                                //mogły się porównać. Potem następuje abort. - metoda trochę mało wyrafinowana (zdroworozsądkowa)
                                
           }
                
           catch(ThreadAbortException e)
           {

           }
           finally
           {

           }
                
           R = new Rysowanie(wm);
           R.Rysuj(wm, NAJLEPSZE);

           Pasek.Close();
           InfoBox();
 
            try { wm.Show(); }
            catch { }
            prz = new Przeciaganie(NAJLEPSZE, wm, this);

            MW.button_rozmiesc_Copy.IsEnabled = true;
            MW.button_rozmiesc_Copy1.IsEnabled = true;

        }
     
        public static void ustal_najlepsze(Rozmieszczenia roz)
        {
            
           if (NAJLEPSZE.Liczba_wykorzystanych_matryc > roz.Liczba_wykorzystanych_matryc)
           {
              NAJLEPSZE = roz;
           }
           else if (NAJLEPSZE.Liczba_wykorzystanych_matryc == roz.Liczba_wykorzystanych_matryc)
           {
                if (NAJLEPSZE.NajPowProNr < roz.NajPowProNr)
                {
                    NAJLEPSZE = roz;
                }
                else if (NAJLEPSZE.NajPowProNr == roz.NajPowProNr)
                {
                     if (NAJLEPSZE.WolPowNrMat < roz.WolPowNrMat)
                     {
                         NAJLEPSZE = roz;
                     }
                     else if (NAJLEPSZE.WolPowNrMat == roz.WolPowNrMat)
                     {
                          if (NAJLEPSZE.SumNajPowPro < roz.SumNajPowPro)
                              NAJLEPSZE = roz;
                     }
                }

           } 
        }
        
        public void InfoBox()
        {
            InfoOkno = new informacyjne();
            InfoOkno.textBox.Text = ("\nRozmieszczenie, szczegóły: \n" +
                        "Liczba wykorzystanych matryc: " + NAJLEPSZE.Liczba_wykorzystanych_matryc +
                        "\nNajwiększa wolna prostokatna powierzchnia: " + NAJLEPSZE.NajPowPro +
                        "\nNajwięszka wolna prostokątna powierzchnia na ostatniej matrycy: " + NAJLEPSZE.NajPowProNr+
                        "\nCałkowita wolna powierzchnia na ostatniej matrycy: " + NAJLEPSZE.WolPowNrMat +
                        "\nLiczba rozmieszczen: " + licznik +
                        "\nEdytowane manualnie: " + NAJLEPSZE.czyZmienaneRecznie+
                        "\nSzerokość matrycy: "+Matka.rozmiar_x+
                        "\nWysokość matrycy: "+ Matka.rozmiar_y+
                        "\nOdstęp (rzaz): "+odstep);
            InfoOkno.textBox.Text += ("\n********************************************************************\n ");
            InfoOkno.textBox.Text += NAJLEPSZE.wypisz() + "\n";
            InfoOkno.Show();
        }

        public static void wygeneruj_indeksy(List<int[]> li, int liczba_figur, int liczba_indeksowan, List<int[]> proponowane)
        {
            Random rand = new Random();
            int j, k;
            int i = proponowane.Count + 1; //indeks 0 jest zajęty przez indeksowanie zgodne z wprowadzoną kolejnością a następne proponowane.Count to indeksowania mające szanse być dobrymi, czyli łacznie losowe generowanie zaczynamy od indeksu proponowane.Count+1

            if(li.Count == 0)
            {
                li.Add(new int[liczba_figur]);
                for (int x = 0; x < liczba_figur; x++)
                    li[0][x] = x;
            }

            if (liczba_figur == 2)
            {
                li.RemoveAt(0);
                for (int x = 0; x < proponowane.Count; x++)
                    li.Add(proponowane[x]);
                
            }
            else  if(liczba_figur > 2)
            {
                for (int x = 0; x < proponowane.Count; x++)
                {
                    li.Add(proponowane[x]);
                }

                while (i < liczba_indeksowan) //chcemy generować tyle indeksowań ile jest rozmieszczanych figur, ale niektóre figury mogą się powtarzać, więc obliczamy ile jest różnych figur i liczbę indeksowań ustalamy na tę obliczoną wartość - dlatego w pętli porównuję i z liczbą indeksowań a nie figur
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
            

        }

        //argumenty: lista prostokątów (porównujemy ich pola) oraz indeksowanie_startowe mogą dotyczyć niepełnej listy prostokątów
        //dla przykładu: indeksowanie 1,4,6,8 - tylko takie prostokąty zostały do rozmieszczenie (pozostałe znalazły się już na pierwszej matrycy)
        public static List<int[]> indeksowania_proponowane(List<Prostokat> lp,int[] indeksowanie_startowe)
        {
            List<int[]> tmp_tab = new List<int[]>();
            int liczba_prostokatow = indeksowanie_startowe.Length;
            int[] tab = new int[liczba_prostokatow];
            int tmp;

            for (int i = 0; i < liczba_prostokatow; i++)
                tab[i] = indeksowanie_startowe[i];

            for (int i = 0; i < liczba_prostokatow; i++)
            {
                for (int j = liczba_prostokatow - 1; j > i; j--)
                {
                    if (lp[indeksowanie_startowe[j]].Pole > lp[indeksowanie_startowe[j - 1]].Pole)
                    {
                        tmp = tab[j];
                        tab[j] = tab[j - 1];
                        tab[j - 1] = tmp;
                    }
                }
            }

            int[] odwrotna_kolejnosc = new int[liczba_prostokatow];

            for (int i = 0; i < liczba_prostokatow; i++)
                odwrotna_kolejnosc[i] = tab[liczba_prostokatow-1-i];

            tmp_tab.Add(tab);
          //  tmp_tab.Add(odwrotna_kolejnosc);
            return tmp_tab;
        }

        public static void miksowanie_indeksow(List<Rozmieszczenia> lista_roz, List<int[]> lista_ind, int liczba_nowych_indeksowan)
        {
            int liczba_rozmieszczen = lista_roz[0].lokalizacja_figur.Length;
            int max_liczba_matryc = maksymalna_liczba_matryc(lista_roz);
            int rozmiar_matrycy = lista_roz[0].lista_matryc[0].rozmiar_x * lista_roz[0].lista_matryc[0].rozmiar_y;

            if(lista_roz.Count > 1)
            {
                Random rand = new Random();
                int[] prob_choice = new int[lista_roz.Count];
                int j, k, suma = 0;
               
                for (int i = 0; i < lista_roz.Count; i++)
                {
                    int g = 0;

                    for (; g < lista_roz[i].Liczba_wykorzystanych_matryc; g++)
                    {
                        suma += lista_roz[i].wolna_powierzchnia_matrycy(g);
                    }
                    int roznica = max_liczba_matryc - lista_roz[i].Liczba_wykorzystanych_matryc;
                    for (int h = 0; h < roznica; h++)
                    {
                        suma += roznica * rozmiar_matrycy;
                    }
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
        }
        
        public static int maksymalna_liczba_matryc(List<Rozmieszczenia> lr)
        {
            int liczba_matryc = 0;

            for(int i = 0; i < lr.Count; i++)
            {
                if (lr[i].Liczba_wykorzystanych_matryc > liczba_matryc) liczba_matryc = lr[i].Liczba_wykorzystanych_matryc;
            }

            return liczba_matryc;
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

                    if (dodaj == true)
                    {
                        nowe_indeksowanie[i] = r2.indeksy[j];
                        break;
                    }
                }
        
                j++;
            }

            mutacja_indeksowania(nowe_indeksowanie);

            return nowe_indeksowanie;
        }

        public static void mutacja_indeksowania(int[] indeksowanie)
        {
            int liczba_figur = indeksowanie.Length;

            if(liczba_figur > 1)
            {
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
        }
        
        public static void generuj_rozmieszczenia(List<Prostokat> lista_figur, List<int[]> lista_indeksow, List<Rozmieszczenia> lista_rozmieszczen, int liczba_figur, int m_rozmiar_x, int m_rozmiar_y,int odstep)
          {
               var sync = new Object();

               Parallel.For(0, lista_indeksow.Count, (int k) =>
               {
                  Rozmieszczenia roz = new Rozmieszczenia(liczba_figur, new Matryca(m_rozmiar_x, m_rozmiar_y),odstep,lista_indeksow[k]);

                  lock (sync)
                  {
                       lista_rozmieszczen.Add(roz);
                  }

                  bool czy_aktualizowac_indeksy = false;

                  for (int i = 0; i < liczba_figur; i++)
                   {
                      int j = 0;
                      roz.lokalizacja_figur[i] = new MatrycaFiguraPunkt(j, lista_figur[lista_indeksow[k][i]], new Punkt());

                      while (!lista_figur[lista_indeksow[k][i]].ustal_pozycje(roz.lokalizacja_figur[i].p, roz.lista_matryc[j],odstep))
                       {
                            if (j == roz.lista_matryc.Count - 1) roz.lista_matryc.Add(new Matryca(m_rozmiar_x, m_rozmiar_y));

                            j++;
                            czy_aktualizowac_indeksy = true;
                       }

                       roz.lokalizacja_figur[i].nr_matrycy = j;
                   }

                  if (czy_aktualizowac_indeksy) roz.zaktualizuj_liste_indeksow();
                   
                  roz.wolna_powierzchnia_ostatnia_matryca();
                  roz.najwieksza_prostokatna_powierzchnia();
                  roz.najwieksza_prostokatna_powierzchnia_nr_matrycy(roz.Liczba_wykorzystanych_matryc - 1);
                  roz.suma_NajPowPro();
              }) ;
        }
        
        public static void go(object tab_arg)
        {
            List<int> arg = (List<int>)tab_arg;
            int m_x = arg[0];
            int m_y = arg[1];
            int odstep = arg[2];
        
            var sync = new Object();
            int[] pierwsze_indeksowanie = new int[lista_obiektow.Count];
            for (int i = 0; i < lista_obiektow.Count; i++)
                pierwsze_indeksowanie[i] = i;

            int liczba_indeksowan = ile_roznych_prostokatow(lista_obiektow,pierwsze_indeksowanie);
            
            if (lista_obiektow.Count < 10)
                liczba_indeksowan *= liczba_indeksowan;
            else
                liczba_indeksowan *= (liczba_indeksowan + liczba_indeksowan);

            List<int[]> lista_indeksow = new List<int[]>();  
            wygeneruj_indeksy(lista_indeksow, lista_obiektow.Count, liczba_indeksowan, indeksowania_proponowane(lista_obiektow,pierwsze_indeksowanie));
            List<Rozmieszczenia> lista_roz2;

            do
            {
                lista_roz2 = new List<Rozmieszczenia>();
                generuj_rozmieszczenia(lista_obiektow, lista_indeksow, lista_roz2, lista_obiektow.Count, m_x, m_y, odstep);
                Rozmieszczenia best = znajdz_najlepsze(lista_roz2);
                if (status)
                {
                    lock (sync)
                    {
                        if (NAJLEPSZE == null) NAJLEPSZE = best;
                        else ustal_najlepsze(best);
                    }
                }
                
                lista_indeksow.Clear();
                miksowanie_indeksow(lista_roz2, lista_indeksow, liczba_indeksowan);

                licznik++; //pomocniczy licznik żeby wiedzieć ile pętli się wykonuje w określonym czasie
            } while (status);

        }

        public static Rozmieszczenia znajdz_najlepsze(List<Rozmieszczenia> lista)
        {
            Rozmieszczenia najlepsze = lista[0];

            for (int i = 0; i < lista.Count; i++)
            {
                if (najlepsze.Liczba_wykorzystanych_matryc > lista[i].Liczba_wykorzystanych_matryc)
                {
                    najlepsze = lista[i];
                }
                else if (najlepsze.Liczba_wykorzystanych_matryc == lista[i].Liczba_wykorzystanych_matryc)
                {
                    if(najlepsze.NajPowProNr < lista[i].NajPowProNr)
                    {
                        najlepsze = lista[i];
                    } else if(najlepsze.NajPowProNr == lista[i].NajPowProNr)
                    {
                        if(najlepsze.WolPowNrMat < lista[i].WolPowNrMat)
                        {
                            najlepsze = lista[i];
                        }
                        else if (najlepsze.WolPowNrMat == lista[i].WolPowNrMat)
                        {
                            if (najlepsze.SumNajPowPro < lista[i].SumNajPowPro)
                                najlepsze = lista[i];
                        }
                    }
                    
                }

            }

            return najlepsze;
        }

        public static int ile_roznych_prostokatow(List<Prostokat> lp,int [] tab_indeksow)
        {
            int[] liczydlo = new int[tab_indeksow.Length];

            for (int i = 0; i < liczydlo.Length; i++)
                liczydlo[i] = 0;

            for(int i = 0; i < liczydlo.Length;)
            {
                for(int j = 0; j < tab_indeksow.Length; j++)
                {
                    if (i != j && lp[tab_indeksow[i]].H == lp[tab_indeksow[j]].H && lp[tab_indeksow[i]].W == lp[tab_indeksow[j]].W)
                        liczydlo[j]++;

                }

                do { i++; } while (i < liczydlo.Length && liczydlo[i] != 0);
                
            }

            int ile_roznych = 0;
            for(int i = 0; i < liczydlo.Length; i++)
            {
                if (liczydlo[i] == 0) ile_roznych++;
            }

            return ile_roznych;
        }
        

    //Inne
    //zamkniecie programu
    internal void zamknij()
        {

            App.Current.Shutdown();

        }





    }
}
