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
        public nowa_matryca(Jądro j)
        {
            J = j;            InitializeComponent();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            J.dodaj_matryce();
        }

        private void button_Copy_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
