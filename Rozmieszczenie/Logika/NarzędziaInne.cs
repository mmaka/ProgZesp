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
using Microsoft.Win32;
using System.Windows.Media.Imaging;

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


    static public class ZapiszObrazMatrycy
    {
               

        public static void Jedna(Widoki.widok_matryca W)
        {
            Canvas canvas = W.canvas;
                      

            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
            dlg.FileName = "matryca"; // Default file name
            dlg.DefaultExt = ".png"; // Default file extension
            dlg.Filter = "Plik png (.png)|*.png"; // Filter files by extension

            // Show save file dialog box
            Nullable<bool> result = dlg.ShowDialog();

            if (result == true)
            {                
                string filename = dlg.FileName;
            }

            _stworz(canvas, dlg.FileName);

        }

        public static void Wszystkie(Widoki.widok_matryca W,  Rysowanie rys)
        {         
            Canvas canvas = W.canvas;

            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
            dlg.FileName = "matryca"; // Default file name
            dlg.DefaultExt = ".png"; // Default file extension
            dlg.Filter = "Plik png (.png)|*.png"; // Filter files by extension

            // Show save file dialog box
            Nullable<bool> result = dlg.ShowDialog();

            if (result == true)
            {
                string filename = dlg.FileName;
            }

            for (int i=0; i <= rys.liczba_matryc; i++) {
                rys.Rysuj(i);

                Char delimiter = '.';
                String[] substrings = dlg.FileName.Split(delimiter);
                String nazwa = substrings[0] + i + "." + substrings[1];              

                _stworz(canvas, nazwa);
            }
            

        }

        static void _stworz(Canvas canvas, string nazwa_pliku)
        {
            RenderTargetBitmap renderBitmap = new RenderTargetBitmap(
                (int)canvas.Width, (int)canvas.Height,
                    96d, 96d, PixelFormats.Pbgra32);
            // needed otherwise the image output is black
            canvas.Measure(new Size((int)canvas.Width, (int)canvas.Height));
            canvas.Arrange(new Rect(new Size((int)canvas.Width, (int)canvas.Height)));

            renderBitmap.Render(canvas);

            //JpegBitmapEncoder encoder = new JpegBitmapEncoder();
            PngBitmapEncoder encoder = new PngBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(renderBitmap));


            using (FileStream file = File.Create(nazwa_pliku))
            {
                encoder.Save(file);
                 MessageBox.Show("Plik zapisano pomyślnie", "Zapis pliku",
                            MessageBoxButton.OK, MessageBoxImage.None);               
            }
        }

    }

    public  class Rysowanie
    {
       Widoki.widok_matryca W;         
       Rozmieszczenia R;
      public  int liczba_matryc;
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

        public Rysowanie(Widoki.widok_matryca w) { W = w;  }

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

            {
                Rectangle rect = new Rectangle();
                rect.Fill = Brushes.LightCyan;
                rect.Stroke = new SolidColorBrush(Colors.Black);
                rect.Width = C.Width;
                rect.Height = C.Height;
                Canvas.SetTop(rect, 0);
                Canvas.SetLeft(rect, 0);
                C.Children.Add(rect);

            }
            
               W.textBlock.Text = "Nr. matrycy: " + aktualna_matryca;
          
                for (int i = 0; i < R.lokalizacja_figur.Length; i++)
                {
                    if (R.lokalizacja_figur[i].nr_matrycy == aktualna_matryca)
                    {
                    int W = R.lokalizacja_figur[i].figura.W;
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
                        C.Children.Add(textBlock);
                    }
                }
                //    float _W=  (float)R.lokalizacja_figur[i].figura.W*mnx;  
                //    float H = (float)R.lokalizacja_figur[i].figura.H*mny;

                //W.textBlock.Text += _W + "  " + H+"  "+ (float)R.lokalizacja_figur[i].p.y*mny+"  "+ (float)R.lokalizacja_figur[i].p.x*mnx;

                //Rectangle rect = new Rectangle();
                //    rect.Fill = Brushes.BurlyWood;
                //    rect.Stroke = new SolidColorBrush(Colors.Black);
                //    rect.Width = (double)_W;
                //    rect.Height = (double)H;

                //double T = (double)(R.lokalizacja_figur[i].p.y * mny);
                //Canvas.SetTop(rect,  1);
                //Canvas.SetLeft(rect, 1);

                //W.textBlock.Text = rect.Width + " " + rect.Height;
                //C.Children.Add(rect);

                ////if (W > 30 && H > 30)
                //{
                //    TextBlock textBlock = new TextBlock();
                //    textBlock.FontSize = Math.Min(_W /2 , H/ 2);
                //    Canvas.SetTop(textBlock, ((double)R.lokalizacja_figur[i].p.y * mny));
                //    Canvas.SetLeft(textBlock, (double)R.lokalizacja_figur[i].p.x * mnx);
                //    //W.textBlock.Text = (float)R.lokalizacja_figur[i].p.y * mny + "   " + mny;
                //    textBlock.Text = R.lokalizacja_figur[i].figura.ID.ToString();
                //    textBlock.Foreground = new SolidColorBrush(Colors.Black);
                //    C.Children.Add(textBlock); }
                //}
            }
        }



        //wersja dla potrzeb zapisywania WSZYSTKICH matryc do pliku obrazu
        public void Rysuj(int ktora)
        {

            Canvas C = W.canvas;
            C.Children.Clear();

            {
                Rectangle rect = new Rectangle();
                rect.Fill = Brushes.LightCyan;
                rect.Stroke = new SolidColorBrush(Colors.Black);
                rect.Width = C.Width;
                rect.Height = C.Height;
                Canvas.SetTop(rect, 0);
                Canvas.SetLeft(rect, 0);
                C.Children.Add(rect);

            }
            

            for (int i = 0; i < R.lokalizacja_figur.Length; i++)
            {
                if (R.lokalizacja_figur[i].nr_matrycy == ktora)
                {
                    int W = R.lokalizacja_figur[i].figura.W;
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
                        C.Children.Add(textBlock);
                    }
                }
            }
        }






            }
      




    }



