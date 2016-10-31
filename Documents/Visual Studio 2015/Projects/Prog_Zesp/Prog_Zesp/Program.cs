using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prog_Zesp
{
    class Program
    {
        static void Main(string[] args)
        {
            IList<Obj> lista_obiektow = new List<Obj>();
            Obj tmp;

            Console.WriteLine("Podaj liczbe obiektow: ");
            int liczba_obiektow = Convert.ToInt32(Console.ReadLine());
            for (int j = 0; j < liczba_obiektow; j++)
            {

                Console.WriteLine("Podaj liczbe wierzcholkow obiektu: ");
                int liczba_wierzch = Convert.ToInt32(Console.ReadLine());
                float[][] tab_wsp = new float[liczba_wierzch][];

                for (int i = 0; i < liczba_wierzch; i++)
                {
                    tab_wsp[i] = new float[2];
                          
                }

                for (int i = 0; i < liczba_wierzch; i++)
                {
               
                    Console.WriteLine("Podaj wspolrzedna x: ");
                    tab_wsp[i][0] = (float)Convert.ToDouble(Console.ReadLine());
                    Console.WriteLine("Podaj wspolrzedna y: ");
                    tab_wsp[i][1] = (float)Convert.ToDouble(Console.ReadLine());
                }
            
                tmp.obj = new Obiekt(tab_wsp);
                tmp.id = (uint)j;   
                lista_obiektow.Add(tmp);
            }

            Console.WriteLine("Podaj rozmiar x matrycy: \n");
            float m_x = (float)Convert.ToDouble(Console.ReadLine());
            Console.WriteLine("Podaj rozmiar y matrycy: \n");
            float m_y = (float)Convert.ToDouble(Console.ReadLine());

            Matryca m = new Matryca(m_x, m_y);
            Console.WriteLine("Podaj dokladnosc modelowania.\n");
            float dokladnosc = (float)Convert.ToDouble(Console.ReadLine());

            Tablica_zajetosci tab_zaj = new Tablica_zajetosci(dokladnosc, m);
   
            if (tab_zaj.wyznacz_polozenie(lista_obiektow, m) == true)
            {
                tab_zaj.drukuj_tablice();
            }

            Console.ReadLine();
        }
    }


    public struct Obj
    {
        public Obiekt obj;
        public uint id;
    }

    public class Obiekt
    {
        public uint liczba_wierzcholkow;
        public float[][] wsp_wierzcholkow;
        public Obiekt()
        {
            liczba_wierzcholkow = 0;
            wsp_wierzcholkow = null;
        }

        public Obiekt(float[][] wierzch)
        {
            liczba_wierzcholkow = (uint)wierzch.Length;
            wsp_wierzcholkow = new float[liczba_wierzcholkow][];

            for (int i = 0; i < liczba_wierzcholkow; i++)
            {
                wsp_wierzcholkow[i] = new float[2];
            }
            
            for (int i = 0; i < liczba_wierzcholkow; i++)
            {
                wsp_wierzcholkow[i][0] = wierzch[i][0];
                wsp_wierzcholkow[i][1] = wierzch[i][1];
            }
        }
        public float dlugosc_krawedzi(uint nr)
        {
            if (nr == liczba_wierzcholkow)
            {
                return (float)System.Math.Sqrt((System.Math.Pow(wsp_wierzcholkow[nr - 1][0] - wsp_wierzcholkow[0][0], 2) + System.Math.Pow(wsp_wierzcholkow[nr - 1][1] - wsp_wierzcholkow[0][1], 2)));
            }
            else
            {
                return (float)System.Math.Sqrt((System.Math.Pow(wsp_wierzcholkow[nr - 1][0] - wsp_wierzcholkow[nr][0], 2) + System.Math.Pow(wsp_wierzcholkow[nr - 1][1] - wsp_wierzcholkow[nr][1], 2)));
            }
        }

        public float najdluzsza_krawedz()
        {
            float najdluzsza = 0f;
            float tmp;

            for (int i = 0; i < liczba_wierzcholkow; i++)
            {
                if (i == liczba_wierzcholkow)
                {
                    tmp = (float)System.Math.Sqrt((System.Math.Pow(wsp_wierzcholkow[i][0] - wsp_wierzcholkow[0][0], 2) + System.Math.Pow(wsp_wierzcholkow[i][1] - wsp_wierzcholkow[0][1], 2)));

                    if (tmp > najdluzsza)
                    {
                        najdluzsza = tmp;
                    }
                }
                else
                {
                    tmp = (float)System.Math.Sqrt((System.Math.Pow(wsp_wierzcholkow[i][0] - wsp_wierzcholkow[i + 1][0], 2) + System.Math.Pow(wsp_wierzcholkow[i][1] - wsp_wierzcholkow[i + 1][1], 2)));

                    if (tmp > najdluzsza)
                    {
                        najdluzsza = tmp;
                    }
                }
            }

            return najdluzsza;
        }
        public float dlugosc_wzdluz_x()
        {
            float wsp_x = 0;

            for (int i = 0; i < liczba_wierzcholkow; i++)
            {
                if (wsp_x < wsp_wierzcholkow[i][0])
                {
                    wsp_x = wsp_wierzcholkow[i][0];

                }
            }

            return wsp_x;
        }

        public float dlugosc_wzdluz_y()
        {
            float wsp_y = 0;

            for (int i = 0; i < liczba_wierzcholkow; i++)
            {
                if (wsp_y < wsp_wierzcholkow[i][1])
                {
                    wsp_y = wsp_wierzcholkow[i][1];

                }
            }

            return wsp_y;
        }

        /*   class Pojemnik
           {
               private float skala; //k jest to rozmiar obiektu reprezentowany przez jedna jednostke pojemnika
               private uint[][] model;
               public Pojemnik(Obiekt obj,float wsp = 1)
               {
                   skala = wsp;
                   uint x = (uint)(-0.1 + obj.dlugosc_wzdluz_x() / skala) + 1;
                   uint y = (uint)(-0.1 + obj.dlugosc_wzdluz_y() / skala) + 1;
                   model = new uint[x][];

                   for (int i = 0; i < x; i++)
                   {
                       model[i] = new uint[y];
                   }

                   for (int i = 0; i < x; i++)
                   {
                       for (int j = 0; j < y; j++)
                       {
                           model[i][j]=
                       }

                   }
               }


           }
           */
    }

     public class Matryca
        {

             private float rozmiar_x;
             private float rozmiar_y;
             public float biezacy_x;
             public float biezacy_y;

        public Matryca() { }

        public Matryca(float x,float y)
        {
            rozmiar_x = x;
            rozmiar_y = y;
            biezacy_x = 0;
            biezacy_y = 0;
        }

        public float calkowita_dlugosc_x()
        {
            return rozmiar_x;
        }
        public float calkowita_dlugosc_y()
        {
            return rozmiar_y;
        }
        public float dlugosc_x_do_konca()
        {
            return rozmiar_x - biezacy_x;
        }
        public float dlugosc_y_do_konca()
        {
            return rozmiar_y - biezacy_y;
        }
        public void ustaw_biezacy_x(float p)
        {
            biezacy_x = p;
        }
        public void ustaw_biezacy_y(float p)
        {
            biezacy_y = p;
        }

    }
    
    class Tablica_zajetosci
    {
        private uint liczba_wolnych;
       // private uint liczba_zajetych;
        private uint rozmiar_x;
        private uint rozmiar_y;
        private uint[][] tablica;
        public int aktualny_x;
        public int aktualny_y;
        private float dokladnosc;

        public Tablica_zajetosci(float skala,Matryca m)
        {

            rozmiar_x = (uint)(-0.1 + m.calkowita_dlugosc_x() / skala) + 1;
            rozmiar_y = (uint)(-0.1 + m.calkowita_dlugosc_y() / skala) + 1;
            tablica = new uint[rozmiar_x][];
            
            for (int i = 0; i < rozmiar_x; i++)
            {
                tablica[i] = new uint[rozmiar_y];
            }

            liczba_wolnych = rozmiar_x * rozmiar_y;
        //    liczba_zajetych = 0;
            aktualny_x = 0;
            aktualny_y = 0;
            dokladnosc = skala;

            for (int i = 0; i < rozmiar_x; i++)
            {
                for (int j = 0; j < rozmiar_y; j++)
                {
                    tablica[i][j] = 0;

                }

            }
            
        }

        public void drukuj_tablice()
        {

            for (int i = 0; i < rozmiar_x; i++)
            {

                for (int j = 0; j < rozmiar_y; j++)
                {

                    Console.Write("{0}",tablica[i][j]);
                   
                }

                Console.Write("\n");


            }

            Console.Write("\n");


        }

        public void Wypelnij_tablice(int start_x, int start_y, int zakres_x, int zakres_y, uint wartosc)
        {

            for (int i = start_x; i < start_x + zakres_x; i++)
            {

                for (int j = start_y; j < start_y + zakres_y; j++)
                {
                    tablica[i][j] = wartosc;

                }
            }
                aktualny_x += zakres_x;
            
        }


        public bool Przeszukaj_tablice(int start_x, int start_y, int zakres_x, int zakres_y, float dlugosc_x, float dlugosc_y)
        {

            if (start_x + zakres_x > dlugosc_x || start_y + zakres_y > dlugosc_y)
            {

                Console.WriteLine("Blad zakresu podczas przeszukiwania tablicy.");
                return false;

            }
            else
            {

                for (int i = start_x; i < start_x+zakres_x; i++)
                {

                    for (int j = start_y; j < start_y+zakres_y; j++)
                    {

                        if (tablica[i][j] != 0)
                            return false;

                    }

                }
            }

            return true;
        }

       public int nastepny_y()
        {

            if (aktualny_y + 1 <= rozmiar_y)
            {

                for (int i = 0; i < rozmiar_x; i++)
                {

                    for (int j = aktualny_y + 1; j < rozmiar_y; j++)
                    {

                        if (tablica[i][j] == 0)
                            return j;

                    }


                }
            }
            return (int)rozmiar_y - 1;

        }

        public bool wyznacz_polozenie(IList<Obj> obiekty, Matryca mat){

            IList<Obj> tmp = obiekty;
            
         for(int i = 0; i< tmp.Count; i++) { 

                int zakres1 = (int)(-0.1 + (tmp[i].obj).dlugosc_wzdluz_x() / dokladnosc) + 1;
                int zakres2 = (int)(-0.1 + ((tmp[i].obj)).dlugosc_wzdluz_y() / dokladnosc) + 1;
                
                if (((tmp[i].obj).dlugosc_wzdluz_x()<= mat.dlugosc_x_do_konca()) && (Przeszukaj_tablice(aktualny_x, aktualny_y,zakres1,zakres2,mat.calkowita_dlugosc_x(),mat.calkowita_dlugosc_y())==true)){

                    Wypelnij_tablice(aktualny_x, aktualny_y,zakres1,zakres2,tmp[i].id+1);			
		           	mat.ustaw_biezacy_x(mat.biezacy_x+(tmp[i].obj).dlugosc_wzdluz_x());
                    
                } else	if(((tmp[i].obj).dlugosc_wzdluz_y()<= mat.dlugosc_x_do_konca()) && (Przeszukaj_tablice(aktualny_x, aktualny_y,zakres2,zakres1,mat.calkowita_dlugosc_x(),mat.calkowita_dlugosc_y())==true)){

                    Wypelnij_tablice(aktualny_x, aktualny_y,zakres2,zakres1,tmp[i].id+1);
			        mat.ustaw_biezacy_x(mat.biezacy_x+(tmp[i].obj).dlugosc_wzdluz_y());

                } else {

        			aktualny_x=0;
		        	aktualny_y = nastepny_y();
                    
			        mat.ustaw_biezacy_x(0.0f);

                    if ((mat.biezacy_y+(mat.calkowita_dlugosc_y())/dokladnosc) <= mat.calkowita_dlugosc_y())
				        mat.ustaw_biezacy_y(mat.biezacy_y+(mat.calkowita_dlugosc_y()));

                    i--;//po to by nie opuscic danego obiektu bez umieszczenia go na matrycy
                }
	


	}

return true;

}



    }
}
