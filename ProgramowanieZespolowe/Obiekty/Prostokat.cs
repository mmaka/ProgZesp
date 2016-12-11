using dodawanie_figur1.Obiekty;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace dodawanie_figur1
{
    class Prostokat: Obiekt, IFigura
    {

        public Punkt punkt_zaczepienia;
        private Font drawFont = new Font("Arial", 10);

        public Prostokat(int width, int height,int _id, string nm= "Prostokąt")
        {
            
            name = nm;
            id = _id;
            w = width;
            h = height;
            punkt_zaczepienia = new Punkt(0, 0);
        }

        public int X { 
            get
            { return punkt_zaczepienia.x; }
        }

        public int Y
        {
            get
            { return punkt_zaczepienia.y; }
        }

        public int Pole
        {
            get
            { return (int)(w * h); }
        }

        public override string Name
        {
             get { return name; }
             
        }

       
        public override void move(int _x, int _y) {
            x = _x;
            y = _y;
            punkt_zaczepienia.x = (int)_x;
            punkt_zaczepienia.y = (int)_y;
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
            return punkt_zaczepienia.x + (int)w;
        }

        public int max_y()
        {
            return punkt_zaczepienia.y + (int)h;
        }

        public void aktualizacja_zajetosci_prostokat(int[][] zajetosc_x, int[][] zajetosc_y)
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

        public void rysuj(PaintEventArgs e)
        {

            Rectangle rect = new Rectangle((int)(punkt_zaczepienia.x),
                 (int)(punkt_zaczepienia.y),
                 (int)(W), (int)(H));

           
            e.Graphics.FillRectangle(Brushes.BurlyWood, rect);
            e.Graphics.DrawRectangle(Pens.Black, rect);

            e.Graphics.DrawString("ID: "+ID+"\n"+name, drawFont, 
                Brushes.Black, 
                (int)(punkt_zaczepienia.x+2), punkt_zaczepienia.y+2);
         }

        public override void rysujOB(object sender, PaintEventArgs e)
        {
            Rectangle rect = new Rectangle((int)(punkt_zaczepienia.x),
                 (int)(punkt_zaczepienia.y),
                 (int)(W), (int)(H));

            e.Graphics.FillRectangle(Brushes.LightSalmon, rect);
            e.Graphics.DrawRectangle(Pens.Red, rect);
           

            e.Graphics.DrawString("ID: " + ID + "\n" + name, drawFont,
               Brushes.Black,
               (int)(punkt_zaczepienia.x+2), punkt_zaczepienia.y+2);

        }


   }  
}




