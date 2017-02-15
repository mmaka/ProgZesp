using System.Windows;
using System.Windows.Input;

namespace Rozmieszczenie.Widoki
{
    /// <summary>
    /// Interaction logic for okno_pomoc.xaml
    /// </summary>
    public partial class okno_pomoc : Window
    {
        public okno_pomoc()
        {
            InitializeComponent();
            text.Text = "INSTRUKCJA OBSŁUGI:\n";
            text.Text += "\nAplikacja ma na celu wsparcie minimalizacji zużycia powierzchni matryc, na których rozmieszczanie są prostokąty.\n";
            text.Text += "\nAby rozpocząć rozmieszczanie prostokątów: \n 1) dodaj prostokąty (\"ręcznie\" lub z pliku xml) \n 2) określ rozmiar matrycy oraz rzaz \n 3) ustal czas (w sekundach) trwania obliczeń\n 4) kliknij 'rozmieść'";
            text.Text +="\n\nAby zapisać obraz matrycy w pliku graficznym kliknij  'Do obrazu TA' aby zapisać obraz aktualnie wyświetlanej matrycy\n lub 'Do obrazu ALL' \n aby zapisaćobraz wszystkich matryc.";
            text.Text += "\n\nAby zapisać listę figur do pliku wybierz Menu Plik i Zapisz listę figur, analogicznie postąp jeśli chcesz zapisaćcały projekt.";
            text.Text += "\n\nAby eksportować listę figur i obrazy matryc po rozmieszczeniu wybierz menu Plik-> Zapisz doPDF";

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
