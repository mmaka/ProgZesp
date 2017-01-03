using Rozmieszczenie.Logika;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Rozmieszczenie.Widoki
{
    public partial class widok_matryca : Window
    {
        Jądro J;        
        int X;
        int Y;        
      

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
            InitializeComponent();            
            canvas.Width = x;
            canvas.Height = y;
            {
                Rectangle rect = new Rectangle();
                rect.Fill = Brushes.LightCyan;
                rect.Stroke = new SolidColorBrush(Colors.Black);
                rect.Width = canvas.Width;
                rect.Height = canvas.Height;
                Canvas.SetTop(rect, 0);
                Canvas.SetLeft(rect, 0);
                canvas.Children.Add(rect);

            }
            //canvas.MinHeight =canvas.Height;
            //canvas.MinWidth = canvas.Width;
            InvalidateVisual();
            UpdateLayout();            
        }

        private void button_zapisz_te_Click(object sender, RoutedEventArgs e)
        {
          
            ZapiszObrazMatrycy.Wszystkie(this, J.R);
        }

        private void button_zapisz_all_Click(object sender, RoutedEventArgs e)
        {
            ZapiszObrazMatrycy.Jedna(this);
        }
    }
}
