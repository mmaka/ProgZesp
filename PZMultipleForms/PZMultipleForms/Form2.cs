using figury;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PZMultipleForms
{
    public partial class Form2 : Form
    {
        //----------------------------------------------------Zmienne
        bool draw = false;
        int x,y, actX, actY, actH, actW;
        int j = 0;
       
        Shape.Item curentItem;
        List<Shape> listaKsztalt = new List<Shape>();
        List<int> listaPoint = new List<int>();
       
        public enum Unit
        {
            mm,cm,dm,m
        }

        /* */
       public static IList<Prostokat> lista_obiektow = new List<Prostokat>();
       public static IList<Prostokat> wstawione_obiekty = new List<Prostokat>();
      public  Prostokat tmp_prostokat;
        /* */
        //---------------------------------------------------
        public Form2()
        {
            InitializeComponent();
            this.DoubleBuffered = true;


        }
        private void ellipseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            curentItem = Shape.Item.Ellipse;
            listaPoint.Clear();
        }

        private void Button_clear_Click(object sender, EventArgs e)
        {

            Graphics g = pictureBox1.CreateGraphics();
            g.Clear(Color.White);
            listaPoint.Clear();

        }

        private void dowolneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            curentItem = Shape.Item.Any;
            listaPoint.Clear();
           
        }

        private void Button_Add_Click(object sender, EventArgs e)
        {
            listaKsztalt.Add(new Shape(curentItem, listaPoint));
            listaPoint.Clear();

            /* */
            tmp_prostokat = new Prostokat(actW, actH);
            tmp_prostokat.f_id = (uint)j;
            j++;
            tmp_prostokat.punkt_zaczepienia = new Punkt(0, 0);
            lista_obiektow.Add(tmp_prostokat);
            /* */
            this.Close();
        }

        private void rectangleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            curentItem = Shape.Item.Rectangle;
            listaPoint.Clear();
        }

        private void Button_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            draw = false;
            Graphics g = pictureBox1.CreateGraphics();
            if (curentItem == Shape.Item.Any)
            {
                if (listaPoint.Count() > 0 && actX + 10 >= listaPoint.ElementAt(0) && actX - 10 <= listaPoint.ElementAt(0) && actY + 10 >= listaPoint.ElementAt(1) && actY - 10 <= listaPoint.ElementAt(1)) //jezeli ostatni punkt ostatniej prosej jest bardzo blisko tego pierwszego to automatycznie dodaje konicowe punkty  = początkowym , tak aby zamknąc figurę
                {
                    actX = listaPoint.ElementAt(0);
                    actY = listaPoint.ElementAt(1);

                    listaPoint.Add(x);
                    listaPoint.Add(y);
                    listaPoint.Add(actX);
                    listaPoint.Add(actY);
                    x = actX;
                    y = actY;

                    g.Clear(Color.White);
                    for (int i = 0; i < listaPoint.Count(); i += 4)
                    {
                        g.DrawLine(new Pen(Color.Red), (int)listaPoint.ElementAt(i), (int)listaPoint.ElementAt(i + 1), (int)listaPoint.ElementAt(i + 2), (int)listaPoint.ElementAt(i + 3));
                    }
                }
                else
                {
                    listaPoint.Add(x);
                    listaPoint.Add(y);
                    listaPoint.Add(actX);
                    listaPoint.Add(actY);
                    x = actX;
                    y = actY;
                }
            }
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (draw)
            {
                Graphics g = pictureBox1.CreateGraphics();


                g.Clear(Color.White);
           
                switch (curentItem)
                {
                    case Shape.Item.Rectangle:
                        actW = actW >= 0 ? actW : -(actW);
                        actH = actH >= 0 ? actH : -(actH);
                        toolStripStatusLabel1.Text = "Szerokość: " + actW + " | Wysokośc: "+ actH;
                       
                        if (e.Y < y)
                        {
                            actX = x;
                            actY = e.Y;
                            actW = e.X - x;
                            actH = y - e.Y;
                            g.FillRectangle(new SolidBrush(Color.Red), x, e.Y, e.X - x, y - e.Y);
                        }
                        if (e.X < x)
                        {
                            actX = e.X;
                            actY = y;
                            actW = x - e.X;
                            actH = e.Y - y;
                            g.FillRectangle(new SolidBrush(Color.Red), e.X, y, x - e.X, e.Y - y);
                        }
                        if (e.X < x && e.Y < y)
                        {
                            actX = e.X;
                            actY = e.Y;
                            actW = x - e.X;
                            actH = y - e.Y;
                            g.FillRectangle(new SolidBrush(Color.Red), e.X, e.Y, x - e.X, y - e.Y);
                        }
                        else
                        {
                            actX = x;
                            actY = y;
                            actW = e.X - x;
                            actH = e.Y - y;
                            g.FillRectangle(new SolidBrush(Color.Red), x, y, e.X - x, e.Y - y);
                        }

                        break;
                    case Shape.Item.Ellipse:
                        actX = x;
                        actY = y;
                        actW = e.X - x;
                        actH = e.Y - y;
                        g.FillEllipse(new SolidBrush(Color.Black), x, y, e.X - x, e.Y - y);

                        break;
                    case Shape.Item.Any:
                        //rysowanie wczyesniej nakreslonych lini
                        for (int i = 0; i < listaPoint.Count(); i+=4)
                        {
                            g.DrawLine(new Pen(Color.Red), (int)listaPoint.ElementAt(i), (int)listaPoint.ElementAt(i + 1), (int)listaPoint.ElementAt(i + 2), (int)listaPoint.ElementAt(i + 3));
                        }
                    


                        //koniec
                        actX = e.X; //wspolrzedne 2 punku danej prostej
                        actY = e.Y;
                        g.DrawLine(new Pen(Color.Red), x, y, e.X, e.Y);
                        
                        toolStripStatusLabel1.Text = dlugoscProstej(listaPoint) + " || Aktualna dlugość: " +dlugoscProstej(new List<int>() {x,y,actX,actY }) ; //wyswietlanie na progres bar dlugosci prostych
                        break;
                    
                }
                
                g.Dispose();
               
            }
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            draw = true;

            if (curentItem == Shape.Item.Any && listaPoint.Any())
            {
                if (listaPoint.ElementAt(1) == listaPoint.Last() && listaPoint.ElementAt(0) == listaPoint.ElementAt(listaPoint.Count() - 2)) // jeżeli figura zamknieta to zablokuj rysowanie
                    draw = false;
            }
            else {
                x = e.X;
                y = e.Y;
            }

        }
        private string dlugoscProstej(List<int> listaPoint)
        {
            string lancuch="";
            int iterator=0;
           double dlugosc;
            
            for(int i=0; i<listaPoint.Count()-3;i+=2)
            {
                dlugosc = Math.Pow((double)(((listaPoint.ElementAt(i) - listaPoint.ElementAt(i + 2)) * (listaPoint.ElementAt(i) - listaPoint.ElementAt(i + 2))) + ((listaPoint.ElementAt(i + 1) - listaPoint.ElementAt(i + 3)) * (listaPoint.ElementAt(i + 1) - listaPoint.ElementAt(i + 3)))), (double)0.5);
                if (dlugosc > 0)
                {
                    lancuch += iterator + ": " + Math.Round(dlugosc, 2).ToString() + " | ";
                    iterator++;
                }
            }
            return lancuch;
        }
    }
}
