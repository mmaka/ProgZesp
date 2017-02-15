using System.Windows;
using System.Windows.Input;

namespace Rozmieszczenie.Widoki
{
    /// <summary>
    /// Interaction logic for pasek.xaml
    /// </summary>
    public partial class pasek : Window
    {
        public pasek(int s)
        {
            InitializeComponent();
            status.Maximum = s;
        }

        private void Window_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed) DragMove();
        }
        private void Zamknij(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Trwa rozmieszczanie prostokątów...", "Błąd",
                         MessageBoxButton.OK, MessageBoxImage.Warning);
            
        }

        private void Minimalizuj(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }


      

       
    }

}
