using Rozmieszczenie.Logika;
using System.Windows;

namespace Rozmieszczenie
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Jądro J;
        public MainWindow()
        {
            J = new Jądro(this);
            InitializeComponent();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            J.nowy_prostokat();
        }

    

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            J.nowy_matryca();
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            J.zamknij();
            Close();
        }

        private void button_rozmiesc_Click(object sender, RoutedEventArgs e)
        {
            J.rozmiesc();
            
        }

        private void dataGrid_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {

        }
    }
}
