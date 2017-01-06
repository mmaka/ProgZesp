using System.Windows;
using Rozmieszczenie.Logika;

namespace Rozmieszczenie.Widoki
{
    /// <summary>
    /// Interaction logic for manualna_edycja_okno_info.xaml
    /// </summary>
    public partial class manualna_edycja_okno_info : Window
    {
        Przeciaganie P;
        public manualna_edycja_okno_info(Przeciaganie _P)
        {
            P = _P;
            InitializeComponent();
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            P.Gotowe();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            P.Przywroc();
        }
    }
}
