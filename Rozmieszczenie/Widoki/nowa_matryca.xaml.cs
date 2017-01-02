using Rozmieszczenie.Logika;
using System.Windows;

namespace Rozmieszczenie.Widoki
{
    /// <summary>
    /// Interaction logic for nowa_matryca.xaml
    /// </summary>
    public partial class nowa_matryca : Window
    {
        Jądro J;
        int nm = 0;
        public nowa_matryca(Jądro j, int nm = 0)
        {
            J = j;            InitializeComponent();  this.nm = nm;
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            if (Sprawdz.CzyNull(textBox_matryca_szerokość, textBox_matryca_wysokość))
            {
                if (nm != 0)
                {
                    J.zmień_matryce(int.Parse(textBox_matryca_szerokość.Text), int.Parse(textBox_matryca_wysokość.Text));
                    Close();
                }
                else
                    J.dodaj_matryce();
            }
        }

        private void button_Copy_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
