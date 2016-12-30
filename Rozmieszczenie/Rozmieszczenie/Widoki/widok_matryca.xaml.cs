using Rozmieszczenie.Logika;
using System.Windows;

namespace Rozmieszczenie.Widoki
{
    /// <summary>
    /// Interaction logic for widok_matryca.xaml
    /// </summary>
    public partial class widok_matryca : Window
    {
        Jądro J;
        public widok_matryca(Jądro j)
        {
            J = j;
            InitializeComponent();
            
        }
    }
}
