using System.Windows.Forms;

namespace ProgramowanieZespolowe
{
    public partial class Form_Nowa_Matryca : Form
    {
        dodawanie_figur1.Akcje_widok AW;
        public Form_Nowa_Matryca(dodawanie_figur1.Akcje_widok _aw)
        {
            InitializeComponent();
            AW = _aw;
        }

        private void button_ok_Click(object sender, System.EventArgs e)
        {
            if (textBox_matrycaH.Text == "" || textBox_matrycaW.Text == "")
                AW.MBNull();
            else
                AW.nowa();
            Close();
        }

        private void button_anuluj_Click(object sender, System.EventArgs e)
        {
            Close();
        }
    }
}
