using System;
using System.Windows.Forms;

namespace dodawanie_figur1
{
    public partial class Form_matryca : Form
    {
        Akcje_widok AW;
        bool czy_Przesuwać = false;
        public Form_matryca(float X, float Y, Akcje_widok _aw)
        {
            InitializeComponent();
            AW = _aw;
            pictureBox1.Width = (int)X;
            pictureBox1.Height = (int)Y;            
            pictureBox1.Show();
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
           
            if (e.Button == MouseButtons.Left)
            {
                Cursor.Current = Cursors.Hand;
                czy_Przesuwać = true;
                AW.klikniecie_ktora_figura(e);
                AW.przemiesc(e);
            }
           
        }

        private void Form_matryca_Load(object sender, EventArgs e)
        {
            DoubleBuffered = true;
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
        
    }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (czy_Przesuwać)
                AW.przemiesc(e);
            else
            {
                
                AW.Chmurka(e);
            }

        }

        private void button_zapisz_Click(object sender, EventArgs e)
        {
            AW.zapiszObraz();
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            czy_Przesuwać = false;
            AW.unselect();

        }

        private void button_drukuj_Click(object sender, EventArgs e)
        {
            AW.Drukuj();
        }
    }
}
