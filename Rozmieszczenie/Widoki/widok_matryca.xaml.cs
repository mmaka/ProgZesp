using Rozmieszczenie.Logika;
using System.Windows;

namespace Rozmieszczenie.Widoki
{
    public partial class widok_matryca : Window
    {
        Jądro J;        
        int X;
        int Y;
        float MnY, MnX; //współczynniki mnożnika w celu zachowania maksymalnego wymiaru matrycy bezprzewijania 

        private void button_kolejna_Click(object sender, RoutedEventArgs e)
        {
            J.R.Aktualna_Matryca++;
            J.R.Rysuj();
        }

        private void button_poprzednia_Click(object sender, RoutedEventArgs e)
        {
            J.R.Aktualna_Matryca--;
            J.R.Rysuj();
        }

        //a jednoczesnie jesli zbyt duza to zmniejszenie obrazka przy wykorzystaniu mnoznikow
        //TODO

        //konstruktor
        public widok_matryca(Jądro j,int x, int y)
        {
            J = j;            
            X = x;
            Y = y;

            MaxWidth = SystemParameters.WorkArea.Width;
            MaxHeight = SystemParameters.WorkArea.Height;
                MnY = (float)MaxHeight/Y;
                MnX = (float)MaxWidth / X;
            InitializeComponent();
            imageGrid.Width = x;
            imageGrid.Height = y;
            canvas.Width = x;
            canvas.Height = y;
            //canvas.MinHeight =canvas.Height;
            //canvas.MinWidth = canvas.Width;
            InvalidateVisual();
            UpdateLayout();            
        }
    }
}
