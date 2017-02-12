using System.Windows;
using System.Windows.Input;

namespace Rozmieszczenie.Widoki
{
    /// <summary>
    /// Interaction logic for okno_about.xaml
    /// </summary>
    public partial class okno_about : Window
    {
        public okno_about()
        {
            InitializeComponent();
            text.Text = "Program umożliwiający ooptymalny rozkład figur prostokątnych na matrycy określonej wielkosci. \n\n";
            text.Text += "Wykorzystując algorytmy genetyczne szuka najlepszej możliwej konfiguracji rozłożenia figur, \n\n\n\n";
            text.Text += "zarówno do cięcia jak i układania. \n Autorzy:\n -Dawid Gruszczyński \n -Michał Makaś \n -Jakub Reszka \n\n UMK WFAiIS Rok 2016/2017 rok 3 ";
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
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
