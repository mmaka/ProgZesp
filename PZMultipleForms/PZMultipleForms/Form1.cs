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
    public partial class Form1 : Form
    {
        bool moveFigure_YN = false;
        public static int HMatryca,WMatryca;
        Shape currentMoveFigure;
        List<Shape> listFiguresOnmatrix;
        int positionMousestartX, positionMousestartY;
        Matryca m = new Matryca(600, 500);
        public Form1()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
        }

        private void dodajToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 FormaAdd = new Form2();
            FormaAdd.Show();
        }

        private void zamknijToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {

        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            positionMousestartX = e.X;
            positionMousestartY = e.Y;

          //  if (moveFigure_YN)
           // { }
        }

        private void pokażToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Form2.lista_obiektow.Count() > 0)
            {
                for (int i = 0; i < Form2.lista_obiektow.Count; i++)
                {

                    if (m.polozenie_poczatkowe_prostokat(Form2.lista_obiektow[i]) == true)
                    {
                        m.idz_max_w_gore(Form2.lista_obiektow[i]);
                        m.idz_max_w_prawo(Form2.lista_obiektow[i]);
                        m.idz_max_w_gore(Form2.lista_obiektow[i]);
                        Form2.lista_obiektow[i].aktualizacja_zajetosci_prostokat(m.zajetosc_x, m.zajetosc_y);
                        Form2.wstawione_obiekty.Add(Form2.lista_obiektow[i]);

                    }
                    else Console.WriteLine("Problem!");

                }
            }

            Graphics g = pictureBox1.CreateGraphics();

            for (int i = 0; i < Form2.wstawione_obiekty.Count(); i++)
            {
                g.FillRectangle(new SolidBrush(Color.Red), Form2.wstawione_obiekty.ElementAt(i).punkt_zaczepienia.x, Form2.wstawione_obiekty.ElementAt(i).punkt_zaczepienia.y, Form2.wstawione_obiekty.ElementAt(i).szerokosc, Form2.wstawione_obiekty.ElementAt(i).wysokosc);
            }
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            //if (e.Button == MouseButtons.Left && moveFigure_YN==true) {
               
                
                    
            
           // }
        }
    }
}
