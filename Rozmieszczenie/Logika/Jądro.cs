using System;
using Rozmieszczenie.Widoki;
using System.Collections.Generic;
using System.Windows;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace Rozmieszczenie.Logika
{
    public class Jądro
    {
        nowa_prostokat np;
        nowa_matryca nm;        
        MainWindow MW;
        Zła_MatrycaFigura zMF;

        public int odstep = 5; //tutaj ręcznie możemy ustawić rozmiar odstępu
        public widok_matryca wm;
        public Matryca Matka;
        public informacyjne InfoOkno;
        public Rysowanie R;
        public Rozmieszczenia NAJLEPSZE;
        public Przeciaganie prz; 
        public static List<Prostokat> lista_obiektow;
        public static int licznik = 0;
        public List<Rozmieszczenia> lista_rozmieszczen;
        //Konstruktor
        public Jądro(MainWindow _mw)
        {
            MW = _mw;           
            lista_obiektow = new List<Prostokat>();
           
        }

        //Określenie matrycy
        public void nowy_matryca(int i=0)
        {
            nm = new nowa_matryca(this,i);
            nm.ShowDialog();

        }      

        public void dodaj_matryce()
        {
            Matka = new Matryca(nm);            
            wm = new widok_matryca(this, Matka.rozmiar_x, Matka.rozmiar_y);
            if(lista_obiektow.Count>0)
            Sprawdź1();
            MW.label.Content = "Rozmiar matrycy: " + Matka.rozmiar_x + " x " + Matka.rozmiar_y;
              
        }
        public void zmień_matryce(int x, int y)
        {
            Matka.EdytujMatrycę(x, y);
            wm.Close();
            wm = new widok_matryca(this, Matka.rozmiar_x, Matka.rozmiar_y);
           
            MW.label.Content = "Rozmiar matrycy: " + Matka.rozmiar_x + " x " + Matka.rozmiar_y;
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
                            zMF = new Zła_MatrycaFigura(item, this, "Figura " + item + " o wymiarach "+lista_obiektow[item-1].W+"x"+ lista_obiektow[item-1].H + " nie zmieści się \n na matrycy (" +Matka.rozmiar_x+"x"+Matka.rozmiar_y+")."+" Co chcesz zrobić?", MW);
                            zMF.ShowDialog();


                        }
                       
                    }
                }
            if (sprawdź2()==1)
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
            MW.textBox_komunikat.Text = ""+lista_obiektow.Count();
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
            if(Matka !=null)
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


        //miksowanko
        public void miksuj()
        {

            {
                
                int S = Convert.ToInt32(MW.textBox_Czas.Text);
                int sekundCzas = Math.Max(S, 10);
                

                int m_x = Matka.rozmiar_x;
                int m_y = Matka.rozmiar_y;
                pasek Pasek = new Widoki.pasek(sekundCzas);
                Pasek.Show();
                Pasek.status.Value = 0;

                NAJLEPSZE = go(m_x, m_y, odstep,sekundCzas,Pasek);
            
                {
                    R = new Rysowanie(wm);
                    R.Rysuj(wm, NAJLEPSZE);
                }
                Pasek.Close();
                InfoBox();
            }
            try { wm.Show(); }
            catch { }
            prz = new Przeciaganie(NAJLEPSZE, wm, this);
            
        }

        
        public void InfoBox()
        {
            InfoOkno = new informacyjne();
            InfoOkno.textBox.Text = ("\nRozmieszczenie, szczegóły: \n" +
                        "Liczba wykorzystanych matryc: " + NAJLEPSZE.Liczba_wykorzystanych_matryc +
                        "\nNajwiększa wolna prostokatna powierzchnia: " + NAJLEPSZE.NajPowPro2 +
                        "\nLiczba rozmieszczen: "+ licznik +
                        "\nEdytowane manualnie: " + NAJLEPSZE.czyZmienaneRecznie);
            InfoOkno.textBox.Text += ("\n********************************************************************\n ");
            InfoOkno.textBox.Text += NAJLEPSZE.wypisz() + "\n";
            InfoOkno.Show();
        }


        //Metody Michała służące całej logice rozmieszczania i wszystkiego innego narazie tutaj ale nie na zawsze


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
            
            
            if(liczba_figur > 1)
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
            tmp_tab.Add(odwrotna_kolejnosc);
            return tmp_tab;
        }

        public static void miksowanie_indeksow2(List<Rozmieszczenia> lista_roz, List<int[]> lista_ind, int liczba_nowych_indeksowan)
        {
            int liczba_rozmieszczen = lista_roz[0].lokalizacja_figur.Length;
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
        //lalalala
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
/*
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
               
               for (int i = 0; i < liczba_figur; i++)
               {
                   int j = 0;
                   roz.lokalizacja_figur[i] = new MatrycaFiguraPunkt(j, lista_figur[lista_indeksow[k][i]], new Punkt());

                   while (!lista_figur[lista_indeksow[k][i]].ustal_pozycje(roz.lokalizacja_figur[i].p, roz.lista_matryc[j],odstep))
                   {
                       if (j == roz.lista_matryc.Count - 1) roz.lista_matryc.Add(new Matryca(m_rozmiar_x, m_rozmiar_y));

                       j++;
                   }

                   roz.lokalizacja_figur[i].nr_matrycy = j;
               }

               roz.wolna_powierzchnia_matrycy(roz.Liczba_wykorzystanych_matryc - 1);
               roz.najwieksza_prostokatna_powierzchnia();
           }) ;
        }
  */  
        public static Rozmieszczenia go(int m_x,int m_y,int odstep,int ile_sekund,pasek Pasek)
        {
            
            int[] pierwsze_indeksowanie = new int[lista_obiektow.Count];
            for (int i = 0; i < lista_obiektow.Count; i++)
                pierwsze_indeksowanie[i] = i;

            int liczba_indeksowan = ile_roznych_prostokatow(lista_obiektow,pierwsze_indeksowanie);
            liczba_indeksowan *= 20;

            List<int[]> lista_indeksow = new List<int[]>();  
            wygeneruj_indeksy(lista_indeksow, lista_obiektow.Count, liczba_indeksowan, indeksowania_proponowane(lista_obiektow,pierwsze_indeksowanie));
            
            bool zaleznosc_od_t = true;
            var startTime = DateTime.UtcNow; //tutaj pobieramy aktualny czas
            List<Rozmieszczenia> lista_roz2;

            if (lista_obiektow.Count < 3)
                zaleznosc_od_t = false;
            do
            {
                lista_roz2 = new List<Rozmieszczenia>();
                rozmieszczanie(lista_roz2, lista_obiektow, lista_indeksow, lista_obiektow.Count, m_x, m_y, odstep);
                
                lista_indeksow.Clear();
                miksowanie_indeksow2(lista_roz2, lista_indeksow, liczba_indeksowan);

                Pasek.status.Dispatcher.Invoke(() => Pasek.status.Value = (DateTime.UtcNow - startTime).Seconds, DispatcherPriority.Background);
                //Pasek.status.Dispatcher.Invoke(() => Pasek.status.Value++, DispatcherPriority.Background);
                licznik++; //pomocniczy licznik żeby wiedzieć ile pętli się wykonuje w określonym czasie
                
            } while (DateTime.UtcNow - startTime < TimeSpan.FromSeconds(ile_sekund) && zaleznosc_od_t);

            Rozmieszczenia NAJLEPSZE = lista_roz2[0];
            for (int i = 0; i < lista_roz2.Count; i++)
            {
                if (NAJLEPSZE.Liczba_wykorzystanych_matryc > lista_roz2[i].Liczba_wykorzystanych_matryc)
                    NAJLEPSZE = lista_roz2[i];
                else if (NAJLEPSZE.Liczba_wykorzystanych_matryc == lista_roz2[i].Liczba_wykorzystanych_matryc)
                {
                    if (NAJLEPSZE.SumNajPowPro < lista_roz2[i].SumNajPowPro)
                        NAJLEPSZE = lista_roz2[i];
                }

            }
            return NAJLEPSZE;
        }


        public static void rozmieszczanie(List<Rozmieszczenia> lista_rozmieszczen,List<Prostokat> lista_figur,List<int[]> lista_indeksow,int liczba_figur,int m_rozmiar_x,int m_rozmiar_y,int odstep)
        {
            var sync = new Object();

            Parallel.For(0, lista_indeksow.Count, (int k) =>
            {
                Rozmieszczenia r;
                r = start(lista_figur,liczba_figur,lista_indeksow[k],m_rozmiar_x,m_rozmiar_y,odstep);
                lock (sync)
                {
                    lista_rozmieszczen.Add(r);
                }
            });

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

        public static Rozmieszczenia start(List<Prostokat> lista_prostokatow,int liczba_figur,int[] indeksowanie,int m_rozmiar_x,int m_rozmiar_y,int odstep)
        {
            int nr_matrycy = 0;
            int liczba_prostokatow = liczba_figur;
            int nr_pierwszego_prostokata = 0;
            

            Rozmieszczenia roz = new Rozmieszczenia(liczba_prostokatow, new Matryca(m_rozmiar_x, m_rozmiar_y), odstep);

            List<int> indeksy_kolejna_matryca = new List<int>();
            List<int[]> lista_indeksowan = new List<int[]>();

            do
            {
                indeksy_kolejna_matryca = roz.generowanie_rozmieszczenia(liczba_prostokatow,lista_prostokatow, indeksowanie,nr_matrycy,nr_pierwszego_prostokata);
                
                if(indeksy_kolejna_matryca.Count != 0)
                {
                    roz.lista_matryc.Add(new Matryca(m_rozmiar_x, m_rozmiar_y));
                    nr_matrycy++;
                    nr_pierwszego_prostokata = liczba_figur - indeksy_kolejna_matryca.Count;
                    liczba_prostokatow = indeksy_kolejna_matryca.Count;
                    indeksowanie = new int[liczba_prostokatow];
                    for (int i = 0; i < liczba_prostokatow; i++)
                        indeksowanie[i] = indeksy_kolejna_matryca[i];

                    int liczba_indeksowan = ile_roznych_prostokatow(lista_obiektow, indeksowanie);

                    lista_indeksowan.Clear();
                    lista_indeksowan.Add(indeksowanie);
                    wygeneruj_indeksy(lista_indeksowan, liczba_prostokatow, liczba_indeksowan, indeksowania_proponowane(lista_obiektow,indeksowanie));
                    var startTime = DateTime.UtcNow;
                    List<Rozmieszczenia> lista_roz2 = new List<Rozmieszczenia>();
                    do
                    {
                        lista_roz2.Clear();
                        rozmieszczanie(lista_roz2,lista_prostokatow, lista_indeksowan,liczba_prostokatow, m_rozmiar_x, m_rozmiar_y, odstep);
                        lista_indeksowan.Clear();
                        miksowanie_indeksow2(lista_roz2, lista_indeksowan, liczba_indeksowan);

                    } while (DateTime.UtcNow - startTime < TimeSpan.FromSeconds(30)); //tutaj czas jest ustalony ręcznie, ale będzie to zmienione

                    Rozmieszczenia NAJLEPSZE = lista_roz2[0];

                    for (int i = 0; i < lista_roz2.Count; i++)
                    {
                        if(NAJLEPSZE.Liczba_wykorzystanych_matryc > lista_roz2[i].Liczba_wykorzystanych_matryc)
                            NAJLEPSZE = lista_roz2[i];
                        else if(NAJLEPSZE.Liczba_wykorzystanych_matryc == lista_roz2[i].Liczba_wykorzystanych_matryc)
                        {
                            if (NAJLEPSZE.SumNajPowPro < lista_roz2[i].SumNajPowPro)
                                NAJLEPSZE = lista_roz2[i];
                        }
                        
                    }

                    for (int i = 0; i < liczba_prostokatow; i++)
                        indeksowanie[i] = NAJLEPSZE.indeksy[i];
                    
                }
            } while (indeksy_kolejna_matryca.Count != 0);


            roz.wolna_powierzchnia_matrycy(roz.Liczba_wykorzystanych_matryc - 1);
            roz.najwieksza_prostokatna_powierzchnia();
            roz.suma_NajPowPro();

            return roz;
        }

        

    //Inne
    //zamkniecie programu
    internal void zamknij()
        {

            App.Current.Shutdown();

        }





    }
}
