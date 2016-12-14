using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace dodawanie_figur1
{
    public partial class Form1 : Form
    {

        Akcje_widok AW;
        public Form1()
        {           
            InitializeComponent();
             AW = new Akcje_widok(this);
           
        }
        

        private void button_dodaj_Click(object sender, EventArgs e)
        {
            AW.dodaj();                           
        }

        private void button_Rysuj_Click(object sender, EventArgs e)
        {
            AW.rysuj();
        }

      

        

        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            AW.rysuj();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            AW.rysuj();
        }

        private void button_usun_Click(object sender, EventArgs e)
        {
            AW.usun();
        }

        private void button_rozmiesc_Click(object sender, EventArgs e)
        {
            AW.rozmiesc();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button_unSel_Click(object sender, EventArgs e)
        {
            AW.unselect();
        }

        private void zakończToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void nowaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AW.nowaM();

        }
    }
}
