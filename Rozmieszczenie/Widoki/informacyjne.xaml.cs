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
using Rozmieszczenie.Logika;

namespace Rozmieszczenie.Widoki
{
    /// <summary>
    /// Interaction logic for informacyjne.xaml
    /// </summary>
    public partial class informacyjne : Window
    {
        public informacyjne()
        {
            InitializeComponent();
        }

        private void button_Copy_Click(object sender, RoutedEventArgs e)
        {
            Drukuj.TxtBox(textBox);
        }
    }
}
