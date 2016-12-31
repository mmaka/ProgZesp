using System;
using System.ComponentModel;
using Rozmieszczenie.Widoki;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Rozmieszczenie.Logika
{
    public class Jądro
    {
        nowa_prostokat np;
        nowa_matryca nm;
        widok_matryca wm;
        MainWindow MW;
        Matryca Matka;
        NarzędziaInne NI;
        public static List<Prostokat> lista_obiektow = new List<Prostokat>();
        public static List<Matryca> listaStworzonychMatryc = new List<Matryca>();
        public List<Rozmieszczenia> lista_rozmieszczen = new List<Rozmieszczenia>();
        //Konstruktor
        public Jądro(MainWindow _mw)
        {
            MW = _mw;
            NI = new NarzędziaInne(this);
           
        }

        //Określenie matrycy
        public void nowy_matryca()
        {
            nm = new nowa_matryca(this);
            nm.ShowDialog();

        }      

        public void dodaj_matryce()
        {
            Matka = new Matryca(nm);
            wm = new widok_matryca(this);
            {
                wm.imageGrid.Width = Matka.rozmiar_x;
                wm.imageGrid.Height = Matka.rozmiar_y;
                wm.image.Width = Matka.rozmiar_x;
                wm.image.Height = Matka.rozmiar_y;
                wm.image.MinHeight = wm.image.Height;
                wm.image.MinWidth = wm.image.Width;
                wm.InvalidateVisual();
                wm.UpdateLayout();
                
            }
            listaStworzonychMatryc.Add(Matka);
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
        public void usuń_prostokąt(Prostokat prostokat)
        {
            lista_obiektow.Remove(prostokat);
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
            int m_x = Matka.rozmiar_x;
            int m_y = Matka.rozmiar_y;
            int liczba_indeksowan = 30;
            List<int[]> lista_indeksow = new List<int[]>();

            wygeneruj_indeksy(lista_indeksow, lista_obiektow.Count, liczba_indeksowan);
            generuj_rozmieszczenia(lista_obiektow, lista_indeksow, lista_rozmieszczen, lista_obiektow.Count, m_x, m_y);

            //    foreach (Rozmieszczenie roz in lista_rozmieszczen)
            //      roz.wypisz();
            int w = 0;
            do
            {
                selekcja(lista_rozmieszczen, 15);
                List<int[]> lista_ind = new List<int[]>();
                List<Rozmieszczenia> lista_roz2 = new List<Rozmieszczenia>();
                miksowanie_indeksow(lista_rozmieszczen, lista_ind, liczba_indeksowan);
                generuj_rozmieszczenia(lista_obiektow, lista_ind, lista_roz2, lista_obiektow.Count, m_x, m_y);

                if (w + 1 == liczba_indeksowan)
                {
                    for (int i = 0; i < lista_roz2.Count; i++)
                       MW.textBox_rozmieszczenia.Text +=("\nRozmieszczenie"+ (i + 1) +":  Prostokatna powierzchnia: "+lista_roz2[i].NajPowPro / 2001+", wolna powierzchnia: "+  lista_roz2[i].WolPowNrMat);

                    for (int i = 0; i < lista_roz2.Count; i++)
                    {
                        MW.textBox_rozmieszczenia.Text+=("********************************************************************\n");
                        MW.textBox_rozmieszczenia.Text+=("Rozmieszczenie nr "+ (i+1));
                        MW.textBox_rozmieszczenia.Text+=lista_roz2[i].wypisz()+"\n";
                        
                    }

                }

                w++;

            } while (w < liczba_indeksowan);
        }



        //Metody Michała służące całej logice rozmieszczania i wszystkiego innego narazie tutaj ale nie na zawsze

        public static void wygeneruj_indeksy(List<int[]> li, int liczba_figur, int liczba_indeksowan)
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
            Parallel.For(0, lista_indeksow.Count, (int k) =>
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
            });
        }
        

        //Inne

        //zamkniecie programu
        internal void zamknij()
        {
            wm.Close();
        }





    }
}
