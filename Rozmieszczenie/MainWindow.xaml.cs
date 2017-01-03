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
       
        private void _Usuń_prostokąt_Click(object sender, RoutedEventArgs e)
        {
            if ((dataGrid.SelectedItem is Prostokat) && dataGrid.SelectedItem != null )
                J.usuń_prostokąt((Prostokat)dataGrid.SelectedItem);

        }
       private void _Usuń_wszystkie_Click(object sender, RoutedEventArgs e)
        {
            J.usuń_wszystkie();

        }
        
          private void button_usuń_Click(object sender, RoutedEventArgs e)
        {
            if ((dataGrid.SelectedItem is Prostokat) && dataGrid.SelectedItem != null)
                J.usuń_prostokąt((Prostokat)dataGrid.SelectedItem);

        }
        private void _Odwróć_prostokąt_Click(object sender, RoutedEventArgs e)
        {
            if ((dataGrid.SelectedItem is Prostokat) && dataGrid.SelectedItem != null)
            {

                int tmp = ((Prostokat)dataGrid.SelectedItem).W;
                ((Prostokat)dataGrid.SelectedItem).W = ((Prostokat)dataGrid.SelectedItem).H;
                ((Prostokat)dataGrid.SelectedItem).H = tmp;
                dataGrid.DataContext = Jądro.lista_obiektow;
                dataGrid.Items.Refresh();

            }

        }
        private void dataGrid_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {

        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            J.test();
        }
    }
}
