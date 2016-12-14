using System.Drawing;
using System.Windows.Forms;
using System.Globalization;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;

namespace dodawanie_figur1
{
    public class Akcje_widok
    {
        Matryca M;
        Form1 okno;
        Form_prostokat prost;
        Form_matryca matrycaPBox;
        ProgramowanieZespolowe.Form_Nowa_Matryca nowaMatryca;
        int licznik = 0;

       

        public Akcje_widok(Form1 _okno)
        {
            okno = _okno;          
        }
       
        public void nowa()
        {
            M = new Matryca(            
             int.Parse(nowaMatryca.textBox_matrycaW.Text, CultureInfo.InvariantCulture.NumberFormat),
             int.Parse(nowaMatryca.textBox_matrycaH.Text, CultureInfo.InvariantCulture.NumberFormat)
             );

            matrycaPBox = new Form_matryca(M.VX, M.VY, this);
            matrycaPBox.pictureBox1.Paint += new PaintEventHandler(clear);
            matrycaPBox.pictureBox1.Paint += new PaintEventHandler(M.rysuj);
            matrycaPBox.pictureBox1.Paint += new PaintEventHandler(rysujOBWODKE);
            //matrycaPBox.pictureBox1.Paint += new PaintEventHandler(rysujChmurka);

            matrycaPBox.Show();

            okno.button_dodaj.Enabled = true;
            okno.button_Rysuj.Enabled = true;
            okno.checkedBox_LISTA.Enabled = true;            
            okno.dataGridView1.Enabled = true;
            okno.button_usun.Enabled = true;            
            okno.button_rozmiesc.Enabled = true;
            matrycaPBox.Location = new Point(okno.Location.X + okno.Width, okno.Location.Y);
            matrycaPBox.Refresh();
        }

        internal void Drukuj()
        {           

        }

        public void zapiszObraz()
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.FileName = "pB";
            dialog.Filter = "png files (*.png) | *.png | jpg files (*.jpg)|*.jpg|All files (*.*)|*.*";
            dialog.FilterIndex = 1;
            dialog.RestoreDirectory = true;
            if (dialog.ShowDialog() == DialogResult.OK)
            {
               
                int width = Convert.ToInt32(matrycaPBox.pictureBox1.Width);
                int height = Convert.ToInt32(matrycaPBox.pictureBox1.Height);
                Bitmap bmp = new Bitmap(width, height);
                matrycaPBox.pictureBox1.DrawToBitmap(bmp, new Rectangle(0, 0, width, height));
                bmp.Save(dialog.FileName, ImageFormat.Jpeg);
            }
        }

        internal void nowaM()
        {
            nowaMatryca = new ProgramowanieZespolowe.Form_Nowa_Matryca(this);
            nowaMatryca.Show();
        }

        internal void klikniecie_ktora_figura(MouseEventArgs e)
        {
            int x = e.X;
            int y = e.Y;


            okno.textBox1.Text = "Klik " + x + " " + y; 

            Prostokat szukany= M.szukaj_obiektuXY(x, y);
            if (szukany != null)
            {
                okno.textBox1.Text = szukany.ID.ToString();
                int id = szukany.ID;
              
                foreach (DataGridViewRow row in okno.dataGridView1.Rows)
                {
                    x =(int) row.Cells[okno.ID.Index].Value;
                    if (id == x)
                    { 
                        row.Selected = true; break;
                    }
                }
            }
        }

       

        internal void rozmiesc()
        {
            for (int i = 0; i < M.lista.Count; i++)
            {
                if (M.polozenie_poczatkowe_prostokat(M.lista[i]) == true)
                {
                    M.idz_max_w_gore(M.lista[i]);
                    M.idz_max_w_prawo(M.lista[i]);
                    M.idz_max_w_gore(M.lista[i]);
                    M.lista[i].aktualizacja_zajetosci_prostokat(M.zajetosc_x, M.zajetosc_y);
                    M.wstawione_obiekty.Add(M.lista[i]);
                    okno.textBox1.Text = "Rozmieszczono.";

                }
                else okno.textBox1.Text="Problem!";

            }
            rysuj();

        }

        internal void usun()
        {
           
             int index;
             if (okno.dataGridView1.SelectedRows.Count != 0)
             {
                 index = (int)okno.dataGridView1.SelectedRows[0].Cells[okno.ID.Index].Value;
                 okno.textBox1.Text = index.ToString();
                 M.usun(index);
                 okno.dataGridView1.Rows.Remove(okno.dataGridView1.SelectedRows[0]);
                 okno.dataGridView1.ClearSelection();
                 wyp_tabele();
                 rysuj();

                 int i = M.lista.Count;
                 okno.textBox1.Text = i.ToString();
             }
        }



        internal void przemiesc(MouseEventArgs e)
         {

            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                int index;
                if (okno.dataGridView1.SelectedRows.Count != 0)
                {
                    if (okno.dataGridView1.SelectedRows[0].Cells[okno.ID.Index].Value != null)
                    {
                        index = (int)okno.dataGridView1.SelectedRows[0].Cells[okno.ID.Index].Value;
                        okno.textBox1.Text = index.ToString();
                        //-----------------------------------
                        List<Prostokat> ListaObiektówDoZderzeń = new List<Prostokat>();

                        foreach (var item in M.lista)  // tworzenie listy obiektów do zderzeń
                        {
                            if (item.ID != index)
                                ListaObiektówDoZderzeń.Add(item);

                        }
                        bool czyZderzy = false;
                        //-----------------------------------
                        foreach (Obiekt O in M.lista)
                        {
                            if (O.ID == index)
                            {
                                foreach (var item in ListaObiektówDoZderzeń) ///Sprawdzanie czy nie zderza sie z innymi figurami 
                                {
                                    if (e.X <= item.X + item.W)
                                        if (e.X + O.W >= item.X)
                                            if (e.Y <= item.Y + item.H)
                                                if (e.Y + O.H >= item.Y)
                                                    czyZderzy = true;

                                }
                                if (!czyZderzy && e.X >= 0 && e.X + O.W <= M.VX && e.Y >= 0 && e.Y + O.H <= M.VY)  //Sprawdzanie czy nie wychodzi figura poza matryce + wynik sprawdzania czy nie doszło do zderzenia z innymi figurami
                                    
                                O.move(e.X, e.Y);                        
                                break;
                            }
                        }                                   
                       rysuj();
                    }
                }
            }
         }

         public  void wyp_tabele()
         {     
             int i = 0;
             foreach (Prostokat O in M.lista)
             {                                        
                 okno.dataGridView1[okno.Figura.Index, i].Value = O.Name;
                 okno.dataGridView1[okno.ID.Index, i].Value = O.ID;
                 okno.dataGridView1[okno.Pole.Index, i].Value = O.Pole;
                 okno.dataGridView1[okno.X.Index, i].Value = O.X;
                 okno.dataGridView1[okno.Y.Index, i].Value = O.Y;
                 okno.dataGridView1[okno.Wysok.Index, i].Value = O.H;
                 okno.dataGridView1[okno.Szer.Index, i].Value = O.W;
                 i++;
             }           
       }



         internal void dodaj_prost(Form_prostokat form_prostokat)
         {
            int y = int.Parse(form_prostokat.textBox_height.Text, CultureInfo.InvariantCulture.NumberFormat);
            int x = int.Parse(form_prostokat.textBox_width.Text, CultureInfo.InvariantCulture.NumberFormat);

             for (int i = 0; i < prost.numericUpDown_ILOSC.Value; i++)
             {
                 M.lista.Add(new Prostokat(x, y, ++licznik, prost.textBox_nazwa.Text));
                 okno.dataGridView1.Rows.Add();
             }
             form_prostokat.Close();
             wyp_tabele();
         }

         public void dodaj()
         {
             /* if (okno.checkedBox_LISTA.CheckedItems != null)
              {
                  switch ((string)okno.checkedBox_LISTA.CheckedItems[0])
                  {
                    default
                     case "Kwadrat":
                          prost = new Form_prostokat(this);
                          prost.Show();
                          break;
                      case "Prostokąt":
                          prost = new Form_prostokat(this);
                          prost.Show();
                          break;
                      case "Koło":
                          M.lista.Add(new prostokat(10, 100,++licznik, "Koło"));
                          break;
                      case "Elipsa":
                          M.lista.Add(new prostokat(10, 10,++licznik,"Elipsa"));
                          break;
                  } }*/

            {
                prost = new Form_prostokat(this);
                prost.Show();

                prost.Location = new Point(okno.Location.X + 10, okno.Location.Y+100);
                prost.Refresh();
            }
            wyp_tabele();
            
        }


        //INNE FUNKCJE POMOCNICZE

        internal void unselect()
        {
            if (okno.dataGridView1.SelectedRows.Count > 0)
                okno.dataGridView1.SelectedRows[0].Selected = false;
        }


        public void MBNull()
        {
            MessageBox.Show("Żadna z wartości nie może być pusta!", "Błąd",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
        }


        //FUNKCJE RYSUJACE, DZIAŁAJĄCE NA PBOX

        public void rysuj()
        {           
            matrycaPBox.pictureBox1.Invalidate();
            matrycaPBox.pictureBox1.Show(); 
            wyp_tabele();
        }

        internal void rysujOBWODKE(object sender, PaintEventArgs e)
        {
            if (okno.dataGridView1.SelectedRows.Count != 0)
            {
                if (okno.dataGridView1.SelectedRows[0].Cells[okno.ID.Index].Value != null)
                {
                    int index = (int)okno.dataGridView1.SelectedRows[0].Cells[okno.ID.Index].Value;
                    okno.textBox1.Text = index.ToString() + " RysujOB";

                    rysuj();

                    foreach (Obiekt O in M.lista)
                    {
                        if (O.ID == index)
                        {
                            O.rysujOB(sender, e);
                            break;
                        }
                    }

                }
            }
        }


        private void clear(object sender, PaintEventArgs e)
        {
            e.Graphics.FillRectangle(Brushes.White,
                new Rectangle((int)0, (int)0, (int)matrycaPBox.pictureBox1.Width, (int)matrycaPBox.pictureBox1.Height));
        }


        internal void Chmurka(MouseEventArgs e)
        {
            int x = e.X;
            int y = e.Y;


            okno.textBox1.Text = "Chmurka" + x + " " + y;
            //matrycaPBox.richTextBox_Dymek.Visible = false;            
            Prostokat szukany = M.szukaj_obiektuXY(x, y);
            if (szukany != null)
            {

                int id = szukany.ID;
                okno.textBox1.Text = szukany.ID.ToString();
                okno.richTextBoxChmurka.Text =
                   "ID: " + id + "\tNazwa: " + szukany.Name
                  + "\tPunkt zaczepienia. \tX: " + szukany.punkt_zaczepienia.x
                  + "\tY: " + szukany.punkt_zaczepienia.y + "\tW: " + szukany.W
                  + "\tH: " + szukany.H;
                matrycaPBox.richTextBox_Dymek.Text = okno.richTextBoxChmurka.Text;

                /*  matrycaPBox.richTextBox_Dymek.Location = new Point(szukany.X, szukany.Y);                
                  matrycaPBox.richTextBox_Dymek.Text = "ID:" + id + "\nNazwa: " + szukany.Name;
                  matrycaPBox.richTextBox_Dymek.Invalidate();
                  matrycaPBox.richTextBox_Dymek.Visible = true; */
            }
            else
            {
                okno.richTextBoxChmurka.Text = "";
                matrycaPBox.richTextBox_Dymek.Text = "";
            }
            //matrycaPBox.Refresh();
        }

    }
}
