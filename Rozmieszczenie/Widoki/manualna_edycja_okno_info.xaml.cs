using System.Windows;
using Rozmieszczenie.Logika;
using System.Windows.Input;

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

        private void Grid_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed) DragMove();
        }
        private void Zamknij(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Minimalizuj(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }
    }
}
