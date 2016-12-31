using Rozmieszczenie.Logika;
using System;
using System.Windows;

namespace Rozmieszczenie.Widoki
{
    /// <summary>
    /// Interaction logic for nowa_prostokat.xaml
    /// </summary>
    public partial class nowa_prostokat : Window
    {
        Jądro J;
        Prostokat prostokat = null;


        public nowa_prostokat(Jądro j, Prostokat prostokat = null)
        {
            J = j;
            InitializeComponent();
            if (prostokat != null)
            {

                this.prostokat = prostokat;
                textBox_ilosc_prostokat.IsEnabled = false;
                textBox_nazwa_prostokat.IsEnabled = false;
                textBox_szerokosc_prostokat.Text = prostokat.W.ToString();
                textBox_wysokosc_prostokat.Text = prostokat.H.ToString();
            }
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {

         
                if (prostokat != null)
                {
                    prostokat.W = int.Parse(textBox_szerokosc_prostokat.Text);
                    prostokat.H = int.Parse(textBox_wysokosc_prostokat.Text);
                    J.modyfikuj_prostokąt();

                    this.Close();
                }

                else
                    J.dodaj_prostokat();
            

        }

        private void button_Copy_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void OdwóćButton_Click(object sender, RoutedEventArgs e)
        {
            var tmp = textBox_szerokosc_prostokat.Text;
            textBox_szerokosc_prostokat.Text = textBox_wysokosc_prostokat.Text;
            textBox_wysokosc_prostokat.Text = tmp;

        }
    }
}
