using System;
using System.Windows;
using System.Windows.Controls;
using System.IO;
using System.Windows.Shapes;
using System.Windows.Media;
using System.Media;
using System.Windows.Media.Imaging;
using Rozmieszczenie.Widoki;
using Microsoft.Win32;
using System.Collections.Generic;
using System.Linq;

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

        public static void Wszystkie(Widoki.widok_matryca W,  Rysowanie rys, bool YN = false)
        {         
            Canvas canvas = W.canvas;

            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
            dlg.FileName = "matryca"; // Default file name
            dlg.DefaultExt = ".png"; // Default file extension
            dlg.Filter = "Plik png (.png)|*.png"; // Filter files by extension

            // Show save file dialog box
            if (YN == false)
            {
                Nullable<bool> result = dlg.ShowDialog();

                if (result == true)
                {
                    string filename = dlg.FileName;
                }
                for (int i = 0; i <= rys.liczba_matryc; i++)
                {
                    rys.Rysuj(i);

                    Char delimiter = '.';
                    String[] substrings = dlg.FileName.Split(delimiter);
                    String nazwa = substrings[0] + i + "." + substrings[1];

                    _stworz(canvas, nazwa);
                }
            }
            else
            {
                dlg.FileName = System.AppDomain.CurrentDomain.BaseDirectory + "tmp\\matryca";
                for (int i = 0; i <= rys.liczba_matryc; i++)
                {
                    rys.Rysuj(i);

                   
                    String nazwa = dlg.FileName + i + ".png";

               _stworz(canvas, nazwa,false);
                }
            }

        }

        public static void PDF(Jądro J, Widoki.widok_matryca W, Rysowanie rys,List<string> listawybranychinformacji)
        {

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "PDF file (*.pdf)|*.pdf";
            //  try
            // {
            if (saveFileDialog.ShowDialog() == true)
            {
                string path = System.AppDomain.CurrentDomain.BaseDirectory + "tmp";
                DirectoryInfo di = Directory.CreateDirectory(path);

                iTextSharp.text.Document doc = new iTextSharp.text.Document(iTextSharp.text.PageSize.A4);
                iTextSharp.text.pdf.PdfWriter wri = iTextSharp.text.pdf.PdfWriter.GetInstance(doc, new FileStream(saveFileDialog.FileName, FileMode.Create));
                doc.Open();



                if (listawybranychinformacji[0] == "Cztery na stronę")
                {
                    iTextSharp.text.pdf.PdfPTable table = new iTextSharp.text.pdf.PdfPTable(2);
                    iTextSharp.text.pdf.PdfPTable tableInfo = new iTextSharp.text.pdf.PdfPTable(1);
                    if (listawybranychinformacji.Count > 1)
                        tableInfo = new iTextSharp.text.pdf.PdfPTable(listawybranychinformacji.Count - 1);


                    float[] widths = new float[] { iTextSharp.text.PageSize.A4.Width - doc.LeftMargin, iTextSharp.text.PageSize.A4.Width - doc.RightMargin };

                    table.SetWidths(widths);
                    table.DefaultCell.FixedHeight = (iTextSharp.text.PageSize.A4.Height - (2 * doc.BottomMargin)) / 2;
                    table.DefaultCell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
                    table.DefaultCell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;

                    Wszystkie(W, rys, true);

                    for (int i = 0; i <= rys.liczba_matryc; i++)
                    {
                        iTextSharp.text.Image PNG = iTextSharp.text.Image.GetInstance(System.AppDomain.CurrentDomain.BaseDirectory + "tmp\\matryca" + i + ".png");
                        RotationPNG(PNG);
                        table.AddCell(PNG);
                        if (rys.liczba_matryc + 1 % 2 != 0 && i + 1 > rys.liczba_matryc)
                            table.AddCell(new iTextSharp.text.pdf.PdfPCell(new iTextSharp.text.Phrase("")));

                        if (listawybranychinformacji.Count > 1 && i % 4 == 0)
                        {
                            for (int k = i; k <= i + 3; k++)
                            {
                                GetInfoPDF(J, k, tableInfo, listawybranychinformacji);
                            }
                            doc.Add(tableInfo);
                            doc.NewPage();
                            tableInfo = new iTextSharp.text.pdf.PdfPTable(listawybranychinformacji.Count - 1);


                        }
                        if ((i % 4 == 3 && i != 0) || i + 1 > rys.liczba_matryc)
                        {
                            doc.Add(table);
                            doc.NewPage();
                            table = new iTextSharp.text.pdf.PdfPTable(2);

                            table.SetWidths(widths);
                            table.DefaultCell.FixedHeight = (iTextSharp.text.PageSize.A4.Height - (2 * doc.BottomMargin)) / 2;
                            table.DefaultCell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
                            table.DefaultCell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;

                        }
                    }

                }
                else if (listawybranychinformacji[0] == "Dwie na stronę")
                {
                    iTextSharp.text.pdf.PdfPTable tableInfo = new iTextSharp.text.pdf.PdfPTable(1);
                    iTextSharp.text.pdf.PdfPTable table = new iTextSharp.text.pdf.PdfPTable(1);
                    if (listawybranychinformacji.Count > 1)
                        tableInfo = new iTextSharp.text.pdf.PdfPTable(listawybranychinformacji.Count - 1);

                    float[] widths = new float[] { iTextSharp.text.PageSize.A4.Width - doc.LeftMargin };

                    table.SetWidths(widths);
                    table.DefaultCell.FixedHeight = (iTextSharp.text.PageSize.A4.Height - (2 * doc.BottomMargin)) / 2;
                    table.DefaultCell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
                    table.DefaultCell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;

                    Wszystkie(W, rys, true);

                    for (int i = 0; i <= rys.liczba_matryc; i++)
                    {
                        iTextSharp.text.Image PNG = iTextSharp.text.Image.GetInstance(System.AppDomain.CurrentDomain.BaseDirectory + "tmp\\matryca" + i + ".png");
                        RotationPNG(PNG);
                        table.AddCell(PNG);
                        if (listawybranychinformacji.Count > 1 && i % 2 == 0)
                        {
                            for (int k = i; k <= i + 1; k++)
                            {
                                GetInfoPDF(J, k, tableInfo, listawybranychinformacji);
                            }
                            doc.Add(tableInfo);
                            doc.NewPage();
                            tableInfo = new iTextSharp.text.pdf.PdfPTable(listawybranychinformacji.Count - 1);


                        }
                        if (i % 2 != 0 && i != 0 || i + 1 > rys.liczba_matryc)
                        {
                            doc.Add(table);
                            doc.NewPage();
                            table = new iTextSharp.text.pdf.PdfPTable(1);
                            table.SetWidths(widths);
                            table.DefaultCell.FixedHeight = (iTextSharp.text.PageSize.A4.Height - (2 * doc.BottomMargin)) / 2;
                            table.DefaultCell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
                            table.DefaultCell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                        }

                    }


                }
                else if (listawybranychinformacji[0] == "Jedna na stronę")
                {
                    iTextSharp.text.pdf.PdfPTable tableInfo = new iTextSharp.text.pdf.PdfPTable(1);
                    iTextSharp.text.pdf.PdfPTable table = new iTextSharp.text.pdf.PdfPTable(1);
                    table.DefaultCell.FixedHeight = (iTextSharp.text.PageSize.A4.Height - (2 * doc.BottomMargin));
                    table.DefaultCell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
                    table.DefaultCell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                    Wszystkie(W, rys, true);

                    if (listawybranychinformacji.Count > 1)
                        tableInfo = new iTextSharp.text.pdf.PdfPTable(listawybranychinformacji.Count - 1);


                    for (int i = 0; i <= rys.liczba_matryc; i++)
                    {
                        
                        iTextSharp.text.Image PNG = iTextSharp.text.Image.GetInstance(System.AppDomain.CurrentDomain.BaseDirectory + "tmp\\matryca" + i + ".png");
                        RotationPNG(PNG);
                      


                        table.AddCell(PNG);
                        if (listawybranychinformacji.Count > 1)
                        {

                            GetInfoPDF(J, i, tableInfo, listawybranychinformacji);
                            doc.Add(tableInfo);
                            doc.NewPage();
                            tableInfo = new iTextSharp.text.pdf.PdfPTable(listawybranychinformacji.Count - 1);


                        }
                        doc.Add(table);
                        doc.NewPage();
                        table = new iTextSharp.text.pdf.PdfPTable(1);
                        table.DefaultCell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
                        table.DefaultCell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;

                    }

                }
                else if (listawybranychinformacji[0] == "Brak")
                {

                    if (listawybranychinformacji.Count > 1)
                    {
                        iTextSharp.text.pdf.PdfPTable table = new iTextSharp.text.pdf.PdfPTable(listawybranychinformacji.Count - 1);
                  

                        for (int i = 0; i <= rys.liczba_matryc; i++)
                        {

                            GetInfoPDF(J,i,table,listawybranychinformacji);

                            doc.Add(table);
                            table = new iTextSharp.text.pdf.PdfPTable(listawybranychinformacji.Count - 1);

                        }
                    }
                }


                doc.Close();
                foreach (FileInfo file in di.GetFiles())
                {
                    file.Delete();
                }
                di.Delete();
            }
            //   }
            //   catch {
            //     MessageBox.Show("Coś poszło nie tak!","Błąd!", MessageBoxButton.OK,MessageBoxImage.Warning);
            //  }



        }
        private static void RotationPNG(iTextSharp.text.Image PNG)
        {
            if (PNG.Width > PNG.Height)
                PNG.RotationDegrees = 90;
                
        }
        private static void  GetInfoPDF(Jądro J,int i, iTextSharp.text.pdf.PdfPTable table, List<string> listawybranychinformacji)
        {
            foreach (var item in listawybranychinformacji)
            {
                if (item == "Numer Matrycy")
                {
                    iTextSharp.text.pdf.PdfPCell cell = new iTextSharp.text.pdf.PdfPCell();
                    iTextSharp.text.Paragraph p = new iTextSharp.text.Paragraph();
                    p.Add(new iTextSharp.text.Phrase("Numer Matrycy"));
                    p.Add(new iTextSharp.text.Phrase(Jądro.NAJLEPSZE.wypiszNRMatrycy(i)));
                    p.Alignment = iTextSharp.text.Element.ALIGN_CENTER;
                    if (Jądro.NAJLEPSZE.wypiszNRMatrycy(i) != "")
                    {
                        cell.AddElement(p);
                        table.AddCell(cell);
                    }
                }
                if (item == "ID")
                {
                    iTextSharp.text.pdf.PdfPCell cell = new iTextSharp.text.pdf.PdfPCell();
                    iTextSharp.text.Paragraph p = new iTextSharp.text.Paragraph();
                    p.Add(new iTextSharp.text.Phrase("ID"));
                    p.Add(new iTextSharp.text.Phrase(Jądro.NAJLEPSZE.wypiszID(i)));
                    p.Alignment = iTextSharp.text.Element.ALIGN_CENTER;
                    if (Jądro.NAJLEPSZE.wypiszNRMatrycy(i) != "")
                    {
                        cell.AddElement(p);
                        table.AddCell(cell);
                    }
                }
                if (item == "Punkt Zaczepienia")
                {
                    iTextSharp.text.pdf.PdfPCell cell = new iTextSharp.text.pdf.PdfPCell();
                    iTextSharp.text.Paragraph p = new iTextSharp.text.Paragraph();
                    p.Add(new iTextSharp.text.Phrase("Punkt Zaczepienia"));
                    p.Add(new iTextSharp.text.Phrase(Jądro.NAJLEPSZE.wypiszPunktZaczepienia(i)));
                    p.Alignment = iTextSharp.text.Element.ALIGN_CENTER;
                    if (Jądro.NAJLEPSZE.wypiszNRMatrycy(i) != "")
                    {
                        cell.AddElement(p);
                        table.AddCell(cell);
                    }
                }
                if (item == "Wymiary")
                {
                    iTextSharp.text.pdf.PdfPCell cell = new iTextSharp.text.pdf.PdfPCell();
                    iTextSharp.text.Paragraph p = new iTextSharp.text.Paragraph();
                    p.Add(new iTextSharp.text.Phrase("Wymiary"));
                    p.Add(new iTextSharp.text.Phrase(Jądro.NAJLEPSZE.wypiszWymiary(i)));
                    p.Alignment = iTextSharp.text.Element.ALIGN_CENTER;
                    if (Jądro.NAJLEPSZE.wypiszNRMatrycy(i) != "")
                    {
                        cell.AddElement(p);
                        table.AddCell(cell);
                    }
                }


            }
        }

        static void _stworz(Canvas canvas, string nazwa_pliku, bool YN = true)
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
                if(YN)
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

        public void Rysuj(object o, Rozmieszczenia R_, int liczbamatryc=0)
            {
                R = R_;
                W = (Widoki.widok_matryca)o;
            if (liczbamatryc == 0)
                liczba_matryc = R.Liczba_wykorzystanych_matryc - 1;
            else
                liczba_matryc = liczbamatryc;
            Rysuj();            
            }


        public void Rysuj() {
            
          Canvas  C = W.canvas;
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

        public void Rysuj(Rozmieszczenia RR) //wersja dla przeciagania
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


            for (int i = 0; i < RR.lokalizacja_figur.Length; i++)
            {
                if (RR.lokalizacja_figur[i].nr_matrycy == aktualna_matryca)
                {
                    int W = RR.lokalizacja_figur[i].figura.W;
                    int H = RR.lokalizacja_figur[i].figura.H;

                    Rectangle rect = new Rectangle();
                    rect.Fill = Brushes.BurlyWood;
                    rect.Stroke = new SolidColorBrush(Colors.Black);
                    rect.Width = W;
                    rect.Height = H;
                    Canvas.SetTop(rect, RR.lokalizacja_figur[i].p.y);
                    Canvas.SetLeft(rect, RR.lokalizacja_figur[i].p.x);
                    C.Children.Add(rect);


                    {
                        TextBlock textBlock = new TextBlock();
                        textBlock.FontSize = Math.Min(W / 2, H / 2);
                        Canvas.SetTop(textBlock, RR.lokalizacja_figur[i].p.y + 1);
                        Canvas.SetLeft(textBlock, RR.lokalizacja_figur[i].p.x + 1);
                        textBlock.Text = RR.lokalizacja_figur[i].figura.ID.ToString();
                        textBlock.Foreground = new SolidColorBrush(Colors.Black);
                        C.Children.Add(textBlock);
                    }
                }



            }
        }

        
        }
    public class Przeciaganie
    {
        Rozmieszczenia R, kopiaR;
        manualna_edycja_okno_info Okno;
        widok_matryca WM;
        Jądro J;
        public bool czyEdytowac;
       

        int am;

        public Przeciaganie(Rozmieszczenia _R, widok_matryca _WM, Jądro _J)
        {
            R = _R; WM = _WM; J = _J; kopiaR = _R;
            MessageBoxResult result = MessageBox.Show("Rozmieszczono figury. \nCzy chesz poprawićrozmieszczenie ręcznie?", "Rozmieszczanie manualne",
                            MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                _R.czyZmienaneRecznie = true;
                Okno = new manualna_edycja_okno_info(this);
                Okno.Show();
                czyEdytowac = true;
            }
            else if (result == MessageBoxResult.No)
            {
                czyEdytowac = false;
            }

        }

        public void Gotowe()
        {
            R = kopiaR;
            Okno.Close();
            J.R.Rysuj();
            J.InfoBox();
        }

        public void Przywroc() //nie dizala bo tu wszystko jest reerencją...
        {
            kopiaR = R;
            J.R.Rysuj(kopiaR);
        }

        public MatrycaFiguraPunkt Znajdz(Point P)
        {
            am = J.R.Aktualna_Matryca;
            MatrycaFiguraPunkt p = null;
            foreach (MatrycaFiguraPunkt O in kopiaR.lokalizacja_figur)
            {
                if (O.nr_matrycy == am)
                {
                    //jezeli to ten to biore figure
                    if (P.X <= O.p.x + O.figura.W)
                        if (P.X >= O.p.x)
                            if (P.Y < O.p.y + O.figura.H)
                                if (P.Y > O.p.y)
                                {
                                    p = O;
                                    J.wm.textBlock.Text = "Nazwa: " + p.figura.Nazwa + "   ID: " + p.figura.ID;
                                    break;
                                }
                }
            }
            return p;
        }
        public void Rusz(MatrycaFiguraPunkt p, Point P)
        {

            if (p != null && czyEdytowac)
            {
                bool czyMogeX = true;
                bool czyMogeY = true;

                double RX = 0, RY = 0;

              //RX = (P.X - ( p.p.x -P.X));
              //RY = (P.Y - ( p.p.y- P.Y ));

               RX = P.X;
               RY = P.Y;



                J.wm.textBlock.Text = "spr";
                foreach (MatrycaFiguraPunkt O in kopiaR.lokalizacja_figur)
                {
                    if (O != p && O.nr_matrycy == am)
                    {

                        if (RX < 0 || RX + p.figura.W > J.Matka.rozmiar_x)
                        {
                            czyMogeX = false;
                            J.wm.textBlock.Text = "   nie można tu przesunąć X (" + RX + "," + RY + ")";
                        }
                        if (RY < 0 || RY + p.figura.H > J.Matka.rozmiar_y)
                        {
                            czyMogeY = false;
                            J.wm.textBlock.Text = "   nie można tu przesunąć Y (" + RX + "," + RY + ")";
                        }
                        if(czyMogeX || czyMogeY){
                            if (RX < O.p.x + O.figura.W)
                                if (RX + p.figura.W > O.p.x)
                                    if (p.p.y < O.p.y + O.figura.H)
                                        if (p.p.y + p.figura.H > O.p.y)
                                        {
                                            czyMogeX = false;
                                            J.wm.textBlock.Text = "   nie można tu przesunąć X (" + RX + "," + RY + ")";
                                        }
                            if (p.p.x < O.p.x + O.figura.W)
                                if (p.p.x + p.figura.W > O.p.x)
                                    if (RY < O.p.y + O.figura.H)
                                        if (RY + p.figura.H > O.p.y)
                                        {
                                            czyMogeY = false;
                                            J.wm.textBlock.Text = "   nie można tu przesunąć Y (" + RX + "," + RY + ")";

                                        }
                        }
                    }
                }


                if (czyMogeX)
                { p.p.x = (int)RX; J.wm.textBlock.Text += " przesunietoX"; }
                if (czyMogeY)
                { p.p.y = (int)RY; J.wm.textBlock.Text += " przesunietoY"; }


                if (czyMogeX || czyMogeY)
                {
                    J.R.Rysuj(kopiaR);
                    J.wm.textBlock.Text = "Nazwa: " + p.figura.Nazwa + "   ID: " + p.figura.ID + "  " + P.X + " " + P.Y;
                }
            }



        }
    }

}



