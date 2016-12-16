using System;
using System.Windows.Forms;

namespace dodawanie_figur1
{
    public partial class Form_prostokat : Form
    {
        Akcje_widok AW;

        public Form_prostokat(Akcje_widok _aw)
        {

            InitializeComponent();
            AW = _aw;
        }

        private void button_ok_Click(object sender, EventArgs e)
        {
            if (textBox_height.Text == "" || textBox_width.Text == "")
                AW.MBNull();
            else
            {
                AW.dodaj_prost(this);
            }
        }

        private void button_anuluj_Click(object sender, EventArgs e)
        {
            Close();
        }


    }
}

