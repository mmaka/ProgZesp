using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Drawing;
using System.Printing;
using System.IO;
using System.Windows.Shapes;
using System.Windows.Xps;
using System.Windows.Media;
using System.Media;

namespace Rozmieszczenie.Logika
{
    static class Sprawdz
    {

        public static bool CzyNull(params object[] o)
        {
            for (int i = 0; i < o.Length; i++)
            {
                TextBox T = (TextBox)o[i];
                if (T.Text == "")
                {
                    MessageBox.Show("Żadna z wartości nie może być pusta!", "Błąd",
                            MessageBoxButton.OKCancel, MessageBoxImage.Warning);
                    return false;
                }
            }
            return true;
        } }

    static class Drukuj
    {
        public static void TxtBox(object o)
        {
            TextBox T = (TextBox)o;
            /*
            PrintDialog dia = new PrintDialog();
            PrintDialog PrintDialog = new PrintDialog();
             //Open Print Dialog.
             Nullable<Boolean> printClicked = PrintDialog.ShowDialog();

             //If Print is clicked
             if (printClicked == true)
             {
                 //Get Printer Capabilities.
                 PrintCapabilities printCapabilities = PrintDialog.PrintQueue.GetPrintCapabilities(PrintDialog.PrintTicket);
                 Size pageAreaSize = new Size(printCapabilities.PageImageableArea.ExtentWidth, printCapabilities.PageImageableArea.ExtentHeight);

                 //Visual Brush for the ptint_TextBox to be printed.
                 VisualBrush VisualBrush = new VisualBrush(T);
                 VisualBrush.Stretch = Stretch.Uniform;
                 VisualBrush.ViewboxUnits = BrushMappingMode.Absolute;
                 VisualBrush.Viewbox = new Rect(0, 0, T.ExtentWidth, T.ExtentHeight);

                 //Rectangle to contain the VisualBrush 
                 Rectangle rect = new Rectangle();
                 rect.Fill = VisualBrush;
                 rect.Arrange(new Rect(new Point(0, 0), pageAreaSize));

                 //Print the ptint_TextBox.
                 XpsDocumentWriter writer = PrintQueue.CreateXpsDocumentWriter(PrintDialog.PrintQueue);
                 writer.Write(rect, PrintDialog.PrintTicket);
             }*/
             

        }
    }


    public  class Rysowanie
    {
       Widoki.widok_matryca W;         
       Rozmieszczenia R;
        int liczba_matryc;
        int aktualna_matryca = 0;
        public int Aktualna_Matryca
        {
            get { return aktualna_matryca; }
            set
            {
                if(value<0 || value>liczba_matryc) SystemSounds.Beep.Play();
                else  aktualna_matryca = value;
            }
            
        }

        public Rysowanie(Widoki.widok_matryca w) { W = w; }

        public void Rysuj(object o, Rozmieszczenia R_)
            {
                R = R_;
                W = (Widoki.widok_matryca)o;
                liczba_matryc = R.Liczba_wykorzystanych_matryc-1;
                Rysuj();

            }


        public void Rysuj() {
                Canvas C = W.canvas;
                C.Children.Clear();
                W.textBlock.Text = "Nr. matrycy: " + aktualna_matryca;
                for (int i = 0; i < R.lokalizacja_figur.Length; i++)
                {
                    if (R.lokalizacja_figur[i].nr_matrycy == aktualna_matryca)
                    {
                        int W=  R.lokalizacja_figur[i].figura.W;
                        int H = R.lokalizacja_figur[i].figura.H;

                        Rectangle rect = new Rectangle();
                        rect.Fill = Brushes.BurlyWood;
                        rect.Stroke = new SolidColorBrush(Colors.Black);
                        rect.Width = W;
                        rect.Height = H;
                        Canvas.SetTop(rect, R.lokalizacja_figur[i].p.y);
                        Canvas.SetLeft(rect, R.lokalizacja_figur[i].p.x);
                        C.Children.Add(rect);

                    //if (W > 30 && H > 30)
                    {
                        TextBlock textBlock = new TextBlock();
                        textBlock.FontSize = Math.Min(W / 2, H / 2);
                        Canvas.SetTop(textBlock, R.lokalizacja_figur[i].p.y + 1);
                        Canvas.SetLeft(textBlock, R.lokalizacja_figur[i].p.x + 1);
                        textBlock.Text = R.lokalizacja_figur[i].figura.ID.ToString();
                        textBlock.Foreground = new SolidColorBrush(Colors.Black);
                        C.Children.Add(textBlock); }
                    }
                }
        }






    }
      




    }



