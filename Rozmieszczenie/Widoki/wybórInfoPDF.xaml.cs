using Rozmieszczenie.Logika;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Rozmieszczenie.Widoki
{
    /// <summary>
    /// Interaction logic for wybórInfoPDF.xaml
    /// </summary>
    public partial class wybórInfoPDF : Window
    {
        Jądro J;
        Widoki.widok_matryca W;
        Rysowanie rys;
        public wybórInfoPDF(Jądro J, Widoki.widok_matryca W, Rysowanie rys)
        {
            InitializeComponent();
            this.J = J;
            this.W = W;
            this.rys = rys;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            List<string> listaWybranychInformacji = new List<string>();

            foreach (ListBoxItem item in listBox.SelectedItems)
            {
                
                listaWybranychInformacji.Add(item.Content.ToString());
            }
            ZapiszObrazMatrycy.PDF(J, J.wm, J.R,listaWybranychInformacji);
           
            this.Close();
            
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
