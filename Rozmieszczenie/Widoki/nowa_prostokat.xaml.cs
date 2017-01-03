using Rozmieszczenie.Logika;
using System;
using System.Media;
using System.Windows;
using System.Windows.Input;

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
            if (Sprawdz.CzyNull(textBox_ilosc_prostokat, textBox_nazwa_prostokat,
                 textBox_szerokosc_prostokat, textBox_wysokosc_prostokat))
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


        private void Window_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.Key == Key.Enter)
            {
                button_Click(null, null);
            }
            else if (e.Key == Key.Escape) Close();
        }

        private void Zaznacz(object sender, RoutedEventArgs e)
        {
          
            var textBox = sender as System.Windows.Controls.TextBox;
            textBox.SelectAll();

        }

        private void Zaznacz(object sender, KeyboardFocusChangedEventArgs e)
        {
            var textBox = sender as System.Windows.Controls.TextBox;
            textBox.SelectAll();
        }

        private void Zaznacz(object sender, System.EventArgs e)
        {
            var textBox = sender as System.Windows.Controls.TextBox;
            textBox.SelectAll();
        }
         

        //Zmniejsz ilosc
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            int i = Convert.ToInt32(textBox_ilosc_prostokat.Text);
            if (i > 0) i--;
            else SystemSounds.Beep.Play();
            textBox_ilosc_prostokat.Text = i.ToString();

        }

        //Zwieksz ilosc
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            int i = Convert.ToInt32(textBox_ilosc_prostokat.Text);
            i++;
            textBox_ilosc_prostokat.Text = i.ToString();
        }
    }
}
