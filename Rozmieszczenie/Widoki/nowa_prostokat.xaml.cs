using Rozmieszczenie.Logika;
using System.Windows;

namespace Rozmieszczenie.Widoki
{
    /// <summary>
    /// Interaction logic for nowa_prostokat.xaml
    /// </summary>
    public partial class nowa_prostokat : Window
    {
        Jądro J;
        public nowa_prostokat(Jądro j)
        {
            J = j;
            InitializeComponent();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            J.dodaj_prostokat();
        }

        private void button_Copy_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
