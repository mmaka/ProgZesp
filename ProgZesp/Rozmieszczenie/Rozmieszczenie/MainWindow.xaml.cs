using Rozmieszczenie.Logika;
using Rozmieszczenie.Widoki;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace Rozmieszczenie
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Jądro J;
        Zła_MatrycaFigura zMF;
        nowa_prostokat np;
        public MainWindow()
        {
            J = new Jądro(this);
            InitializeComponent();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            J = new Jądro(this);
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
        private void _Edytuj_Prostokąt_Click(object sender, RoutedEventArgs e)
        {
            var x1 = dataGrid.SelectedItem;
            np = new nowa_prostokat(J, (Prostokat)x1);
            np.Show();
            dataGrid.DataContext = Jądro.lista_obiektow;
            dataGrid.Items.Refresh();

        }
        private void _Usuń_Prostokąt_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid.SelectedItem != null)
                J.usuń_prostokąt((Prostokat)dataGrid.SelectedItem);

        }
        private void button_rozmiesc_Click(object sender, RoutedEventArgs e)
        {
            if (Jądro.listaStworzonychMatryc.Count == 0)   //zabazpieczenia przed rozmieszczeniem figur bez matrycy
                MessageBox.Show("Stwórz Matrycę!", "Brak Matrycy", MessageBoxButton.OK);
            else
            {


                List<Prostokat> listaObiektówKtóreMieszcząSieNaMatrycach = new List<Prostokat>();
                foreach (var item2 in Jądro.lista_obiektow)                             //sprawdzanie czy obiekt zmieści sie na matrycy, jesli tak dodajemy do listy
                {
                    foreach (var item in Jądro.listaStworzonychMatryc)
                    {

                        if (item2.W <= item.rozmiar_x && item2.H <= item.rozmiar_y)
                            listaObiektówKtóreMieszcząSieNaMatrycach.Add(item2);

                    }
                }

                if (listaObiektówKtóreMieszcząSieNaMatrycach.Count != Jądro.lista_obiektow.Count)  //sprawdzamy czy jakaś figura się nie mieści 
                {
                    List<int> listaIDdoUsunięcia = new List<int>();
                    var zapytanie = Jądro.lista_obiektow.Except<Prostokat>(listaObiektówKtóreMieszcząSieNaMatrycach);

                    foreach (var item in zapytanie) //szukanie i dodawanie iod figur które sie nie mieszcza
                    {
                        listaIDdoUsunięcia.Add(item.ID);
                    }


                    if (zapytanie.Count() != 0)// jesli lista nie jest pusta to znaczy ze sa takie fiugury i trzeba albo je usunac albo zmienic rozmiar matrycy
                    {
                        foreach (var item in listaIDdoUsunięcia)
                        {
                            zMF = new Zła_MatrycaFigura(item, J, "Figura " + item + " nie zmieści się na\n żadnej matrycy. Co chcesz zrobić?", this);
                            zMF.ShowDialog();

                        }

                    }
                }


                J.miksuj();
            }
        }
    }
}
